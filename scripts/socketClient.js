var ws = null;
function openClick() {
    if (!window.WebSocket){
        console.log("This browser does not support WebSocket.");
        return;
    }
    var url="ws://localhost:8880/";
    //url="ws://nobita:password@localhost:8880/";//验证
    //var ws = new WebSocket("ws://my_token@example.com/service");
    ws = new WebSocket(url, 'echo-protocol');

    ws.onopen = function (evt) {
        console.log("ws Connection open ...");
        ws.send("Hello world!");
        console.log(this.readyState);
    };

    ws.onmessage = function (evt) {
        console.log("Client Received Message: " + evt.data);
        //ws.close();
    };

    ws.onclose = function (evt) {
        console.log("Connection closed.");
    };
}
//发送
function sendClick() {
    if(ws==null)return;
    if (ws.readyState == ws.OPEN) {
        var content = document.getElementById('txt1').value;
        //如果发送缓冲区没有数据才继续发送
        console.log('bufferedAmount is '+ws.bufferedAmount);
        if (ws.bufferedAmount <= 0) {
            ws.send(content);
        }
    }else{
        console.log("Connection has closed.");
    }
}
//关闭
/*
* 关闭链接说明，在IE浏览器下还会触发 onerror事件
*  WebSocket Error: Network Error 12030, 与服务器的连接意外终止
*/
function closeClick() {
    //默认关闭代码 1006
    if(ws==null)return;
    ws.close();
    ws = null;
    //Uncaught InvalidAccessError: Failed to execute 'close' on 'WebSocket': The code must be either 1000, or between 3000 and 4999. 1006 is neither.
    //ws.close(3000,'客户端主动关闭');
}
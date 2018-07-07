var client = new W3CWebSocket('ws://localhost:8880/', 'echo-protocol');
    
ws.onopen = function() {    
   alert("Opened");    
   ws.send("I'm client");    
};    
    
ws.onmessage = function (evt) {     
    alert(evt.data);    
};    
    
ws.onclose = function() {    
   alert("Closed");    
};    
    
ws.onerror = function(err) {    
   alert("Error: " + err);    
};
/// <reference
///     path="../Scripts/jquery-1.6.4.js" />
/// <reference
///     path="../Scripts/jquery.signalR-2.2.0.js" />

$(function() {
    var updater = $.connection.updateHandler;

    updater.client.processCommand = function(command) {
        if (command.CommandType === "WindowUpdate") {
            alert("A window update has been received!");
        } else {
            alert("An input update has been received!");
        }
    };

    $.connection.hub.logging = true;
    $.connection.hub.start("~/signalr");
});
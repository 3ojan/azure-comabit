var messageHubConnection;

document.addEventListener('DOMContentLoaded', function () {

    //// Get the user name and store it to prepend to messages.
    //var username = $('div#sender').data('name');

    //// Set initial focus to message input box.
    //var messageInput = document.getElementById('message');
    //messageInput.focus();

    //function createMessageEntry(encodedName, encodedMsg) {
    //    return encodedName + ': ' + encodedMsg;
    //}

    function bindConnectionMessage(connection) {
        var receiveMessageCallback = function (name, message) {
            if (!message) {
                console.log("empty message");
                return;
            }
            // Html encode display name and message.
            var encodedName = name;
            var encodedMsg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
            var messageEntry = createMessageEntry(encodedName, encodedMsg);

            console.log(messageEntry);
        };

        connection.on('ReceiveMessage', receiveMessageCallback);

        var notificationCallback = function (totalNewMessages) {
            console.log("Notification Update: " + totalNewMessages);
        };
        connection.on('ReceiveNotificationUpdate', notificationCallback);
    }

    function onConnected(connection) {
        console.log('connection started');
        ////connection.send('BroadcastMessage', '_SYSTEM_', username + ' JOINED');
        //document.getElementById('message').addEventListener('keypress', function (event) {
        //    if (event.keyCode === 13) {
        //        event.preventDefault();

        //        // Call the method on the hub.

        //        if (messageInput.value) {

        //            if ($('#receiver').val()) {
        //                console.log("JS SendMessage ", username, messageInput.value);
        //                connection.send('SendMessage', $('#receiver').val(), username, messageInput.value);
        //            }
        //            else {
        //                console.log("JS BroadcastMessage ", username, messageInput.value);
        //                connection.send('BroadcastMessage', username, messageInput.value);
        //            }
        //        }

        //        // Clear text box and reset focus for next comment.
        //        messageInput.value = '';
        //        messageInput.focus();
        //        event.preventDefault();

        //        return false;
        //    }
        //});
    }

    messageHubConnection = new signalR.HubConnectionBuilder()
        .withUrl('/message')
        .configureLogging(signalR.LogLevel.Information)
        .build();

    bindConnectionMessage(messageHubConnection);

    async function start() {
        try {
            await messageHubConnection.start()
                .then(function () {
                    onConnected(messageHubConnection);
                })
                .catch(function (error) {
                    console.error(error.message);
                });;            
        } catch (err) {
            console.log(err);
            setTimeout(() => start(), 5000);
        }
    };

    messageHubConnection.onclose(async (error) => {

        if (error && error.message) {
            console.error(error.message);
        }

        await start();
    });

    start();
});
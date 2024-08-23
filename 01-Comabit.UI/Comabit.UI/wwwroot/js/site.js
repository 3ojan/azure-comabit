// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$.fn.serializeObject = function () {
    var o = {};
    var a = this.serializeArray();

    $.each(a, function () {
        var value = this.value;
        if (o[this.name] !== undefined) {
            // Hack für .net MVC - zweites hidden Field bei
            // checkboxes kann ignoriert werden, wenn true:
            if (o[this.name] !== "true") {
                if (!o[this.name].push) {
                    o[this.name] = [o[this.name]];
                }
                o[this.name].push(value || '');
            }
        } else {
            o[this.name] = value || '';
        }
    });
    return o;
};

$.fn.serializeWithFiles = function () {
    var form = $(this),
        formData = new FormData(),
        formParams = form.serializeArray();

    $.each(form.find('input[type="file"]'), function (i, tag) {
        $.each($(tag)[0].files, function (i, file) {
            formData.append(tag.name, file);
        });
    });

    $.each(formParams, function (i, val) {
        formData.append(val.name, val.value);
    });

    return formData;
};

$.fn.submitCallback = function () {
    return false;
};

$("body").on("submit", "form.modal-form", function () {
    var form = $(this);
    var url = $(this).attr("action");

    if ($(this).hasClass("form-with-files")) {
        var dataJson = $(this, document).serializeWithFiles();
    }
    else {
        var dataJson = $(this).serializeObject();
    }

    console.log(dataJson);

    $.ajax({
        url: url,
        type: 'POST',
        data: dataJson,
        //processData: false,
        //contentType: false,
        beforeSend: function () {
            $(this).find('.modal-content').html("<div class='spinner-border text-dark me-2' role='status'><span class='visually-hidden'>Loading...</span></div>");
        },
        success: function (result) {
            if (result.status == "ok") {
                var modalId = form.closest(".modal").attr('id');
                $('#' + modalId).modal("hide");

                $.fn.submitCallback(result.html);
            }
            else {
                $(this).find('.modal-content').html(result.html);
            }
        },
        error: function (result) {
            $(this).find('.modal-content').html('Es ist ein Fehler aufgetreten.');
        }
    });

    return false;
});

$("body").on("click", ".tr-link", function () {


    var url = $(this).data("href");

    window.location.href = url;

    return false;
});

$("body").on("click", ".dropdown, td a", function (e) {
    e.stopPropagation();
});

// ------------------------------------------------------------
// Chat Messages
// ------------------------------------------------------------

/**
 * Creates Message tag and contents
 *
 * @param {message} object Message View Model
 */
var getChatMessageHtml = function (message) {
    var messageTag, messageBoxTag, messageTitle, messageText;

    if (message.isUserMessage) {
        messageTag = $('<div data-id="' + message.id + '" data-receiverid="' + message.receiverId + '" class="chat-message ' + (message.isOwnMessage ? 'chat-message-right' : 'chat-message-left') + ' ' + (message.isRead ? 'read' : 'unread text-success') + ' pb-4"></div>');
        messageBoxTag = $('<div class="flex-shrink-1 bg-light rounded py-2 px-3 me-3"></div>');
        messageTitle = $('<div class="text-muted small mb-1"></div>').text((message.isOwnMessage ? 'Sie' : message.userName) + ' ' + (message.dateIsToday ? message.hour : message.date + ' ' + message.hour));
        messageText = $('<span></span>').text(message.text);
    } else {
        messageTag = $('<div class="pb-4 text-center"></div>');
        messageBoxTag = $('<div class="flex-shrink-1 py-2 px-3 me-3"></div>');
        messageTitle = $('<div class="text-muted small mb-1"></div>').text(message.date + ' ' + message.hour);
        messageText = $('<span class="badge bg-success"></span>').text(message.typeDescription);
    }

    messageTag.append(messageBoxTag.append(messageTitle).append(messageText));

    return messageTag;
}

/**
 * Returns the text from a HTML string
 * 
 * @param {html} String The html string
 */
function stripHtml(html) {
    var temporalDivElement = document.createElement("div");
    temporalDivElement.innerHTML = html;
    return temporalDivElement.textContent || temporalDivElement.innerText || "";
}
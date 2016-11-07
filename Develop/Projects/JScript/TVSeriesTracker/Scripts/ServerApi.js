function ServerApi(baseUrl, apiKey) {
    var self = this;

    self.onError = function (jqXHR, textStatus, errorThrown) {
        console.log("error " + textStatus);
        console.log("error " + errorThrown);
        alert(errorThrown);
        alert(textStatus);
    };


    self.getLists = function (onSuccess) {
        var url = baseUrl + "user/lists.json/" + apiKey + "/" + escape(usr);
        $.ajax({
            url: url,
            type: 'GET',
            crossDomain: true,
            dataType: 'jsonp',
            success: onSuccess,
            error: self.onError,
            beforeSend: function (xhr) {
    xhr.setRequestHeader('Accept', 'text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8');
    xhr.setRequestHeader('Accept-Encoding', 'deflate');
}
        });
    };

    self.doLogin = function (username, password, onSuccess) {

        var url = baseUrl + "account/test/" + apiKey;
        $.support.cors = true;
        $.ajax({
            url: url,
            type: 'POST',
            dataType: 'json',
            crossDomain: true,
            success: onSuccess,
            error: self.onError,
            data: { username: username, password: password },
            beforeSend: function (xhr) {
                xhr.setRequestHeader('Accept', 'text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8');
                xhr.setRequestHeader('Accept-Encoding', 'deflate');
            }
        });

    };
}
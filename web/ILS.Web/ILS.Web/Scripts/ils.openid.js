Ext.ns('ils', 'ils.openid');



if (Ext.util.Cookies.get("language") == null) Ext.util.Cookies.set("language", "Russian");
if (Ext.util.Cookies.get("language") == "Russian") isRussian = true; else isRussian = false;
ils.openid.panelName = 'Login via OpenID';
if (isRussian)
{
	ils.openid.panelName = 'Вход через OpenID';
}


function parseURLParams(url) {
  var queryStart = url.indexOf("?") + 1;
  var queryEnd   = url.indexOf("#") + 1 || url.length + 1;
  var query      = url.slice(queryStart, queryEnd - 1);

  if (query === url || query === "") return;

  var params  = {};
  var nvPairs = query.replace(/\+/g, " ").split("&");

  for (var i=0; i<nvPairs.length; i++) {
    var nv = nvPairs[i].split("=");
    var n  = decodeURIComponent(nv[0]);
    var v  = decodeURIComponent(nv[1]);
    if ( !(n in params) ) {
      params[n] = [];
    }
    params[n].push(nv.length === 2 ? v : null);
  }
  return params;
}

/*

Ext.onReady(function () {
    ils.admin.logon = new Ext.Window({
        title: ils.openid.panelName,
        layout: 'fit',
        width: 400,
        height: 150,
        y: 150,
        closable: false,
        resizable: false,
        draggable: false,
        plain: true,
        border: false,
        items: new Ext.Panel({

            bodyStyle: { "background-color": "#DFE8F6" },
            items: [{
                xtype: 'displayfield',
                value: 'Вход через сайт Школы Информатики<br>',
                x: 95,
                y: 10
            },
						{
						    xtype: 'button',
						    height: 40,
						    width: 150,
						    text: 'Войти',
						    x: 125,
						    y: 25,
						    handler: function () {
						        if (document.location.href.indexOf("http://") == -1) {
						            document.location.href = "http://www.ischool-ssau.ru/Account/LogOn/?extUrl=" + document.location.href;
						        }
						        else {
						            document.location.href = "http://www.ischool-ssau.ru/Account/LogOn/?extUrl=" + document.location.href.slice(7);
						        }

						    }
						}]
        }
			)
    });

    ils.admin.success = new Ext.Window({
        title: ils.openid.panelName,
        layout: 'fit',
        width: 400,
        height: 150,
        y: 150,
        closable: false,
        resizable: false,
        draggable: false,
        plain: true,
        border: false,
        items: new Ext.Panel({

            bodyStyle: { "background-color": "#DFE8F6" },
            items: [{
                xtype: 'displayfield',
                value: 'Вход успешно произведен.',
                x: 110,
                y: 10
            },
						{
						    xtype: 'displayfield',
						    value: 'Вы будете перенаправлены на главную страницу.',
						    x: 60,
						    y: 10
						},
						]
        })
    });

    ils.admin.failure = new Ext.Window({
        title: ils.openid.panelName,
        layout: 'fit',
        width: 400,
        height: 150,
        y: 150,
        closable: false,
        resizable: false,
        draggable: false,
        plain: true,
        border: false,
        items: new Ext.Panel({

            bodyStyle: { "background-color": "#DFE8F6" },
            items: [{
                xtype: 'displayfield',
                value: 'Произошла неизвестная ошибка.',
                x: 100,
                y: 10
            },
						{
						    xtype: 'displayfield',
						    value: 'Попробуйте еще раз.',
						    x: 135,
						    y: 10
						},
						{
						    xtype: 'button',
						    height: 40,
						    width: 150,
						    text: 'Войти',
						    x: 125,
						    y: 15,
						    handler: function () {
						        var needSlice = (document.location.href.indexOf("http://") != -1);
						        var url = document.location.href.slice(0, document.location.href.indexOf("?"));
						        document.location.href = "http://www.ischool-ssau.ru/Account/LogOn/?extUrl=" + (needSlice ? url.slice(7) : url);
						    }
						}]
        }
			)
    });

*/

    var urlParams = parseURLParams(document.URL);
    if (urlParams != null && urlParams['key'] != null && urlParams['login'] != null && urlParams['hash'] != null && /*urlParams['firstName'] != null && urlParams['lastName'] != null && */urlParams['email'] != null) {
        Ext.Ajax.request({
            url: document.location.href.slice(0, document.location.href.indexOf("?")),
            success: function (response, opts) {
                var a = eval('(' + response.responseText + ')');
                if (a.success) {
                    //ils.admin.success.show();
                    var message = new Object();
                    message.header = "authorise";
                    message.firstName = a.firstName == null ? "Имя" : a.firstName;
                    message.lastName = a.lastName == null ? "Фамилия" : a.lastName;
                    message.admin = a.admin;
                    message.success = true;
                    window.opener.postMessage(JSON.stringify(message), '*');
                }
                else {
                    //ils.admin.failure.show();
                    var message = new Object();
                    message.header = "authorise";
                    message.firstName = "";
                    message.lastName = "";
                    message.admin = false;
                    message.success = false;
                    window.opener.postMessage(JSON.stringify(message), '*');

                }
                window.open('', '_parent', '');
                setTimeout(window.close, 250);
            },
            failure: function (response, opts) {
                ils.admin.failure.show();
                window.open('', '_parent', '');
                setTimeout(window.close, 250);
            },
            jsonData: [{
                'login': urlParams['login'][0],
                'hash': urlParams['hash'][0],
                'firstName': urlParams['firstName'] == null ? "" : urlParams['firstName'][0],
                'lastName': urlParams['lastName'] == null ? "" : urlParams['lastName'][0],
                'email': urlParams['email'][0],
                'key': urlParams['key'][0]
            }
				]
        });
    }
    else {
        //ils.admin.logon.show();
    }
//});






//ФУНКЦИИ, ВЫЗЫВАЕМЫЕ ИЗНУТРИ ЮНИТИ

function UnityLoaded() {
    document.getElementById("unityPlayer").style.height = "400px";
    document.getElementById("unityPlayer").style.width = "810px";
    document.getElementById("updater").style.display = 'none';
}

function GetLanguage() {
    var unity = unityObject.getObjectById("unityPlayer");
    if (isRussian) unity.SendMessage("Bootstrap", "SetLanguage", 0);
    else unity.SendMessage("Bootstrap", "SetLanguage", 1);
}

function GetLanguageCust() {
    var unity = unityObject.getObjectById("unityPlayer");
    if (isRussian) unity.SendMessage("_Customization", "SetLanguage", 0);
    else unity.SendMessage("_Customization", "SetLanguage", 1);
}

function LoadRPG() {
    Ext.Ajax.request({
        url: link_unityRPG,
        method: 'POST',
        success: function (responseObject) {
            var unity = unityObject.getObjectById("unityPlayer");
            unity.SendMessage("Bootstrap", "RoleSystemSet", responseObject.responseText);
        }
    });
}

function SaveRPG(s) {
    Ext.Ajax.request({
        url: link_unitysaveRPG,
        params: { s: s },
        method: 'POST'
    });
}

function LoadCoursesList() {
    Ext.Ajax.request({
        url: link_unitylist,
        method: 'POST',
        success: function (responseObject) {
            var unity = unityObject.getObjectById("unityPlayer");
            unity.SendMessage("CS_Screen", "CourseDisplay", responseObject.responseText);
        }
    });
}

function LoadCourseData(id) {
    var flag = false; var s = "";
    Ext.Ajax.request({
        url: link_unitydata,
        params: { id: id },
        method: 'POST',
        success: function (responseObject) {
            if (!flag) {
                flag = true; s = responseObject.responseText;
            } else {
                var unity = unityObject.getObjectById("unityPlayer");
                unity.SendMessage("Bootstrap", "CourseConstructor", responseObject.responseText);
                unity.SendMessage("Bootstrap", "StatisticDisplay", s);
            }
        }
    });
    Ext.Ajax.request({
        url: link_unitystat,
        params: { id: id },
        method: 'POST',
        success: function (responseObject) {
            if (!flag) {
                flag = true; s = responseObject.responseText;
            } else {
                var unity = unityObject.getObjectById("unityPlayer");
                unity.SendMessage("Bootstrap", "CourseConstructor", s);
                unity.SendMessage("Bootstrap", "StatisticDisplay", responseObject.responseText);
            }
        }
    });
}

function SaveStatistic(s) {
    Ext.Ajax.request({
        url: link_unitysave,
        params: { s: s },
        method: 'POST'
    });
}

function SaveTestResult(mode, id1, id2, a, t) {
    Ext.Ajax.request({
        url: link_unitytest,
        params: { mode: mode, theme_run_id: id1, test_id: id2, answers: a, time: t },
        method: 'POST',
        success: function (responseObject) {
            var unity = unityObject.getObjectById("unityPlayer");
            var res = parseInt(responseObject.responseText, 10);
            unity.SendMessage("FinishTestObject_" + id2, "DisplayResults", res);
        }
    });
}
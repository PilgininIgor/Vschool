Ext.ns('ils', 'ils.aboutadmin');

Ext.require('Ext.ux.grid.*');

if (Ext.util.Cookies.get("language") == null) Ext.util.Cookies.set("language", lang_pref);
if (Ext.util.Cookies.get("language") == "Russian") isRussian = true; else isRussian = false;
ils.aboutadmin.title = 'About';
if (isRussian) {
    ils.aboutadmin.title = 'О проекте';
}

Ext.onReady(function () {

    ils.aboutadmin.TabPanel = Ext.create('Ext.tab.Panel', {
        title: ils.aboutadmin.title,
        activeTab: 0,
        layout: 'fit',
        layoutConfig: {
            columns: 1
        },
        bodyStyle: "border: none;",
        width: 600,
        height: 600,
        items: [
                EDucationAuthorGrid,
                ScreenGrid,
				AwardGrid,
				AchievementGrid
        ],
        renderTo: Ext.getBody()
    });
	
	renderToMainArea(ils.aboutadmin.TabPanel);

});



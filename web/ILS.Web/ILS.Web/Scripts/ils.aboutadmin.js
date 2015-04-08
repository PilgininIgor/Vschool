Ext.ns('ils', 'ils.aboutadmin');

Ext.require('Ext.ux.grid.*');


Ext.onReady(function () {

    ils.aboutadmin.TabPanel = Ext.create('Ext.tab.Panel', {
        activeTab: 0,
        layout: 'fit',
        layoutConfig: {
            columns: 1
        },
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



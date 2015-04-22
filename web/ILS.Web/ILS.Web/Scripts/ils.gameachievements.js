Ext.ns('ils', 'ils.gameachievements');

Ext.require('Ext.ux.grid.*');

if (Ext.util.Cookies.get("language") == null) {
    Ext.util.Cookies.set("language", lang_pref);
}
if (Ext.util.Cookies.get("language") === "Russian") {
    isRussian = true;
} else {
    isRussian = false;
}

ils.gameachievements.textName = 'Название';
ils.gameachievements.textImagePath = 'Иконка';
ils.gameachievements.textMessage = 'Сообщение';
ils.gameachievements.textIndex = 'Номер';
ils.gameachievements.textPriority = 'Приоритет';
ils.gameachievements.textScore = 'Награда';
ils.gameachievements.textAdditionalParameters = 'Доп. параметры';
ils.gameachievements.textAchievementExecutor = 'Класс';
ils.gameachievements.gridName = 'Список достижений';
ils.gameachievements.textRemove = 'Удалить достижение';
ils.gameachievements.textAdd = 'Добавить достижение';
ils.gameachievements.alert = 'Информация';
ils.gameachievements.alertText = 'Не выбрана ни одна строка';
ils.gameachievements.promptRemove = 'Удаление достижения';
ils.gameachievements.promptRemoveText = 'Вы уверены?';

Ext.define('GameAchievementModel', {
    extend: 'Ext.data.Model',
    fields: [{
        name: 'Name',
        type: 'string'
    }, {
        name: 'ImagePath',
        type: 'string'
    }, {
        name: 'Message',
        type: 'string'
    }, {
        name: 'Index',
        type: 'int'
    }, {
        name: 'Priority',
        type: 'int'
    }, {
        name: 'Score',
        type: 'int'
    }, {
        name: 'AdditionalParameters',
        type: 'string'
    }, {
        name: 'AchievementExecutor',
        type: 'string'
    }]
});

ils.gameachievements.store = new Ext.data.Store({
    extend: 'Ext.data.Store',
    autoLoad: true,
    autoSync: true,
    model: GameAchievementModel,
    proxy: {
        type: 'ajax',
        url: ils.gameachievements.getAchievements,
        api: {
            read: ils.gameachievements.readAchievement,
            create: ils.gameachievements.createAchievement,
            update: ils.gameachievements.updateAchievement,
            destroy: ils.gameachievements.deleteAchievement
        },
        reader: new Ext.data.JsonReader({
            totalProperty: 'total',
            rootProperty: 'data'
        }, GameAchievementModel),
        writer: new Ext.data.JsonWriter({
            encode: false,
            listful: true,
            writeAllFields: true
        }),
        headers: { 'Content-Type': 'application/json; charset=UTF-8' },
        autoSave: true,
        autoLoad: true,
        remoteSort: false
    }
});

ils.gameachievements.achievementProfile = new Ext.Window({
    title: ils.gameachievements.textProfile,
    layout: 'fit',
//    width: 300,
//    height: 270,
    y: 150,
    draggable: false,
    plain: true,
    border: false,
    items: new Ext.FormPanel({
        frame: false,
        border: false,
        cls: 'white-form-panel',
        defaultType: 'textfield',
        padding: "10 0 0 0",
        fieldDefaults: {
            padding: "0 10 2 10",
            labelWidth: 100,
            msgTarget: 'side',
            width: 600
        },
        bodyStyle: { "background-color": "#FFFFFF" },
        items: [{
            fieldLabel: ils.gameachievements.textName,
            xtype: 'textfield',
            name: 'Name',
            id: 'Name',
            y: 5,
            x: 5
        }, {
            fieldLabel: ils.gameachievements.textImagePath,
            xtype: 'textfield',
            name: 'ImagePath',
            id: 'ImagePath',
            y: 5,
            x: 5
        }, {
            fieldLabel: ils.gameachievements.textMessage,
            xtype: 'textfield',
            name: 'Message',
            id: 'Message',
            y: 5,
            x: 5
        }, {
            fieldLabel: ils.gameachievements.textIndex,
            xtype: 'textfield',
            name: 'Index',
            id: 'Index',
            y: 5,
            x: 5
        }, {
            fieldLabel: ils.gameachievements.textPriority,
            xtype: 'textfield',
            name: 'Priority',
            id: 'Priority',
            y: 5,
            x: 5
        }, {
            fieldLabel: ils.gameachievements.textScore,
            xtype: 'textfield',
            name: 'Score',
            id: 'Score',
            y: 5,
            x: 5
        }, {
            fieldLabel: ils.gameachievements.textAdditionalParameters,
            xtype: 'textfield',
            name: 'AdditionalParameters',
            id: 'AdditionalParameters',
            y: 5,
            x: 5
        }, {
            fieldLabel: ils.gameachievements.textAchievementExecutor,
            xtype: 'textfield',
            name: 'AchievementExecutor',
            id: 'AchievementExecutor',
            y: 5,
            x: 5
        }, {
            fieldLabel: 'OK',
            name: 'update',
            xtype: 'button',
            y: 5,
            x: 235,
            width: 45,
            value: 'OK',
            text: 'OK'
        }]
    })
});

ils.gameachievements.achievementGrid = new Ext.grid.GridPanel({
    store: ils.gameachievements.store,
    columns: [
    {
        header: ils.gameachievements.textName,
        dataIndex: 'Name',
        width: 150,
        sortable: true,
        editor: {
            xtype: 'textfield',
            allowBlank: false
        }
    }, {
        header: ils.gameachievements.textImagePath,
        dataIndex: 'ImagePath',
        width: 150,
        sortable: true,
        editor: {
            xtype: 'textfield',
            allowBlank: false
        }
    }, {
        header: ils.gameachievements.textMessage,
        dataIndex: 'Message',
        width: 150,
        sortable: true,
        editor: {
            xtype: 'textfield',
            allowBlank: false
        }
    }, {
        header: ils.gameachievements.textIndex,
        dataIndex: 'Index',
        width: 250,
        sortable: true,
        editor: {
            xtype: 'textfield',
            allowBlank: false
        }
    }, {
        header: ils.gameachievements.textPriority,
        dataIndex: 'Priority',
        width: 250,
        sortable: true,
        editor: {
            xtype: 'textfield',
            allowBlank: false
        }
    }, {
        header: ils.gameachievements.textScore,
        dataIndex: 'Score',
        width: 250,
        sortable: true,
        editor: {
            xtype: 'textfield',
            allowBlank: false
        }
    }, {
        header: ils.gameachievements.textAdditionalParameters,
        dataIndex: 'AdditionalParameters',
        width: 250,
        sortable: true,
        editor: {
            xtype: 'textfield',
            allowBlank: false
        }
    }, {
        header: ils.gameachievements.textAchievementExecutor,
        dataIndex: 'AchievementExecutor',
        width: 250,
        sortable: true,
        editor: {
            xtype: 'textfield',
            allowBlank: false
        }
    }],
    listeners: {
        itemdblclick: function () {
            var grid = this;
            var s = grid.getSelectionModel().getSelection();
            if (!s.length) {
                Ext.Msg.alert(ils.gameachievements.alert, ils.gameachievements.alertText);
                return;
            }
            var index = grid.store.indexOf(s[0]);
            Ext.Ajax.request({
                url: document.location.href + '/ReadAchievement',
                success: function (response, opts) {
                    var a = eval('(' + response.responseText + ')');
                    ils.gameachievements.achievementProfile.show();
                },
                jsonData: {
                    'login': ils.gameachievements.store.data.items[index].data.Name

                }
            });

        }
    }
});

Ext.onReady(function () {
    ils.gameachievements.layout = new Ext.Panel({
        title: ils.gameachievements.gridName,
        layout: 'fit',
        layoutConfig: {
            columns: 1
        },
        width: 600,
        height: 600,
        items: [ils.gameachievements.achievementGrid],
        dockedItems: [
            new Ext.panel.Panel({
                bodyStyle: { "background-color": "#157fcc" },
                defaults: {
                    margin: "4 0 0 4",
                    padding: "2 8 2 8"
                },
                items: [{
                    xtype: 'button',
                    iconCls: 'add',
                    text: ils.gameachievements.textAdd,
                    handler: function () {
                        ils.gameachievements.achievementProfile.show();
                    }                            
                    }, {
                        xtype: 'button',
                        ref: '../removeBtn',
                        iconCls: 'remove',
                        text: ils.gameachievements.textRemove,
                        handler: function () {
                            var grid = this.up('grid');
                            var s = grid.getSelectionModel().getSelection();
                            if (!s.length) {
                                Ext.Msg.alert(ils.gameachievements.alert, ils.gameachievements.alertText);
                                return;
                            }
                            Ext.Msg.confirm(ils.gameachievements.promptRemove, ils.gameachievements.promptRemoveText, function (button) {
                                if (button == 'yes') {
                                    ils.gameachievements.store.remove(s[0]);
                                }
                            });
                        }
                    }]
            })
        ]
    });

    renderToMainArea(ils.gameachievements.layout);

});
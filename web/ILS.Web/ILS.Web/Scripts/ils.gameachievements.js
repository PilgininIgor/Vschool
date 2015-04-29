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
ils.gameachievements.promptRemoveText = 'Вы уверены, что хотите удалить достижение?';
ils.gameachievements.ok = 'OK';
ils.gameachievements.cancel = 'Отмена';
ils.gameachievements.achievemntCreation = 'Создание достижения';
ils.gameachievements.achievemntEdition = 'Изменение достижения';

Ext.define('GameAchievementModel', {
    extend: 'Ext.data.Model',
    fields: [{
        name: 'GameAchievementId',
        type: 'string'
    }, {
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

window.ils.gameachievements.store = new Ext.data.Store({
    extend: 'Ext.data.Store',
    autoLoad: true,
    model: GameAchievementModel,
    proxy: {
        type: 'ajax',
        url: window.ils.gameachievements.getAchievements,
        api: {
            read: window.ils.gameachievements.getAchievements,
            create: window.ils.gameachievements.createAchievement,
            update: window.ils.gameachievements.updateAchievement,
            destroy: window.ils.gameachievements.deleteAchievement
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

function createAchievementProfileWindow(isCreate) {
    var title, buttons;
    if (isCreate) {
        title = window.ils.gameachievements.achievemntCreation;
        buttons = [
            {
                text: window.ils.gameachievements.ok,
                formBind: false,
                handler: function() {
                    var profileItem = achievementProfile.items.items[0];
                    var newItem =
					{
					    Name: profileItem.getComponent('Name').getValue(),
					    ImagePath: profileItem.getComponent('ImagePath').getValue(),
					    Message: profileItem.getComponent('Message').getValue()
					};

                    window.ils.gameachievements.store.add(newItem);
                    window.ils.gameachievements.store.sync();
                    window.ils.gameachievements.store.reload();
                    achievementProfile.close();
                }

            }, {
                text: window.ils.gameachievements.cancel,
                formBind: false,
                handler: function() {
                    achievementProfile.close();
                }
            }
        ];
    } else {
        title = window.ils.gameachievements.achievemntEdition;
        buttons = [
            {
                text: window.ils.gameachievements.ok,
                formBind: false,
                handler: function () {
                    achievementProfile.close();
                }
            }, {
                text: window.ils.gameachievements.cancel,
                formBind: false,
                handler: function () {
                    achievementProfile.close();
                }
            }
        ];
    }

    var achievementProfile = new Ext.Window({
        title: title,
        layout: 'fit',
        width: 440,
        height: 360,
        y: 150,
        modal: true,
        dragable: true,
        plain: true,
        resizable: false,
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
                width: 430
            },
            bodyStyle: { "background-color": "#FFFFFF" },
            items: [{
                fieldLabel: window.ils.gameachievements.textName,
                xtype: 'textfield',
                name: 'Name',
                id: 'Name'
            }, {
                fieldLabel: window.ils.gameachievements.textImagePath,
                xtype: 'textfield',
                name: 'ImagePath',
                id: 'ImagePath'
            }, {
                fieldLabel: window.ils.gameachievements.textMessage,
                xtype: 'textfield',
                name: 'Message',
                id: 'Message'
            }, {
                fieldLabel: window.ils.gameachievements.textIndex,
                xtype: 'textfield',
                name: 'Index',
                id: 'Index'
            }, {
                fieldLabel: window.ils.gameachievements.textPriority,
                xtype: 'textfield',
                name: 'Priority',
                id: 'Priority'
            }, {
                fieldLabel: window.ils.gameachievements.textScore,
                xtype: 'textfield',
                name: 'Score',
                id: 'Score'
            }, {
                fieldLabel: window.ils.gameachievements.textAdditionalParameters,
                xtype: 'textfield',
                name: 'AdditionalParameters',
                id: 'AdditionalParameters'
            }, {
                fieldLabel: window.ils.gameachievements.textAchievementExecutor,
                xtype: 'textfield',
                name: 'AchievementExecutor',
                id: 'AchievementExecutor'
            }],
            buttons: buttons
        })
    });

    achievementProfile.show();
}
window.ils.gameachievements.achievementGrid = new Ext.grid.GridPanel({
    store: window.ils.gameachievements.store,
    columns: [
    {
        header: window.ils.gameachievements.textName,
        dataIndex: 'Name',
        width: 150,
        sortable: true,
        editor: {
            xtype: 'textfield',
            allowBlank: false
        }
    }, {
        header: window.ils.gameachievements.textImagePath,
        dataIndex: 'ImagePath',
        width: 150,
        sortable: true,
        editor: {
            xtype: 'textfield',
            allowBlank: false
        }
    }, {
        header: window.ils.gameachievements.textMessage,
        dataIndex: 'Message',
        width: 150,
        sortable: true,
        editor: {
            xtype: 'textfield',
            allowBlank: false
        }
    }, {
        header: window.ils.gameachievements.textIndex,
        dataIndex: 'Index',
        width: 250,
        sortable: true,
        editor: {
            xtype: 'textfield',
            allowBlank: false
        }
    }, {
        header: window.ils.gameachievements.textPriority,
        dataIndex: 'Priority',
        width: 250,
        sortable: true,
        editor: {
            xtype: 'textfield',
            allowBlank: false
        }
    }, {
        header: window.ils.gameachievements.textScore,
        dataIndex: 'Score',
        width: 250,
        sortable: true,
        editor: {
            xtype: 'textfield',
            allowBlank: false
        }
    }, {
        header: window.ils.gameachievements.textAdditionalParameters,
        dataIndex: 'AdditionalParameters',
        width: 250,
        sortable: true,
        editor: {
            xtype: 'textfield',
            allowBlank: false
        }
    }, {
        header: window.ils.gameachievements.textAchievementExecutor,
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
                Ext.Msg.alert(window.ils.gameachievements.alert, window.ils.gameachievements.alertText);
                return;
            }
            var index = grid.store.indexOf(s[0]);
            Ext.Ajax.request({
                url: window.ils.gameachievements.getAchievements,
                success: function (response, opts) {
                    var a = eval('(' + response.responseText + ')');
                    createAchievementProfileWindow();
                },
                jsonData: {
                    'login': window.ils.gameachievements.store.data.items[index].data.Name
                }
            });

        }
    }
});

Ext.onReady(function () {
    window.ils.gameachievements.layout = new Ext.Panel({
        title: window.ils.gameachievements.gridName,
        layout: 'fit',
        layoutConfig: {
            columns: 1
        },
        items: [window.ils.gameachievements.achievementGrid],
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
                        text: window.ils.gameachievements.textAdd,
                        handler: function () {
                            createAchievementProfileWindow(true);
                        }                            
                    }, {
                        xtype: 'button',
                        ref: '../removeBtn',
                        iconCls: 'remove',
                        text: window.ils.gameachievements.textRemove,
                        handler: function () {
                            var selection = window.ils.gameachievements.achievementGrid.getSelectionModel().getSelection();
                            if (!selection.length) {
                                Ext.Msg.alert(window.ils.gameachievements.alert, window.ils.gameachievements.alertText);
                                return;
                            }
                            Ext.Msg.confirm(window.ils.gameachievements.promptRemove, window.ils.gameachievements.promptRemoveText, function (button) {
                                if (button == 'yes') {
                                    window.ils.gameachievements.store.remove(selection[0]);
                                    window.ils.gameachievements.store.sync();
                                    window.ils.gameachievements.store.reload();
                                }
                            });
                        }
                    }]
            })
        ]
    });

    renderToMainArea(window.ils.gameachievements.layout);

});
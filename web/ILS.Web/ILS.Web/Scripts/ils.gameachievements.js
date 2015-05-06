Ext.ns("ils", "ils.gameachievements");

Ext.require("Ext.ux.grid.*");

if (Ext.util.Cookies.get("language") == null) {
    Ext.util.Cookies.set("language", lang_pref);
}
if (Ext.util.Cookies.get("language") === "Russian") {
    isRussian = true;
} else {
    isRussian = false;
}

ils.gameachievements.textName = "Название";
ils.gameachievements.textImagePath = "Иконка";
ils.gameachievements.textMessage = "Сообщение";
ils.gameachievements.textIndex = "Номер";
ils.gameachievements.textPriority = "Приоритет";
ils.gameachievements.textScore = "Награда";
ils.gameachievements.textAdditionalParameters = "Доп. параметры";
ils.gameachievements.textAchievementExecutor = "Класс";
ils.gameachievements.gridName = "Список достижений";
ils.gameachievements.textRemove = "Удалить достижение";
ils.gameachievements.textAdd = "Добавить достижение";
ils.gameachievements.alert = "Информация";
ils.gameachievements.alertText = "Не выбрана ни одна строка";
ils.gameachievements.promptRemove = "Удаление достижения";
ils.gameachievements.promptRemoveText = "Вы уверены, что хотите удалить достижение?";
ils.gameachievements.ok = "OK";
ils.gameachievements.cancel = "Отмена";
ils.gameachievements.achievemntCreation = "Создание достижения";
ils.gameachievements.achievemntEdition = "Изменение достижения";

Ext.define("GameAchievementModel", {
    extend: "Ext.data.Model",
    fields: [{
        name: "GameAchievementId",
        type: "string"
    }, {
        name: "Name",
        type: "string"
    }, {
        name: "ImagePath",
        type: "string"
    }, {
        name: "Message",
        type: "string"
    }, {
        name: "Index",
        type: "int"
    }, {
        name: "Priority",
        type: "int"
    }, {
        name: "Score",
        type: "int"
    }, {
        name: "AdditionalParameters",
        type: "string"
    }, {
        name: "AchievementExecutor",
        type: "string"
    }]
});

window.ils.gameachievements.store = new Ext.data.Store({
    extend: "Ext.data.Store",
    autoLoad: true,
    model: GameAchievementModel,
    proxy: {
        type: "ajax",
        url: window.ils.gameachievements.getAchievements,
        api: {
            read: window.ils.gameachievements.getAchievements,
            create: window.ils.gameachievements.createAchievement,
            update: window.ils.gameachievements.updateAchievement,
            destroy: window.ils.gameachievements.deleteAchievement
        },
        reader: new Ext.data.JsonReader({
            totalProperty: "total",
            rootProperty: "data"
        }, GameAchievementModel),
        writer: new Ext.data.JsonWriter({
            encode: false,
            listful: true,
            writeAllFields: true
        }),
        headers: { "Content-Type": "application/json; charset=UTF-8" },
        autoSave: true,
        autoLoad: true,
        remoteSort: false
    }
});

function createAchievementProfileWindow(achievementModel) {
    var title, buttons;
    if (achievementModel) {
        title = window.ils.gameachievements.achievemntEdition;
        buttons = [
            {
                text: window.ils.gameachievements.ok,
                formBind: false,
                handler: function () {
                    var profileItem = achievementProfile.items.items[0];
                    var newItem =
					{
					    GameAchievementId: achievementModel.GameAchievementId,
					    Name: profileItem.getComponent("Name").getValue(),
					    ImagePath: profileItem.getComponent("ImagePath").getValue(),
					    Message: profileItem.getComponent("Message").getValue(),
					    Index: profileItem.getComponent("Index").getValue(),
					    Priority: profileItem.getComponent("Priority").getValue(),
					    Score: profileItem.getComponent("Score").getValue(),
					    AdditionalParameters: profileItem.getComponent("AdditionalParameters").getValue(),
					    AchievementExecutor: profileItem.getComponent("AchievementExecutor").getValue()
					};

                    Ext.Ajax.request({
                        url: window.ils.gameachievements.updateAchievement,
                        method: "POST",
                        params: newItem,
                        success: function () {
                            window.ils.gameachievements.store.sync();
                            window.ils.gameachievements.store.reload();
                            achievementProfile.close();
                        },
                        failure: function (response) {
                            console.log("Ошибка: " + response);
                        }
                    });
                }
            }, {
                text: window.ils.gameachievements.cancel,
                formBind: false,
                handler: function () {
                    achievementProfile.close();
                }
            }
        ];
    } else {
        title = window.ils.gameachievements.achievemntCreation;
        buttons = [
            {
                text: window.ils.gameachievements.ok,
                formBind: false,
                handler: function() {
                    var profileItem = achievementProfile.items.items[0];
                    var newItem =
					{
					    Name: profileItem.getComponent("Name").getValue(),
					    ImagePath: profileItem.getComponent("ImagePath").getValue(),
					    Message: profileItem.getComponent("Message").getValue(),
					    Index: profileItem.getComponent("Index").getValue(),
					    Priority: profileItem.getComponent("Priority").getValue(),
					    Score: profileItem.getComponent("Score").getValue(),
					    AdditionalParameters: profileItem.getComponent("AdditionalParameters").getValue(),
					    AchievementExecutor: profileItem.getComponent("AchievementExecutor").getValue()
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
    }

    var achievementProfile = new Ext.Window({
        title: title,
        layout: "fit",
        width: 440,
        height: 560,
        y: 150,
        modal: true,
        dragable: true,
        plain: true,
        resizable: false,
        border: false,
        items: new Ext.FormPanel({
            frame: false,
            border: false,
            cls: "white-form-panel",
            defaultType: "textfield",
            padding: "10 0 0 0",
            fieldDefaults: {
                padding: "0 10 2 10",
                labelWidth: 100,
                msgTarget: "side",
                width: 430
            },
            bodyStyle: { "background-color": "#FFFFFF" },
            items: [{
                fieldLabel: window.ils.gameachievements.textIndex,
                xtype: "numberfield",
                name: "Index",
                id: "Index",
                value: achievementModel ? achievementModel.Index : ""
            }, {
                fieldLabel: window.ils.gameachievements.textName,
                xtype: "textfield",
                name: "Name",
                id: "Name",
                value: achievementModel? achievementModel.Name : ""
            }, {
                fieldLabel: window.ils.gameachievements.textScore,
                xtype: "numberfield",
                name: "Score",
                id: "Score",
                value: achievementModel ? achievementModel.Score : ""
            }, {
                fieldLabel: window.ils.gameachievements.textMessage,
                xtype: "textarea",
                name: "Message",
                id: "Message",
                value: achievementModel ? achievementModel.Message : ""
            }, {
                fieldLabel: window.ils.gameachievements.textPriority,
                xtype: "numberfield",
                name: "Priority",
                id: "Priority",
                value: achievementModel ? achievementModel.Priority : ""
            }, {
                fieldLabel: window.ils.gameachievements.textAchievementExecutor,
                xtype: "textfield",
                name: "AchievementExecutor",
                id: "AchievementExecutor",
                value: achievementModel ? achievementModel.AchievementExecutor : ""
            }, {
                fieldLabel: window.ils.gameachievements.textAdditionalParameters,
                xtype: "textfield",
                name: "AdditionalParameters",
                id: "AdditionalParameters",
                value: achievementModel ? achievementModel.AdditionalParameters : ""
            }, {
                fieldLabel: window.ils.gameachievements.textImagePath,
                xtype: "fileuploadfield",
                name: "ImagePath",
                id: "ImagePath",
                value: achievementModel ? achievementModel.ImagePath : ""
            }
        ],
            buttons: buttons
        })
    });

    achievementProfile.show();
}

window.ils.gameachievements.achievementGrid = new Ext.grid.GridPanel({
    store: window.ils.gameachievements.store,
    columns: [
        {
            header: window.ils.gameachievements.textIndex,
            dataIndex: "Index",
            sortable: true,
            editor: {
                xtype: "numberfield",
                allowBlank: false
            }
        }, {
            header: window.ils.gameachievements.textName,
            dataIndex: "Name",
            sortable: true,
            editor: {
                xtype: "textfield",
                allowBlank: false
            }
        }, {
            header: window.ils.gameachievements.textScore,
            dataIndex: "Score",
            sortable: true,
            editor: {
                xtype: "numberfield",
                allowBlank: false
            }
        }, {
            header: window.ils.gameachievements.textMessage,
            dataIndex: "Message",
            sortable: true,
            editor: {
                xtype: "textarea",
                allowBlank: false
            }
        }, {
            header: window.ils.gameachievements.textPriority,
            dataIndex: "Priority",
            sortable: true,
            editor: {
                xtype: "numberfield",
                allowBlank: false
            }
        }, {
            header: window.ils.gameachievements.textAchievementExecutor,
            dataIndex: "AchievementExecutor",
            sortable: true,
            editor: {
                xtype: "textfield",
                allowBlank: false
            }
        }, {
            header: window.ils.gameachievements.textAdditionalParameters,
            dataIndex: "AdditionalParameters",
            sortable: true,
            editor: {
                xtype: "textfield",
                allowBlank: false
            }
        }, {
            header: window.ils.gameachievements.textImagePath,
            dataIndex: "ImagePath",
            sortable: true,
            editor: {
                xtype: "textfield",
                allowBlank: false
            }
        }
    ],
    listeners: {
        itemdblclick: function() {
            var grid = this;
            var s = grid.getSelectionModel().getSelection();
            if (!s.length) {
                Ext.Msg.alert(window.ils.gameachievements.alert, window.ils.gameachievements.alertText);
                return;
            }
            createAchievementProfileWindow(s[0].data);
        }
    }
});

Ext.onReady(function () {
    window.ils.gameachievements.layout = new Ext.Panel({
        title: window.ils.gameachievements.gridName,
        layout: "fit",
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
                    xtype: "button",
                        iconCls: "add",
                        text: window.ils.gameachievements.textAdd,
                        handler: function () {
                            createAchievementProfileWindow();
                        }                            
                    }, {
                        xtype: "button",
                        ref: "../removeBtn",
                        iconCls: "remove",
                        text: window.ils.gameachievements.textRemove,
                        handler: function () {
                            var selection = window.ils.gameachievements.achievementGrid.getSelectionModel().getSelection();
                            if (!selection.length) {
                                Ext.Msg.alert(window.ils.gameachievements.alert, window.ils.gameachievements.alertText);
                                return;
                            }
                            Ext.Msg.confirm(window.ils.gameachievements.promptRemove, window.ils.gameachievements.promptRemoveText, function (button) {
                                if (button === "yes") {
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
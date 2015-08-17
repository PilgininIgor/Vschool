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
ils.gameachievements.textAchievementAwardType = "Тип награды";
ils.gameachievements.textAchievementTrigger = "Триггер";
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
ils.gameachievements.waitMsg = "Подождите, идет загрузка";

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
        name: "AchievementTrigger",
        type: "string"
    }, {
        name: "AchievementAwardType",
        type: "string"
    }, {
        name: "AchievementExecutor",
        type: "string"
    }]
});

ils.gameachievements.triggerStore = Ext.create("Ext.data.Store", {
    fields: ["id", "name"],
    data: [
        { "id": "0", "name": "Обучение" },
        { "id": "1", "name": "Тест" },
        { "id": "2", "name": "Лекция" },
        { "id": "3", "name": "Тема" },
        { "id": "4", "name": "Курс" },
        { "id": "5", "name": "Параграф" },
        { "id": "6", "name": "Мультплеер" },
        { "id": "7", "name": "Гид" },
        { "id": "8", "name": "Стенд" },
        { "id": "9", "name": "Телепорт" }
    ]
});

ils.gameachievements.awardTypeStore = Ext.create("Ext.data.Store", {
    fields: ["id", "name"],
    data: [
        { "id": "0", "name": "Монеты" },
        { "id": "1", "name": "Рейтинг" }
    ]
});

ils.gameachievements.store = new Ext.data.Store({
    extend: "Ext.data.Store",
    autoLoad: true,
    model: GameAchievementModel,
    proxy: {
        type: "ajax",
        url: window.ils.gameachievements.getAchievements,
        api: {
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
    var uploaded = false;
    var imageForm = new Ext.form.Panel({
        defaultType: "displayfield",
        border: false,
        bodyStyle: { "background-color": "#FFFFFF" },
        items: [{
            fieldLabel: window.ils.gameachievements.textImagePath,
            xtype: "fileuploadfield",
            name: "Image",
            id: "Image",
            emptyText: achievementModel ? achievementModel.ImagePath : "",
            onChange: function () {
                uploaded = true;
            }
        }]
    });
    var achievementProfile;
    if (achievementModel) {
        title = window.ils.gameachievements.achievemntEdition;
        buttons = [
            {
                text: window.ils.gameachievements.ok,
                formBind: false,
                handler: function () {
                    var img = imageForm.items.items[0].getValue();
                    if (uploaded && img != null && img !== "") {
                        var form = imageForm.getForm();
                        form.submit({
                            url: window.ils.gameachievements.uploadImage,
                            waitMsg: window.ils.gameachievements.waitMsg,
                            failure: function (f, a) {
                                Ext.Msg.alert("Ошибка", a.result.msg);
                            }
                        });
                    }

                    var profileItem = achievementProfile.items.items[0];
                    if (!img) {
                        img = achievementModel.ImagePath;
                    }
                    if (img.indexOf("/") !== -1) {
                        img = img.slice(img.lastIndexOf("/") + 1);
                    }
                    if (img.indexOf("\\") !== -1) {
                        img = img.slice(img.lastIndexOf("\\") + 1);
                    }

                    var newItem =
					{
					    GameAchievementId: achievementModel.GameAchievementId,
					    Name: profileItem.getComponent("Name").getValue(),
					    ImagePath: img,
					    Message: profileItem.getComponent("Message").getValue(),
					    Index: profileItem.getComponent("Index").getValue(),
					    Priority: profileItem.getComponent("Priority").getValue(),
					    Score: profileItem.getComponent("Score").getValue(),
					    AdditionalParameters: profileItem.getComponent("AdditionalParameters").getValue(),
					    AchievementExecutor: profileItem.getComponent("AchievementExecutor").getValue(),				    
					    AchievementTrigger: profileItem.getComponent("AchievementTrigger").getValue(),
					    AchievementAwardType: profileItem.getComponent("AchievementAwardType").getValue()
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
                            Ext.Msg.alert("Ошибка", response);
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
                handler: function () {
                    var img = imageForm.items.items[0].getValue();
                    var profileItem = achievementProfile.items.items[0];
                    if (uploaded && img != null && img !== "") {
                        var form = imageForm.getForm();
                        form.submit({
                            url: window.ils.gameachievements.uploadImage,
                            //waitMsg: window.ils.gameachievements.waitMsg,
                            failure: function (f, a) {
                                Ext.Msg.alert("Ошибка", a.result.msg);
                            }
                        });
                        if (!img) {
                            img = profileItem.getComponent("ImagePath").getValue();
                        }
                        if (img.indexOf("/") !== -1) {
                            img = img.slice(img.lastIndexOf("/") + 1);
                        }
                        if (img.indexOf("\\") !== -1) {
                            img = img.slice(img.lastIndexOf("\\") + 1);
                        }
                    }

                    var newItem =
					{
					    Name: profileItem.getComponent("Name").getValue(),
					    ImagePath: img,
					    Message: profileItem.getComponent("Message").getValue(),
					    Index: profileItem.getComponent("Index").getValue(),
					    Priority: profileItem.getComponent("Priority").getValue(),
					    Score: profileItem.getComponent("Score").getValue(),
					    AdditionalParameters: profileItem.getComponent("AdditionalParameters").getValue(),
					    AchievementExecutor: profileItem.getComponent("AchievementExecutor").getValue(),
					    AchievementTrigger: profileItem.getComponent("AchievementTrigger").getValue(),
					    AchievementAwardType: profileItem.getComponent("AchievementAwardType").getValue()
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
    achievementProfile = new Ext.Window({
        title: title,
        layout: "fit",
        width: 440,
        height: 500,
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
                    allowBlank: false,
                    value: achievementModel ? achievementModel.Index : ""
                }, {
                    fieldLabel: window.ils.gameachievements.textName,
                    xtype: "textfield",
                    name: "Name",
                    id: "Name",
                    allowBlank: false,
                    value: achievementModel? achievementModel.Name : ""
                }, {
                    fieldLabel: window.ils.gameachievements.textAchievementAwardType,
                    xtype: "combo",
                    name: "AchievementAwardType",
                    id: "AchievementAwardType",
                    editable: false,
                    queryMode: "local",
                    displayField: "name",
                    valueField: "id",
                    allowBlank: false,
                    tpl: Ext.create("Ext.XTemplate",
                        "<tpl for=\".\">",
                        "<div class=\"x-boundlist-item\" style=\"font-family:wf_SegoeUILight;font-weight:normal;font-size:14px;padding-top:4px;padding-bottom:4px;\">{name}</div>",
                        "</tpl>"
                    ),
                    store: window.ils.gameachievements.awardTypeStore,
                    value: achievementModel ? window.ils.gameachievements.awardTypeStore.findRecord("name", achievementModel.AchievementAwardType) : ""
                }, {
                    fieldLabel: window.ils.gameachievements.textScore,
                    xtype: "numberfield",
                    name: "Score",
                    id: "Score",
                    allowBlank: false,
                    value: achievementModel ? achievementModel.Score : "100"
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
                    allowBlank: false,
                    value: achievementModel ? achievementModel.Priority : "10"
                }, {
                    fieldLabel: window.ils.gameachievements.textAchievementTrigger,
                    xtype: "combo",
                    name: "AchievementTrigger",
                    id: "AchievementTrigger",
                    editable: false,
                    queryMode: "local",
                    displayField: "name",
                    valueField: "id",
                    allowBlank: false,
                    store: window.ils.gameachievements.triggerStore,
                    tpl: Ext.create("Ext.XTemplate",
                        "<tpl for=\".\">",
                        "<div class=\"x-boundlist-item\" style=\"font-family:wf_SegoeUILight;font-weight:normal;font-size:14px;padding-top:4px;padding-bottom:4px;\">{name}</div>",
                        "</tpl>"
                    ),
                    value: achievementModel ? window.ils.gameachievements.triggerStore.findRecord("name", achievementModel.AchievementTrigger) : ""
                }, {
                    fieldLabel: window.ils.gameachievements.textAchievementExecutor,
                    xtype: "textfield",
                    name: "AchievementExecutor",
                    id: "AchievementExecutor",
                    allowBlank: false,
                    value: achievementModel ? achievementModel.AchievementExecutor : ""
                }, {
                    fieldLabel: window.ils.gameachievements.textAdditionalParameters,
                    xtype: "textfield",
                    name: "AdditionalParameters",
                    id: "AdditionalParameters",
                    value: achievementModel ? achievementModel.AdditionalParameters : ""
                },
                imageForm
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
            },
            width: 150
        }, {
            header: window.ils.gameachievements.textAchievementAwardType,
            dataIndex: "AchievementAwardType",
            sortable: true,
            editor: {
                xtype: "textfield",
                allowBlank: false
            },
            width: 125
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
            header: window.ils.gameachievements.textAchievementTrigger,
            dataIndex: "AchievementTrigger",
            sortable: true,
            editor: {
                xtype: "textfield",
                allowBlank: false
            }
        }, {
            header: window.ils.gameachievements.textAchievementExecutor,
            dataIndex: "AchievementExecutor",
            sortable: true,
            editor: {
                xtype: "textfield",
                allowBlank: false
            },
            width: 150
        }, {
            header: window.ils.gameachievements.textAdditionalParameters,
            dataIndex: "AdditionalParameters",
            sortable: true,
            editor: {
                xtype: "textfield",
                allowBlank: false
            },
            width: 150
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
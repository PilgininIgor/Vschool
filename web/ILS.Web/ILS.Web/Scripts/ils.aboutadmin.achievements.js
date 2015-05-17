Ext.ns('ils', 'ils.aboutadmin.achievements');

Ext.require('Ext.ux.grid.*');


if (Ext.util.Cookies.get("language") == null) Ext.util.Cookies.set("language", lang_pref);
if (Ext.util.Cookies.get("language") == "Russian") isRussian = true; else isRussian = false;
ils.aboutadmin.achievements.textProfile = 'Achievement';
ils.aboutadmin.achievements.textAdd = 'Add achievement';
ils.aboutadmin.achievements.textRemove = 'Remove achievement';
ils.aboutadmin.achievements.textName = 'Name';
ils.aboutadmin.achievements.textDescription = 'Description';
ils.aboutadmin.achievements.textImage = 'Image';
ils.aboutadmin.achievements.uploadImage = 'Upload image';
ils.aboutadmin.achievements.promptDescription = 'Description';
ils.aboutadmin.achievements.promptDescriptionText = 'Please enter description:';
ils.aboutadmin.achievements.promptName = 'Name';
ils.aboutadmin.achievements.promptNameText = 'Please enter name:';
ils.aboutadmin.achievements.promptRemove = 'Remove achievement';
ils.aboutadmin.achievements.promptRemoveText = 'Are you sure?';
ils.aboutadmin.achievements.alert = 'Info';
ils.aboutadmin.achievements.alertText = 'No rows selected';
ils.aboutadmin.achievements.gridName = 'List of achievements';
ils.aboutadmin.achievements.profileName = 'Name';
ils.aboutadmin.achievements.profileDescription = 'Description';
ils.aboutadmin.achievements.profileImage = 'Image';
ils.aboutadmin.achievements.profilePriority = 'Priority';
ils.aboutadmin.achievements.authors = 'Achievements';
if (isRussian) {
    ils.aboutadmin.achievements.textProfile = 'Достижение';
    ils.aboutadmin.achievements.textAdd = 'Добавить достижение';
    ils.aboutadmin.achievements.textRemove = 'Удалить достижение';
    ils.aboutadmin.achievements.textName = 'Название';
    ils.aboutadmin.achievements.textDescription = 'Описание';
    ils.aboutadmin.achievements.textImage = 'Изображение';
    ils.aboutadmin.achievements.uploadImage = 'Загрузить изображение';
    ils.aboutadmin.achievements.promptDescription = 'Описание';
    ils.aboutadmin.achievements.promptDescriptionText = 'Пожалуйста, введите описание:';
    ils.aboutadmin.achievements.promptName = 'Название';
    ils.aboutadmin.achievements.promptNameText = 'Пожалуйста, введите название:';
    ils.aboutadmin.achievements.promptRemove = 'Удаление награды';
    ils.aboutadmin.achievements.promptRemoveText = 'Вы уверены?';
    ils.aboutadmin.achievements.alert = 'Информация';
    ils.aboutadmin.achievements.alertText = 'Не выбрана ни одна строка';
    ils.aboutadmin.achievements.gridName = 'Список авторов';
    ils.aboutadmin.achievements.profileName = 'Название';
    ils.aboutadmin.achievements.profileDescription = 'Описание';
    ils.aboutadmin.achievements.profileImage = 'Изображение';
    ils.aboutadmin.achievements.profilePriority = 'Приоритет';
    ils.aboutadmin.achievements.authors = 'Достижения';
}

Ext.define('AchievementModel', {
    extend: 'Ext.data.Model',
    fields: [{
        name: 'Name',
        type: 'string'
    }, {
        name: 'Description',
        type: 'string'
    }, {
        name: 'Image',
        type: 'string'
    }, {
        name: 'Priority',
        type: 'string'
    }]
});

ils.aboutadmin.achievements.store = new Ext.data.Store({
    extend: 'Ext.data.Store',
    autoLoad: true,
    autoSync: true,
    model: AchievementModel,
    proxy: {
        type: 'ajax',
        url: ils.aboutadmin.awards.readAward,
        api: {
            read: ils.aboutadmin.achievements.readAchievement,
            create: ils.aboutadmin.achievements.createAchievement,
            update: ils.aboutadmin.achievements.updateAchievement,
            destroy: ils.aboutadmin.achievements.deleteAchievement
        },
        reader: new Ext.data.JsonReader({
            rootProperty: 'jsonList'
        }, AchievementModel),
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

addAchievementHandler = function () {
    var grid = this.up('grid');

    var uploaded = false;

    ils.aboutadmin.achievements.AchievementProfile = new Ext.Window({
        title: ils.aboutadmin.achievements.textProfile,
        modal: true,
        layout: 'fit',
        width: 410,
        height: 470,
        closable: true,
        resizable: false,
        draggable: false,
        plain: true,
        border: false,
        items: new Ext.FormPanel({
            frame: false,
            border: false,
            cls: 'white-form-panel',
            padding: "10px 0px 0px 0px",
            fieldDefaults: {
                padding: "0px 10px 2px 10px",
                msgTarget: 'side',
                width: 400
            },
            defaultType: 'displayfield',
            bodyStyle: { "background-color": "white" },
            items: [{

                fieldLabel: ils.aboutadmin.achievements.profileName,
                xtype: 'textfield',
                name: 'AchievementName',
                id: 'AchievementName'
            }, {
                fieldLabel: ils.aboutadmin.achievements.profileDescription,
                xtype: 'textarea',
                name: 'Description',
                id: 'Description',
                height: 110,
                grow: false
            }, {
                fieldLabel: ils.aboutadmin.achievements.profilePriority,
                xtype: 'textfield',
                name: 'Priority',
                id: 'Priority'
            },
            getAchievementImageGrid()],
            buttons: [{
                formBind: false,
                text: 'OK',
                handler: function () {
                    var img = "";
                    for (var achievementImage in ils.aboutadmin.achievements.images.store.data.items)
                        img += ils.aboutadmin.achievements.images.store.data.items[achievementImage].data.Image + ";";
                    var f = ils.aboutadmin.achievements.AchievementProfile.items.items[0];
                    Ext.Ajax.request({
                        url: document.location.href + '/CreateAchievement',
                        jsonData: {
                            'name': f.getComponent('AchievementName').getValue(),
                            'description': f.getComponent('Description').getValue(),
                            'image': img,
                            'priority': f.getComponent('Priority').getValue()
                        },
                        success: function (responce, opts) {
                            var newItem = AchievementModel.create(
                            {
                                Name: f.getComponent('AchievementName').getValue(),
                                Description: f.getComponent('Description').getValue(),
                                Image: img,
                                Priority: f.getComponent('Priority').getValue()
                            });
                            ils.aboutadmin.achievements.store.add(newItem);
                            ils.aboutadmin.achievements.AchievementProfile.close();
                        }
                    });

                }
            }]
        })
    });
    ils.aboutadmin.achievements.AchievementProfile.show();
    ils.aboutadmin.achievements.images.store.loadData([], false);
};

removeAchievementHandler = function () {
    var grid = this.up('grid');
    var s = grid.getSelectionModel().getSelection();
    if (!s.length) {
        Ext.Msg.alert(ils.aboutadmin.achievements.alert, ils.aboutadmin.achievements.alertText);
        return;
    }
    Ext.Msg.confirm(ils.aboutadmin.achievements.promptRemove, ils.aboutadmin.achievements.promptRemoveText, function (button) {
        if (button == 'yes') {
            Ext.Ajax.request({
                url: ils.aboutadmin.achievements.deleteAchievement,
                jsonData: {
                    'name': s[0].data.Name
                },
                success: function (responce, opts) {
                    ils.aboutadmin.achievements.store.remove(s[0]);
                }
            });
        }
    });
};

var tlbarAchievements = new Ext.panel.Panel({
    bodyStyle: { "background-color": "#4b9cd7" },
    border: false,
    defaults: {
        margin: "4 0 0 4",
        padding: "2 8 2 8"
    },
    items: [{
        xtype: 'button',
        ref: '../addBtn',
        iconCls: 'add',
        text: ils.aboutadmin.achievements.textAdd,
        handler: addAchievementHandler
    }, {
        xtype: 'button',
        ref: '../removeBtn',
        iconCls: 'remove',
        text: ils.aboutadmin.achievements.textRemove,
        handler: removeAchievementHandler
    }]
});

itemdblclickHandler = function () {
    var grid = this;
    var s = grid.getSelectionModel().getSelection();
    if (!s.length) {
        Ext.Msg.alert(ils.aboutadmin.achievements.alert, ils.aboutadmin.achievements.alertText);
        return;
    }
    var index = grid.store.indexOf(s[0]);
    Ext.Ajax.request({
        url: document.location.href + '/AchievementProfile',
        success: function (response, opts) {
            var a = eval('(' + response.responseText + ')');

            ils.aboutadmin.achievements.AchievementProfile = new Ext.Window({
                title: ils.aboutadmin.achievements.textProfile,
                modal: true,
                layout: 'fit',
                width: 410,
                height: 470,
                closable: true,
                resizable: false,
                draggable: false,
                plain: true,
                border: false,
                items: new Ext.FormPanel({
                    frame: false,
                    border: false,
                    cls: 'white-form-panel',
                    padding: "10px 0px 0px 0px",
                    fieldDefaults: {
                        padding: "0px 10px 2px 10px",
                        msgTarget: 'side',
                        width: 400
                    },
                    defaultType: 'displayfield',
                    bodyStyle: { "background-color": "white" },
                    items: [{
                        fieldLabel: ils.aboutadmin.achievements.profileName,
                        name: 'AchievementName',
                        id: 'AchievementName',
                        value: a[0].Name
                    }, {
                        fieldLabel: ils.aboutadmin.achievements.profileDescription,
                        xtype: 'textarea',
                        name: 'Description',
                        id: 'Description',
                        height: 110,
                        grow: false,
                        value: a[0].Description
                    }, {
                        fieldLabel: ils.aboutadmin.achievements.profilePriority,
                        xtype: 'textfield',
                        name: 'Priority',
                        id: 'Priority',
                        value: a[0].Priority
                    },
                    getAchievementImageGrid()],
                    buttons: [{
                        formBind: false,
                        text: 'OK',
                        handler: function () {
                            var img = "";
                            for (var achievementImage in ils.aboutadmin.achievements.images.store.data.items)
                                img += ils.aboutadmin.achievements.images.store.data.items[achievementImage].data.Image + ";";
                            if (img == null || img == "")
                                img = a[0].Image;
                            var f = ils.aboutadmin.achievements.AchievementProfile.items.items[0];
                            Ext.Ajax.request({
                                url: document.location.href + '/UpdateAchievement',
                                jsonData: {
                                    'name': f.getComponent('AchievementName').getValue(),
                                    'description': f.getComponent('Description').getValue(),
                                    'image': img,
                                    'priority': f.getComponent('Priority').getValue()
                                },
                                success: function (responce, opts) {
                                    ils.aboutadmin.achievements.images.store.sync();
                                    ils.aboutadmin.achievements.images.store.reload();
                                    ils.aboutadmin.achievements.store.sync();
                                    ils.aboutadmin.achievements.store.reload();
                                    ils.aboutadmin.achievements.AchievementProfile.close();
                                }
                            });

                        }
                    }]
                })
            });
            ils.aboutadmin.achievements.AchievementProfile.show();
            ils.aboutadmin.achievements.images.store.loadData([], false);
            var images = a[0].Image.split(";");
            for (var img in images) {
                if (images[img] != "") {
                    var newItem = { Image: images[img] };
                    ils.aboutadmin.achievements.images.store.add(newItem);
                }
            }
        },
        jsonData: {
            'name': ils.aboutadmin.achievements.store.data.items[index].data.Name

        }
    });
};

var AchievementGrid = new Ext.grid.GridPanel({
    store: ils.aboutadmin.achievements.store,
    title: ils.aboutadmin.achievements.authors,
    columns: [
    {
        header: ils.aboutadmin.achievements.textName,
        dataIndex: 'Name',
        width: 220,
        sortable: true,
        editor: {
            xtype: 'textfield',
            allowBlank: false
        }
    }, {
        header: ils.aboutadmin.achievements.textImage,
        dataIndex: 'Image',
        width: 200,
        sortable: true,
        editor: {
            xtype: 'textfield',
            allowBlank: false

        }
    }, {
        header: ils.aboutadmin.achievements.textDescription,
        dataIndex: 'Description',
        width: 300,
        sortable: true,
        editor: {
            xtype: 'textfield',
            allowBlank: false

        }
    }],
    listeners: {
        itemdblclick: itemdblclickHandler
    },
    dockedItems: [tlbarAchievements]
});
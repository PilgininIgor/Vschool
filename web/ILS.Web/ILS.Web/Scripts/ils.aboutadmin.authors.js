Ext.ns('ils', 'ils.aboutadmin.authors');

Ext.require('Ext.ux.grid.*');


if (Ext.util.Cookies.get("language") == null) Ext.util.Cookies.set("language", lang_pref);
if (Ext.util.Cookies.get("language") == "Russian") isRussian = true; else isRussian = false;
ils.aboutadmin.authors.textProfile = 'Author';
ils.aboutadmin.authors.textAdd = 'Add Author';
ils.aboutadmin.authors.textRemove = 'Remove author';
ils.aboutadmin.authors.textSaveChanges = 'Save changes';
ils.aboutadmin.authors.textName = 'Name';
ils.aboutadmin.authors.textDescription = 'Description';
ils.aboutadmin.authors.textImage = 'Image';
ils.aboutadmin.authors.uploadImage = 'Upload image';
ils.aboutadmin.authors.promptDescription = 'Description';
ils.aboutadmin.authors.promptDescriptionText = 'Please enter description:';
ils.aboutadmin.authors.promptName = 'Name';
ils.aboutadmin.authors.promptNameText = 'Please enter name:';
ils.aboutadmin.authors.promptRemove = 'Remove author';
ils.aboutadmin.authors.promptRemoveText = 'Are you sure?';
ils.aboutadmin.authors.alert = 'Info';
ils.aboutadmin.authors.alertText = 'No rows selected';
ils.aboutadmin.authors.gridName = 'List of authors';
ils.aboutadmin.authors.profileName = 'Name';
ils.aboutadmin.authors.profileDescription = 'Description';
ils.aboutadmin.authors.profileImage = 'Image';
ils.aboutadmin.authors.profilePriority = 'Priority';
ils.aboutadmin.authors.authors = 'Authors';
if (isRussian) {
    ils.aboutadmin.authors.textProfile = 'Разработчик';
    ils.aboutadmin.authors.textAdd = 'Добавить разработчика';
    ils.aboutadmin.authors.textRemove = 'Удалить разработчика';
    ils.aboutadmin.authors.textSaveChanges = 'Сохранить изменения';
    ils.aboutadmin.authors.textToggle = 'Утвердить автора';
    ils.aboutadmin.authors.textName = 'Имя';
    ils.aboutadmin.authors.textDescription = 'Описание';
    ils.aboutadmin.authors.textImage = 'Изображение';
    ils.aboutadmin.authors.uploadImage = 'Загрузить изображение';
    ils.aboutadmin.authors.promptDescription = 'Описание';
    ils.aboutadmin.authors.promptDescriptionText = 'Пожалуйста, введите описание:';
    ils.aboutadmin.authors.promptName = 'Имя';
    ils.aboutadmin.authors.promptNameText = 'Пожалуйста, введите имя:';
    ils.aboutadmin.authors.promptRemove = 'Удаление разработчика';
    ils.aboutadmin.authors.promptRemoveText = 'Вы уверены?';
    ils.aboutadmin.authors.alert = 'Информация';
    ils.aboutadmin.authors.alertText = 'Не выбрана ни одна строка';
    ils.aboutadmin.authors.gridName = 'Список разработчиковов';
    ils.aboutadmin.authors.profileName = 'Имя';
    ils.aboutadmin.authors.profileDescription = 'Описание';
    ils.aboutadmin.authors.profileImage = 'Изображение';
    ils.aboutadmin.authors.profilePriority = 'Приоритет';
    ils.aboutadmin.authors.authors = 'Разработчики';
}

Ext.define('EDucationAuthorModel', {
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

ils.aboutadmin.authors.store = new Ext.data.Store({
    extend: 'Ext.data.Store',
    autoLoad: true,
    autoSync: true,
    model: EDucationAuthorModel,
    proxy: {
        type: 'ajax',
        url: ils.aboutadmin.authors.readEDucationAuthor,
        api: {
            read: ils.aboutadmin.authors.readEDucationAuthor,
            create: ils.aboutadmin.authors.createEDucationAuthor,
            update: ils.aboutadmin.authors.updateEDucationAuthor,
            destroy: ils.aboutadmin.authors.deleteEDucationAuthor
        },
        reader: new Ext.data.JsonReader({
            rootProperty: 'jsonList'
        }, EDucationAuthorModel),
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


addItemToStoreAndReload = function (name, description, image, priority) {
    var newItem = EDucationAuthorModel.create({
        Name: name,
        Description: description,
        Image: image,
        Priority: priority
    });
    ils.aboutadmin.authors.store.add(newItem);
    ils.aboutadmin.authors.store.sync();
    ils.aboutadmin.authors.store.reload();
};

addAuthorHandler = function () {
    var grid = this.up('grid');

    var uploaded = false;

    ils.aboutadmin.authors.imageForm = new Ext.form.Panel({
        defaultType: 'displayfield',
        fieldDefaults: {
            msgTarget: 'side'
        },
        frame: false,
        border: false,
        bodyStyle: { "background-color": "white" },
        items: [{
            fieldLabel: ils.aboutadmin.authors.profileImage,
            xtype: 'fileuploadfield',
            name: 'Image',
            id: 'Image',
            width: 360,
            onChange: function (value) {
                var newValue = value.replace(/C:\\fakepath\\/g, '');
                this.setRawValue(newValue);
                uploaded = true;
            }
        }]
    });

    ils.aboutadmin.authors.EDucationAuthorProfile = new Ext.Window({
        title: ils.aboutadmin.authors.textProfile,
        modal: true,
        layout: 'fit',
        width: 430,
        height: 300,
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
            defaults: {
                padding: "0px 10px 2px 10px",
                msgTarget: 'side',
                width: 400
            },
            defaultType: 'displayfield',
            bodyStyle: { "background-color": "white" },
            items: [{
                fieldLabel: ils.aboutadmin.authors.profileName,
                xtype: 'textfield',
                name: 'EDucationAuthorName',
                id: 'EDucationAuthorName'
            }, {
                fieldLabel: ils.aboutadmin.authors.profileDescription,
                xtype: 'textarea',
                name: 'Description',
                id: 'Description',
                height: 110,
                grow: false
            }, {
                fieldLabel: ils.aboutadmin.authors.profilePriority,
                xtype: 'textfield',
                name: 'Priority',
                id: 'Priority'
            },
            ils.aboutadmin.authors.imageForm],
            buttons: [{
                formBind: false,
                text: 'OK',
                handler: function () {
                    var uploadFinished = !uploaded;
                    var updateFinished = false;
                    var img = ils.aboutadmin.authors.imageForm.items.items[0].getValue();
                    var f = ils.aboutadmin.authors.EDucationAuthorProfile.items.items[0];
                    if (uploaded && img != null && img != "") {
                        var form = ils.aboutadmin.authors.imageForm.getForm();
                        form.submit({
                            url: '/aboutadmin/uploadimage',
                            waitMsg: 'Uploading your file...',
                            success: function (form, action) {
                                uploadFinished = true;
                                if (uploadFinished && updateFinished) {
                                    addItemToStoreAndReload(
                                        f.getComponent('EDucationAuthorName').getValue(),
                                        f.getComponent('Description').getValue(),
                                        img,
                                        f.getComponent('Priority').getValue());
                                    ils.aboutadmin.authors.EDucationAuthorProfile.close();
                                }
                            }
                        });
                    }
                    Ext.Ajax.request({
                        url: document.location.href + '/CreateEDucationAuthor',
                        jsonData: {
                            'name': f.getComponent('EDucationAuthorName').getValue(),
                            'description': f.getComponent('Description').getValue(),
                            'image': img,
                            'priority': f.getComponent('Priority').getValue()
                        },
                        success: function (responce, opts) {
                            updateFinished = true;
                            if (uploadFinished && updateFinished) {
                                addItemToStoreAndReload(
                                    f.getComponent('EDucationAuthorName').getValue(),
                                    f.getComponent('Description').getValue(),
                                    img,
                                    f.getComponent('Priority').getValue());
                                ils.aboutadmin.authors.EDucationAuthorProfile.close();
                            }
                        }
                    });

                }
            }]
        })
    });
    ils.aboutadmin.authors.EDucationAuthorProfile.show();
};

removeAuthorHandler = function () {
    var grid = this.up('grid');
    var s = grid.getSelectionModel().getSelection();
    if (!s.length) {
        Ext.Msg.alert(ils.aboutadmin.authors.alert, ils.aboutadmin.authors.alertText);
        return;
    }
    Ext.Msg.confirm(ils.aboutadmin.authors.promptRemove, ils.aboutadmin.authors.promptRemoveText, function (button) {
        if (button == 'yes') {
            Ext.Ajax.request({
                url: ils.aboutadmin.authors.deleteEDucationAuthor,
                jsonData: {
                    'name': s[0].data.Name
                },
                success: function (responce, opts) {
                    ils.aboutadmin.authors.store.remove(s[0]);
                }
            });
        }
    });
};

var tlbarAuthors = new Ext.panel.Panel({
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
        text: ils.aboutadmin.authors.textAdd,
        handler: addAuthorHandler
    }, {
        xtype: 'button',
        ref: '../removeBtn',
        iconCls: 'remove',
        text: ils.aboutadmin.authors.textRemove,
        handler: removeAuthorHandler
    }]
});

itemdblclickHandler = function () {
    var grid = this;
    var s = grid.getSelectionModel().getSelection();
    if (!s.length) {
        Ext.Msg.alert(ils.aboutadmin.authors.alert, ils.aboutadmin.authors.alertText);
        return;
    }
    var index = grid.store.indexOf(s[0]);
    Ext.Ajax.request({
        url: document.location.href + '/EDucationAuthorProfile',
        success: function (response, opts) {
            var a = eval('(' + response.responseText + ')');

            var uploaded = false;

            ils.aboutadmin.authors.imageForm = new Ext.form.Panel({
                defaultType: 'displayfield',
                fieldDefaults: {
                    msgTarget: 'side'
                },
                frame: false,
                border: false,
                bodyStyle: { "background-color": "white" },
                items: [{
                    fieldLabel: ils.aboutadmin.authors.profileImage,
                    xtype: 'fileuploadfield',
                    name: 'Image',
                    id: 'Image',
                    width: 360,
                    emptyText: a[0].Image,
                    onChange: function (value) {
                        var newValue = value.replace(/C:\\fakepath\\/g, '');
                        this.setRawValue(newValue);
                        uploaded = true;
                    }
                }]
            });

            ils.aboutadmin.authors.EDucationAuthorProfile = new Ext.Window({
                title: ils.aboutadmin.authors.textProfile,
                modal: true,
                layout: 'fit',
                width: 430,
                height: 300,
                closable: true,
                resizable: false,
                draggable: false,
                plain: true,
                border: false,
                items: new Ext.Panel({
                    frame: false,
                    border: false,
                    cls: 'white-form-panel',
                    padding: "10px 0px 0px 0px",
                    defaults: {
                        padding: "0px 10px 2px 10px",
                        msgTarget: 'side',
                        width: 400
                    },
                    defaultType: 'displayfield',
                    bodyStyle: { "background-color": "white" },
                    items: [{
                        fieldLabel: ils.aboutadmin.authors.profileName,
                        name: 'EDucationAuthorName',
                        id: 'EDucationAuthorName',
                        value: a[0].Name
                    }, {
                        fieldLabel: ils.aboutadmin.authors.profileDescription,
                        xtype: 'textarea',
                        name: 'Description',
                        id: 'Description',
                        height: 110,
                        grow: false,
                        value: a[0].Description
                    }, {
                        fieldLabel: ils.aboutadmin.authors.profilePriority,
                        xtype: 'textfield',
                        name: 'Priority',
                        id: 'Priority',
                        value: a[0].Priority
                    },
                    ils.aboutadmin.authors.imageForm],
                    buttons: [{
                        formBind: false,
                        text: 'OK',
                        handler: function () {
                            var uploadFinished = !uploaded;
                            var updateFinished = false;
                            var img = ils.aboutadmin.authors.imageForm.items.items[0].getValue();
                            if (uploaded && img != null && img != "") {
                                var form = ils.aboutadmin.authors.imageForm.getForm();
                                form.submit({
                                    url: '/aboutadmin/uploadimage',
                                    waitMsg: 'Uploading your file...',
                                    success: function (form, action) {
                                        uploadFinished = true;
                                        if (uploadFinished && updateFinished)
                                            ils.aboutadmin.authors.EDucationAuthorProfile.close();
                                    }
                                });
                            }
                            if (img == null || img == "")
                                img = a[0].Image;
                            if (img.indexOf("/") != -1)
                                img = img.slice(img.lastIndexOf("/") + 1);
                            if (img.indexOf("\\") != -1)
                                img = img.slice(img.lastIndexOf("\\") + 1);

                            var f = ils.aboutadmin.authors.EDucationAuthorProfile.items.items[0];
                            Ext.Ajax.request({
                                url: document.location.href + '/UpdateProfile',
                                jsonData: {
                                    'name': f.getComponent('EDucationAuthorName').getValue(),
                                    'description': f.getComponent('Description').getValue(),
                                    'image': img,
                                    'priority': f.getComponent('Priority').getValue()
                                },
                                success: function (responce, opts) {
                                    updateFinished = true;
                                    if (uploadFinished && updateFinished) {
                                        var newItem = ils.aboutadmin.authors.store.data.items[index];
                                        newItem.set('Description', f.getComponent('Description').getValue());
                                        newItem.set('Image', img);
                                        newItem.set('Priority', f.getComponent('Priority').getValue());
                                        ils.aboutadmin.authors.EDucationAuthorProfile.close();
                                    }
                                }
                            });

                        }
                    }]
                })
            });
            ils.aboutadmin.authors.EDucationAuthorProfile.show();
        },
        jsonData: {
            'name': ils.aboutadmin.authors.store.data.items[index].data.Name
        }
    });
};

var EDucationAuthorGrid = new Ext.grid.GridPanel({
    store: ils.aboutadmin.authors.store,
    title: ils.aboutadmin.authors.authors,
    columns: [
    {
        header: ils.aboutadmin.authors.textName,
        dataIndex: 'Name',
        width: 200,
        sortable: true,
        editor: {
            xtype: 'textfield',
            allowBlank: false
        }
    }, {
        header: ils.aboutadmin.authors.textImage,
        dataIndex: 'Image',
        width: 150,
        sortable: true,
        editor: {
            xtype: 'textfield',
            allowBlank: false

        }
    }, {
        header: ils.aboutadmin.authors.textDescription,
        dataIndex: 'Description',
        width: 600,
        sortable: true,
        editor: {
            xtype: 'textfield',
            allowBlank: false

        }
    }],
    listeners: {
        itemdblclick: itemdblclickHandler
    },
    dockedItems: [tlbarAuthors]
});
Ext.ns('ils', 'ils.aboutadmin.authors');

Ext.require('Ext.ux.grid.*');


if (Ext.util.Cookies.get("language") == null) Ext.util.Cookies.set("language", lang_pref);
if (Ext.util.Cookies.get("language") == "Russian") isRussian = true; else isRussian = false;
ils.aboutadmin.authors.textProfile = 'Author';
ils.aboutadmin.authors.textAdd = 'Add Author';
ils.aboutadmin.authors.textRemove = 'Remove Author';
ils.aboutadmin.authors.textSaveChanges = 'Save Changes';
ils.aboutadmin.authors.textName = 'Name';
ils.aboutadmin.authors.textDescription = 'Description';
ils.aboutadmin.authors.textImage = 'Image';
ils.aboutadmin.authors.uploadImage = 'Upload Image';
ils.aboutadmin.authors.promptDescription = 'Description';
ils.aboutadmin.authors.promptDescriptionText = 'Please enter description:';
ils.aboutadmin.authors.promptName = 'Name';
ils.aboutadmin.authors.promptNameText = 'Please enter name:';
ils.aboutadmin.authors.promptRemove = 'Remove Author';
ils.aboutadmin.authors.promptRemoveText = 'Are you sure?';
ils.aboutadmin.authors.alert = 'Info';
ils.aboutadmin.authors.alertText = 'No rows selected';
ils.aboutadmin.authors.gridName = 'List of Authors';
ils.aboutadmin.authors.profileName = 'Name';
ils.aboutadmin.authors.profileDescription = 'Description';
ils.aboutadmin.authors.profileImage = 'Image';
ils.aboutadmin.authors.profilePriority = 'Priority';
ils.aboutadmin.authors.authors = 'Authors';
if (isRussian)
{
    ils.aboutadmin.authors.textProfile = 'Автор';
    ils.aboutadmin.authors.textAdd = 'Добавить автора';
    ils.aboutadmin.authors.textRemove = 'Удалить автора';
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
    ils.aboutadmin.authors.promptRemove = 'Удаление автора';
    ils.aboutadmin.authors.promptRemoveText = 'Вы уверены?';
    ils.aboutadmin.authors.alert = 'Информация';
    ils.aboutadmin.authors.alertText = 'Не выбрана ни одна строка';
    ils.aboutadmin.authors.gridName = 'Список авторов';
    ils.aboutadmin.authors.profileName = 'Имя';
    ils.aboutadmin.authors.profileDescription = 'Описание';
    ils.aboutadmin.authors.profileImage = 'Изображение';
    ils.aboutadmin.authors.profilePriority = 'Приоритет';
    ils.aboutadmin.authors.authors = 'Авторы';
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

//ils.aboutadmin.authors.store = new Ext.data.Store({
//    proxy: new Ext.data.HttpProxy({
//        api: {
//            read : ils.aboutadmin.authors.readEDucationAuthor,
//            create: ils.aboutadmin.authors.createEDucationAuthor,
//            update: ils.aboutadmin.authors.updateEDucationAuthor,
//            destroy: ils.aboutadmin.authors.deleteEDucationAuthor
//        },
//		headers: { 'Content-Type': 'application/json; charset=UTF-8' },
//		afterRequest: function(req, res) {
              
//			var a = eval('(' + req.operation.response.responseText + ')');

//			ils.aboutadmin.authors.store.loadData([], false);
			
//			for (var Name in a) {
//				if(a.hasOwnProperty(Name)){
//				    ils.aboutadmin.authors.store.add(a[Name]);
//				}
//			}
			
			
//        }
//    }),
//	model:  EDucationAuthorModel,
//    reader: new Ext.data.JsonReader({
//            totalProperty: 'total',
//            rootProperty: 'data'
//        }, EDucationAuthorModel),
//	writer: new Ext.data.JsonWriter({
//            encode: false,
//            listful: true,
//            writeAllFields: true
//        }),
//    autoSave: true,
//    autoLoad: true,
//    remoteSort: false
//});

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
            //totalProperty: 'total',
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

var EDucationAuthorGrid = new Ext.grid.GridPanel({
    store: ils.aboutadmin.authors.store,
    title: ils.aboutadmin.authors.authors,
    columns: [
    {
        header: ils.aboutadmin.authors.textName,
        dataIndex: 'Name',
        width: 220,
        sortable: true,
        editor: {
            xtype: 'textfield',
            allowBlank: false
        }
    }, {
        header: ils.aboutadmin.authors.textImage,
        dataIndex: 'Image',
        width: 200,
        sortable: true,
        editor: {
            xtype: 'textfield',
            allowBlank: false

        }
    }, {
        header: ils.aboutadmin.authors.textDescription,
        dataIndex: 'Description',
        width: 300,
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
                        bodyStyle: { "background-color": "#DFE8F6" },
                        items: [{
                            fieldLabel: ils.aboutadmin.authors.profileImage,
                            xtype: 'fileuploadfield',
                            name: 'Image',
                            id: 'Image',
                            width: 300,
                            y: 10,
                            x: 15,
                            emptyText: a[0].Image,
                            onChange: function (value) {
                                uploaded = true;
                            }
                        }]
                    });

                    ils.aboutadmin.authors.EDucationAuthorProfile = new Ext.Window({
                        title: ils.aboutadmin.authors.textProfile,
                        layout: 'fit',
                        width: 500,
                        height: 270,
                        y: 150,
                        closable: true,
                        resizable: false,
                        draggable: false,
                        plain: true,
                        border: false,
                        items: new Ext.Panel({
                            defaultType: 'displayfield',
                            fieldDefaults: {
                                labelWidth: 400,
                                msgTarget: 'side'
                            },
                            bodyStyle: { "background-color": "#DFE8F6" },
                            items: [{

                                fieldLabel: ils.aboutadmin.authors.profileName,
                                name: 'EDucationAuthorName',
                                id: 'EDucationAuthorName',
                                width: 450,
                                y: 5,
                                x: 15,
                                value: a[0].Name
                            }, {
                                fieldLabel: ils.aboutadmin.authors.profileDescription,
                                xtype: 'textarea',
                                name: 'Description',
                                id: 'Description',
                                height: 110,
                                width: 450,
                                grow: false,
                                y: 5,
                                x: 15,
                                value: a[0].Description
                            }, {
                                fieldLabel: ils.aboutadmin.authors.profilePriority,
                                xtype: 'textfield',
                                name: 'Priority',
                                id: 'Priority',
                                y: 5,
                                x: 15,
                                value: a[0].Priority

                            },
                            ils.aboutadmin.authors.imageForm,
                            {
                                fieldLabel: 'OK',
                                name: 'update',
                                xtype: 'button',
                                y: 5,
                                x: 435,
                                width: 45,
                                value: 'OK',
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

        }
    },
    tbar: [{
        ref: '../removeBtn',
        iconCls: 'remove',
        text: ils.aboutadmin.authors.textRemove,
        handler: function () {
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

        }
    }, {
        ref: '../addBtn',
        iconCls: 'add',
        text: ils.aboutadmin.authors.textAdd,
        handler: function () {
            var grid = this.up('grid');

            var uploaded = false;

            ils.aboutadmin.authors.imageForm = new Ext.form.Panel({
                defaultType: 'displayfield',
                fieldDefaults: {
                    msgTarget: 'side'
                },
                frame: false,
                border: false,
                bodyStyle: { "background-color": "#DFE8F6" },
                items: [{
                    fieldLabel: ils.aboutadmin.authors.profileImage,
                    xtype: 'fileuploadfield',
                    name: 'Image',
                    id: 'Image',
                    width: 300,
                    y: 10,
                    x: 15,
                    onChange: function (value) {
                        uploaded = true;
                    }
                }]
            });

            ils.aboutadmin.authors.EDucationAuthorProfile = new Ext.Window({
                title: ils.aboutadmin.authors.textProfile,
                layout: 'fit',
                width: 500,
                height: 270,
                y: 150,
                closable: true,
                resizable: false,
                draggable: false,
                plain: true,
                border: false,
                items: new Ext.Panel({
                    defaultType: 'displayfield',
                    fieldDefaults: {
                        labelWidth: 400,
                        msgTarget: 'side'
                    },
                    bodyStyle: { "background-color": "#DFE8F6" },
                    items: [{

                        fieldLabel: ils.aboutadmin.authors.profileName,
                        xtype: 'textfield',
                        name: 'EDucationAuthorName',
                        id: 'EDucationAuthorName',
                        width: 450,
                        y: 5,
                        x: 15
                    }, {
                        fieldLabel: ils.aboutadmin.authors.profileDescription,
                        xtype: 'textarea',
                        name: 'Description',
                        id: 'Description',
                        height: 110,
                        width: 450,
                        grow: false,
                        y: 5,
                        x: 15
                    }, {
                        fieldLabel: ils.aboutadmin.authors.profilePriority,
                        xtype: 'textfield',
                        name: 'Priority',
                        id: 'Priority',
                        y: 5,
                        x: 15

                    },
                            ils.aboutadmin.authors.imageForm,
                            {
                                fieldLabel: 'OK',
                                name: 'update',
                                xtype: 'button',
                                y: 5,
                                x: 435,
                                width: 45,
                                value: 'OK',
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
                                    var f = ils.aboutadmin.authors.EDucationAuthorProfile.items.items[0];
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
                                                var newItem = EDucationAuthorModel.create(
												{
												    Name: f.getComponent('EDucationAuthorName').getValue(),
												    Description: f.getComponent('Description').getValue(),
												    Image: img,
												    Priority: f.getComponent('Priority').getValue()
												});
                                                ils.aboutadmin.authors.store.add(newItem);
                                                ils.aboutadmin.authors.EDucationAuthorProfile.close();
                                            }
                                        }
                                    });

                                }
                            }]
                })
            });
            ils.aboutadmin.authors.EDucationAuthorProfile.show();
        }
    }]
});
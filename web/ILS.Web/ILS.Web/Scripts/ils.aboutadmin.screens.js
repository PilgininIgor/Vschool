Ext.ns('ils', 'ils.aboutadmin.screens');

Ext.require('Ext.ux.grid.*');


if (Ext.util.Cookies.get("language") == null) Ext.util.Cookies.set("language", lang_pref);
if (Ext.util.Cookies.get("language") == "Russian") isRussian = true; else isRussian = false;
ils.aboutadmin.screens.textProfile = 'Screenshot';
ils.aboutadmin.screens.textAdd = 'Add screenshot';
ils.aboutadmin.screens.textRemove = 'Remove screenshot';
ils.aboutadmin.screens.textImage = 'Image';
ils.aboutadmin.screens.uploadImage = 'Upload image';
ils.aboutadmin.screens.promptRemove = 'Remove screenshot';
ils.aboutadmin.screens.promptRemoveText = 'Are you sure?';
ils.aboutadmin.screens.alert = 'Info';
ils.aboutadmin.screens.alertText = 'No rows selected';
ils.aboutadmin.screens.gridName = 'List of screenshots';
ils.aboutadmin.screens.profileImage = 'Image';
ils.aboutadmin.screens.screens = 'Screenshots';
if (isRussian) {
    ils.aboutadmin.screens.textProfile = 'Скриншот';
    ils.aboutadmin.screens.textAdd = 'Добавить скриншот';
    ils.aboutadmin.screens.textRemove = 'Удалить скриншот';
    ils.aboutadmin.screens.textImage = 'Изображение';
    ils.aboutadmin.screens.uploadImage = 'Загрузить изображение';
    ils.aboutadmin.screens.promptRemove = 'Удаление скриншота';
    ils.aboutadmin.screens.promptRemoveText = 'Вы уверены?';
    ils.aboutadmin.screens.alert = 'Информация';
    ils.aboutadmin.screens.alertText = 'Не выбрана ни одна строка';
    ils.aboutadmin.screens.gridName = 'Список скриншотов';
    ils.aboutadmin.screens.profileImage = 'Изображение';
    ils.aboutadmin.screens.screens = 'Скриншоты';
}

Ext.define('ScreenModel', {
    extend: 'Ext.data.Model',
    fields: [{
        name: 'Image',
        type: 'string'
    }]
});

ils.aboutadmin.screens.filename = "";

ils.aboutadmin.screens.store = new Ext.data.Store({
    extend: 'Ext.data.Store',
    autoLoad: true,
    autoSync: true,
    model: ScreenModel,
    proxy: {
        type: 'ajax',
        url: ils.aboutadmin.screens.readScreen,
        api: {
            read: ils.aboutadmin.screens.readScreen,
            destroy: ils.aboutadmin.screens.deleteScreen
        },
        reader: new Ext.data.JsonReader({
            rootProperty: 'screenshots'
        }, ScreenModel),
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

addScreenHandler = function () {
    var grid = this.up('grid');

    var uploaded = false;

    ils.aboutadmin.screens.imageForm = new Ext.form.Panel({
        defaultType: 'displayfield',
        fieldDefaults: {
            msgTarget: 'side'
        },
        frame: false,
        border: false,
        bodyStyle: { "background-color": "white" },
        items: [{
            fieldLabel: ils.aboutadmin.screens.profileImage,
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

    ils.aboutadmin.screens.ScreenProfile = new Ext.Window({
        title: ils.aboutadmin.screens.textProfile,
        modal: true,
        layout: 'fit',
        width: 420,
        height: 150,
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
            items: [ils.aboutadmin.screens.imageForm],
            buttons: [{
                formBind: false,
                text: 'OK',
                handler: function () {
                    ils.aboutadmin.screens.filename = ils.aboutadmin.screens.imageForm.items.items[0].getValue();
                    if (uploaded && ils.aboutadmin.screens.filename != null && ils.aboutadmin.screens.filename != "") {
                        var form = ils.aboutadmin.screens.imageForm.getForm();
                        form.submit({
                            url: '/aboutadmin/uploadscreen',
                            waitMsg: 'Uploading your file...',
                            success: function (form, action) {
                                uploadFinished = true;
                                var newItem = ils.aboutadmin.screens.store.model.create({
                                    Image: ils.aboutadmin.screens.filename
                                });
                                ils.aboutadmin.screens.store.add(newItem);
                                ils.aboutadmin.screens.store.sync();
                                ils.aboutadmin.screens.store.reload();
                                ils.aboutadmin.screens.ScreenProfile.close();
                            }
                        });
                    }
                }
            }]
        })
    });
    ils.aboutadmin.screens.ScreenProfile.show();
}

removeScreenHandler = function () {
    var grid = this.up('grid');
    var s = grid.getSelectionModel().getSelection();
    if (!s.length) {
        Ext.Msg.alert(ils.aboutadmin.screens.alert, ils.aboutadmin.screens.alertText);
        return;
    }
    Ext.Msg.confirm(ils.aboutadmin.screens.promptRemove, ils.aboutadmin.screens.promptRemoveText, function (button) {
        if (button == 'yes') {
            Ext.Ajax.request({
                url: ils.aboutadmin.screens.deleteScreen,
                jsonData: {
                    'Image': s[0].data.Image
                },
                success: function (responce, opts) {
                    ils.aboutadmin.screens.store.remove(s[0]);
                }
            });
        }
    });
};

var tlbarScreen = new Ext.panel.Panel({
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
        text: ils.aboutadmin.screens.textAdd,
        handler: addScreenHandler
    }, {
        xtype: 'button',
        ref: '../removeBtn',
        iconCls: 'remove',
        text: ils.aboutadmin.screens.textRemove,
        handler: removeScreenHandler
    }]
});

var ScreenGrid = new Ext.grid.GridPanel({
    store: ils.aboutadmin.screens.store,
    title: ils.aboutadmin.screens.screens,
    columns: [
    {
        header: ils.aboutadmin.screens.textImage,
        dataIndex: 'Image',
        width: 400,
        sortable: true,
        editor: {
            xtype: 'textfield',
            allowBlank: false
        }
    }],
    dockedItems: [tlbarScreen]
});
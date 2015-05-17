Ext.ns('ils', 'ils.aboutadmin.awards.images');

Ext.require('Ext.ux.grid.*');


if (Ext.util.Cookies.get("language") == null) Ext.util.Cookies.set("language", lang_pref);
if (Ext.util.Cookies.get("language") == "Russian") isRussian = true; else isRussian = false;
ils.aboutadmin.awards.images.textProfile = 'Image';
ils.aboutadmin.awards.images.textAdd = 'Add image';
ils.aboutadmin.awards.images.textRemove = 'Remove image';
ils.aboutadmin.awards.images.textImage = 'Image';
ils.aboutadmin.awards.images.uploadImage = 'Upload image';
ils.aboutadmin.awards.images.promptRemove = 'Remove image';
ils.aboutadmin.awards.images.promptRemoveText = 'Are you sure?';
ils.aboutadmin.awards.images.alert = 'Info';
ils.aboutadmin.awards.images.alertText = 'No rows selected';
ils.aboutadmin.awards.images.gridName = 'List of images';
ils.aboutadmin.awards.images.profileImage = 'Image';
ils.aboutadmin.awards.images.screens = 'Images';
if (isRussian) {
    ils.aboutadmin.awards.images.textProfile = 'Изображение';
    ils.aboutadmin.awards.images.textAdd = 'Добавить изображение';
    ils.aboutadmin.awards.images.textRemove = 'Удалить изображение';
    ils.aboutadmin.awards.images.textImage = 'Изображение';
    ils.aboutadmin.awards.images.uploadImage = 'Загрузить изображение';
    ils.aboutadmin.awards.images.promptRemove = 'Удаление изображения';
    ils.aboutadmin.awards.images.promptRemoveText = 'Вы уверены?';
    ils.aboutadmin.awards.images.alert = 'Информация';
    ils.aboutadmin.awards.images.alertText = 'Не выбрана ни одна строка';
    ils.aboutadmin.awards.images.gridName = 'Список изображений';
    ils.aboutadmin.awards.images.profileImage = 'Изображение';
    ils.aboutadmin.awards.images.screens = 'Изображения';
}

Ext.define('AwardImageModel', {
    extend: 'Ext.data.Model',
    fields: [{
        name: 'Image',
        type: 'string'
    }]
});

ils.aboutadmin.awards.images.store = new Ext.data.Store({
    model: AwardImageModel
});

addAwardImage = function () {
    var grid = this.up('grid');

    var uploaded = false;

    ils.aboutadmin.awards.images.imageForm = new Ext.form.Panel({
        defaultType: 'displayfield',
        fieldDefaults: {
            msgTarget: 'side'
        },
        frame: false,
        border: false,
        bodyStyle: { "background-color": "white" },
        items: [{
            fieldLabel: ils.aboutadmin.awards.images.profileImage,
            xtype: 'fileuploadfield',
            name: 'Image',
            id: 'Image',
            width: 300,
            onChange: function (value) {
                var newValue = value.replace(/C:\\fakepath\\/g, '');
                this.setRawValue(newValue);
                uploaded = true;
            }
        }]
    });

    ils.aboutadmin.awards.images.AwardImageProfile = new Ext.Window({
        title: ils.aboutadmin.awards.images.textProfile,
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
            items: [ils.aboutadmin.awards.images.imageForm],
            buttons: [{
                formBind: false,
                text: 'OK',
                handler: function () {
                    var img = ils.aboutadmin.awards.images.imageForm.items.items[0].getValue();
                    if (uploaded && img != null && img != "") {
                        var form = ils.aboutadmin.awards.images.imageForm.getForm();
                        form.submit({
                            url: '/aboutadmin/uploadawardimage',
                            waitMsg: 'Uploading your file...',
                            success: function (form, action) {
                                uploadFinished = true;
                                ils.aboutadmin.awards.images.AwardImageProfile.close();
                            }
                        });
                    }
                    if (img.indexOf("/") != -1)
                        img = img.slice(img.lastIndexOf("/") + 1);
                    if (img.indexOf("\\") != -1)
                        img = img.slice(img.lastIndexOf("\\") + 1);

                    var newItem = { Image: img };
                    ils.aboutadmin.awards.images.store.add(newItem);
                }
            }]
        })
    });
    ils.aboutadmin.awards.images.AwardImageProfile.show();
};

removeAwardImage = function () {
    var grid = this.up('grid');
    var s = grid.getSelectionModel().getSelection();
    if (!s.length) {
        Ext.Msg.alert(ils.aboutadmin.awards.images.alert, ils.aboutadmin.awards.images.alertText);
        return;
    }
    Ext.Msg.confirm(ils.aboutadmin.awards.images.promptRemove, ils.aboutadmin.awards.images.promptRemoveText, function (button) {
        if (button == 'yes') {
            Ext.Ajax.request({
                url: ils.aboutadmin.awards.images.deleteAwardImage,
                jsonData: {
                    'Image': s[0].data.Image
                },
                success: function (responce, opts) {
                    ils.aboutadmin.awards.images.store.remove(s[0]);
                }
            });
        }
    });

};

function getAwardImageGrid() {
    return new Ext.grid.GridPanel({
        store: ils.aboutadmin.awards.images.store,
        title: ils.aboutadmin.awards.images.screens,
        width: "100%",
        height: 200,
        columns: [
		{
		    header: ils.aboutadmin.awards.images.textImage,
		    dataIndex: 'Image',
		    width: 380,
		    sortable: true,
		    editor: {
		        xtype: 'textfield',
		        allowBlank: false
		    }
		}],
        dockedItems: [new Ext.panel.Panel({
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
                text: ils.aboutadmin.awards.images.textAdd,
                handler: addAwardImage
            }, {
                xtype: 'button',
                ref: '../removeBtn',
                iconCls: 'remove',
                text: ils.aboutadmin.awards.images.textRemove,
                handler: removeAwardImage
            }]
        })]
    });
}
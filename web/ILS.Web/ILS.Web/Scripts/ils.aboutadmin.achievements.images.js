Ext.ns('ils', 'ils.aboutadmin.achievements.images');

Ext.require('Ext.ux.grid.*');

if (Ext.util.Cookies.get("language") == null) Ext.util.Cookies.set("language", lang_pref);
if (Ext.util.Cookies.get("language") == "Russian") isRussian = true; else isRussian = false;
ils.aboutadmin.achievements.images.textProfile = 'image';
ils.aboutadmin.achievements.images.textAdd = 'Add image';
ils.aboutadmin.achievements.images.textRemove = 'Remove image';
ils.aboutadmin.achievements.images.textImage = 'Image';
ils.aboutadmin.achievements.images.uploadImage = 'Upload image';
ils.aboutadmin.achievements.images.promptRemove = 'Remove image';
ils.aboutadmin.achievements.images.promptRemoveText = 'Are you sure?';
ils.aboutadmin.achievements.images.alert = 'Info';
ils.aboutadmin.achievements.images.alertText = 'No rows selected';
ils.aboutadmin.achievements.images.gridName = 'List of images';
ils.aboutadmin.achievements.images.profileImage = 'Image';
ils.aboutadmin.achievements.images.screens = 'Images';
if (isRussian) {
    ils.aboutadmin.achievements.images.textProfile = 'Изображение';
    ils.aboutadmin.achievements.images.textAdd = 'Добавить изображение';
    ils.aboutadmin.achievements.images.textRemove = 'Удалить изображение';
    ils.aboutadmin.achievements.images.textImage = 'Изображение';
    ils.aboutadmin.achievements.images.uploadImage = 'Загрузить изображение';
    ils.aboutadmin.achievements.images.promptRemove = 'Удаление изображения';
    ils.aboutadmin.achievements.images.promptRemoveText = 'Вы уверены?';
    ils.aboutadmin.achievements.images.alert = 'Информация';
    ils.aboutadmin.achievements.images.alertText = 'Не выбрана ни одна строка';
    ils.aboutadmin.achievements.images.gridName = 'Список изображений';
    ils.aboutadmin.achievements.images.profileImage = 'Изображение';
    ils.aboutadmin.achievements.images.screens = 'Изображения';
}

Ext.define('AchievementImageModel', {
    extend: 'Ext.data.Model',
    fields: [{
        name: 'Image',
        type: 'string'
    }]
});

ils.aboutadmin.achievements.images.store = new Ext.data.Store({
    model: AchievementImageModel
});


addAchievementImage = function () {
    var grid = this.up('grid');

    var uploaded = false;

    ils.aboutadmin.achievements.images.imageForm = new Ext.form.Panel({
        defaultType: 'displayfield',
        fieldDefaults: {
            msgTarget: 'side'
        },
        frame: false,
        border: false,
        bodyStyle: { "background-color": "white" },
        items: [{
            fieldLabel: ils.aboutadmin.achievements.images.profileImage,
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

    ils.aboutadmin.achievements.images.AchievementImageProfile = new Ext.Window({
        title: ils.aboutadmin.achievements.images.textProfile,
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
            items: [ils.aboutadmin.achievements.images.imageForm],
            buttons: [{
                formBind: false,
                text: 'OK',
                handler: function () {
                    var img = ils.aboutadmin.achievements.images.imageForm.items.items[0].getValue();
                    if (uploaded && img != null && img != "") {
                        var form = ils.aboutadmin.achievements.images.imageForm.getForm();
                        form.submit({
                            url: '/aboutadmin/uploadachievementimage',
                            waitMsg: 'Uploading your file...',
                            success: function (form, action) {
                                uploadFinished = true;
                                ils.aboutadmin.achievements.images.AchievementImageProfile.close();
                            }
                        });
                    }
                    if (img.indexOf("/") != -1)
                        img = img.slice(img.lastIndexOf("/") + 1);
                    if (img.indexOf("\\") != -1)
                        img = img.slice(img.lastIndexOf("\\") + 1);
                    var newItem = { Image: img };
                    ils.aboutadmin.achievements.images.store.add(newItem);
                }
            }]
        })
    });
    ils.aboutadmin.achievements.images.AchievementImageProfile.show();
};

removeAchievementImage = function () {
    var grid = this.up('grid');
    var s = grid.getSelectionModel().getSelection();
    if (!s.length) {
        Ext.Msg.alert(ils.aboutadmin.achievements.images.alert, ils.aboutadmin.achievements.images.alertText);
        return;
    }
    Ext.Msg.confirm(ils.aboutadmin.achievements.images.promptRemove, ils.aboutadmin.achievements.images.promptRemoveText, function (button) {
        if (button == 'yes') {
            Ext.Ajax.request({
                url: ils.aboutadmin.achievements.images.deleteAchievementImage,
                jsonData: {
                    'Image': s[0].data.Image
                },
                success: function (responce, opts) {
                    ils.aboutadmin.achievements.images.store.remove(s[0]);
                }
            });
        }
    });
};

function getAchievementImageGrid() {

    return new Ext.grid.GridPanel({
        store: ils.aboutadmin.achievements.images.store,
        title: ils.aboutadmin.achievements.images.screens,
        width: "100%",
        height: 200,
        columns: [
		{
		    header: ils.aboutadmin.achievements.images.textImage,
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
                text: ils.aboutadmin.achievements.images.textAdd,
                handler: addAchievementImage
            }, {
                xtype: 'button',
                ref: '../removeBtn',
                iconCls: 'remove',
                text: ils.aboutadmin.achievements.images.textRemove,
                handler: removeAchievementImage
            }]
        })]
    });
}
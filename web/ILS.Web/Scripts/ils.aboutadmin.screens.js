Ext.ns('ils', 'ils.aboutadmin.screens');

Ext.require('Ext.ux.grid.*');


if (Ext.util.Cookies.get("language") == null) Ext.util.Cookies.set("language", lang_pref);
if (Ext.util.Cookies.get("language") == "Russian") isRussian = true; else isRussian = false;
ils.aboutadmin.screens.textProfile = 'Screenshot';
ils.aboutadmin.screens.textAdd = 'Add Screenshot';
ils.aboutadmin.screens.textRemove = 'Remove Screenshot';
ils.aboutadmin.screens.textImage = 'Image';
ils.aboutadmin.screens.uploadImage = 'Upload Image';
ils.aboutadmin.screens.promptRemove = 'Remove Screenshot';
ils.aboutadmin.screens.promptRemoveText = 'Are you sure?';
ils.aboutadmin.screens.alert = 'Info';
ils.aboutadmin.screens.alertText = 'No rows selected';
ils.aboutadmin.screens.gridName = 'List of Screenshots';
ils.aboutadmin.screens.profileImage = 'Image';
ils.aboutadmin.screens.screens = 'Screenshots';
if (isRussian)
{
	ils.aboutadmin.screens.textProfile = 'Скриншот';
	ils.aboutadmin.screens.textAdd = 'Добавить Скриншот';
	ils.aboutadmin.screens.textRemove = 'Удалить Скриншот';
	ils.aboutadmin.screens.textImage = 'Изображение';
	ils.aboutadmin.screens.uploadImage = 'Загрузить изображение';
	ils.aboutadmin.screens.promptRemove = 'Удаление Скриншота';
	ils.aboutadmin.screens.promptRemoveText = 'Вы уверены?';
	ils.aboutadmin.screens.alert = 'Информация';
	ils.aboutadmin.screens.alertText = 'Не выбрана ни одна строка';
	ils.aboutadmin.screens.gridName = 'Список Скриншотов';
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

ils.aboutadmin.screens.store = new Ext.data.Store({
    proxy: new Ext.data.HttpProxy({
        api: {
            read : ils.aboutadmin.screens.readScreen,
            destroy: ils.aboutadmin.screens.deleteScreen
        },
		headers: { 'Content-Type': 'application/json; charset=UTF-8' },
		afterRequest: function(req, res) {
              
			var a = eval('(' + req.operation.response.responseText + ')');

			ils.aboutadmin.screens.store.loadData([], false);
			
			for (var Name in a) {
				if(a.hasOwnProperty(Name)){
				    ils.aboutadmin.screens.store.add(a[Name]);
				}
			}
			
			
        }
    }),
	model:  ScreenModel,
    reader: new Ext.data.JsonReader({
            totalProperty: 'total',
            root: 'data'
        }, ScreenModel),
	writer: new Ext.data.JsonWriter({
            encode: false,
            listful: true,
            writeAllFields: true
        }),
    autoSave: true,
    autoLoad: true,
    remoteSort: false
});





var ScreenGrid = new Ext.grid.GridPanel({
    store: ils.aboutadmin.screens.store,
    title: ils.aboutadmin.screens.screens,
    columns: [
    {
        header: ils.aboutadmin.screens.textImage,
        dataIndex: 'Image',
        width: 200,
        sortable: true,
        editor: {
            xtype: 'textfield',
            allowBlank: false

        }
    }],
    tbar: [{
        ref: '../removeBtn',
        iconCls: 'remove',
        text: ils.aboutadmin.screens.textRemove,
        handler: function () {
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

        }
    }, {
        ref: '../addBtn',
        iconCls: 'add',
        text: ils.aboutadmin.screens.textAdd,
        handler: function () {
            var grid = this.up('grid');

            var uploaded = false;

            ils.aboutadmin.screens.imageForm = new Ext.form.Panel({
                defaultType: 'displayfield',
                fieldDefaults: {
                    msgTarget: 'side'
                },
                frame: false,
                border: false,
                bodyStyle: { "background-color": "#DFE8F6" },
                items: [{
                    fieldLabel: ils.aboutadmin.screens.profileImage,
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

            ils.aboutadmin.screens.ScreenProfile = new Ext.Window({
                title: ils.aboutadmin.screens.textProfile,
                layout: 'fit',
                width: 500,
                height: 100,
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
                    items: [ils.aboutadmin.screens.imageForm,
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
                                    var img = ils.aboutadmin.screens.imageForm.items.items[0].getValue();
                                    if (uploaded && img != null && img != "") {
                                        var form = ils.aboutadmin.screens.imageForm.getForm();
                                        form.submit({
                                            url: '/ils2/aboutadmin/uploadscreen',
                                            waitMsg: 'Uploading your file...',
                                            success: function (form, action) {
                                                uploadFinished = true;
                                                ils.aboutadmin.screens.ScreenProfile.close();
                                            }
                                        });
                                    }
									if (img.indexOf("/") != -1)
										img = img.slice(img.lastIndexOf("/") + 1);
									if (img.indexOf("\\") != -1)
										img = img.slice(img.lastIndexOf("\\") + 1);
									var newItem = {Image: img};
									ils.aboutadmin.screens.store.add(newItem);
                                }
                            }]
                })
            });
            ils.aboutadmin.screens.ScreenProfile.show();
        }
    }]
});
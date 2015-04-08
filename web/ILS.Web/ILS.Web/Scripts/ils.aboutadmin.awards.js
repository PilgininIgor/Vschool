Ext.ns('ils', 'ils.aboutadmin.awards');

Ext.require('Ext.ux.grid.*');


if (Ext.util.Cookies.get("language") == null) Ext.util.Cookies.set("language", lang_pref);
if (Ext.util.Cookies.get("language") == "Russian") isRussian = true; else isRussian = false;
ils.aboutadmin.awards.textProfile = 'Award';
ils.aboutadmin.awards.textAdd = 'Add Award';
ils.aboutadmin.awards.textRemove = 'Remove Award';
ils.aboutadmin.awards.textName = 'Name';
ils.aboutadmin.awards.textDescription = 'Description';
ils.aboutadmin.awards.textImage = 'Image';
ils.aboutadmin.awards.uploadImage = 'Upload Image';
ils.aboutadmin.awards.promptDescription = 'Description';
ils.aboutadmin.awards.promptDescriptionText = 'Please enter description:';
ils.aboutadmin.awards.promptName = 'Name';
ils.aboutadmin.awards.promptNameText = 'Please enter name:';
ils.aboutadmin.awards.promptRemove = 'Remove Award';
ils.aboutadmin.awards.promptRemoveText = 'Are you sure?';
ils.aboutadmin.awards.alert = 'Info';
ils.aboutadmin.awards.alertText = 'No rows selected';
ils.aboutadmin.awards.gridName = 'List of Awards';
ils.aboutadmin.awards.profileName = 'Name';
ils.aboutadmin.awards.profileDescription = 'Description';
ils.aboutadmin.awards.profileImage = 'Image';
ils.aboutadmin.awards.profilePriority = 'Priority';
ils.aboutadmin.awards.authors = 'Awards';
if (isRussian)
{
	ils.aboutadmin.awards.textProfile = 'Награда';
	ils.aboutadmin.awards.textAdd = 'Добавить награды';
	ils.aboutadmin.awards.textRemove = 'Удалить награды';
	ils.aboutadmin.awards.textName = 'Название';
	ils.aboutadmin.awards.textDescription = 'Описание';
	ils.aboutadmin.awards.textImage = 'Изображение';
	ils.aboutadmin.awards.uploadImage = 'Загрузить изображение';
	ils.aboutadmin.awards.promptDescription = 'Описание';
	ils.aboutadmin.awards.promptDescriptionText = 'Пожалуйста, введите описание:';
	ils.aboutadmin.awards.promptName = 'Название';
	ils.aboutadmin.awards.promptNameText = 'Пожалуйста, введите название:';
	ils.aboutadmin.awards.promptRemove = 'Удаление награды';
	ils.aboutadmin.awards.promptRemoveText = 'Вы уверены?';
	ils.aboutadmin.awards.alert = 'Информация';
	ils.aboutadmin.awards.alertText = 'Не выбрана ни одна строка';
	ils.aboutadmin.awards.gridName = 'Список авторов';
	ils.aboutadmin.awards.profileName = 'Название';
	ils.aboutadmin.awards.profileDescription = 'Описание';
	ils.aboutadmin.awards.profileImage = 'Изображение';
	ils.aboutadmin.awards.profilePriority = 'Приоритет';
    ils.aboutadmin.awards.authors = 'Награды';
}

Ext.define('AwardModel', {
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

ils.aboutadmin.awards.store = new Ext.data.Store({
    proxy: new Ext.data.HttpProxy({
        api: {
            read : ils.aboutadmin.awards.readAward,
            create: ils.aboutadmin.awards.createAward,
            update: ils.aboutadmin.awards.updateAward,
            destroy: ils.aboutadmin.awards.deleteAward
        },
		headers: { 'Content-Type': 'application/json; charset=UTF-8' },
		afterRequest: function(req, res) {
              
			var a = eval('(' + req.operation.response.responseText + ')');

			ils.aboutadmin.awards.store.loadData([], false);
			
			for (var Name in a) {
				if(a.hasOwnProperty(Name)){
				    ils.aboutadmin.awards.store.add(a[Name]);
				}
			}
			
			
        }
    }),
	model:  AwardModel,
    reader: new Ext.data.JsonReader({
            totalProperty: 'total',
            rootProperty: 'data'
        }, AwardModel),
	writer: new Ext.data.JsonWriter({
            encode: false,
            listful: true,
            writeAllFields: true
        }),
    autoSave: true,
    autoLoad: true,
    remoteSort: false
});





var AwardGrid = new Ext.grid.GridPanel({
    store: ils.aboutadmin.awards.store,
    title: ils.aboutadmin.awards.authors,
    columns: [
    {
        header: ils.aboutadmin.awards.textName,
        dataIndex: 'Name',
        width: 220,
        sortable: true,
        editor: {
            xtype: 'textfield',
            allowBlank: false
        }
    }, {
        header: ils.aboutadmin.awards.textImage,
        dataIndex: 'Image',
        width: 200,
        sortable: true,
        editor: {
            xtype: 'textfield',
            allowBlank: false

        }
    }, {
        header: ils.aboutadmin.awards.textDescription,
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
                Ext.Msg.alert(ils.aboutadmin.awards.alert, ils.aboutadmin.awards.alertText);
                return;
            }
            var index = grid.store.indexOf(s[0]);
            Ext.Ajax.request({
                url: document.location.href + '/AwardProfile',
                success: function (response, opts) {
                    var a = eval('(' + response.responseText + ')');

                    ils.aboutadmin.awards.AwardProfile = new Ext.Window({
                        title: ils.aboutadmin.awards.textProfile,
                        layout: 'fit',
                        width: 500,
                        height: 470,
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

                                fieldLabel: ils.aboutadmin.awards.profileName,
                                name: 'AwardName',
                                id: 'AwardName',
                                width: 450,
                                y: 5,
                                x: 15,
                                value: a[0].Name
                            }, {
                                fieldLabel: ils.aboutadmin.awards.profileDescription,
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
                                fieldLabel: ils.aboutadmin.awards.profilePriority,
                                xtype: 'textfield',
                                name: 'Priority',
                                id: 'Priority',
                                y: 5,
                                x: 15,
                                value: a[0].Priority

                            },
                            getAwardImageGrid(),
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
                                    var img = "";
									for (var awardImage in ils.aboutadmin.awards.images.store.data.items)
										img += ils.aboutadmin.awards.images.store.data.items[awardImage].data.Image + ";";
                                    if (img == null || img == "")
                                        img = a[0].Image;
                                    var f = ils.aboutadmin.awards.AwardProfile.items.items[0];
                                    Ext.Ajax.request({
                                        url: document.location.href + '/UpdateAward',
                                        jsonData: {
                                            'name': f.getComponent('AwardName').getValue(),
                                            'description': f.getComponent('Description').getValue(),
                                            'image': img,
                                            'priority': f.getComponent('Priority').getValue()
                                        },
                                        success: function (responce, opts) {
											ils.aboutadmin.awards.AwardProfile.close();
                                        }
                                    });

                                }
                            }]
                        })
                    });
                    ils.aboutadmin.awards.AwardProfile.show();
					ils.aboutadmin.awards.images.store.loadData([], false);
					var images = a[0].Image.split(";");
					for (var img in images) {
						if (images[img] != "") {
							var newItem = {Image: images[img]};
							ils.aboutadmin.awards.images.store.add(newItem);
						}
					}
                },
                jsonData: {
                    'name': ils.aboutadmin.awards.store.data.items[index].data.Name

                }
            });

        }
    },
    tbar: [{
        ref: '../removeBtn',
        iconCls: 'remove',
        text: ils.aboutadmin.awards.textRemove,
        handler: function () {
            var grid = this.up('grid');
            var s = grid.getSelectionModel().getSelection();
            if (!s.length) {
                Ext.Msg.alert(ils.aboutadmin.awards.alert, ils.aboutadmin.awards.alertText);
                return;
            }
            Ext.Msg.confirm(ils.aboutadmin.awards.promptRemove, ils.aboutadmin.awards.promptRemoveText, function (button) {
                if (button == 'yes') {
                    Ext.Ajax.request({
                        url: ils.aboutadmin.awards.deleteAward,
                        jsonData: {
                            'name': s[0].data.Name
                        },
                        success: function (responce, opts) {
                            ils.aboutadmin.awards.store.remove(s[0]);
                        }
                    });
                }
            });

        }
    }, {
        ref: '../addBtn',
        iconCls: 'add',
        text: ils.aboutadmin.awards.textAdd,
        handler: function () {
            var grid = this.up('grid');

            var uploaded = false;

			ils.aboutadmin.awards.AwardProfile = new Ext.Window({
				title: ils.aboutadmin.awards.textProfile,
				layout: 'fit',
				width: 500,
				height: 470,
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

						fieldLabel: ils.aboutadmin.awards.profileName,
						xtype: 'textfield',
						name: 'AwardName',
						id: 'AwardName',
						width: 450,
						y: 5,
						x: 15
					}, {
						fieldLabel: ils.aboutadmin.awards.profileDescription,
						xtype: 'textarea',
						name: 'Description',
						id: 'Description',
						height: 110,
						width: 450,
						grow: false,
						y: 5,
						x: 15
					}, {
						fieldLabel: ils.aboutadmin.awards.profilePriority,
						xtype: 'textfield',
						name: 'Priority',
						id: 'Priority',
						y: 5,
						x: 15
					},
					getAwardImageGrid(),
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
							var img = "";
							for (var awardImage in ils.aboutadmin.awards.images.store.data.items)
								img += ils.aboutadmin.awards.images.store.data.items[awardImage].data.Image + ";";
							var f = ils.aboutadmin.awards.AwardProfile.items.items[0];
							Ext.Ajax.request({
								url: document.location.href + '/CreateAward',
								jsonData: {
									'name': f.getComponent('AwardName').getValue(),
									'description': f.getComponent('Description').getValue(),
									'image': img,
									'priority': f.getComponent('Priority').getValue()
								},
								success: function (responce, opts) {
									var newItem = AwardModel.create(
									{
										Name: f.getComponent('AwardName').getValue(),
										Description: f.getComponent('Description').getValue(),
										Image: img,
										Priority: f.getComponent('Priority').getValue()
									});
									ils.aboutadmin.awards.store.add(newItem);
									ils.aboutadmin.awards.AwardProfile.close();
								}
							});

						}
					}]
				})
			});
			ils.aboutadmin.awards.AwardProfile.show();
			ils.aboutadmin.awards.images.store.loadData([], false);
        }
    }]
});
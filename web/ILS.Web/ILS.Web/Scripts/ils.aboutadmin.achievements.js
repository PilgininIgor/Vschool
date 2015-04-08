Ext.ns('ils', 'ils.aboutadmin.achievements');

Ext.require('Ext.ux.grid.*');


if (Ext.util.Cookies.get("language") == null) Ext.util.Cookies.set("language", lang_pref);
if (Ext.util.Cookies.get("language") == "Russian") isRussian = true; else isRussian = false;
ils.aboutadmin.achievements.textProfile = 'Achievement';
ils.aboutadmin.achievements.textAdd = 'Add Achievement';
ils.aboutadmin.achievements.textRemove = 'Remove Achievement';
ils.aboutadmin.achievements.textName = 'Name';
ils.aboutadmin.achievements.textDescription = 'Description';
ils.aboutadmin.achievements.textImage = 'Image';
ils.aboutadmin.achievements.uploadImage = 'Upload Image';
ils.aboutadmin.achievements.promptDescription = 'Description';
ils.aboutadmin.achievements.promptDescriptionText = 'Please enter description:';
ils.aboutadmin.achievements.promptName = 'Name';
ils.aboutadmin.achievements.promptNameText = 'Please enter name:';
ils.aboutadmin.achievements.promptRemove = 'Remove Achievement';
ils.aboutadmin.achievements.promptRemoveText = 'Are you sure?';
ils.aboutadmin.achievements.alert = 'Info';
ils.aboutadmin.achievements.alertText = 'No rows selected';
ils.aboutadmin.achievements.gridName = 'List of Achievements';
ils.aboutadmin.achievements.profileName = 'Name';
ils.aboutadmin.achievements.profileDescription = 'Description';
ils.aboutadmin.achievements.profileImage = 'Image';
ils.aboutadmin.achievements.profilePriority = 'Priority';
ils.aboutadmin.achievements.authors = 'Achievements';
if (isRussian)
{
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
    proxy: new Ext.data.HttpProxy({
        api: {
            read : ils.aboutadmin.achievements.readAchievement,
            create: ils.aboutadmin.achievements.createAchievement,
            update: ils.aboutadmin.achievements.updateAchievement,
            destroy: ils.aboutadmin.achievements.deleteAchievement
        },
		headers: { 'Content-Type': 'application/json; charset=UTF-8' },
		afterRequest: function(req, res) {
              
			var a = eval('(' + req.operation.response.responseText + ')');

			ils.aboutadmin.achievements.store.loadData([], false);
			
			for (var Name in a) {
				if(a.hasOwnProperty(Name)){
				    ils.aboutadmin.achievements.store.add(a[Name]);
				}
			}
			
			
        }
    }),
	model:  AchievementModel,
    reader: new Ext.data.JsonReader({
            totalProperty: 'total',
            rootProperty: 'data'
        }, AchievementModel),
	writer: new Ext.data.JsonWriter({
            encode: false,
            listful: true,
            writeAllFields: true
        }),
    autoSave: true,
    autoLoad: true,
    remoteSort: false
});





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
        itemdblclick: function () {
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

                                fieldLabel: ils.aboutadmin.achievements.profileName,
                                name: 'AchievementName',
                                id: 'AchievementName',
                                width: 450,
                                y: 5,
                                x: 15,
                                value: a[0].Name
                            }, {
                                fieldLabel: ils.aboutadmin.achievements.profileDescription,
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
                                fieldLabel: ils.aboutadmin.achievements.profilePriority,
                                xtype: 'textfield',
                                name: 'Priority',
                                id: 'Priority',
                                y: 5,
                                x: 15,
                                value: a[0].Priority

                            },
                            getAchievementImageGrid(),
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
							var newItem = {Image: images[img]};
							ils.aboutadmin.achievements.images.store.add(newItem);
						}
					}
                },
                jsonData: {
                    'name': ils.aboutadmin.achievements.store.data.items[index].data.Name

                }
            });

        }
    },
    tbar: [{
        ref: '../removeBtn',
        iconCls: 'remove',
        text: ils.aboutadmin.achievements.textRemove,
        handler: function () {
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

        }
    }, {
        ref: '../addBtn',
        iconCls: 'add',
        text: ils.aboutadmin.achievements.textAdd,
        handler: function () {
            var grid = this.up('grid');

            var uploaded = false;

			ils.aboutadmin.achievements.AchievementProfile = new Ext.Window({
				title: ils.aboutadmin.achievements.textProfile,
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

						fieldLabel: ils.aboutadmin.achievements.profileName,
						xtype: 'textfield',
						name: 'AchievementName',
						id: 'AchievementName',
						width: 450,
						y: 5,
						x: 15
					}, {
						fieldLabel: ils.aboutadmin.achievements.profileDescription,
						xtype: 'textarea',
						name: 'Description',
						id: 'Description',
						height: 110,
						width: 450,
						grow: false,
						y: 5,
						x: 15
					}, {
						fieldLabel: ils.aboutadmin.achievements.profilePriority,
						xtype: 'textfield',
						name: 'Priority',
						id: 'Priority',
						y: 5,
						x: 15
					},
					getAchievementImageGrid(),
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
        }
    }]
});
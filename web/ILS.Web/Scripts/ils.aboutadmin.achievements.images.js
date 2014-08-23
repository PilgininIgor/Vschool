Ext.ns('ils', 'ils.aboutadmin.achievements.images');

Ext.require('Ext.ux.grid.*');


if (Ext.util.Cookies.get("language") == null) Ext.util.Cookies.set("language", lang_pref);
if (Ext.util.Cookies.get("language") == "Russian") isRussian = true; else isRussian = false;
ils.aboutadmin.achievements.images.textProfile = 'Image';
ils.aboutadmin.achievements.images.textAdd = 'Add Image';
ils.aboutadmin.achievements.images.textRemove = 'Remove Image';
ils.aboutadmin.achievements.images.textImage = 'Image';
ils.aboutadmin.achievements.images.uploadImage = 'Upload Image';
ils.aboutadmin.achievements.images.promptRemove = 'Remove Image';
ils.aboutadmin.achievements.images.promptRemoveText = 'Are you sure?';
ils.aboutadmin.achievements.images.alert = 'Info';
ils.aboutadmin.achievements.images.alertText = 'No rows selected';
ils.aboutadmin.achievements.images.gridName = 'List of Images';
ils.aboutadmin.achievements.images.profileImage = 'Image';
ils.aboutadmin.achievements.images.screens = 'Images';
if (isRussian)
{
	ils.aboutadmin.achievements.images.textProfile = 'Изображение';
	ils.aboutadmin.achievements.images.textAdd = 'Добавить Изображение';
	ils.aboutadmin.achievements.images.textRemove = 'Удалить Изображение';
	ils.aboutadmin.achievements.images.textImage = 'Изображение';
	ils.aboutadmin.achievements.images.uploadImage = 'Загрузить изображение';
	ils.aboutadmin.achievements.images.promptRemove = 'Удаление Изображения';
	ils.aboutadmin.achievements.images.promptRemoveText = 'Вы уверены?';
	ils.aboutadmin.achievements.images.alert = 'Информация';
	ils.aboutadmin.achievements.images.alertText = 'Не выбрана ни одна строка';
	ils.aboutadmin.achievements.images.gridName = 'Список Изображений';
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
	model:  AchievementImageModel
});

function getAchievementImageGrid() {

	return new Ext.grid.GridPanel({
		store: ils.aboutadmin.achievements.images.store,
		title: ils.aboutadmin.achievements.images.screens,
		height: 200,
		columns: [
		{
			header: ils.aboutadmin.achievements.images.textImage,
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
			text: ils.aboutadmin.achievements.images.textRemove,
			handler: function () {
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

			}
		}, {
			ref: '../addBtn',
			iconCls: 'add',
			text: ils.aboutadmin.achievements.images.textAdd,
			handler: function () {
				var grid = this.up('grid');

				var uploaded = false;

				ils.aboutadmin.achievements.images.imageForm = new Ext.form.Panel({
					defaultType: 'displayfield',
					fieldDefaults: {
						msgTarget: 'side'
					},
					frame: false,
					border: false,
					bodyStyle: { "background-color": "#DFE8F6" },
					items: [{
						fieldLabel: ils.aboutadmin.achievements.images.profileImage,
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

				ils.aboutadmin.achievements.images.AchievementImageProfile = new Ext.Window({
					title: ils.aboutadmin.achievements.images.textProfile,
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
						items: [ils.aboutadmin.achievements.images.imageForm,
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
										var img = ils.aboutadmin.achievements.images.imageForm.items.items[0].getValue();
										if (uploaded && img != null && img != "") {
											var form = ils.aboutadmin.achievements.images.imageForm.getForm();
											form.submit({
												url: '/ils2/aboutadmin/uploadachievementimage',
												waitMsg: 'Uploading your file...',
												success: function (form, action) {
													uploadFinished = true;
													ils.aboutadmin.achievements.images.AchievementImageProfile.close();
												}
											});
										}
										
										var newItem = {Image: img};
										ils.aboutadmin.achievements.images.store.add(newItem);
									}
								}]
					})
				});
				ils.aboutadmin.achievements.images.AchievementImageProfile.show();
			}
		}]
	});
}
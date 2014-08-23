Ext.ns('ils', 'ils.aboutadmin.awards.images');

Ext.require('Ext.ux.grid.*');


if (Ext.util.Cookies.get("language") == null) Ext.util.Cookies.set("language", lang_pref);
if (Ext.util.Cookies.get("language") == "Russian") isRussian = true; else isRussian = false;
ils.aboutadmin.awards.images.textProfile = 'Image';
ils.aboutadmin.awards.images.textAdd = 'Add Image';
ils.aboutadmin.awards.images.textRemove = 'Remove Image';
ils.aboutadmin.awards.images.textImage = 'Image';
ils.aboutadmin.awards.images.uploadImage = 'Upload Image';
ils.aboutadmin.awards.images.promptRemove = 'Remove Image';
ils.aboutadmin.awards.images.promptRemoveText = 'Are you sure?';
ils.aboutadmin.awards.images.alert = 'Info';
ils.aboutadmin.awards.images.alertText = 'No rows selected';
ils.aboutadmin.awards.images.gridName = 'List of Images';
ils.aboutadmin.awards.images.profileImage = 'Image';
ils.aboutadmin.awards.images.screens = 'Images';
if (isRussian)
{
	ils.aboutadmin.awards.images.textProfile = 'Изображение';
	ils.aboutadmin.awards.images.textAdd = 'Добавить Изображение';
	ils.aboutadmin.awards.images.textRemove = 'Удалить Изображение';
	ils.aboutadmin.awards.images.textImage = 'Изображение';
	ils.aboutadmin.awards.images.uploadImage = 'Загрузить изображение';
	ils.aboutadmin.awards.images.promptRemove = 'Удаление Изображения';
	ils.aboutadmin.awards.images.promptRemoveText = 'Вы уверены?';
	ils.aboutadmin.awards.images.alert = 'Информация';
	ils.aboutadmin.awards.images.alertText = 'Не выбрана ни одна строка';
	ils.aboutadmin.awards.images.gridName = 'Список Изображений';
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
	model:  AwardImageModel
});

function getAwardImageGrid() {

	return new Ext.grid.GridPanel({
		store: ils.aboutadmin.awards.images.store,
		title: ils.aboutadmin.awards.images.screens,
		height: 200,
		columns: [
		{
			header: ils.aboutadmin.awards.images.textImage,
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
			text: ils.aboutadmin.awards.images.textRemove,
			handler: function () {
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

			}
		}, {
			ref: '../addBtn',
			iconCls: 'add',
			text: ils.aboutadmin.awards.images.textAdd,
			handler: function () {
				var grid = this.up('grid');

				var uploaded = false;

				ils.aboutadmin.awards.images.imageForm = new Ext.form.Panel({
					defaultType: 'displayfield',
					fieldDefaults: {
						msgTarget: 'side'
					},
					frame: false,
					border: false,
					bodyStyle: { "background-color": "#DFE8F6" },
					items: [{
						fieldLabel: ils.aboutadmin.awards.images.profileImage,
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

				ils.aboutadmin.awards.images.AwardImageProfile = new Ext.Window({
					title: ils.aboutadmin.awards.images.textProfile,
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
						items: [ils.aboutadmin.awards.images.imageForm,
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
										var img = ils.aboutadmin.awards.images.imageForm.items.items[0].getValue();
										if (uploaded && img != null && img != "") {
											var form = ils.aboutadmin.awards.images.imageForm.getForm();
											form.submit({
												url: '/ils2/aboutadmin/uploadawardimage',
												waitMsg: 'Uploading your file...',
												success: function (form, action) {
													uploadFinished = true;
													ils.aboutadmin.awards.images.AwardImageProfile.close();
												}
											});
										}
										
										var newItem = {Image: img};
										ils.aboutadmin.awards.images.store.add(newItem);
									}
								}]
					})
				});
				ils.aboutadmin.awards.images.AwardImageProfile.show();
			}
		}]
	});
}
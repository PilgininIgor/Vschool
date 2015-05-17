Ext.ns('ils', 'ils.admin');

Ext.require('Ext.ux.grid.*');

if (Ext.util.Cookies.get("language") == null) Ext.util.Cookies.set("language", lang_pref);
if (Ext.util.Cookies.get("language") == "Russian") isRussian = true; else isRussian = false;
ils.admin.textProfile = 'User profile';
ils.admin.textAdd = 'Add User';
ils.admin.textRemove = 'Remove User';
ils.admin.textSaveChanges = 'Save Changes';
ils.admin.textToggle = 'Approve User';
ils.admin.textName = 'User Name';
ils.admin.textFirstName = 'Name';
ils.admin.textLastName = 'Surname';
ils.admin.textEmail = 'Email';
ils.admin.textIsApproved = 'Active';
ils.admin.promptEmail = 'Email';
ils.admin.promptEmailText = 'Please enter your email:';
ils.admin.promptName = 'Name';
ils.admin.promptNameText = 'Please enter your name:';
ils.admin.promptRemove = 'Remove User';
ils.admin.promptRemoveText = 'Are you sure?';
ils.admin.alert = 'Info';
ils.admin.alertText = 'No rows selected';
ils.admin.gridName = 'List of users';
ils.admin.profileName = 'Account';
ils.admin.profileEmail = 'Email';
ils.admin.profileFirstName = 'First name';
ils.admin.profileLastName = 'Last name';
ils.admin.profileEXP = 'Experience';
ils.admin.profileAdmin = 'Admin';
ils.admin.profileTeacher = 'Teacher';
ils.admin.profileStudent = 'Student';
if (isRussian) {
    ils.admin.textProfile = 'Профиль пользователя';
    ils.admin.textAdd = 'Добавить пользователя';
    ils.admin.textRemove = 'Удалить пользователя';
    ils.admin.textSaveChanges = 'Сохранить изменения';
    ils.admin.textToggle = 'Утвердить пользователя';
    ils.admin.textName = 'Имя пользователя';
    ils.admin.textFirstName = 'Имя';
    ils.admin.textLastName = 'Фамилия';
    ils.admin.textEmail = 'Email';
    ils.admin.textIsApproved = 'Утвержден';
    ils.admin.promptEmail = 'Email';
    ils.admin.promptEmailText = 'Пожалуйста, введите email:';
    ils.admin.promptName = 'Имя';
    ils.admin.promptNameText = 'Пожалуйста, введите имя:';
    ils.admin.promptRemove = 'Удаление пользователя';
    ils.admin.promptRemoveText = 'Вы уверены?';
    ils.admin.alert = 'Информация';
    ils.admin.alertText = 'Не выбрана ни одна строка';
    ils.admin.gridName = 'Список пользователей';
    ils.admin.profileName = 'Аккаунт';
    ils.admin.profileEmail = 'Email';
    ils.admin.profileFirstName = 'Имя';
    ils.admin.profileLastName = 'Фамилия';
    ils.admin.profileEXP = 'Опыт';
    ils.admin.profileAdmin = 'Администратор';
    ils.admin.profileTeacher = 'Преподаватель';
    ils.admin.profileStudent = 'Слушатель';
}

Ext.define('UserModel', {
    extend: 'Ext.data.Model',
    fields: [{
        name: 'RecordId',
        type: 'string'
    }, {
        name: 'Name',
        type: 'string'
    }, {
        name: 'FirstName',
        type: 'string'
    }, {
        name: 'LastName',
        type: 'string'
    }, {
        name: 'Email',
        type: 'string'
    }, {
        name: 'IsApproved',
        type: 'boolean'
    }]
});

ils.admin.store = new Ext.data.Store({
    extend: 'Ext.data.Store',
    autoLoad: true,
    //autoSync: true,
    model: UserModel,
    proxy: {
        type: 'ajax',
        url: ils.admin.readUser,
        api: {
            read: ils.admin.readUser,
            create: ils.admin.createUser,
            update: ils.admin.updateUser,
            destroy: ils.admin.deleteUser
        },
        reader: new Ext.data.JsonReader({
            totalProperty: 'total',
            rootProperty: 'data'
        }, UserModel),
        writer: new Ext.data.JsonWriter({
            encode: false,
            listful: true,
            writeAllFields: true,
            getRecordData: function (record) {
                return { 'data': Ext.JSON.encode(record.data) };
            }
        }),
        headers: { 'Content-Type': 'application/json; charset=UTF-8' },
        autoSave: true,
        autoLoad: true,
        remoteSort: false
    }
});

ils.admin.userGrid = new Ext.grid.GridPanel({
    store: ils.admin.store,
    columns: [
    {
        header: ils.admin.textName,
        dataIndex: 'Name',
        width: 150,
        sortable: true,
        editor: {
            xtype: 'textfield',
            allowBlank: false
        }
    }, {
        header: ils.admin.textFirstName,
        dataIndex: 'FirstName',
        width: 150,
        sortable: true,
        editor: {
            xtype: 'textfield',
            allowBlank: false
        }
    }, {
        header: ils.admin.textLastName,
        dataIndex: 'LastName',
        width: 150,
        sortable: true,
        editor: {
            xtype: 'textfield',
            allowBlank: false
        }
    }, {
        header: ils.admin.textEmail,
        dataIndex: 'Email',
        width: 250,
        sortable: true,
        editor: {
            xtype: 'textfield',
            allowBlank: false
        }
    }, {
        xtype: 'booleancolumn',
        header: ils.admin.textIsApproved,
        dataIndex: 'IsApproved',
        width: 150,
        trueText: 'Yes',
        falseText: 'No',
        editor: {
            xtype: 'checkbox'
        }
    }],
    listeners: {
        itemdblclick: function () {
            var grid = this;
            var s = grid.getSelectionModel().getSelection();
            if (!s.length) {
                Ext.Msg.alert(ils.admin.alert, ils.admin.alertText);
                return;
            }
            var index = grid.store.indexOf(s[0]);
            Ext.Ajax.request({
                url: document.location.href + '/UserProfile',
                success: function (response, opts) {
                    var a = eval('(' + response.responseText + ')');

                    var formPanel = new Ext.FormPanel({
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
                            fieldLabel: ils.admin.profileName,
                            name: 'UserName',
                            id: 'UserName',
                            value: a[0].Name
                        }, {
                            fieldLabel: ils.admin.profileFirstName,
                            xtype: 'textfield',
                            name: 'FirstName',
                            id: 'FirstName',
                            value: a[0].FirstName
                        }, {
                            fieldLabel: ils.admin.profileLastName,
                            xtype: 'textfield',
                            name: 'LastName',
                            id: 'LastName',
                            value: a[0].LastName
                        }, {
                            fieldLabel: ils.admin.profileEmail,
                            name: 'Email',
                            xtype: 'textfield',
                            id: 'Email',
                            value: a[0].Email
                        }, {
                            fieldLabel: ils.admin.profileEXP,
                            name: 'EXP',
                            value: a[0].EXP,
                            hidden: true
                        }, {
                            fieldLabel: ils.admin.profileAdmin,
                            name: 'Admin',
                            xtype: 'checkboxfield',
                            id: 'Admin',
                            checked: a[0].IsAdmin
                        }, {
                            fieldLabel: ils.admin.profileTeacher,
                            name: 'Teacher',
                            xtype: 'checkboxfield',
                            id: 'Teacher',
                            checked: a[0].IsTeacher
                        }, {
                            fieldLabel: ils.admin.profileStudent,
                            name: 'Student',
                            xtype: 'checkboxfield',
                            id: 'Student',
                            checked: a[0].IsStudent
                        }],
                        buttons: [{
                            formBind: false,
                            text: 'OK',
                            handler: function () {
                                var f = ils.admin.userProfile.items.items[0];
                                Ext.Ajax.request({
                                    url: document.location.href + '/UpdateProfile',
                                    jsonData: {
                                        'login': f.getComponent('UserName').getValue(),
                                        'email': f.getComponent('Email').getValue(),
                                        'firstName': f.getComponent('FirstName').getValue(),
                                        'lastName': f.getComponent('LastName').getValue(),
                                        'isAdmin': f.getComponent('Admin').checked,
                                        'isTeacher': f.getComponent('Teacher').checked,
                                        'isStudent': f.getComponent('Student').checked
                                    }
                                });
                                ils.admin.userProfile.close();
                                ils.admin.store.sync();
                                ils.admin.store.reload();
                            }
                        }]
                    });

                    ils.admin.userProfile = new Ext.Window({
                        title: ils.admin.textProfile,
                        modal: true,
                        layout: 'fit',
                        width: 440,
                        height: 300,
                        closable: true,
                        resizable: false,
                        draggable: false,
                        plain: true,
                        border: false,
                        items: [formPanel]
                    });
                    ils.admin.userProfile.show();
                },
                jsonData: {
                    'login': ils.admin.store.data.items[index].data.Name
                }
            });
        }
    }
});

Ext.onReady(function () {
    ils.admin.layout = new Ext.Panel({
        title: ils.admin.gridName,
        layout: 'fit',
        layoutConfig: {
            columns: 1
        },
        width: 600,
        height: 600,
        items: [ils.admin.userGrid],
        dockedItems: [
            new Ext.panel.Panel({
                bodyStyle: { "background-color": "#157fcc" },
                defaults: {
                    margin: "4 0 0 4",
                    padding: "2 8 2 8"
                },
                items: [/*{
                            iconCls: 'icon-user-add',
                            text: ils.admin.textAdd,
                            handler: function () {
                				var enteredName = 'New Friend';
                				var enteredEmail = 'new@google.com';
                				Ext.Msg.prompt(ils.admin.promptName, ils.admin.promptNameText, function(btn, text){
                					if (btn == 'ok'){
                						enteredName = text;
                						Ext.Msg.prompt(ils.admin.promptEmail, ils.admin.promptEmailText, function(btn, text){
                							if (btn == 'ok'){
                								enteredEmail = text;
                								var e = new UserModel({
                									Name: enteredName,
                									IsApproved: 'false',
                									Email: enteredEmail
                								});
                								//editor.stopEditing();
                								//store.insert(0, e);
                								ils.admin.store.add(e);
                								//grid.getView().refresh();
                								//grid.getSelectionModel().selectRow(0);
                								//editor.startEditing(0);
                							}
                						});
                					}
                				});
					
                    
                            }
                        }, */{
                            xtype: 'button',
                            ref: '../removeBtn',
                            iconCls: 'remove',
                            text: ils.admin.textRemove,
                            handler: function () {
                                //editor.stopEditing();
                                var s = ils.admin.userGrid.getSelectionModel().getSelection();
                                if (!s.length) {
                                    Ext.Msg.alert(ils.admin.alert, ils.admin.alertText);
                                    return;
                                }
                                Ext.Msg.confirm(ils.admin.promptRemove, ils.admin.promptRemoveText, function (button) {
                                    if (button == 'yes') {
                                        ils.admin.store.remove(s[0]);
                                        ils.admin.store.sync();
                                        ils.admin.store.reload();
                                    }
                                });
                            }
                        }/*, { // Кнопка "Утвердить пользователя"
                            xtype: 'button',
                            iconCls: 'add',
                            text: ils.admin.textToggle,
                            handler: function () {
                                //editor.stopEditing();
                                var grid = this.up('grid');
                                var s = grid.getSelectionModel().getSelection();
                                if (!s.length) {
                                    Ext.Msg.alert(ils.admin.alert, ils.admin.alertText);
                                    return;
                                }
                                var index = grid.store.indexOf(s[0]);
                                ils.admin.store.data.items[index].data.IsApproved = !ils.admin.store.data.items[index].data.IsApproved;
                                grid.getView().refresh();
                                //ils.admin.store.remove(s[0]);
                                //s[0].data.IsApproved = !s[0].data.IsApproved;
                                //ils.admin.store.add(s[0]);
                            }
                        }, { // Кнопка "Сохранить изменения"
                            xtype: 'button',
                            iconCls: 'paragraph_add',
                            text: ils.admin.textSaveChanges,
                            handler: function () {
                                ils.admin.store.save();
                            }
                        }*/]
            })
        ]
    });

    renderToMainArea(ils.admin.layout);

});
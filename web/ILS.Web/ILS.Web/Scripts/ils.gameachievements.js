Ext.ns('ils', 'ils.gameachievements');

Ext.require('Ext.ux.grid.*');

if (Ext.util.Cookies.get("language") == null) {
    Ext.util.Cookies.set("language", lang_pref);
}
if (Ext.util.Cookies.get("language") === "Russian") {
    isRussian = true;
} else {
    isRussian = false;
}

ils.gameachievements.textName = 'Название';
ils.gameachievements.textImagePath = 'Иконка';
ils.gameachievements.textMessage = 'Сообщение';
ils.gameachievements.textIndex = 'Номер';
ils.gameachievements.textPriority = 'Приоритет';
ils.gameachievements.textScore = 'Награда';
ils.gameachievements.textAdditionalParameters = 'Дополнительные параметры';
ils.gameachievements.textAchievementExecutor = 'Класс';
ils.gameachievements.gridName = 'Список достижений';

Ext.define('GameAchievementModel', {
    extend: 'Ext.data.Model',
    fields: [{
        name: 'Name',
        type: 'string'
    }, {
        name: 'ImagePath',
        type: 'string'
    }, {
        name: 'Message',
        type: 'string'
    }, {
        name: 'Index',
        type: 'int'
    }, {
        name: 'Priority',
        type: 'int'
    }, {
        name: 'Score',
        type: 'int'
    }, {
        name: 'AdditionalParameters',
        type: 'string'
    }, {
        name: 'AchievementExecutor',
        type: 'string'
    }]
});

ils.gameachievements.store = new Ext.data.Store({
    extend: 'Ext.data.Store',
    autoLoad: true,
    autoSync: true,
    model: GameAchievementModel,
    proxy: {
        type: 'ajax',
        url: ils.gameachievements.readAchievement,
        api: {
            read: ils.gameachievements.readAchievement,
            create: ils.gameachievements.createAchievement,
            update: ils.gameachievements.updateAchievement,
            destroy: ils.gameachievements.deleteAchievement
        },
        reader: new Ext.data.JsonReader({
            totalProperty: 'total',
            rootProperty: 'data'
        }, GameAchievementModel),
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

ils.gameachievements.achievementGrid = new Ext.grid.GridPanel({
    store: ils.gameachievements.store,
    columns: [
    {
        header: ils.gameachievements.textName,
        dataIndex: 'Name',
        width: 150,
        sortable: true,
        editor: {
            xtype: 'textfield',
            allowBlank: false
        }
    }, {
        header: ils.gameachievements.textImagePath,
        dataIndex: 'ImagePath',
        width: 150,
        sortable: true,
        editor: {
            xtype: 'textfield',
            allowBlank: false
        }
    }, {
        header: ils.gameachievements.textMessage,
        dataIndex: 'Message',
        width: 150,
        sortable: true,
        editor: {
            xtype: 'textfield',
            allowBlank: false
        }
    }, {
        header: ils.gameachievements.textIndex,
        dataIndex: 'Index',
        width: 250,
        sortable: true,
        editor: {
            xtype: 'textfield',
            allowBlank: false
        }
    }, {
        header: ils.gameachievements.textPriority,
        dataIndex: 'Priority',
        width: 250,
        sortable: true,
        editor: {
            xtype: 'textfield',
            allowBlank: false
        }
    }, {
        header: ils.gameachievements.textScore,
        dataIndex: 'Score',
        width: 250,
        sortable: true,
        editor: {
            xtype: 'textfield',
            allowBlank: false
        }
    }, {
        header: ils.gameachievements.textAdditionalParameters,
        dataIndex: 'AdditionalParameters',
        width: 250,
        sortable: true,
        editor: {
            xtype: 'textfield',
            allowBlank: false
        }
    }, {
        header: ils.gameachievements.textAchievementExecutor,
        dataIndex: 'AchievementExecutor',
        width: 250,
        sortable: true,
        editor: {
            xtype: 'textfield',
            allowBlank: false
        }
    }],
    listeners: {
        itemdblclick: function () {
//            var grid = this;
//            var s = grid.getSelectionModel().getSelection();
//            if (!s.length) {
//                Ext.Msg.alert(ils.gameachievements.alert, ils.gameachievements.alertText);
//                return;
//            }
//            var index = grid.store.indexOf(s[0]);
//            Ext.Ajax.request({
//                url: document.location.href + '/UserProfile',
//                success: function (response, opts) {
//                    var a = eval('(' + response.responseText + ')');
//
//
//                    ils.gameachievements.userProfile = new Ext.Window({
//                        title: ils.gameachievements.textProfile,
//                        layout: 'fit',
//                        width: 300,
//                        height: 270,
//                        y: 150,
//                        closable: true,
//                        resizable: false,
//                        draggable: false,
//                        plain: true,
//                        border: false,
//                        items: new Ext.Panel({
//                            defaultType: 'displayfield',
//                            fieldDefaults: {
//                                labelWidth: 200,
//                                msgTarget: 'side'
//                            },
//                            bodyStyle: { "background-color": "#DFE8F6" },
//                            items: [{
//
//                                fieldLabel: ils.gameachievements.profileName,
//                                name: 'UserName',
//                                id: 'UserName',
//                                y: 5,
//                                x: 5,
//                                value: a[0].Name
//                            }, {
//                                fieldLabel: ils.gameachievements.profileFirstName,
//                                xtype: 'textfield',
//                                name: 'FirstName',
//                                id: 'FirstName',
//                                y: 5,
//                                x: 5,
//                                value: a[0].FirstName
//                            }, {
//                                fieldLabel: ils.gameachievements.profileLastName,
//                                xtype: 'textfield',
//                                name: 'LastName',
//                                id: 'LastName',
//                                y: 5,
//                                x: 5,
//                                value: a[0].LastName
//                            }, {
//                                fieldLabel: ils.gameachievements.profileEmail,
//                                name: 'Email',
//                                xtype: 'textfield',
//                                id: 'Email',
//                                y: 5,
//                                x: 5,
//                                value: a[0].Email
//                            }, {
//                                fieldLabel: ils.gameachievements.profileEXP,
//                                name: 'EXP',
//                                y: 5,
//                                x: 5,
//                                value: a[0].EXP
//
//                            }, {
//                                fieldLabel: ils.gameachievements.profileAdmin,
//                                name: 'Admin',
//                                xtype: 'checkboxfield',
//                                id: 'Admin',
//                                y: 5,
//                                x: 5,
//                                checked: a[0].IsAdmin
//                            }, {
//                                fieldLabel: ils.gameachievements.profileTeacher,
//                                name: 'Teacher',
//                                xtype: 'checkboxfield',
//                                id: 'Teacher',
//                                y: 5,
//                                x: 5,
//                                checked: a[0].IsTeacher
//                            }, {
//                                fieldLabel: ils.gameachievements.profileStudent,
//                                name: 'Student',
//                                xtype: 'checkboxfield',
//                                id: 'Student',
//                                y: 5,
//                                x: 5,
//                                checked: a[0].IsStudent
//                            }, {
//                                fieldLabel: 'OK',
//                                name: 'update',
//                                xtype: 'button',
//                                y: 5,
//                                x: 235,
//                                width: 45,
//                                value: 'OK',
//                                text: 'OK',
//                                handler: function () {
//                                    var f = ils.gameachievements.userProfile.items.items[0];
//                                    Ext.Ajax.request({
//                                        url: document.location.href + '/UpdateProfile',
//                                        jsonData: {
//                                            'login': f.getComponent('UserName').getValue(),
//                                            'email': f.getComponent('Email').getValue(),
//                                            'firstName': f.getComponent('FirstName').getValue(),
//                                            'lastName': f.getComponent('LastName').getValue(),
//                                            'isAdmin': f.getComponent('Admin').checked,
//                                            'isTeacher': f.getComponent('Teacher').checked,
//                                            'isStudent': f.getComponent('Student').checked
//                                        }
//                                    });
//                                    ils.gameachievements.userProfile.close();
//                                }
//                            }]
//                        })
//                    });
//                    ils.gameachievements.userProfile.show();
//
//                },
//                jsonData: {
//                    'login': ils.gameachievements.store.data.items[index].data.Name
//
//                }
//            });
//
        }
    }
});

Ext.onReady(function () {
    ils.gameachievements.layout = new Ext.Panel({
        title: ils.gameachievements.gridName,
        layout: 'fit',
        layoutConfig: {
            columns: 1
        },
        width: 600,
        height: 600,
        items: [ils.gameachievements.achievementGrid],
        dockedItems: [
            new Ext.panel.Panel({
                bodyStyle: { "background-color": "#157fcc" },
                defaults: {
                    margin: "4 0 0 4",
                    padding: "2 8 2 8"
                },
                items: [{
                            xtype: 'button',
                            ref: '../removeBtn',
                            iconCls: 'remove',
                            text: ils.gameachievements.textRemove,
                            handler: function () {
                                var grid = this.up('grid');
                                var s = grid.getSelectionModel().getSelection();
                                if (!s.length) {
                                    Ext.Msg.alert(ils.gameachievements.alert, ils.gameachievements.alertText);
                                    return;
                                }
                                Ext.Msg.confirm(ils.gameachievements.promptRemove, ils.gameachievements.promptRemoveText, function (button) {
                                    if (button == 'yes') {
                                        ils.gameachievements.store.remove(s[0]);
                                    }
                                });
                            }
                        }, {
                            xtype: 'button',
                            iconCls: 'add',
                            text: ils.gameachievements.textToggle,
                            handler: function () {
                                var grid = this.up('grid');
                                var s = grid.getSelectionModel().getSelection();
                                if (!s.length) {
                                    Ext.Msg.alert(ils.gameachievements.alert, ils.gameachievements.alertText);
                                    return;
                                }
                                var index = grid.store.indexOf(s[0]);
                                ils.gameachievements.store.data.items[index].data.IsApproved = !ils.gameachievements.store.data.items[index].data.IsApproved;
                                grid.getView().refresh();
                            }
                        }, {
                            xtype: 'button',
                            iconCls: 'paragraph_add',
                            text: ils.gameachievements.textSaveChanges,
                            handler: function () {
                                ils.gameachievements.store.save();
                            }
                        }]
            })
        ]
    });

    renderToMainArea(ils.gameachievements.layout);

});
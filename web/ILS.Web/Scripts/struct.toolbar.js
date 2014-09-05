var ip;
addEntity = function (link) {
    if (link == link_addCourse) { var pth = '/treeRoot'; var pid = ""; }
    else { pth = extractPath(currently_selected); pid = currently_selected.data.id; }
    Ext.Ajax.request({
        url: link, method: 'POST',
        params: { parent_id: pid },
        success: function (result) {
            treestore.load({ callback: function () { tree.selectPath(pth + '/' + result.responseText); } });
        }
    });
}

removeEntity = function (link) {
    if (currently_selected.parentNode.childNodes.length == 1)
        var pth = extractPath(currently_selected.parentNode);
    else if (currently_selected == currently_selected.parentNode.childNodes[0])
        pth = extractPath(currently_selected.parentNode.childNodes[1]);
    else pth = extractPath(currently_selected.parentNode.childNodes[0]);
    console.log(pth);

    Ext.Ajax.request({
        url: link,
        params: { id: currently_selected.data.id, parent_id: currently_selected.parentNode.data.id },
        method: 'POST',
        success: function () {
            treestore.load({ callback: function () { tree.selectPath(pth); } });
        }
    });
}
addDoc = function (link,type_file) {
    pth = extractPath(currently_selected);
    pid = currently_selected.data.id;
    var ip = new Ext.window.Window({
        layout: 'fit',
        title: 'Загрузка документа',
//        closable: false,
        resizable: false,
       // plain: true,
        border: false,
        modal: true,  autoShow: true,
        items: [
            Ext.create('Ext.form.Panel', {
               // title: 'File Uploader',
                
                bodyPadding: 10,
                frame: true,
                items: [                
                    {
                        xtype: 'textfield',
                        hidden: true,
                        name: 'id',
                        value: pid

                    },
                    {
                        xtype: 'textfield',
                        hidden: true,
                        name: 'type_file',
                        value: type_file

                    }, 
                    {
                        xtype: 'filefield',
                        name: 'file',
                        fieldLabel: 'Файл',
                        labelWidth: 30,
                        msgTarget: 'side',
                        allowBlank: false,
                        anchor: '100%',
                        buttonText: 'Обзор...'
                    }
                ],

                buttons: [
                    {
                        text: 'Загрузить',
                        handler: function () {
                            var form = this.up('form').getForm();
                            if (form.isValid()) {
                                form.submit({
                                    url: link,
                                    waitMsg: 'Загрузка файла...',
                                    success: function (fp, o) {
                                        treestore.load({ callback: function () { tree.selectPath(pth); } });
                                        Ext.Msg.alert('Сообщение', 'Файл успешно загружен');
                                        ip.destroy();

                                    }
                                });
                            }
                        }
                    }
                ]
                
            })
            
            
        ]

        //,
       // listeners: { render: function (c) { c.getEl().on('click', function () { ip.destroy(); }); } }
            });
}

listDownloadedContentFromMoodle = function (link, listTestsAndLections_checked) {
    var alrt = Ext.Msg.show({
        title: 'Ждите, идет загрузка...',
        width: 200,
        height: 20,
        buttons: []
    });
    var pth = extractPath(currently_selected);
    
    var list = [];
    if (Ext.isObject(listTestsAndLections_checked)) {
        list.push(listTestsAndLections_checked);
    } else {
        list = listTestsAndLections_checked;
    }
    //Ajax запросом передаем в метод контроллера список выбранных лекций и тестов
    Ext.Ajax.request({
        url: link, method: 'GET',
        params: {
            id: Ext.getCmp('themeId').value,
            host: Ext.getCmp('hostId').value,
            db: Ext.getCmp('dbId').value,
            user: Ext.getCmp('userId').value,
            password: Ext.getCmp('passwordId').value,
            list: Ext.encode(list)
        },
        success: function (result) {
            treestore.load({ callback: function () { tree.selectPath(pth + '/' + result.responseText); } });
            ip.destroy();
            alrt.close();
            Ext.Msg.alert('Сообщение', 'Данные успешно загружены!');
        },
        failure: function () {
            Ext.MessageBox.alert('Сообщение', 'Не удалось подключиться к БД. Проверьте введенные данные!');
        }
    });
}

createListFromMoodle = function (listTestsAndLections) {
    var itemsD = [];
    for (var i = 0; i < listTestsAndLections.data.length; i++) {
        itemsD.push({
            boxLabel: listTestsAndLections.data[i].Name.toString(),
            name: 'topping',
            inputValue: listTestsAndLections.data[i],
            id: 'checkbox' + (i + 1).toString()
        });
    }
    var checkedValues = [];
    var ip2 = new Ext.window.Window({
        layout: 'fit',
        title: 'Список контента',
        resizable: false,
        border: false,
        modal: true,
        autoShow: true,
        items: [
                t = Ext.create('Ext.form.Panel', {
                    bodyPadding: 10,
                    width: 300,
                    title: '',
                    items: [
                    checkBoxes = new Ext.form.CheckboxGroup({
                        id: 'myGroup',
                        columns: 1,
                        items: itemsD,
                        listeners:
                            {
                                change: function (field, newValue, oldValue, eOpts) {
                                    checkedValues = newValue.topping;
                                    console.log(newValue.topping);
                                }
                            }
                    })],
                    bbar: [
                    {
                        text: 'Выбрать всё',
                        handler: function () {
                            for (var i = 0; i < itemsD.length; i++) {
                                Ext.getCmp('checkbox' + (i + 1).toString()).setValue(true);
                            }
                        }
                    },
                    '-',
                    {
                        text: 'Отменить выбор',
                        handler: function () {
                            for (var i = 0; i < itemsD.length; i++) {
                                Ext.getCmp('checkbox' + (i + 1).toString()).setValue(false);
                            }
                        }
                    },
                    {
                        text: 'Загрузить',
                        xtype: 'button',
                        handler: function () {
                            ip2.destroy();
                       
                            listDownloadedContentFromMoodle(link_listDownloadedContentFromMoodle, checkedValues);                          
                        }
                    }
                    ]
                })
        ]
    })
}

getListFromMoodle = function (link, bd_v, host_v, login_v, pass_v) {
    //var alrt = Ext.Msg.alert('', 'Ждите, идет загрузка...', Ext.emptyFn);
    var alrt = Ext.Msg.show({
        title: 'Ждите, идет загрузка...',
        width: 200,
        height: 20,
        buttons: []
    });
    Ext.Ajax.request({
        url: link, method: 'GET',
        timeout: 300000,
        params: {
            BD: bd_v,
            host: host_v,
            login: login_v,
            password: pass_v
        },
        success: function (result) {
            alrt.close();
            var jsonData = Ext.decode(result.responseText);
            console.log(jsonData);
            //            console.log(jsonData);
            //            console.log(jsonData.data[0]);
            //            console.log(jsonData.data.length);
            //Ext.Msg.alert('Сообщение', 'Вернули список лекций и тестов!');
            createListFromMoodle(jsonData);
        },
        failure: function (result) {
            console.log(result);
            alrt.close();
            Ext.MessageBox.alert('Сообщение', 'Не удалось подключиться к БД. Проверьте введенные данные!');
        }
    });
}

addMoodle = function (link) {
    var pth = extractPath(currently_selected);
    ip = new Ext.window.Window({
        layout: 'fit',
        id: 'connectionWindow',
        title: 'Авторизация Moodle',
        resizable: false,
        border: false,
        modal: true, autoShow: true,
        items: [
                Ext.create('Ext.form.Panel', {
                    id: 'connectionPanel',
                    name: 'myForm',
                    bodyPadding: 10,
                    frame: true,
                    defaultType: 'textfield',
                    fieldDefaults: {
                        labelWidth: 150,
                        msgTarget: 'side'
                    },
                    items: [
                        {
                            xtype: 'textfield',
                            id: 'themeId',
                            hidden: true,
                            name: 'id',
                            value: currently_selected.data.id

                        },
                        {
                            fieldLabel: 'Сервер',
                            id: 'hostId',
                            name: 'Host',
                            allowBlank: false,
                            value: "localhost"//"distanceitschool"
                        },
                        {
                            fieldLabel: 'База данных',
                            id: 'dbId',
                            name: 'DB',
                            allowBlank: false,
                            value: "itschool"//"distance.itschool.ssau.ru"
                        },
                        {
                            fieldLabel: 'Пользователь',
                            id: 'userId',
                            name: 'User',
                            allowBlank: false,
                            value: "root"//"distanceitschool"
                        },
                        {
                            fieldLabel: 'Пароль',
                            id: 'passwordId',
                            name: 'Password',
                            inputType: 'password',
                            allowBlank: false,
                            value: "elfxf23031990"//"zD6ZCZA"
                        }/*,
                        {
                            xtype: 'textfield',
                            hidden: false,
                            name: 'BD',
                            value: "localhost"//"distanceitschool"

                        },
                        {
                            xtype: 'textfield',
                            hidden: false,
                            name: 'host',
                            value: "itschool"//"distance.itschool.ssau.ru"

                        },
                        {
                            xtype: 'textfield',
                            hidden: false,
                            name: 'login',
                            value: "root"//"distanceitschool"

                        },
                        {
                            xtype: 'textfield',
                            hidden: false,
                            name: 'password',
                            value: "elfxf23031990"//"zD6ZCZA"

                        }*/
                    ],
                    buttons: [
                        {
                            text: 'Загрузить всё',
                            handler: function () {
                                var form = this.up('form').getForm();
                                console.log(link);
                                if (form.isValid()) {
                                    form.submit({
                                        url: link,
                                        timeout: 1000,
                                        waitMsg: 'Загрузка данных...',
                                        success: function (result) {
                                            treestore.load({ callback: function () { tree.selectPath(pth + '/' + result.responseText); } });
                                            Ext.Msg.alert('Сообщение', 'Данные успешно загружены!');
                                            ip.destroy();
                                        },
                                        failure: function () {
                                            Ext.MessageBox.alert('Сообщение', 'Не удалось подключиться к БД. Проверьте введенные данные!');
                                        }
                                    });
                                }
                            }
                        },
                        {
                            text: 'Выбрать',
                            handler: function () {
                                var bd_value = this.up('form').getForm().findField('DB').getSubmitValue();
                                var host_value = this.up('form').getForm().findField('Host').getSubmitValue();
                                var login_value = this.up('form').getForm().findField('User').getSubmitValue();
                                var pass_value = this.up('form').getForm().findField('Password').getSubmitValue();
                                getListFromMoodle(link_getListFromMoodle, bd_value, host_value, login_value, pass_value);
                            }
                        }
                    ]

                })
        ]

    });
}

changeOrderNumber = function (link) {
    var pth = extractPath(currently_selected);
    Ext.Ajax.request({
        url: link, method: 'POST',
        params: {
            id: currently_selected.data.id,
            parent_id: currently_selected.parentNode.data.id,
            depth: currently_selected.getDepth(),
            type: currently_selected.raw.iconCls
        },
        success: function (result) {
            treestore.load({ callback: function () { tree.selectPath(pth); } });
        }
    });
}

//тулбар с кнопками, которые отвечают за добавление новых сущностей (функция addEntity),
//удаление сущностей (функция removeEntity) и изменение порядка сущностей (фукция changeOrderNumber)
//функции общие, меняются только ссылки на конкретные методы
var tlbar = new Ext.toolbar.Toolbar({
    items: [{
        text: 'Добавить курс', iconCls: 'course_add',
        handler: function () { addEntity(link_addCourse); }
    }, {
        text: 'Удалить курс', iconCls: 'course_remove',
        handler: function () { removeEntity(link_removeCourse); }
    }, {
        text: 'Добавить тему', iconCls: 'theme_add', hidden: true,
        handler: function () { addEntity(link_addTheme); }
    }, {
        text: 'Удалить тему', iconCls: 'theme_remove', hidden: true,
        handler: function () { removeEntity(link_removeTheme); }
    }, {
        text: 'Добавить лекцию', iconCls: 'lecture_add', hidden: true,
        handler: function () { addEntity(link_addLecture); }
    }, {
        text: 'Удалить лекцию', iconCls: 'lecture_remove', hidden: true,
        handler: function () { removeEntity(link_removeContent); }
    }, {
        text: 'Добавить тест', iconCls: 'add', hidden: true,
        handler: function () { addEntity(link_addTest); }
    }, {
        text: 'Удалить тест', iconCls: 'remove', hidden: true,
        handler: function () { removeEntity(link_removeContent); }
    }, {
        text: 'Добавить параграф', iconCls: 'paragraph_add', hidden: true,
        handler: function () { addEntity(link_addParagraph); }
    }, {
        text: 'Удалить параграф', iconCls: 'paragraph_remove', hidden: true,
        handler: function () { removeEntity(link_removeParagraph); }
    }, {
        text: 'Добавить вопрос', iconCls: 'add', hidden: true,
        handler: function () { addEntity(link_addQuestion); }    
    }, {
        text: 'Удалить вопрос', iconCls: 'remove', hidden: true,
        handler: function () { removeEntity(link_removeQuestion); }
    }, {
        text: 'Поднять', iconCls: 'move_up', hidden: true,
        handler: function () { changeOrderNumber(link_moveUp); }
    }, {
        text: 'Опустить', iconCls: 'move_down', hidden: true,
        handler: function () { changeOrderNumber(link_moveDown); }
    }, {
        text: 'Загрузить файл с лекцией', iconCls: 'paragraph_add', hidden: true,
        handler: function () { addDoc(link_addDoc, 'lecture'); }
    }, {
        text: 'Загрузить файл с тестом', iconCls: 'paragraph_add', hidden: true,
        handler: function () { addDoc(link_addDoc,'test'); }
    }, {
        text: 'Загрузить из moodle', iconCls: 'moodle', hidden: true,
        handler: function () { addMoodle(link_addMoodle); }
    }/*, {
        text: 'Обновить список из Moodle', iconCls: 'moodleUpdate', hidden: true,
        handler: function () { updateListMoodle(link_moodleListUpdate); }
    }*/]
});
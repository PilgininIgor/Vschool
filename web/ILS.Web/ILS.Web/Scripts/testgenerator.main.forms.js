//====================================================================================================
//================================================ФУНКЦИИ=============================================
//====================================================================================================

setVisibleOfNumericFieldCountOfTasks = function () {
    // Если в комбобоксе значение "Случайные N вопросов - отображаем контролл для задания количества вопросов"
    Ext.getCmp('numericFieldCountOfTasks').setVisible(Ext.getCmp('comboboxCountOfTaskMode').getValue() == '1fa32ada-2eec-4858-834a-85a902734077');
}

setEnableOfMinutesSecondsControls = function () {
    var isChecked = Ext.getCmp('checkboxIsTimeLimitMode').getValue();
    Ext.getCmp('numericFieldTimeLimitMinutes').setReadOnly(!isChecked);
    Ext.getCmp('numericFieldTimeLimitSeconds').setReadOnly(!isChecked);
}

//====================================================================================================
//===============================================  ФОРМЫ  ============================================
//====================================================================================================

if (Ext.util.Cookies.get("language") == null) Ext.util.Cookies.set("language", lang_pref);
if (Ext.util.Cookies.get("language") == "Russian") {
    var struct_lang_LBL1 = "Основная информация"; var struct_lang_LBL2 = "Название"; var struct_lang_LBL3 = "Номер";
    var struct_lang_LBL4 = "Поле не может быть пустым"; var struct_lang_LBL5 = "Сохранить"; var struct_lang_LBL6 = "Сохранение...";
    var struct_lang_LBL7 = "Параграф"; var struct_lang_LBL8 = "Порядковый номер"; var struct_lang_LBL9 = "Заголовок параграфа";
    var struct_lang_LBL10 = "Текст параграфа"; var struct_lang_LBL11 = "Количество иллюстраций"; var struct_lang_LBL12 = "Вопрос";
    var struct_lang_LBL13 = "Текст вопроса"; var struct_lang_LBL14 = "Картинка-пояснение"; var struct_lang_LBL15 = "Путь";
    var struct_lang_LBL16 = "Выбрать..."; var struct_lang_LBL17 = "Формат ответов"; var struct_lang_LBL18 = "Текст";
    var struct_lang_LBL19 = "Количество вариантов"; var struct_lang_LBL20 = "Вариант ответа"; var struct_lang_LBL21 = "Верный";
    var struct_lang_LBL22 = "Изображение"; var struct_lang_LBL23 = "Верные"; var struct_lang_LBL24 = "";
    var struct_lang_LBL25 = ""; var struct_lang_LBL26 = ""; var struct_lang_LBL27 = "";
    var struct_lang_LBL28 = "Режим формирования теста";
    var struct_lang_LBL29 = "Количество вопросов";
    var struct_lang_LBL30 = "Описание";
    var struct_lang_LBL31 = "Порядок вопросов";
    var struct_lang_LBL32 = "Порядок вариатов ответа";
    var struct_lang_LBL33 = "Ограничение по времени";
    var struct_lang_LBL34 = "минут";
    var struct_lang_LBL35 = "секунд";
    var struct_lang_LBL36 = "Способ оценивания прохождения теста";
} else {
    struct_lang_LBL1 = "General Information"; struct_lang_LBL2 = "Name"; struct_lang_LBL3 = "Number";
    struct_lang_LBL4 = "This field is required"; struct_lang_LBL5 = "Save"; struct_lang_LBL6 = "Saving...";
    struct_lang_LBL7 = "Paragraph"; struct_lang_LBL8 = "Order number"; struct_lang_LBL9 = "Paragraph's header";
    struct_lang_LBL10 = "Paragraph's text"; struct_lang_LBL11 = "Number of pictures"; struct_lang_LBL12 = "Question";
    struct_lang_LBL13 = "Question's text"; struct_lang_LBL14 = "Illustration to the question"; struct_lang_LBL15 = "Path";
    struct_lang_LBL16 = "Select..."; struct_lang_LBL17 = "Form of variants"; struct_lang_LBL18 = "Text";
    struct_lang_LBL19 = "Number of variants"; struct_lang_LBL20 = "Answer variant"; struct_lang_LBL21 = "Correct";
    struct_lang_LBL22 = "Picture"; struct_lang_LBL23 = "Correct ones"; struct_lang_LBL24 = "";
    struct_lang_LBL25 = ""; struct_lang_LBL26 = ""; struct_lang_LBL27 = "";
    var struct_lang_LBL28 = "Test generation mode";
    var struct_lang_LBL29 = "Count of questions";
    var struct_lang_LBL30 = "Description";
    var struct_lang_LBL31 = "Order of questions";
    var struct_lang_LBL32 = "Order of answer variants";
    var struct_lang_LBL33 = "Time limit";
    var struct_lang_LBL34 = "minutes";
    var struct_lang_LBL35 = "seconds";
    var struct_lang_LBL36 = "The scale evaluation of passing the test";
}

var form_cttc = new Ext.form.Panel({
    hidden: true, bodyPadding: 10, waitMsgTarget: true,
    items: [{
        xtype: 'fieldset', title: struct_lang_LBL1, layout: 'anchor',
        items: [{
            xtype: 'textfield', name: 'id', anchor: '100%', hidden: true
        }, {
            xtype: 'textfield', name: 'type', anchor: '100%', hidden: true
        }, {
            xtype: 'textfield', name: 'ordernumber', anchor: '100%',
            fieldLabel: struct_lang_LBL3, labelAlign: 'right', labelWidth: 60,
            readOnly: true, disabled: true
        }, {
            xtype: 'textfield', name: 'name', anchor: '100%',
            fieldLabel: struct_lang_LBL2, labelAlign: 'right', labelWidth: 60,
            msgTarget: 'side', allowBlank: false, blankText: struct_lang_LBL4
        }]
    }],
    buttons: [{
        text: struct_lang_LBL5, name: 'saver',
        formBind: true,
        handler: function () {
            this.up('form').submit({ //переслать данные из формы на сервер
                url: link_saveCTTC,
                waitMsg: struct_lang_LBL6,
                success: function () { //получив ответ от сервера, обновить дерево
                    var s = ''; s = extractPath(currently_selected);
                    treestore.load(
                        {
                            callback: function () { tree.selectPath(s); }
                        });
                }
            });
        }
    }]
});

Ext.define('TGCountOfTaskModeModel', {
    extend: 'Ext.data.Model',
    fields: [
        { name: 'id', type: 'string' },
        { name: 'name', type: 'string' }
    ]
});

Ext.define('TGMixModeModel', {
    extend: 'Ext.data.Model',
    fields: [
        { name: 'id', type: 'string' },
        { name: 'name', type: 'string' }
    ]
});

Ext.define('TGRatingCalculationModeModel', {
    extend: 'Ext.data.Model',
    fields: [
        { name: 'id', type: 'string' },
        { name: 'name', type: 'string' },
        { name: 'description', type: 'string' }
    ]
});

var tgCountOfTaskModeStore = Ext.create('Ext.data.Store', {
    model: 'TGCountOfTaskModeModel',
    proxy: {
        type: 'ajax',
        url: link_readTGCountOfTaskModes,
        reader: {
            type: 'json',
            rootProperty: 'data'
        }
    },
    autoLoad: true
});

var tgTasksMixModeStore = Ext.create('Ext.data.Store', {
    model: 'TGMixModeModel',
    proxy: {
        type: 'ajax',
        url: link_readTGMixModes,
        reader: {
            type: 'json',
            rootProperty: 'data'
        }
    },
    autoLoad: true
});

var tgAnswersMixModeStore = Ext.create('Ext.data.Store', {
    model: 'TGMixModeModel',
    proxy: {
        type: 'ajax',
        url: link_readTGMixModes,
        reader: {
            type: 'json',
            rootProperty: 'data'
        }
    },
    autoLoad: true
});

var tgRatingCalculationModeStore = Ext.create('Ext.data.Store', {
    model: 'TGRatingCalculationModeModel',
    proxy: {
        type: 'ajax',
        url: link_readTGRatingCalculationModes,
        reader: {
            type: 'json',
            rootProperty: 'data'
        }
    },
    autoLoad: true
});

var form_tgtest = new Ext.form.Panel({
    hidden: true, bodyPadding: 10, waitMsgTarget: true,
    items: [{
        xtype: 'fieldset', title: struct_lang_LBL1,
        layout: {
            type: 'vbox',
            align: 'stretch',
            pack: 'start'
        },
        fieldDefaults: {
            labelAlign: 'top',
            anchor: '100%',
            width: '100%',
            msgTarget: 'side',
            padding: '0 10px 0 0'
        },
        items: [{
            xtype: 'textfield', name: 'id', hidden: true
        }, {
            xtype: 'textfield', name: 'name',
            fieldLabel: struct_lang_LBL2,
            allowBlank: false, blankText: struct_lang_LBL4
        }, {
            xtype: 'textareafield',
            name: 'description',
            fieldLabel: struct_lang_LBL30
        }, {
            layout: {
                type: 'hbox',
                align: 'stretch',
                pack: 'start',
            },
            bodyStyle: { "background-color": "transparent" },
            border: 0,
            defaults: {
                border: 0,
                bodyStyle: { "background-color": "transparent" }
            },
            items: [
            {
                id: 'comboboxCountOfTaskMode',
                xtype: 'combobox',
                name: 'countoftaskmode',
                fieldLabel: struct_lang_LBL28,
                store: tgCountOfTaskModeStore,
                displayField: 'name',
                valueField: 'id',
                allowBlank: false,
                width: "33%",
                tpl: Ext.create('Ext.XTemplate',
                    '<tpl for=".">',
                        '<div class="x-boundlist-item" style="font-family:wf_SegoeUILight;font-weight:normal;font-size:14px;padding-top:4px;padding-bottom:4px;">{name}</div>',
                    '</tpl>'
                ),
                listeners: {
                    'select': function () {
                        setVisibleOfNumericFieldCountOfTasks();
                    }
                }
            }, {
                id: 'numericFieldCountOfTasks',
                xtype: 'numberfield',
                fieldLabel: struct_lang_LBL29,
                name: 'countoftasks',
                width: "33%",
                minValue: 0,
                maxValue: 999
            }]
        }, {
            layout: {
                type: 'hbox',
                align: 'stretch',
                pack: 'start',
            },
            bodyStyle: { "background-color": "transparent" },
            border: 0,
            defaults: {
                border: 0,
                bodyStyle: { "background-color": "transparent" }
            },
            items: [
            {
                id: 'comboboxTasksMixMode',
                xtype: 'combobox',
                name: 'tasksmixmode',
                fieldLabel: struct_lang_LBL31,
                store: tgTasksMixModeStore,
                displayField: 'name',
                valueField: 'id',
                allowBlank: false,
                width: "33%",
                tpl: Ext.create('Ext.XTemplate',
                    '<tpl for=".">',
                        '<div class="x-boundlist-item" style="font-family:wf_SegoeUILight;font-weight:normal;font-size:14px;padding-top:4px;padding-bottom:4px;">{name}</div>',
                    '</tpl>'
                )
            }, {
                id: 'comboboxAnswersMixMode',
                xtype: 'combobox',
                name: 'answersmixmode',
                fieldLabel: struct_lang_LBL32,
                store: tgAnswersMixModeStore,
                displayField: 'name',
                valueField: 'id',
                allowBlank: false,
                width: "33%",
                tpl: Ext.create('Ext.XTemplate',
                    '<tpl for=".">',
                        '<div class="x-boundlist-item" style="font-family:wf_SegoeUILight;font-weight:normal;font-size:14px;padding-top:4px;padding-bottom:4px;">{name}</div>',
                    '</tpl>'
                )
            }]
        }, {
            layout: {
                type: 'hbox',
                align: 'stretch',
                pack: 'start',
            },
            bodyStyle: { "background-color": "transparent" },
            border: 0,
            defaults: {
                border: 0,
                bodyStyle: { "background-color": "transparent" }
            },
            items: [
            {
                id: 'comboboxRatingCalculationMode',
                xtype: 'combobox',
                name: 'ratingcalculationmode',
                fieldLabel: struct_lang_LBL36,
                store: tgRatingCalculationModeStore,
                displayField: 'name',
                valueField: 'id',
                allowBlank: false,
                width: "66%",
                tpl: Ext.create('Ext.XTemplate',
                    '<tpl for=".">',
                        '<div class="x-boundlist-item" style="font-family:wf_SegoeUILight;font-weight:normal;font-size:14px;padding-top:4px;padding-bottom:4px;">{name}<br><i>{description}</i></div>',
                    '</tpl>'
                )
            }]
        }, {
            layout: {
                type: 'hbox',
                align: 'bottom',
                pack: 'start'
            },
            bodyStyle: { "background-color": "transparent" },
            border: 0,
            margin: '8px 0 0 0',
            defaults: {
                border: 0,
                bodyStyle: { "background-color": "transparent" }
            },
            items: [
            {
                id: 'checkboxIsTimeLimitMode',
                xtype: 'checkboxfield',
                name: 'istimelimitmode',
                boxLabel: struct_lang_LBL33,
                width: "33%",
                margin: '5px 0 0 0',
                listeners: {
                    change: function (newValue, oldValue, eOpts) {
                        console.log();
                        setEnableOfMinutesSecondsControls();
                    }
                }
            }, {
                id: 'numericFieldTimeLimitMinutes',
                xtype: 'numberfield',
                name: 'timelimitminutes',
                fieldLabel: struct_lang_LBL34,
                labelAlign: 'left',
                labelWidth: '50px',
                align: 'bottom',
                width: "16.5%",
                minValue: 0,
                maxValue: 99
            }, {
                id: 'numericFieldTimeLimitSeconds',
                xtype: 'numberfield',
                name: 'timelimitseconds',
                fieldLabel: struct_lang_LBL35,
                labelAlign: 'left',
                labelWidth: '50px',
                width: "16.5%",
                minValue: 0,
                maxValue: 59
            }]
        }, {
            layout: 'column',
            colspan: 3,
            bodyStyle: { "background-color": "transparent" },
            border: 0,
            defaults: {
                border: 0,
                bodyStyle: { "background-color": "transparent" }
            },
            items: [
                /*{
                    cls: 'customColorPanel',
                    columnWidth: .33,
                    items: [
                    {
                        xtype: 'textfield', name: 'name',
                        fieldLabel: struct_lang_LBL2,
                        allowBlank: false, blankText: struct_lang_LBL4
                    }]
                },
                {
                    cls: 'customColorPanel',
                    columnWidth: .33,
                    items: [
                    {
                        xtype: 'textfield', name: 'name',
                        fieldLabel: struct_lang_LBL2 + '2',
                        allowBlank: false, blankText: struct_lang_LBL4
                    }]
                }*/
            ]
        }
        ]
    }],
    buttons: [{
        text: struct_lang_LBL5, name: 'saver',
        formBind: true,
        handler: function () {
            this.up('form').submit({ //переслать данные из формы на сервер
                url: link_saveTGTest,
                waitMsg: struct_lang_LBL6,
                success: function () { //получив ответ от сервера, обновить дерево
                    var s = ''; s = extractPath(currently_selected);
                    treestore.load(
                        {
                            callback: function () {
                                tree.selectPath(s);
                                if (!Ext.getCmp('checkboxIsTimeLimitMode').getValue()) {
                                    Ext.getCmp('numericFieldTimeLimitMinutes').setValue(0);
                                    Ext.getCmp('numericFieldTimeLimitSeconds').setValue(0);
                                }
                            }
                        });
                }
            });
        }
    }],
    listeners: {
        'actioncomplete': function (form, action) {
            setVisibleOfNumericFieldCountOfTasks();
            setEnableOfMinutesSecondsControls();
        }
    }
});

var form_tgtasktemplate = new Ext.form.Panel({
    hidden: true, bodyPadding: 10, waitMsgTarget: true,
    items: [{
        xtype: 'fieldset', title: struct_lang_LBL1,
        layout: {
            type: 'vbox',
            align: 'left',
            pack: 'start'
        },
        fieldDefaults: {
            labelAlign: 'top',
            anchor: '100%',
            width: '100%',
            msgTarget: 'side',
            padding: '0 10px 0 0'
        },
        items: [{
            xtype: 'textfield', name: 'id', hidden: true
        }, {
            xtype: 'textfield', name: 'name',
            fieldLabel: struct_lang_LBL2,
            allowBlank: false, blankText: struct_lang_LBL4
        }, {
            xtype: 'textareafield',
            name: 'description',
            fieldLabel: struct_lang_LBL30
        }]
    }],
    buttons: [{
        xtype: 'button',
        text: 'Открыть в редакторе',
        name: 'ololo',
        iconCls: 'loading_gear',
        width: 200,
        height: 30,
        handler: function() {
            window.location.href = link_taskEditor;
        }
    }, {
        text: struct_lang_LBL5, name: 'saver',
        formBind: true,
        handler: function () {
            this.up('form').submit({
                url: link_saveTGTaskTemplate,
                waitMsg: struct_lang_LBL6,
                success: function () {
                    var s = ''; s = extractPath(currently_selected);
                    treestore.load({
                        callback: function () {
                            tree.selectPath(s);
                        }
                    });
                }
            });
        }
    }]
});
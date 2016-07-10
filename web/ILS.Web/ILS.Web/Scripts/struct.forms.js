﻿//====================================================================================================
//================================================ФУНКЦИИ=============================================
//====================================================================================================

paragraph_piccount_change = function (newValue) {
    for (var i = 1; i <= newValue; i++) form_paragraph.down('[name=pic' + i + ']').show();
    for (i = newValue + 1; i <= 20; i++)
        if (i == 1) form_paragraph.down('[name=pic1]').hide();
        else form_paragraph.down('[name=pic' + i + ']').hide();    
}

paragraph_pic_listener = function ()
{
    form_paragraph.down('[id=image_temp]').getEl().on('load', function ()
    {
        if (form_paragraph.hidden)
            return;

        var w = this.dom.width;
        var h = this.dom.height;

        var ip = new Ext.window.Window({
            closable: false,
            draggable: false,
            modal: true,
            resizable: false,
            autoShow: true,
            layout: 'fit',
            items: {
                xtype: 'image',
                src: form_paragraph.down('[id=image_temp]').src,
                width: w,
                height: h
            },
            listeners: {
                render: function (c)
                {
                    c.getEl().on('click',
                        function ()
                        {
                            ip.destroy();
                        });
                }
            }
        });
    });
}

paragraph_pic_popup = function (source) { 
    form_paragraph.down('[id=image_temp]').setSrc(source);
}


question_anscount1_change = function (val) {
    if (form_question.down('[name=rb]').getValue()) switch (val) {
        case 2:
            form_question.down('[name=q3]').setDisabled(true); form_question.down('[name=q4]').setDisabled(true);
            form_question.down('[name=q5]').setDisabled(true); break;
        case 3:
            form_question.down('[name=q3]').setDisabled(false); form_question.down('[name=q4]').setDisabled(true);
            form_question.down('[name=q5]').setDisabled(true); break;
        case 4:
            form_question.down('[name=q3]').setDisabled(false); form_question.down('[name=q4]').setDisabled(false);
            form_question.down('[name=q5]').setDisabled(true); break;
        case 5:
            form_question.down('[name=q3]').setDisabled(false); form_question.down('[name=q4]').setDisabled(false);
            form_question.down('[name=q5]').setDisabled(false); break;
    }
}

question_anscount2_change = function (val) {
    if (!form_question.down('[name=rb]').getValue()) switch (val) {
        case 2:
            form_question.down('[name=avp3]').setDisabled(true); form_question.down('[name=avp4]').setDisabled(true);
            form_question.down('[name=avp5]').setDisabled(true); break;
        case 3:
            form_question.down('[name=avp3]').setDisabled(false); form_question.down('[name=avp4]').setDisabled(true);
            form_question.down('[name=avp5]').setDisabled(true); break;
        case 4:
            form_question.down('[name=avp3]').setDisabled(false); form_question.down('[name=avp4]').setDisabled(false);
            form_question.down('[name=avp5]').setDisabled(true); break;
        case 5:
            form_question.down('[name=avp3]').setDisabled(false); form_question.down('[name=avp4]').setDisabled(false);
            form_question.down('[name=avp5]').setDisabled(false); break;
    }
}

question_radio_change = function (val) {
    form_question.down('[name=anscount1]').setDisabled(!val); form_question.down('[name=anscount2]').setDisabled(val);
    form_question.down('[name=q1-5]').setDisabled(val);
    form_question.down('[name=q1]').setDisabled(!val); form_question.down('[name=q2]').setDisabled(!val);
    form_question.down('[name=q3]').setDisabled(!val); form_question.down('[name=q4]').setDisabled(!val);
    form_question.down('[name=q5]').setDisabled(!val);
    form_question.down('[name=pica_preview]').setDisabled(val);
    form_question.down('[name=pica_file]').setDisabled(val);
    if (val) question_anscount1_change(form_question.down('[name=anscount1]').value);
    else question_anscount2_change(form_question.down('[name=anscount2]').value);
}

task1_radio_change = function (val) {
    form_task1.down('[name=scale]').setDisabled(!val);
    form_task1.down('[name=operation]').setDisabled(!val);
    form_task1.down('[name=number1]').setDisabled(!val);
    form_task1.down('[name=number2]').setDisabled(!val);
    form_task1.down('[name=scale1]').setDisabled(val);
    form_task1.down('[name=scale2]').setDisabled(val);
    form_task1.down('[name=number]').setDisabled(val);
}

question_pic_popup = function (c, nm) {
    c.getEl().on('click', function () {
        if (form_question.down('[name=' + nm + ']').getValue() != "") {
            var ip = new Ext.window.Window({
                layout: 'fit', closable: false, draggable: false, modal: true, resizable: false, autoShow: true,
                items: { xtype: 'image', src: c.src },
                listeners: { render: function (c) { c.getEl().on('click', function () { ip.destroy(); }); } }
            });
        }
    });
}

function validateScalesNotEqual(value)
{
    var task_type = Ext.ComponentQuery.query('[name=rb_task]')[0].getGroupValue();
    var scale1 = form_task1.down('[name=scale1]').getValue();
    var scale2 = form_task1.down('[name=scale2]').getValue();
    if (scale1 == scale2 && task_type == "translation")
    {
        form_task1.getForm().markInvalid({ 'scale1': struct_lang_LBL47 });
        form_task1.getForm().markInvalid({ 'scale2': struct_lang_LBL47 });
        return false;
    }
    form_task1.down('[name=scale1]').clearInvalid();
    form_task1.down('[name=scale2]').clearInvalid();
    return true;
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
    var struct_lang_LBL25 = ""; var struct_lang_LBL26 = ""; var struct_lang_LBL27 = ""; var struct_lang_LBL28 = "Задание на системы счисления";
    var struct_lang_LBL29 = "Операция"; var struct_lang_LBL30 = "Тип задания"; var struct_lang_LBL31 = "Система счисления";
    var struct_lang_LBL32 = "Число 1 (в 10 СС)"; var struct_lang_LBL33 = "Число 2 (в 10 СС)"; var struct_lang_LBL34 = "Перевод";
    var struct_lang_LBL35 = "Система счисления 1"; var struct_lang_LBL36 = "Система счисления 2"; var struct_lang_LBL37 = "Число (в 10 СС)";
    var struct_lang_LBL38 = "Задание на Ханойскую башню"; var struct_lang_LBL39 = "Количество колец"; var struct_lang_LBL40 = "Задание на алгебру логики";
    var struct_lang_LBL41 = "Линейная запись формулы"; var struct_lang_LBL42 = "Линейная скобочная запись используется для представления дерева логической формулы в виде строки. Для обозначения логических операций и значений введены следующие обозначения: 1 - истина, 0 - ложь, b - пропущенное значение, c - конъюнкция, d - дизъюнкция, e - эквивалентность, i - импликация, o - пропущенная операция.";
    var struct_lang_LBL43 = "Предел оценки 5"; var struct_lang_LBL44 = "Предел оценки 4"; var struct_lang_LBL45 = "Предел оценки 4 отсчитывается от предела оценки 5.";
    var struct_lang_LBL46 = "Линейная скобочная запись используется для представления дерева логической формулы в виде строки. Для обозначения логических операций и значений введены следующие обозначения: 1 - истина, 0 - ложь, b - пропущенное значение, c - конъюнкция, d - дизъюнкция, e - эквивалентность, i - импликация, o - пропущенная операция (символы букв на английском языке). Вы можете составить логическую формулу, используя эти обозначения и скобки для расстановки приоритетов. Знак операции ставится перед скобками, внутри которых указываются операнды. Максимальный уровень вложенности операндов равен 3, на последнем уровне операндами обязательно должны являться логические значения или их пропуск. В формуле можно допустить до 4ех пропусков. Удостоверьтесь, что путем подстановки учащийся сможет получить истинное значение формулы. Примеры правильно построенных формул:";
    var struct_lang_LBL47 = "Системы счисления не должны совпадать.";
    var struct_lang_LBLMinutes = "Количество минут";
    var struct_lang_LBLDifficulty = "Сложность";
    var struct_lang_LBLTest = "Тест";
} else {
    struct_lang_LBL1 = "General Information"; struct_lang_LBL2 = "Name"; struct_lang_LBL3 = "Number";
    struct_lang_LBL4 = "This field is required"; struct_lang_LBL5 = "Save"; struct_lang_LBL6 = "Saving...";
    struct_lang_LBL7 = "Paragraph"; struct_lang_LBL8 = "Order number"; struct_lang_LBL9 = "Paragraph's header";
    struct_lang_LBL10 = "Paragraph's text"; struct_lang_LBL11 = "Number of pictures"; struct_lang_LBL12 = "Question";
    struct_lang_LBL13 = "Question's text"; struct_lang_LBL14 = "Illustration to the question"; struct_lang_LBL15 = "Path";
    struct_lang_LBL16 = "Select..."; struct_lang_LBL17 = "Form of variants"; struct_lang_LBL18 = "Text";
    struct_lang_LBL19 = "Number of variants"; struct_lang_LBL20 = "Answer variant"; struct_lang_LBL21 = "Correct";
    struct_lang_LBL22 = "Picture"; struct_lang_LBL23 = "Correct ones"; struct_lang_LBL24 = ""; 
    struct_lang_LBL25 = ""; struct_lang_LBL26 = ""; struct_lang_LBL27 = ""; struct_lang_LBL28 = "Numeric systems task";
    struct_lang_LBL29 = "Operation"; struct_lang_LBL30 = "Task type"; struct_lang_LBL31 = "Numeric system";
    struct_lang_LBL32 = "Number 1 (decimal)"; struct_lang_LBL33 = "Number 2 (decimal)"; struct_lang_LBL34 = "Translation";
    struct_lang_LBL35 = "Numeric system 1"; struct_lang_LBL36 = "Numeric system 2"; struct_lang_LBL37 = "Number (decimal)";
    struct_lang_LBL38 = "Tower of Hanoi task"; struct_lang_LBL39 = "Amount of rings"; struct_lang_LBL40 = "Logic task";
    struct_lang_LBL41 = "Linear notation of a formula"; struct_lang_LBL42 = "Linear bracket notation is used to represent the tree of a logical formula in a line. The following notation is used to indicate logical operations and values: 1 - true, 0 - false, b - blank value, c - conjunction, d - disjunction, e - equivalence, i - implication, o - blank operation.";
    struct_lang_LBL43 = "Limit of mark 5"; struct_lang_LBL44 = "Limit of mark 4"; struct_lang_LBL45 = "Limit of mark 4 is counted from limit of mark 5.";
    struct_lang_LBL46 = "Linear bracket notation is used to represent the tree of a logical formula in a line. The following notation is used to indicate logical operations and values: 1 - true, 0 - false, b - blank value, c - conjunction, d - disjunction, e - equivalence, i - implication, o - blank operation. You can compose a logical formula using these symbols and brackets. Operation symbol is placed before brackets, inside which operand symbols are placed. Maximum nesting level of operands is 3, on the last level operands have to be logical values or blank. You can put up to 4 blank values or operations into formula. Make sure a student can get true value as a result of substituting. Examples of formulas:";
    struct_lang_LBL47 = "Numeric systems should not be the same.";
    struct_lang_LBLMinutes = "Minutes";
    struct_lang_LBLDifficulty = "Difficulty";
    struct_lang_LBLTest = "Test";
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
                    treestore.load({ callback: function () { tree.selectPath(s); } });
                }
            });
        }
    }]
});

var form_test = new Ext.form.Panel({
    title: struct_lang_LBLTest,
    hidden: true, bodyPadding: 10, waitMsgTarget: true,
    items: [{
        xtype: 'fieldset', title: struct_lang_LBL1, layout: 'anchor',
        items: [{
            xtype: 'textfield', name: 'id', anchor: '100%', hidden: true
        }, {
            xtype: 'textfield', name: 'type', anchor: '100%', hidden: true
        }, {
            xtype: 'textfield', name: 'ordernumber', anchor: '100%',
            fieldLabel: struct_lang_LBL3, labelAlign: 'right', labelWidth: 90,
            readOnly: true, disabled: true
        }, {
            xtype: 'textfield', name: 'name', anchor: '100%',
            fieldLabel: struct_lang_LBL2, labelAlign: 'right', labelWidth: 90,
            msgTarget: 'side', allowBlank: false, blankText: struct_lang_LBL4
        }, {
            xtype: 'numberfield', name: 'difficulty', anchor: '100%',
            fieldLabel: struct_lang_LBLDifficulty, labelAlign: 'right', labelWidth: 90,
            msgTarget: 'side', allowBlank: false, blankText: struct_lang_LBL4
        }, {
            xtype: 'numberfield', name: 'minutes', anchor: '100%',
            fieldLabel: struct_lang_LBLMinutes, labelAlign: 'right', labelWidth: 90,
            msgTarget: 'side', allowBlank: false, blankText: struct_lang_LBL4
        }]
    }],
    buttons: [{
        text: struct_lang_LBL5, name: 'saver',
        formBind: true,
        handler: function () {
            this.up('form').submit({ //переслать данные из формы на сервер
                url: link_saveTest,
                waitMsg: struct_lang_LBL6,
                success: function () { //получив ответ от сервера, обновить дерево
                    var s = extractPath(currently_selected);
                    treestore.load({ callback: function () { tree.selectPath(s); } });
                }
            });
        }
    }]
});

var form_paragraph = new Ext.form.Panel({
    hidden: true, title: struct_lang_LBL7, bodyPadding: 10, waitMsgTarget: true, layout: 'anchor',
    items: [{
        name: 'id', xtype: 'textfield', anchor: '100%',
        fieldLabel: 'Guid', labelAlign: 'right', labelWidth: 160,
        hidden: true
    }, {
        name: 'ordernumber', xtype: 'textfield', anchor: '100%',
        fieldLabel: struct_lang_LBL8, labelAlign: 'right', labelWidth: 160,
        hidden: true
    }, {
        id: 'image_temp', xtype: 'image', src: '', hidden: true,
        listeners: { render: function () { paragraph_pic_listener(); } }
    }, {
        name: 'header', xtype: 'textfield', anchor: '100%',
        fieldLabel: struct_lang_LBL9, labelAlign: 'right', labelWidth: 160,
        msgTarget: 'side', allowBlank: false, blankText: struct_lang_LBL4
    }, {
        name: 'text', xtype: 'textarea', anchor: '100%',
        fieldLabel: struct_lang_LBL10, labelAlign: 'right', labelWidth: 160,
        grow: true
    }, {
        name: 'piccount', xtype: 'numberfield', anchor: '100%',
        fieldLabel: struct_lang_LBL11, labelAlign: 'right', labelWidth: 160,
        minValue: 0, maxValue: 20, value: 0, editable: false,
        listeners: { change: { fn: function (t, newValue, oldValue) { paragraph_piccount_change(newValue); } } }
    }],
    buttons: [{
        text: struct_lang_LBL5, name: 'saver',
        formBind: true,
        handler: function () {
            this.up('form').submit({
                url: link_saveParagraph,
                waitMsg: struct_lang_LBL6,
                success: function () {
                    var s = ''; s = extractPath(currently_selected);
                    treestore.load({ callback: function () { tree.selectPath(s); } });
                }
            });
        }
    }]
});

form_paragraph_addPic = function (i) {
    form_paragraph.add({
        hidden: true,
        name: 'pic' + i, id: 'pic' + i,
        xtype: 'fieldcontainer', layout: 'hbox',
        fieldLabel: isRussian ? ('Изображение №' + i) : ('Picture №' + i),
        labelAlign: 'right', labelWidth: 160,
        items: [{
            name: 'pic' + i + '_path', id: 'pic' + i + '_path',
            xtype: 'textfield', hidden: true
        }, {
            name: 'pic' + i + '_show', id: 'pic' + i + '_show',
            xtype: 'button', hidden: true,
            text: isRussian ? 'Показать' : 'Show',
            flex: 1, margin: '0 2 0 0',
            handler: function () {
                var source = form_paragraph.down('[name=' + this.name.replace('_show', '') + '_path]').getValue();
                paragraph_pic_popup(source);
            }
        }, {
            name: 'pic' + i + '_file', id: 'pic' + i + '_file',
            xtype: 'filefield', readOnly: true, msgTarget: 'side',
            buttonText: isRussian ? 'Выбрать...' : 'Find...',
            flex: 5//,
            /*validator: function (value) {          
                if (this.up().hidden) return true;
                if ((value != "") || (this.up().down('textfield').getValue() != "")) return true;
                return "Файл не выбран";
            }*/
        }]
    });
}

var labelWidthOfTextfield = 175;

var form_task3 = new Ext.form.Panel({
    hidden: true, title: struct_lang_LBL38, waitMsgTarget: true,
    items: [{ //служебная часть
        bodyPadding: 10, layout: 'anchor', hidden: true,
        items: [{
            name: 'Id', xtype: 'textfield', anchor: '100%', hidden: true,
            fieldLabel: 'Guid', labelAlign: 'right', labelWidth: 170
        }, {
            name: 'ordernumber', xtype: 'textfield', anchor: '100%',
            fieldLabel: 'Порядковый номер', labelAlign: 'right', labelWidth: 170
        }]
    }, {
        border: false,
        layout: { type: 'hbox', align: 'stretchmax' },
        items: [{
            flex: 1, bodyPadding: 10, layout: 'anchor', height: 75,
            items: [{
                name: 'numberOfCylinders', xtype: 'numberfield', anchor: '100%', editable: false,
                minValue: 2, maxValue: 6,
                fieldLabel: struct_lang_LBL39, labelAlign: 'right', labelWidth: labelWidthOfTextfield
            }]
        }]
    }],
    buttons: [{
        text: struct_lang_LBL5, name: 'saver',
        formBind: true,
        handler: function () {
            this.up('form').submit({
                url: link_saveTask3,
                waitMsg: struct_lang_LBL6,
                success: function () {
                    var s = extractPath(currently_selected);
                    treestore.load({ callback: function () { tree.selectPath(s); } });
                }
            });
        }
    }]
});

var form_task3 = new Ext.form.Panel({
    hidden: true, title: struct_lang_LBL38, waitMsgTarget: true,
    items: [{ //служебная часть
        bodyPadding: 10, layout: 'anchor', hidden: true,
        items: [{
            name: 'Id', xtype: 'textfield', anchor: '100%', hidden: true,
            fieldLabel: 'Guid', labelAlign: 'right', labelWidth: 170
        }, {
            name: 'ordernumber', xtype: 'textfield', anchor: '100%',
            fieldLabel: 'Порядковый номер', labelAlign: 'right', labelWidth: 170
        }]
    }, {
        border: false,
        layout: { type: 'hbox', align: 'stretchmax' },
        items: [{
            flex: 1, bodyPadding: 10, layout: 'anchor', height: 150,
            items: [{
                name: 'numberOfCylinders', xtype: 'numberfield', anchor: '100%', editable: false,
                minValue: 2, maxValue: 6,
                fieldLabel: struct_lang_LBL39, labelAlign: 'left', labelWidth: labelWidthOfTextfield
            }, {
                name: 'limitOf5', xtype: 'numberfield', anchor: '100%', editable: false,
                minValue: 0, maxValue: 10,
                fieldLabel: struct_lang_LBL43, labelAlign: 'left', labelWidth: labelWidthOfTextfield
            }, {
                name: 'limitOf4', xtype: 'numberfield', anchor: '100%', editable: false,
                minValue: 1, maxValue: 10,
                fieldLabel: struct_lang_LBL44, labelAlign: 'left', labelWidth: labelWidthOfTextfield
            }, {
                name: 'hint', xtype: 'label', anchor: '100%',
                html: struct_lang_LBL45, cls: 'customlabel',
            }]
        }]
    }],
    buttons: [{
        text: struct_lang_LBL5, name: 'saver',
        formBind: true,
        handler: function () {
            this.up('form').submit({
                url: link_saveTask3,
                waitMsg: struct_lang_LBL6,
                success: function () {
                    var s = extractPath(currently_selected);
                    treestore.load({ callback: function () { tree.selectPath(s); } });
                }
            });
        }
    }]
});

var form_task2 = new Ext.form.Panel({
    hidden: true, title: struct_lang_LBL40, waitMsgTarget: true,
    items: [{ //служебная часть
        bodyPadding: 10, layout: 'anchor', hidden: true,
        items: [{
            name: 'Id', xtype: 'textfield', anchor: '100%', hidden: true,
            fieldLabel: 'Guid', labelAlign: 'right', labelWidth: 170
        }, {
            name: 'ordernumber', xtype: 'textfield', anchor: '100%',
            fieldLabel: 'Порядковый номер', labelAlign: 'right', labelWidth: 170
        }]
    }, {
        border: false,
        layout: { type: 'hbox', align: 'stretchmax' },
        //items: [{
        //    flex: 1, bodyPadding: 10, layout: 'anchor', height: 100,
        //    items: [{
        //        name: 'taskStr', xtype: 'combo', anchor: '100%', editable: false,
        //        store: new Ext.data.SimpleStore({
        //            data: [
        //                ['e(d(i(b,b),e(b,1)),i(1,0))'],
        //                ['c(c(d(0,b),o(0,0)),c(o(1,0),e(b,0)))'],
        //                ['e(c(i(1,b),d(0,b)),i(o(1,0),o(0,0)))'],
        //                ['c(e(b,b),c(o(1,0),b))'],
        //                ['e(d(c(1,b),b),e(o(1,0),b))'],
        //            ],
        //            fields: ['formula']
        //        }),
        //        valueField: 'formula',
        //        displayField: 'formula',
        //        fieldLabel: struct_lang_LBL41, labelAlign: 'left', labelWidth: labelWidthOfTextfield
        //    }, {
        //        name: 'hint', xtype: 'label', anchor: '100%',
        //        html: struct_lang_LBL42, cls: 'customlabel',
        //    }]
        //}]
        items: [{
            flex: 1, bodyPadding: 10, layout: 'anchor',                                              //тут нет проверки правильности ввода формулы
            items: [{
                name: 'taskStr', xtype: 'textfield', anchor: '100%', editable: true,
                fieldLabel: struct_lang_LBL41, labelAlign: 'left', labelWidth: labelWidthOfTextfield
            }, {
                name: 'hint', xtype: 'label', anchor: '100%',
                html: struct_lang_LBL46 + '<br>', cls: 'customlabel',
            }, {
                name: 'formula1', xtype: 'label', anchor: '100%',
                html: 'e(d(i(b, b), e(b, 1)), i(1, 0))<br>', cls: 'customlabel',
            }, {
                name: 'formula2', xtype: 'label', anchor: '100%',
                html: 'c(c(d(0,b),o(0,0)),c(o(1,0),e(b,0)))<br>', cls: 'customlabel',
            }, {
                name: 'formula3', xtype: 'label', anchor: '100%',
                html: 'e(c(i(1,b),d(0,b)),i(o(1,0),o(0,0)))<br>', cls: 'customlabel',
            }, {
                name: 'formula4', xtype: 'label', anchor: '100%',
                html: 'c(e(b,b),c(o(1,0),b))<br>', cls: 'customlabel',
            }, {
                name: 'formula5', xtype: 'label', anchor: '100%',
                html: 'e(d(c(1,b),b),e(o(1,0),b))', cls: 'customlabel',
            }]
        }]
    }],
    buttons: [{
        text: struct_lang_LBL5, name: 'saver',
        formBind: true,
        handler: function () {
            this.up('form').submit({
                url: link_saveTask2,
                waitMsg: struct_lang_LBL6,
                success: function () {
                    var s = extractPath(currently_selected);
                    treestore.load({ callback: function () { tree.selectPath(s); } });
                }
            });
        }
    }]
});

var form_task1 = new Ext.form.Panel({
    hidden: true, title: struct_lang_LBL28, waitMsgTarget: true,
    items: [{ //служебная часть
        bodyPadding: 10, layout: 'anchor', hidden: true,
        items: [{
            name: 'Id', xtype: 'textfield', anchor: '100%', hidden: true,
            fieldLabel: 'Guid', labelAlign: 'right', labelWidth: 170
        }, {
            name: 'ordernumber', xtype: 'textfield', anchor: '100%',
            fieldLabel: 'Порядковый номер', labelAlign: 'right', labelWidth: 170
        }]
    }, {
        border: false,
        layout: { type: 'hbox', align: 'stretchmax' },
        items: [{ //операция
            flex: 1, bodyPadding: 10, layout: 'anchor', height: 200,
            items: [{
                name: 'rb_task', inputValue: 'operation', id: 'rb_task1',
                xtype: 'radiofield', boxLabel: struct_lang_LBL29,
                fieldLabel: struct_lang_LBL30, labelAlign: 'right', labelWidth: labelWidthOfTextfield,
                validateValue: function (value) { return validateScalesNotEqual(value);},
                checked: true, handler: function () { task1_radio_change(this.checked); }
            }, {
                name: 'scale', xtype: 'combobox', anchor: '100%', editable: false,
                fieldLabel: struct_lang_LBL31, labelAlign: 'right', labelWidth: labelWidthOfTextfield,
                store: ['2', '8', '16']
            }, {
                name: 'operation', xtype: 'combobox', anchor: '100%', editable: false,
                fieldLabel: struct_lang_LBL29, labelAlign: 'right', labelWidth: labelWidthOfTextfield,
                store: ['+', '-', '*']
            }, {
                name: 'number1', xtype: 'numberfield', anchor: '100%', editable: false,
                minValue: 10, maxValue: 50,
                fieldLabel: struct_lang_LBL32, labelAlign: 'right', labelWidth: labelWidthOfTextfield
            }, {
                name: 'number2', xtype: 'numberfield', anchor: '100%', editable: false,
                minValue: 10, maxValue: 50,
                fieldLabel: struct_lang_LBL33, labelAlign: 'right', labelWidth: labelWidthOfTextfield
            }]
        }, { //перевод
            flex: 1, bodyPadding: 10, layout: 'anchor', height: 200,
            items: [{
                name: 'rb_task', inputValue: 'translation', id: 'rb_task2',
                xtype: 'radiofield', boxLabel: struct_lang_LBL34,
                fieldLabel: struct_lang_LBL30, labelAlign: 'right', labelWidth: labelWidthOfTextfield,
                validateValue: function (value) { return validateScalesNotEqual(value); }
            }, {
                name: 'scale1', xtype: 'combobox', anchor: '100%', disabled: true, editable: false,
                fieldLabel: struct_lang_LBL35, labelAlign: 'right', labelWidth: labelWidthOfTextfield,
                store: new Ext.data.SimpleStore({
                    data: [
                        ['2'],
                        ['8'],
                        ['10'],
                        ['16']
                    ],
                    fields: ['sc1']
                }),
                valueField: 'sc1',
                displayField: 'sc1',
                validateValue: function (value) { return validateScalesNotEqual(value);}
            }, {
                name: 'scale2', xtype: 'combobox', anchor: '100%', disabled: true, editable: false,
                fieldLabel: struct_lang_LBL36, labelAlign: 'right', labelWidth: labelWidthOfTextfield,
                store: new Ext.data.SimpleStore({
                    data: [
                        ['2'],
                        ['8'],
                        ['10'],
                        ['16']
                    ],
                    fields: ['sc2']
                }),
                valueField: 'sc2',
                displayField: 'sc2',
                validateValue: function (value) { return validateScalesNotEqual(value); }
            }, {
                name: 'number', xtype: 'numberfield', anchor: '100%', disabled: true, editable: false,
                minValue: 10, maxValue: 50,
                fieldLabel: struct_lang_LBL37, labelAlign: 'right', labelWidth: labelWidthOfTextfield
            }]
        }]
    }],
    buttons: [{
        text: struct_lang_LBL5, name: 'saver',
        formBind: true,
        handler: function () {
            this.up('form').submit({
                url: link_saveTask1,
                waitMsg: struct_lang_LBL6,
                success: function () {
                    var s = extractPath(currently_selected);
                    treestore.load({ callback: function () { tree.selectPath(s); } });
                }
            });
        }
    }]
});

var form_question = new Ext.form.Panel({
    hidden: true, title: struct_lang_LBL12, waitMsgTarget: true,
    items: [{ //КРАЙНЯЯ ВЕРХНЯЯ ЧАСТЬ - служебная
        bodyPadding: 10, layout: 'anchor', hidden: true,
        items: [{
            name: 'id', xtype: 'textfield', anchor: '100%',
            fieldLabel: 'Guid', labelAlign: 'right', labelWidth: 170
        }, {
            name: 'ordernumber', xtype: 'textfield', anchor: '100%',
            fieldLabel: 'Порядковый номер', labelAlign: 'right', labelWidth: 170
        }, {
            name: 'picq_path', xtype: 'textfield', anchor: '100%',
            fieldLabel: 'Путь к картинке-пояснению', labelAlign: 'right', labelWidth: 170
        }, {
            name: 'pica_path', xtype: 'textfield', anchor: '100%',
            fieldLabel: 'Путь к картинке с ответами', labelAlign: 'right', labelWidth: 170
        }]
    }, {
        border: false,
        layout: { type: 'hbox', align: 'stretchmax' },
        items: [{ //ВЕРХНЯЯ ЛЕВАЯ ЧЕТВЕРТЬ - текст вопроса
            flex: 1, bodyPadding: 10, layout: 'anchor',
            items: {
                name: 'text', xtype: 'textarea',
                fieldLabel: struct_lang_LBL13, labelAlign: 'top',
                anchor: '100%', height: 188
            }
        }, { //ВЕРХНЯЯ ПРАВАЯ ЧЕТВЕРТЬ - картинка-пояснение
            flex: 1, bodyPadding: 10, layout: 'anchor', height: 210,
            items: [{
                xtype: 'label', name: 'clarification', html: '<center>' + struct_lang_LBL14 + ':</center>',
                cls: 'customlabel',
            }, {
                name: 'picq_preview', xtype: 'image', src: '',
                anchor: '100%', height: 150, padding: 5,
                listeners: { render: function (c) { question_pic_popup(c, 'picq_path'); } }
            }, {
                name: 'picq_file', xtype: 'filefield', readOnly: false,
                fieldLabel: struct_lang_LBL15, labelWidth: 30,
                buttonText: struct_lang_LBL16, anchor: '100%'
            }]
        }]
    }, {
        border: false,
        layout: { type: 'hbox', align: 'stretchmax' },        
        items: [{ //НИЖНЯЯ ЛЕВАЯ ЧЕТВЕРТЬ - ответы текстом
            flex: 1, bodyPadding: 10, layout: 'anchor', height: 240,
            items: [{
                name: 'rb', inputValue: 'by_txt', id: 'rb1',
                xtype: 'radiofield', boxLabel: struct_lang_LBL18,
                fieldLabel: struct_lang_LBL17, labelAlign: 'right', labelWidth: labelWidthOfTextfield,
                checked: true, handler: function () { question_radio_change(this.checked); }
            }, {
                name: 'anscount1', xtype: 'numberfield', anchor: '100%',
                fieldLabel: struct_lang_LBL19, labelAlign: 'right', labelWidth: labelWidthOfTextfield,
                minValue: 2, maxValue: 5, value: 5, editable: false, padding: '5 0 5 0',
                listeners: { change: { fn: function () { question_anscount1_change(this.value); } } }
            }, {
                name: 'q1', xtype: 'fieldcontainer', bodyPadding: 10,
                fieldLabel: struct_lang_LBL20 + ' №1', labelAlign: 'right', labelWidth: labelWidthOfTextfield,
                anchor: '100%', layout: 'hbox',
                items: [
                    { name: 'q1_text', xtype: 'textfield', flex: 2 },
                    { name: 'q1_stat', xtype: 'checkbox', flex: 1, boxLabel: struct_lang_LBL21, padding: '0 0 0 10' }
                ]
            }, {
                name: 'q2', xtype: 'fieldcontainer', bodyPadding: 10,
                fieldLabel: struct_lang_LBL20 + ' №2', labelAlign: 'right', labelWidth: labelWidthOfTextfield,
                anchor: '100%', layout: 'hbox',
                items: [
                    { name: 'q2_text', xtype: 'textfield', flex: 2 },
                    { name: 'q2_stat', xtype: 'checkbox', flex: 1, boxLabel: struct_lang_LBL21, padding: '0 0 0 10' }
                ]
            }, {
                name: 'q3', xtype: 'fieldcontainer', bodyPadding: 10,
                fieldLabel: struct_lang_LBL20 + ' №3', labelAlign: 'right', labelWidth: labelWidthOfTextfield,
                anchor: '100%', layout: 'hbox',
                items: [
                    { name: 'q3_text', xtype: 'textfield', flex: 2 },
                    { name: 'q3_stat', xtype: 'checkbox', flex: 1, boxLabel: struct_lang_LBL21, padding: '0 0 0 10' }
                ]
            }, {
                name: 'q4', xtype: 'fieldcontainer', bodyPadding: 10,
                fieldLabel: struct_lang_LBL20 + ' №4', labelAlign: 'right', labelWidth: labelWidthOfTextfield,
                anchor: '100%', layout: 'hbox',
                items: [
                    { name: 'q4_text', xtype: 'textfield', flex: 2 },
                    { name: 'q4_stat', xtype: 'checkbox', flex: 1, boxLabel: struct_lang_LBL21, padding: '0 0 0 10' }
                ]
            }, {
                name: 'q5', xtype: 'fieldcontainer', bodyPadding: 10,
                fieldLabel: struct_lang_LBL20 + ' №5', labelAlign: 'right', labelWidth: labelWidthOfTextfield,
                anchor: '100%', layout: 'hbox',
                items: [
                    { name: 'q5_text', xtype: 'textfield', flex: 2 },
                    { name: 'q5_stat', xtype: 'checkbox', flex: 1, boxLabel: struct_lang_LBL21, padding: '0 0 0 10' }
                ]
            }]
        }, { //НИЖНЯЯ ПРАВАЯ ЧЕТВЕРТЬ - ответы картинкой
            flex: 1, bodyPadding: 10, layout: 'anchor', height: 240,
            items: [{
                name: 'rb', inputValue: 'by_pic', id: 'rb2',
                xtype: 'radiofield', boxLabel: struct_lang_LBL22,
                fieldLabel: struct_lang_LBL17, labelAlign: 'right', labelWidth: 110
            }, {
                name: 'anscount2', xtype: 'numberfield', anchor: '100%', disabled: true,
                fieldLabel: struct_lang_LBL19, labelAlign: 'right', labelWidth: 150,
                minValue: 2, maxValue: 5, value: 5, editable: false, padding: '5 0 5 0',
                listeners: { change: { fn: function () { question_anscount2_change(this.value); } } }
            }, {
                name: 'q1-5', xtype: 'fieldcontainer', bodyPadding: 10, disabled: true,
                fieldLabel: struct_lang_LBL23, labelAlign: 'right', labelWidth: 110,
                layout: 'hbox',
                items: [
                    { name: 'avp1', xtype: 'checkbox', flex: 1, boxLabel: '1' },
                    { name: 'avp2', xtype: 'checkbox', flex: 1, boxLabel: '2' },
                    { name: 'avp3', xtype: 'checkbox', flex: 1, boxLabel: '3' },
                    { name: 'avp4', xtype: 'checkbox', flex: 1, boxLabel: '4' },
                    { name: 'avp5', xtype: 'checkbox', flex: 1, boxLabel: '5' }
                ]
            }, {
                name: 'pica_preview', xtype: 'image', src: '',
                anchor: '100%', height: 100, padding: 5, disabled: true,
                listeners: { render: function (c) { question_pic_popup(c, 'pica_path'); } }
            }, {
                name: 'pica_file', xtype: 'filefield', readOnly: false,
                fieldLabel: struct_lang_LBL15, labelWidth: 30, disabled: true,
                buttonText: struct_lang_LBL16, anchor: '100%'
            }]
        }]
    }],
    buttons: [{
        text: struct_lang_LBL5, name: 'saver',
        formBind: true,
        handler: function () {
            this.up('form').down('[name=pica_file]').setDisabled(false);
            this.up('form').submit({
                url: link_saveQuestion,
                waitMsg: struct_lang_LBL6,
                success: function () {
                    form_question.down('[name=pica_file]').setDisabled(true);
                    var s = ''; s = extractPath(currently_selected);
                    treestore.load({ callback: function () { tree.selectPath(s); } });
                }
            });
        }
    }]
});
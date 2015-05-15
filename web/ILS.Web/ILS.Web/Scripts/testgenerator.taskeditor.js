Ext.define('TestGenerator.model.Person', {
    extend: 'Ext.data.Model',
    idProperty: 'PersonID',
    fields: [
        { name: 'PersonID', type: 'int', useNull: true },
        { name: 'FirstName', type: 'string' },
        { name: 'LastName', type: 'string' },
        { name: 'CreateDate', type: 'date', dateFormat: 'MS' }
    ]
});

Ext.define('TestGenerator.model.PersonStore', {
    extend: 'Ext.data.Store',
    autoLoad: true,
    autoSync: true,
    model: 'TestGenerator.model.Person',
    proxy: {
        type: 'ajax',
        url: urlPersonNewList,
        api: {
            read: urlPersonNewList,
            create: urlPersonCreate,
            update: urlPersonEdit,
            destroy: urlPersonDelete
        },
        reader: {
            type: 'json',
            rootProperty: 'data',
            totalProperty: 'total'
        },
        writer: {
            type: 'json',
            encode: false,
            listful: true,
            writeAllFields: true,
            getRecordData: function (record) {
                return { 'data': Ext.JSON.encode(record.data) };
            }
        },
        headers: { 'Content-Type': 'application/json; charset=UTF-8' }
    }
});

////////////////////////////////////

var myData = [
    ['a', 'Целый'],
    ['b', 'Целый'],
    ['c', 'Целый'],
    ['d', 'Целый'],
    ['число_a', 'Число'],
    ['число_b', 'Число'],
    ['мин', 'Целый'],
    ['макс', 'Целый'],
    ['средн_арифм', 'Дробный']
];

var myDataTypes = [
    ['Целый'],
    ['Дробный'],
    ['Логический'],
    ['Текстовый'],
    ['Число']
];

var store = Ext.create('Ext.data.ArrayStore', {
    fields: ['data', 'typeOfData'],
    data: myData
});

var storeDataTypes = Ext.create('Ext.data.ArrayStore', {
    fields: ['dataType'],
    data: myDataTypes
});

// Левая панель "Библиотека функций"
var panelFunctions = {
    title: 'Библиотека функций',
    region: 'west',
    xtype: 'treepanel',
    margins: '4 0 0 4',
    border: 0,
    width: '25%',
    height: '100%',
    collapsible: true,
    id: 'west-region-container',
    layout: 'fit',
    split: true,
    useArrows: true,
    rootVisible: false,
    tools: [{
        type: 'expand',
        callback: function (panelFunctions) {
            panelFunctions.expandAll();
        }
    }, {
        type: 'collapse',
        callback: function (panelFunctions) {
            panelFunctions.collapseAll();
        }
    }, {
        type: 'search',
        callback: function (panel) {
            // do search
        }
    }, {
        type: 'maximize',
        callback: function (panelFunctions) {
            var panelFunctionsClone = panelFunctions.cloneConfig();
            var window = new Ext.window.Window({
                rtl: false,
                maximized: true,
                title: "Code Preview",
                closable: true,
                layout: "fit",
                items: panelFunctionsClone
            });
            window.show(panelFunctions);
        }
    }, {
        type: 'help',
        callback: function () {
            var window = new Ext.window.Window({
                rtl: false,
                height: 200,
                width: 400,
                padding: 5,
                border: 0,
                title: 'Справка / Библиотека функций',
                closable: true,
                layout: "fit",
                html: '<h4>Библиотека функций</h4>\nЗдесь будет написана справочная информация по работе с разделом "Библиотека функций"'
            });
            window.show();
        }
    }
    ],
    root: {
        children: [{
            text: 'СТАНДАРТНЫЕ',
            expanded: true,
            children:
            [{
                text: "Математические",
                children: [{
                    text: "Тригонометрические",
                    expanded: true,
                    children: [{
                        text: "СИНУС",
                        leaf: true
                    }, {
                        text: "КОСИНУС",
                        leaf: true
                    }, {
                        text: "ТАНГЕНС",
                        leaf: true
                    }, {
                        text: "КОТАНГЕНС",
                        leaf: true
                    }]
                }, {
                    text: "Сравнения",
                    expanded: true,
                    children: [{
                        text: "РАВНО",
                        leaf: true
                    }, {
                        text: "МЕНЬШЕ",
                        leaf: true
                    }, {
                        text: "БОЛЬШЕ",
                        leaf: true
                    }]
                }, {
                    text: "КОРЕНЬ",
                    leaf: true
                }, {
                    text: "МОДУЛЬ",
                    leaf: true
                }],
                "expanded": true
            },
            {
                text: "Статистические",
                expanded: true,
                children: [{
                    text: "СРЕДНЕЕ",
                    leaf: true
                }, {
                    text: "МИНИМУМ",
                    leaf: true
                }, {
                    text: "МАКСИМУМ",
                    leaf: true
                }]
            },
            {
                text: "Логические",
                expanded: true,
                children: [{
                    text: "ИСТИНА",
                    leaf: true
                }, {
                    text: "ЛОЖЬ",
                    leaf: true
                }, {
                    text: "И",
                    leaf: true
                }, {
                    text: "ИЛИ",
                    leaf: true
                }, {
                    text: "НЕ",
                    leaf: true
                }]
            }]
        }, {
            text: 'ПОЛЬЗОВАТЕЛЬСКИЕ',
            expanded: true,
            children:
            [{
                text: "Системы счисления",
                children: [{
                    text: "1",
                    leaf: true
                }, {
                    text: "2",
                    leaf: true
                }, {
                    text: "3",
                    leaf: true
                }, {
                    text: "4",
                    leaf: true
                }]
            }, {
                text: "Алгебра логики",
                expanded: true,
                children: [{
                    text: "5",
                    leaf: true
                }, {
                    text: "6",
                    leaf: true
                }, {
                    text: "7",
                    leaf: true
                }, {
                    text: "8",
                    leaf: true
                }]
            }]
        }]
    }
};

// Правая панель с панелями "Данные" и "Типы данных"
var panelDataAndDatatypes = {
    title: 'Данные предметной области',
    region: 'east',
    xtype: 'panel',
    padding: '0 0 0 3',
    width: '25%',
    border: 0,
    split: true,
    collapsible: true,
    id: 'east-region-container',
    layout: 'border',
    listeners: {
        collapse: function (panel) {
            panel.padding = "5 5 5 5";
        },
        expand: function (panel) {
            panel.padding = "15 15 15 15";
        }
    },
    items: [{
        title: 'Переменные величины',
        region: 'center',
        minHeight: 200,
        border: 0,
        layout: 'fit',
        split: true,
        tools: [{
            type: 'plus',
        }, {
            type: 'minus',
        }, {
            type: 'search',
        }, {
            type: 'help',
        }],
        items: Ext.create('Ext.grid.Panel', {
            store: store,
            border: false,
            columns: [
                {
                    sortable: true,
                    text: 'Название',
                    dataIndex: 'data',
                    width: '47%'
                },
                {
                    sortable: true,
                    text: 'Тип',
                    dataIndex: 'typeOfData',
                    width: '47%'
                }
            ],
            height: '100%',
            width: '100%'
        })
    }, {
        title: 'Типы данных',
        region: 'south',
        minHeight: 200,
        height: 200,
        border: 0,
        layout: 'fit',
        split: true,
        collapsible: true,
        padding: '3 0 0 0',
        tools: [{
            type: 'plus',
        }, {
            type: 'minus',
        }, {
            type: 'search',
        }, {
            type: 'help',
        }],
        items: Ext.create('Ext.grid.Panel', {
            store: storeDataTypes,
            border: false,
            columns: [
                {
                    sortable: true,
                    text: 'Тип',
                    dataIndex: 'dataType',
                    width: '100%'
                }
            ],
            height: '100%',
            width: '100%'
        })
    }]
};

Ext.tip.QuickTipManager.init();  // enable tooltips

// Верхняя область редактора шаблона
var panelEditorArea = {
    title: 'Редактор шаблона',
    region: 'center',
    xtype: 'panel',
    layout: 'fit',
    padding: '0 0 0 3',
    border: 0,
    items: [
        new Ext.form.HtmlEditor({
            id: 'code_editor_area',
            cls: 'code_editor',
            xtype: 'htmleditor',
            border: 0,
            enableAlignments: false,
            enableFontSize: false,
            enableFormat: false,
            enableLists: false,
            enableSourceEdit: false,
            getDocMarkup: function () {
                // отключить проверку орфографии в контролле
                return '<html><head><style type="text/css">p{margin:0em !important; font-family:Courier New !important;}</style></head><body spellcheck="false" style="line-height: 1.5 !important; font-family:Courier New !important;"><p></p></body></html>';
            }
        })]
        /*{
        id: 'sourceCodeEditor',
        xtype: 'textarea',
        margins: '-1 0 0 0',
        border: 0,
        value: 'a = ГЕНЕР_ЧИСЛО(мин, макс)\nb = ГЕНЕР_ЧИСЛО(мин, макс)\nчисло_a = ПЕРЕВОД(a, 2)\nчисло_b = ПЕРЕВОД(b, 2)\nсредн_арифм = СРЕДНЕЕ(число_a, число_b)\nОТВЕТ = ПЕРЕВОД(средн_арифм, 8)',
    }*/
    };

var personGrid = Ext.create('Ext.grid.Panel', {
    height: '100%',
    width: '100%',
    border: false,
    store: Ext.create('TestGenerator.model.PersonStore'),
    columns: [
            { dataIndex: 'PersonID', text: 'PersonID', width: 40, sortable: true },
            {
                dataIndex: 'FirstName', text: 'First Name', width: 120, sortable: true,
                field: { xtype: 'textfield' }
            },
            {
                dataIndex: 'LastName', text: 'Last Name', width: 120, sortable: true,
                field: { xtype: 'textfield' }
            },
            {
                dataIndex: 'CreateDate', text: 'Create Date', width: 120, sortable: true, renderer: Ext.util.Format.dateRenderer('d/m/Y'),
                field: { xtype: 'datefield' }
            }],
    plugins: [
            Ext.create('Ext.grid.plugin.RowEditing', {
                clicksToEdit: 1,
                pluginId: 'rowEditing'
            })
    ],
    listeners: {
        itemclick: function (view, record, item, index, e) {
            /*var personID = record.get('PersonID');
            comInfoStore.load({
                params: {
                    personID: personID
                }
            });*/
        }
    }/*,
    dockedItems: [{
        xtype: 'pagingtoolbar',
        store: personStore,
        dock: 'bottom',
        displayInfo: true,
        emptyMsg: 'No data to displayy'
    }]*/
});

// Нижняя центральная панель "Результат работы генератора"
var panelResultOfGeneration = {
    title: 'Результат работы генератора',
    region: 'south',
    xtype: 'panel',
    layout: 'fit',
    padding: '3 0 0 3',
    weight: -100,
    minHeight: 200,
    height: 200,
    split: true,
    collapsible: true,
    border: 0,
    //html: '<div id="personGrid"></div>',
    items: personGrid
};

// Главная панель (включает в себя все остальные панели и области)
Ext.define('TestGenerator.mainPanel', {
    extend: 'Ext.panel.Panel',
    config: {
        width: '100%',
        layout: 'fit',
        items: {
            id: 'mainpanel',
            layout: {
                type: 'border',
                padding: '5'
            },
            border: 0,
            xtype: 'panel',
            tools: [{
                type: 'close',
            }, {
                type: 'minimize',
            }, {
                type: 'maximize',
            }, {
                type: 'restore',
            }, {
                type: 'toggle',
            }, {
                type: 'gear',
            }, {
                type: 'prev',
            }, {
                type: 'next',
            }, {
                type: 'pin',
            }, {
                type: 'unpin',
            }, {
                type: 'right',
            }, {
                type: 'left',
            }, {
                type: 'down',
            }, {
                type: 'up',
            }, {
                type: 'refresh',
            }, {
                type: 'plus',
            }, {
                type: 'minus',
            }, {
                type: 'search',
            }, {
                type: 'save',
            }, {
                type: 'print',
            }, {
                type: 'search',
            }, {
                type: 'help',
            }],
            items: [
                panelFunctions,
                panelDataAndDatatypes,
                panelEditorArea,
                panelResultOfGeneration
            ]
        }
    }
});

convertToHTML = function (text) {
    var htmlText = "";
    var lines = text.split("\n");
    for (var i = 0; i < lines.length; i++) { htmlText += Ext.String.format('<div>{0}</div>', lines[i]); }
    return htmlText;
}

convertFromHTML = function () {
    var htmlText = Ext.getCmp('code_editor_area').getValue();
    var result = Ext.util.Format.stripTags(htmlText.replace(new RegExp("<div>", 'g'), "<div>\n")).replace(new RegExp("&lt;", 'g'), "<").replace(new RegExp("&gt;", 'g'), ">").replace(new RegExp("&nbsp;", 'g'), " ");
    return (htmlText.startsWith("<div>")) ? result.substr(1) : result;
}

Ext.onReady(function () {
    var mainPanel = Ext.create('TestGenerator.mainPanel');
    mainPanel.getComponent('mainpanel').setTitle("Генератор тестов");
    Ext.getCmp('code_editor_area').getToolbar().hide();
    Ext.getCmp("code_editor_area").setValue(" ");
    renderToMainArea(mainPanel);

    var text = 'a = ГЕНЕР_ЧИСЛО(мин, макс);\nb = ГЕНЕР_ЧИСЛО(мин, макс);\nчисло_a = ПЕРЕВОД(a, 2);\nчисло_b = ПЕРЕВОД(b, 2);\nсредн_арифм = СРЕДНЕЕ(число_a, число_b);\nОТВЕТ = ПЕРЕВОД(средн_арифм, 8)';
    //Ext.getCmp("code_editor_area").setValue(convertToHTML(text));

    // так можно получить plaintext:
    // Ext.util.Format.stripTags(Ext.getCmp('code_editor_area').getValue().replace(new RegExp("</p><p", 'g'), "</p>\n<p")).replace(new RegExp("&lt;", 'g'), "<").replace(new RegExp("&gt;", 'g'), ">").replace(new RegExp("&nbsp;", 'g'), " ")

    //var personGrid = Ext.create('Ext.grid.Panel', {
    //    renderTo: 'personGrid',
    //    width: 600,
    //    height: 300,
    //    //frame: true,
    //    //title: 'Persons',
    //    store: Ext.create('TestGenerator.model.PersonStore'),
    //    iconCls: 'icon-user',
    //    columns: [
    //            { dataIndex: 'PersonID', text: 'PersonID', width: 40, sortable: true },
    //            {
    //                dataIndex: 'FirstName', text: 'First Name', width: 120, sortable: true,
    //                field: { xtype: 'textfield' }
    //            },
    //            {
    //                dataIndex: 'LastName', text: 'Last Name', width: 120, sortable: true,
    //                field: { xtype: 'textfield' }
    //            },
    //            {
    //                dataIndex: 'CreateDate', text: 'Create Date', width: 120, sortable: true, renderer: Ext.util.Format.dateRenderer('d/m/Y'),
    //                field: { xtype: 'datefield' }
    //            }],
    //    plugins: [
    //            Ext.create('Ext.grid.plugin.RowEditing', {
    //                clicksToEdit: 1,
    //                pluginId: 'rowEditing'
    //            })
    //    ],
    //    listeners: {
    //        itemclick: function (view, record, item, index, e) {
    //            /*var personID = record.get('PersonID');
    //            comInfoStore.load({
    //                params: {
    //                    personID: personID
    //                }
    //            });*/
    //        }
    //    }/*,
    //    dockedItems: [{
    //        xtype: 'pagingtoolbar',
    //        store: personStore,
    //        dock: 'bottom',
    //        displayInfo: true,
    //        emptyMsg: 'No data to displayy'
    //    }]*/
    //});
});
Ext.require(['Ext.draw.Component', 'Ext.Window']);

Ext.define('Course', {
    extend: 'Ext.data.Model',
    fields: [
        { name: 'name', type: 'string' },
        { name: 'id', type: 'string' }
    ]
});

var myStore = Ext.create('Ext.data.Store', {
    model: 'Course',
    proxy: {
        type: 'ajax',
        url: link_readCourses,
        reader: {
            type: 'json',
            root: 'courses'
        }
    },
    autoLoad: true
});

var courseSelect = new Ext.FormPanel({
    frame: true,
    defaultType: 'textfield',
    monitorValid: true,

    fieldDefaults: {
        labelWidth: 150,
        msgTarget: 'side'
    },
    items: [{
        id: 'coursecmb',
        xtype: 'combobox',
        fieldLabel: 'Выберите курс',
        store: myStore,
        displayField: 'name',
        valueField: 'id'
    }],

    buttons: [{
        text: 'Открыть',
        formBind: false,
        // Function that fires when user clicks the button 
        handler: function () {
            selectedId = Ext.getCmp('coursecmb').getValue();
            wwin.hide();
            Ext.Ajax.request({
                url: link_getCourse,
                params: {
                    id: selectedId
                },
                success: function (response) {
                    var text = response.responseText;

                    var main = Ext.getCmp('mainArea');
                    var paper = Joint.paper('mainArea', main.width, main.height);
                    if (text.length != 0) 
                    {
                        var dia = Joint.dia.parse(text.replace(/'/g, "\""));
                    }
                }
            });
        }
    }]
});


// This just creates a window to wrap the login form. 
// The login object is passed to the items collection.       
var wwin = new Ext.Window({
    layout: 'fit',
    width: 350,
    height: 150,
    closable: false,
    resizable: false,
    plain: true,
    border: false,
    title: 'Выбор курса',
    items: [courseSelect],
    modal: true
});

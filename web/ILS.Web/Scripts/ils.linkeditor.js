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
        //warning: hardcoded
        url: '/ils2/linkeditor/ReadCourses',
        reader: {
            type: 'json',
            root: 'courses'
        }
    },
    autoLoad: true
});

var instance;

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
                url: '/ils2/linkeditor/GetCourse',
                params: {
                    id: selectedId
                },
                success: function (response) {                   
                    var course = JSON.parse(response.responseText);
                    var bodyText = '<div class="demo dynamic-demo" id="dynamic-demo">\n';
                    for (var i = 0; i < course.themes.length; i++) {
                        bodyText += '<div class="window" id="' + course.themes[i].id + '"><strong>' + course.themes[i].name + '</strong><br/><br/></div>\n';
                    }
                    bodyText += '</div>';
                    var body = new Ext.panel.Panel({
                        html: bodyText
                    });
                    var tlbar = new Ext.toolbar.Toolbar({
                        items: [{
                            text: 'Сохранить', iconCls: 'paragraph_add',
                            handler: function () { saveCourse(); }
                        }]
                    });

                    var editor = new Ext.Panel({
                        title: course.name,
                        layout: 'auto',
                        dockedItems: [tlbar],
                        items: [body]
                    });

                    renderToMainArea(editor);

                    var sourceAnchors = [[0.2, 0, 0, -1, 0, 0, "foo"], [1, 0.2, 1, 0, 0, 0, "bar"], [0.8, 1, 0, 1, 0, 0, "baz"], [0, 0.8, -1, 0, 0, 0, "qux"]],
		                targetAnchors = [[0.6, 0, 0, -1], [1, 0.6, 1, 0], [0.4, 1, 0, 1], [0, 0.4, -1, 0]],

		                exampleColor = '#00f',
		                exampleDropOptions = {
		                    tolerance: 'touch',
		                    hoverClass: 'dropHover',
		                    activeClass: 'dragActive'
		                },
		                connector = ["Straight", { cssClass: "connectorClass", hoverClass: "connectorHoverClass"}],
		                connectorStyle = {
		                    gradient: { stops: [[0, exampleColor], [0.5, '#09098e'], [1, exampleColor]] },
		                    lineWidth: 5,
		                    strokeStyle: exampleColor
		                },
		                hoverStyle = {
		                    strokeStyle: "#449999"
		                },
		                overlays = [["Arrow", { fillStyle: "#09098e", width: 15, length: 15, location: 0.3}],
                            ["Arrow", { fillStyle: "#09098e", width: 15, length: 15, location: 0.7}]],
		                endpoint = ["Dot", { cssClass: "endpointClass", radius: 10, hoverClass: "endpointHoverClass"}],
		                endpointStyle = { fillStyle: exampleColor },
		                anEndpoint = {
		                    endpoint: endpoint,
		                    paintStyle: endpointStyle,
		                    hoverPaintStyle: { fillStyle: "#449999" },
		                    isSource: true,
		                    isTarget: true,
		                    maxConnections: -1,
		                    connector: connector,
		                    connectorStyle: connectorStyle,
		                    connectorHoverStyle: hoverStyle,
		                    connectorOverlays: overlays
		                };

                    instance = jsPlumb.getInstance({
                        DragOptions: { cursor: 'pointer', zIndex: 2000 },
                        Container: "dynamic-demo"
                    });

                    // suspend drawing and initialise.
                    instance.doWhileSuspended(function () {

		                var endpoints = {},
                        // ask jsPlumb for a selector for the window class
		                divsWithWindowClass = jsPlumb.getSelector(".dynamic-demo .window");

                        // add endpoints to all of these - one for source, and one for target, configured so they don't sit
                        // on top of each other.
                        for (var i = 0; i < divsWithWindowClass.length; i++) {
                            var id = instance.getId(divsWithWindowClass[i]);
                            endpoints[id] = [
                            // note the three-arg version of addEndpoint; lets you re-use some common settings easily.
				                instance.addEndpoint(id, anEndpoint, { anchor: sourceAnchors }),
				                instance.addEndpoint(id, anEndpoint, { anchor: targetAnchors })
			                ];
                        }
                        // then connect blocks
                        for (var i = 0; i < course.themes.length; i++) {
                            var outputLinkesArray = course.themes[i].outputThemeLinks;
                            if (outputLinkesArray == null) {
                                continue;
                            }
                            for (var j = 0; j < outputLinkesArray.length; j++) {
                                instance.connect({
                                    source: endpoints[outputLinkesArray[j].parentThemeId][0],
                                    target: endpoints[outputLinkesArray[j].linkedThemeId][1]
                                });
                            }
                        }

                        // bind click listener; delete connections on click			
                        instance.bind("click", function (conn) {
                            instance.detach(conn);
                        });


                        // bind beforeDetach interceptor: will be fired when the click handler above calls detach, and the user
                        // will be prompted to confirm deletion.
                        instance.bind("beforeDetach", function (conn) {
                            return confirm("Вы уверены, что хотите удалить связь?");
                        });

                        instance.draggable(divsWithWindowClass);
                    });
                }
            });
        }
    }]

});

Ext.onReady(function () 
{
    wwin.show();
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

saveCourse = function () {
    var connections = instance.getAllConnections();

    var connctionsForSend = [connections.length];

    for (var i = 0; i < connections.length; i++) {
        var conn = new Object();
        conn.parentThemeLink = connections[i].sourceId;
        conn.linkedThemeLink = connections[i].targetId;
        connctionsForSend[i] = conn;
    }


    Ext.Ajax.request({
        //warning: hardcoded
        url: '/ils2/linkeditor/SaveCourse',
        method: 'POST',
        params: { connections: JSON.stringify(connctionsForSend) }
        
    });
}
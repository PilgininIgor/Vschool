//рекурсивная функция, определяющая полный путь в дереве от корня до указанного узла
//крайне понадобится нам, чтобы возвращаться к нужному элементу после перезагрузки дерева
extractPath = function (node) {
    if (node.getDepth() > 0) {
        return extractPath(node.parentNode) + '/' + node.id;
    }
    else return '/treeRoot';
}

//хранилище данных, связанное с деревом
//с такими настройками хранилище будет само обращаться по ссылке link_data каждый раз, когда юзер разворачивает очередную ветвь
//это ссылка определена в файле Struct/Index.cshtml и ведет на метод ReadTree контроллера StructController
//то есть обращение по ссылке ведет к вызову метода
//а в методе мы пишем код, который вернет дереву нужные данные
//данные возвращаются в формате JSON и без перезагрузки страницы, т.е. асинхронно, т.е. AJAX
var treestore = new Ext.data.TreeStore({
    root: {
        text: 'root',
        id: 'treeRoot'
    },
    proxy: {
        type: 'ajax',
        url: link_data        
    }
});

//левая часть - дерево
var tree = new Ext.tree.Panel({
    id: 'learnContentTree',
    region: 'west',
    width: '25%',
    split: true,
    title: 'Список учебных материалов',
    store: treestore,
    rootVisible: false,
    animate: false
});

var currently_selected; //переменная, где всегда будет ссылка на текущий выделенный элемент дерева

//вызывается каждый раз, когда юзер выделяет кликом новый элемент дерева
tree.on('selectionchange', function (dataView, selections) {
    if (selections.length == 1) {
        //console.log(selections[0].data.id + ' ' + selections[0].data.text + ' ' + selections[0].getDepth());
        currently_selected = selections[0];
        //определить глубину (понятно, что 1 - это курсы, 2 - темы, 3 - лекции с тестами + задания и остров, 4 - параграфы с вопросами)
        var d = selections[0].getDepth();

        //кусок, который отвечает за вывод в правую часть окна нужной формы и загрузки в нее данных
        if (d < 4) {
            //если это курсы, то скрываем поле с порядковым номером, т.к. курсы единственные, где его нет
            if (d == 1) form_cttc.items.items[0].items.items[2].hide();
            else form_cttc.items.items[0].items.items[2].show();
            if (selections[0].raw.iconCls == "test") {
                form_test.getForm().load({
                    //загрузить данные в форму
                    url: link_readCTTC, //обратиться по этой ссылке, т.е. вызвать метод ReadCTTC из контроллера Struct
                    params: { id: selections[0].data.id, depth: d } //передать методу ReadCTTC эти параметры
                });
                form_cttc.hide();
                form_test.show();
                form_paragraph.hide();
                form_question.hide();
                form_task1.hide();
            } else if (selections[0].raw.iconCls == "tgtasktemplate") {
                form_task1.getForm().load({
                    //загрузить данные в форму
                    url: link_readTask1, //обратиться по этой ссылке, т.е. вызвать метод readTask1 из контроллера Struct
                    params: { id_s: selections[0].data.id } //передать методу readTask1 эти параметры
                });
                form_cttc.hide();
                form_test.hide();
                form_paragraph.hide();
                form_question.hide();
                form_task1.show();
            } else {
                form_cttc.getForm().load({
//загрузить данные в форму
                    url: link_readCTTC, //обратиться по этой ссылке, т.е. вызвать метод ReadCTTC из контроллера Struct
                    params: { id: selections[0].data.id, depth: d }, //передать методу ReadCTTC эти параметры
                    success: function() { //после того, как метод вернул результат и данные успешно загружены
                        if (isRussian) {
                            if (this.form.getValues().type == "course") form_cttc.setTitle("Курс");
                            else if (this.form.getValues().type == "theme") form_cttc.setTitle("Тема");
                            else if (this.form.getValues().type == "lecture") form_cttc.setTitle("Лекция");
                            else if (this.form.getValues().type == "task") form_cttc.setTitle("Задание");
                            //else if (this.form.getValues().type == "island") form_cttc.setTitle("Локация Остров");
                            else form_cttc.setTitle("Тест");
                        } else {
                            if (this.form.getValues().type == "course") form_cttc.setTitle("Course");
                            else if (this.form.getValues().type == "theme") form_cttc.setTitle("Theme");
                            else if (this.form.getValues().type == "lecture") form_cttc.setTitle("Lecture");
                            else if (this.form.getValues().type == "task") form_cttc.setTitle("Task");
                            //else if (this.form.getValues().type == "island") form_cttc.setTitle("Island Location");
                            else form_cttc.setTitle("Test");
                        }
                    }
                });
                form_cttc.show();
                form_test.hide();
                form_paragraph.hide();
                form_question.hide();
                form_task1.hide();
            }
        } else {
            if (selections[0].raw.iconCls == "paragraph") {
                form_paragraph.getForm().load({
                    params: { id: selections[0].data.id },
                    url: link_readParagraph,
                    waitMsg: isRussian ? 'Загрузка...' : 'Loading...',
                    success: function () {
                        for (var i = 1; i <= 20; i++) {
                            if (form_paragraph.down('[name=pic' + i + '_path]').getValue() != "")
                                form_paragraph.down('[name=pic' + i + '_show]').show();
                            else form_paragraph.down('[name=pic' + i + '_show]').hide();
                        }
                    }
                });
                form_cttc.hide(); form_test.hide();
                form_paragraph.show(); form_question.hide();
                form_task1.hide();
            } else {
                form_question.getForm().load({
                    params: { id_s: selections[0].data.id },
                    url: link_readQuestion,
                    success: function () {
                        var s1 = form_question.down("[name=picq_path]").getValue();
                        var s2 = form_question.down("[name=pica_path]").getValue();
                        //if (s1 == "") s1 = "http://localhost/ILS/Content/pics_service/EmptyImage.png";
                        //if (s2 == "") s2 = "http://localhost/ILS/Content/pics_service/EmptyImage.png";
                        form_question.down("[name=picq_preview]").setSrc(s1);
                        form_question.down("[name=pica_preview]").setSrc(s2);
                    }
                });
                form_cttc.hide(); form_paragraph.hide();
                form_test.hide(); form_question.show();
                form_task1.hide();
            }
        }

        //кусок, который скрывает ненужные и показывает нужные кнопки на тулбаре
        tlbar.items.items[0].hide(); tlbar.items.items[1].hide(); tlbar.items.items[2].hide(); tlbar.items.items[3].hide();
        tlbar.items.items[4].hide(); tlbar.items.items[5].hide(); tlbar.items.items[6].hide(); tlbar.items.items[7].hide();
        tlbar.items.items[8].hide(); tlbar.items.items[9].hide(); tlbar.items.items[10].hide(); tlbar.items.items[11].hide();
        tlbar.items.items[12].hide(); tlbar.items.items[13].hide(); tlbar.items.items[14].hide(); tlbar.items.items[15].hide();
        tlbar.items.items[16].hide(); tlbar.items.items[17].hide(); tlbar.items.items[18].hide(); tlbar.items.items[19].hide();
        tlbar.items.items[20].hide(); tlbar.items.items[21].hide(); //tlbar.items.items[21].hide(); //tlbar.items.items[17].hide();
        switch (d) {
            case 1:
                tlbar.items.items[0].show(); tlbar.items.items[1].show(); tlbar.items.items[2].show();
                break;
            case 2:
                tlbar.items.items[3].show(); tlbar.items.items[4].show(); tlbar.items.items[6].show();
                tlbar.items.items[17].show(); tlbar.items.items[18].show(); tlbar.items.items[20].show(); tlbar.items.items[21].show();
                tlbar.items.items[12].show(); tlbar.items.items[13].show();                
                tlbar.items.items[16].show();                
                //tlbar.items.items[17].show();
                break;
            case 3:
                if (selections[0].raw.iconCls == "test") {
                    tlbar.items.items[7].show(); tlbar.items.items[10].show(); tlbar.items.items[15].show();
                } else if (selections[0].raw.iconCls == "lecture") {
                    tlbar.items.items[5].show(); tlbar.items.items[8].show(); tlbar.items.items[14].show();
                } else {
                    tlbar.items.items[19].show();
                } 
                tlbar.items.items[12].show(); tlbar.items.items[13].show();
                break;
            case 4:
                if (selections[0].raw.iconCls == "paragraph") tlbar.items.items[9].show();
                else tlbar.items.items[11].show();
                tlbar.items.items[12].show(); tlbar.items.items[13].show();
                break;
        }
    }
});
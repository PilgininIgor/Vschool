if (Ext.util.Cookies.get("language") == null) Ext.util.Cookies.set("language", lang_pref);
if (Ext.util.Cookies.get("language") == "Russian") isRussian = true; else isRussian = false;
titleOfSection = 'Course editor';
if (isRussian) {
    titleOfSection = 'Редактор курсов';
}

var forms = new Ext.Panel({
    region: 'center',
    width: '75%',
    autoScroll: true,
    margin: "-1px 0px 0px 0px",
    //все формы загружаются в контейнер сразу, но активна одновременна максимум одна из них, остальные скрыты
    //это обеспечивается кодом из обработчика tree.on('selectionchange'
    items: [form_cttc, form_paragraph, form_question, form_test, form_task1, form_task2, form_task3]
});

var editor = new Ext.Panel({
    title: titleOfSection,
    layout: 'border',
    dockedItems: [tlbar],
    items: [tree, forms]
});

Ext.onReady(function () {
    renderToMainArea(editor);
    for (var i = 1; i <= 20; i++) form_paragraph_addPic(i);
    if (!isRussian) changeToEnglish();
});

changeToEnglish = function () {
    tlbar.items.items[0].setText('Add a course'); tlbar.items.items[1].setText('Remove the course');
    tlbar.items.items[2].setText('Add a theme'); tlbar.items.items[3].setText('Remove the theme');
    tlbar.items.items[4].setText('Add a lecture'); tlbar.items.items[5].setText('Remove the lecture');
    tlbar.items.items[6].setText('Add a test'); tlbar.items.items[7].setText('Remove the test');
    tlbar.items.items[8].setText('Add a paragraph'); tlbar.items.items[9].setText('Remove the paragraph');
    tlbar.items.items[10].setText('Add a question'); tlbar.items.items[11].setText('Remove the question');
    tlbar.items.items[12].setText('Move up'); tlbar.items.items[13].setText('Move down');
    tlbar.items.items[14].setText('Upload a file with a lecture'); tlbar.items.items[15].setText('Upload a file with a test');
    tlbar.items.items[16].setText('Upload from Moodle'); tlbar.items.items[17].setText('Add numeric systems task');
    tlbar.items.items[18].setText('Add logic task'); tlbar.items.items[19].setText('Remove task');
    tlbar.items.items[20].setText('Add tower of Hanoi task'); tlbar.items.items[21].setText('Add Island location');
    //tlbar.items.items[21].setText('Remove location'); tlbar.items.items[17].setText('Update list from Moodle');
    tree.setTitle('List of learning materials');
}
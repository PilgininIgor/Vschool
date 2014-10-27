function escapeRegExp(string) {
    return string.replace(/([.*+?^=!:${}()|\[\]\/\\])/g, "\\$1");
}


var sliderAuthorsFrame;
var authorsCount;
var sliderAuthorsFrames;
var pointAuthorsFrames;
var pointScreensFrames;
var textAuthorsFrames;

var sliderAwardsFrame;
var awardsCount;
var sliderAwardsFrames;
var pointAwardsFrames;
var textAwardsFrames;
var imgAwardsFrame;
var imgAwardsFrames;

var sliderAchievementsFrame;
var achievementsCount;
var sliderAchievementsFrames;
var pointAchievementsFrames;
var textAchievementsFrames;
var imgAchievementsFrame;
var imgAchievementsFrames;


var sliderScreensFrame;
var sliderScreensFrames;
var screenshotsCount;
var screenListsCount;

function initContents() {
    var url = document.location.href.slice(0, document.location.href.lastIndexOf("/") + 1) + "about/getAuthors";
    Ext.Ajax.request({
        url: url,
        success: function (response, opts) {
            var a = eval('(' + response.responseText + ')');
            //alert('success');
            authorsCount = a.authors.length;
            document.getElementById("sliderAuthorsContainer").innerHTML = "";
            document.getElementById('textAuthorsContainer').innerHTML = "";
            document.getElementById('authorsPointContainer').innerHTML = "";

            for (i = 0; i < authorsCount; i++) {
                document.getElementById("sliderAuthorsContainer").innerHTML += '<img src="/ils2/Content/Sprites/authors/' + a.authors[i].Image + '">';
                document.getElementById('authorsPointContainer').innerHTML += '<div class="arrow" style="background: url(content/sprites/point_' + (i == sliderAuthorsFrame ? '' : 'i') + 'a.png) no-repeat;" onclick="javascript:sliderAuthorsGoto(' + i + ')"></div>';
                document.getElementById('textAuthorsContainer').innerHTML += '<div class="text-pane"><p align="center" class="author_name">' + a.authors[i].Name + '</p><div class="scroll-pane"><p align="justify" class="author_description">' + a.authors[i].Description + '</p></div></div>';
            }
            sliderAuthorsFrames = document.getElementById("sliderAuthorsContainer").children;
            textAuthorsFrames = document.getElementById('textAuthorsContainer').children;
            pointAuthorsFrames = document.getElementById('authorsPointContainer').children;
            document.getElementById('authorsArrowsContainer').style.marginLeft = 20 + (-(62 + pointAuthorsFrames.length * 31) / 2) + "px";
            document.getElementById("authorsNumber").style.marginLeft = ((44 + pointAuthorsFrames.length * 22) + 20) + "px";

            $('.scroll-pane').jScrollPane();

            for (i = 0; i < authorsCount; i++) {
                if (i == sliderAuthorsFrame) {
                    textAuthorsFrames[i].style.display = "block";
                    sliderAuthorsFrames[i].style.display = "block";
                } else {
                    textAuthorsFrames[i].style.display = "none";
                    sliderAuthorsFrames[i].style.display = "none";
                }
            }
            document.getElementById("authorsNumber").innerHTML = sliderAuthorsFrame + 1 + "/" + authorsCount;


        },
        failure: function (response, opts) {
            //alert('fail');
        }
    });

    var url = document.location.href.slice(0, document.location.href.lastIndexOf("/") + 1) + "about/getAwards";
    Ext.Ajax.request({
        url: url,
        success: function (response, opts) {
            var a = eval('(' + response.responseText + ')');
            //alert('success');
            awardsCount = a.awards.length;
            document.getElementById("sliderAwardsContainer").innerHTML = "";
            document.getElementById('textAwardsContainer').innerHTML = "";
            document.getElementById('awardsPointContainer').innerHTML = "";
            sliderAwardsFrame = 0;
            for (i = 0; i < awardsCount; i++) {
                var imgs = a.awards[i].Image.split(";");
                var imgString = '<div style="display:' + (i == 0 ? 'block' : 'none') + '">';
                for (j = 0; j < imgs.length; j++) {
                    if (imgs[j] == "")
                        continue;
                    imgString += '<img style="position:absolute;display:' + (j == 0 ? 'block' : 'none') + '" src="/ils2/Content/Sprites/awards/' + imgs[j].replace(new RegExp(escapeRegExp('.png'), 'g'), '.jpg') + '"';
                    imgString += 'onclick="javascript:enlargeImage(';
                    imgString += "'";
                    //imgString += [imgs[j].slice(0,imgs[j].lastIndexOf('/')), '/full', imgs[j].slice(imgs[j].lastIndexOf('/'))].join('');
                    imgString += ['/ils2/Content/Sprites/awards/full/', imgs[j]].join('');
                    imgString += "')";
                    imgString += '">';
                }
                imgString += '</div>';
                document.getElementById("sliderAwardsContainer").innerHTML += imgString;
                document.getElementById('awardsPointContainer').innerHTML += '<div class="arrow" style="background: url(content/sprites/point_' + (i == sliderAwardsFrame ? '' : 'i') + 'a.png) no-repeat;" onclick="javascript:sliderAwardsGoto(' + i + ')"></div>';
                document.getElementById('textAwardsContainer').innerHTML += '<div class="text-pane"><p align="center" class="author_name">' + a.awards[i].Name + '</p><div class="scroll-pane"><p align="justify" class="author_description">' + a.awards[i].Description + '</p></div></div>';
            }
            sliderAwardsFrames = document.getElementById("sliderAwardsContainer").children;
            textAwardsFrames = document.getElementById('textAwardsContainer').children;
            pointAwardsFrames = document.getElementById('awardsPointContainer').children;
            document.getElementById('awardsArrowsContainer').style.marginLeft = 20 + (-(62 + pointAwardsFrames.length * 31) / 2) + "px";
            document.getElementById("awardsNumber").style.marginLeft = ((44 + pointAwardsFrames.length * 22) + 20) + "px";



            if (sliderAwardsFrames.length > 0) {
                imgAwardsFrames = sliderAwardsFrames[sliderAwardsFrame].children;
                imgAwardsFrame = 0;
                for (i = 0; i < awardsCount; i++) {
                    if (i == sliderAwardsFrame) {
                        textAwardsFrames[i].style.display = "block";
                        sliderAwardsFrames[i].style.display = "block";
                    } else {
                        textAwardsFrames[i].style.display = "none";
                        sliderAwardsFrames[i].style.display = "none";
                    }
                }

                document.getElementById("awardsNumber").innerHTML = sliderAwardsFrame + 1 + "/" + awardsCount;
            } else {
                imgAwardsFrames = {};
                imgAwardsFrame = 0;
                document.getElementById("awardsNumber").innerHTML = sliderAwardsFrame + 0 + "/" + awardsCount;
            }
        },
        failure: function (response, opts) {
            //alert('fail');
        }
    });

    var url = document.location.href.slice(0, document.location.href.lastIndexOf("/") + 1) + "about/getAchievements";
    Ext.Ajax.request({
        url: url,
        success: function (response, opts) {
            var a = eval('(' + response.responseText + ')');
            //alert('success');
            achievementsCount = a.achievements.length;
            document.getElementById("sliderAchievementsContainer").innerHTML = "";
            document.getElementById('textAchievementsContainer').innerHTML = "";
            document.getElementById('achievementsPointContainer').innerHTML = "";
            sliderAchievementsFrame = 0;
            for (i = 0; i < achievementsCount; i++) {
                var imgs = a.achievements[i].Image.split(";");
                var imgString = '<div style="display:' + (i == 0 ? 'block' : 'none') + '">';
                for (j = 0; j < imgs.length; j++) {
                    if (imgs[j] == "")
                        continue;
                    imgString += '<img style="position:absolute;display:' + (j == 0 ? 'block' : 'none') + '" src="/ils2/Content/Sprites/achievements/' + imgs[j].replace(new RegExp(escapeRegExp('.png'), 'g'), '.jpg') + '"';
                    imgString += 'onclick="javascript:enlargeImage(';
                    imgString += "'";
                    //imgString += [imgs[j].slice(0,imgs[j].lastIndexOf('/')), '/full', imgs[j].slice(imgs[j].lastIndexOf('/'))].join('');
                    imgString += ['/ils2/Content/Sprites/achievements/full/', imgs[j]].join('');
                    imgString += "')";
                    imgString += '">';
                }
                imgString += '</div>';
                document.getElementById("sliderAchievementsContainer").innerHTML += imgString;
                document.getElementById('achievementsPointContainer').innerHTML += '<div class="arrow" style="background: url(content/sprites/point_' + (i == sliderAchievementsFrame ? '' : 'i') + 'a.png) no-repeat;" onclick="javascript:sliderAchievementsGoto(' + i + ')"></div>';
                document.getElementById('textAchievementsContainer').innerHTML += '<div class="text-pane"><p align="center" class="author_name">' + a.achievements[i].Name + '</p><div class="scroll-pane"><p align="justify" class="author_description">' + a.achievements[i].Description + '</p></div></div>';
            }
            sliderAchievementsFrames = document.getElementById("sliderAchievementsContainer").children;
            textAchievementsFrames = document.getElementById('textAchievementsContainer').children;
            pointAchievementsFrames = document.getElementById('achievementsPointContainer').children;
            document.getElementById('achievementsArrowsContainer').style.marginLeft = 20 + (-(62 + pointAchievementsFrames.length * 31) / 2) + "px";
            document.getElementById("achievementsNumber").style.marginLeft = ((44 + pointAchievementsFrames.length * 22) + 20) + "px";




            if (sliderAchievementsFrames.length > 0) {
                imgAchievementsFrames = sliderAchievementsFrames[sliderAchievementsFrame].children;
                imgAchievementsFrame = 0;
                for (i = 0; i < achievementsCount; i++) {
                    if (i == sliderAchievementsFrame) {
                        textAchievementsFrames[i].style.display = "block";
                        sliderAchievementsFrames[i].style.display = "block";
                    } else {
                        textAchievementsFrames[i].style.display = "none";
                        sliderAchievementsFrames[i].style.display = "none";
                    }
                }

                document.getElementById("achievementsNumber").innerHTML = sliderAchievementsFrame + 1 + "/" + achievementsCount;
            } else {
                imgAchievementsFrames = {};
                imgAchievementsFrame = 0;
                document.getElementById("achievementsNumber").innerHTML = sliderAchievementsFrame + 0 + "/" + achievementsCount;
            }

        },
        failure: function (response, opts) {
            //alert('fail');
        }
    });

    var url = document.location.href.slice(0, document.location.href.lastIndexOf("/") + 1) + "about/getScreenshots";
    Ext.Ajax.request({
        url: url,
        success: function (response, opts) {
            var a = eval('(' + response.responseText + ')');
            //alert('success');
            screenshotsCount = a.screenshots.length;
            document.getElementById("screenshots").innerHTML = "";
            document.getElementById('sliderScreensContainer').innerHTML = "";
            document.getElementById('screensPointContainer').innerHTML = "";
            sliderScreensFrame = 0;
            var display = true;
            var containerString = '';
            var screenshotsString = '';
            for (i = 0; i < screenshotsCount; i++) {

                switch (i % 6) {
                    case 0:
                        containerString += display ? '<div>' : '<div style="display:none">';
                        containerString += '<img class="screen_lt" ';
                        break;
                    case 1:
                        containerString += '<img class="screen_mt" ';
                        break;
                    case 2:
                        containerString += '<img class="screen_rt" ';
                        break;
                    case 3:
                        containerString += '<img class="screen_lb" ';
                        break;
                    case 4:
                        containerString += '<img class="screen_mb" ';
                        break;
                    case 5:
                        containerString += '<img class="screen_rb" ';
                        display = false;
                        break;
                }
                containerString += 'src="' + a.screenshots[i] + '" ';
                containerString += 'onclick="javascript:showScreen(';
                containerString += "'" + (i + 1) + "'";
                containerString += ')"';
                containerString += 'style="cursor: pointer;">';
                if (i % 6 == 5)
                    containerString += '</div>';


                screenshotsString += '<div style="position:absolute;top:10px;left:50%;display:none;width:809px;height:400px;margin-left:-404px;cursor:pointer;" onclick="javascript:hideScreen(';
                screenshotsString += "'";
                screenshotsString += (i + 1);
                screenshotsString += "')";
                screenshotsString += '">';
                screenshotsString += '<img align="middle" src="';
                screenshotsString += [a.screenshots[i].slice(0, a.screenshots[i].lastIndexOf('/')), '/full', a.screenshots[i].slice(a.screenshots[i].lastIndexOf('/'))].join('');
                screenshotsString += '" style="max-width: 809px; max-height:400px; left:50%">';
                screenshotsString += '</div>';

                document.getElementById('screensPointContainer').innerHTML += '<div class="arrow" style="background: url(content/sprites/point_' + (i == sliderScreensFrame ? '' : 'i') + 'a.png) no-repeat;" onclick="javascript:sliderAchievementsGoto(' + i + ')"></div>';
            }
            screenshotsString += '<div class="screens_left" onclick="javascript:screensDecr()" style="cursor:ponter;"></div><div class="screens_right" onclick="javascript:screensIncr()" style="cursor:ponter;"></div>';
            containerString = containerString.replace(new RegExp(escapeRegExp('.png'), 'g'), '.jpg');
            screenListsCount = Math.ceil(a.screenshots.length / 6);
            document.getElementById('sliderScreensContainer').innerHTML = containerString;
            document.getElementById("screenshots").innerHTML = screenshotsString;
        },
        failure: function (response, opts) {
            //alert('fail');
        }
    });
}




function sliderAwardsInit() {
    document.getElementById("backScreens").style.display = "none";
    document.getElementById("back").style.display = "block";
    if (sliderAwardsFrames.length == 0)
        return;
    if (sliderAwardsFrame == null)
        sliderAwardsFrame = 0;
    imgAwardsFrames = sliderAwardsFrames[sliderAwardsFrame].children;
    imgAwardsFrame = 0;
}

function sliderAwardsDecr() {
    if (sliderAwardsFrames.length == 0)
        return;
    if (sliderAwardsFrames == null) {
        sliderAwardsInit();
    }
    sliderAwardsFrames[sliderAwardsFrame].style.display = "none";
    textAwardsFrames[sliderAwardsFrame].style.display = "none";
    pointAwardsFrames[sliderAwardsFrame].style.background = "url(content/sprites/point_ia.png) no-repeat";
    sliderAwardsFrame--;
    if (sliderAwardsFrame < 0) {
        sliderAwardsFrame = awardsCount - 1;
    }
    sliderAwardsFrames[sliderAwardsFrame].style.display = "block";
    textAwardsFrames[sliderAwardsFrame].style.display = "block";
    pointAwardsFrames[sliderAwardsFrame].style.background = "url(content/sprites/point_a.png) no-repeat";
    document.getElementById("awardsNumber").innerHTML = sliderAwardsFrame + 1 + "/" + awardsCount;
    imgAwardsFrames = sliderAwardsFrames[sliderAwardsFrame].children;
    imgAwardsFrame = 0;
    $('.scroll-pane').jScrollPane();
}

function sliderAwardsIncr() {
    if (sliderAwardsFrames.length == 0)
        return;
    if (sliderAwardsFrames == null) {
        sliderAwardsInit();
    }
    sliderAwardsFrames[sliderAwardsFrame].style.display = "none";
    textAwardsFrames[sliderAwardsFrame].style.display = "none";
    pointAwardsFrames[sliderAwardsFrame].style.background = "url(content/sprites/point_ia.png) no-repeat";
    sliderAwardsFrame++;
    if (sliderAwardsFrame > awardsCount - 1) {
        sliderAwardsFrame = 0;
    }
    sliderAwardsFrames[sliderAwardsFrame].style.display = "block";
    textAwardsFrames[sliderAwardsFrame].style.display = "block";
    pointAwardsFrames[sliderAwardsFrame].style.background = "url(content/sprites/point_a.png) no-repeat";
    document.getElementById("awardsNumber").innerHTML = sliderAwardsFrame + 1 + "/" + awardsCount;
    imgAwardsFrames = sliderAwardsFrames[sliderAwardsFrame].children;
    imgAwardsFrame = 0;
    $('.scroll-pane').jScrollPane();
}

function sliderAwardsGoto(index) {
    if (sliderAwardsFrames.length == 0)
        return;
    if (sliderAwardsFrames == null) {
        sliderAwardsInit();
    }
    sliderAwardsFrames[sliderAwardsFrame].style.display = "none";
    textAwardsFrames[sliderAwardsFrame].style.display = "none";
    pointAwardsFrames[sliderAwardsFrame].style.background = "url(content/sprites/point_ia.png) no-repeat";
    sliderAwardsFrame = index;
    sliderAwardsFrames[sliderAwardsFrame].style.display = "block";
    textAwardsFrames[sliderAwardsFrame].style.display = "block";
    pointAwardsFrames[sliderAwardsFrame].style.background = "url(content/sprites/point_a.png) no-repeat";
    document.getElementById("awardsNumber").innerHTML = sliderAwardsFrame + 1 + "/" + awardsCount;
    imgAwardsFrames = sliderAwardsFrames[sliderAwardsFrame].children;
    imgAwardsFrame = 0;
    $('.scroll-pane').jScrollPane();
}

function sliderAwardsImgIncr() {
    if (sliderAwardsFrames.length == 0 || imgAwardsFrames.length == 0)
        return;
    imgAwardsFrames[imgAwardsFrame].style.display = "none";
    imgAwardsFrame--;
    if (imgAwardsFrame < 0) {
        imgAwardsFrame = imgAwardsFrames.length - 1;
    }
    imgAwardsFrames[imgAwardsFrame].style.display = "block";
}

function sliderAwardsImgDecr() {
    if (sliderAwardsFrames.length == 0 || imgAwardsFrames.length == 0)
        return;
    imgAwardsFrames[imgAwardsFrame].style.display = "none";
    imgAwardsFrame++;
    if (imgAwardsFrame > imgAwardsFrames.length - 1) {
        imgAwardsFrame = 0;
    }
    imgAwardsFrames[imgAwardsFrame].style.display = "block";
}



function sliderAchievementsInit() {
    document.getElementById("backScreens").style.display = "none";
    document.getElementById("back").style.display = "block";
    if (sliderAchievementsFrames.length == 0)
        return;
    if (sliderAchievementsFrame == null)
        sliderAchievementsFrame = 0;
    imgAchievementsFrames = sliderAchievementsFrames[sliderAchievementsFrame].children;
    imgAchievementsFrame = 0;
}

function sliderAchievementsDecr() {
    if (sliderAchievementsFrames.length == 0)
        return;
    if (sliderAchievementsFrames == null) {
        sliderAchievementsInit();
    }
    sliderAchievementsFrames[sliderAchievementsFrame].style.display = "none";
    textAchievementsFrames[sliderAchievementsFrame].style.display = "none";
    pointAchievementsFrames[sliderAchievementsFrame].style.background = "url(content/sprites/point_ia.png) no-repeat";
    sliderAchievementsFrame--;
    if (sliderAchievementsFrame < 0) {
        sliderAchievementsFrame = achievementsCount - 1;
    }
    sliderAchievementsFrames[sliderAchievementsFrame].style.display = "block";
    textAchievementsFrames[sliderAchievementsFrame].style.display = "block";
    pointAchievementsFrames[sliderAchievementsFrame].style.background = "url(content/sprites/point_a.png) no-repeat";
    document.getElementById("achievementsNumber").innerHTML = sliderAchievementsFrame + 1 + "/" + achievementsCount;
    imgAchievementsFrames = sliderAchievementsFrames[sliderAchievementsFrame].children;
    imgAchievementsFrame = 0;
    $('.scroll-pane').jScrollPane();
}

function sliderAchievementsIncr() {
    if (sliderAchievementsFrames.length == 0)
        return;
    if (sliderAchievementsFrames == null) {
        sliderAchievementsInit();
    }
    sliderAchievementsFrames[sliderAchievementsFrame].style.display = "none";
    textAchievementsFrames[sliderAchievementsFrame].style.display = "none";
    pointAchievementsFrames[sliderAchievementsFrame].style.background = "url(content/sprites/point_ia.png) no-repeat";
    sliderAchievementsFrame++;
    if (sliderAchievementsFrame > achievementsCount - 1) {
        sliderAchievementsFrame = 0;
    }
    sliderAchievementsFrames[sliderAchievementsFrame].style.display = "block";
    textAchievementsFrames[sliderAchievementsFrame].style.display = "block";
    pointAchievementsFrames[sliderAchievementsFrame].style.background = "url(content/sprites/point_a.png) no-repeat";
    document.getElementById("achievementsNumber").innerHTML = sliderAchievementsFrame + 1 + "/" + achievementsCount;
    imgAchievementsFrames = sliderAchievementsFrames[sliderAchievementsFrame].children;
    imgAchievementsFrame = 0;
    $('.scroll-pane').jScrollPane();
}

function sliderAchievementsGoto(index) {
    if (sliderAchievementsFrames.length == 0)
        return;
    if (sliderAchievementsFrames == null) {
        sliderAchievementsInit();
    }
    sliderAchievementsFrames[sliderAchievementsFrame].style.display = "none";
    textAchievementsFrames[sliderAchievementsFrame].style.display = "none";
    pointAchievementsFrames[sliderAchievementsFrame].style.background = "url(content/sprites/point_ia.png) no-repeat";
    sliderAchievementsFrame = index;
    sliderAchievementsFrames[sliderAchievementsFrame].style.display = "block";
    textAchievementsFrames[sliderAchievementsFrame].style.display = "block";
    pointAchievementsFrames[sliderAchievementsFrame].style.background = "url(content/sprites/point_a.png) no-repeat";
    document.getElementById("achievementsNumber").innerHTML = sliderAchievementsFrame + 1 + "/" + achievementsCount;
    imgAchievementsFrames = sliderAchievementsFrames[sliderAchievementsFrame].children;
    imgAchievementsFrame = 0;
    $('.scroll-pane').jScrollPane();
}

function sliderAchievementsImgIncr() {
    if (sliderAchievementsFrames.length == 0 || imgAchievementsFrames.length == 0)
        return;
    imgAchievementsFrames[imgAchievementsFrame].style.display = "none";
    imgAchievementsFrame--;
    if (imgAchievementsFrame < 0) {
        imgAchievementsFrame = imgAchievementsFrames.length - 1;
    }
    imgAchievementsFrames[imgAchievementsFrame].style.display = "block";
}

function sliderAchievementsImgDecr() {
    if (sliderAchievementsFrames.length == 0 || imgAchievementsFrames.length == 0)
        return;
    imgAchievementsFrames[imgAchievementsFrame].style.display = "none";
    imgAchievementsFrame++;
    if (imgAchievementsFrame > imgAchievementsFrames.length - 1) {
        imgAchievementsFrame = 0;
    }
    imgAchievementsFrames[imgAchievementsFrame].style.display = "block";
}


function sliderAuthorsInit() {
    document.getElementById("backScreens").style.display = "none";
    document.getElementById("back").style.display = "block";
    if (sliderAuthorsFrame == null)
        sliderAuthorsFrame = 0;
}

function sliderAuthorsDecr() {
    if (sliderAuthorsFrames == null) {
        sliderAuthorsInit();
    }
    sliderAuthorsFrames[sliderAuthorsFrame].style.display = "none";
    textAuthorsFrames[sliderAuthorsFrame].style.display = "none";
    pointAuthorsFrames[sliderAuthorsFrame].style.background = "url(content/sprites/point_ia.png) no-repeat";
    sliderAuthorsFrame--;
    if (sliderAuthorsFrame < 0) {
        sliderAuthorsFrame = authorsCount - 1;
    }
    sliderAuthorsFrames[sliderAuthorsFrame].style.display = "block";
    textAuthorsFrames[sliderAuthorsFrame].style.display = "block";
    pointAuthorsFrames[sliderAuthorsFrame].style.background = "url(content/sprites/point_a.png) no-repeat";
    document.getElementById("authorsNumber").innerHTML = sliderAuthorsFrame + 1 + "/" + authorsCount;
    $('.scroll-pane').jScrollPane();
}

function sliderAuthorsIncr() {
    if (sliderAuthorsFrames == null) {
        sliderAuthorsInit();
    }
    sliderAuthorsFrames[sliderAuthorsFrame].style.display = "none";
    textAuthorsFrames[sliderAuthorsFrame].style.display = "none";
    pointAuthorsFrames[sliderAuthorsFrame].style.background = "url(content/sprites/point_ia.png) no-repeat";
    sliderAuthorsFrame++;
    if (sliderAuthorsFrame > authorsCount - 1) {
        sliderAuthorsFrame = 0;
    }
    sliderAuthorsFrames[sliderAuthorsFrame].style.display = "block";
    textAuthorsFrames[sliderAuthorsFrame].style.display = "block";
    pointAuthorsFrames[sliderAuthorsFrame].style.background = "url(content/sprites/point_a.png) no-repeat";
    document.getElementById("authorsNumber").innerHTML = sliderAuthorsFrame + 1 + "/" + authorsCount;
    $('.scroll-pane').jScrollPane();
}

function sliderAuthorsGoto(index) {
    if (sliderAuthorsFrames == null) {
        sliderAuthorsInit();
    }
    sliderAuthorsFrames[sliderAuthorsFrame].style.display = "none";
    textAuthorsFrames[sliderAuthorsFrame].style.display = "none";
    pointAuthorsFrames[sliderAuthorsFrame].style.background = "url(content/sprites/point_ia.png) no-repeat";
    sliderAuthorsFrame = index;
    sliderAuthorsFrames[sliderAuthorsFrame].style.display = "block";
    textAuthorsFrames[sliderAuthorsFrame].style.display = "block";
    pointAuthorsFrames[sliderAuthorsFrame].style.background = "url(content/sprites/point_a.png) no-repeat";
    document.getElementById("authorsNumber").innerHTML = sliderAuthorsFrame + 1 + "/" + authorsCount;
    $('.scroll-pane').jScrollPane();

}


function sliderScreensInit() {
    document.getElementById("backScreens").style.display = "block";
    document.getElementById("back").style.display = "none";
    if (document.getElementById('screenshots').children.length == 2)
        return;
    sliderScreensFrames = document.getElementById("sliderScreensContainer").children;
    pointScreensFrames = document.getElementById('screensPointContainer').children;
    document.getElementById('screensArrowsContainer').style.marginLeft = 20 + (-(62 + pointScreensFrames.length * 31) / 2) + "px";

    if (sliderScreensFrame == null) {
        sliderScreensFrame = 0;
    }
}

function sliderScreensDecr() {
    if (sliderScreensFrames == null) {
        sliderScreensInit();
    }
    if (document.getElementById('screenshots').children.length == 2)
        return;
    sliderScreensFrames[sliderScreensFrame].style.display = "none";
    pointScreensFrames[sliderScreensFrame].style.background = "url(content/sprites/point_ia.png) no-repeat";
    sliderScreensFrame--;
    if (sliderScreensFrame < 0) {
        sliderScreensFrame = screenListsCount - 1;
    }
    sliderScreensFrames[sliderScreensFrame].style.display = "block";
    pointScreensFrames[sliderScreensFrame].style.background = "url(content/sprites/point_a.png) no-repeat";
}

function sliderScreensIncr() {
    if (sliderScreensFrames == null) {
        sliderScreensInit();
    }
    if (document.getElementById('screenshots').children.length == 2)
        return;
    sliderScreensFrames[sliderScreensFrame].style.display = "none";
    pointScreensFrames[sliderScreensFrame].style.background = "url(content/sprites/point_ia.png) no-repeat";
    sliderScreensFrame++;
    if (sliderScreensFrame > screenListsCount - 1) {
        sliderScreensFrame = 0;
    }
    sliderScreensFrames[sliderScreensFrame].style.display = "block";
    pointScreensFrames[sliderScreensFrame].style.background = "url(content/sprites/point_a.png) no-repeat";
}

function sliderScreensGoto(index) {
    if (document.getElementById('screenshots').children.length == 2)
        return;
    if (sliderScreensFrames == null) {
        sliderScreensInit();
    }
    sliderScreensFrames[sliderScreensFrame].style.display = "none";
    pointScreensFrames[sliderScreensFrame].style.background = "url(content/sprites/point_ia.png) no-repeat";
    sliderScreensFrame = index;
    sliderScreensFrames[sliderScreensFrame].style.display = "block";
    pointScreensFrames[sliderScreensFrame].style.background = "url(content/sprites/point_a.png) no-repeat";
    document.getElementById("ScreensNumber").innerHTML = sliderScreensFrame + 1 + "/" + screenListsCount;

}

function screensIncr() {
    if (document.getElementById('screenshots').children.length == 2)
        return;
    document.getElementById('screenshots').children[currentScreen].style.display = "none";
    currentScreen++;
    if (currentScreen > screenshotsCount - 1) {
        currentScreen = 0;
    }
    document.getElementById('screenshots').children[currentScreen].style.display = "block";
}

function screensDecr() {
    if (document.getElementById('screenshots').children.length == 2)
        return;
    document.getElementById('screenshots').children[currentScreen].style.display = "none";
    currentScreen--;
    if (currentScreen < 0) {
        currentScreen = screenshotsCount - 1;
    }
    document.getElementById('screenshots').children[currentScreen].style.display = "block";
}

var selected = 0;

function selectAuthors() {
    //if (selected == 0)
    //    return;
    selected = 0;
    sliderAuthorsInit();
    document.getElementById('authorsButton').src = "/ils2/Content/Sprites/about/authorsSel.png";
    document.getElementById('screensButton').src = "/ils2/Content/Sprites/about/screens.png"
    document.getElementById('awardsButton').src = "/ils2/Content/Sprites/about/awards.png"
    document.getElementById('achievementsButton').src = "/ils2/Content/Sprites/about/achievements.png"
    document.getElementById('authorsContainer').style.display = "block";
    document.getElementById('screensContainer').style.display = "none";
    document.getElementById('awardsContainer').style.display = "none";
    document.getElementById('achievementsContainer').style.display = "none";
    $('.scroll-pane').jScrollPane();
}

function selectScreens() {
    //if (selected == 1)
    //    return;
    selected = 1;
    sliderScreensInit();
    document.getElementById('authorsButton').src = "/ils2/Content/Sprites/about/authors.png";
    document.getElementById('screensButton').src = "/ils2/Content/Sprites/about/screensSel.png"
    document.getElementById('awardsButton').src = "/ils2/Content/Sprites/about/awards.png"
    document.getElementById('achievementsButton').src = "/ils2/Content/Sprites/about/achievements.png"
    document.getElementById('authorsContainer').style.display = "none";
    document.getElementById('screensContainer').style.display = "block";
    document.getElementById('awardsContainer').style.display = "none";
    document.getElementById('achievementsContainer').style.display = "none";
}

function selectAwards() {
    //if (selected == 2)
    //    return;
    selected = 2;
    sliderAwardsInit();
    document.getElementById('authorsButton').src = "/ils2/Content/Sprites/about/authors.png";
    document.getElementById('screensButton').src = "/ils2/Content/Sprites/about/screens.png"
    document.getElementById('awardsButton').src = "/ils2/Content/Sprites/about/awardsSel.png"
    document.getElementById('achievementsButton').src = "/ils2/Content/Sprites/about/achievements.png"
    document.getElementById('authorsContainer').style.display = "none";
    document.getElementById('screensContainer').style.display = "none";
    document.getElementById('awardsContainer').style.display = "block";
    document.getElementById('achievementsContainer').style.display = "none";
    $('.scroll-pane').jScrollPane();
}

function selectAchievements() {
    //if (selected == 3)
    //    return;
    selected = 3;
    sliderAchievementsInit();
    document.getElementById('authorsButton').src = "/ils2/Content/Sprites/about/authors.png";
    document.getElementById('screensButton').src = "/ils2/Content/Sprites/about/screens.png"
    document.getElementById('awardsButton').src = "/ils2/Content/Sprites/about/awards.png"
    document.getElementById('achievementsButton').src = "/ils2/Content/Sprites/about/achievementsSel.png"
    document.getElementById('authorsContainer').style.display = "none";
    document.getElementById('screensContainer').style.display = "none";
    document.getElementById('awardsContainer').style.display = "none";
    document.getElementById('achievementsContainer').style.display = "block";
    $('.scroll-pane').jScrollPane();
}


function enlargeImage(src) {
    document.getElementById('fullsize').style.maxWidth = window.innerWidth;
    document.getElementById('fullsize').style.maxHeight = window.innerHeight;
    document.getElementById('fullsize').src = src;
    document.getElementById('blackout').style.display = 'block';
    document.getElementById('fullsize').style.left = '50%';
    document.getElementById('fullsize').style.top = '50%';
    function adjustSize() {
        document.getElementById('fullsize').style.display = 'block';
        document.getElementById('fullsize').style.marginLeft = -(document.getElementById('fullsize').width / 2) + 'px';
        document.getElementById('fullsize').style.marginTop = -(document.getElementById('fullsize').height / 2) + 'px';

    }
    setTimeout(adjustSize, 200);
}

function hideImage() {
    document.getElementById('blackout').style.display = 'none';
    document.getElementById('fullsize').style.display = 'none';
}

var currentScreen = 0;

function showScreen(number) {
    document.getElementById('screenshots').style.display = "block";
    document.getElementById('screenshots').children[number - 1].style.display = "block";
    currentScreen = number - 1;
}

function hideScreen(number) {
    document.getElementById('screenshots').style.display = "none";
    document.getElementById('screenshots').children[number - 1].style.display = "none";
    currentScreen = number - 1;
}
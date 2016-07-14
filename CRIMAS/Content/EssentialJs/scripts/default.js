var slider1 = null,currentSamplepage=undefined, slider2 = null, oldthemepath = null, SamplesList = null, editcontrolpath = null, selthemecolor = null, cssheight = null; window.theme = "azure", minScrollerHeight = 550;
window.themecolor = ""; window.themestyle = ""; window.themevarient = "";

$(function () {
    var sbModel = new ViewModel(), isloadLeft = false;
    //if(window.heightval<minScrollerHeight)
    //    window.heightval = minScrollerHeight;
    //else
    //    window.heightval = window.heightval - $("#footer").outerHeight();
    //$("#scrollcontainer").ejScroller({ height: window.heightval, scrollerSize: 8 }).width(252)
    //    .find(".content").addClass("scrollercontainer-content");

    window.Path && Path.map("#!/:theme/:control(/:category1)(/:category2)(/:category3)").to(function () {
        var control = "", categories = [], currentSample, theme = "";
        for (var prop in this.params) {
            if (prop === "theme") {
                window.theme = this.params[prop];
                continue;
            }
            else if (prop === "control") {
                control = this.params[prop];
                continue;
            }
            categories.push(this.params[prop]);
        }

        if (window.theme.indexOf("dark") != -1)
            $(document.body).addClass("darktheme");

        currentSample = categories.pop();
        if (control !== "")
            loadSamplePage(control, currentSample, categories);
        updateSBTheme();
    });
    var updateSBTheme = function () {
        if (window.location.hash.indexOf("lime") != -1) {
            $(".htmljssamplebrowser").attr("class", "htmljssamplebrowser " + "lime");
            $("#themebutton").ejButton('option', 'prefixIcon', 'e-icons ' + "lime");
        }
        else if (window.location.hash.indexOf("saffron") != -1) {
            $(".htmljssamplebrowser").attr("class", "htmljssamplebrowser " + "saffron");
            $("#themebutton").ejButton('option', 'prefixIcon', 'e-icons ' + "saffron");
        }
        else if (window.location.hash.indexOf("bootstrap") != -1) {
            $(".htmljssamplebrowser").attr("class", "htmljssamplebrowser " + "azure");
            $("#themebutton").ejButton('option', 'prefixIcon', 'e-icons ' + "azure");            
        }
    };
    var loadSamplePage = function (control, currentSample, categories) {

        if (control == null) {
            $("#samplesDiv").css("display", "none");
            $(".samples").css("display", "none");
            $("#samplefile").attr('src', 'about:blank');
            self.loadSBMainPage(null);
            return;
        }

        var sample = findSample(control, currentSample, categories), name = sample, parentId = null;

        while (name.samples) {
            if (name.samples) {
                parentId = parseInt(name.id) ? name.id : null;
                name = name.samples[0];
                currentSample = name.querystring;
            } else
                break;
        }

        self.removeSelectedItemcss(control, name.id);

        if ($("#sbtooltipbox").data("ejDialog"))
            $("#sbtooltipbox").ejDialog("close");

        if (isloadLeft || Number(name.id) <= 1)
            self.loadLeftTab(control, currentSample);
        var sampleurl = control + "/" + currentSample + ".html";
        self.loadSamplePage(sampleurl, control, currentSample, parentId, name.id);
        setTimeout("updateHeight()", 200);
    };

    var findSample = function (control, currentSample, categories) {
        var sample = ej.Query().using(ej.DataManager(SamplesList)).where("id", "==", control), current = sample, res;

        for (var k = 0; k < categories.length; k++) {
            current.hierarchy((current = ej.Query("samples").where("url", "==", categories[k])));
        }

        if (currentSample)
            current.hierarchy(ej.Query("samples").where("url", "==", currentSample));

        res = sample.executeLocal();

        if (res.length) res = res[0];
        return res;
    };

    $("#scrollcontainer").on("click", ".secondlevelload, .anchorclass", function (e) {
        var hashBang = $(e.currentTarget).attr("hashbang");
        if (hashBang) {
            var path = hashBang.replace("flat", window.theme);
            if (path != window.location.hash) {
                $('#samplefile').css('visibility', 'hidden');

                window.setTimeout(function () {
                    $(".accordion-panel").height("auto");
                    $(".accordion-panel").height($(".accordion-panel")[0].scrollHeight);
                }, 1000);
            }
            window.location.hash = path;
        }
        var viewportWidth = $(window).width();
        if (viewportWidth < 981) {
            $('.content-container-fluid .row > .navigation').addClass('collapsePanel');
            $('.accordion-panel').removeClass("expandPanel");
        }
    });


    $("#themebutton").ejButton({
        size: "normal",
        width: "60px",
        height: "55px",
        cssClass: "metroblue",
        click: "themebtnClick",
        contentType: "imageonly",
        prefixIcon: "e-uiLight e-icon-handup"
    });
    $("#buybutton").ejButton({
        size: "normal",
        width: "66px",
        height: "55px",
        cssClass: "metroblue",
        contentType: "imageonly",
        prefixIcon: "e-uiLight e-icon-handup"
    });
    $('#productdropdown').ejDropDownList({
        selectedItemIndex: 0,
        width: "130px",
        template: "<div class='txt'>${text}</div>",
        dataSource: [{ text: "Web" },
            { text: "Mobile" }]
    });
    $(document).delegate("#productdropdown_popup_wrapper .txt", "click", function (e) {
        viewdemo(e.target.innerHTML);
    });
    $("#sbtooltipbox").ejDialog({ height: "84px", width: "176px", enableResize: false, showOnInit: false, showHeader: false, cssClass: "metroblue" });
    $("#themeDialog").ejDialog({ height: "160px", enableResize: false, showOnInit: false, showHeader: false, cssClass: "metroblue" });
    $("#metrotext").ejRadioButton({ value: "flat", size: "medium", name: "themestyle", cssClass: "metroblue", change: 'themeonchange' });
    $("#gradienttext").ejRadioButton({ cssClass: "metroblue", size: "medium", name: "themestyle", change: 'themeonchange' });
    $("#lighttext").ejRadioButton({ size: "medium", name: "themevarient", cssClass: "metroblue", change: 'themeonchange' });
    $("#darktext").ejRadioButton({ cssClass: "metroblue", name: "themevarient", size: "medium", change: 'themeonchange' });
    $("#bootstrapcheck").ejCheckBox({ size: "small", change: "bootstraponselect" });
    themeButtonSelect();
    var metroradio = $("#JobSearch").data("ejMenu");
    var firstlevelsamples = [];

    var isContentPageLoaded = null;
    var index = 0;

    function editItem(id, back) {
        var divid = id;
        self.removeSelectedItemcss();
        url = window.location.pathname;
        $("#subsamplesDiv").hide().css("left", "250px");
        $(".cols-iframe,#sourceTab").hide();
        window.location.hash = "" + "#!/" + window.theme;
        loadSamplePage();
        $(".accordion-panel").height("auto");
        $(".accordion-panel").height($(".accordion-panel")[0].scrollHeight);
        //var scroller = $("#scrollcontainer").data("ejScroller");
        //scroller.setModel({ cssClass: "metroblue" });
        //scroller.refresh();
    }
    function ViewModel() {
        this.Controls = SamplesList;
        this.controlname = "";
        this.controlName = null;
        this.sampleName = null;

        self.editSubItem = function (id, back) {
            var divid = id;
            removeSelectedItemcss();
            $("#subsamplesDiv").html('');
            $("#" + divid).css("margin-top", "0px");
            $("#subsamplesDiv").hide().css("left", "250px");
            $("#samplesDiv").css("left", "0px").show();
            $("#" + divid).show().css("visibility", "visible");
            $("#" + divid + "_back").css("display", "block");
            $("#" + divid + "_back").addClass("dashboarddiv");
            $("#" + divid).children(".subsamples").find("a >div").removeClass("dashboardhover");
            $("#samplesDiv").find("#" + divid).css("left", "-250px");
            $("#samplesDiv").find("#" + divid).children(".subsamples").show();
            $("#samplesDiv").find("#" + divid).animate({ left: '0px' }, 200);
            var samplename = null, controlname = null;
            //var scroller = $("#scrollcontainer").data("ejScroller");
            //scroller.setModel({ cssClass: "metroblue" });
            //scroller.refresh();
            window.location.hash = window.location.hash.replace(/(#!\/[^\/]+)\/.+/, "$1");
        };

        self.loadLeftTab = function (divid) {
            if ($("#" + divid).prev().length > 0) {
                $("#" + divid).css("margin-top", "0px");
            }
            $("#accordion2").css("left", "-250px");
            $("#accordion2").prev().css("display", "none");
            $("#accordion2").css("display", "none");
            $("#subsamplesDiv").css("display", "none");
            $(".samples").hide();
            $("#samplesDiv").children("#" + divid).css("display", "block");
            $("#samplesDiv").children("#" + divid).children(".subsamples").show();
            $("#samplesDiv").children("#" + divid).children(".subsamples").find("#subControls").hide();
            $("#" + divid + "_back").css("display", "block");
            $("#" + divid + "_back").addClass("dashboardheader");
            $("#samplesDiv").css("display", "block");
            $("#samplesDiv").css("margin-top", "0px");
            $("#samplesDiv").animate({ left: '0px' }, 0);
            $('html, body').animate({
                scrollTop: 0
            }, 0);
            $(".accordion-panel").height("auto");
            $(".accordion-panel").height($(".accordion-panel")[0].scrollHeight);
            //var scroller = $("#scrollcontainer").data("ejScroller");
            //scroller.setModel({ cssClass: "metroblue" });
            //scroller.refresh();
        };


        self.findSample = function (ctrlname, samplename, subchild, currentsampleid) {
            var query = ej.Query().using(ej.DataManager(SamplesList))
                .where("id", "==", ctrlname), curr = query, res;

            if (subchild)
                query.hierarchy(
                    curr = ej.Query("samples")
                        .where("id", "==", subchild));

            curr.hierarchy(
                ej.Query("samples")
                    .where("id", "==", currentsampleid)
            );

            res = query.executeLocal();

            if (res.length) res = res[0];
            return res;

        },
        self.loadSamplePage = function (url, ctrlname, samplename, subchild, currentsampleid) {
            currentSamplepage = url;
            if ($("#auto").data("ejAutocomplete"))
                $("#auto").ejAutocomplete("clearText");

            $(".sampleheadingtext").empty();
            var sample = self.findSample(ctrlname, samplename, subchild, currentsampleid), sampleTitle = "";

            while (sample) {
                if (sampleTitle)
                    sampleTitle += " / ";

                sampleTitle += sample.name;

                sample = sample.samples && sample.samples[0];
            }

            var names = sampleTitle.split("/"), _samplename = names.pop();
            sampleTitle = names.join("/") + "/ ";

            var sampletitleobj = ej.buildTag("div.samplename sbsamplename");
            ej.buildTag("span", sampleTitle).appendTo(sampletitleobj);
            ej.buildTag("span.sbtxt " + window.themecolor, _samplename).appendTo(sampletitleobj);
            var navigation = ej.buildTag("span.navigation-btn");
			ej.buildTag("a#newsamplewindow", {}, {}, {title:"New Window", target:"_blank"}).appendTo(navigation);
			$("<div>").addClass("windsep").appendTo(navigation);
            var prevState = ej.buildTag("span.prev", "Prev",{},{title:"Previous"}).appendTo(navigation);
            var nextstate = ej.buildTag("span.next", "Next ",{},{title:"Next"}).appendTo(navigation);
            navigation.appendTo(sampletitleobj);
            $(".cols-iframe .sampleheadingtext:first").html(sampletitleobj);
            editcontrolpath = ctrlname + "/" + samplename + ".html";
            document.title = "Essential Studio for JavaScript : " + sampleTitle.replace(/\//g, "-") + " Demo";
            index = 0;
            self.setVisibility("productpage", "cols-iframe");
            //Waiting popoup template
            $(".control-panel").ejWaitingPopup({ template: $("#sbwaitingTemplate") });
            var popupObj = $(".control-panel").ejWaitingPopup("instance");
            popupObj.maindiv.addClass("sbloadingpopup");
            popupObj.show();
            $(".sourcecodetab").hide();
            if (subchild != null) {
                $("#samplesDiv").children("#" + ctrlname).children(".subsamples").children("#" + subchild).children("#subControls").children()[0].id = ctrlname;
                var divtoappend = $("#samplesDiv").children("#" + ctrlname).children(".subsamples").children("#" + subchild).children("#subControls").children().clone(true);
                $("#subsamplesDiv").html('');
                $(divtoappend).appendTo($("#subsamplesDiv"));
                $("#" + ctrlname).children(".dashboard").removeClass("dashboardhover");
                $("#samplesDiv").hide().css("left", "-250px");
                $("#subsamplesDiv").css("margin-top", "0px");
                if ($("#subsamplesDiv").find(".itemselected").length > 0) {
                    $("#subsamplesDiv").find(".itemselected").removeClass("itemselected");
                    $("#subsamplesDiv").find(".itemselected").parent().removeClass("highlighttextbg");
                }
                $("#subsamplesDiv").show();
                $("#subsamplesDiv").children(".firstlevelback").children(".anchor").addClass("sbheading").text("All Controls");
                $("#samplesDiv").children("#" + ctrlname).children(".subsamples").hide();
                $("#subsamplesDiv").find(".highlighttextbg").removeClass("highlighttextbg");
                $("#subsamplesDiv").children(".anchor").show();
                $("#subsamplesDiv").children(".anchor").children().removeClass("hideParent").addClass("dashboard");
                $("#subsamplesDiv").children("#" + subchild).show();
                $("#subsamplesDiv").find("#" + subchild + "_back").find('span').last().text(ctrlname.charAt(0).toUpperCase() + ctrlname.slice(1));
                $("#subsamplesDiv").children(".anchor").children().addClass("dashboardheader")
                $("#subsamplesDiv").children().last().children("#" + currentsampleid).children(".dashboard").children(".anchor").addClass("itemselected");
                $("#subsamplesDiv").find(".itemselected").parent().addClass("highlighttextbg");
                $("#subsamplesDiv").animate({ left: '0px' }, 0);
                $('div.secondlevelback').bind('click', function (evt, args) {
                    editSubItem(ctrlname, 'back');
                });
            }

            $(".prev").ejButton({
                size: "mini",
                cssClass: "metroblue",
                contentType: "imageonly",
                prefixIcon: "e-rarrowleft-2x"
            });
            $(".next").ejButton({
                size: "mini",
                cssClass: "metroblue",
                contentType: "imageonly",
                prefixIcon: "e-rarrowright-2x"
            });
			$("#newsamplewindow").ejButton({
                size: "mini",
                cssClass: "metroblue",
                contentType: "imageonly",
                prefixIcon:"newwindow "
            });

            if (window.theme != "flat")
                url = url + "?theme=" + window.theme;
            $("#samplefile").attr('src', url);
            $("#samplefile")[0].contentWindow.focus();
			$("#newsamplewindow").attr('href',currentSamplepage);

            var curr;
            if (curr = ($("#samplesDiv").children("#" + ctrlname).children('.subsamples').find("div[querystring=" + samplename + "]"))) {
                if ($(curr).attr("childcount") == 0 && !$(curr).hasClass('secondlevelload')) {
                    $("#samplesDiv").children("#" + ctrlname).children('.subsamples').find("div[querystring=" + samplename + "]").children('span.anchor').addClass("itemselected");
                    $("#samplesDiv").children("#" + ctrlname).children('.subsamples').find("div[querystring=" + samplename + "]").addClass("highlighttextbg");
                }
                else {
                    $("#samplesDiv").children("#" + ctrlname).children('.subsamples').find("div[querystring=" + samplename + "]" + '.secondlevelload').find('.anchor').addClass('itemselected');
                    $("#samplesDiv").children("#" + ctrlname).children('.subsamples').find("div[querystring=" + samplename + "]" + '.secondlevelload').addClass("highlighttextbg");
                }
            }
            var currentname = ctrlname;
            var currentfile = samplename;
            var currFile = $('.samples .anchorclass .itemselected').parent('.firstlevelload').parent('.anchorclass');
            var currFileParent = $(currFile).parents('div');
            var subFile = $('.samples .anchorclass .itemselected').parents('.secondlevelload');
            var subFileParent = $('.samples .anchorclass .itemselected').parents('.secondlevelload').parents('.anchorclass');
            $('span.prev').bind('click', function (evt, args) {
                $('#samplefile').css('visibility', 'hidden');
                if ($('.samples .anchorclass .itemselected').parent('.firstlevelload').length == 1) {
                    if ($(currFile).children('div').attr('childcount') == 0 && $(currFile).prev().children('div').attr('childcount') == 0)
                        var hashBang = $(currFile).prev().attr('hashBang');
                    else if ($(currFile).children('div').attr('childcount') == 0 && $(currFile).prev('.anchorclass').length == 0 && $(currFileParent).prev().children('div .anchorclass').last().children('div').attr('childcount') == 0)
                        var hashBang = $(currFileParent).prev().children('div .anchorclass').last().attr('hashBang');
                    else if ($(currFile).children('div').attr('childcount') == 0 && $(currFile).prev('.anchorclass').children('div').attr('childcount') == 1)
                        //var hashBang = $(currFile).prev().children().find('.secondlevelload').last().attr('hashbang');
                        var hashBang = $(currFile).prev().children().find('.anchor').last().parents('.secondlevelload').attr('hashbang');
                    else if ($(currFile).children('div').attr('childcount') == 0 && $(currFile).prev('.anchorclass').length == 0 && $(currFileParent).prev().children('div .anchorclass').last().children('div').attr('childcount') == 1)
                        var hashBang = $(currFile).parents('div').prev().children().find('.secondlevelload').last().attr('hashbang');
                }
                else if ($(subFile).prev('.secondlevelload').length == 1)
                    var hashBang = $(subFile).prev().attr('hashbang');
                else if ($(subFileParent).prev('.anchorclass').children('div').attr('childcount') == 0)
                    var hashBang = $(subFileParent).prev().attr('hashbang');
                else if ($(subFile).prev('.secondlevelload').length == 0 && $(subFileParent).prev().children('div').attr('childcount') == 1)
                    var hashBang = $(subFileParent).prev().children().find('.secondlevelload').last().attr('hashbang');
                queryChange(hashBang);
            });

            $('span.next').bind('click', function (evt, args) {
                $('#samplefile').css('visibility', 'hidden');
                if ($('.samples .anchorclass .itemselected').parent('.firstlevelload').length == 1) {
                    if ($(currFile).children('div').attr('childcount') == 0 && $(currFile).next().children('div').attr('childcount') == 0)
                        var hashBang = $(currFile).next().attr('hashBang');
                    else if ($(currFile).children('div').attr('childcount') == 0 && $(currFile).next('.anchorclass').length == 0 && $(currFileParent).next().children('div .anchorclass').first().children('div').attr('childcount') == 0)
                        var hashBang = $(currFileParent).next().children('div .anchorclass').first().attr('hashBang');
                    else if ($(currFile).children('div').attr('childcount') == 0 && $(currFile).next('.anchorclass').children('div').attr('childcount') == 1)
                        var hashBang = $(currFile).next().children().find('.secondlevelload').first().attr('hashbang');
                }
                else if ($(subFile).next('.secondlevelload').length == 1)
                    var hashBang = $(subFile).next().attr('hashbang');
                else if ($(subFileParent).next('.anchorclass').children('div').attr('childcount') == 0)
                    var hashBang = $(subFileParent).next().attr('hashbang');
                else if ($(subFile).next('.secondlevelload').length == 0 && $(subFileParent).next().children('div').attr('childcount') == 1)
                    var hashBang = $(subFileParent).next().children().find('.secondlevelload').first().attr('hashbang');
                else if ($(subFile).next('.secondlevelload').length == 0 && $(subFileParent).next('.anchorclass').length == 0)
                    var hashBang = $(subFileParent).parent('div').next().children('.anchorclass').first().attr('hashbang');
                queryChange(hashBang);
            });
        };


        self.loadSBMainPage = function (divid) {
            $(".samplesection").hide();
            if (divid != null) {
                $("#" + divid).css("visibility", "hidden");
                $("#" + divid + "_back").css("display", "none");
            }
            self.loadSBPage(divid);
            $(".accordion-panel").height($(".accordion-panel")[0].scrollHeight);
        };
        self.loadSBPage = function (divid) {
            $("#accordion2").prev().css("display", "block");
            $("#accordion2>a>.dashboardhover").removeClass("dashboardhover");
            $(".sourcecodetab").hide();
            if (divid != null)
                $("#" + divid).hide();
            $("#accordion2").show();
            $("#samplesDiv").css("left", "250px");
            $("#sbdashboard").addClass("highlighttextbg itemselected");
            $("#accordion2").animate({ left: 0 }, 200, function () {
                self.setVisibility("cols-iframe", "productpage");
            });
            $(".accordion-panel").height($(".content-container-fluid").height() - ($(".htmljssamplebrowser>.header").height() + 5));
            $('html, body').animate({
                scrollTop: 0
            }, 0);
            $(".accordion-panel").height($(".accordion-panel")[0].scruollHeight);
        };
        self.loadSourceCodeTab = function (url) {
            $.ajax({
                url: url,
                dataType: "html",
                success: function (data) {
                    var content = data;
                    $("#srccodearea").val('');
                    $(".sourcecodetab").show();
                    $("#sourceTab").ejAccordion({ cssClass: "metroblue" }).show();
                    var htmlEditor = CodeMirror.fromTextArea($("#srccodearea").val(content)[0], {
                        lineNumbers: false,
                        mode: "text/html"
                    });
					if(window.themevarient.indexOf("dark") != -1)
                       replacebg(true);
                    $(".CodeMirror").each(function (i, obj) {
                        if (i > 0)
                            $(obj).remove();
                    });
                    $("#sourceTab .CodeMirror").find('textarea').attr('readonly', 'readonly');

                }
            });
        };
        self.setVisibility = function (oldpage, newpage) {
            $("." + newpage).show();
            $("." + oldpage).hide();
        };
        this.getCssClass = function (item) {
            var value = Number(item.childcount);

            if (value >= 1) {
                return 'arrow';
            }
            return;
        };
        this.getCssVisibility = function (item) {
            var value = Number(item.id);

            if (value != 1) {
                return 'hideParent';
            }
            return 'dashboard';
        };
        self.removeSelectedItemcss = function (ctrlname, controlid) {
            var obj = $(".itemselected");
            obj.each(function () {
                $(obj).removeClass('itemselected');
                $(obj).parent().removeClass('highlighttextbg');
            });
        };
    }

    var samplesdetails = SamplesList;
    $(samplesdetails).each(function (index1, sampleslist) {
        if (sampleslist) {
            var smpls = {}, secsamples = [];
            smpls["id"] = sampleslist.id;
            smpls["name"] = sampleslist.name;
            smpls["type"] = sampleslist.type;
            smpls["url"] = SamplesList[index1]["url"] = sampleslist.querystring.split(" ").join("");
            $(sampleslist.samples).each(function (ind, subsampleslist) {
                if (subsampleslist) {
                    var subsmpls = {};
                    subsmpls["id"] = subsampleslist.id;
                    subsmpls["name"] = subsampleslist.name;
                    subsmpls["querystring"] = subsampleslist.querystring;
                    subsmpls["childcount"] = subsampleslist.childcount;
                    subsmpls["type"] = subsampleslist.type;
                    subsmpls["url"] = SamplesList[index1].samples[ind]["url"] = subsampleslist.name.split(" ").join("");

                    if (subsampleslist.childcount == 1)
                        subsmpls["arrowclass"] = "arrow";
                    else
                        subsmpls["arrowclass"] = "";

                    $(subsampleslist.samples).each(function (j, thirdlist) {
                        if (thirdlist)
                            SamplesList[index1].samples[ind].samples[j]["url"] = thirdlist.name.split(" ").join("");
                    });
                    subsmpls["samples"] = subsampleslist.samples;
                    secsamples.push(subsmpls);
                }
            });
            smpls["samples"] = secsamples;
            firstlevelsamples.push(smpls);
        }
    });

    $("#accordion2").html($("#accordionTmpl").render(SamplesList));
    $("#samplesDiv").html($("#accordionTmplchild").render(firstlevelsamples));

    $('div.firstlevelback').bind('click', function (evt, args) {
        editItem(evt.currentTarget.id, 'back');
    });

    $(".dashboard").mouseover(function () {
        if (!$(".vHandle").is(":visible")) {
            if (window.themevarient.indexOf("dark") != -1 && !$(this).hasClass("dark-highlighttextbg"))
                $(this).addClass("dark-dashboardhover");
            else
                $(this).addClass("dashboardhover");
        }
    });
    $(".dashboard").click(function () {
        $(this).find(".anchor").addClass("itemselected");
        $(".dark-dashboard").hasClass("dark-highlighttextbg");
        $(".dark-dashboard").removeClass("dark-highlighttextbg");
        $(this).find(".itemselected").parent().addClass("highlighttextbg");

    });
    $(".dashboard").mouseout(function () {
        if ($(this).hasClass("dashboardhover"))
            $(this).removeClass("dashboardhover");
        else if ($(this).hasClass("dark-dashboardhover"))
            $(this).removeClass("dark-dashboardhover");
    });
    var collection = SamplesList;
    var subdata = [], i, j, k;
    for (i = 0; i < collection.length; i++) {
        if (!collection[i]) continue;
        for (j = 0; j < collection[i].samples.length; j++) {
            if (!collection[i].samples[j]) continue;
            var properties = {}, len;
            var controlName = collection[i].id;
            properties["id"] = controlName + "/" + collection[i].samples[j].url;
            properties["text"] = collection[i].name + "/" + collection[i].samples[j].name;
            if (collection[i].samples[j].childcount != "0") {
                if (/msie 8.0/.test(navigator.userAgent.toLowerCase()))
                    len = collection[i].samples[j].samples.length - 1;
                else
                    len = collection[i].samples[j].samples.length;
                if (collection[i].samples[j].name == "Selection")
                    var s = true;
                for (k = 0; k < len; k++) {
                    if (!collection[i].samples[j].samples[k]) continue;
                    var subprops = {};
                    var subcontrolName = collection[i].samples[j].url;
                    subprops["id"] = controlName + "/" + subcontrolName + "/" + collection[i].samples[j].samples[k].url;
                    subprops["text"] = collection[i].name + "/" + collection[i].samples[j].name + "/" + collection[i].samples[j].samples[k].name;
                    subdata.push(subprops);
                }
            }
            subdata.push(properties);
        }
    }
    $('#auto').ejAutocomplete({
        watermarkText: "Search",
        showPopupButton: false,
        filterType: "Contains",
        dataSource: subdata,
        fields: { key: "id", text: "text" },
        width: "183px",
        select: "onSelectSearchItem"
    });
    $('.navigation').bind('click', function () {
        var move, proxy = this;
        if ($(this).hasClass("expandPanel")) {
            $('.navigation').removeClass("expandPanel").addClass("collapsePanel"), move = -$('.accordion-panel').outerWidth();
            $('.control-panel.cols-content-fluid').addClass('center-flow');
            $('content-container-fluid > row > .navigation').addClass("expandPanel");
        }
        else {
            $(proxy).removeClass("expandPanel");
            $('.accordion-panel').removeClass('accordionHide').addClass("expandPanel");
            $('.control-panel.cols-content-fluid').removeClass('accordionHide').removeClass('center-flow');
            $('.accordion-panel > .navigation').addClass("expandPanel");
            $('.navigation').removeClass("collapsePanel"), move = 0;
            if (parseInt($('.accordion-panel').css('left')) >= 0) {
                $('.accordion-panel').css('left', '-' + $('.accordion-panel').outerWidth() + 'px');
            }
            $('.accordion-panel .navigation').addClass("expandPanel")
        }
        $('.accordion-panel').animate({
            "left": move + "px",
        }, 500, function () {
            if ($('.navigation').hasClass("collapsePanel")) {
                $('.accordion-panel').addClass('accordionHide');
                $('.navigation').show();

            }
            if ($(proxy).hasClass("expandPanel")) {
                $('.accordion-panel').addClass("expandPanel");
            }
        });
    });
    $(window).bind("resize", function () {
        var viewportWidth = $(window).width();
        if (viewportWidth < 981) {
            $('.content-container-fluid .row > .navigation').addClass('collapsePanel');
            $('.accordion-panel').removeClass("expandPanel");
        }
    });
    if (Path.match(window.location.hash)) {
        $("#sbtooltipbox").ejDialog("close");
        isloadLeft = true;
        // $(".cols-iframe").show();
        showTooltipbox();
    }
    //var heightval = (window.innerHeight || $(window).height()) - ($(".header").outerHeight() + 5 + $(".search").outerHeight());
    //if(heightval<minScrollerHeight)
    //    heightval = minScrollerHeight;
    //else
    //    heightval = heightval - $("#footer").outerHeight();
    //var scroller = $("#scrollcontainer").width(253).data("ejScroller");
    //scroller.setModel({ height: heightval, width: 0, cssClass: "metroblue" });
    //scroller.refresh();

    $(document).click(function (evt) {
        if ($("#sbtooltipbox").ejDialog("isOpened"))
            $("#sbtooltipbox").ejDialog("close");
        var ele = false;
        var chromebrowser = /chrome/.test(navigator.userAgent.toLowerCase());
        var safaribrowser = /safari/.test(navigator.userAgent.toLowerCase());
        if (chromebrowser || safaribrowser)
		if (evt.target.parentElement.parentElement != null)
            if (evt.target && evt.target.parentElement.parentElement.id != null && evt.target.parentElement.parentElement.id == "themebutton")
                ele = true;
        if (!ele && $(evt.target).parents("#themeDialog_wrapper").length <= 0 && evt.target.id != "themebutton") {
            if ($("#themeDialog").ejDialog("isOpened"))
                $("#themeDialog").ejDialog("close");
        }
    });
    $(".editcode").click(function () {
        $("#sbeditcodedialog").ejDialog(
                {
                    enableModal: true,
                    showOnInit: false,
                    allowDraggable: false,
                    width: "98%",
                    height: "95%",
                    cssClass: "metroblue",
                    enableResize: false,
                    content: "iframe",
                    contentUrl: "editcode.html?" + editcontrolpath,
                    title: "LIVE EDIT",
                    close: "onClose"
                });
        $("#sbeditcodedialog").show();
        $("#sbeditcodedialog").ejDialog("open");
        $('html, body').animate({
            scrollTop: 0
        }, 0);
        $("#sbeditcodedialog iframe").bind("load", function () {
            $('.liveeditctrls').insertAfter($("#sbeditcodedialog_wrapper .e-dialog-title")).addClass("showliveeditctrls");
            $("#sbeditcodedialog_wrapper .liveeditctrls #cssarea").ejCheckBox({ cssClass: "metroblue", "change": "oncssareaShowHide" });
            $("#sbeditcodedialog .e-iframe").contents().find("#apply").insertAfter($('.liveeditctrls'));
        });
    });
    $(".circlebaseouter").mouseover(function (evt) {
        if ($(".circlebaseouter").parent().hasClass("e-disable")) return false;
        $(".hoverselcolor").removeClass("hoverselcolor");
        $(this).addClass("hoverselcolor");
    });
    $(".circlebaseouter").mouseout(function (evt) {
        if ($(".circlebaseouter").parent().hasClass("e-disable")) return false;
        $(".hoverselcolor").removeClass("hoverselcolor");
    });
    $(".circlebaseouter .azure,.circlebaseouter .lime,.circlebaseouter .saffron").click(function (evt) {
        if ($(".circlebaseouter").parent().hasClass("e-disable")) return false;
        $(".colorsel").removeClass("colorsel");
        $(this).parent().addClass("colorsel");
        var selcolor = $(this).parent().attr("id");
        window.themecolor = selcolor.replace("theme", "");
        updateTheme();
    });
    //var mouseHover = function () {
    //    $("#scrollcontainer").find(".vScroll").show().addClass("scrollhandlehover");
    //    $("#scrollcontainer").find(".vScroll").prev().addClass("scrollercontainer-content");
    //};

    //var mouseOut = function () {
    //    $("#scrollcontainer").find(".vScroll").hide().removeClass("scrollhandlehover");
    //    $("#scrollcontainer").find(".vScroll").prev().removeClass("scrollercontainer-content");
    //    $("#scrollcontainer").find(".content").addClass("scrollercontainer-content");
    //};

    //$("#scrollcontainer").bind("mouseover", mouseHover);
    //$("#scrollcontainer").bind("mouseout", mouseOut);
    //$("#scrollcontainer").on("mousedown", ".vHandle", function () {
    //    $("#scrollcontainer").unbind("mouseout", mouseOut);

    //    $(document).one("mouseup", function () {
    //        $("#scrollcontainer").bind("mouseout", mouseOut);
    //        $("#scrollcontainer").find(".vScroll").hide();
    //    });
    //});
    Path.listen();

    var QueryString = function () {
        // This function is anonymous, is executed immediately and 
        // the return value is assigned to QueryString!
        var query_string = {};
        var query = window.location.search.substring(1);
        var vars = query.split("&");
        for (var i = 0; i < vars.length; i++) {
            var pair = vars[i].split("=");
            // If first entry with this name
            if (typeof query_string[pair[0]] === "undefined") {
                query_string[pair[0]] = pair[1];
                // If second entry with this name
            } else if (typeof query_string[pair[0]] === "string") {
                var arr = [query_string[pair[0]], pair[1]];
                query_string[pair[0]] = arr;
                // If third or later entry with this name
            } else {
                query_string[pair[0]].push(pair[1]);
            }
        }
        return query_string;
    }();
});
function oncssareaShowHide(args) {
    if (args.isChecked) {
        if ($($("#ejcssarea .e-innerspan")[1]).hasClass("e-chk-inactive"))
            $($("#ejcssarea .e-innerspan")[1]).addClass("e-chk-active")

        $($("#sbeditcodedialog .e-iframe").contents().find("#spliter2 .v-pane")[1]).show();
        $("#sbeditcodedialog .e-iframe").contents().find("#spliter2 .v-bar").show();
        var cssheight = $($("#sbeditcodedialog .e-iframe").contents().find("#spliter1 .v-pane")[0]).css("height");
        $($("#sbeditcodedialog .e-iframe").contents().find("#spliter2 .v-pane")[0]).css("height", cssheight);
    }
    else {
        $($("#sbeditcodedialog .e-iframe").contents().find("#spliter2 .v-pane")[1]).hide();
        $("#sbeditcodedialog .e-iframe").contents().find("#spliter2 .v-bar").hide();
        cssheight = $($("#sbeditcodedialog .e-iframe").contents().find("#spliter2 .v-pane")[0]).css("height");
        $($("#sbeditcodedialog .e-iframe").contents().find("#spliter2 .v-pane")[0]).css("height", "100%");
    }
}
function refreshIFrameTheme() {
    autoResize("samplefile");
    if (currentSamplepage != undefined && !$("#sbdashboard").is(":visible"))
		self.loadSourceCodeTab(currentSamplepage);
    setTimeout(function () {
        if ($(".control-panel").data("ejWaitingPopup"))
            $(".control-panel").ejWaitingPopup("hide");
    }, 100);
    setTimeout(function () {
        $(".accordion-panel").height($(".content-container-fluid").height() - ($(".htmljssamplebrowser>.header").height() + 6));
    }, 1000);
    $('#samplefile').css('visibility', 'visible');
}
function onClose(args) {

}
function updateHeight() {
    $(".cols-iframe").show();
    $(".accordion-panel").height($(".content-container-fluid").height() - ($(".htmljssamplebrowser>.header").height() + 5));
    $("#footer").show();
    //$("#footer").css("top", ($(".controlpage").height() + $(".controlpage").offset().top) + "px");
    //$(".accordion-panel").height($(".controlpage").height() + $(".controlpage").offset().top);
}


function preparetheme() {
    var themename = "";
    if (window.location.hash.indexOf('dark') != -1 && window.themevarient != 'light')
        window.themevarient = "dark";
    if (window.location.hash.indexOf('gradient') != -1 && window.themestyle != 'flat')
        window.themestyle = "gradient";
    if (window.themevarient == "dark") {
        if (window.themestyle == "gradient")
            themename = window.themestyle + window.themecolor + window.themevarient;
        else
            themename = (window.themecolor != "") ? window.themecolor + window.themevarient : window.themestyle + window.themevarient;

    }
    else {
        if (window.themestyle == "gradient")
            themename = window.themestyle + window.themecolor;
        else
            themename = (window.themecolor != "") ? window.themecolor : window.themestyle;
    }
     if (window.theme.indexOf("bootstrap") != -1) {
        themename = window.theme;
        window.themestyle = "flat";
        window.themevarient = "light";
    }
    window.theme = themename;
}

function updateTheme(selcolor) {
    preparetheme();
    replacebg(window.themevarient == "dark");
    replaceTheme();
}
var themes = {
    "flat": "themes/default-theme/ej.widgets.all.min.css",
    "flatdark": "themes/flat-azure-dark/ej.widgets.all.min.css",
    "azure": "themes/default-theme/ej.widgets.all.min.css",
    "azuredark": "themes/flat-azure-dark/ej.widgets.all.min.css",
    "lime": "themes/flat-lime/ej.widgets.all.min.css",
    "limedark": "themes/flat-lime-dark/ej.widgets.all.min.css",
    "saffron": "themes/flat-saffron/ej.widgets.all.min.css",
    "saffrondark": "themes/flat-saffron-dark/ej.widgets.all.min.css",
    "gradient": "themes/gradient-azure/ej.widgets.all.min.css",
    "gradientdark": "themes/gradient-azure-dark/ej.widgets.all.min.css",
    "gradientazure": "themes/gradient-azure/ej.widgets.all.min.css",
    "gradientazuredark": "themes/gradient-azure-dark/ej.widgets.all.min.css",
    "gradientlime": "themes/gradient-lime/ej.widgets.all.min.css",
    "gradientlimedark": "themes/gradient-lime-dark/ej.widgets.all.min.css",
    "gradientsaffron": "themes/gradient-saffron/ej.widgets.all.min.css",
    "gradientsaffrondark": "themes/gradient-saffron-dark/ej.widgets.all.min.css",
    "bootstrap": "themes/bootstrap-theme/ej.theme.min.css"
};
function updateLinkTag() {
    var links = $(document.head || document.getElementsByTagName('head')[0]).find("link");
    for (var i = 0; i < links.length; i++) {
        if (links[i].href.indexOf("ej.widgets.all.min.css") != -1)
            links[i].href = themes[theme];
    }
}
function replaceTheme() {
    if ((window.theme.indexOf("lime") != -1) || (window.location.hash.indexOf('lime') != -1 && window.themecolor != 'azure' && window.themecolor != "saffron"))
        window.themecolor = 'lime';
    else if ((window.theme.indexOf('saffron') != -1) || (window.location.hash.indexOf('saffron') != -1 && window.themecolor != 'azure' && window.themecolor != 'lime'))
        window.themecolor = 'saffron';
    else
        window.themecolor = "azure";
    var selcolor = (window.themecolor == "") ? "azure" : window.themecolor;
    $("#themebutton").ejButton('option', 'prefixIcon', 'e-icons ' + selcolor);
    $("#themebutton .e-uiLight").addClass(selcolor);
    $(".htmljssamplebrowser").attr("class", "htmljssamplebrowser " + selcolor);

    updateLinkTag();
    if (window.currentSamplepage) {
        var querystring = "";
        if (window.theme != "flat") {
            querystring = "?theme=" + window.theme;
        }
        $("#samplefile").attr('src', currentSamplepage + querystring);
		$("#newsamplewindow").attr('href',currentSamplepage);
    }
    if (window.location.hash === "")
        window.location.hash = "#!/" + window.theme;
    window.location.hash = window.location.hash.replace(/(#!\/[^\/]+)/, "!/" + window.theme);
}
function replacebg(enable) {
    if (enable)
        $("body").addClass("darktheme");
    else
        $("body").removeClass("darktheme");
}
function onSelectSearchItem(args) {
    var samples = (args.key).split("/");
    var url = "#!/" + window.theme + "/" + args.key;
    $(".samples").hide();
    self.loadLeftTab(samples[0], samples[1]);
    window.location.hash = url;
}
function themebtnClick(args) {
    showThemeDialog();
}
function showTooltipbox() {
    var $btnelement = $("#themebutton");
    var pos = $btnelement.offset();
    var left = $("#sbtooltipbox_wrapper").width() + pos.left;
    if (left > $(".samplecontainerparent").width())
        left = (pos.left + ($btnelement.width() / 2)) - $("#sbtooltipbox_wrapper").width();
    else
        left = pos.left;
    $("#sbtooltipbox").ejDialog("option", "position", { X: left + 10 });
    $("#sbtooltipbox").ejDialog("open");
}
function showThemeDialog() {
    var $btnelement = $("#themebutton");
    var pos = $btnelement.offset();
    var left = $("#themeDialog_wrapper").width() + pos.left;
    if (left > $(".samplecontainerparent").width())
        left = (pos.left + $btnelement.width()) - $("#themeDialog_wrapper").width();
    else
        left = pos.left;
    $("#themeDialog").ejDialog("option", "position", { X: left });
    $("#themeDialog").ejDialog("open");
    //$(".metroclstxt").appendTo($("#ejmetrotext"));
    //$(".graclstxt").appendTo($("#ejgradienttext"));
    //$(".liteclstxt").appendTo($("#ejlighttext"));
    //$(".darkclstxt").appendTo($("#ejdarktext"));
}
function themeonchange(args) {
if ($("." + args.model.name).hasClass("e-disable")) return false;
    switch (args.model.name) {
        case "themestyle":
            window.themestyle = args.model.value;
            break;
        case "themevarient":
            window.themevarient = args.model.value;
            break;
    }
    updateTheme();
    radioButtonSelect();
}
function bootstraponselect(args) {
    if (args.isChecked) {
        enableBootstrap(true);
        window.theme = "bootstrap";		
        updateTheme(window.theme);
    }
    else {
        enableBootstrap(false);
        window.theme = "azure";       
        window.themecolor = "azure";
        updateTheme(window.theme);
        themeButtonSelect();
    }
}
function enableBootstrap(enable) {
    if (enable) {
        $(".themecolors").addClass("e-disable");
        $(".themevarient").addClass("e-disable");
        $(".themestyle").addClass("e-disable");
    }
    else {
        $(".themecolors").removeClass("e-disable");
        $(".themevarient").removeClass("e-disable");
        $(".themestyle").removeClass("e-disable");
    }
    $("#lighttext").ejRadioButton("option", "enabled", !enable);
    $("#darktext").ejRadioButton("option", "enabled", !enable);
    $("#metrotext").ejRadioButton("option", "enabled", !enable);
    $("#gradienttext").ejRadioButton("option", "enabled", !enable);


}
function radioButtonSelect() {
    $('#ejmetrotext,#ejgradienttext,#ejlighttext,#ejdarktext').children('span').children('span').removeClass('e-rad-active');
    if (window.location.hash.indexOf("gradient") != -1) {
        $('#ejgradienttext').children('span').children('span').addClass('e-rad-active');
        window.themestyle = 'gradient';
    }
    else if (window.location.hash.indexOf("dark") != 1 && window.location.hash.indexOf("light") != 1) {
        $('#ejmetrotext').children('span').children('span').addClass('e-rad-active');
        window.themestyle = 'flat';
    }
    if (window.location.hash.indexOf("dark") != -1) {
        $('#ejdarktext').children('span').children('span').addClass('e-rad-active');
        window.themevarient = 'dark';
    }
    else if (window.location.hash.indexOf("gradient") != 1 && window.location.hash.indexOf("flat") != 1) {
        $('#ejlighttext').children('span').children('span').addClass('e-rad-active');
        window.themevarient = 'light';
    }
    if ((window.location.hash.indexOf("bootstrap") != -1)) {
        $("#bootstrapcheck").ejCheckBox("option", "checked", true);
        enableBootstrap(true);
    }

}
function themeButtonSelect() {
    $('#themesaffron,#themeazure,#themelime').removeClass('colorsel');
    if ((window.theme.indexOf("lime") != -1) || (window.location.hash.indexOf('lime') != -1)) {
        $('#themelime').addClass('colorsel');
        window.themecolor = 'lime';
    }
    else if ((window.theme.indexOf("saffron") != -1) || (window.location.hash.indexOf('saffron') != -1)) {
        $('#themesaffron').addClass('colorsel');
        window.themecolor = 'saffron';
    }
    else {
        $('#themeazure').addClass('colorsel');
        window.themecolor = 'azure';
    }
    radioButtonSelect();
    theme = (themestyle == "flat" ? "" : themestyle) + themecolor + (themevarient == "light" ? "" : themevarient);
    updateLinkTag();
}
function queryChange(hashBang) {
    if (hashBang != null)
        window.location.hash = hashBang.replace("flat", window.theme);
}
function adjustpopupposition(args) {
    var offset = $("#selectControls_dropdown").offset();
    var height = $("#selectControls_wrapper").height();
    $("#selectControls_popup").css("top", (offset.top + height + 14) + "px");
    var left = $("#selectControls_popup").width() + offset.left;
    if (left > $(".content-container-fluid").width())
        left = (offset.left + $("#selectControls_dropdown").width()) - $("#selectControls_popup").width() - 12;
    $("#selectControls_popup").css("left", left + "px");
}
function viewdemo(product) {
    product = product.toLowerCase();
    if (location.protocol == "file:" || location.port == "") {
        if (product == "web") return; else window.location.href = window.mobileJsUrl;
    }
    else {
        var text = 'StartDevServer.ashx', prot = window.location.protocol, hostname = window.location.host;
        if (product == "web") return; else product = "mobile";
        $.ajax({
            url: prot + "//" + hostname + "/" + text + "?product=" + product,
            success: function (data) {
                window.location = data;
            }
        });
    }
}
function autoResize(id) {
    var newheight;
    var newwidth;

    if (document.getElementById) {
        newheight = document.getElementById(id).contentWindow.document.body.scrollHeight;
        newwidth = document.getElementById(id).contentWindow.document.body.scrollWidth;
    }

    $("#" + id).height(newheight + 20);
}



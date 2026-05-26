<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default-Security.aspx.cs" Inherits="Default_Security" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="System.Linq" %>
<%@ Register src="UserNameProfile.ascx" tagname="UserNameProfile" tagprefix="us1" %>
<%
    
    string manv = Context.User.Identity.Name;
    String selAdmin = "select isadmin, suaGia, doiNCU from nhanvien where manv = @manv";
    var dtAdmin = mdbc.GetData(selAdmin, "@manv", manv);
    bool isadmin = (dtAdmin.Rows[0]["isadmin"] + "").ToLower() == "true";
    bool suaGiaPO = (dtAdmin.Rows[0]["suaGia"] + "").ToLower() == "true";
    bool doiNCU = (dtAdmin.Rows[0]["doiNCU"] + "").ToLower() == "true";
    bool? xemgiaHH, xemgiaHHST, xemgiaHHNHD, xemgiaHHDQ, xemgiaDSDH, xemgiaNoiHH;
    xemgiaHH = xemgiaHHST = xemgiaHHNHD = xemgiaHHDQ =  xemgiaDSDH = xemgiaNoiHH = false;

    bool? chietxuatHH, chietxuatHHST, chietxuatHHNHD, chietxuatHHDQ, chietxuatLHH, chietxuatDGSP, chietxuatDGST, chietxuatHSCode, chietxuatNNL, chietxuatKDSP, chietxuatCNSP, chietxuatDeTai, chietxuatMauSac;
    chietxuatHH = chietxuatHHST = chietxuatHHNHD = chietxuatHHDQ = chietxuatLHH = chietxuatDGSP = chietxuatDGST = chietxuatHSCode = chietxuatNNL = chietxuatKDSP = chietxuatCNSP = chietxuatDeTai = chietxuatMauSac = false;

    String sql = "select * from menu m, phanquyen pq, vaitro vt, nhanvien nv " +
        " WHERE m.hoatdong = 1 AND nv.hoatdong = 1 " +
        " AND pq.hoatdong = 1 AND vt.hoatdong = 1 " +
        " AND m.mamenu = pq.mamenu " +
        " AND pq.mavt = vt.mavt " +
        " AND vt.mavt = nv.mavt " +
        " AND nv.manv = @manv " +
        " ORDER BY m.sortorder asc, m.ngaytao desc";

    String sqlAdmin = "select * from menu WHERE hoatdong = 1 ORDER BY sortorder asc, ngaytao desc";


    System.Data.DataTable dt = new System.Data.DataTable();

    bool PTSP, NGHVU, KETOAN, BAOCAO, HETHONG;
    PTSP = NGHVU = KETOAN = BAOCAO = HETHONG = false;

    if (isadmin)
    {
        dt = mdbc.GetData(sqlAdmin);
        PTSP = NGHVU = KETOAN = BAOCAO = HETHONG = true;
        xemgiaHH = xemgiaNoiHH = xemgiaHHST = xemgiaHHNHD = xemgiaHHDQ = xemgiaDSDH = true;
        chietxuatHH = chietxuatHHST = chietxuatHHNHD =
        chietxuatHHDQ = chietxuatLHH = chietxuatDGSP =
        chietxuatDGST = chietxuatHSCode = chietxuatNNL =
        chietxuatKDSP = chietxuatCNSP = chietxuatDeTai =
        chietxuatMauSac = true;
    }
    else
    {
        dt = mdbc.GetData(sql, "@manv", manv);

        #region XemGia
        var xemgiaFld = (from myRow in dt.AsEnumerable()
                         where myRow.Field<string>("mamenu") == "c8a12cddaf85c128673b499264846938"
                         select new {
                             gia = myRow.Field<bool>("gia"),
                             vaitro = myRow.Field<string>("tenvt")
                         }).FirstOrDefault();

        if(xemgiaFld != null) {
            if (xemgiaFld.gia == true)
                xemgiaHH = true;
            if (xemgiaFld.vaitro != "Nghiệp Vụ")
                xemgiaNoiHH = true;
        }

        xemgiaHHST = (from myRow in dt.AsEnumerable()
                      where myRow.Field<string>("mamenu") == "68ff4867b9727e6f4bb055821fe1fa75"
                      select myRow.Field<bool>("gia")).FirstOrDefault();

        xemgiaHHNHD = (from myRow in dt.AsEnumerable()
                       where myRow.Field<string>("mamenu") == "43a087d627cbdc5272b4484044dc799d"
                       select myRow.Field<bool>("gia")).FirstOrDefault();

        xemgiaDSDH = (from myRow in dt.AsEnumerable()
                      where myRow.Field<string>("url") == "inc-ds-dathang.aspx"
                      select myRow.Field<bool>("gia")).FirstOrDefault();
        #endregion XemGia

        #region ChietXuat
        chietxuatHH = (from myRow in dt.AsEnumerable()
                       where myRow.Field<string>("url") == "inc-hanghoa.aspx"
                       select myRow.Field<bool>("chietxuat")).FirstOrDefault();

        chietxuatHHST = (from myRow in dt.AsEnumerable()
                         where myRow.Field<string>("url") == "inc-hanghoasoanthao.aspx"
                         select myRow.Field<bool>("chietxuat")).FirstOrDefault();

        chietxuatHHNHD = (from myRow in dt.AsEnumerable()
                          where myRow.Field<string>("url") == "inc-hanghoa-nhd.aspx"
                          select myRow.Field<bool>("chietxuat")).FirstOrDefault();

        chietxuatHHDQ = (from myRow in dt.AsEnumerable()
                         where myRow.Field<string>("url") == "inc-hanghoa-docquyen.aspx"
                         select myRow.Field<bool>("chietxuat")).FirstOrDefault();

        chietxuatLHH = (from myRow in dt.AsEnumerable()
                        where myRow.Field<string>("url") == "inc-loaihanghoa.aspx"
                        select myRow.Field<bool>("chietxuat")).FirstOrDefault();

        chietxuatDGSP = (from myRow in dt.AsEnumerable()
                         where myRow.Field<string>("url") == "inc-donggoi-sanpham.aspx"
                         select myRow.Field<bool>("chietxuat")).FirstOrDefault();

        chietxuatDGST = (from myRow in dt.AsEnumerable()
                         where myRow.Field<string>("url") == "inc-donggoi-sanpham-soanthao.aspx"
                         select myRow.Field<bool>("chietxuat")).FirstOrDefault();

        chietxuatHSCode = (from myRow in dt.AsEnumerable()
                           where myRow.Field<string>("url") == "inc-hscode.aspx"
                           select myRow.Field<bool>("chietxuat")).FirstOrDefault();

        chietxuatNNL = (from myRow in dt.AsEnumerable()
                        where myRow.Field<string>("url") == "inc-nhomnangluc.aspx"
                        select myRow.Field<bool>("chietxuat")).FirstOrDefault();

        chietxuatKDSP = (from myRow in dt.AsEnumerable()
                         where myRow.Field<string>("url") == "inc-kieudangsanpham.aspx"
                         select myRow.Field<bool>("chietxuat")).FirstOrDefault();

        chietxuatCNSP = (from myRow in dt.AsEnumerable()
                         where myRow.Field<string>("url") == "inc-chucnangsanpham.aspx"
                         select myRow.Field<bool>("chietxuat")).FirstOrDefault();

        chietxuatDeTai = (from myRow in dt.AsEnumerable()
                          where myRow.Field<string>("url") == "inc-detai.aspx"
                          select myRow.Field<bool>("chietxuat")).FirstOrDefault();

        chietxuatMauSac = (from myRow in dt.AsEnumerable()
                           where myRow.Field<string>("url") == "inc-mausac.aspx"
                           select myRow.Field<bool>("chietxuat")).FirstOrDefault();
        #endregion ChietXuat

        foreach (System.Data.DataRow row in dt.Rows)
        {
            bool truyxuat = bool.Parse(row["truyxuat"].ToString());
            if (row["module_id"].Equals("PTSP") && truyxuat)
            {
                PTSP = true;
            }

            if (row["module_id"].Equals("NGHVU") && truyxuat)
            {
                NGHVU = true;
            }

            if (row["module_id"].Equals("KETOAN") && truyxuat)
            {
                KETOAN = true;
            }

            if (row["module_id"].Equals("BAOCAO") && truyxuat)
            {
                BAOCAO = true;
            }

            if (row["module_id"].Equals("HETHONG") && truyxuat)
            {
                HETHONG = true;
            }
        }
    }
%>
<html>
<head>
<title>Vinh Gia Emanagement 3.0</title>

<!-- jQuery Core -->
<script src="jQuery/Core/jquery-1.7.1.min.js" type="text/javascript"></script>
<!--/ jQuery Core -->

<!-- jQuery UI -->
<script src="jQuery/UI/js/jquery-ui-1.8.18.custom.min.js"
	type="text/javascript"></script>
<link rel="stylesheet" type="text/css" media="screen"
	href="jQuery/UI/css/humanity/jquery-ui-1.8.21.custom.css" />
	
<!--/ jQuery UI -->

<!-- jQuery LightBox -->
<link rel="stylesheet" href="jQuery/LightBox/css/prettyPhoto.css" type="text/css" media="screen" title="prettyPhoto main stylesheet" charset="utf-8" />
<script src="jQuery/LightBox/js/jquery.prettyPhoto.js" type="text/javascript" charset="utf-8"></script>
<!--/ jQuery LightBox -->

<!--/ Mutil Selectboxes -->
<script type="text/javascript" src="jQuery/MultiSelectboxes/js/plugins/localisation/jquery.localisation-min.js"></script>
<script type="text/javascript" src="jQuery/MultiSelectboxes/js/plugins/scrollTo/jquery.scrollTo-min.js"></script>
<script type="text/javascript" src="jQuery/MultiSelectboxes/js/ui.multiselect.js"></script>
<link type="text/css" href="jQuery/MultiSelectboxes/css/ui.multiselect.css" rel="stylesheet" />
<!--/ Mutil Selectboxes -->

<!-- jQuery Grid -->
<script src="jQuery/jQGrid/js/i18n/grid.locale-vn.js" type="text/javascript"></script>
<script src="jQuery/jQGrid/js/jquery.jqGrid.min.4.3.3.js" type="text/javascript"></script>
<!--<script src="jQuery/jQGrid/js/jquery.jqGrid.src.js" type="text/javascript"></script>-->
<link rel="stylesheet" type="text/css" media="screen" href="jQuery/jQGrid/css/ui.jqgrid.css" />

<script src="jQuery/jQGrid/common/jquery.jqgrid.common.js" type="text/javascript"></script>
<!--/ jQuery Grid -->

<!-- Combo grid -->
<link rel="stylesheet" type="text/css" media="screen" href="jQuery/ComboGrid/css/jquery.ui.combogrid.css"/>
<script type="text/javascript" src="jQuery/ComboGrid/plugin/jquery.ui.combogrid-1.6.2.js"></script>
<!--/ Combo grid -->

<!-- layout -->
<script src="jQuery/Layout/jquery.layout.1.3.0.js" type="text/javascript"></script>
<link rel="stylesheet" type="text/css" media="screen" href="jQuery/Layout/layout-default-latest.css" />
<!--/ layout -->

<!-- Metro Type -->
<script type="text/javascript" src="jQuery/MetroType/jquery.metro.js"></script>
<link type="text/css" rel="Stylesheet" href='jQuery/MetroType/jquery.metro.css' />
<!--/ Metro Type -->

<!-- ScrollerMenu -->
<script type="text/javascript" src="jQuery/ScrollerMenu/sc-menu.js"></script>
<link type="text/css" rel="Stylesheet" href='jQuery/ScrollerMenu/sc_menu.css' />
<!--/ ScrollerMenu -->



<script src="jQuery/jquery.maskedinput-1.3.min.js" type="text/javascript"></script>
<script src="jQuery/jquery.form.js" type="text/javascript"></script>
<script src="jQuery/jquery.signalR-2.2.0.min.js" type="text/javascript"></script>

<!-- theme -->
<link rel="stylesheet" type="text/css" media="screen" href="theme/silver/css/style.css" />
<!--/ theme -->

<!-- icon collection -->
<link rel="stylesheet" type="text/css" media="screen" href="iconcollection/icon-style.css" />
<!--/ icon collection -->

<!-- validation -->
<script type="text/javascript" src="jQuery/Validation/jquery.validate.min.js"></script>
<!-- / validation -->



<!-- Upload File -->
<link rel="stylesheet" href="jQuery/FileUpload/jquery.ui.plupload/css/jquery.ui.plupload.css" />

<!-- Load plupload and all it's runtimes and finally the jQuery UI queue widget -->
<script src="jQuery/FileUpload/plupload.full.js"></script>
<script type="text/javascript" src="jQuery/FileUpload/jquery.plupload.queue/jquery.plupload.queue.js"></script>
<script type="text/javascript" src="jQuery/FileUpload/jquery.ui.plupload/jquery.ui.plupload.js"></script>
<!--/ Upload File -->




<!-- Add mousewheel plugin (this is optional) -->
<script type="text/javascript" src="jQuery/FancyBox/jquery.mousewheel-3.0.6.pack.js"></script>

<!-- Add fancyBox main JS and CSS files -->
<script type="text/javascript" src="jQuery/FancyBox/jquery.fancybox.js"></script>
<link rel="stylesheet" type="text/css" href="jQuery/FancyBox/jquery.fancybox.css" media="screen" />

<!-- Add Button helper (this is optional) -->
<link rel="stylesheet" type="text/css" href="jQuery/FancyBox/helpers/jquery.fancybox-buttons.css" />
<script type="text/javascript" src="jQuery/FancyBox/helpers/jquery.fancybox-buttons.js"></script>

<!-- Add Thumbnail helper (this is optional) -->
<link rel="stylesheet" type="text/css" href="jQuery/FancyBox/helpers/jquery.fancybox-thumbs.css" />
<script type="text/javascript" src="jQuery/FancyBox/helpers/jquery.fancybox-thumbs.js"></script>

<!-- Add Media helper (this is optional) -->
<script type="text/javascript" src="jQuery/FancyBox/helpers/jquery.fancybox-media.js"></script>

<SCRIPT type="text/javascript">
	var username_public = "<%=manv%>";
    // function show message dialog
    function showMsgDialog(data) {
        var type = eval($(data.responseText).find('type').text());
        var message = $(data.responseText).find('message').text();
        if (type == 0) {
            return [false, message];
        } else {
            return [true, message];
        }
    }
    
    $(document).ready(function() {
        
        

        $('#ribbon div.tab-content').hide();
        $('#ribbon div.tab-content:first').show();
        $('#ribbon ul.tab-navigation li:first').addClass('active');

        $('#ribbon ul.tab-navigation li a').click(function() {
            $('#ribbon ul.tab-navigation li').removeClass('active');
            $(this).parent().addClass('active');
            var currentTab = $(this).attr('href');
            $('#ribbon div.tab-content').hide();
            $(currentTab).show();
            return false;
        });

        $(".items").hover(function() {
            $(this).addClass("items-hover");
            $(this).find("span").css({
                color: 'white'
            });
        },

		function() {
		    $(this).removeClass("items-hover");
		    $(this).find("span").css({
		        color: 'white'
		    });
		});
    });

    var pageLayout, tabsLayout, $Tabs;
    // options for the layout generated _inside_ the tab(s)
    var tabLayoutOptions = {
        //resizeWithWindow: false, // *** IMPORTANT *** tab-layouts must NOT resize with the window
        center__contentSelector: ".ui-widget-content"
    };

    $(document)
			.ready(
					function() {
					    pageLayout = $('body').layout({
					        resizable: false,
					        closable: false,
					        center__paneSelector: "#tabs-main",
					        north: {
					            size: 92,
					            slidable: false,
					            resizable: false,
					            spacing_open: 0
					        },
					        south: {
					            size: 24,
					            slidable: false,
					            resizable: false,
					            spacing_open: 0
					        }
					    }); //

					    // init the Tabs-Wrapper layout inside the center-pane of the pageLayout
					    tabsLayout = pageLayout.panes.center
								.layout({
								    resizable: false,
								    closable: false,
								    spacing_open: 0,
								    north__paneSelector: "#tab_buttons" // tab-buttons
									,
								    center__paneSelector: "#tab_panels" // tab-panels-wrapper
									,
								    center__onresize: $.layout.callbacks.resizeTabLayout
								    // resize 'visible tab layout' if pane resizes
								});
					    $Tabs = $("#tabs-main")
								.tabs(
										{
										    //	load Ajax tabs only once - when it is created
										    cache: true

										    //	use a tab-button template so we can add a close (X) button/icon
											,
										    tabTemplate: '<LI style="position:relative;padding-right:10px"><A href="#{href}" rel="#{href}">#{label}  <SPAN style="position:absolute; top:3px; right:10px;" class="ui-icon ui-icon-close"></SPAN></A></LI>'
										    //	use a tab-template template so we can add our own class, and hide it temporarily
											,
										    panelTemplate: '<DIV class="tab-panel"></DIV>',
										    idPrefix: "tab_panel_" // prefix for auto-generated ID for panel - eg: "tab_panel_3"

										    //	load() runs after widget finishes loading Ajax content into a tab
											,
										    load: function(evt, ui) {
										        var $panel = $(ui.panel), $tab = $(ui.tab)
										        //	get 'first element' of the injected content - used to store 'data'
												, $child = $panel
														.children(":first");

										        //	look for layout-options data on the *first element* of the injected content
										        //	this trick could also be used to trigger initialization of other widgets, like tabs or accordions
										        var options = $child
														.data("layout_options"); // eg: "tabLayoutOptions"
										        if ($.type(options) === "string")
										            options = window[options];
										        // if layout options found, then init the layout with these options
										        if (options)
										            $panel.layout(options);
										    },
										    add: function(evt, ui) {
										        var $panel = $(ui.panel), $tab = $(ui.tab);

										        $tab.closest("ul").siblings(
														"div.ui-tabs-panel")
														.remove();
										        $tab
														.find(".ui-icon-close")
														.click(remove_tab)
														.attr("title",
																"Close This Tab");
										        $Tabs.tabs("select", ui.index);
										    }

										    //	remove() runs after widget removes a tab
											,
										    remove: function(evt, ui) {
										        // resize tabs-layout in case tabs no longer wrapped to another line
										        $Tabs.layout().resizeAll();
										    }

										    //	create() runs after widget is created and the first tab is shown
											,
										    create: function(evt, ui) {
										        // create the layout inside the first tab
										        $("#first_panel").layout(
														tabLayoutOptions);
										    }
										    , select: function(event, ui) {
										        $(ui.panel).css({ 'display': 'block' });
										    }
										});

					    // resize the tabsLayout after creating the tabs to fit the tabs precisely
					    tabsLayout.resizeAll();
					});

    // onClick event of the Add New Tab button
    function add_tab(name, url) {
        var check = false;
        $.each($("#tabs-main ul.ui-tabs-nav li a"), function(index, item) {
            if ($(item).attr("rel") == url) {
                $("#tabs-main").tabs("select", index);
                check = true;
                return false;
            }
        });

        if (!check) {
            $Tabs.tabs("add", url, name);
        }
    };

    // bound to 'X' icon inside NEW tab-buttons
    function remove_tab(evt) {
        //if (confirm("Close this tab?")) {
        var idx = $(this).closest("li").index();
        $Tabs.tabs("remove", idx);
        // }
    };
	
	function ip_fd() {
		return '<b style="color:red">(*)</b>';
	}
	function chk_spec(str, field){
		var chk = !/[@~`!#$%\^&*+=\_\[\]\\';,./{}|\\":<>()\?]/g.test(str);
		if(chk == false) {
			return field + ' không thể có kí tự đặc biệt.';
		}
		else {
			return '';
		}
	}
	
	function get_today(){
		var today = new Date();
		var dd = today.getDate();
		var mm = today.getMonth()+1; //January is 0!

		var yyyy = today.getFullYear();
		if(dd<10){
			dd='0'+dd;
		} 
		if(mm<10){
			mm='0'+mm;
		} 
		var today = dd+'/'+mm+'/'+yyyy;
		return today;
	}
</SCRIPT>
<style type="text/css">
    #page_header, #tabs-main,  #tab_buttons, #tab_panels, .tab-panel 
    {
        border: none;
        padding: 0 !important;
        overflow: hidden;
        position: relative;
        width: 100%;
	    height: 100%;
    }

    #tab_buttons span.ui-icon {
	    margin-right: -7px;
	    vertical-align: middle;
	    display: inline-block;
	    cursor: pointer;
    }

    #tab_buttons ul {
	    background: none;
    }

    .tab-panel .ui-layout-north {
	    border-width: 0 0 1px;
	    overflow: hidden;
    }

    .tab-panel .ui-layout-center{
	    border-width: 1px 0 0;
	    padding: 0px;
	    overflow: hidden;
    }

    #lay-south-sanpham
    {
	    padding:0 !important;
	    overflow:hidden !important;
	    width:100%;
	    height:100%;
	}
    .ui-tabs-panel
    {
	    padding:0 !important;
	    width:100%;
	    height:100%;
	}

    .ui-layout-center,
    .ui-layout-north,
    .ui-layout-south,
    .ui-layout-west,
    .ui-layout-east
    {
	    overflow: hidden !important;
    }

    /* Custom View Form */
    div.ui-jqdialog-content td.form-view-data {
        white-space: normal !important;
        height: auto;
        vertical-align: middle;
        padding-top: 3px; padding-bottom: 3px
    }
    /* Custom View Form */

    .ui-layout-east, .ui-layout-west
    {
	    overflow:hidden;
	}

    .custom-menu-vnn {
        display: none;
        z-index: 1000;
        position: fixed;
        overflow: auto;
        border: 1px solid #CCC;
        white-space: nowrap;
        font-family: sans-serif;
        background: #FFF;
        color: #333;
        border-radius: 5px;
        padding: 0;
        box-shadow: 1px 1px 4px 1px #4c4c4c;
        max-height: 200px;
    }

    /* Each of the items in the list */
    .custom-menu-vnn li {
        padding: 8px 12px;
        cursor: pointer;
        list-style-type: none;
        transition: all .3s ease;
        user-select: none;
    }

    .custom-menu-vnn li:hover {
        background-color: #DEF;
    }
</style>

</head>
<body>

	<div class="ui-layout-north">
	    <div style="position:absolute; top:0px; right:100px; width:220px; padding:6px; "><us1:UserNameProfile ID="UserNameProfile1" runat="server" /></div>
		<div style="position: absolute; top: 1px; right: 3px; background: url('images/vinhgia_logo_25px.png') no-repeat; width: 70px; height: 25px;"></div>
		<!-- toolbar -->
		<!-- content -->
		<div id="ribbon">
			<ul class="tab-navigation">
			    <% if (PTSP){ %>
				    <li><a href="#tab-phatrien-sanpham">Phát triển sản phẩm</a></li>
				<% } %>
				<% if (NGHVU){ %>
				    <li><a href="#tab-nghiepvu">Nghiệp vụ</a></li>
				<% } %>
				<% if (KETOAN){ %>
				    <li><a href="#tab-ketoan">Kế toán</a></li>
				<% } %>
				<% if (BAOCAO){ %>
				    <li><a href="#tab-baocao-thongke">Báo cáo thống kê</a></li>
				<% } %>
                <% if (HETHONG){ %>
				    <li><a href="#tab-hethong">Hệ thống</a></li>
                <% } %>
			</ul>


            <% if (PTSP)
               { %>
			<!-- phat trien san pham -->
			<div class="tab-content" id="tab-phatrien-sanpham">

				<div class="sc_menu">
					<ul class="sc_menu">
					<% foreach (System.Data.DataRow row in dt.Rows)%>
                    <% { %>
                        <%  String mamenu, title, module_id, tenmenu, image, url;
                            int sortorder;
                            mamenu = row[0].ToString();
                            module_id = row[1].ToString();
                            title = row[2].ToString();
                            tenmenu = row[3].ToString();
                            image = row[4].ToString();
                            url = row[5].ToString();
                            sortorder = int.Parse(row[6].ToString());
                            
                        %>
                        

						<% if (module_id.Equals("PTSP"))
         { %>
						    <li>
						        <div class="items" id="<%= mamenu %>" title="<%= title %>"
								    onclick="add_tab('<%= tenmenu %>', '<%= url %>')">
								    <div>
									    <img class="icon" src="images/icon/<%= image %>" />
								    </div>
								    <div class="name">
									    <span><%= tenmenu%></span>
								    </div>
							    </div>
							</li>
					    <%  } // end if 
                       } // end for %>
					</ul>
					
					<div class="clear"></div>
				</div>

			</div>
			<!-- / phat trien san pham  -->
			<% } %>
			
			
            <% if (NGHVU)
               { %>
			<!-- nghiep vu -->
			<div class="tab-content" id="tab-nghiepvu">
				<div class="sc_menu">
					<ul class="sc_menu">
                    <% foreach (System.Data.DataRow row in dt.Rows)%>
                    <% { %>
                        <%  String mamenu, title, module_id, tenmenu, image, url;
                            int sortorder;
                            mamenu = row[0].ToString();
                            module_id = row[1].ToString();
                            title = row[2].ToString();
                            tenmenu = row[3].ToString();
                            image = row[4].ToString();
                            url = row[5].ToString();
                            sortorder = int.Parse(row[6].ToString());
                            
                        %>
                        

						<% if (module_id.Equals("NGHVU"))
         { %>
						    <li>
						        <div class="items" id="<%= mamenu %>" title="<%= title %>"
								    onclick="add_tab('<%= tenmenu %>', '<%= url %>')">
								    <div>
									    <img class="icon" src="images/icon/<%= image %>" />
								    </div>
								    <div class="name">
									    <span><%= tenmenu%></span>
								    </div>
							    </div>
							</li>
					    <%  } // end if 
                       } // end for %>
					</ul>
					<div class="clear"></div>
				</div>
			</div>
			<!-- / nghiep vu -->
			<% } %>
			
			
			
			
            <% if (KETOAN)
               { %>
			<!-- ke toan -->
			<div class="tab-content" id="tab-ketoan">
				<div class="sc_menu">
					<ul class="sc_menu">
						<% foreach (System.Data.DataRow row in dt.Rows)%>
                    <% { %>
                        <%  String mamenu, title, module_id, tenmenu, image, url;
                            int sortorder;
                            mamenu = row[0].ToString();
                            module_id = row[1].ToString();
                            title = row[2].ToString();
                            tenmenu = row[3].ToString();
                            image = row[4].ToString();
                            url = row[5].ToString();
                            sortorder = int.Parse(row[6].ToString());
                            
                        %>
                        

						<% if (module_id.Equals("KETOAN"))
         { %>
						    <li>
						        <div class="items" id="<%= mamenu %>" title="<%= title %>"
								    onclick="add_tab('<%= tenmenu %>', '<%= url %>')">
								    <div>
									    <img class="icon" src="images/icon/<%= image %>" />
								    </div>
								    <div class="name">
									    <span><%= tenmenu%></span>
								    </div>
							    </div>
							</li>
					    <%  } // end if 
                       } // end for %>
					</ul>
					<div class="clear"></div>
				</div>

			</div>
			<!-- / ke toan -->
			<%} %>
			
			
			


            <% if (BAOCAO)
               { %>
			<!-- thong ke -->

			<div class="tab-content" id="tab-baocao-thongke">
				<div class="sc_menu">
					<ul class="sc_menu">
						<% foreach (System.Data.DataRow row in dt.Rows)%>
                    <% { %>
                        <%  String mamenu, title, module_id, tenmenu, image, url;
                            int sortorder;
                            mamenu = row[0].ToString();
                            module_id = row[1].ToString();
                            title = row[2].ToString();
                            tenmenu = row[3].ToString();
                            image = row[4].ToString();
                            url = row[5].ToString();
                            sortorder = int.Parse(row[6].ToString());
                            
                        %>
                        

						<% if (module_id.Equals("BAOCAO"))
         { %>
						    <li>
						        <div class="items" id="<%= mamenu %>" title="<%= title %>"
								    onclick="add_tab('<%= tenmenu %>', '<%= url %>')">
								    <div>
									    <img class="icon" src="images/icon/<%= image %>" />
								    </div>
								    <div class="name">
									    <span><%= tenmenu%></span>
								    </div>
							    </div>
							</li>
					    <%  } // end if 
                       } // end for %>
					</ul>
					<div class="clear"></div>
				</div>
			</div>
			<!-- / thong ke -->
            <% } %>

			<!-- he thong -->
			<div class="tab-content" id="tab-hethong">
				<div class="sc_menu">
					<ul class="sc_menu">
						<% foreach (System.Data.DataRow row in dt.Rows)%>
                    <% { %>
                        <%  String mamenu, title, module_id, tenmenu, image, url;
                            int sortorder;
                            mamenu = row[0].ToString();
                            module_id = row[1].ToString();
                            title = row[2].ToString();
                            tenmenu = row[3].ToString();
                            image = row[4].ToString();
                            url = row[5].ToString();
                            sortorder = int.Parse(row[6].ToString());
                            
                        %>
                        

						<% if (module_id.Equals("HETHONG"))
         { %>
						    <li>
						        <div class="items" id="Div1" title="<%= title %>"
								    onclick="add_tab('<%= tenmenu %>', '<%= url %>')">
								    <div>
									    <img class="icon" src="images/icon/<%= image %>" />
								    </div>
								    <div class="name">
									    <span><%= tenmenu%></span>
								    </div>
							    </div>
							</li>
					    <%  } // end if 
                            } // end for %>
					</ul>
					<div class="clear"></div>
				</div>
			</div>
			<!-- / he thong -->
            
            
            
            

		</div>
		<!--/ content -->
		<!--/ toolbar -->
	</div>




	<DIV id="tabs-main" class="ui-layout-center">
		<DIV id="tab_buttons" class="sc_menu" style="height: 28px">
			<UL class="sc_menu" style="height: 28px">
				<LI><A href="#first_panel">Start</A></LI>
			</UL>
		</DIV>
		<!-- center-pane with dynamic added tab-panels -->
		<DIV id="tab_panels">
			<DIV id="first_panel" class="tab-panel">
				<DIV class="ui-layout-center"style="position: relative; background: url('images/anco_background.png')">
					<div style="text-align: center">
						<img style="padding-top: 130px" src="images/vinhgia_banner.png" />
					</div>
				</DIV>
			</DIV>
		</DIV>
	</DIV>


	<div class="ui-layout-south bg">
		<!-- footer -->
		<div id="lay-footer" class="lay-content">
			<!-- content -->
			<div style="padding-top: 4px; font-family: arial; font-size: 10px">eMan SCM 3.0 2010 - 2012</div>
			<!--/ content -->
		</div>
		<!--/ footer -->
	</div>
	
	<script id="publicScriptVNN">
		function funcPrintSPPublic(namegrid, status_) {
			if(status_ != null) {
				status_ = "&trangthai="+status_;
			}
			else {
				status_ = "";
			}
			var windowSize = "width=700,height=700,scrollbars=yes";
			var poId = $('#'+namegrid).jqGrid('getGridParam', 'selrow');
				var name = 'dialog-print-sp';
				var title = 'Trích xuất dữ liệu';
				var content =  `<table style="width:100%">
									<tr>
										<td>
											<fieldset style="padding:10px 0px">
												<legend>
													<input id="printImportWebsite" type="radio" name="rdoPrintType" value="printImportWebsite" />
													<label for="printImportWebsite">Chiết xuất thông tin import website </label>
												</legend>
											</fieldset>
										</td>
									</tr>
									<tr>
										<td>
											<fieldset style="padding:10px 0px">
												<legend>
													<input id="printThongTin" type="radio" name="rdoPrintType" value="printThongTin" />
													<label for="printThongTin">Chiết xuất thông tin sản phẩm </label>
												</legend>
												<label for="valueLike">Giá trị lọc:</label> 
												<input type="text" id="valueLike" />
												
												<div style="padding-top: 7px;text-align: right;padding-right: 23px;color: blue;">
													<input type="checkbox" id="chk_dgmd_ptt0" />
													<label for="chk_dgmd_ptt0">Chọn đóng gói khác mặc định</label>
												</div>
											</fieldset>
										</td>
									</tr>
									<tr>
										<td>
											<fieldset style="padding:10px 0px">
												<legend>
													<input id="printThongTinMore" type="radio" name="rdoPrintType" value="printThongTinMore" />
													<label for="printThongTinMore">Chiết xuất thông tin sản phẩm (xls) </label>
												</legend>
												<label for="valueLikeMore">Giá trị lọc:</label> 
												<input type="text" id="valueLikeMore" />
													
												<div style="padding-top: 7px;text-align: right;padding-right: 23px;color: blue;">
													<input type="checkbox" id="chk_dgmd_pttm" />
													<label for="chk_dgmd_pttm">Chọn đóng gói khác mặc định</label>
												</div>
												</fieldset>
										</td>
									</tr>
									<tr>
										<td>
											<fieldset style="padding:10px 0px"><legend><input id="printThongTinQRCode" type="radio" name="rdoPrintType" value="printThongTinQRCode" />
											<label for="printThongTinQRCode">Chiết xuất excel cho QRcode (xls) </label></legend>
											<label for="valueQRCode">Giá trị lọc:</label> <input type="text" id="valueQRCode" /></fieldset>
										</td>
									</tr>
									<tr>
										<td>
											<fieldset style="padding:10px 0px"><legend><input id="InOutQRCode" type="radio" name="rdoPrintType" value="InOutQRCode" />
												<label for="InOutQRCode">Nhập xuất excel cho QRcode (xls) </label></legend>
												<form  enctype="multipart/form-data" id="frm_InOutQRCode" action='PrintControllers/InThongTinSanPhamQRCode/ExportQRCode.aspx' target="exportEX_popup"
													method="post" onsubmit="window.open('about:blank','exportEX_popup','${windowSize}');"
												>
													<input type="file" id="fileExcel" name="fileExcel" accept=".xls,.xlsx"/>
												</form>
											</fieldset>
										</td>
									</tr>
                                    <tr>
										<td>
											<label for="valueTinhTrangHH">Tình Trạng HH: </label>
                                            <select id="select_TinhTrang" style="width: 136px">
                                                <option value="">Tất cả</option>
	                                            <%String sql_tt = "select md_danhmuchanghoa_id, ten_danhmuc from md_danhmuchanghoa where hoatdong = 1 order by ma_danhmuc";
                                                    DataTable data_tt = mdbc.GetData(sql_tt);
                                                    foreach (DataRow tt in data_tt.Rows) { %>
                                                    <option value='<%=tt[0].ToString()%>'><%=tt[1].ToString()%></option>
                                                <%} %>
                                            </select>
                                            
										</td>
									</tr>
								</table>`;

				$('body').append('<div title="' + title + '" id="' + name + '">' + content + '</div>');
				var nameParent = '#' + name;
				$(nameParent).dialog({
					modal: true
					, open: function (event, ui) {
						//hide close button.
						$(this).parent().children().children('.ui-dialog-titlebar-close').hide();
						var fileExcel = $('#fileExcel',nameParent);
						fileExcel.prop('disabled', true);
						$('#printImportWebsite', nameParent).prop('checked', true);
						$('input[name="rdoPrintType"]', nameParent).click(function(){
							var val = $(this).val();
							if(val == 'InOutQRCode') {
								fileExcel.prop('disabled', false);
							}
							else {
								fileExcel.prop('disabled', true);
							}
						});
					    //hide select_TinhTrang
						var select_TinhTrang = $('#select_TinhTrang');
						select_TinhTrang.prop('disabled', false);
						$('input[name="rdoPrintType"]', nameParent).click(function () {
						    var val = $(this).val();
						    if (val == 'InOutQRCode') {
						        select_TinhTrang.prop('disabled', true);
						    }
						    else {
						        select_TinhTrang.prop('disabled', false);
						    }
						});

					}
					, buttons: [
					{
						id: "btn-print-ok",
						text: "In",
						click: function () {
						    debugger;
							var printType = $('input[name=rdoPrintType]:checked', '#dialog-print-sp').val();
							var printURL = "PrintControllers/";
							var select_TinhTrang = $('#select_TinhTrang').val();
							if (typeof printType != 'undefined') {
								if (printType == "printImportWebsite") {
									if (poId != null) {
										printURL += "InThongTinSanPham/";
										window.open(printURL + "?md_sanpham_id=" + poId + "&md_tinhtranghanghoa_id=" + select_TinhTrang, "In Thông Tin Sản Phẩm", windowSize);
									} else {
										alert('Bạn chưa chọn sản phẩm cần in.!');
									}
								}else if (printType == "printThongTin") {
									var valueLike = $('#valueLike').val();
									if (valueLike != null) {
										printURL += "InThongTinSanPhamReport/";
										window.open(printURL + "?md_sanpham_id=" + valueLike + "&typedg=" + $('#chk_dgmd_ptt0').prop('checked') + "&md_tinhtranghanghoa_id=" + select_TinhTrang
										+ status_, "In Thông Tin Sản Phẩm", windowSize);
									} else {
										alert('Bạn chưa chọn sản phẩm cần in.!');
									}
								}
								else if (printType == "printThongTinMore") {
									var valueLike = $('#valueLikeMore').val();
									if (valueLike != null) {
										printURL += "InThongTinSanPhamMore/";
										window.open(printURL + "?md_sanpham_id=" + valueLike + "&typedg=" + $('#chk_dgmd_pttm').prop('checked') + "&md_tinhtranghanghoa_id=" + select_TinhTrang
										+ status_, "In Thông Tin Sản Phẩm", windowSize);
									} else {
										alert('Bạn chưa chọn sản phẩm cần in.!');
									}
								}
								else if (printType == "printThongTinQRCode") {
									var valueLike = $('#valueQRCode').val();
									if (valueLike != null) {
										printURL += "InThongTinSanPhamQRCode/";
										window.open(printURL + "?md_sanpham_id=" + valueLike + "&md_tinhtranghanghoa_id=" + select_TinhTrang
										+status_, "In Thông Tin Sản Phẩm", windowSize);
									} else {
										alert('Bạn chưa chọn sản phẩm cần in.!');
									}
								}
								else if (printType == "InOutQRCode") {
									$('#frm_InOutQRCode', nameParent).submit();
								}
							} else {
								alert('Hãy chọn cách in.!');
							}
						}
					}
					, {
						id: "btn-print-close",
						text: "Thoát",
						click: function () {
							$(this).dialog("destroy").remove();
						}
					}
					]
				});
		}

        function disableSelOption(el, frmID, val) {
            var element = $(el, frmID);
            if (element.attr('id') != null) {
                element.prop('disabled', val);
            }
            else {
                setTimeout(function () { disableSelOption(el, frmID, val); }, 10);
            }
        }
		
        var vnnIsAdmin = '<%=isadmin%>';
        var suaGiaPO = '<%=suaGiaPO%>';
        var doiNCU = '<%=doiNCU%>';
		var showPBGCforEveryOne = false;
		var xemgiaHH, xemgiaHHST, xemgiaHHNHD, xemgiaHHDQ, xemgiaDSDH, xemgiaNoiHH;
		xemgiaHH = '<%=xemgiaHH%>';
		xemgiaHHST = '<%=xemgiaHHST%>';
		xemgiaHHNHD = '<%=xemgiaHHNHD%>';
		xemgiaHHDQ = '<%=xemgiaHHDQ%>';
		xemgiaDSDH = '<%=xemgiaDSDH%>';
	    xemgiaNoiHH = '<%=xemgiaNoiHH%>';
		
		var chietxuatHH, chietxuatHHST, chietxuatHHNHD, chietxuatHHDQ, chietxuatLHH, chietxuatDGSP, chietxuatDGST, chietxuatHSCode, chietxuatNNL, chietxuatKDSP, chietxuatCNSP, chietxuatDeTai, chietxuatMauSac, chietxuatBoCai, selectHinhThucBan;
		chietxuatHH = '<%=chietxuatHH%>';
		chietxuatHHST = '<%=chietxuatHHST%>';
		chietxuatHHNHD = '<%=chietxuatHHNHD%>';
		chietxuatHHDQ = '<%=chietxuatHHDQ%>';
		chietxuatLHH = '<%=chietxuatLHH%>';
		chietxuatDGSP = '<%=chietxuatDGSP%>';
		chietxuatDGST = '<%=chietxuatDGST%>';
		chietxuatHSCode = '<%=chietxuatHSCode%>';
		chietxuatNNL = '<%=chietxuatNNL%>';
		chietxuatKDSP = '<%=chietxuatKDSP%>';
		chietxuatCNSP = '<%=chietxuatCNSP%>';
		chietxuatDeTai = '<%=chietxuatDeTai%>';
		chietxuatMauSac = '<%=chietxuatMauSac%>';
	    selectHinhThucBan = ':;Bán đại trà:Bán đại trà;Độc quyền:Độc quyền;Quản lý ngoài:Quản lý ngoài;Bán thử nghiệm:Bán thử nghiệm';

	    var createRightPanel = function (id) {
	        let menu = $('#gbox_' + id + ' .ui-jqgrid-bdiv');

	        menu.off('contextmenu');
	        menu.contextmenu(function (event) {
	            let leftFunc = $('#' + id + '-pager_left');
	            leftFunc = leftFunc.find('.ui-pg-button');

	            event.preventDefault();
	            let selRowIds = $('#' + id).jqGrid('getGridParam', 'selarrrow');

	            if (selRowIds.length <= 1)
                    $(event.target).click();

	            $(".custom-menu-vnn").remove();
	            menu.parent().parent().parent().parent().prepend("<ul class='custom-menu-vnn'></ul>");
	            
	            leftFunc.each(function () {
	                let thisLeftFunc = $(this);
	                let text = thisLeftFunc.attr('title');
	                let textIcon = thisLeftFunc.text()
	                let icon = thisLeftFunc.html().replace(textIcon, '');
	                if (text) {
	                    let liAction = $("<li><table><tr><td>" + icon + "</td><td><div style='width:5px'></div></td><td>" + text + "</td><td><div style='width:5px'></div></table></li>").appendTo($('.custom-menu-vnn'));
	                    liAction.off("click");
	                    liAction.click(function () {
	                        thisLeftFunc.click();
	                        $(".custom-menu-vnn").fadeOut(300);
	                    });
	                }
	            });

	            let haveScroll = $(".custom-menu-vnn").height() < $(".custom-menu-vnn").scrollHeight;
	            let footerHeight = $('#lay-footer').height();
	            let innerHeight = window.innerHeight - footerHeight;
	            let widthCTMN = $(".custom-menu-vnn").width() + (haveScroll ? 10 : 0);
	            let maxWidth = event.clientX + widthCTMN;
	            let maxHeight = event.clientY + $(".custom-menu-vnn").height();
	            
	            
	            let xScreen = maxWidth > menu.width() ? event.clientX - $(".custom-menu-vnn").width() + (menu.width() - event.clientX - 10) : event.clientX;
	            let yScreen = maxHeight > innerHeight ? event.clientY - $(".custom-menu-vnn").height() + (innerHeight - event.clientY - 10) : event.clientY;

	            $(".custom-menu-vnn").fadeIn(300).css({
	                top: yScreen + "px",
	                left: xScreen + "px",
	                width: widthCTMN
	            });
	        });
	    };

	    var uuidv4 = function() {
	        return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
	            var r = Math.random() * 16 | 0, v = c == 'x' ? r : (r & 0x3 | 0x8);
	            return v.toString(16);
	        });
	    }

	    $(document).bind("mousedown", function (e) {
	        if (!$(e.target).parents(".custom-menu-vnn").length > 0) {
	            $(".custom-menu-vnn").fadeOut(300);
	        }
	    });
	</script>
</body>
</html>

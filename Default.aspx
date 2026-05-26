<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register src="UserNameProfile.ascx" tagname="UserNameProfile" tagprefix="us1" %>
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

<script src="jQuery/jquery.form.js" type="text/javascript"></script>


<!-- ScrollerMenu -->
<script type="text/javascript" src="jQuery/ScrollerMenu/sc-menu.js"></script>
<link type="text/css" rel="Stylesheet" href='jQuery/ScrollerMenu/sc_menu.css' />
<!--/ ScrollerMenu -->





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
        //resizeWithWindow: false,  // *** IMPORTANT *** tab-layouts must NOT resize with the window
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
										        $("#first_panel").layout();
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
	function chk_spec(str){
		return !/[~`!#$%\^&*+=\_\[\]\\';,/{}|\\":<>\?]/g.test(str);
	}
</SCRIPT>
<style>
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
</style>

</head>
<body>
	<div class="ui-layout-north">
	    <div style="position:absolute; ; top:0px; right:100px; width:130px; padding:6px; "><us1:UserNameProfile ID="UserNameProfile1" runat="server" /></div>
		<div style="position: absolute; top: 0px; right: 3px; background: url('images/anco_logo_25px.png') no-repeat; width: 70px; height: 25px;"></div>
		<!-- toolbar -->
		<!-- content -->
		<div id="ribbon">
			<ul class="tab-navigation">
				<li><a href="#tab-phatrien-sanpham">Phát triển sản phẩm</a></li>
				<li><a href="#tab-nghiepvu">Nghiệp vụ</a></li>
				<li><a href="#tab-ketoan">Kế toán</a></li>
				<li><a href="#tab-baocao-thongke">Báo cáo thống kê</a></li>
				<li><a href="#tab-import">Import</a></li>
				<li><a href="#tab-hethong">Hệ thống</a></li>
			</ul>

			<!-- phat trien san pham -->
			<div class="tab-content" id="tab-phatrien-sanpham">

				<div class="sc_menu">
					<ul class="sc_menu">
						<li>
							<div class="items" id="loaihanghoa" title="Loại hàng hóa"
								onclick="add_tab('Loại hàng hóa', 'inc-loaihanghoa.aspx')">
								<div>
									<img class="icon" src="images/icon/product_cate.png" />
								</div>
								<div class="name">
									<span>Loại hàng hóa</span>
								</div>
							</div>
						</li>
						<li>
							<div class="items" id="hanghoa" title="Hàng hóa"
								onclick="add_tab('Hàng hóa', 'inc-hanghoa.aspx')">
								<div>
									<img class="icon" src="images/icon/product.png" />
								</div>
								<div class="name">
									<span>Hàng hóa</span>
								</div>
							</div>
						</li>
						
						<!--<li>
							<div class="items" id="hanghoatam" title="Hàng hóa tạm"
								onclick="add_tab('Hàng hóa tạm', 'inc-hanghoatam.aspx')">
								<div>
									<img class="icon" src="images/icon/product.png" />
								</div>
								<div class="name">
									<span>HH Tạm</span>
								</div>
							</div>
						</li> -->
						
						<li>
							<div class="items" id="hanghoadocquyen"
								title="Hàng hóa độc quyền"
								onclick="add_tab('Hàng hóa độc quyền', 'inc-hanghoa-docquyen.aspx')">
								<div>
									<img class="icon" src="images/icon/product-partner.png" />
								</div>
								<div class="name">
									<span>H/h độc quyền</span>
								</div>
							</div>
						</li>
						<%--<li>
						        <div class="items" id="donggoi" title="Đóng gói" onclick="add_tab('Đóng gói', 'inc-donggoi.aspx')">
							        <div><img class="icon" src="images/icon/product_comp.png"/></div>
							        <div class="name">
								        <span>Đóng gói</span>
							        </div>
						        </div>
        						</li>--%>
						<li>
							<div class="items" id="donggoisanpham" title="Đóng gói sản phẩm"
								onclick="add_tab('Đóng gói sản phẩm', 'inc-donggoi-sanpham.aspx')">
								<div>
									<img class="icon" src="images/icon/product_comp.png" />
								</div>
								<div class="name">
									<span>Đóng gói SP</span>
								</div>
							</div>
						</li>
						<li>
							<div class="items" id="donggoimix" title="Đóng gói mix"
								onclick="add_tab('Đóng gói mix', 'inc-donggoi-mix.aspx')">
								<div>
									<img class="icon" src="images/icon/product_comp_mix.png" />
								</div>
								<div class="name">
									<span>Đóng gói mix</span>
								</div>
							</div>
						</li>
						<%--<li>
						        <div class="items" id="nhacungcap" title="Nhà cung cấp" onclick="add_tab('Nhà cung cấp', 'inc-nhacungcap.aspx')">
							        <div><img class="icon" src="images/icon/provider.png"/></div>
							        <div class="name">
								        <span>Nhà cung cấp</span>
							        </div>
						        </div>
        						</li>--%>
						<%--<li>
						        <div class="items" id="giaban" title="Giá bán(FOB)" onclick="add_tab('Giá bán(FOB)', 'inc-giaban.aspx')">
							        <div><img class="icon" src="images/icon/price.png"/></div>
							        <div class="name">
								        <span>Giá bán(FOB)</span>
							        </div>
						        </div>
        						</li><li>
						        <div class="items" id="giamua" title="Giá mua" onclick="add_tab('Giá mua', 'inc-giamua.aspx')">
							        <div><img class="icon" src="images/icon/price01.png"/></div>
							        <div class="name">
								        <span>Giá mua</span>
							        </div>
						        </div>
        						</li>--%>
						<%--<li>
						            <div class="items"  id="banggia" title="Bảng giá" onclick="add_tab('Bảng giá', 'inc-banggia.aspx')"">
							            <div><img class="icon" src="images/icon/price_list.png"/></div>
							            <div class="name">
								            <span>Bảng Giá</span>
							            </div>
						            </div>
            						</li><li>
						            <div class="items"  id="phienbangia" title="Phiên bản giá" onclick="add_tab('Phiên bản giá', 'inc-phienbangia.aspx')"">
							            <div><img class="icon" src="images/icon/price_list_version.png"/></div>
							            <div class="name">
								            <span>Phiên Bản Giá</span>
							            </div>
						            </div>
            						</li>--%>
						<li>
							<div class="items" id="giasanpham" title="Giá sản phẩm"
								onclick="add_tab('Giá sản phẩm', 'inc-giasanpham.aspx')"">
								<div>
									<img class="icon" src="images/icon/product_price.png" />
								</div>
								<div class="name">
									<span>Giá Sản Phẩm</span>
								</div>
							</div>
						</li>
						
						<li>
							<div class="items" id="cangbien" title="Cảng biển"
								onclick="add_tab('Cảng biển', 'inc-cangbien.aspx')"">
								<div>
									<img class="icon" src="images/icon/anchor.png" />
								</div>
								<div class="name">
									<span>Cảng biển</span>
								</div>
							</div>
						</li>
						<li>
							<div class="items" id="kho" title="kho"
								onclick="add_tab('Kho', 'inc-kho.aspx')">
								<div>
									<img class="icon" src="images/icon/warehouse.png" />
								</div>
								<div class="name">
									<span>Kho</span>
								</div>
							</div>
						</li>
                        <li>
							<div class="items" id="donvitinh" title="Đơn vị tính"
								onclick="add_tab('Đơn vị tính', 'inc-donvitinh.aspx')"">
								<div>
									<img class="icon" src="images/icon/unit.png" />
								</div>
								<div class="name">
									<span>Đơn vị tính</span>
								</div>
							</div>
						</li>
						<li>
							<div class="items" id="donvitinhsanpham" title="Đơn vị tính Sản Phẩm"
								onclick="add_tab('Đơn vị tính sản phẩm', 'inc-donvitinh-sanpham.aspx')"">
								<div>
									<img class="icon" src="images/icon/unit.png" />
								</div>
								<div class="name">
									<span>Đơn vị tính Sp</span>
								</div>
							</div>
						</li>
                        <li>
							<div class="items" id="trongluong"
								title="Trọng lượng"
								onclick="add_tab('Trọng lượng', 'inc-trongluong.aspx')"">
								<div>
									<img class="icon" src="images/icon/hinhthucdonggoi.png" />
								</div>
								<div class="name">
									<span>Trọng lượng</span>
								</div>
							</div>
						</li>

                        <li>
							<div class="items" id="kichthuoc"
								title="Kích thước"
								onclick="add_tab('Kích thước', 'inc-kichthuoc.aspx')"">
								<div>
									<img class="icon" src="images/icon/hinhthucdonggoi.png" />
								</div>
								<div class="name">
									<span>Kích thước</span>
								</div>
							</div>
						</li>
						
						<li>
							<div class="items" id="nhomnangluc"
								title="Nhóm Năng Lực"
								onclick="add_tab('Nhóm Năng Lực', 'inc-nhomnangluc.aspx')"">
								<div>
									<img class="icon" src="images/icon/hinhthucdonggoi.png" />
								</div>
								<div class="name">
									<span>Nhóm N/Lực</span>
								</div>
							</div>
						</li>
						
						<li>
							<div class="items" id="kieudang"
								title="Kiểu dáng sản phẩm"
								onclick="add_tab('Kiểu dáng sản phẩm', 'inc-kieudangsanpham.aspx')"">
								<div>
									<img class="icon" src="images/icon/hinhthucdonggoi.png" />
								</div>
								<div class="name">
									<span>Kiểu Dáng S/P</span>
								</div>
							</div>
						</li>
						
						<li>
							<div class="items" id="chucnang"
								title="Chức năng sản phẩm"
								onclick="add_tab('Chức năng sản phẩm', 'inc-chucnangsanpham.aspx')"">
								<div>
									<img class="icon" src="images/icon/hinhthucdonggoi.png" />
								</div>
								<div class="name">
									<span>Chức năng S/P</span>
								</div>
							</div>
						</li>
						
						<%--<li>
						            <div class="items" id="hangtrongkho" title="Hàng hóa trong kho" onclick="add_tab('Hàng hóa trong kho', 'inc-hangtrongkho.aspx')">
							            <div><img class="icon" src="images/icon/warehouse.png"/></div>
							            <div class="name">
								            <span>H/H Trong Kho</span>
							            </div>
						            </div>
						            </li>--%>
					</ul>
					<div class="clear"></div>
				</div>

			</div>
			<!-- / phat trien san pham  -->

			<!-- nghiep vu -->
			<div class="tab-content" id="tab-nghiepvu">
				<div class="sc_menu">
					<ul class="sc_menu">
						<li>
							<div class="items" id="quotation" title="Quotation"
								onclick="add_tab('Quotation', 'inc-quotation.aspx')">
								<div>
									<img class="icon" src="images/icon/quotation.png" />
								</div>
								<div class="name">
									<span>Quotation</span>
								</div>
							</div>
						</li>
						<li>
							<div class="items" id="po" title="P/O"
								onclick="add_tab('P/O', 'inc-po.aspx')">
								<div>
									<img class="icon" src="images/icon/po.png" />
								</div>
								<div class="name">
									<span>P/O</span>
								</div>
							</div>
						</li>
						<li>
							<div class="items" id="danhsachdathang"
								title="Danh sách đặt hàng"
								onclick="add_tab('D/S đặt hàng', 'inc-ds-dathang.aspx')">
								<div>
									<img class="icon" src="images/icon/danhsachdathang.png" />
								</div>
								<div class="name">
									<span>D/S đặt hàng</span>
								</div>
							</div>
						</li>
						<%--<li>
							<div class="items" id="baobi" title="Bao bì"
								onclick="add_tab('Bao bì', 'inc-baobi.aspx')">
								<div>
									<img class="icon" src="images/icon/packing.png" />
								</div>
								<div class="name">
									<span>Bao Bì</span>
								</div>
							</div>
						</li>--%>
						<li>
							<div class="items" id="kehoachxuathang"
								title="Kế hoạch xuất hàng"
								onclick="add_tab('Kế hoạch xuất hàng', 'inc-kehoach-xuathang.aspx')">
								<div>
									<img class="icon" src="images/icon/kehoachxuathang.png" />
								</div>
								<div class="name">
									<span>K/H xuất hàng</span>
								</div>
							</div>
						</li>
						<li>
							<div class="items" id="packinglistinvoice"
								title="Packing list - Invoice"
								onclick="add_tab('Packing list - Invoice', 'inc-packing-list-invoice.aspx')">
								<div>
									<img class="icon" src="images/icon/pi.png" />
								</div>
								<div class="name">
									<span>Chứng từ XK</span>
								</div>
							</div>
						</li>
						
						<li>	
				            <div class="items" id="congsuatnhacungung" title="Công suất nhà cung ứng" onclick="add_tab('Công suất CU', 'inc-congsuat-nhacungung.aspx')">
					            <div><img class="icon" src="images/icon/product.png"/></div>
					            <div class="name">
						            <span>Công suất NCƯ</span>
					            </div>
				            </div>
    					</li>
    					
    					<li>	
				            <div class="items" id="xemnangluc" title="Xem Năng Lực" onclick="add_tab('Xem Năng Lực', 'inc-xemnangluc.aspx')">
					            <div><img class="icon" src="images/icon/product.png"/></div>
					            <div class="name">
						            <span>Xem Năng Lực</span>
					            </div>
				            </div>
    					</li>
    					
						<%--<li>
						                <div class="items" id="xuathangchokhach" title="Xuất hàng cho khách" onclick="add_tab('Xuất hàng cho khách', 'inc-xuathang-chokhach.aspx')">
							                <div><img class="icon" src="images/icon/product.png"/></div>
							                <div class="name">
								                <span>X/H cho khách</span>
							                </div>
						                </div>
						            </li>--%>
					</ul>
					<div class="clear"></div>
				</div>
			</div>
			<!-- / nghiep vu -->

			<!-- ke toan -->
			<div class="tab-content" id="tab-ketoan">
				<div class="sc_menu">
					<ul class="sc_menu">
						<li>
							<div class="items" id="namtaichinh" title="Năm tài chính"
								onclick="add_tab('Năm tài chính', 'inc-namtaichinh.aspx')"">
								<div>
									<img class="icon" src="images/icon/namtaichinh.png" />
								</div>
								<div class="name">
									<span>Năm tài chính</span>
								</div>
							</div>
						</li>
						<%--<li>
						        <div class="items"  id="kytrongnam" title="Kỳ trong năm" onclick="add_tab('Kỳ trong năm', 'inc-kytrongnam.aspx')"">
							        <div><img class="icon" src="images/icon/product.png"/></div>
							        <div class="name">
								        <span>Kỳ trong năm</span>
							        </div>
						        </div>
        						</li>--%>
						<li>
							<div class="items" id="modongky" title="Mở - đóng kỳ"
								onclick="add_tab('Mở - đóng kỳ', 'inc-modong-ky.aspx')">
								<div>
									<img class="icon" src="images/icon/modongky.png" />
								</div>
								<div class="name">
									<span>Mở/đóng kỳ</span>
								</div>
							</div>
						</li>
						<%--<li>
						        <div class="items" id="tondauky" title="Tồn đầu kỳ" onclick="add_tab('Tồn đầu kỳ', 'inc-tondau-ky.aspx')">
							        <div><img class="icon" src="images/icon/product.png"/></div>
							        <div class="name">
								        <span>Tồn đầu kỳ</span>
							        </div>
						        </div>
        						</li>--%>
						<li>
							<div class="items" id="kiemkekho" title="Kiểm kê kho"
								onclick="add_tab('Kiểm kê kho', 'inc-kiemke-kho.aspx')">
								<div>
									<img class="icon" src="images/icon/kiemkekho.png" />
								</div>
								<div class="name">
									<span>Kiểm kê kho</span>
								</div>
							</div>
						</li>
						<li>
							<div class="items" id="nhapkho" title="Nhập kho"
								onclick="add_tab('Nhập kho', 'inc-nhapkho.aspx')">
								<div>
									<img class="icon" src="images/icon/nhapkho.png" />
								</div>
								<div class="name">
									<span>Nhập kho</span>
								</div>
							</div>
						</li>
						<li>
							<div class="items" id="xuatkho" title="Xuất kho"
								onclick="add_tab('Xuất kho', 'inc-xuatkho.aspx')">
								<div>
									<img class="icon" src="images/icon/xuatkho.png" />
								</div>
								<div class="name">
									<span>Xuất kho</span>
								</div>
							</div>
						</li>
						<li>
							<div class="items" id="thuchi" title="Phải Thu"
								onclick="add_tab('Phải Thu', 'inc-thuchi.aspx')">
								<div>
									<img class="icon" src="images/icon/phaithu.png" />
								</div>
								<div class="name">
									<span>Phải Thu</span>
								</div>
							</div>
						</li>
						
						<li>
							<div class="items" id="phaichi" title="Phải chi"
								onclick="add_tab('Phải chi', 'inc-phaichi.aspx')">
								<div>
									<img class="icon" src="images/icon/phaichi.png" />
								</div>
								<div class="name">
									<span>Phải chi</span>
								</div>
							</div>
						</li>
						
						<%--<li>
							<div class="items" id="phaithu" title="Phải thu"
								onclick="add_tab('Phải thu', 'inc-phaithu.aspx')">
								<div>
									<img class="icon" src="images/icon/phaithu.png" />
								</div>
								<div class="name">
									<span>Phải thu</span>
								</div>
							</div>
						</li>
						--%>
						<li>
							<div class="items" id="dieukhoanthanhtoan"
								title="Điều khoản thanh toán"
								onclick="add_tab('Điều khoản thanh toán', 'inc-dieukhoan-thanhtoan.aspx')"">
								<div>
									<img class="icon" src="images/icon/dieukhoan-tt.png" />
								</div>
								<div class="name">
									<span>ĐK T/Toán</span>
								</div>
							</div>
						</li>
						
						<li>
							<div class="items" id="nganhang"
								title="Ngân Hàng"
								onclick="add_tab('Ngân Hàng', 'inc-nganhang.aspx')"">
								<div>
									<img class="icon" src="images/icon/dieukhoan-tt.png" />
								</div>
								<div class="name">
									<span>Ngân Hàng</span>
								</div>
							</div>
						</li>
						
						<li>
						        <div class="items"  id="loaichungtu" title="Loại chứng từ" onclick="add_tab('Loại chứng từ', 'inc-loaichungtu.aspx')"">
							        <div><img class="icon" src="images/icon/product.png"/></div>
							        <div class="name">
								        <span>Loại Chứng Từ</span>
							        </div>
						        </div>
        						</li>
						<li>
							<div class="items" id="sochungtu" title="Số chứng từ"
								onclick="add_tab('Số chứng từ', 'inc-sochungtu.aspx')"">
								<div>
									<img class="icon" src="images/icon/sochungtu.png" />
								</div>
								<div class="name">
									<span>Số Chứng Từ</span>
								</div>
							</div>
						</li>
						<li>
							<div class="items" id="dongtien" title="Đồng tiền"	onclick="add_tab('Đồng tiền', 'inc-dongtien.aspx')"">
								<div>
									<img class="icon" src="images/icon/dong-tien.png" />
								</div>
								<div class="name">
									<span>Đồng tiền</span>
								</div>
							</div>
						</li>
						<li>
							<div class="items" id="tygia" title="Tỷ giá"
								onclick="add_tab('Tỷ giá', 'inc-tygia.aspx')"">
								<div>
									<img class="icon" src="images/icon/tygia.png" />
								</div>
								<div class="name">
									<span>Tỷ giá</span>
								</div>
							</div>
						</li>
					</ul>
					<div class="clear"></div>
				</div>

			</div>
			<!-- / ke toan -->



			<!-- thong ke -->

			<div class="tab-content" id="tab-baocao-thongke">
				<div class="sc_menu">
					<ul class="sc_menu">
						<li>
							<div class="items" id="baocaokho" title="Báo cáo kho"
								onclick="add_tab('Báo cáo kho', 'inc-baocao-kho.aspx')">
								<div>
									<img class="icon" src="images/icon/baocaokho.png" />
								</div>
								<div class="name">
									<span>Báo cáo kho</span>
								</div>
							</div>
						</li>
						<li>
							<div class="items" id="baocaokhachhang" title="B/C khách hàng"
								onclick="add_tab('B/C khách hàng', 'inc-baocao-khachhang.aspx')">
								<div>
									<img class="icon" src="images/icon/baocaokhachhang.png" />
								</div>
								<div class="name">
									<span>B/C K/Hàng</span>
								</div>
							</div>
						</li>
						<li>
							<div class="items" id="baocaonhacungung" title="B/C nhà cung ứng"
								onclick="add_tab('B/C nhà cung ứng', 'inc-baocao-nhacungung.aspx')">
								<div>
									<img class="icon" src="images/icon/baocaonhacungung.png" />
								</div>
								<div class="name">
									<span>B/C Nhà CƯ</span>
								</div>
							</div>
						</li>
						<li>
							<div class="items" id="baocaothuchi" title="B/C thu chi"
								onclick="add_tab('B/C thu chi', 'inc-baocao-thuchi.aspx')">
								<div>
									<img class="icon" src="images/icon/baocaothuchi.png" />
								</div>
								<div class="name">
									<span>B/C thu chi</span>
								</div>
							</div>
						</li>
						<li>
							<div class="items" id="bi" title="BI (Business Intelligence)"
								onclick="add_tab('BI (Business Intelligence)', 'inc-bi.aspx')">
								<div>
									<img class="icon" src="images/icon/bi.png" />
								</div>
								<div class="name">
									<span>BI</span>
								</div>
							</div>
						</li>
					</ul>
					<div class="clear"></div>
				</div>
			</div>
			<!-- / thong ke -->
			
			<!-- import -->
			<div class="tab-content" id="tab-import">
			    <div class="sc_menu">
					<ul class="sc_menu">
					    <li>
					        <div class="items" id="import-item" title="Import" onclick="add_tab('Import dữ liệu', 'inc-import.aspx')">
								<div>
									<img class="icon" src="images/icon/danhsachmenu.png" />
								</div>
								<div class="name">
									<span>Import</span>
								</div>
							</div>
					    </li>
					</ul>
				</div>
			</div>
            <!-- / import -->

			<!-- he thong -->
			<div class="tab-content" id="tab-hethong">
				<div class="sc_menu">
					<ul class="sc_menu">
						<li>
							<div class="items" id="menu" title="D/S menu"
								onclick="add_tab('D/S menu', 'inc-menu.aspx')"">
								<div>
									<img class="icon" src="images/icon/danhsachmenu.png" />
								</div>
								<div class="name">
									<span>D/S menu</span>
								</div>
							</div>
						</li>
						<li>
							<div class="items" id="vaitro" title="Vai trò"
								onclick="add_tab('Vai trò', 'inc-vaitro.aspx')">
								<div>
									<img class="icon" src="images/icon/vaitro.png" />
								</div>
								<div class="name">
									<span>Vai trò</span>
								</div>
							</div>
						</li>
						<li>
							<div class="items" id="nhom" title="Nhóm"
								onclick="add_tab('Nhóm', 'inc-nhom.aspx')">
								<div>
									<img class="icon" src="images/icon/vaitro.png" />
								</div>
								<div class="name">
									<span>Nhóm</span>
								</div>
							</div>
						</li>
						<li>
							<div class="items" id="users" title="Users"
								onclick="add_tab('D/S Tài khoản', 'inc-users.aspx')">
								<div>
									<img class="icon" src="images/icon/danhsachtaikhoan.png" />
								</div>
								<div class="name">
									<span>D/S Tài khoản</span>
								</div>
							</div>
						</li>
						<li>
							<div class="items" id="phanquyen" title="Phân quyền"
								onclick="add_tab('Phân quyền', 'inc-phanquyen.aspx')">
								<div>
									<img class="icon" src="images/icon/phanquyen.png" />
								</div>
								<div class="name">
									<span>Phân quyền</span>
								</div>
							</div>
						</li>
						<li>
							<div class="items" id="quocgia" title="Quốc gia"
								onclick="add_tab('Quốc gia', 'inc-quocgia.aspx')"">
								<div>
									<img class="icon" src="images/icon/quocgia.png" />
								</div>
								<div class="name">
									<span>Quốc gia</span>
								</div>
							</div>
						</li>
						<li>
							<div class="items" id="khuvuc" title="Khu vực"
								onclick="add_tab('Khu vực', 'inc-khuvuc.aspx')"">
								<div>
									<img class="icon" src="images/icon/khuvuc.png" />
								</div>
								<div class="name">
									<span>Khu vực</span>
								</div>
							</div>
						</li>
						<li>
							<div class="items" id="loaidoitackinhdoanh"
								title="Loại đối tác kinh doanh"
								onclick="add_tab('Loại đối tác kinh doanh', 'inc-loaidoitackinhdoanh.aspx')"">
								<div>
									<img class="icon" src="images/icon/loaidoitac.png" />
								</div>
								<div class="name">
									<span>Loại Đối Tác</span>
								</div>
							</div>
						</li>
						<%--<li>
						            <div class="items"  id="loaitrangthai" title="Loại trạng thái" onclick="add_tab('Loại trạng thái', 'inc-loai-trangthai.aspx')"">
							            <div><img class="icon" src="images/icon/product.png"/></div>
							            <div class="name">
								            <span>Loại trạng thái</span>
							            </div>
						            </div>
            						</li><li>--%>
            			<%--<li>
						    <div class="items" id="trangthai" title="Trạng thái"
							    onclick="add_tab('Trạng thái', 'inc-trangthai.aspx')"">
							    <div>
								    <img class="icon" src="images/icon/trangthai.png" />
							    </div>
							    <div class="name">
								    <span>Trạng thái</span>
							    </div>
						    </div>
						</li>--%>
						<li>
							<div class="items" id="doitackinhdoanh"
								title="Đối tác kinh doanh"
								onclick="add_tab('Đối tác kinh doanh', 'inc-doitackinhdoanh.aspx')"">
								<div>
									<img class="icon" src="images/icon/doitac.png" />
								</div>
								<div class="name">
									<span>Đ/T Kinh Doanh</span>
								</div>
							</div>
						</li>
						<%--<li>
							<div class="items" id="Div2" title="Kiểu đóng gói"
								onclick="add_tab('Kiểu đóng gói', 'inc-kieudonggoi.aspx')"">
								<div>
									<img class="icon" src="images/icon/kieudonggoi.png" />
								</div>
								<div class="name">
									<span>Kiểu đóng gói</span>
								</div>
							</div>
						</li>
						<li>
							<div class="items" id="hinhthucdonggoi"
								title="Hình thức đóng gói"
								onclick="add_tab('Hình thức gói', 'inc-hinhthucdonggoi.aspx')"">
								<div>
									<img class="icon" src="images/icon/hinhthucdonggoi.png" />
								</div>
								<div class="name">
									<span>H/T đóng gói</span>
								</div>
							</div>
						</li>--%>
						
						<li>
							<div class="items" id="quytacmota"
								title="Quy tắc mô tả"
								onclick="add_tab('Quy tắc mô tả', 'inc-quytacmota.aspx')"">
								<div>
									<img class="icon" src="images/icon/hinhthucdonggoi.png" />
								</div>
								<div class="name">
									<span>Q/T Mô tả</span>
								</div>
							</div>
						</li>
						
						<li>
							<div class="items" id="bocai"
								title="Quy tắc bộ cái"
								onclick="add_tab('Quy tắc bộ cái', 'inc-quytacbocai.aspx')"">
								<div>
									<img class="icon" src="images/icon/hinhthucdonggoi.png" />
								</div>
								<div class="name">
									<span>Q/T Bộ cái</span>
								</div>
							</div>
						</li>
						
						<li>
							<div class="items" id="mailtemplate"
								title="Mail Template"
								onclick="add_tab('Mail Template', 'inc-mailtemplate.aspx')"">
								<div>
									<img class="icon" src="images/icon/hinhthucdonggoi.png" />
								</div>
								<div class="name">
									<span>Mail Template</span>
								</div>
							</div>
						</li>
						
						

                        
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
						<img style="padding-top: 130px" src="images/anco_banner.png" />
					</div>
				</DIV>
			</DIV>
		</DIV>
	</DIV>


	<div class="ui-layout-south bg">
		<!-- footer -->
		<div id="lay-footer" class="lay-content">
			<!-- content -->
			<div style="padding-top: 4px; font-family: arial; font-size: 10px">eMan
				SCM 3.0 2010 - 2012</div>
			<!--/ content -->
		</div>
		<!--/ footer -->
	</div>
</body>
</html>

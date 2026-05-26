using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class jqGrid : System.Web.UI.UserControl
{
    protected String _colNames /* example "['Id', 'No', 'Date']" */
        , _toppager = "false"
        , _caption = ""
        , _urlFileAction
        , _autoWidth = "true"
        , _dataType = "xml"
        , _sortName
        , _rowList = "[10, 20, 50, 200]"
        , _sortOrder = "desc"
        , _rowNumbers
        , _rowNum = "20"
        , _colModel
        , _multiSelect = "false"
        , _showAdd = "false"
        , _showEdit = "false"
        , _showDel = "false"
        , _showSearch = "false"
        , _showView = "true"
        , _showRefresh = "true"
        , _cellEdit = "false"
        , _afterRefresh = ""
        , _validation = ""
        , _height = "390"
        , _width = "500"
        , _addFormOptions = ""
        , _editFormOptions = ""
        , _delFormOptions = ""
        , _gridComplete = "" // $(this).setGridHeight($(window).height() - 220);
         , _gridBeforeRequest = "" // $(this).setGridHeight($(window).height() - 220);
        , _loadError = "alert('error')" // alert('error')
        , _filterToolbar = ""
        , _onSelectRow = ""
        , _ondblClickRow = ""
        , _toolbar = ""
        , _funcImportPicture = ""
        , _funcTaoKy = ""
        , _funcPO = ""
        , _funcPI = ""
        , _funcQoByProducts = ""
        , _funcActiveQO = ""
        , _funcActivePO = ""
        , _funcActivePI = ""
        , _postData = ""
        , _funcNangLuc = ""
        , _funcViewCapacity = ""
        , _funcTaoTuanNangLuc = ""
        , _viewFormOptions = ""
        , _funcChiaTachPOTaoPI = ""
        , _funcTaoPhieuXuat = ""
        , _funcActivePhieuXuat = ""
        , _funcTaoPhieuNhapXuat = ""
        , _funcActiveNhapKho = ""
        , _funcDeactiveNhapKho = ""
        , _funcTaoPhieuXuatKho = ""
        , _funcTaoPKLCungPO = ""
        , _funcTaoPKLCungKH = ""
        , _funcCopyPhienBan = ""
        , _funcActive = ""
        , _funcSendMail = ""
        , _btnPrint = ""
        , _funcTaoPhieuXuatKhoN = ""
        , _funcKetThuc = ""
        , _autofuncKetThuc = ""
        , _overrideAdd = ""
        , _overrideEdit = ""
        , _overrideDelete = ""
        , _funcOpenClose = ""
        , _funcXemNangLuc = ""
        , _funcChangeStatus = ""
        , _funcPhanBoCocInv = ""
        , _GuiDonHang = "" //Gui don hang
        , _InPhieuChi = "" //In Phieu Chi
        , _funcCopyDonHang = "" //Copy don hang po
        , _funcReturnDonHang = "" //Tra don hang po ve soan thao
        , _funcReturnPI = "" //Tra pi ve soan thao
        , _funcviewCapacityRaBo = "" //Xem nang luc cai
        , _funcConfirmPI = "" //Xac nha PI
        , _funcCoppyChiTietNangLuc = ""
        , _funcReturnPKL = ""

        ;
    public String Toppager
    {
        get { return Toppager; }
        set { Toppager = value; }
    }

    public String FuncPhanBoCocInv
    {
        get { return _funcPhanBoCocInv; }
        set { _funcPhanBoCocInv = value; }
    }

    public String FuncChangeStatus
    {
        get { return _funcChangeStatus; }
        set { _funcChangeStatus = value; }
    }

    public String FuncXemNangLuc
    {
        get { return _funcXemNangLuc; }
        set { _funcXemNangLuc = value; }
    }

    public String FuncOpenClose
    {
        get { return _funcOpenClose; }
        set { _funcOpenClose = value; }
    }

    public String OverrideDelete
    {
        get { return _overrideDelete; }
        set { _overrideDelete = value; }
    }

    public String OverrideEdit
    {
        get { return _overrideEdit; }
        set { _overrideEdit = value; }
    }

    public String OverrideAdd
    {
        get { return _overrideAdd; }
        set { _overrideAdd = value; }
    }

    public String FuncKetThuc
    {
        get { return _funcKetThuc; }
        set { _funcKetThuc = value; }
    }

    public String FuncAutoKetThuc
    {
        get { return _autofuncKetThuc; }
        set { _autofuncKetThuc = value; }
    }

    public String FuncTaoPhieuXuatKhoN
    {
        get { return _funcTaoPhieuXuatKhoN; }
        set { _funcTaoPhieuXuatKhoN = value; }
    }

    public String FuncQoByProducts
    {
        get { return _funcQoByProducts; }
        set { _funcQoByProducts = value; }
    }

    public String FuncSendMail
    {
        get { return _funcSendMail; }
        set { _funcSendMail = value; }
    }

    public String FuncActive
    {
        get { return _funcActive; }
        set { _funcActive = value; }
    }

    public String FuncCopyPhienBan
    {
        get { return _funcCopyPhienBan; }
        set { _funcCopyPhienBan = value; }
    }

    public String FuncActivePI
    {
        get { return _funcActivePI; }
        set { _funcActivePI = value; }
    }

    public String FuncTaoPKLCungKH
    {
        get { return _funcTaoPKLCungKH; }
        set { _funcTaoPKLCungKH = value; }
    }

    public String FuncTaoPKLCungPO
    {
        get { return _funcTaoPKLCungPO; }
        set { _funcTaoPKLCungPO = value; }
    }

    public String FuncTaoPhieuXuatKho
    {
        get { return _funcTaoPhieuXuatKho; }
        set { _funcTaoPhieuXuatKho = value; }
    }

    public String FuncActiveNhapKho
    {
        get { return _funcActiveNhapKho; }
        set { _funcActiveNhapKho = value; }
    }

    public String FuncDeactiveNhapKho
    {
        get { return _funcDeactiveNhapKho; }
        set { _funcDeactiveNhapKho = value; }
    }

    public String FuncTaoPhieuNhapXuat
    {
        get { return _funcTaoPhieuNhapXuat; }
        set { _funcTaoPhieuNhapXuat = value; }
    }

    public String BtnPrint
    {
        get { return _btnPrint; }
        set { _btnPrint = value; }
    }

    public String FuncActivePhieuXuat
    {
        get { return _funcActivePhieuXuat; }
        set { _funcActivePhieuXuat = value; }
    }

    public String FuncTaoPhieuXuat
    {
        get { return _funcTaoPhieuXuat; }
        set { _funcTaoPhieuXuat = value; }
    }

    public String CellEdit
    {
        get { return _cellEdit; }
        set { _cellEdit = value; }
    }

    public String AfterRefresh
    {
        get { return _afterRefresh; }
        set { _afterRefresh = value; }
    }

    public String ShowRefresh
    {
        get { return _showRefresh; }
        set { _showRefresh = value; }
    }

    public String FuncChiaTachPOTaoPI
    {
        get { return _funcChiaTachPOTaoPI; }
        set { _funcChiaTachPOTaoPI = value; }
    }

    public String ViewFormOptions
    {
        get { return _viewFormOptions; }
        set { _viewFormOptions = value; }
    }

    public String FuncTaoTuanNangLuc
    {
        get { return _funcTaoTuanNangLuc; }
        set { _funcTaoTuanNangLuc = value; }
    }

    public String FuncViewCapacity
    {
        get { return _funcViewCapacity; }
        set { _funcViewCapacity = value; }
    }

    public String FuncNangLuc
    {
        get { return _funcNangLuc; }
        set { _funcNangLuc = value; }
    }

    public String PostData
    {
        get { return _postData; }
        set { _postData = value; }
    }

    public String FuncActivePO
    {
        get { return _funcActivePO; }
        set { _funcActivePO = value; }
    }

    public String FuncPI
    {
        get { return _funcPI; }
        set { _funcPI = value; }
    }

    public String FuncActiveQO
    {
        get { return _funcActiveQO; }
        set { _funcActiveQO = value; }
    }


    public String FuncPO
    {
        get { return _funcPO; }
        set { _funcPO = value; }
    }

    public String FuncTaoKy
    {
        get { return _funcTaoKy; }
        set { _funcTaoKy = value; }
    }

    public String FuncImportPicture
    {
        get { return _funcImportPicture; }
        set { _funcImportPicture = value; }
    }


    public String toolbar
    {
        get { return _toolbar; }
        set { _toolbar = value; }
    }

    public String OndblClickRow
    {
        get { return _ondblClickRow; }
        set { _ondblClickRow = value; }
    }

    public String Width
    {
        get { return _width; }
        set { _width = value; }
    }

    public String FilterToolbar
    {
        get { return _filterToolbar; }
        set { _filterToolbar = value; }
    }

    public String LoadError
    {
        get { return _loadError; }
        set { _loadError = value; }
    }

    public String AddFormOptions
    {
        get { return _addFormOptions; }
        set { _addFormOptions = value; }
    }

    public String EditFormOptions
    {
        get { return _editFormOptions; }
        set { _editFormOptions = value; }
    }

    public String DelFormOptions
    {
        get { return _delFormOptions; }
        set { _delFormOptions = value; }
    }


    public String Caption
    {
        get { return _caption; }
        set { _caption = value; }
    }

    public String OnSelectRow
    {
        get { return _onSelectRow; }
        set { _onSelectRow = value; }
    }

    public String GridComplete
    {
        get { return _gridComplete; }
        set { _gridComplete = value; }
    }

    public String GridBeforeRequest
    {
        get { return _gridBeforeRequest; }
        set { _gridBeforeRequest = value; }
    }

    public String Height
    {
        get { return _height; }
        set { _height = value; }
    }

    public String Validation
    {
        get { return _validation; }
        set { _validation = value; }
    }

    public String ShowSearch
    {
        get { return _showSearch; }
        set { _showSearch = value; }
    }

    public String ShowAdd
    {
        get { return _showAdd; }
        set { _showAdd = value; }
    }

    public String ShowEdit
    {
        get { return _showEdit; }
        set { _showEdit = value; }
    }

    public String ShowDel
    {
        get { return _showDel; }
        set { _showDel = value; }
    }

    public String MultiSelect
    {
        get { return _multiSelect; }
        set { _multiSelect = value; }
    }

    public String ColModel
    {
        get { return _colModel; }
        set { _colModel = value; }
    }


    public String RowNum
    {
        get { return _rowNum; }
        set { _rowNum = value; }
    }

    public String RowNumbers
    {
        get { return _rowNumbers; }
        set { _rowNumbers = value; }
    }

    public String SortOrder
    {
        get { return _sortOrder; }
        set { _sortOrder = value; }
    }

    public String RowList
    {
        get { return _rowList; }
        set { _rowList = value; }
    }

    public String SortName
    {
        get { return _sortName; }
        set { _sortName = value; }
    }

    public String DataType
    {
        get { return _dataType; }
        set { _dataType = value; }
    }

    public String ColNames
    {
        get { return _colNames; }
        set { _colNames = value; }
    }

    public String UrlFileAction
    {
        get { return _urlFileAction; }
        set { _urlFileAction = value; }
    }

    public String AutoWidth
    {
        get { return _autoWidth; }
        set { _autoWidth = value; }
    }

    //_GuiDonHang
    public String GuiDonHang
    {
        get { return _GuiDonHang; }
        set { _GuiDonHang = value; }
    }
    //_InPhieuChi
    public String InPhieuChi
    {
        get { return _InPhieuChi; }
        set { _InPhieuChi = value; }
    }

    //_FuncCopyDonHang
    public String FuncCopyDonHang
    {
        get { return _funcCopyDonHang; }
        set { _funcCopyDonHang = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    //_FuncReturnDonHang
    public String ReturnDonHang
    {
        get { return _funcReturnDonHang; }
        set { _funcReturnDonHang = value; }
    }

    //_FuncReturnPI
    public String ReturnPI
    {
        get { return _funcReturnPI; }
        set { _funcReturnPI = value; }
    }

    //_FuncConfirmPI
    public String funcConfirmPI
    {
        get { return _funcConfirmPI; }
        set { _funcConfirmPI = value; }
    }

    //_FuncReturnPKL
    public String ReturnPKL
    {
        get { return _funcReturnPKL; }
        set { _funcReturnPKL = value; }
    }

    //_viewCapacityRaBo
    public String FuncViewCapacityRaBo
    {
        get { return _funcviewCapacityRaBo; }
        set { _funcviewCapacityRaBo = value; }
    }

    public String FuncCoppyChiTietNangLuc
    {
        get { return _funcCoppyChiTietNangLuc; }
        set { _funcCoppyChiTietNangLuc = value; }
    }
}

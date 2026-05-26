using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class SendMail_PaymentRequest : System.Web.UI.Page
{
    public LinqDBDataContext db = new LinqDBDataContext();
    public string logo = "", sothapphan = "", inPDF = "";
    public InfoPrint infoPrint = PrintAnco2.GetInfoPrint();
    public HttpContext context = HttpContext.Current;
    protected void Page_Load(object sender, EventArgs e)
    {
        var context = HttpContext.Current;
        sothapphan = PrintAnco2.GetDecimal(context.Request.QueryString["stp"], 1);
        string nameTemp = "(CT) Payment Request.xls";
        string nameRpt = "Payment Request";
        string sql = CreateSql(context);

        inPDF = "2";
        var task = new System.Threading.Tasks.Task(() =>
        {
            viewReport(sql);
        });

        var compineF = Request.QueryString["compineF"];
        nameRpt = string.IsNullOrWhiteSpace(compineF) ? nameRpt + "-" + DateTime.Now.ToString("ddMMyy") : nameRpt;
        PrintAnco2.exportDataWithType(task, sql, inPDF, nameTemp, nameRpt, ReportViewer1, string.IsNullOrWhiteSpace(compineF), !string.IsNullOrWhiteSpace(compineF));
    }

    public void viewReport(String SqlQuery)
    {

    }

    public string CreateSql(HttpContext context)
    {
        String pklId = context.Request.QueryString["pklId"];
        var pdkInvoice = db.c_packinginvoices.Where(s => s.c_packinginvoice_id == pklId).FirstOrDefault();

        // Lấy packing list - invoice
        var info = (from pkl in db.c_packinginvoices
                    join cb in db.md_cangbiens on pkl.noidi equals cb.md_cangbien_id
                    where pkl.c_packinginvoice_id.Equals(pklId)
                    select new { pkl, cb }).Single();

        // Lấy số lượng sản phẩm Packing List Invoice
        var totalQuantity = (from p in db.c_dongpklinvs where p.c_packinginvoice_id.Equals(pklId) select p.soluong).Sum();



        // Lấy thông tin khách hàng
        var dtkd = (from pkl in db.c_packinginvoices
                    join dt in db.md_doitackinhdoanhs on pkl.md_doitackinhdoanh_id equals dt.md_doitackinhdoanh_id
                    where pkl.c_packinginvoice_id.Equals(pklId)
                    select dt).Single();

        // Lấy danh sách đơn hàng thuộc invoice đang chọn
        var dsDonHang = from p in db.c_packinginvoices
                        join dp in db.c_dongpklinvs on p.c_packinginvoice_id equals dp.c_packinginvoice_id
                        join dh in db.c_donhangs on dp.c_donhang_id equals dh.c_donhang_id
                        where p.c_packinginvoice_id.Equals(pklId)
                        select new { dh };

        // Lấy danh sách số chứng từ đơn hàng
        var chungTuDonHang = (from dh in dsDonHang select new { dh.dh.sochungtu }).Distinct();

        // Lấy thông tin ngân hàng
        var nganhang = db.md_nganhangs.Single(
                    p => p.md_nganhang_id.Equals(dsDonHang.Take(1).Single().dh.md_nganhang_id)
                );

        //Chuyển danh sách đơn hàng sang chuỗi
        string dhStr = string.Join(", ", chungTuDonHang.Select(s=>s.sochungtu).ToList());

        // Lấy tổng số kiện
        var soKien = (from nxk in db.c_nhapxuats
                      join dnxk in db.c_dongnhapxuats on nxk.c_nhapxuat_id equals dnxk.c_nhapxuat_id
                      where nxk.c_donhang_id.Equals(dsDonHang.Take(1).Single().dh.c_donhang_id)
                      select new { dnxk.sokien_thucte });

        Nullable<decimal> toSoKien = soKien.Sum(p => p.sokien_thucte);

        String noidungNCC1 = "", noidungNCC2 = "", noidungNCC3 = "", noidungNCC4 = "", noidungNCC5 = "",
               noidungNCC6 = "", noidungNCC7 = "", noidungNCC8 = "", noidungNCC9 = "", link = "";
        if (pdkInvoice.hangcua == "VG")
        {
            noidungNCC1 = "VINH GIA COMPANY LIMITED";
            noidungNCC2 = "Northern Chu Lai Industrial Zone, Nui Thanh Commune, Danang City, Viet Nam";
            noidungNCC3 = "Tel: (84-235) 3567393   Fax: (84-235) 3567494";
            noidungNCC4 = "VINH GIA COMPANY LIMITED";
            noidungNCC5 = "046.137.3860862";

            noidungNCC6 = "JOINT STOCK COMMERCIAL BANK FOR FOREIGN TRADE OF VIET NAM - TAN BINH DUONG BRANCH,";
            noidungNCC7 = "NO 16, LE TRONG TAN STREET, BINH DUONG 2 QUARTER, DI AN WARD, HO CHI MINH CITY, VIET NAM.";
            noidungNCC8 = "Tel: (84 - 274) 3792 158 - Fax: (84 - 274) 3793 970,";
            noidungNCC9 = "Swift code: BFTVVNVX";

            link = "images/more/ckcdct.jpg";
        }
        else
        {
            noidungNCC1 = "HOANG HUY HUNG COMPANY LIMITED";
            noidungNCC2 = "Tam Hiep 2 Urban residential area,  Nui Thanh Commune, Danang City, Viet Nam";
            noidungNCC3 = "Tel: (84-235) 3567393   Fax: (84-235) 3567494";
            noidungNCC4 = "HOANG HUY HUNG COMPANY LIMITED";
            noidungNCC5 = "1021045695";

            noidungNCC6 = "JOINT STOCK COMMERCIAL BANK FOR FOREIGN TRADE OF VIETNAM";
            noidungNCC7 = "35 TRAN HUNG DAO STREET, BAN THACH WARD, DA NANG CITY, VIET NAM";
            noidungNCC8 = "";
            noidungNCC9 = "Swift code: BFTVVNVX065";

            link = "images/more/hhh.jpg";
        }
        var nd_Atbank = noidungNCC6  + "\r\n" + noidungNCC7  + "\r\n" + noidungNCC8 + "\r\n" + noidungNCC9;

        string sql = string.Format(@"
            select '{0}' as sopo, '{1}' as ngayguimail, '{2}' as tenkh, '{3}' as chungloai, '{4}' as tentau, '{5}' as noidi
            , '{6}' as noiden, '{7}' as shipmenttime, '{8}' as sobil, '{9}' as slinvoice, '{10}' as sokien, '{11}' as tongtieninvoice
            , '{12}' as soinvoice, '{13}' as datcoc, '{14}' as giatriconlai, N'{15}' as nganhang
            , N'{16}' as picture_signature
            , N'{17}' as noidungNCC1
            , N'{18}' as noidungNCC2
            , N'{20}' as noidungNCC4
            , N'{21}' as noidungNCC5
            , N'{22}' as nd_Atbank
 
        "
        , dhStr, DateTime.Now.ToString("dd/MMM/yyyy"), dtkd.ten_dtkd.Replace("'", "''"), info.pkl.commodity, info.pkl.mv, info.cb.ten_cangbien.Replace("'", "''")
        , info.pkl.noiden.Replace("'", "''"), info.pkl.etd.Value.ToString("dd/MMM/yyyy"), info.pkl.blno, totalQuantity == null ? "0" : totalQuantity.Value.ToString()
        , toSoKien == null ? "0" : toSoKien.Value.ToString(), info.pkl.totalgross == null ? "0" : info.pkl.totalgross.Value.ToString()
        , info.pkl.so_inv, info.pkl.tiendatcoc == null ? "0" : info.pkl.tiendatcoc.Value.ToString(), info.pkl.tienconlai == null ? "0" : info.pkl.tienconlai.Value.ToString()
        , nganhang.thongtin.Replace("'", "''")
        , link
        , noidungNCC1 , noidungNCC2 , noidungNCC3 , noidungNCC4 , noidungNCC5
        , nd_Atbank);

        //throw new ArgumentNullException(sql);
        return sql;
    }
}
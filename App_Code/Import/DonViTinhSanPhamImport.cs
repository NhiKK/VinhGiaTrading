using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using ExcelLibrary.SpreadSheet;


public class DonViTinhSanPhamImport
{
    private String maDVT, tenDVT;

    public String MaDVT { get; set; }
    public String TenDVT { get; set; }

    public DonViTinhSanPhamImport()
    {
    
    }

    public void Import()
    {
        String insert = "insert into md_donvitinhsp(md_donvitinhsp_id, ma_edi, ten_dvt) value( " +
            " (select RIGHT(sys.fn_sqlvarbasetostr(HashBytes('MD5', cast( newid() as nvarchar(255) )),32)) " +
            " , @ma_edi, @ten_dvt)";
        mdbc.ExcuteNonQuery(insert
            , "@ma_edi", maDVT
            , "@ten_dvt", tenDVT);
    }
}

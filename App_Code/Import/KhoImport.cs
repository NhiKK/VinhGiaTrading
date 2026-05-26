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


public class KhoImport
{
    private String maKho, tenKho;

    public String MaKho { get; set; }
    public String Tenkho { get; set; }

    public KhoImport()
    {
    
    }

    public void Import()
    {
        String insert = "insert into md_kho(md_kho_id, ma_kho, ten_kho) value( " +
            " (select RIGHT(sys.fn_sqlvarbasetostr(HashBytes('MD5', cast( newid() as nvarchar(255) )),32)) " +
            " , @ma_kho, @ten_kho)";
        mdbc.ExcuteNonQuery(insert
            , "@ma_kho", maKho
            , "@ten_kho", tenKho);
    }
}

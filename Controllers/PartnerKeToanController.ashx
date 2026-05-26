<%@ WebHandler Language="C#" Class="PartnerKeToanController" %>

using System;
using System.Web;
using System.Data.Linq;
using System.Linq;
using System.Data;

public class PartnerKeToanController : IHttpHandler
{
    private LinqDBDataContext db = new LinqDBDataContext();

    public void ProcessRequest(HttpContext context)
    {
        String action, oper;
        action = context.Request.QueryString["action"];
        oper = context.Request.Form["oper"];
        switch (action)
        {
            case "getop_loaidtkd":
                this.getop_loaidtkd(context);
                break;
            default:
                switch (oper)
                {
                    case "del":
                        this.del(context);
                        break;
                    case "edit":
                        this.edit(context);
                        break;
                    case "add":
                        this.add(context);
                        break;
                    default:
                        this.load(context);
                        break;
                }
                break;
        }
    }

    public void getop_loaidtkd(HttpContext context) {
        String sql = "select md_loaidtkd_id, ten_loaidtkd from md_loaidtkd where hoatdong = 1 and ma_loaidtkd in ('NCCVT') order by ma_loaidtkd asc ";
        SelectHtmlControl s = new SelectHtmlControl(sql);
        context.Response.Write(s.ToString());
    }
    
    public void edit(HttpContext context)
    {
        String h = context.Request.Form["hoatdong"].ToLower();
        bool hd= false, ncc = false;
        
        if (h.Equals("on") || h.Equals("true"))
        { hd = true; }
	
        md_doitackinhdoanh dtkd = db.md_doitackinhdoanhs.FirstOrDefault(p => p.ma_dtkd.Equals(context.Request.Form["ma_dtkd"]) && !p.md_doitackinhdoanh_id.Equals(context.Request.Form["md_doitackinhdoanh_id"]));
        if (dtkd == null)
        {
            md_doitackinhdoanh m = db.md_doitackinhdoanhs.Where(p => p.md_doitackinhdoanh_id == context.Request.Form["id"]).FirstOrDefault();
            m.md_loaidtkd_id = context.Request.Form["tenloai"];
            //m.md_quocgia_id = context.Request.Form["tenqg"];
            //m.md_khuvuc_id = context.Request.Form["tenkv"];
			//m.md_nguondtkd_id = context.Request.Form["md_nguondtkd_id"];
            //m.md_banggia_id = context.Request.Form["banggia"] == "" ? null : context.Request.Form["banggia"];
            m.ma_dtkd = context.Request.Form["ma_dtkd"];
            m.ten_dtkd = context.Request.Form["ten_dtkd"];
            m.daidien = context.Request.Form["daidien"];
            m.chucvu = context.Request.Form["chucvu"];
            m.tel = context.Request.Form["tel"];
            m.fax = context.Request.Form["fax"];
            //m.email = context.Request.Form["email"];
            //m.url = context.Request.Form["url"];
            m.diachi = context.Request.Form["diachi"];
            m.so_taikhoan = context.Request.Form["so_taikhoan"];
            m.nganhang = context.Request.Form["nganhang"];
            m.masothue = context.Request.Form["masothue"];
            //m.isncc = ncc;
            //m.md_cangbien_id = context.Request.Form["md_cangbien_id"];

            m.mota = context.Request.Form["mota"];
            m.hoatdong = hd;
            m.nguoicapnhat = UserUtils.getUser(context);
            m.ngaycapnhat = DateTime.Now;

            db.SubmitChanges();
        }
        else
        {
            jqGridHelper.Utils.writeResult(0, "Mã đối tác đã tồn tại!");
        }
    }

    public void add(HttpContext context)
    {
        String h = context.Request.Form["hoatdong"].ToLower();
        bool hd = false, ncc = false;

        if (h.Equals("on") || h.Equals("true"))
        { hd = true; }

        md_doitackinhdoanh dtkd = db.md_doitackinhdoanhs.FirstOrDefault(p => p.ma_dtkd.Equals(context.Request.Form["ma_dtkd"]));
        if (dtkd == null)
        {
            md_doitackinhdoanh mnu = new md_doitackinhdoanh
            {
                md_doitackinhdoanh_id = Security.EncodeMd5Hash(DateTime.Now.ToString("yyyyMMddddhhmmss")),
                md_loaidtkd_id = context.Request.Form["tenloai"],
                md_quocgia_id = context.Request.Form["tenqg"],
                md_khuvuc_id = context.Request.Form["tenkv"],
				md_nguondtkd_id = context.Request.Form["md_nguondtkd_id"],
                md_banggia_id = context.Request.Form["banggia"] == "" ? null : context.Request.Form["banggia"],
                ma_dtkd = context.Request.Form["ma_dtkd"],
                ten_dtkd = context.Request.Form["ten_dtkd"],
                daidien = context.Request.Form["daidien"],
                chucvu = context.Request.Form["chucvu"],
                tel = context.Request.Form["tel"],
                fax = context.Request.Form["fax"],
                email = context.Request.Form["email"],
                url = context.Request.Form["url"],
                diachi = context.Request.Form["diachi"],
                so_taikhoan = context.Request.Form["so_taikhoan"],
                nganhang = context.Request.Form["nganhang"],
                masothue = context.Request.Form["masothue"],
                tong_congno = 0,
                isncc = ncc,
                md_cangbien_id = context.Request.Form["md_cangbien_id"],
                islienhe = false,

                mota = context.Request.Form["mota"],
                hoatdong = true,
                nguoitao = UserUtils.getUser(context),
                nguoicapnhat = UserUtils.getUser(context),
                ngaytao = DateTime.Now,
                ngaycapnhat = DateTime.Now
            };

            db.md_doitackinhdoanhs.InsertOnSubmit(mnu);
            db.SubmitChanges();
        }
        else
        {
            jqGridHelper.Utils.writeResult(0, "Mã đối tác đã tồn tại!");
        }
    }


    public void del(HttpContext context)
    {
        String id = context.Request.Form["id"];
        id = id.Insert(0, "'");
        id = id.Insert(id.Length, "'");
        id = id.Replace(",", "','");
        // Kiem tra NCC da dc su dung
        string sqlCount = @"DECLARE @SearchStr nvarchar(MAX) = " + id + @"
							DECLARE @Results TABLE (ColumnName nvarchar(370), ColumnValue nvarchar(3630))
							SET NOCOUNT ON
							DECLARE @TableName nvarchar(256), @ColumnName nvarchar(128), @SearchStr2 nvarchar(110)
							SET  @TableName = ''
							SET @SearchStr2 = QUOTENAME('%' + @SearchStr + '%','''')
							WHILE @TableName IS NOT NULL
							BEGIN
								SET @ColumnName = ''
								SET @TableName = 
								(
									SELECT MIN(QUOTENAME(TABLE_SCHEMA) + '.' + QUOTENAME(TABLE_NAME))
									FROM     INFORMATION_SCHEMA.TABLES
									WHERE         TABLE_TYPE = 'BASE TABLE'
										AND    QUOTENAME(TABLE_SCHEMA) + '.' + QUOTENAME(TABLE_NAME) > @TableName
										AND    OBJECTPROPERTY(
												OBJECT_ID(
													QUOTENAME(TABLE_SCHEMA) + '.' + QUOTENAME(TABLE_NAME)
													 ), 'IsMSShipped'
													   ) = 0
								)
								WHILE (@TableName IS NOT NULL) AND (@ColumnName IS NOT NULL)
								BEGIN
									SET @ColumnName =
									(
										SELECT MIN(QUOTENAME(COLUMN_NAME))
										FROM     INFORMATION_SCHEMA.COLUMNS
										WHERE         TABLE_SCHEMA    = PARSENAME(@TableName, 2)
											AND    TABLE_NAME    = PARSENAME(@TableName, 1)
											AND    DATA_TYPE IN ('char', 'varchar', 'nchar', 'nvarchar', 'int', 'decimal')
											AND    QUOTENAME(COLUMN_NAME) > @ColumnName
									)
									IF @ColumnName IS NOT NULL
									BEGIN
										INSERT INTO @Results
										EXEC
										(
											'SELECT ''' + @TableName + ''', LEFT(' + @ColumnName + ', 3630) 
											FROM ' + @TableName + ' (NOLOCK) ' +
											' WHERE ' + @ColumnName + ' LIKE ' + @SearchStr2
										)
									END
								END    
							END
							SELECT count(distinct ColumnName) as count FROM @Results WHERE ColumnName != '[dbo].[md_doitackinhdoanh]'";
		int count = (int)mdbc.ExecuteScalar(sqlCount);
		if(count > 0)
		{
			jqGridHelper.Utils.writeResult(0, "ĐT/ Kinh doanh đang được sử dụng, không thể xóa!");
		}
		else
		{
			String sql = "delete from md_doitackinhdoanh where md_doitackinhdoanh_id IN (" + id + ")";
			mdbc.ExcuteNonQuery(sql);
			jqGridHelper.Utils.writeResult(1, "Xóa thành công.");
		}
    }

    public void load(HttpContext context)
    {
        String isncc = context.Request.QueryString["isncc"];
        String productId = context.Request.QueryString["productId"];
        
        context.Response.ContentType = "text/xml";
        context.Response.Charset = "UTF-8";

        //// filter
        String filter = "";
        bool _search = bool.Parse(context.Request.QueryString["_search"]);
        if (_search)
        {
            String _filters = context.Request.QueryString["filters"];
            jqGridHelper.Filter f = jqGridHelper.Filter.CreateFilter(_filters);
            filter = f.ToScript();
        }
        
        String sqlCount = @"SELECT COUNT(1) AS count 
        FROM md_doitackinhdoanh dtkd 
        LEFT JOIN md_quocgia qg ON dtkd.md_quocgia_id = qg.md_quocgia_id
        LEFT JOIN md_khuvuc kv ON dtkd.md_khuvuc_id = kv.md_khuvuc_id
        LEFT JOIN md_loaidtkd ldt ON dtkd.md_loaidtkd_id = ldt.md_loaidtkd_id
		LEFT JOIN md_nguondtkd ndt ON dtkd.md_nguondtkd_id = ndt.md_nguondtkd_id
        WHERE 1=1 and dtkd.hoatdong = 1 {0} and ldt.ma_loaidtkd in ('NCCVT') " + filter;

        if (isncc != null)
        {
            sqlCount = String.Format(sqlCount, " AND isncc = " + isncc);
        }
        else {
            sqlCount = String.Format(sqlCount, "");
        }
        
        int page = int.Parse(context.Request.QueryString["page"]);
        int limit = int.Parse(context.Request.QueryString["rows"]);
        String sidx = context.Request.QueryString["sidx"];
        String sord = context.Request.QueryString["sord"];
        int total_page;
        int count = (int)mdbc.ExecuteScalar(sqlCount);
        int start, end;

        if (count > 0)
        {
            total_page = (int)Math.Ceiling(1.0 * count / limit);
        }
        else
        {
            total_page = 0;
        }

        if (page > total_page) page = total_page;
        start = limit * page - limit;
        end = (page * limit) + 1;

        if (sidx.Equals("") || sidx == null)
        {
            sidx = "dtkd.ma_dtkd";
        }

        string strsql = @"SELECT * FROM( 
            SELECT dtkd.md_doitackinhdoanh_id, dtkd.md_loaidtkd_id, dtkd.md_quocgia_id, dtkd.md_khuvuc_id, 
			dtkd.ma_dtkd, dtkd.ten_dtkd, dtkd.daidien, dtkd.chucvu, dtkd.tel, dtkd.fax, dtkd.email, dtkd.url, dtkd.diachi, 
			dtkd.md_banggia_id, dtkd.so_taikhoan, dtkd.nganhang, dtkd.masothue, dtkd.tong_congno, dtkd.isncc, 
			dtkd.ngaytao, dtkd.nguoitao, dtkd.ngaycapnhat, dtkd.nguoicapnhat, dtkd.mota, dtkd.hoatdong, dtkd.islienhe, dtkd.isdocquyen, dtkd.md_cangbien_id, 
			qg.ten_quocgia as tenqg, kv.ten_khuvuc as tenkv, ldt.ten_loaidtkd as tenloai, 
            (case when md_cangbien_id is not null then (select ten_cangbien from md_cangbien where md_cangbien_id = dtkd.md_cangbien_id)
                    else null end) as cangbien, 
			ndt.ten_nguondtkd as md_nguondtkd_id, 
            ROW_NUMBER() OVER (ORDER BY " + sidx + " " + sord + @") as RowNum 
            FROM md_doitackinhdoanh dtkd 
			LEFT JOIN md_quocgia qg ON dtkd.md_quocgia_id = qg.md_quocgia_id 
            LEFT JOIN md_khuvuc kv ON dtkd.md_khuvuc_id = kv.md_khuvuc_id 
            LEFT JOIN md_loaidtkd ldt ON dtkd.md_loaidtkd_id = ldt.md_loaidtkd_id  
			LEFT JOIN md_nguondtkd ndt ON dtkd.md_nguondtkd_id = ndt.md_nguondtkd_id
            WHERE 1=1 and dtkd.hoatdong = 1 {0} and ldt.ma_loaidtkd in ('NCCVT') " + filter +
            ")P WHERE RowNum > @start AND RowNum < @end";
        
        if (isncc != null)
        {
            strsql = String.Format(strsql, " AND isncc = " + isncc);
        }
        else
        {
            strsql = String.Format(strsql, "");
        }
        
        System.Data.DataTable dt = mdbc.GetData(strsql, "@start", start, "@end", end);
        string xml = "<?xml version=\"1.0\" encoding=\"UTF-8\" ?>";
        xml += "<rows>";
        xml += "<page>" + page + "</page>";
        xml += "<total>" + total_page + "</total>";
        xml += "<records>" + count + "</records>";
        foreach (System.Data.DataRow row in dt.Rows)
        {
            xml += "<row>";
            xml += "<cell><![CDATA[" + row["md_doitackinhdoanh_id"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["tenloai"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["ma_dtkd"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["ten_dtkd"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["daidien"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["chucvu"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["tel"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["fax"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["email"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["url"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["diachi"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["tenqg"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["tenkv"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["md_banggia_id"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["so_taikhoan"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["nganhang"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["masothue"] + "]]></cell>";
            xml += "<cell><![CDATA[" + CultureInfoUtil.currencyUSD(row["tong_congno"]) + "]]></cell>";
            xml += "<cell><![CDATA[" + row["isncc"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["cangbien"] + "]]></cell>";
			xml += "<cell><![CDATA[" + row["md_nguondtkd_id"] + "]]></cell>";
            xml += "<cell><![CDATA[" + DateTime.Parse(row["ngaytao"].ToString()).ToString("dd/MM/yyyy") + "]]></cell>";
            xml += "<cell><![CDATA[" + row["nguoitao"] + "]]></cell>";
            xml += "<cell><![CDATA[" + DateTime.Parse(row["ngaycapnhat"].ToString()).ToString("dd/MM/yyyy") + "]]></cell>";
            xml += "<cell><![CDATA[" + row["nguoicapnhat"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["mota"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["hoatdong"] + "]]></cell>";
            xml += "</row>";
        }
        xml += "</rows>";
        context.Response.Write(xml);
    }

    public bool IsReusable
    {
        get { return false; }
    }
}

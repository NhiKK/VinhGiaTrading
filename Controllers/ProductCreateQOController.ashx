<%@ WebHandler Language="C#" Class="ProductCreateQOController" %>

using System;
using System.Web;

public class ProductCreateQOController : IHttpHandler
{
    
    public void ProcessRequest (HttpContext context) {
        this.load(context);
    }

    public void load(HttpContext context)
    {
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
            FROM md_sanpham sp with (nolock)
            , md_donggoi dg with (nolock)
            , md_donggoisanpham dgsp with (nolock)  
            WHERE 1=1 
            {0} 
            and dg.md_donggoi_id = dgsp.md_donggoi_id 
			and sp.md_sanpham_id = dgsp.md_sanpham_id 
			and dgsp.macdinh = 1 
			AND sp.hoatdong = 1 
			AND sp.trangthai = 'DHD'";

        sqlCount = String.Format(sqlCount, filter);

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
            sidx = "sp.ma_sanpham";
        }

        // string strsql = @"select
                            // P.md_sanpham_id, 
                            // P.md_sanpham_id as picture,
                            // P.ma_sanpham,
	                        // P.mota_tiengviet, 
                            // P.mota_tienganh,
                            // isnull((select 
		                            // top 1 gsp.gia
	                            // from 
		                            // md_phienbangia pbg with (nolock), 
                                    // md_banggia bg with (nolock), 
                                    // md_giasanpham gsp with (nolock)
	                            // where 
		                            // gsp.md_phienbangia_id = pbg.md_phienbangia_id
		                            // and pbg.md_banggia_id = bg.md_banggia_id
		                            // and banggiaban = 1
		                            // and pbg.md_trangthai_id = 'HIEULUC'
		                            // and gsp.md_sanpham_id = P.md_sanpham_id
								    // order by pbg.ngay_hieuluc desc),0) as giafob,
                            // P.ten_donggoi, P.v2, P.noofpack, P.RowNum
                        // from(
	                        // select 
                            // sp.md_sanpham_id, 
                            // sp.md_sanpham_id as picture,
                            // sp.ma_sanpham,
	                        // sp.mota_tiengviet, 
                            // sp.mota_tienganh,
	                        // dg.ten_donggoi, dg.v2, dg.soluonggoi_ctn as noofpack,
							// ROW_NUMBER() OVER (ORDER BY {0} {1}) as RowNum
	                        // FROM md_sanpham sp with (nolock) 
                            // , md_donggoi dg with (nolock)
                            // , md_donggoisanpham dgsp with (nolock)
	                        // WHERE 1=1 {2} 
	                        // AND sp.hoatdong = 1 
                            // AND sp.trangthai = 'DHD'
							// and dg.md_donggoi_id = dgsp.md_donggoi_id
							// and sp.md_sanpham_id = dgsp.md_sanpham_id
							// and dgsp.macdinh = 1
                        // )P WHERE RowNum > @start AND RowNum < @end";
		
		string strsql = @"
		declare @tbl table (
	        md_sanpham_id varchar(32), 
            picture varchar(32),
            ma_sanpham varchar(50),
            mota_tiengviet nvarchar(MAX), 
            mota_tienganh nvarchar(MAX),
            ten_donggoi nvarchar(MAX), 
            v2 decimal(18,3), 
            noofpack decimal(18,0),
            RowNum bigint
        )

        insert into @tbl
        select
            P.md_sanpham_id, 
            P.md_sanpham_id as picture,
            P.ma_sanpham,
            P.mota_tiengviet, 
            P.mota_tienganh,
            dg.ten_donggoi, 
            dg.v2, 
            dg.soluonggoi_ctn as noofpack,
            P.RowNum
			from
			(
				select sp.*, dgsp.md_donggoi_id
				from (
					select 
					sp.md_sanpham_id, 
					sp.md_sanpham_id as picture,
					sp.ma_sanpham,
					sp.mota_tiengviet, 
					sp.mota_tienganh,
					ROW_NUMBER() OVER (ORDER BY {0} {1}) as RowNum
					FROM md_sanpham sp with (nolock) 
					WHERE 1=1 {2}
					AND sp.hoatdong = 1 
					AND sp.trangthai = 'DHD'
				)sp 
				inner join md_donggoisanpham dgsp with (nolock) on sp.md_sanpham_id = dgsp.md_sanpham_id
				where 
				dgsp.macdinh = 1
				and RowNum > @start AND RowNum < @end
			)P
			inner join md_donggoi dg with (nolock) on P.md_donggoi_id = dg.md_donggoi_id 

        select 
			a.md_sanpham_id, 
			a.picture,
			a.ma_sanpham,
			a.mota_tiengviet, 
			a.mota_tienganh,
			tbl.gia,
			a.ten_donggoi, 
			a.v2, 
			a.noofpack, 
			a.RowNum
        from (
			select B.gia, B.md_sanpham_id from (
				select 
				gsp.gia
				, gsp.md_sanpham_id
				, ROW_NUMBER() OVER (PARTITION BY gsp.md_sanpham_id ORDER BY pbg.ngay_hieuluc desc) as RowNum 
				from (
				select 
					gsp.gia
					, gsp.md_sanpham_id
					, gsp.md_phienbangia_id
				from 
					md_giasanpham gsp with (nolock)
				where 
					gsp.md_sanpham_id in (select md_sanpham_id from @tbl)
				)gsp
				inner join md_phienbangia pbg with (nolock) on gsp.md_phienbangia_id = pbg.md_phienbangia_id 
				inner join md_banggia bg with (nolock) on pbg.md_banggia_id = pbg.md_banggia_id
				where 
				pbg.md_trangthai_id = N'HIEULUC'
				and banggiaban = 1
			)B
			where B.RowNum = 1
        )tbl
        inner join @tbl a on a.md_sanpham_id = tbl.md_sanpham_id
		";
		
        strsql = String.Format(strsql, sidx, sord, filter);

        System.Data.DataTable dt = mdbc.GetData(strsql, "@start", start, "@end", end);
        //string xml = "<?xml version=\"1.0\" encoding=\"UTF-8\" ?>";
        //xml += "<rows>";
        //xml += "<page>" + page + "</page>";
        //xml += "<total>" + total_page + "</total>";
        //xml += "<records>" + count + "</records>";
        //foreach (System.Data.DataRow row in dt.Rows)
        //{
        //    xml += "<row>";
        //    xml += "<cell><![CDATA[" + row["md_sanpham_id"] + "]]></cell>";
        //    xml += "<cell><![CDATA[" + row["md_sanpham_id"] + "]]></cell>";
        //    xml += "<cell><![CDATA[" + row["ma_sanpham"] + "]]></cell>";
        //    xml += "<cell><![CDATA[" + row["mota_tiengviet"] + "]]></cell>";
        //    xml += "<cell><![CDATA[" + row["mota_tienganh"] + "]]></cell>";
        //    xml += "<cell><![CDATA[" + row["giafob"] + "]]></cell>";
        //    xml += "<cell><![CDATA[" + row["ten_donggoi"] + "]]></cell>";
        //    xml += "<cell><![CDATA[" + row["v2"] + "]]></cell>";
        //    xml += "<cell><![CDATA[" + row["noofpack"] + "]]></cell>";
        //    xml += "</row>";
        //}
        //xml += "</rows>";
        //context.Response.Write(xml);
		//context.Response.Write(strsql);
        context.Response.Write(dt.ConvertXML(page, total_page, count));
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}
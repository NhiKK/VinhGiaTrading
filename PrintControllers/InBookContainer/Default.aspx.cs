using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.XtraReports.UI;
using System.Data.SqlClient;
using System.Data;
using System.IO;


public partial class PrintControllers_InBookContainer_Default : System.Web.UI.Page
{
    public string logo = "", sothapphan = "", inPDF = "";
    public InfoPrint infoPrint = PrintAnco2.GetInfoPrint();
    public HttpContext context = HttpContext.Current;

    protected void Page_Load(object sender, EventArgs e)
    {
        var context = HttpContext.Current;
        sothapphan = PrintAnco2.GetDecimal(context.Request.QueryString["stp"], 1);
        string nameTemp = "(NV) BookContainer.xls";
        string nameRpt = "BookContainer-" + DateTime.Now.ToString("ddMMyyyy");
        string sql = CreateSql(context);

        inPDF = "2";
        var task = new System.Threading.Tasks.Task(() =>
        {
            viewReport(sql);
        });

        PrintAnco2.exportDataWithType(task, sql, inPDF, nameTemp, nameRpt, ReportViewer1, true);
    }

    public void viewReport(String SqlQuery)
    {

    }

    public String CreateSql(HttpContext context)
    {
        String c_donhang_id = Request.QueryString["donhang_id"];
        string sql = string.Format(@"
            declare @donhang_id varchar(32) = '{0}'

            declare @shipper nvarchar(max), @consignee nvarchar(max), @notify nvarchar(255), @alsonotify nvarchar(255),
					@from_ nvarchar(255), @to_ nvarchar(255), @commodity nvarchar(255), @orderno nvarchar(60),
					@quantity_ctn nvarchar(max), @etd datetime, @quantity numeric(18, 0), @packing numeric(18, 0),
					@grweight numeric(18, 1), @volume numeric(18, 3), @buyer nvarchar(300), @hscode nvarchar(max), @hscodeCP nvarchar(255)
	
	        declare @cont20 numeric(18, 0), @cont40 numeric(18, 0), @cont40hc numeric(18,0), @contle numeric(18, 0), @cont45 numeric(18, 0)
	
	        set @shipper = ''  set @consignee = ''  set @notify = ''   set @alsonotify = ''
	        set @from_ = ''  set @to_ = '' set @quantity_ctn = ''
	        set @quantity = 0  set @packing = 0  set @grweight = 0  set @volume = 0
	        set @buyer = coalesce((
		        select top 1
		        dtkd.ten_dtkd + char(10) +  
		        dtkd.diachi + char(10) +  
		        dtkd.tel + char(10) +  
		        dtkd.fax
		        from c_donhang dh
		        left join md_doitackinhdoanh dtkd on dtkd.md_doitackinhdoanh_id = dh.md_doitackinhdoanh_id
		        where dh.c_donhang_id = @donhang_id
	        ), '')
	
	        set @commodity = (select dbo.get_commodity(@donhang_id))
	        set @consignee = coalesce((select mota from c_donhang where c_donhang_id = @donhang_id), '')
	        set @notify = @consignee
	        Select @orderno = sochungtu, @etd  = shipmenttime from c_donhang where c_donhang_id = @donhang_id
	        set @from_ = (select ten_cangbien from md_cangbien where md_cangbien_id = (select md_cangbien_id from c_donhang where c_donhang_id = @donhang_id))
	        set @to_ = (select portdischarge from c_donhang where c_donhang_id = @donhang_id)
	
	        -- ghep quantity of container
	        select @cont20 = cont20, @cont40 = cont40, @cont40hc = cont40hc, @cont45 = cont45, @contle = contle from c_donhang where c_donhang_id = @donhang_id
	
	        if(@cont20 > 0) begin set @quantity_ctn = (cast(@cont20 as nvarchar(10)) + 'x20"" + ') end

            if (@cont40 > 0) begin set @quantity_ctn = @quantity_ctn + (cast(@cont40 as nvarchar(10)) + 'x40"" + ') end

            if (@cont40hc > 0) begin set @quantity_ctn = @quantity_ctn + (cast(@cont40hc as nvarchar(10)) + 'x40HC + ') end

            if (@cont45 > 0) begin set @quantity_ctn = @quantity_ctn + (cast(@cont45 as nvarchar(10)) + 'x45"" + ') end

            if (@contle > 0) begin set @quantity_ctn = @quantity_ctn + (cast(@cont45 as nvarchar(10)) + 'xLCL +') end

            if (len(@quantity_ctn) > 0) begin
                    set @quantity_ctn = substring(@quantity_ctn, 0, len(@quantity_ctn) - 1)

            end

            -- tinh packing, gross weight, volume
            declare @soluong numeric(18, 0), @sl_outer numeric(18, 1),@l2 numeric(18, 1), @w2 numeric(18, 1), @h2 numeric(18, 1), 
			        @sl_inner numeric(18, 1), @l1 numeric(18, 1), @w1 numeric(18, 1), @h1 numeric(18, 1), @trongluong numeric(18, 2)
	
	        declare @nw numeric(18, 1)

            declare cur_book cursor for

                select cddh.sl_inner, cddh.l1, cddh.w1, cddh.h1, cddh.sl_outer, cddh.l2,
                    cddh.w2, cddh.h2, sp.trongluong, cddh.soluong, hs.hscode

                from c_donhang dh

                inner
                join c_dongdonhang cddh on dh.c_donhang_id = cddh.c_donhang_id

            inner
                join md_sanpham sp on cddh.md_sanpham_id = sp.md_sanpham_id

            left
                join md_hscode hs on sp.md_hscode_id = hs.md_hscode_id

                where dh.c_donhang_id = @donhang_id


            open cur_book

            fetch next from cur_book

            into @sl_inner, @l1, @w1, @h1, @sl_outer, @l2, @w2, @h2, @trongluong, @soluong, @hscodeCP

            while @@fetch_status = 0

            begin

                set @quantity = @quantity + @soluong

                set @packing = (@packing + coalesce(@soluong / @sl_outer, 0))

                set @nw = (@trongluong * @soluong)

                set @volume = @volume + round(((@l2 * @w2 * @h2) / 1000000 * @soluong / @sl_outer), 3)


                IF(coalesce(@sl_inner, 0) > 1)begin
                    set @grweight = @grweight + ((((@l1 + @w1) * (@w1 + @h1) / 5400) * @sl_outer / @sl_inner) * @packing + @nw)

                end
		        else
			        set @grweight = @grweight + (((@l2 + @w2) * (@w2 + @h2) / 5400) * @packing + @nw)


                if (isnull(@hscode, '') = '')
                    begin
                        set @hscode = @hscodeCP

                end
		        else
		        begin

                    if ((select count(1) from dbo.Split(@hscode, '/') a where trim(a.Items) = @hscodeCP) <= 0)
			        begin
                        set @hscode = @hscode + ' / ' + @hscodeCP
                    end
                end


                fetch next from cur_book

                into @sl_inner, @l1, @w1, @h1,@sl_outer, @l2, @w2, @h2, @trongluong, @soluong, @hscodeCP
            end

            close cur_book

            deallocate cur_book

            select 
                @shipper as shipper, 
                @buyer as buyer,
                @alsonotify as alsonotify,
                @from_ as from_, 
                @to_ as to_,
                @commodity as commodity,
                @hscode as hscode,
                @orderno as orderno,
                @quantity_ctn as quantity_ctn,
                (
                    select top 1 inv.crd
                    from c_dongpklinv dinv
                    inner join c_packinginvoice inv on inv.c_packinginvoice_id = dinv.c_packinginvoice_id
                    where c_donhang_id = @donhang_id
                    order by inv.ngay_motokhai desc
                ) as crd,
                FORMAT(@etd, 'dd/MM/yyyy') as etd,
                @quantity as quantity,
                @packing as packing,
                @grweight as grweight, 
                @volume as volume,
                @consignee, 
                @notify
        ", 
        c_donhang_id);

        return sql;
    }
}


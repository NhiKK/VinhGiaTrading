using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.HSSF.Util;
using System.Data.Linq;
using System.Linq;
using ExcelLibrary.SpreadSheet;
using ExcelLibrary;

public partial class Tool_XoaHangHoaNHD : System.Web.UI.Page
{
    private LinqDBDataContext db = new LinqDBDataContext();
    string id = Guid.NewGuid().ToString().Replace("-", "").ToLower();
    string filePath = "", filenameLC = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        string startdate = Request.QueryString["startdate"];
        string enddate = Request.QueryString["enddate"];
        string type = Request.QueryString["type"];
        startdate = startdate + " 00:00:00";
        enddate = enddate + " 23:59:59";

        HttpFileCollection files = Request.Files;
        string arrID = "";
        if (files.Count > 0)
        {
            filenameLC = files[0].FileName.Replace(".xlsx", ".xls");
            filePath = Server.MapPath("../VNN_Files/" + id + files[0].FileName);
            files[0].SaveAs(filePath);
            try
            {
                int j = filePath.LastIndexOf(".");
                if (filePath.Substring(j) != ".xls")
                {
                    filePath = ConvertXLSXToXLS.ConvertWorkbookXSSFToHSSF(filePath);
                }
                Workbook wb = Workbook.Load(filePath);
                Worksheet ws = wb.Worksheets[0];
                var cellCollection = ws.Cells;
                int totalCount = cellCollection.Rows.Count;
                for (int i = 0; i < totalCount; i++)
                {
                    try
                    {
                        Row row = cellCollection.Rows[i];
                        arrID += "'" + row.GetCell(0).Value.ToString() + "',";
                    }
                    catch { }
                }
            }
            catch { }
        }

        if (arrID != "")
        {
            arrID = arrID.Remove(arrID.Length - 1);
            arrID = "and sp.ma_sanpham in (" + arrID + ")";
        }

        string sql = string.Format(@" 
            SELECT
	            sp.md_sanpham_id, 
                sp.ma_sanpham, 
                kd.ten_tv as ten_kd, 
                kd.ma_kieudang as ma_kd, 
                cn.ten_tv as ten_cn, 
                cn.ma_chucnang as ma_cn,
	            sp.mota_tiengviet,  
                sp.mota_tienganh, 
                sp.L_inch, 
                sp.W_inch, 
                sp.H_inch, 
                sp.L_cm, 
                sp.W_cm, 
                sp.H_cm, 
                sp.trongluong, 
                sp.dientich,
                sp.ngayngunghd,
	            (nnl.hehang + '('+ nnl.nhom +')') as nangluc
            FROM 
	            md_sanpham sp 
	            left join md_chucnang cn on sp.md_chucnang_id = cn.md_chucnang_id
	            left join md_nhomnangluc nnl on sp.md_nhomnangluc_id = nnl.md_nhomnangluc_id
	            left join md_kieudang kd on sp.md_kieudang_id = kd.md_kieudang_id
            WHERE 1=1
            and sp.ngayngunghd between convert(datetime, '{0}', 103) and convert(datetime, '{1}', 103)
            and sp.trangthai = 'NHD'
            {2}
            order by sp.ma_sanpham
        ", startdate, enddate, arrID);

        DataTable dt = mdbc.GetData(sql);
        if (dt.Rows.Count != 0)
        {
            if (type == "1")
            {
                this.CreateWorkBookPO(dt, Request.QueryString["startdate"], Request.QueryString["enddate"]);
            }
            else if(type == "2")
            {
                string msg = "";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string idSP = dt.Rows[i]["md_sanpham_id"].ToString();
                    md_sanpham sp = db.md_sanphams.Where(s => s.md_sanpham_id == idSP).FirstOrDefault();
                    if (sp == null)
                    {
                        msg += string.Format("<div style='color: red'>Không tìm thấy sản phẩm.</div>");
                    }
                    else
                    {
                        try
                        {
                            string msgDT = "";

                            #region Xóa nhập xuất kho
                            try
                            {
                                foreach (c_dongnhapxuat dnxk in db.c_dongnhapxuats.Where(s => s.md_sanpham_id == sp.md_sanpham_id))
                                {
                                    var phieuNXK = db.c_nhapxuats.Where(s => s.c_nhapxuat_id == dnxk.c_nhapxuat_id).FirstOrDefault();
                                    if (phieuNXK != null)
                                    {
                                        if (phieuNXK.md_trangthai_id == "KETTHUC" | phieuNXK.md_trangthai_id == "HUY")
                                        {
                                            db.c_dongnhapxuats.DeleteOnSubmit(dnxk);
                                        }
                                        else
                                        {
                                            msgDT += string.Format("<div style='color: red'>{0} vẫn còn trong phiếu NXK {1}.</div>", sp.ma_sanpham, phieuNXK.sophieu);
                                        }
                                    }
                                }
                                db.SubmitChanges();
                                msgDT += string.Format("<div style='color: blue'>{0} đã xóa phiếu nhập xuất kho.</div>", sp.ma_sanpham);
                            }
                            catch (Exception ex1)
                            {
                                msgDT += string.Format("<div style='color: red'>{0} xóa phiếu nhập xuất kho lỗi {1}.</div>", sp.ma_sanpham, ex1.Message);
                            }
                            #endregion

                            #region Xóa INV
                            try
                            {
                                foreach (c_dongpklinv dinv in db.c_dongpklinvs.Where(s => s.md_sanpham_id == sp.md_sanpham_id))
                                {
                                    var phieuINV = db.c_packinginvoices.Where(s => s.c_packinginvoice_id == dinv.c_packinginvoice_id).FirstOrDefault();
                                    if (phieuINV != null)
                                    {
                                        if (phieuINV.md_trangthai_id == "KETTHUC" | phieuINV.md_trangthai_id == "HUY")
                                        {
                                            db.c_dongpklinvs.DeleteOnSubmit(dinv);
                                        }
                                        else
                                        {
                                            msgDT += string.Format("<div style='color: red'>{0} vẫn còn trong INV {1}.</div>", sp.ma_sanpham, phieuINV.so_inv);
                                        }
                                    }
                                }
                                db.SubmitChanges();
                                msgDT += string.Format("<div style='color: blue'>{0} đã xóa invoice.</div>", sp.ma_sanpham);
                            }
                            catch (Exception ex1)
                            {
                                msgDT += string.Format("<div style='color: red'>{0} xóa invoice lỗi {1}.</div>", sp.ma_sanpham, ex1.Message);
                            }
                            #endregion

                            #region Xóa Phiếu Hạn Ngạch
                            try
                            {
                                foreach (c_chitietphieuhanngach dphn in db.c_chitietphieuhanngaches.Where(s => s.md_sanpham_id == sp.md_sanpham_id))
                                {
                                    var phieuHN = db.c_phieuhanngaches.Where(s => s.c_phieuhanngach_id == dphn.c_phieuhanngach_id).FirstOrDefault();
                                    if (phieuHN != null)
                                    {
                                        if (phieuHN.md_trangthai_id == "KETTHUC" | phieuHN.md_trangthai_id == "HUY")
                                        {
                                            db.c_chitietphieuhanngaches.DeleteOnSubmit(dphn);
                                        }
                                        else
                                        {
                                            msgDT += string.Format("<div style='color: red'>{0} vẫn còn trong phiếu GHN {1}.</div>", sp.ma_sanpham, phieuHN.sochungtu);
                                        }
                                    }
                                }
                                db.SubmitChanges();
                                msgDT += string.Format("<div style='color: blue'>{0} đã xóa phiếu giảm hạn ngạch.</div>", sp.ma_sanpham);
                            }
                            catch (Exception ex1)
                            {
                                msgDT += string.Format("<div style='color: red'>{0} xóa phiếu giảm hạn ngạch lỗi {1}.</div>", sp.ma_sanpham, ex1.Message);
                            }
                            #endregion

                            #region Xóa DSĐH
                            try
                            {
                                foreach (c_dongdsdh ddsdh in db.c_dongdsdhs.Where(s => s.md_sanpham_id == sp.md_sanpham_id))
                                {
                                    var phieuDSDH = db.c_danhsachdathangs.Where(s => s.c_danhsachdathang_id == ddsdh.c_danhsachdathang_id).FirstOrDefault();
                                    if (phieuDSDH != null)
                                    {
                                        if (phieuDSDH.md_trangthai_id == "KETTHUC" | phieuDSDH.md_trangthai_id == "HUY")
                                        {
                                            db.c_dongdsdhs.DeleteOnSubmit(ddsdh);
                                        }
                                        else
                                        {
                                            msgDT += string.Format("<div style='color: red'>{0} vẫn còn trong DSDH {1}.</div>", sp.ma_sanpham, phieuDSDH.sochungtu);
                                        }
                                    }
                                }
                                db.SubmitChanges();
                                msgDT += string.Format("<div style='color: blue'>{0} đã xóa DSĐH.</div>", sp.ma_sanpham);
                            }
                            catch (Exception ex1)
                            {
                                msgDT += string.Format("<div style='color: red'>{0} xóa DSĐH lỗi {1}.</div>", sp.ma_sanpham, ex1.Message);
                            }
                            #endregion

                            #region Xóa QO
                            try
                            {
                                foreach (c_chitietbaogia dbg in db.c_chitietbaogias.Where(s => s.md_sanpham_id == sp.md_sanpham_id))
                                {
                                    var phieuQO = db.c_baogias.Where(s => s.c_baogia_id == dbg.c_baogia_id).FirstOrDefault();
                                    if (phieuQO != null)
                                    {
                                        if (phieuQO.md_trangthai_id == "KETTHUC" | phieuQO.md_trangthai_id == "HUY")
                                        {
                                            db.c_chitietbaogias.DeleteOnSubmit(dbg);
                                        }
                                        else
                                        {
                                            msgDT += string.Format("<div style='color: red'>{0} vẫn còn trong QO {1}.</div>", sp.ma_sanpham, phieuQO.sobaogia);
                                        }
                                    }
                                }
                                db.SubmitChanges();
                                msgDT += string.Format("<div style='color: blue'>{0} đã xóa QO.</div>", sp.ma_sanpham);
                            }
                            catch (Exception ex1)
                            {
                                msgDT += string.Format("<div style='color: red'>{0} xóa QO lỗi {1}.</div>", sp.ma_sanpham, ex1.Message);
                            }
                            #endregion

                            #region Xóa PO
                            try
                            {
                                foreach (c_dongdonhang ddh in db.c_dongdonhangs.Where(s => s.md_sanpham_id == sp.md_sanpham_id))
                                {
                                    var phieuPO = db.c_donhangs.Where(s => s.c_donhang_id == ddh.c_donhang_id).FirstOrDefault();
                                    if (phieuPO != null)
                                    {
                                        if (phieuPO.md_trangthai_id == "KETTHUC" | phieuPO.md_trangthai_id == "HUY")
                                        {
                                            db.c_dongdonhangs.DeleteOnSubmit(ddh);
                                        }
                                        else
                                        {
                                            msgDT += string.Format("<div style='color: red'>{0} vẫn còn trong PO {1}.</div>", sp.ma_sanpham, phieuPO.sochungtu);
                                        }
                                    }
                                }
                                db.SubmitChanges();
                                msgDT += string.Format("<div style='color: blue'>{0} đã xóa PO.</div>", sp.ma_sanpham);
                            }
                            catch (Exception ex1)
                            {
                                msgDT += string.Format("<div style='color: red'>{0} xóa PO lỗi {1}.</div>", sp.ma_sanpham, ex1.Message);
                            }
                            #endregion

                            if (!msgDT.Contains("'color: red'"))
                            {
                                db.md_sanphams.DeleteOnSubmit(sp);
                                db.SubmitChanges();
                                msgDT += string.Format("<div style='color: blue'>{0} đã được xóa.</div>", sp.ma_sanpham);
                            }

                            msg += string.Format("<div style='border: 1px solid #CCC; padding: 5px; margin-bottom: 5px'>{0}</div>", msgDT);
                        }
                        catch (Exception ex)
                        {
                            msg += string.Format("<div style='color: red'>{0} bị lỗi: {1}.</div>", sp.ma_sanpham, ex.Message);
                        }
                    }
                }
                Response.Write(msg);
            }
        }
        else
        {
            Response.Write("<h3>Không có dữ liệu</h3>");
        }
    }

    public void CreateWorkBookPO(DataTable dt, string tungay, string denngay)
    {
        HSSFWorkbook hssfworkbook = new HSSFWorkbook();
        ISheet s1 = hssfworkbook.CreateSheet("Sheet1");
        HSSFSheet hssfsheet = (HSSFSheet)s1;
        ExportExcel export = new ExportExcel(hssfworkbook, 0, 14);
        int row = export.SetHeaderAnco(hssfsheet, "DANH SÁCH HÀNG HÓA NGƯNG HOẠT ĐỘNG", tungay, denngay, false);

        #region Header Column
        int widthDF = 5000;
        List<ItemValue> lstHeader = new List<ItemValue>();
        var item = new ItemValue
        {
            item = "STT",
            value = "rowNum",
            witdh = 2000
        };
        lstHeader.Add(item);

        item = new ItemValue
        {
            item = "Mã SP",
            value = "ma_sanpham",
            witdh = widthDF
        };
        lstHeader.Add(item);

        item = new ItemValue
        {
            item = "Tên KD",
            value = "ten_kd",
            witdh = 4000
        };
        lstHeader.Add(item);

        item = new ItemValue
        {
            item = "Mã KD",
            value = "ma_kd",
            witdh = 3000
        };
        lstHeader.Add(item);

        item = new ItemValue
        {
            item = "Tên CN",
            value = "ten_cn",
            witdh = 3000
        };
        lstHeader.Add(item);

        item = new ItemValue
        {
            item = "Mã CN",
            value = "ma_cn",
            witdh = 3000
        };
        lstHeader.Add(item);

        item = new ItemValue
        {
            item = "Mô tả TV",
            value = "mota_tiengviet",
            witdh = 7000
        };
        lstHeader.Add(item);

        item = new ItemValue
        {
            item = "Mô tả TA",
            value = "mota_tienganh",
            witdh = 7000
        };
        lstHeader.Add(item);

        item = new ItemValue
        {
            item = "L(cm)",
            value = "L_cm",
            witdh = 3000
        };
        lstHeader.Add(item);

        item = new ItemValue
        {
            item = "W(cm)",
            value = "W_cm",
            witdh = 3000
        };
        lstHeader.Add(item);

        item = new ItemValue
        {
            item = "H(cm)",
            value = "H_cm",
            witdh = 3000
        };
        lstHeader.Add(item);

        item = new ItemValue
        {
            item = "Trọng lượng",
            value = "trongluong",
            witdh = 3000
        };
        lstHeader.Add(item);

        item = new ItemValue
        {
            item = "Diện tích",
            value = "dientich",
            witdh = 3000
        };
        lstHeader.Add(item);

        item = new ItemValue
        {
            item = "Ngày NHĐ",
            value = "ngayngunghd",
            witdh = widthDF
        };
        lstHeader.Add(item);

        item = new ItemValue
        {
            item = "Nhóm NL",
            value = "nangluc",
            witdh = widthDF
        };
        lstHeader.Add(item);

        IRow rowColumnHeader = s1.CreateRow(row);
        rowColumnHeader.HeightInPoints = 40;
        for (int j = 0; j < lstHeader.Count; j++)
        {
            rowColumnHeader.CreateCell(j).SetCellValue(lstHeader[j].item);
            rowColumnHeader.GetCell(j).CellStyle = export.borderWrap;
            s1.SetColumnWidth(j, lstHeader[j].witdh);
        }
        row++;
        #endregion

        #region Set Table
        int start_sum = row + 1;
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            IRow row_t = s1.CreateRow(row);
            float height = 25;
            for (int j = 0; j < lstHeader.Count; j++)
            {
                var cell = row_t.CreateCell(j);
                string[] arrDecimal = new string[] { "L_cm", "W_cm", "H_cm", "trongluong", "dientich" };
                string[] arrDate = new string[] { "ngayngunghd" };
                if (arrDecimal.Contains(lstHeader[j].value))
                {
                    try
                    {
                        cell.SetCellValue(double.Parse(dt.Rows[i][lstHeader[j].value].ToString()));
                        cell.CellStyle = export.borderright;
                    }
                    catch { }
                }
                else if (lstHeader[j].value == "rowNum")
                {
                    cell.SetCellValue(i + 1);
                    cell.CellStyle = export.bordercenter;
                }
                else if (arrDate.Contains(lstHeader[j].value))
                {
                    cell.SetCellValue(DateTime.Parse(dt.Rows[i][lstHeader[j].value].ToString()).ToString("dd/MM/yyyy"));
                    cell.CellStyle = export.bordercenter;
                }
                else
                {
                    string str = dt.Rows[i][lstHeader[j].value].ToString();
                    float heightMAX = export.MeasureTextHeight(str, lstHeader[j].witdh);
                    if (heightMAX > height)
                    {
                        height = heightMAX;
                    }
                    cell.SetCellValue(str);
                    cell.CellStyle = export.border;
                }
            }
            row_t.HeightInPoints = height;
            row++;
        }
        int end_sum = row;
        #endregion

        hssfsheet = export.PrintExcel(hssfsheet, row);
        String saveAsFileName = String.Format("DSSanPhamNHD-{0}.xls", DateTime.Now);
        export.SaveFile(hssfworkbook, saveAsFileName, Context);
    }
}

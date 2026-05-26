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

public partial class Tool_KetThucPO : System.Web.UI.Page
{
    private LinqDBDataContext db = new LinqDBDataContext();
    string id = Guid.NewGuid().ToString().Replace("-", "").ToLower();
    string filePath = "", filenameLC = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        string startdate = Request.QueryString["startdate"];
        string enddate = Request.QueryString["enddate"];
        string type = Request.QueryString["type"];
		string inputMaSP = Request.Form["MaHangKTX"];
        startdate = startdate + " 00:00:00";
        enddate = enddate + " 23:59:59";

        HttpFileCollection files = Request.Files;
        string arrID = "";
        if (files.Count > 0)
        {
            filenameLC = files[0].FileName.Replace(".xlsx", ".xls");
            if (filenameLC != "")
            {
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
                catch (Exception ex)
                {
                    Response.Write(ex.Message);
                    arrID = "'Error',";
                }
            }
        }
		else if (!string.IsNullOrEmpty(inputMaSP))
        {
            arrID = string.Format("'{0}',", inputMaSP);
        }

        if(arrID != "") {
            arrID = arrID.Remove(arrID.Length - 1);
            arrID = "and dh.sochungtu in (" + arrID + ")";
        }
        string sql = string.Format(@" 
            select 
            dh.c_donhang_id,
            dtkd.ma_dtkd, 
            dh.sochungtu, 
            dh.customer_order_no, 
            cbien.ten_cangbien,
            dh.shipmenttime,
            dh.totalcbm,
            (
				dh.amount - (dh.amount * dh.discount)/100 + 
				isnull((select SUM(phi) from c_phidonhang where phitang = 1 and c_donhang_id = dh.c_donhang_id), 0) -
				isnull((select SUM(phi) from c_phidonhang where phitang = 0 and c_donhang_id = dh.c_donhang_id), 0)
			) as amount,
            dh.discount,
            pmt.ten_paymentterm,
            dh.hoahong,
            dh.ghichu,
            dh.ngaylap,
            dh.nguoilap
            FROM c_donhang dh with (nolock)
			left join md_doitackinhdoanh dtkd with (nolock) on dh.md_doitackinhdoanh_id = dtkd.md_doitackinhdoanh_id
			left join md_cangbien cbien with (nolock) on dh.md_cangbien_id = cbien.md_cangbien_id
            left join md_paymentterm pmt with (nolock) on dh.md_paymentterm_id = pmt.md_paymentterm_id
            WHERE 1=1 
            and dh.shipmenttime between convert(datetime, '{0}', 103) and convert(datetime, '{1}', 103)
            and dh.md_trangthai_id != 'KETTHUC'
            {2}
            order by dtkd.ma_dtkd, dh.sochungtu
        ", startdate, enddate, arrID);

        DataTable dt = mdbc.GetData(sql);
        if (dt.Rows.Count != 0)
        {
            if (type == "1")
            {
                this.CreateWorkBookPO(dt, Request.QueryString["startdate"], Request.QueryString["enddate"]);
            }
            else if (type == "2")
            {
                string msg = "";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string idDonHang = dt.Rows[i]["c_donhang_id"].ToString();
                    c_donhang dh = db.c_donhangs.Where(s => s.c_donhang_id == idDonHang).FirstOrDefault();
                    if (dh == null)
                    {
                        msg += string.Format("<div style='color: red'>Không tìm thấy đơn hàng.</div>");
                    }
                    else
                    {
                        try
                        {
                            string msgDT = "";
                            string[] arrDDH = db.c_dongdonhangs.Where(s => s.c_donhang_id == dh.c_donhang_id).Select(s => s.c_dongdonhang_id).Distinct().ToArray();

                            //Kết thúc thu chi
                            try
                            {
                                string[] arrTC = db.c_chitietthuchis.Where(s => s.c_donhang_id == dh.c_donhang_id).Select(s => s.c_thuchi_id).Distinct().ToArray();
                                foreach (c_thuchi tc in db.c_thuchis.Where(s => arrTC.Contains(s.c_thuchi_id)))
                                {
                                    tc.md_trangthai_id = "KETTHUC";
                                }
                                db.SubmitChanges();
                                msgDT += string.Format("<div style='color: blue'>{0} đã kết thúc thu chi.</div>", dh.sochungtu);
                            }
                            catch (Exception ex1)
                            {
                                msgDT += string.Format("<div style='color: red'>{0} kết thúc thu chi lỗi {1}.</div>", dh.sochungtu, ex1.Message);
                            }

                            //Kết thúc nhập xuất kho
                            try
                            {
                                string[] arrPNXK = db.c_dongnhapxuats.Where(s => arrDDH.Contains(s.c_dongdonhang_id)).Select(s => s.c_nhapxuat_id).Distinct().ToArray();
                                foreach (c_nhapxuat nxk in db.c_nhapxuats.Where(s => arrPNXK.Contains(s.c_nhapxuat_id)))
                                {
                                    nxk.md_trangthai_id = "KETTHUC";
                                }
                                db.SubmitChanges();
                                msgDT += string.Format("<div style='color: blue'>{0} đã kết thúc nhập xuất kho.</div>", dh.sochungtu);
                            }
                            catch (Exception ex1)
                            {
                                msgDT += string.Format("<div style='color: red'>{0} kết thúc nhập xuất kho lỗi {1}.</div>", dh.sochungtu, ex1.Message);
                            }

                            //Kết thúc INV
                            try
                            {
                                string[] arrINV = db.c_dongpklinvs.Where(s => s.c_donhang_id == dh.c_donhang_id).Select(s => s.c_packinginvoice_id).Distinct().ToArray();
                                foreach (c_packinginvoice inv in db.c_packinginvoices.Where(s => arrINV.Contains(s.c_packinginvoice_id)))
                                {
                                    inv.md_trangthai_id = "KETTHUC";
                                }
                                db.SubmitChanges();
                                msgDT += string.Format("<div style='color: blue'>{0} đã kết thúc invoice.</div>", dh.sochungtu);
                            }
                            catch (Exception ex1)
                            {
                                msgDT += string.Format("<div style='color: red'>{0} kết thúc invoice lỗi {1}.</div>", dh.sochungtu, ex1.Message);
                            }

                            //Kết thúc Phiếu Hạn Ngạch
                            try
                            {
                                foreach (c_phieuhanngach phn in db.c_phieuhanngaches.Where(s => s.c_donhang_id == dh.c_donhang_id))
                                {
                                    phn.md_trangthai_id = "KETTHUC";
                                }
                                db.SubmitChanges();
                                msgDT += string.Format("<div style='color: blue'>{0} đã kết thúc phiếu giảm hạn ngạch.</div>", dh.sochungtu);
                            }
                            catch (Exception ex1)
                            {
                                msgDT += string.Format("<div style='color: red'>{0} kết thúc phiếu giảm hạn ngạch lỗi {1}.</div>", dh.sochungtu, ex1.Message);
                            }

                            //Kết thúc Kế hoạch xuất hàng
                            try
                            {
                                foreach (c_kehoachxuathang khxh in db.c_kehoachxuathangs.Where(s => s.c_donhang_id == dh.c_donhang_id))
                                {
                                    khxh.md_trangthai_id = "KETTHUC";
                                }
                                db.SubmitChanges();
                                msgDT += string.Format("<div style='color: blue'>{0} đã kết thúc KHXH.</div>", dh.sochungtu);
                            }
                            catch (Exception ex1)
                            {
                                msgDT += string.Format("<div style='color: red'>{0} kết thúc KHXH lỗi {1}.</div>", dh.sochungtu, ex1.Message);
                            }

                            //Kết thúc Danh sách đặt hàng
                            try
                            {
                                foreach (c_danhsachdathang dsdh in db.c_danhsachdathangs.Where(s => s.c_donhang_id == dh.c_donhang_id))
                                {
                                    dsdh.md_trangthai_id = "KETTHUC";
                                }
                                db.SubmitChanges();
                                msgDT += string.Format("<div style='color: blue'>{0} đã kết thúc DSĐH.</div>", dh.sochungtu);
                            }
                            catch (Exception ex1)
                            {
                                msgDT += string.Format("<div style='color: red'>{0} kết thúc DSĐH lỗi {1}.</div>", dh.sochungtu, ex1.Message);
                            }

                            if (!msgDT.Contains("'color: red'"))
                            {
                                dh.md_trangthai_id = "KETTHUC";
                                db.SubmitChanges();
                                msgDT += string.Format("<div style='color: blue'>{0} đã được kết thúc.</div>", dh.sochungtu);
                            }
                            msg += string.Format("<div style='border: 1px solid #CCC; padding: 5px; margin-bottom: 5px'>{0}</div>", msgDT);
                        }
                        catch (Exception ex)
                        {
                            msg += string.Format("<div style='color: red'>{0} bị lỗi: {1}.</div>", dh.sochungtu, ex.Message);
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
        ExportExcel export = new ExportExcel(hssfworkbook, 0, 13);
        int row = export.SetHeaderAnco(hssfsheet, "DANH SÁCH PO CẦN KẾT THÚC", tungay, denngay, false);

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
            item = "Khách hàng",
            value = "ma_dtkd",
            witdh = widthDF
        };
        lstHeader.Add(item);

        item = new ItemValue
        {
            item = "Số chứng từ",
            value = "sochungtu",
            witdh = widthDF
        };
        lstHeader.Add(item);

        item = new ItemValue
        {
            item = "Customer Order No",
            value = "customer_order_no",
            witdh = widthDF
        };
        lstHeader.Add(item);

        item = new ItemValue
        {
            item = "Cảng biển",
            value = "ten_cangbien",
            witdh = widthDF
        };
        lstHeader.Add(item);

        item = new ItemValue
        {
            item = "Shipment Time",
            value = "shipmenttime",
            witdh = widthDF
        };
        lstHeader.Add(item);

        item = new ItemValue
        {
            item = "Total CBM",
            value = "totalcbm",
            witdh = widthDF
        };
        lstHeader.Add(item);

        item = new ItemValue
        {
            item = "Amount",
            value = "amount",
            witdh = widthDF
        };
        lstHeader.Add(item);

        item = new ItemValue
        {
            item = "Discount",
            value = "discount",
            witdh = widthDF
        };
        lstHeader.Add(item);

        item = new ItemValue
        {
            item = "PaymentTerm",
            value = "ten_paymentterm",
            witdh = widthDF
        };
        lstHeader.Add(item);

        item = new ItemValue
        {
            item = "Hoa hồng",
            value = "hoahong",
            witdh = widthDF
        };
        lstHeader.Add(item);

        item = new ItemValue
        {
            item = "Ngày lập",
            value = "ngaylap",
            witdh = widthDF
        };
        lstHeader.Add(item);

        item = new ItemValue
        {
            item = "Người lập",
            value = "nguoilap",
            witdh = widthDF
        };
        lstHeader.Add(item);

        item = new ItemValue
        {
            item = "Ghi chú",
            value = "ghichu",
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
                string[] arrDecimal = new string[] { "totalcbm", "amount", "discount", "hoahong" };
                string[] arrDate = new string[] { "ngaylap", "shipmenttime" };
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
        String saveAsFileName = String.Format("DSPOCanKetThuc-{0}.xls", DateTime.Now);
        export.SaveFile(hssfworkbook, saveAsFileName, Context);
    }
}

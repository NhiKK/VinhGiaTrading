<%@ WebHandler Language="C#" Class="UploadFile" %>

using System;
using System.Web;
using System.Linq;
using System.Text.RegularExpressions;

public class UploadFile : IHttpHandler
{
    LinqDBDataContext db = new LinqDBDataContext();

    public void ProcessRequest(HttpContext context)
    {

        if (context.Request.Files.Count != 0)
        {
            HttpFileCollection files = context.Request.Files;
            for (int i = 1; i <= files.Count; i++)
            {
                string fileName = files[i - 1].FileName;
                string filterName = fileName.Substring(0, fileName.LastIndexOf("."));
                string jpg = fileName.Substring(fileName.LastIndexOf("."));
                //if (filterName.Length == 14)
                //{
                //    try
                //    {
                //        if (filterName.ElementAt(9).ToString().ToLower().Equals("f"))
                //        {
                //            filterName = filterName.Substring(0, 11);
                //        }
                //        else
                //        {
                //            filterName = filterName.Substring(0, 8);
                //        }
                //    }
                //    catch
                //    {
                //        filterName = filterName.Substring(0, 8);
                //    }
                //}

                var rgxSanPham = new Regex("^[0-9][0-9]-[0-9][0-9][0-9][0-9][0-9]$");
                var rgxSanPhamF = new Regex("^[0-9][0-9]-[0-9][0-9][0-9][0-9][0-9]-F[0-9]$");
                var rgxDeTai = new Regex("^[0-9][0-9]-[0-9][0-9]$");
                var rgxMauSac = new Regex("^[0-9][0-9]-[0-9][0-9]-[0-9][0-9]$");
                var rgxMauSac2 = new Regex("^[0-9][0-9]-[0-9][0-9][0-9][0-9][0-9]-[0-9][0-9]$");
                var rgxMauSac3 = new Regex("^[0-9][0-9]-[0-9][0-9][0-9][0-9][0-9]-F[0-9]-[0-9][0-9]$");
                if (rgxDeTai.IsMatch(filterName))
                {
                    files[i - 1].SaveAs(Public.imgPatterns + files[i - 1].FileName);
                    context.Response.Write(Public.imgPatterns + files[i - 1].FileName);
                }
                else if (rgxMauSac.IsMatch(filterName) | rgxMauSac2.IsMatch(filterName) | rgxMauSac3.IsMatch(filterName))
                {
                    files[i - 1].SaveAs(Public.imgColors + files[i - 1].FileName);
                    context.Response.Write(Public.imgColors + files[i - 1].FileName);
                }
                else if (rgxSanPham.IsMatch(filterName) | rgxSanPhamF.IsMatch(filterName))
                {
                    files[i - 1].SaveAs(Public.imgProducts + filterName + jpg);
                    md_anhsanpham asp = db.md_anhsanphams.FirstOrDefault(p => p.url.Equals(filterName) && p.filter.Equals(filterName));

                    if (asp == null)
                    {
                        md_anhsanpham img = new md_anhsanpham();
                        img.md_anhsanpham_id = ImportUtils.getNEWID();
                        img.url = fileName;
                        img.filter = filterName;
                        img.ngaytao = DateTime.Now;
                        img.ngaycapnhat = DateTime.Now;
                        img.hoatdong = true;

                        db.md_anhsanphams.InsertOnSubmit(img);
                        db.SubmitChanges();
                    }
                }
                else
                {
                    context.Response.Write("Tên hình ảnh sai quy cách." + filterName);
                }
            }
        }
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}
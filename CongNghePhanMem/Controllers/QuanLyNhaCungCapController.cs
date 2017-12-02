using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CongNghePhanMem.Models;
using PagedList;
using PagedList.Mvc;

namespace CongNghePhanMem.Controllers
{
    public class QuanLyNhaCungCapController : Controller
    {
        // GET: QuanLyNhaCungCap
        CongNghePhanMemEntities cn = new CongNghePhanMemEntities();
        protected void SetAlert(string message, string type)
        {
            TempData["AlertMessage"] = message;
            if (type == "success")
            {
                TempData["AlertType"] = "alert-success";
            }
            else if (type == "warning")
            {
                TempData["AlertType"] = "alert-warning";
            }
            else if (type == "error")
            {
                TempData["AlertType"] = "alert-danger";
            }
        }
 public ActionResult NhaCungCap(int ?page)
        {
            int pageSize = 25;
            int pageNumber = (page ?? 1);
            var ncc = cn.NhaCungCaps.ToList().OrderBy(n=>n.MaNCC).ToPagedList(pageNumber,pageSize);
            return View(ncc);
        }

        //Thêm mới
        [HttpGet]
        public ActionResult ThemNCC()
        {
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ThemNCC(NhaCungCap ncc)
        {
            if(ModelState.IsValid)
            {
                NhaCungCap ncc1 = new NhaCungCap();
                ncc1.TenNCC = ncc.TenNCC;
                ncc1.DiaChi = ncc.DiaChi;
                ncc1.SDT = ncc.SDT;
                ncc1.Email = ncc.Email;
                cn.NhaCungCaps.Add(ncc1);
                cn.SaveChanges();
                SetAlert("Thêm thành công!", "success");

            }
            return RedirectToAction("NhaCungCap", "QuanLyNhaCungCap");
        }

        //Sửa nhà cung cấp
        [HttpGet]
        public ActionResult SuaNCC(int MaNCC=0)
        {
            NhaCungCap ncc = cn.NhaCungCaps.SingleOrDefault(n => n.MaNCC == MaNCC);
            return View(ncc);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SuaNCC(NhaCungCap ncc)
        {
            if(ModelState.IsValid)
            {
                NhaCungCap ncc1 = cn.NhaCungCaps.SingleOrDefault(n => n.MaNCC == ncc.MaNCC);
                if(ncc1==null)
                {
                    Response.StatusCode = 404;
                    return null;
                }
                ncc1.TenNCC = ncc.TenNCC;
                ncc1.DiaChi = ncc.DiaChi;
                ncc1.SDT = ncc.SDT;
                ncc1.Email = ncc.Email;
                cn.SaveChanges();
                SetAlert("SuaThanhCong", "success");
            }
            return RedirectToAction("NhaCungCap", "QuanLyNhaCungCap");

        }
        //Xóa nhà cung cấp
        public ActionResult XoaNCC(int MaNCC=0)
        {
            if(ModelState.IsValid)
            {
                HopDongNCC hd = cn.HopDongNCCs.SingleOrDefault(n => n.MaNCC == MaNCC);
                if (hd != null)
                {
                    SetAlert("Nhà cung cấp tồn tại trong hợp đồng!", "success");
                }
                else
                {
                    NhaCungCap ncc = cn.NhaCungCaps.SingleOrDefault(n => n.MaNCC == MaNCC);
                    if (ncc == null)
                    {
                        Response.StatusCode = 404;
                        return null;
                    }
                    cn.NhaCungCaps.Remove(ncc);
                    cn.SaveChanges();
                    SetAlert("Xóa thành công", "success");
                }
                
            }
            return RedirectToAction("NhaCungCap", "QuanLyNhaCungCap");
        }
        
    }
}
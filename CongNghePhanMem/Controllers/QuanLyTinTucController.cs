using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CongNghePhanMem.Models;
using System.IO;
using PagedList;
using PagedList.Mvc;

namespace CongNghePhanMem.Controllers
{
    public class QuanLyTinTucController : Controller
    {

        CongNghePhanMemEntities cn = new CongNghePhanMemEntities();
        // GET: QuanLyTinTuc
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
        [HttpGet]
        [ValidateInput(false)]
        public ActionResult TraLoi(int MaPH = 0)
        {
            PhanHoi ph = cn.PhanHois.SingleOrDefault(n => n.MaPH == MaPH);
            return View(ph);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult TraLoi(PhanHoi ph)
        {
            if (ModelState.IsValid)
            {
                PhanHoi ph1 = cn.PhanHois.SingleOrDefault(n => n.MaPH == ph.MaPH);
                if (ph1 == null)
                {
                    Response.StatusCode = 404;
                    return null;
                }
                ph1.TraLoi = ph.TraLoi.ToString();
                cn.SaveChanges();
                SetAlert("Trả lời thành công!", "success");
            }
            return RedirectToAction("YKien", "QuanLyTinTuc");


        }
        public ActionResult XoaYKien(int MaPH = 0)
        {
            if (ModelState.IsValid)
            {
                PhanHoi ph = cn.PhanHois.SingleOrDefault(n => n.MaPH == MaPH);
                cn.PhanHois.Remove(ph);
                cn.SaveChanges();
                SetAlert("Xóa thành công", "success");
            }
            return View();
        }
	//Tin Tức
        public ActionResult TinTuc(int ? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            var tt = cn.TinTucs.ToList().OrderBy(n=>n.MaTT).ToPagedList(pageNumber,pageSize);
            return View(tt);
        }
        [HttpGet]
        [ValidateInput(false)]
        public ActionResult SuaTT(int MaTT = 0)
        {
            TinTuc tt = cn.TinTucs.SingleOrDefault(n => n.MaTT == MaTT);
            return View(tt);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SuaTT(TinTuc tt)
        {
            if (ModelState.IsValid)
            {
                
                TinTuc tt1 = cn.TinTucs.SingleOrDefault(n => n.MaTT == tt.MaTT);
                
                    tt1.TenTT = tt.TenTT;
                    tt1.MoTa = tt.MoTa;
                    cn.SaveChanges();
                    SetAlert("Sửa thành công!", "success");
                
            }
            return RedirectToAction("TinTuc", "QuanLyTinTuc");
        }
        //Xóa
        public ActionResult XoaTT(int MaTT = 0)
        {
            TinTuc tt = cn.TinTucs.SingleOrDefault(n => n.MaTT == MaTT);
            if (tt == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            cn.TinTucs.Remove(tt);
            cn.SaveChanges();
            SetAlert("Xóa thành công!", "success");
            return RedirectToAction("TinTuc", "QuanLyTinTuc");
        }

        //Thêm tin tức
        [HttpGet]
        public ActionResult ThemTT()
        {
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ThemTT(TinTuc tt,HttpPostedFileBase fileUpload)
        {
            var fileName = Path.GetFileName(fileUpload.FileName);
            var path = Path.Combine(Server.MapPath("~/image/TinTuc"), fileName);
            if (System.IO.File.Exists(path))
            {
                ViewBag.ThongBao = "Hình ảnh đã tồn tại...";
            }
            else
            {
                if (ModelState.IsValid)
                {
                    TinTuc tt1 = new TinTuc();                   
                    fileUpload.SaveAs(path);
                    tt1.TenTT = tt.TenTT;
                    tt1.TenTT = tt.TenTT;
                    tt1.AnhBia = fileName;
                    tt1.MoTa = tt.MoTa;
                    cn.TinTucs.Add(tt1);
                    cn.SaveChanges();
                    SetAlert("Thêm thành công!", "success");
                }
            }
            return RedirectToAction("TinTuc", "QuanLyTinTuc");
        }
    }
}
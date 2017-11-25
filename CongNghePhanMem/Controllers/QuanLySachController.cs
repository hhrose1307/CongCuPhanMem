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
    public class QuanLySachController : Controller
    {
        CongNghePhanMemEntities cn = new CongNghePhanMemEntities();
        // GET: QuanLySach
        public ActionResult Index()
        {
            return View();
        }
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
        public ActionResult ChuDe(int?page)
        {
            int pageSize = 25;
            int pageNumber = (page ?? 1);
            var cd = cn.ChuDes.ToList().OrderBy(n=>n.TenChuDe).ToPagedList(pageNumber,pageSize);
            return View(cd);
        }
[HttpGet]
        public ActionResult ThemCD()
        {
            return View();

        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ThemCD(ChuDe cd)
        {
            if(ModelState.IsValid)
            {
                ChuDe cd1 = new ChuDe();
                cd1.TenChuDe = cd.TenChuDe;
                cn.ChuDes.Add(cd1);
                cn.SaveChanges();
                SetAlert("Thêm thành công!", "success");
            }
            return RedirectToAction("ChuDe", "QuanLySach");
        }
        public ActionResult XoaCD(int MaCD=0)
        {
            if(ModelState.IsValid)
            {
                Sach sach = cn.Saches.FirstOrDefault(n => n.MaCD == MaCD);
                if(sach!=null)
                {
                    SetAlert("Tồn tại chủ đề trong quản lý sách!", "warning");
                }
                else
                {
                    ChuDe cd = cn.ChuDes.SingleOrDefault(n => n.MaCD == MaCD);
                    cn.ChuDes.Remove(cd);
                    cn.SaveChanges();
                    SetAlert("Xóa thành công", "success");
                }
                
            }
            return RedirectToAction("ChuDe","QuanLySach");
        }
	 //Loại sách

        public ActionResult LoaiSach(int ?page)
        {
            int pageSize = 25;
            int pageNumber = (page ?? 1);
            var ls = cn.LoaiSaches.ToList().OrderBy(n => n.MaLoai).ToPagedList(pageNumber,pageSize);
            return View(ls);
        }
         
        public ActionResult XoaLoai(int MaLoai=0)
        {
            if(ModelState.IsValid)
            {
                Sach sach = cn.Saches.FirstOrDefault(n => n.MaLoai == MaLoai);
                if(sach!=null)
                {
                    SetAlert("Tồn tại loại trong quản lý sách", "warning");
                }
                else
                {
                    LoaiSach ls = cn.LoaiSaches.SingleOrDefault(n => n.MaLoai == MaLoai);
                    cn.LoaiSaches.Remove(ls);
                    cn.SaveChanges();
                    SetAlert("Xóa thành công!", "success");
                }
                
            }
            return RedirectToAction("LoaiSach");
        }
        [HttpGet]
        public ActionResult ThemLoai()
        {
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ThemLoai(LoaiSach ls)
        {
            if(ModelState.IsValid)
            {
                LoaiSach ls1 = new LoaiSach();
                ls1.TenLoai = ls.TenLoai;
                cn.LoaiSaches.Add(ls1);
                cn.SaveChanges();
                SetAlert("Thêm thành công!", "success");
            }
            return RedirectToAction("LoaiSach");
        }

    }
}
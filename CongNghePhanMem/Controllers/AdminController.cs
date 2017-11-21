using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CongNghePhanMem.Models;
using System.IO;

namespace CongNghePhanMem.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        CongNghePhanMemEntities cn = new CongNghePhanMemEntities();
        protected void SetAlert(string message, string type)
        {
            TempData["AlertMessage"] = message;
            if(type=="success")
            {
                TempData["AlertType"] = "alert-success";
            }
            else if(type=="warning")
            {
                TempData["AlertType"] = "alert-warning";
            }
            else if(type=="error")
            {
                TempData["AlertType"] = "alert-danger";
            }
        }
        public ActionResult Index()
        {
            if (Session["TenDangNhap"] == null||Session["TenDangNhap"].ToString()=="")
            {
                return RedirectToAction("LoginAdmin", "Admin");
            }
            return View(); 
        }
		[HttpGet]
        public ActionResult LoginAdmin()
        {           
            return View();
        }
        [HttpPost]
        public ActionResult LoginAdmin(FormCollection f)
        {
            if(ModelState.IsValid)
            {
                string sTen = f["txtTen"].ToString();
                string sPass = f["txtPass"].ToString();
                NguoiDung nd = cn.NguoiDungs.SingleOrDefault(n => n.TenDangNhap == sTen && n.MatKhau == sPass && n.MaTT == 1);
                if (nd != null)
                {
                    ViewBag.ThongBao = "Thành công";
                    Session["TenDangNhap"] = nd;
                    SetAlert("Đăng nhập thành công!", "success");
                    return RedirectToAction("Index", "Admin");
                }
                ViewBag.ThongBao = "Thất bại";
                return View();
            }
            return View();
            
        }
        public ActionResult LogOut()
        {
            Session["TenDangNhap"] = null;
            return RedirectToAction("LoginAdmin", "Admin");
        }   
		[HttpGet]
        public ActionResult SuaFB()
        {
            GiaoDien gd = cn.GiaoDiens.SingleOrDefault(n => n.ID == 5);
            if(gd==null)
            {
                Response.StatusCode = 404;
                return null;
                
            }
            return View(gd);
        }     
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SuaFB(GiaoDien gd)
        {
            if(ModelState.IsValid)
            {
                GiaoDien gd1 = cn.GiaoDiens.SingleOrDefault(n => n.ID == gd.ID);
                gd1.ThuocTinh = gd.ThuocTinh;
                gd1.GiaTri = gd.GiaTri;
                gd1.GiaTri1 = gd.GiaTri1;
                cn.SaveChanges();
                SetAlert("Sửa địa chỉ mạng xã hội thành công!", "success");
                
            }
            return View();

        }

        public ActionResult TheNganHang()
        {
            List<GiaoDien> gd = cn.GiaoDiens.Where(n => n.sys_del == true).ToList();
            return View(gd);
        }
        [HttpGet]
        public ActionResult ThemThe()
        {
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ThemThe(GiaoDien gd)
        {
            if(ModelState.IsValid)
            {
                GiaoDien gd1 = new GiaoDien();
                gd1.ThuocTinh = gd.ThuocTinh;
                gd1.GiaTri = gd.GiaTri;
                gd1.GiaTri1 = gd.GiaTri1;
                gd1.GiaTri2 = gd.GiaTri2;
                gd1.sys_del = true;
                cn.GiaoDiens.Add(gd1);
                cn.SaveChanges();
                SetAlert("Thêm thẻ ngân hàng thành công!", "success");
            }         
            return RedirectToAction("TheNganHang", "Admin");
        }

        [HttpGet]
        public ActionResult SuaThe(int ID=0)
        {
            GiaoDien gd = cn.GiaoDiens.SingleOrDefault(n => n.ID == ID);
            if (gd == null)
            {
                Response.StatusCode = 404;
                return null;

            }
            return View(gd);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SuaThe(GiaoDien gd)
        {
            if(ModelState.IsValid)
            {
                GiaoDien gd1 = cn.GiaoDiens.SingleOrDefault(n => n.ID == gd.ID);
                gd1.ThuocTinh = gd.ThuocTinh;
                gd1.GiaTri = gd.GiaTri;
                gd1.GiaTri1 = gd.GiaTri1;
                gd1.GiaTri2 = gd.GiaTri2;
                cn.SaveChanges();
                SetAlert("Sửa thẻ ngân hàng thành công!", "success");
            }            
            return RedirectToAction("TheNganHang","Admin");
        }
        public ActionResult XoaThe(int ID=0)
        {
            if(ModelState.IsValid)
            {
                GiaoDien gd = cn.GiaoDiens.SingleOrDefault(n => n.ID == ID);
                if (gd == null)
                {
                    Response.StatusCode = 404;
                    return null;
                }
                cn.GiaoDiens.Remove(gd);
                cn.SaveChanges();
                SetAlert("Xóa thẻ ngân hàng thành công!", "success");
            }
            
            return RedirectToAction("TheNganHang","Admin");
        }
        [HttpGet]
        public ActionResult SuaMap()
        {
            GiaoDien gd = cn.GiaoDiens.SingleOrDefault(n => n.ID == 10);
            return View(gd);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SuaMap(GiaoDien gd)
        {
            if(ModelState.IsValid)
            {
                GiaoDien gd1 = cn.GiaoDiens.SingleOrDefault(n => n.ID == gd.ID);
                gd1.GiaTri = gd.GiaTri;
                gd1.GiaTri1 = gd.GiaTri1;
                gd1.GiaTri2 = gd.GiaTri2;
                cn.SaveChanges();
                SetAlert("Xóa thẻ ngân hàng thành công!", "success");
            }
            
            return View();
        }
        public ActionResult ThongTinWeb()
        {
            List<GiaoDien> nd = cn.GiaoDiens.Where(n => n.ID <=3).ToList();
            return View(nd);
        }
        public ActionResult SuaThongTin(int ID = 0)
        {
            GiaoDien gd = cn.GiaoDiens.SingleOrDefault(n => n.ID == ID);
            if (gd == null)
            {
                Response.StatusCode = 404;
                return null;

            }
            return View(gd);
        }
        
	}
}
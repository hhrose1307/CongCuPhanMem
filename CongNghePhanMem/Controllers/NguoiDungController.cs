using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CongNghePhanMem.Models;

namespace CongNghePhanMem.Controllers
{
    public class NguoiDungController : Controller
    {
        CongNghePhanMemEntities cn = new CongNghePhanMemEntities();
        // GET: NguoiDung
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
        public ActionResult DangNhap()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangNhap(FormCollection f, string Command)
        {
            if (Command == "Đăng nhập")
            {
                string sTen = f["txtTen"].ToString();
                string sPass = f["txtPass"].ToString();
                NguoiDung nd = cn.NguoiDungs.SingleOrDefault(n => n.TenDangNhap == sTen && n.MatKhau == sPass);
                if (nd!= null)
                {
                    Session["TenDangNhap"] = nd;
                    ViewBag.NguoiDung = nd.MaND;
                    SetAlert("Đăng nhập thành công!", "success");
                    return View();
                }
                SetAlert("Sai tài khoản, đăng nhập thất bại!", "warning");
                return View();
            }
            return null;
        }
        [HttpGet]
        public ActionResult DangKy()
        {
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult DangKy(NguoiDung nd, string Command)
        {
            if (Command == "Đăng Ký")
            {
                NguoiDung nd1 = new NguoiDung();
                nd1.TenDangNhap = nd.TenDangNhap;
                nd1.MatKhau = nd.MatKhau;
                nd1.HoTen = nd.HoTen;
                nd1.Email = nd.Email;
                nd1.SDT = nd.SDT;
                nd1.AnhDaiDien = "noavatar.jpg";
                nd1.MaTT = 2;
                cn.NguoiDungs.Add(nd1);
                cn.SaveChanges();
                SetAlert("Đăng ký thành công, vui lòng đăng nhập", "success");
            }
            return View();
        }
        public ActionResult LogOut()
        {
            Session["TenDangNhap"] = null;
            SetAlert("Bạn đã đăng xuất, đăng nhập để thực hiện nhiều tác vụ!", "success");
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult ThongTin()
        {
            NguoiDung nd = (NguoiDung)Session["TenDangNhap"];
            if (Session["TenDangNhap"]==null||Session["TenDangNhap"].ToString()=="")
            {
                SetAlert("Bạn chưa đăng nhập!", "warning");
                return RedirectToAction("Index", "Home");
            }
            else
            {
                NguoiDung lst = cn.NguoiDungs.SingleOrDefault(n => n.MaND == nd.MaND);
                return View(lst);
            }
            
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ThongTin(NguoiDung nd)
        {
            NguoiDung nd1 = cn.NguoiDungs.SingleOrDefault(n=>n.MaND==nd.MaND);
            if(nd==null)
            {
                Response.StatusCode = 404;
                return null;
            }
            nd1.TenDangNhap = nd.TenDangNhap;
            nd1.MatKhau = nd.MatKhau;
            nd1.HoTen = nd.HoTen;
            nd1.Email = nd.Email;
            nd1.SDT = nd.SDT;
            nd1.NgaySinh = nd.NgaySinh;
            nd1.GioiTinh = nd.GioiTinh;
            cn.SaveChanges();
            SetAlert("Lưu thông tin thành công!", "success");
            return View();
        }
        [HttpGet]
        public ActionResult QuenMatKhau()
        {
            return View();
        }
        [HttpPost]
        public ActionResult QuenMatKhau(FormCollection f)
        {
            string sTen = f["txtTen"].ToString();
            string sEmail = f["txtEmail"].ToString();
            string sMK = f["txtPass"].ToString();
            NguoiDung nd = cn.NguoiDungs.SingleOrDefault(n => n.TenDangNhap == sTen);
            if(nd==null)
            {
                ViewBag.ThongBao = "Nhập sai tên đăng nhập !";
                return View();
            }
            else
            {
                if (nd.Email != sEmail)
                {
                    ViewBag.ThongBao = "Nhập sai Email !";
                    return View();
                }
            }            
            nd.MatKhau = sMK;
            cn.SaveChanges();
            SetAlert("Lấy mật khẩu thành công, vui lòng đăng nhập lại!", "success");
            return RedirectToAction("Index","Home");
        }
    }
    
}
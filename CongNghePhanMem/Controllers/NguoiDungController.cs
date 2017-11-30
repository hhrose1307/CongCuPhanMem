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
      
        }
    }
    
}
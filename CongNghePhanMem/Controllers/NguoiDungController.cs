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
       
    
}
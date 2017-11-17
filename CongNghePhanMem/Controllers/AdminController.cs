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
        
	}
}
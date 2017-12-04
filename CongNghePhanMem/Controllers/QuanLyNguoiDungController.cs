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
    public class QuanLyNguoiDungController : Controller
    {
        CongNghePhanMemEntities cn = new CongNghePhanMemEntities();
        // GET: QuanLyNguoiDung
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

        
    }
}
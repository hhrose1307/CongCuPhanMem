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
    public class QuanLyKhuyenMaiController : Controller
    {
        // GET: QuanLyKhuyenMai
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
        public ActionResult KhuyenMai(int ?page)
        {
            int pageSize = 15;
            int pageNumber = (page ?? 1);
            var km = cn.KhuyenMais.ToList().OrderBy(n => n.MaKM).ToPagedList(pageNumber,pageSize);
            return View(km);
        }


    }
}
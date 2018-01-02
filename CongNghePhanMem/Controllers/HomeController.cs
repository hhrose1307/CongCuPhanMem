using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CongNghePhanMem.Models;

namespace CongNghePhanMem.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        CongNghePhanMemEntities cn = new CongNghePhanMemEntities();
        public ActionResult Index()
        {
            return View();
        }
        public PartialViewResult MapApi()
        {
            return PartialView();
        }
        public ActionResult LienHe()
        {
            return View();
        }
        public PartialViewResult ThanhToan()
        {
            var lst = cn.GiaoDiens.Where(n => n.sys_del == true).ToList();
            return PartialView(lst);
        }
        public PartialViewResult Map()
        {
            ViewBag.Ten = cn.GiaoDiens.Single(n => n.ID == 10).GiaTri.ToString();
            ViewBag.Toado1 = cn.GiaoDiens.Single(n => n.ID == 10).GiaTri1.ToString();
            ViewBag.Toado2 = cn.GiaoDiens.Single(n => n.ID == 10).GiaTri2.ToString();
            ViewBag.Diachi = cn.GiaoDiens.Single(n => n.ID == 4).GiaTri.ToString();
            return PartialView();
        }
    }
}
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
    public class LoaiSachController : Controller
    {
        // GET: LoaiSach
        CongNghePhanMemEntities cn = new CongNghePhanMemEntities();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult LoaiSachPartial()
        {
            var lst = cn.LoaiSaches.Take(18).ToList();
            return PartialView(lst);
        }
       
    }
}
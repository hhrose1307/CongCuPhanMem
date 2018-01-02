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
    public class TacGiaController : Controller
    {
        //GET: TacGia
        public ActionResult Index()
        {
            return View();
        }
        CongNghePhanMemEntities cn = new CongNghePhanMemEntities();
        public PartialViewResult TenTacGiaPartial()
        {
            var lst = cn.TacGias.ToList();
            return PartialView(lst);
        }
        public ViewResult SachTheoTacGia(int? page, int MaTG = 0)
        {
            int pageSize = 18;
            int pageNumber = (page ?? 1);
            TacGia cd = cn.TacGias.SingleOrDefault(n => n.MaTG == MaTG);
            if (cd == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            var lst = cn.VietSaches.Where(n => n.MaTG == MaTG).ToList().OrderBy(n => n.MaSach).ToPagedList(pageNumber, pageSize);
            ViewBag.Ten = cn.TacGias.Single(n => n.MaTG == MaTG).TenTG.ToString();
            if (lst.Count == 0)
            {
                ViewBag.Sach = "Không có sách thuộc tác giả này!";
            }
            return View(lst);
        }
    }
}
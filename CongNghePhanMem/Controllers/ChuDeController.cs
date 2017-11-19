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
    public class ChuDeController : Controller
    {
        // GET: ChuDe
        public ActionResult Index()
        {
            return View();
        }
        CongNghePhanMemEntities cn = new CongNghePhanMemEntities();
        public PartialViewResult ChuDePartial()
        {
            var lst = cn.ChuDes.ToList();
            return PartialView(lst);
        }
        public ViewResult SachTheoChuDe(int? page,int MaCD = 0)
        {
            int pageSize = 18;
            int pageNumber = (page ?? 1);
            ChuDe cd = cn.ChuDes.SingleOrDefault(n => n.MaCD == MaCD);
            if (cd == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            var lst = cn.Saches.Where(n => n.MaCD == MaCD).ToList().OrderBy(n => n.TenSach).ToPagedList(pageNumber,pageSize);
            if (lst.Count == 0)
            {
                ViewBag.Sach = "Không có sách nào thuộc chủ đề!";
            }
            else
            {
                ViewBag.Sach = "Chủ đề:" + cd.TenChuDe;
            }
            
            return View(lst);
        }

    }
}
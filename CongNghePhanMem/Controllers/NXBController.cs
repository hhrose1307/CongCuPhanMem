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
    public class NXBController : Controller
    {
        // GET: NXB
        CongNghePhanMemEntities cn = new CongNghePhanMemEntities();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult NXBPartial()
        {
            var lst = cn.NhaXuatBans.ToList();
            return PartialView(lst);
        }
        public ViewResult SachTheoNXB(int? page, int MaNXB = 0)
        {
            int pageSize = 18;
            int pageNumber = (page ?? 1);
            NhaXuatBan cd = cn.NhaXuatBans.SingleOrDefault(n => n.MaNXB == MaNXB);
            if (cd == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            var lst = cn.Saches.Where(n => n.MaNXB == MaNXB).ToList().OrderBy(n => n.TenSach).ToPagedList(pageNumber, pageSize);
            if (lst.Count == 0)
            {
                ViewBag.Sach = "Không có sách thuộc nhà xuất bản này!";
            }
            else
            {
                ViewBag.Sach = "Nhà xuất bản:" + cd.TenNXB;
            }
            
            return View(lst);
        }
    }
}
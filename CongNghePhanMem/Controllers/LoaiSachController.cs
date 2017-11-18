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
        public ViewResult SachTheoLoai(int? page, int MaLoai = 0)
        {
            int pageSize = 18;
            int pageNumber = (page ?? 1);
            LoaiSach cd = cn.LoaiSaches.SingleOrDefault(n => n.MaLoai == MaLoai);
            if (cd == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            var lst = cn.Saches.Where(n => n.MaLoai == MaLoai).ToList().OrderBy(n => n.TenSach).ToPagedList(pageNumber, pageSize);
            if (lst.Count == 0)
            {
                ViewBag.Sach = "Không có sách thuộc loại này!";
            }
            else
            {
                ViewBag.Sach = "Loại:"+cd.TenLoai;
            }          
            return View(lst);
        }
    }
}
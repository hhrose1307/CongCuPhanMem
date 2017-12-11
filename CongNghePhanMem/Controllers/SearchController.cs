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
    public class SearchController : Controller
    {
       // GET: Search
        CongNghePhanMemEntities cn = new CongNghePhanMemEntities();
        [HttpPost]
        public ActionResult KetQuaSearch(FormCollection f, int? page,string Command)
        {
            if (Command == "Tìm kiếm")
            {
                string sTuKhoa = f["txtSearch"].ToString();
                ViewBag.TuKhoa = sTuKhoa;
                List<Sach> lstSearch = cn.Saches.Where(n => n.TenSach.Contains(sTuKhoa)).ToList();
                //phân trang
                int pageNumber = (page ?? 1);
                int pageSize = 18;
                //kieetm tra
                if (lstSearch.Count == 0)
                {
                    ViewBag.ThongBao = "Không tìm thấy sản phẩm nào";
                    return View(cn.Saches.OrderBy(n => n.TenSach).ToPagedList(pageNumber, pageSize));
                }
                ViewBag.ThongBao = "Đã tìm thấy" + lstSearch.Count + "kết quả.";
                return View(lstSearch.OrderBy(n => n.TenSach).ToPagedList(pageNumber, pageSize));
            }
            else
                return View();
        }
	 [HttpGet]
        public ActionResult KetQuaSearch(string sTuKhoa, int? page)
        {
            List<Sach> lstSearch = cn.Saches.Where(n => n.TenSach.Contains(sTuKhoa)).ToList();
            //phân trang
            int pageNumber = (page ?? 1);
            int pageSize = 18;
            //kieetm tra
            if (lstSearch.Count == 0)
            {
                ViewBag.ThongBao = "Không tìm thấy sản phẩm nào";
                return View(cn.Saches.OrderBy(n => n.TenSach).ToPagedList(pageNumber, pageSize));
            }
            ViewBag.ThongBao = "Đã tìm thấy"  +   lstSearch.Count   +  "kết quả!";
            return View(lstSearch.OrderBy(n => n.TenSach).ToPagedList(pageNumber, pageSize));
        }
    }
}
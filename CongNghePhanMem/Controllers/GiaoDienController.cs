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
    public class GiaoDienController : Controller
    {
        // GET: GiaoDien
        public ActionResult Index()
        {
            return View();
        }
        CongNghePhanMemEntities cn = new CongNghePhanMemEntities();
        public PartialViewResult Logo()
        {
            GiaoDien gd = cn.GiaoDiens.SingleOrDefault(n => n.ID == 1);
            return PartialView(gd);
        }
        public PartialViewResult TieuDe()
        {
            GiaoDien tieude = cn.GiaoDiens.SingleOrDefault(n => n.ID == 2);
            return PartialView(tieude);
        }
        public PartialViewResult DienThoai()
        {   
            GiaoDien dt = cn.GiaoDiens.SingleOrDefault(n => n.ID == 3);
            return PartialView(dt);
        }
        public PartialViewResult DiaChi()
        {
            GiaoDien diachi = cn.GiaoDiens.SingleOrDefault(n => n.ID == 4);
            return PartialView(diachi);
        }
        public PartialViewResult FaceBook()
        {
            GiaoDien fb = cn.GiaoDiens.SingleOrDefault(n => n.ID == 5);
            return PartialView(fb);
        }
        public ActionResult Ykien(int ?page)
        {
            NguoiDung nd = (NguoiDung)Session["TenDangNhap"];
            if (Session["TenDangNhap"] == null || Session["TenDangNhap"].ToString() == "")
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                int pageSize = 2;
                int pageNumber = (page ?? 1);
                var lst = cn.PhanHois.Where(n => n.MaND == nd.MaND).ToList().OrderBy(n=>n.MaPH).ToPagedList(pageNumber,pageSize);
                ViewBag.Ten = nd.HoTen;
                ViewBag.Anh = nd.AnhDaiDien;
                return View(lst);
            }
        }

        [HttpGet]
        public ActionResult Send()
        {
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Send(FormCollection f)
        {
            //Kiểm tra nội dung tồn tại
            if (f["txtNoiDung"] == null|| f["txtNoiDung"] =="")
            {
                return View();
            }
            PhanHoi ddh = new PhanHoi();
            NguoiDung nd = (NguoiDung)Session["TenDangNhap"];
            //Thêm phản hôig
            ddh.MaND = nd.MaND;
            ddh.NoiDung = f["txtNoiDung"].ToString();
            cn.PhanHois.Add(ddh);
            cn.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
    }
}
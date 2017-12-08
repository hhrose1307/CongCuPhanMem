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

        public ActionResult TinhTrang()
        {
            List<TinhTrangNguoiDung> nguoidung = cn.TinhTrangNguoiDungs.ToList();
            return View(nguoidung);
        }
        [HttpGet]
		public ActionResult Them()
        {
            return View();
        }
        [HttpPost]
        //[ValidateInput(false)]
        public ActionResult Them(TinhTrangNguoiDung nd)
        {
            if(ModelState.IsValid)
            {
                cn.TinhTrangNguoiDungs.Add(nd);
                cn.SaveChanges();
                SetAlert("Thêm thành công!", "success");
            }
            return RedirectToAction("TinhTrang", "QuanLyNguoiDung");
        } 
        [HttpGet]
        public ActionResult SuaTT(int MaTT=0)
        {
            TinhTrangNguoiDung nd = cn.TinhTrangNguoiDungs.SingleOrDefault(n => n.MaTT == MaTT);
            return View(nd);
        }
        [HttpPost]
        public ActionResult SuaTT(TinhTrangNguoiDung nd)
        {
            if(ModelState.IsValid)
            {
                TinhTrangNguoiDung nd1 = cn.TinhTrangNguoiDungs.SingleOrDefault(n => n.MaTT == nd.MaTT);
                nd1.TenTT = nd.TenTT;
                cn.SaveChanges();
                SetAlert("Sửa thành công!","success");
            }
            return RedirectToAction("TinhTrang", "QuanLyNguoiDung");         
        }
		public ActionResult NguoiDung(int ? page)
        {
            int pageSize = 20;
            int pageNumber = (page ?? 1);
            var nd = cn.NguoiDungs.ToList().OrderBy(n=>n.TenDangNhap).ToPagedList(pageNumber,pageSize);
            return View(nd);
        }
        [HttpGet]
        public ActionResult SuaNguoiDung(int MaND=0)
        {
            NguoiDung nd = cn.NguoiDungs.SingleOrDefault(n => n.MaND == MaND);
            //lấy dữ liệu vao dropdown
            ViewBag.MaTT = new SelectList( cn.TinhTrangNguoiDungs.ToList(),"MaTT","TenTT",nd.MaND);
            return View();
        }
        public ActionResult SuaNguoiDung(NguoiDung nd)
        {
            if(ModelState.IsValid)
            {
                NguoiDung nd1 = cn.NguoiDungs.SingleOrDefault(n => n.MaND == nd.MaND);
                nd1.MaTT = nd.MaTT;
                cn.SaveChanges();
                SetAlert("Sửa thành công!", "success");
            }
            return RedirectToAction("NguoiDung", "QuanLyNguoiDung");                       
        }
    }
}
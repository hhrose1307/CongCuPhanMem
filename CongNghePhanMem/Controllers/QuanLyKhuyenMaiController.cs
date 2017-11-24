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
		[HttpGet]

        public ActionResult ThemKM()
        {
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ThemKM(KhuyenMai km, HttpPostedFileBase fileupload)
        {
            if(ModelState.IsValid)
            {

                var fileName = Path.GetFileName(fileupload.FileName);
                var path = Path.Combine(Server.MapPath("~/image/KhuyenMai"), fileName);
                if (System.IO.File.Exists(path))
                {
                    ViewBag.ThongBao = "Hình ảnh đã tồn tại...";
                    SetAlert("Hình ảnh đã tồn tại","warning");
                }
                else
                {
                    fileupload.SaveAs(path);
                    KhuyenMai km1 = new KhuyenMai();
                    km1.TenKM = km.TenKM;
                    km1.NgayBatDau = km.NgayBatDau;
                    km1.NgayKetThuc = km.NgayKetThuc;
                    km1.AnhKM = fileName;
                    km1.NoiDung = km.NoiDung;
                    cn.KhuyenMais.Add(km1);
                    cn.SaveChanges();
                    SetAlert("Thêm thành công!", "success");
                }

            }
            return RedirectToAction("KhuyenMai");
        }

        public ActionResult XoaKM(int MaKM=0)
        {
            if(ModelState.IsValid)
            {
                ChiTietKhuyenMai ct = cn.ChiTietKhuyenMais.FirstOrDefault(n => n.MaKM == MaKM);
                if(ct!=null)
                {
                    SetAlert("Tồn tại sách thuộc khuyến mãi này!","warning");
                }
                else
                {
                    KhuyenMai km = cn.KhuyenMais.SingleOrDefault(n => n.MaKM == MaKM);
                    if (km == null)
                    {
                        Response.StatusCode = 404;
                        return null;
                    }
                    cn.KhuyenMais.Remove(km);
                    cn.SaveChanges();
                    SetAlert("Xóa thành công!", "success");
                }
                
            }
            return RedirectToAction("KhuyenMai");
        }
        [HttpGet]
        public ActionResult SuaKM(int MaKM=0)
        {
            KhuyenMai km = cn.KhuyenMais.SingleOrDefault(n => n.MaKM == MaKM);
            if(km==null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(km);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SuaKM(KhuyenMai km)
        {
            if(ModelState.IsValid)
            {
                KhuyenMai km1 = cn.KhuyenMais.SingleOrDefault(n => n.MaKM == km.MaKM);
                if(km1==null)
                {
                    Response.StatusCode=404;
                    return null;
                }
                 km1.TenKM = km.TenKM;
                 km1.NgayBatDau = km.NgayBatDau;
                 km1.NgayKetThuc = km.NgayKetThuc;
                 km1.NoiDung = km.NoiDung;
                 cn.SaveChanges();
                 SetAlert("Sửa thành công!", "success");
            }
            return RedirectToAction("KhuyenMai");
        }
		public ActionResult ChiTietKM(int ?page,int MaKM=0)
        {
            int pageSize = 25;
            int pageNumber = (page ?? 1);
            var ct = cn.ChiTietKhuyenMais.Where(n => n.MaKM == MaKM).ToList().OrderBy(n=>n.MaSach).ToPagedList(pageNumber,pageSize);
            Session["MaKM"] = MaKM;
            return View(ct);
        }
        [HttpGet]
        public ActionResult ThemSachKM()
        {
            ViewBag.MaSach = new SelectList(cn.Saches.Where(n=>n.GiaKhuyenMai==null).OrderBy(n => n.TenSach).ToList(), "MaSach", "TenSach");
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ThemSachKM(ChiTietKhuyenMai ct)
        {
            if(ModelState.IsValid)
            {
                Sach sach = cn.Saches.SingleOrDefault(n => n.MaSach == ct.MaSach);
                if(sach.GiaBan<ct.GiamGia)
                {
                    SetAlert("Giá bán cao hơn giá bán!", "warning");
                }
                else
                {
                    string ma = Session["MaKM"].ToString();
                    ChiTietKhuyenMai ct1 = new ChiTietKhuyenMai();
                    ct1.MaSach = ct.MaSach;
                    ct1.MaKM = int.Parse(ma);
                    ct1.GiamGia = ct.GiamGia;
                    cn.ChiTietKhuyenMais.Add(ct1);
                    cn.SaveChanges();
                    SetAlert("Thêm thành công", "success");
                }               
            }
            return RedirectToAction("ChiTietKM",new { @MaKM = Session["MaKM"]});
        }
        public ActionResult XoaSachKM(int MaKM = 0, int MaSach = 0)
        {
            if(ModelState.IsValid)
            {

                ChiTietKhuyenMai ct = cn.ChiTietKhuyenMais.SingleOrDefault(n => n.MaKM == MaKM && n.MaSach == MaSach);
                if(ct==null)
                {
                    Response.StatusCode = 404;
                    return null;
                }
                cn.ChiTietKhuyenMais.Remove(ct);
                cn.SaveChanges();
                SetAlert("Xoa thành công", "success");
            }
            return RedirectToAction("ChiTietKM", new { @MaKM = Session["MaKM"] });
        }

    }
}
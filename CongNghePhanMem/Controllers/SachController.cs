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
    public class SachController : Controller
    {
        // GET: Sach
        public ActionResult Index()
        {
            return View();
        }
        CongNghePhanMemEntities cn = new CongNghePhanMemEntities();
        public PartialViewResult KhuyenMaiPartial()
        {
            var lst = cn.KhuyenMais.Where(n => n.NgayKetThuc >= DateTime.Now).ToList();
            return PartialView(lst);
        }
        public PartialViewResult AllSach(int ? page)
        {
            int pageSize = 18;
            int pageNumber = (page ?? 1);
            var lst = cn.Saches.ToList().OrderBy(n=>n.TenSach).ToPagedList(pageNumber,pageSize);
            return PartialView(lst);
        }
        public PartialViewResult SachMoi()
        {
            var lst = cn.Saches.Where(n => n.SachMoi == 1).Take(18).OrderBy(n=>n.TenSach).ToList();
            return PartialView(lst);
        }
        public PartialViewResult MuaNhieu()
        {
            var lst = cn.Saches.OrderByDescending(n=>n.SoLuotXem).Take(18).ToList();
            return PartialView(lst);
        }
        public PartialViewResult KhuyenMai()
        {
            var lst = cn.Saches.Where(n => n.GiaKhuyenMai != null).Take(18).ToList();
            return PartialView(lst);
        }
        public PartialViewResult AllSachMoi(int? page)
        {
            int pageSize = 18;
            int pageNumber = (page ?? 1);
            var lst = cn.Saches.Where(n=>n.SachMoi==1).ToList().OrderBy(n => n.TenSach).ToPagedList(pageNumber, pageSize);
            return PartialView(lst);
        }
        public PartialViewResult AllKhuyenMai(int? page)
        {
            int pageSize = 18;
            int pageNumber = (page ?? 1);
            var lst = cn.Saches.Where(n => n.GiaKhuyenMai!=null).ToList().OrderBy(n => n.TenSach).ToPagedList(pageNumber, pageSize);
            return PartialView(lst);
        }
        public ViewResult XemChiTiet()
        {
            return View();
        }
        public ViewResult XemKhuyenMai(int MaKM=0)
        {
            KhuyenMai km = cn.KhuyenMais.SingleOrDefault(n => n.MaKM == MaKM);
            if(km==null)
            {
                Response.StatusCode = 404;
                return null;
            }
            var lst = cn.KhuyenMais.Where(n => n.MaKM == MaKM).ToList();
            return View(lst);

        }
        public ViewResult ChiTiet(int MaSach=0)
        {
            Sach sach = cn.Saches.SingleOrDefault(n => n.MaSach == MaSach);
            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            ViewBag.TenChuDe = cn.ChuDes.Single(n => n.MaCD == sach.MaCD).TenChuDe;
            ViewBag.TenLoai = cn.LoaiSaches.Single(n => n.MaLoai == sach.MaLoai).TenLoai;
            ViewBag.TenNhaXuatBan = cn.NhaXuatBans.Single(n => n.MaNXB == sach.MaNXB).TenNXB;
            var vs = cn.VietSaches.FirstOrDefault(n => n.MaSach == sach.MaSach);
            ViewBag.TenTG = cn.TacGias.FirstOrDefault(n => n.MaTG == vs.MaTG).TenTG;

            if(sach.SLTon==0)
            {
                ViewBag.TinhTrang = "Hết hàng";
            }
            else
            {
                ViewBag.TinhTrang = "Còn hàng";
            }
            return View(sach);
        }
        public ViewResult ChiTietKhuyenMai(int MaKM=0)
        {
            KhuyenMai km = cn.KhuyenMais.SingleOrDefault(n => n.MaKM == MaKM);
            if (km == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(km);
        }
        public ViewResult SachThuocKM(int?page, int MaKM=0)
        {
            int pageSize = 18;
            int pageNumber = (page ?? 1);
            KhuyenMai km = cn.KhuyenMais.SingleOrDefault(n => n.MaKM == MaKM);
            if (km == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            var lst = cn.ChiTietKhuyenMais.Where(n => n.MaKM == MaKM).ToList().OrderBy(n => n.MaSach).ToPagedList(pageNumber, pageSize);
            return View(lst);
        }
        public PartialViewResult SachRight()
        {
            var lst = cn.Saches.Where(n => n.GiaKhuyenMai != null).Take(5).ToList();           
            return PartialView(lst);
        }

    }
    
}
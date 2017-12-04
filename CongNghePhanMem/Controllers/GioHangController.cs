using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CongNghePhanMem.Models;
using System.Configuration;

namespace CongNghePhanMem.Controllers
{
    public class GioHangController : Controller
    {
        //Lấy giỏ hàng
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
        public List<GioHang> LayGioHang()
        {
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang == null)
            {
                lstGioHang = new List<GioHang>();
                Session["GioHang"] = lstGioHang;

            }
            return lstGioHang;
        }
        //Thêm giỏ hàng
        public ActionResult ThemGioHang(int iMaSach, string strURL)
        {
            Sach sach = cn.Saches.SingleOrDefault(n => n.MaSach == iMaSach);
            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }//bắt lỗi khi sp hết hàng
            if (sach.SLTon == 0)
            {
                return Redirect(strURL);
            }
            else
            {
                //Lấy session giỏ hàng
                List<GioHang> lstGioHang = LayGioHang();
                GioHang sanpham = lstGioHang.Find(n => n.iMaSach == iMaSach);
                if (sanpham == null)
                {
                    sanpham = new GioHang(iMaSach);
                    lstGioHang.Add(sanpham);
                    return Redirect(strURL);
                }
                else
                {
                    sanpham.iSoLuong++;
                    return Redirect(strURL);
                }

            }
            
        }
        //sửa
        public ActionResult SuaGioHang(int iMaSP, FormCollection f)
        {
            Sach sach = cn.Saches.SingleOrDefault(n => n.MaSach == iMaSP);
            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            //Bắt lỗi
            if(int.Parse(f["txtSoLuong"].ToString())>sach.SLTon)
            {
                ViewBag.BatLoi = "Số lượng không đủ";
                SetAlert("Số lượng sách không đủ!", "warning");
                return Redirect("GioHang");
            }
            else
            {
                List<GioHang> lstGioHang = LayGioHang();
                GioHang sanpham = lstGioHang.SingleOrDefault(n => n.iMaSach == iMaSP);
                if (sanpham != null)
                {
                    sanpham.iSoLuong = int.Parse(f["txtSoLuong"].ToString());
                }
                ViewBag.BatLoi="Cập nhật thành công";
                SetAlert("Cập nhật thành công!", "success");
                return RedirectToAction("GioHang");
            }
            
        }
        //xóa
        public ActionResult XoaGioHang(int iMaSP)
        {
            Sach sach = cn.Saches.SingleOrDefault(n => n.MaSach == iMaSP);
            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            List<GioHang> lstGioHang = LayGioHang();
            GioHang sanpham = lstGioHang.SingleOrDefault(n => n.iMaSach == iMaSP);
            if (sanpham != null)
            {
                lstGioHang.RemoveAll(n => n.iMaSach == iMaSP);
                SetAlert("Xóa giỏ hàng thành công!", "success");


            }
            if (lstGioHang.Count == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("GioHang");
        }
        public ActionResult GioHang()
        {
            if (Session["GioHang"] == null)
            {
                RedirectToAction("Index", "Home");
            }
            List<GioHang> lstGioHang = LayGioHang();
            ViewBag.Tong = TongTien();
            ViewBag.sl = TongSoLuong();
            return View(lstGioHang);
        }
        
        
    }
}
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
        //Tính tổng sl
        private int TongSoLuong()
        {
            int iTongSoLuong = 0;
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang != null)
            {
                iTongSoLuong = lstGioHang.Sum(n => n.iSoLuong);
            }
            return iTongSoLuong;
        }
        public double TongTien()
        {
            double dTongTien = 0;
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang != null)
            {
                dTongTien = lstGioHang.Sum(n => n.dThanhTien);
            }
            return dTongTien;

        }
        public ActionResult GioHangPartial()
        {
            if (TongSoLuong() == 0)
            {
                return PartialView();
            }
            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongTien = TongTien();
            return PartialView();
        }
        //Xây dựng đặt hàng
        [HttpGet]
        public ActionResult DatHang()
        {
            return View();
        }

        [HttpPost]

        public ActionResult DatHang(FormCollection f)
        {
            //Kiểm tra tồn tại giỏ hàng
            if(Session["GioHang"]==null)
            {
                RedirectToAction("Index", "Home");
            }
            //Thêm đơn hàng
            DonDatHang ddh = new DonDatHang();
            List<GioHang> gh = LayGioHang();
            NguoiDung nd = (NguoiDung)Session["TenDangNhap"];
            //Thêm đơn hàng
            if(Session["TenDangNhap"] == null || Session["TenDangNhap"].ToString() == "")
            {
                ddh.MaND = null;
            }
            else
            {
                ddh.MaND = nd.MaND;
            }
            ddh.NgayDat = DateTime.Now;
            ddh.HoTen = f["txtHoTen"].ToString();
            ddh.DiaChi = f["txtDiaChi"].ToString();
            ddh.SDT = f["txtDT"].ToString();
            ddh.Email = f["txtMail"].ToString();
            ddh.Tongtien = (decimal)TongTien();
            cn.DonDatHangs.Add(ddh);
            cn.SaveChanges();           
            //thêm chi tiết đơn hàng
            foreach(var item in gh)
            {
                ChiTietDonHang ctdh = new ChiTietDonHang();
                ctdh.MaDH = ddh.MaDH;
                ctdh.MaSach = item.iMaSach;
                ctdh.SoLuong = item.iSoLuong;
                ctdh.DonGia =(decimal)item.dDonGia;
                cn.ChiTietDonHangs.Add(ctdh);
            }
            cn.SaveChanges();
            try
            {
                string content = System.IO.File.ReadAllText(Server.MapPath("~/Content/TT/ttkhm.html"));

                content = content.Replace("{{CustomerName}}", ddh.HoTen);
                content = content.Replace("{{Phone}}", ddh.SDT);
                content = content.Replace("{{Email}}", ddh.Email);
                content = content.Replace("{{Address}}", ddh.DiaChi);
                content = content.Replace("{{Total}}", ddh.Tongtien.ToString());
                var toEmail = ConfigurationManager.AppSettings["ToEmailAddress"].ToString();

                new MailHelper().SendMail(ddh.Email, "Đơn hàng mới từ Hoàn Đa Cấp", content);
                new MailHelper().SendMail(toEmail, "Đơn hàng mới từ Hoàn Đa Cấp", content);
            }
            catch
            {
                ViewBag.error = "fail";
            }
            SetAlert("Đặt hàng thành công!", "success");
            return RedirectToAction("AllSach", "Sach");
        }
        
        
    }
}
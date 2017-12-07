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
    public class DonHangController : Controller
    {
       
        // GET: DonHang
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

        public ActionResult DonHang(int ? page)
        {
            int pageSize = 15;
            int pageNumber = (page ?? 1);
            var dh = cn.DonDatHangs.Where(n=>n.TinhTrangGiaoHang==null||n.DaThanhToan==null).ToList().OrderBy(n=>n.MaDH).ToPagedList(pageNumber,pageSize);
            return View(dh);
        }
        //xóa
        public ActionResult XoaDH(int MaDH=0)
        {
            if(ModelState.IsValid)
            {
                List<ChiTietDonHang> ls = cn.ChiTietDonHangs.Where(n => n.MaDH == MaDH).ToList();
                for (int i = 0; i < ls.Count; i++)
                {
                    ChiTietDonHang ct = cn.ChiTietDonHangs.FirstOrDefault(n => n.MaDH == MaDH);
                    cn.ChiTietDonHangs.Remove(ct);
                    cn.SaveChanges();
                }
                DonDatHang dh = cn.DonDatHangs.SingleOrDefault(n => n.MaDH==MaDH);
                cn.DonDatHangs.Remove(dh);
                cn.SaveChanges();
                SetAlert("Xóa thành công!", "success");
            }
            return RedirectToAction("DonHang");
            
        }
    }

}
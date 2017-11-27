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
    public class QuanLySachController : Controller
    {
        CongNghePhanMemEntities cn = new CongNghePhanMemEntities();
        // GET: QuanLySach
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
        public ActionResult ChuDe(int?page)
        {
            int pageSize = 25;
            int pageNumber = (page ?? 1);
            var cd = cn.ChuDes.ToList().OrderBy(n=>n.TenChuDe).ToPagedList(pageNumber,pageSize);
            return View(cd);
        }
[HttpGet]
        public ActionResult ThemCD()
        {
            return View();

        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ThemCD(ChuDe cd)
        {
            if(ModelState.IsValid)
            {
                ChuDe cd1 = new ChuDe();
                cd1.TenChuDe = cd.TenChuDe;
                cn.ChuDes.Add(cd1);
                cn.SaveChanges();
                SetAlert("Thêm thành công!", "success");
            }
            return RedirectToAction("ChuDe", "QuanLySach");
        }
        public ActionResult XoaCD(int MaCD=0)
        {
            if(ModelState.IsValid)
            {
                Sach sach = cn.Saches.FirstOrDefault(n => n.MaCD == MaCD);
                if(sach!=null)
                {
                    SetAlert("Tồn tại chủ đề trong quản lý sách!", "warning");
                }
                else
                {
                    ChuDe cd = cn.ChuDes.SingleOrDefault(n => n.MaCD == MaCD);
                    cn.ChuDes.Remove(cd);
                    cn.SaveChanges();
                    SetAlert("Xóa thành công", "success");
                }
                
            }
            return RedirectToAction("ChuDe","QuanLySach");
        }
	 //Loại sách

        public ActionResult LoaiSach(int ?page)
        {
            int pageSize = 25;
            int pageNumber = (page ?? 1);
            var ls = cn.LoaiSaches.ToList().OrderBy(n => n.MaLoai).ToPagedList(pageNumber,pageSize);
            return View(ls);
        }
         
        public ActionResult XoaLoai(int MaLoai=0)
        {
            if(ModelState.IsValid)
            {
                Sach sach = cn.Saches.FirstOrDefault(n => n.MaLoai == MaLoai);
                if(sach!=null)
                {
                    SetAlert("Tồn tại loại trong quản lý sách", "warning");
                }
                else
                {
                    LoaiSach ls = cn.LoaiSaches.SingleOrDefault(n => n.MaLoai == MaLoai);
                    cn.LoaiSaches.Remove(ls);
                    cn.SaveChanges();
                    SetAlert("Xóa thành công!", "success");
                }
                
            }
            return RedirectToAction("LoaiSach");
        }
        [HttpGet]
        public ActionResult ThemLoai()
        {
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ThemLoai(LoaiSach ls)
        {
            if(ModelState.IsValid)
            {
                LoaiSach ls1 = new LoaiSach();
                ls1.TenLoai = ls.TenLoai;
                cn.LoaiSaches.Add(ls1);
                cn.SaveChanges();
                SetAlert("Thêm thành công!", "success");
            }
            return RedirectToAction("LoaiSach");
        }
	//Tác giả
        public ActionResult TacGia(int ? page)
        {
            int pageSize = 30;
            int pageNumber = (page ?? 1);
            var tg = cn.TacGias.ToList().OrderBy(n => n.MaTG).ToPagedList(pageNumber,pageSize);
            return View(tg);
        }
         
        public ActionResult XoaTG(int MaTG=0)
        {
            if(ModelState.IsValid)
            {
                VietSach vs = cn.VietSaches.FirstOrDefault(n => n.MaTG == MaTG);
                if(vs!=null)
                {
                    SetAlert("Tồn tại tác giả trong quản lý viết sách!", "warning");
                }
                else
                {
                    TacGia tg = cn.TacGias.SingleOrDefault(n => n.MaTG == MaTG);
                    cn.TacGias.Remove(tg);
                    cn.SaveChanges();
                    SetAlert("Xóa thành công", "success");
                }
                
            }
            return RedirectToAction("TacGia");

        }

        [HttpGet]
        public ActionResult ThemTG()
        {
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]

        public ActionResult ThemTG(TacGia tg)
        {
            if (ModelState.IsValid)
            {
                TacGia t = new TacGia();
                t.TenTG = tg.TenTG;
                t.DiaChi = tg.DiaChi;
                t.TieuSu = tg.TieuSu;
                t.SDT = null;
                cn.TacGias.Add(t);
                cn.SaveChanges();
                SetAlert("Thêm thành công", "success");
                
            }
            return RedirectToAction("TacGia");           
        }

        [HttpGet]
        public ActionResult SuaTG(int MaTG=0)
        {
            TacGia tg = cn.TacGias.SingleOrDefault(n => n.MaTG == MaTG);
            return View(tg);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SuaTG(TacGia tg)
        {
            if(ModelState.IsValid)
            {
                TacGia tg1 = cn.TacGias.SingleOrDefault(n => n.MaTG == tg.MaTG);
                if(tg1==null)
                {
                    Response.StatusCode = 404;
                    return null;
                }
                tg1.TenTG = tg.TenTG;
                tg1.DiaChi = tg.DiaChi;
                tg1.TieuSu = tg.TieuSu;
                cn.SaveChanges();
                SetAlert("Sửa thành công!", "success");
            }
            return RedirectToAction("TacGia");
        }
	//Nhà xuất bản
        public ActionResult NhaXuatBan(int? page)
        {
            int pageSize = 30;
            int pageNumber = (page ?? 1);
            var nxb = cn.NhaXuatBans.ToList().OrderBy(n => n.MaNXB).ToPagedList(pageNumber,pageSize);
            return View(nxb);
        }
        public ActionResult XoaNXB(int MaNXB=0)
        {
            if(ModelState.IsValid)
            {
                Sach sach = cn.Saches.FirstOrDefault(n => n.MaNXB == MaNXB);
                if(sach!=null)
                {
                    SetAlert("Tồn tại nhà xuất bản trong quản lý sách!", "warning");
                }
                else
                {
                    NhaXuatBan nxb = cn.NhaXuatBans.SingleOrDefault(n => n.MaNXB == MaNXB);
                    cn.NhaXuatBans.Remove(nxb);
                    cn.SaveChanges();
                    SetAlert("Xóa thành công!", "success");
                }              
            }
            return RedirectToAction("NhaXuatBan");
        }
        [HttpGet]
        public ActionResult ThemNXB()
        {
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ThemNXB(NhaXuatBan n)
        {
            if(ModelState.IsValid)
            {
                NhaXuatBan nn = new NhaXuatBan();
                nn.TenNXB = n.TenNXB;
                nn.DiaChi = n.DiaChi;
                nn.Mail = n.Mail;
                cn.NhaXuatBans.Add(nn);
                cn.SaveChanges();
                SetAlert("Thêm thành công!", "success");
            }
            return RedirectToAction("NhaXuatBan");
        }

        [HttpGet]
        public ActionResult SuaNXB(int MaNXB=0)
        {
            NhaXuatBan nxb = cn.NhaXuatBans.SingleOrDefault(n => n.MaNXB == MaNXB);
            return View(nxb);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SuaNXB(NhaXuatBan nb)
        {
            if(ModelState.IsValid)
            {
                NhaXuatBan n1 = cn.NhaXuatBans.SingleOrDefault(n => n.MaNXB == nb.MaNXB);
                if(n1==null)
                {
                    Response.StatusCode = 404;
                    return null;
                }
                n1.TenNXB = nb.TenNXB;
                n1.DiaChi = nb.DiaChi;
                n1.Mail = nb.Mail;
                cn.SaveChanges();
                SetAlert("Sửa thành công!", "success");
            }
            return RedirectToAction("NhaXuatBan");
        }
    }
}
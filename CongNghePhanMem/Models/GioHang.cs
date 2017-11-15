using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;   

namespace CongNghePhanMem.Models
{
    public class GioHang
    {
        public int iMaSach { get; set; }
        public string sTenSach { get; set; }
        public string sHinhAnh { get; set; }
        public int iSoLuong { get; set; }
        public double dDonGia { get; set; }
        public double dThanhTien {
            get { return iSoLuong * dDonGia; }
           
        }
        public double dTong
        {
            get { return dThanhTien; }
        }
       
        CongNghePhanMemEntities cn = new CongNghePhanMemEntities();
        //hàm tạo
        public GioHang(int MaSach)
        {
            iMaSach = MaSach;
            Sach sach = cn.Saches.Single(n => n.MaSach == iMaSach);
            sTenSach = sach.TenSach;
            sHinhAnh = sach.AnhBia;         
            if(sach.GiaKhuyenMai!=null)
            {
                dDonGia = double.Parse(sach.GiaKhuyenMai.ToString());
            }
            else
            {
                dDonGia = double.Parse(sach.GiaBan.ToString());
            }
            iSoLuong = 1;
            
            
        }

    }

}
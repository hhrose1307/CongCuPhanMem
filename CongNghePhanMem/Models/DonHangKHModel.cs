using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;

namespace CongNghePhanMem.Models
{
    public class DonHangKHModel
    {

        internal IQueryable<object> ThongKeDoanhThu(DateTime? froms, DateTime? tos)
        {
            CongNghePhanMemEntities db = new CongNghePhanMemEntities();
            var s = from p in db.DonDatHangs
                    where p.NgayDat >= froms && p.NgayDat <= tos
                    group p by EntityFunctions.TruncateTime(p.NgayDat) into gro
                    select new { ngaymua = gro.Key.Value, tongtien = gro.Sum(r =>r.Tongtien) };
            return s;
        }


        internal IQueryable<object> ThongKeTiTrong(DateTime? froms, DateTime? tos)
        {
            CongNghePhanMemEntities db = new CongNghePhanMemEntities();
            var s = from p in db.ChiTietDonHangs
                    where  p.DonDatHang.NgayDat >= froms && p.DonDatHang.NgayDat <= tos
                    group p by p.Sach.TenSach into gro
                    select new { TenSP = gro.Key, SL = gro.Sum(r => r.SoLuong) };
            return s;
        }
    }
}
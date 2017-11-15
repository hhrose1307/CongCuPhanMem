using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CongNghePhanMem.Models
{
    [MetadataTypeAttribute(typeof(DonDatHangMetadata))]
    public partial class DonDatHang
    {
        internal sealed class DonDatHangMetadata
        {
            [Display(Name = "Đơn hàng")]
            public int MaDH { get; set; }

            [Display(Name = "Thanh toán")]
            public Nullable<bool> DaThanhToan { get; set; }

            [Display(Name = "Giao hàng")]

            public Nullable<bool> TinhTrangGiaoHang { get; set; }

            [Display(Name = "Ngày đặt hàng")]
            [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
            [DataType(DataType.Date)]
            //[Required(ErrorMessage = "{0}Bạn chưa nhập ngày đặt!")]
            public Nullable<System.DateTime> NgayDat { get; set; }

            [Display(Name = "Ngày giao hàng")]
            [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
            [DataType(DataType.Date)]
            public Nullable<System.DateTime> NgayGiao { get; set; }

            [Display(Name = "Tổng tiền")]
            [DisplayFormat(DataFormatString = "{0:#,##0VND}")]
            public Nullable<decimal> Tongtien { get; set; }

            [Display(Name = "Khách hàng")]
            public Nullable<int> MaND { get; set; }

            [Display(Name = "Họ tên người nhận")]
            //[Required(ErrorMessage = "{0}Bạn chưa nhập tên!")]
            public string HoTen { get; set; }

            [Display(Name = "Địa chỉ nhận")]
            //[Required(ErrorMessage = "{0}Bạn chưa nhập địa chỉ!")]
            public string DiaChi { get; set; }

            [StringLength(11, ErrorMessage = "Vượt quá 11 số!")]
            [Display(Name = "Số điện thoại")]
            //[Required(ErrorMessage = "{0}Bạn chưa nhập số điện thoại!")]
            public string SDT { get; set; }

            [Display(Name = "Email")]
            //[Required(ErrorMessage = "{0}Bạn chưa nhập Email!")]
            public string Email { get; set; }
        }
    }
}
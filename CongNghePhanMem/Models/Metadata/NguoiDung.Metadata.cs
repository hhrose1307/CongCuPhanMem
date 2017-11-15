using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CongNghePhanMem.Models
{
    [MetadataTypeAttribute(typeof(NguoiDungMetadata))]
    public partial class NguoiDung
    {
        internal sealed class NguoiDungMetadata
        {
            [Display(Name = "Người dùng")]
            public int MaND { get; set; }

            [Display(Name = "Tên đăng nhập")]
            public string TenDangNhap { get; set; }

            [Display(Name = "Mật khẩu")]
            public string MatKhau { get; set; }

            [Display(Name = "Tên người dùng")]
            public string HoTen { get; set; }

            [Display(Name = "Email")]
            public string Email { get; set; }

            [Display(Name = "Số điện thoại")]
            public Nullable<int> SDT { get; set; }

            [Display(Name = "Ngày sinh")]
            [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
            [DataType(DataType.Date)]
            public Nullable<System.DateTime> NgaySinh { get; set; }

            [Display(Name = "Giới tính")]
            public string GioiTinh { get; set; }

            [Display(Name = "Địa chỉ")]
            public string DiaChi { get; set; }

            [Display(Name = "Ảnh đại diện")]
            public string AnhDaiDien { get; set; }

            [Display(Name = "Tình trạng tài khoản")]
            public Nullable<int> MaTT { get; set; }
        }
    }
}
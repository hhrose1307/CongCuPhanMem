using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CongNghePhanMem.Models
{
    [MetadataTypeAttribute(typeof(SachMetadata))]
    public partial class Sach
    {
        internal sealed class SachMetadata
        {
            [Display(Name = "Sách")]
            public int MaSach { get; set; }

            [Display(Name = "Ảnh bìa")]
            public string AnhBia { get; set; }

            [Display(Name = "Tên sách")]
            [Required(ErrorMessage = "{0}Bạn chưa nhập tên!")]
            public string TenSach { get; set; }

            [Display(Name = "Số lượt mua")]
            public Nullable<int> SoLuotXem { get; set; }

            [Display(Name = "Mô tả")]
            [Required(ErrorMessage = "{0}Bạn chưa nhập mô tả!")]
            public string MoTa { get; set; }

            [Display(Name = "Số lượng tồn")]
            [Required(ErrorMessage = "{0}Bạn chưa nhập số lượng!")]
            public Nullable<int> SLTon { get; set; }

            [Display(Name = "Ngày cập nhật")]
            [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
            [DataType(DataType.Date)]
            public Nullable<System.DateTime> NgayCapNhat { get; set; }

            [Display(Name = "Giá bán")]
            [DisplayFormat(DataFormatString = "{0:#,##0VND}")]
            [Required(ErrorMessage = "{0}Bạn chưa nhập giá!")]
            public Nullable<decimal> GiaBan { get; set; }

            [Display(Name = "Giá khuyến mãi")]
            [DisplayFormat(DataFormatString = "{0:#,##0VND}")]
            public string GiaKhuyenMai { get; set; }

            [Display(Name = "Chủ đề")]
            [Required(ErrorMessage = "{0}Bạn chưa nhập chủ đề!")]
            public Nullable<int> MaCD { get; set; }

            [Display(Name = "Loại sách")]
            [Required(ErrorMessage = "{0}Bạn chưa nhập loại sách")]
            public Nullable<int> MaLoai { get; set; }

            [Display(Name = "Nhà xuất bản")]
            [Required(ErrorMessage = "{0}Bạn chưa nhập nhà xuất bản!")]
            public Nullable<int> MaNXB { get; set; }

            [Display(Name = "Sách mới")]
            public Nullable<int> SachMoi { get; set; }
        }
    }
}
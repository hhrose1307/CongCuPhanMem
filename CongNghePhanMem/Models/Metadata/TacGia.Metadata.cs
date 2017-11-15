using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CongNghePhanMem.Models
{
    [MetadataTypeAttribute(typeof(TacGiaMetadata))]
    public partial class TacGia
    {
        internal sealed class TacGiaMetadata
        {
            [Display(Name = "Nhà cung cấp")]
            public int MaTG { get; set; }

            [Display(Name = "Tên tác giả")]
            [Required(ErrorMessage = "{0}Bạn chưa nhập tên!")]
            public string TenTG { get; set; }

            [Display(Name = "Địa chỉ")]
            [Required(ErrorMessage = "{0}Bạn chưa nhập địa chỉ!")]
            public string DiaChi { get; set; }

            [Display(Name = "Tiểu sử")]
            [Required(ErrorMessage = "{0}Bạn chưa nhập tiểu sử!")]
            public string TieuSu { get; set; }

            [Display(Name = "Số điện thoại")]

            public string SDT { get; set; }
        }
    }
}
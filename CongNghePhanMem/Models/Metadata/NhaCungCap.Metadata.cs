using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CongNghePhanMem.Models
{
    [MetadataTypeAttribute(typeof(NhaCungCapMetadata))]
    public partial class NhaCungCap
    {
        internal sealed class NhaCungCapMetadata
        {
            [Display(Name = "Nhà cung cấp")]
            public int MaNCC { get; set; }

            [Display(Name = "Tên nhà cung cấp")]
            [Required(ErrorMessage = "{0}Bạn chưa nhập tên!")]
            public string TenNCC { get; set; }

            [Display(Name = "Địa chỉ")]
            [Required(ErrorMessage = "{0}Bạn chưa nhập địa chỉ!")]
            public string DiaChi { get; set; }

            [StringLength(11, ErrorMessage = "Vượt quá 11 số!")]
            [Display(Name = "Số điện thoại")]
            [Required(ErrorMessage = "{0}Bạn chưa nhập Số điện thoại!")]
            public string SDT { get; set; }

            [Display(Name = "Email")]
            [Required(ErrorMessage = "{0}Bạn chưa nhập Email!")]
            public string Email { get; set; }
        }
    }
}
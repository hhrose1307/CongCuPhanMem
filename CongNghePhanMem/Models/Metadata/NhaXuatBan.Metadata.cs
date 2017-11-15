using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CongNghePhanMem.Models
{
    [MetadataTypeAttribute(typeof(NhaXuatBanMetadata))]
    public partial class NhaXuatBan
    {
        internal sealed class NhaXuatBanMetadata
        {
            [Display(Name = "Nhà xuất bản")]
            public int MaNXB { get; set; }

            [Display(Name = "Tên nhà cung cấp")]
            [Required(ErrorMessage = "{0}Bạn chưa nhập tên!")]
            public string TenNXB { get; set; }

            [Display(Name = "Địa chỉ")]
            [Required(ErrorMessage = "{0}Bạn chưa nhập địa chỉ!")]
            public string DiaChi { get; set; }

            [Display(Name = "Email")]
            [Required(ErrorMessage = "{0}Bạn chưa nhập Email!")]
            public string Mail { get; set; }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CongNghePhanMem.Models
{
    [MetadataTypeAttribute(typeof(VietSachMetadata))]
    public partial class VietSach
    {
        internal sealed class VietSachMetadata
        {
            [Display(Name = "Tác giả")]
            public int MaTG { get; set; }

            [Display(Name = "Sách")]
            public int MaSach { get; set; }

            [Display(Name = "Vai trò")]
            [Required(ErrorMessage = "{0}Bạn chưa nhập vai trò!")]
            public string VaiTro { get; set; }

            [Display(Name = "Vị trí")]
            [Required(ErrorMessage = "{0}Bạn chưa nhập vị trí!")]
            public string ViTri { get; set; }
        }
    }
}
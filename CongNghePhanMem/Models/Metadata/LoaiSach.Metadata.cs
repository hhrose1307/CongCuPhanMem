using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CongNghePhanMem.Models
{
    [MetadataTypeAttribute(typeof(LoaiSachMetadata))]
    public partial class LoaiSach
    {
        internal sealed class LoaiSachMetadata
        {
            [Display(Name = "Loại")]
            public int MaLoai { get; set; }

            [Display(Name = "Tên loại")]
            [Required(ErrorMessage = "{0}Bạn chưa nhập tên!")]
            public string TenLoai { get; set; }
        }
    }
}
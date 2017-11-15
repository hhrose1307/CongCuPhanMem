using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CongNghePhanMem.Models
{
    [MetadataTypeAttribute(typeof(TinhTrangNguoiDungMetadata))]
    public partial class TinhTrangNguoiDung
    {
        internal sealed class TinhTrangNguoiDungMetadata
        {
            [Display(Name = "Tình trạng")]
            public int MaTT { get; set; }

            [Display(Name = "Tên tình trạng")]
            [Required(ErrorMessage = "{0}Bạn chưa nhập tên")]
            public string TenTT { get; set; }
        }
    }
}
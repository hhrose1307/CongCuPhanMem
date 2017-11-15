using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CongNghePhanMem.Models
{
    [MetadataTypeAttribute(typeof(TinTucMetadata))]
    public partial class TinTuc
    {
        internal sealed class TinTucMetadata
        {
            [Display(Name = "Tình trạng!")]
            public int MaTT { get; set; }

            [Display(Name = "Tên tình trạng")]
            [Required(ErrorMessage = "{0}Bạn chưa nhập tên!")]
            public string TenTT { get; set; }

            [Display(Name = "Mô tả")]
            [Required(ErrorMessage = "{0}Bạn chưa nhập mô tả!")]
            public string MoTa { get; set; }
            [Display(Name = "Ảnh tượng trưng")]
            public string AnhBia { get; set; }
        }
    }
}
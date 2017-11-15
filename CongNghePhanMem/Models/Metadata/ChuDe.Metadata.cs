using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CongNghePhanMem.Models
{
    [MetadataTypeAttribute(typeof(ChuDeMetadata))]
    public partial class ChuDe
    {
        internal sealed class ChuDeMetadata
        {
            [Display(Name = "Chủ đề")]
            public int MaCD { get; set; }
            [Display(Name = "Tên chủ đề")]
            [Required(ErrorMessage = "{0}Bạn chưa nhập tên!")]
            public string TenChuDe { get; set; }
        }
    }
}
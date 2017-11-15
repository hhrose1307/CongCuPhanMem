using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CongNghePhanMem.Models
{
    [MetadataTypeAttribute(typeof(KhuyenMaiMetadata))]
    public partial class KhuyenMai
    {
        internal sealed class KhuyenMaiMetadata
        {
            [Display(Name = "Khuyến mãi")]
            public int MaKM { get; set; }

            [Display(Name = "Tên khuyến mãi")]
            [Required(ErrorMessage = "{0}Bạn chưa nhập tên!")]
            public string TenKM { get; set; }

            [Display(Name = "Ngày bắt đầu")]
            [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
            [DataType(DataType.Date)]
            public Nullable<System.DateTime> NgayBatDau { get; set; }

            [Display(Name = "Ngày kết thúc")]
            [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
            [DataType(DataType.Date)]
            public Nullable<System.DateTime> NgayKetThuc { get; set; }
            [Display(Name = "Nội dung")]
            [Required(ErrorMessage = "{0}Bạn chưa nhập nội dung!")]
            public string NoiDung { get; set; }

            [Display(Name = "Ảnh minh họa")]
            public string AnhKM { get; set; }
        }
    }
}
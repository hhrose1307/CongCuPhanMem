using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CongNghePhanMem.Models
{
    [MetadataTypeAttribute(typeof(PhanHoiMetadata))]
    public partial class PhanHoi
    {
        internal sealed class PhanHoiMetadata
        {
            [Display(Name = "Phản hồi")]
            public int MaPH { get; set; }

            [Display(Name = "Đọc giả")]
            public Nullable<int> MaND { get; set; }

            [Display(Name = "Nội dung")]
            [Required(ErrorMessage = "{0}Bạn chưa nhập nội dung!")]
            public string NoiDung { get; set; }

            [Display(Name = "Trả lời")]
            public string TraLoi { get; set; }
        }
    }
}
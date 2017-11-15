using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CongNghePhanMem.Models
{
    [MetadataTypeAttribute(typeof(GiaoDienMetadata))]
    public partial class GiaoDien
    {
        internal sealed class GiaoDienMetadata
        {
            [Display(Name = "Mã")]
            public int ID { get; set; }

            [Display(Name = "Thuộc tính")]
            //[Required(ErrorMessage = "{0}Bạn chưa nhập thuộc tính")]
            public string ThuocTinh { get; set; }

            [Display(Name = "Giá trị")]
            //[Required(ErrorMessage = "{0}Bạn chưa nhập giá trị!")]
            public string GiaTri { get; set; }

            [Display(Name = "Giá trị 1")]
            //[Required(ErrorMessage = "{0}Bạn chưa nhập giá trị 1!")]
            public string GiaTri1 { get; set; }

            [Display(Name = "Giá trị 2")]
            //[Required(ErrorMessage = "{0}Bạn chưa nhập giá trị!")]
            public string GiaTri2 { get; set; }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CongNghePhanMem.Models
{
    [MetadataTypeAttribute(typeof(ChiTietKhuyenMaiMetadata))]
    public partial class ChiTietKhuyenMai
    {
        internal sealed class ChiTietKhuyenMaiMetadata
        {
            [Display(Name = "Khuyến mãi")]
            public int MaKM { get; set; }

            [Display(Name = "Sách")]
            public int MaSach { get; set; }

            [Display(Name = "Giá khuyến mãi")]
            [DisplayFormat(DataFormatString = "{0:#,##0VND}")]
            [Required(ErrorMessage = "{0}Bạn chưa nhập giá!")]
            public Nullable<int> GiamGia { get; set; }
        }
    }
}
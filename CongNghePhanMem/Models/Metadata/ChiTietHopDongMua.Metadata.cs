using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CongNghePhanMem.Models
{
    [MetadataTypeAttribute(typeof(ChiTietHopDongMuaMetadata))]
    public partial class ChiTietHopDongMua
    {
        internal sealed class ChiTietHopDongMuaMetadata
        {
            public int STT { get; set; }

            [Display(Name = "Mã Hợp Đồng")]
            public int MaHD { get; set; }

            [Display(Name = "Số lượng")]
            [Required(ErrorMessage = "{0}Bạn chưa nhập số lượng!")]
            public Nullable<int> SoLuong { get; set; }


            [Display(Name = "Giá mua")]
            [DisplayFormat(DataFormatString = "{0:#,##0VND}")]
            [Required(ErrorMessage = "{0}Bạn chưa nhập giá!")]
            public Nullable<decimal> GiaMua { get; set; }

            [Display(Name = "Tên sách")]
            [Required(ErrorMessage = "{0}Bạn chưa nhập tên!")]
            public string TenSach { get; set; }
        }
    }
}
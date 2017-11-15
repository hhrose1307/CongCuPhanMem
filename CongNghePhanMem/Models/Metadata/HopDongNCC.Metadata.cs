using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CongNghePhanMem.Models
{
    [MetadataTypeAttribute(typeof(HopDongNCCMetadata))]
    public partial class HopDongNCC
    {
        internal sealed class HopDongNCCMetadata
        {
            [Display(Name = ("Hợp đồng"))]
            public int MaHD { get; set; }

            [Display(Name = "Tên hợp đồng")]
            [Required(ErrorMessage = "{0}Bạn chưa nhập tên!")]
            public string TenHD { get; set; }

            [Display(Name = "Nhà cung cấp")]
            [Required(ErrorMessage = "{0}Bạn chưa nhập nhà cung cấp!")]
            public Nullable<int> MaNCC { get; set; }

            [Display(Name = "Ngày ký")]
            [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
            [DataType(DataType.Date)]
            public Nullable<System.DateTime> NgayKi { get; set; }
            public string ThoiHan { get; set; }

            [Display(Name = "Ngày giao hàng")]
            [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
            [DataType(DataType.Date)]
            public Nullable<System.DateTime> NgayGiao { get; set; }

            [Display(Name = "Tình trạng giao hàng")]
            public Nullable<bool> TinhTrangGiaoHang { get; set; }

            [Display(Name = "Tổng tiền")]
            [DisplayFormat(DataFormatString = "{0:#,##0VND}")]
            [Required(ErrorMessage = "{0}Bạn chưa nhập giá!")]
            public Nullable<decimal> TongTien { get; set; }

            [Display(Name = "Thanh toán")]
            public Nullable<bool> DaThanhToan { get; set; }
        }
    }
}
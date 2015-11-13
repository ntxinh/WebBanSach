using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using 2 thư viện thiết kế class metadata
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Vinabook.Models
{
    [MetadataTypeAttribute(typeof(DonHangMetadata))]
    public partial class DonHang
    {
        internal sealed class DonHangMetadata
        {
            [Display(Name = "Mã đơn hàng")]
            public int MaDonHang { get; set; }

            [Display(Name = "Ngày giao")]
            [DataType(DataType.Date)]
            [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
            public DateTime NgayGiao { get; set; }

            [Display(Name = "Ngày đặt")]
            [DataType(DataType.Date)]
            [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
            //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
            public DateTime NgayDat { get; set; }

            [Display(Name = "Đã thanh toán")]
            public string DaThanhToan { get; set; }

            [Display(Name = "Tình trạng giao hàng")]
            public int TinhTrangGiaoHang { get; set; }

            [Display(Name = "Mã khách hàng")]
            public int MaKH { get; set; }
        }
    }
}
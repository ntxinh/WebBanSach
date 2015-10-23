using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using 2 thư viện thiết kế class metadata
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Vinabook.Models.Metadata
{
    [MetadataTypeAttribute(typeof(ChiTietDonHangMetadata))]
    public partial class ChiTietDonHang
    {
        internal sealed class ChiTietDonHangMetadata
        {
            [Display(Name = "Mã đơn hàng")]
            public int MaDonHang { get; set; }

            [Display(Name = "Mã sách")]
            public int MaSach { get; set; }

            [Display(Name = "Số lượng")]
            public Nullable<int> SoLuong { get; set; }

            [Display(Name = "Đơn giá")]
            public Nullable<decimal> DonGia { get; set; }
        }
    }
}
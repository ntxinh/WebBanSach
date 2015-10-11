using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using 2 thư viện thiết kế class metadata
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Vinabook.Models
{
    [MetadataTypeAttribute(typeof(NhaXuatBanMetadata))]
    public partial class NhaXuatBan
    {
        internal sealed class NhaXuatBanMetadata
        {
            [Display(Name = "Mã nhà xuất bản")]
            public int MaNXB { get; set; }

            [Display(Name = "Tên nhà xuất bản")]
            public string TenNXB { get; set; }

            [Display(Name = "Địa chỉ")]
            public string DiaChi { get; set; }

            [Display(Name = "Điện thoại")]
            public string DienThoai { get; set; }
        }
    }
}
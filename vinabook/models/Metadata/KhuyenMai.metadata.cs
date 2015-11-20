using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using 2 thư viện thiết kế class metadata
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Vinabook.Models
{
    [MetadataTypeAttribute(typeof(KhuyenMaiMetadata))]
    public partial class KhuyenMai
    {
        internal sealed class KhuyenMaiMetadata
        {
            [Display(Name = "Mã khuyến mãi")]
            public string MaKM { get; set; }

            [Display(Name = "Giá trị KM")]
            public string GiaTriKM { get; set; }

            [Display(Name = "Ngày bắt đầu KM")]
            [DataType(DataType.Date)]
            [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
            public DateTime NgayBDKM { get; set; }

            [Display(Name = "Ngày kết thúc KM")]
            [DataType(DataType.Date)]
            [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
            public DateTime NgayKTKM { get; set; }

            [Display(Name = "Đã sử dụng")]
            public bool DaSuDung { get; set; }

        }
    }
}
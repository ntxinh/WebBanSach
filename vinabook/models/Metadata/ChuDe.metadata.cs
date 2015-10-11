using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using 2 thư viện thiết kế class metadata
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Vinabook.Models
{
    [MetadataTypeAttribute(typeof(ChuDeMetadata))]
    public partial class ChuDe
    {
        internal sealed class ChuDeMetadata
        {
            [Display(Name = "Mã chủ đề")]
            public int MaChuDe { get; set; }

            [Display(Name = "Tên chủ đề")]
            public string TenChuDe { get; set; }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vinabook.Models;

namespace Vinabook.Models
{
    public class GioHang
    {
        Vinabook.Models.QuanLyBanSachEntities db = new Vinabook.Models.QuanLyBanSachEntities();
        public int iMaSach { get; set; }
        public string sTenSach { get; set; }
        public string sAnhBia { get; set; }
        public double dDongia { get; set; }
        public int iSoLuong { get; set; }
        public double dThanhTien
        {
            get { return iSoLuong * dDongia; }
        }
        public GioHang(int MaSach)
        {
            iMaSach = MaSach;
            Sach sach = db.Saches.Single(n => n.MaSach == iMaSach);
            sTenSach = sach.TenSach;
            sAnhBia = sach.AnhBia;
            dDongia = double.Parse(sach.GiaBan.ToString());
            iSoLuong = 1;
        }
    }
}
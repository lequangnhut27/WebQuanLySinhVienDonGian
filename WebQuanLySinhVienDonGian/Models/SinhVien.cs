using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebQuanLySinhVienDonGian.Models
{
    public class SinhVien
    {
        public int maSinhVien { get; set; }
        public string hoLot { get; set; }
        public string ten { get; set; }
        public string lop { get; set; }
        public bool nu { get; set; }
        public int khoa { get; set; }
        public DateTime ngaySinh { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLRapChieuPhim.Models
{
    public class Rating
    {
        public class RatingBase
        {
            public int Id { get; set; }
            public int PhimId { get; set; }
            public DateTime Ngay { get; set; }
            public int Diem { get; set; }
            public string Ip { get; set; }
        }
        public class Input
        {

        }
        public class Output
        {
            public class ThongTinRating : RatingBase
            {
                public Phim.PhimBase Phim { get; set; }

                public ThongTinRating()
                {
                    Phim = new Phim.PhimBase();
                }
            }
        }
    }
}

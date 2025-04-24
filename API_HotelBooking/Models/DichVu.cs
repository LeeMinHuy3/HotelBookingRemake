using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace API_HotelBooking.Models
{
    public class DichVu
    {
        [Key]
        public int MaDV { get; set; }

        [Required]
        public string KieuDichVu { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal Gia { get; set; }

        public string? ImageUrl { get; set; } // Thêm dòng này

        public ICollection<DatDichVu>? DatDichVus { get; set; }
    }
}

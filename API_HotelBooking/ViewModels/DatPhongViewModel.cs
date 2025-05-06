namespace API_HotelBooking.ViewModels
{
	public class DatPhongViewModel
	{
		public int MaDP {  get; set; }
		public int MaND { get; set; }
		public int MaP { get; set; }
		public string? TenPhong { get; set; }
		public int? MaKM { get; set; }
		public DateTime ThoiGianCheckIn { get; set; }
		public DateTime ThoiGianCheckOut { get; set; }
		public int SoNguoi { get; set; }
		public decimal TongTien { get; set; }
		public string TrangThai { get; set; }

	}
}

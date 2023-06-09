namespace Backend_Task03.Models
{
    public class Account
    {
        public int ID { get; set; }
        public string OpenIDIssuer { get; set; }
        public string OpenIDSubject { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public List<Review>? Reviews { get; set; }
		public List<Beer>? FavoriteBeers { get; set; }
	}
}

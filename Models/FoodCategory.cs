namespace Backend_Task03.Models
{
    public class FoodCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Review> Reviews { get; set; } = new List<Review>();
    }
}
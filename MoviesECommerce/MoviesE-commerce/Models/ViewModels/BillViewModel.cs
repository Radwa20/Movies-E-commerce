namespace MoviesE_commerce.Models.ViewModels
{
    public class BillViewModel
    {
        public List<Bill> Bills { get; set; }
        public string UserFirstName { get; set; }
        public int Subtotal { get; set; }
        public int Discount { get; set; }
        public int Total { get; set; }
    }
}
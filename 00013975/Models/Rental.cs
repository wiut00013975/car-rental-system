namespace CarRentalSystem.Models
{
    public class Rental
    {
        public int RentalID { get; set; }
        public int CarID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal TotalCost { get; set; }
        public string Status { get; set; } // For example, "Booked", "Active", "Completed"
        public Car Car { get; set; }
    }
}

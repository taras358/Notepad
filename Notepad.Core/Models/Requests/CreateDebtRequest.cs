namespace Notepad.Core.Models.Requests
{
    public class CreateDebtRequest
    {
        public string DebtorId { get; set; }
        public double Amount { get; set; }
        public string Description { get; set; }
    }
}

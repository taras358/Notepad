namespace Notepad.Core.Models.Requests
{
    public class DeleteDebtRequest
    {
        public string DebtorId { get; set; }
        public double Amount { get; set; }
    }
}

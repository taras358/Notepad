using System.Collections.Generic;

namespace Notepad.Core.Models.Responses
{
    public class DebtorResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public double TotalDebt { get; set; }
        public List<DebtResponse> Debts { get; set; }
    }
}

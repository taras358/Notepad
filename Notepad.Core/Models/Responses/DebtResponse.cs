using System;

namespace Notepad.Core.Models.Responses
{
    public class DebtResponse
    {
        public string Id { get; set; }
        public DateTimeOffset CreationDate { get; set; }        
        public double Amount { get; set; }
        public string Description{ get; set; }
    }
}

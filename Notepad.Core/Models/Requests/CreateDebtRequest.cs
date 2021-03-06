﻿using System.ComponentModel.DataAnnotations;

namespace Notepad.Core.Models.Requests
{
    public class CreateDebtRequest
    {
        [Required]
        public string DebtorId { get; set; }
        [Required]
        public double Amount { get; set; }
        [Required]
        public string Description { get; set; }
    }
}

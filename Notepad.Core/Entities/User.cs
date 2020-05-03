using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Notepad.Core.Entities
{
    public sealed class User: IdentityUser
    {

        [MaxLength(50)]
        [Column(Order = 1)]
        public string Name { get; set; }

        [MaxLength(50)]
        [Column(Order = 2)]
        public string Surname { get; set; }

        public DateTimeOffset CreationDate { get; }

        public DateTimeOffset UpdateDate { get; set; }

        [Range(0, double.MaxValue)]
        public double FixedTax { get; set; }

        [Range(0, 100)]
        public double PartialTax { get; set; }

        [Range(0, 100)]
        public double Saving { get; set; }

        public User()
        {
            Id = Guid.NewGuid().ToString();
            CreationDate = DateTimeOffset.UtcNow;
            UpdateDate = DateTimeOffset.UtcNow;
        }
    }
}
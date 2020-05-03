using System;
using System.ComponentModel.DataAnnotations;

namespace Notepad.Core.Entities
{
    public class BaseEntity
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public DateTimeOffset CreationDate { get; set; } = DateTimeOffset.UtcNow;

    }
}

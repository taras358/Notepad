using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Notepad.Core.Entities
{
    public class Debtor: BaseEntity
    {
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(50)]
        public string Surname { get; set; }

        [MaxLength(100)]
        public string FullName { get; set; }

        public virtual ICollection<Debt> Debts { get; set; }
    }
}

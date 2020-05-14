using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Notepad.Core.Entities
{
    public class Debt : BaseEntity
    {
        public double Amount { get; set; }

        [MaxLength(250)]
        public string Description{ get; set; }

        [ForeignKey(nameof(Entities.Debtor))]
        public string DebtorId { get; set; }
        public bool IsRepaid { get; set; }

        public virtual Debtor Debtor { get; set; }
    }
}

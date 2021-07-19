using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InAndOut.Models
{
    public class Expense
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("Expense")]
        [Required]
        public string ExpenseName { get; set; }
        [Required]
        [Range(1,int.MaxValue, ErrorMessage = "Amount must be greater than 0!")]
        public int Amount { get; set; }
        
        // Erstellt einen FK in die Datenbank und stellt eine Beziehung her
        [DisplayName("ExpenseType")]
        public int ExpenseTypeId { get; set; }
        [ForeignKey("ExpenseTypeId")]
        public virtual ExpenseType ExpenseType { get; set; }
        
    }
}
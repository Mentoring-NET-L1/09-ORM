using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Northwind.Dal.EntityFramework.Models
{
    public class Card
    {
        public int CardID { get; set; }

        [Required]
        [StringLength(16)]
        public string Number { get; set; }

        public DateTime ExpirationDate { get; set; }

        [Required]
        [StringLength(50)]
        public string CardHolder { get; set; }

        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int EmployeeID { get; set; }

        public Employee Employee { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.DTOs.Model
{
    public class TransactionModel
    {
        [Key]
        public int TransactionID { get; set; }

        [Required]
        public int ProductID { get; set; }
        [Required]
        public int UserID { get; set; }

        [Required (ErrorMessage = "The Type of transaction is required")]
        [StringLength(20)]
        public string Type { get; set; }=string.Empty;

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be a positive integer")]
        public int Quantity { get; set; }

        [Required]
        public DateTime TransactionDate { get; set; }

        public ProductModel? Product { get; set; }

        public UserModel? User { get; set; }

    }
}

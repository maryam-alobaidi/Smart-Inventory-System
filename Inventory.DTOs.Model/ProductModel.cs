using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.DTOs.Model
{
    public class ProductModel
    {
        [Key]
        public int ProductID { get; set; }
        [Required(ErrorMessage ="The name of product is required")]
        [StringLength(100)]
        public string ProductName { get; set; }= string.Empty;


        [Required]
        [Range(0,int.MaxValue,ErrorMessage ="Quantity must be a non-negative integer")]
        public int Quantity { get; set; }


        [Required]
        [Range(0.01,double.MaxValue,ErrorMessage ="Price must be a positive decimal number")]
        public decimal Price { get; set; }

        [Required]
        public int MinStockLevel { get; set; }
        public int CategoryID { get; set; }
        
        public CategoryModel? Category { get; set; }
    }
}

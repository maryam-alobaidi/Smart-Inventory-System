using Inventory.DataAccess;
using Inventory.DTOs.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.BusinessLogic
{
    public class clsProducts
    {
        public enum enMode { addNew = 0, update = 1 }
        public enMode Mode = enMode.addNew;



        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public int MinStockLevel { get; set; }
        public int CategoryID { get; set; }
        public clsProducts()
        {
            this.ProductID = -1;
            this.ProductName = "";
            this.Quantity = -1;
            this.Price = 0.0m;
            this.MinStockLevel = -1;
            this.CategoryID = -1;
            this.Mode = enMode.addNew;
        }

        private clsProducts(int ProductID, string ProductName, int Quantity, decimal Price, int MinStockLevel, int CategoryID)
        {
            this.ProductID = ProductID;
            this.ProductName = ProductName;
            this.Quantity = Quantity;
            this.Price = Price;
            this.MinStockLevel = MinStockLevel;
            this.CategoryID = CategoryID;

            this.Mode = enMode.update;
        }

        private async Task<bool> _AddNewProducts()
        {
            // Call DataAccess Layer
            this.ProductID = (int)await clsProductsData.AddNewProducts(this.ProductName, this.Quantity, this.Price, this.MinStockLevel, this.CategoryID);
            return (this.ProductID != -1);
        }

        public static Task<bool> Delete(int ProductID)
        {
            // Call DataAccess Layer
            return clsProductsData.DeleteProducts(ProductID);
        }

        public async static Task<ProductModel?> Find(int ProductID)
        {
            ProductModel? productModel =await clsProductsData.FindById(ProductID);
            return productModel;
        }

        //public static clsProducts FindByName(string ProductName)
        //{
        //    // Call DataAccess Layer
        //    int ProductID = -1;
        //    int Quantity = -1;
        //    decimal Price = 0.0m;
        //    int MinStockLevel = -1;
        //    int CategoryID = -1;

        //    bool IsFound = clsProductsData.FindByName(ref ProductID, ProductName, ref Quantity, ref Price, ref MinStockLevel, ref CategoryID);
        //    if (IsFound)
        //        return new clsProducts(ProductID, ProductName, Quantity, Price, MinStockLevel, CategoryID);
        //    else
        //        return null;
        //}

        public static async Task<List<ProductModel>> GetAllProducts()
        {
            return await clsProductsData.GetAllProducts();
        }

        public async Task<bool> Save()
        {
            switch (Mode)
            {
                case enMode.addNew:
                    Mode = enMode.update;
                    return await _AddNewProducts();
                case enMode.update:
                    return await _UpdateProducts();
            }
            return false;
        }

        private async Task<bool> _UpdateProducts()
        {
            // Call DataAccess Layer
            return await clsProductsData.UpdateProducts(this.ProductID, this.ProductName, this.Quantity, this.Price, this.MinStockLevel, this.CategoryID) ?? false;
        }

    }

}

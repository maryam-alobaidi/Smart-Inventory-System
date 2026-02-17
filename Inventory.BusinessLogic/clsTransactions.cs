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
    public class clsTransactions
    {
        public enum enMode { addNew = 0, update = 1 }
        public enMode Mode = enMode.addNew;



        public int TransactionID { get; set; }
        public int ProductID { get; set; }
        public int UserID { get; set; }
        public string Type { get; set; }
        public int Quantity { get; set; }
        public DateTime TransactionDate { get; set; }
        public clsTransactions()
        {
            this.TransactionID = -1;
            this.ProductID = -1;
            this.UserID = -1;
            this.Type = "";
            this.Quantity = -1;
            this.TransactionDate = DateTime.MinValue;
            this.Mode = enMode.addNew;
        }

        private clsTransactions(int TransactionID, int ProductID, int UserID, string Type, int Quantity, DateTime TransactionDate)
        {
            this.TransactionID = TransactionID;
            this.ProductID = ProductID;
            this.UserID = UserID;
            this.Type = Type;
            this.Quantity = Quantity;
            this.TransactionDate = TransactionDate;

            this.Mode = enMode.update;
        }

        private async Task<bool> _AddNewTransactions()
        {
            // Call DataAccess Layer
            this.TransactionID = (int)await clsTransactionsData.AddNewTransactions(this.ProductID, this.UserID, this.Type, this.Quantity, this.TransactionDate);
            return (this.TransactionID != -1);
        }

        public static Task<bool> Delete(int TransactionID)
        {
            // Call DataAccess Layer
            return clsTransactionsData.DeleteTransactions(TransactionID);
        }

        public async static Task<TransactionModel?> Find(int TransactionID)
        {


            TransactionModel? transactionModel =await clsTransactionsData.FindByID(TransactionID);
           
            return transactionModel;
        }

        //public static clsTransactions FindByName(int ProductID)
        //{
        //    // Call DataAccess Layer
        //    int TransactionID = -1;
        //    int UserID = -1;
        //    string Type = "";
        //    int Quantity = -1;
        //    DateTime TransactionDate = DateTime.MinValue;

        //    bool IsFound = clsTransactionsData.FindByName(ref TransactionID, ProductID, ref UserID, ref Type, ref Quantity, ref TransactionDate);
        //    if (IsFound)
        //        return new clsTransactions(TransactionID, ProductID, UserID, Type, Quantity, TransactionDate);
        //    else
        //        return null;
        //}

        public static async Task<List<TransactionModel>> GetAllTransactions()
        {
            return await clsTransactionsData.GetAllTransactions();
        }

        public async Task<bool> Save()
        {
            switch (Mode)
            {
                case enMode.addNew:
                    Mode = enMode.update;
                    return await _AddNewTransactions();
                case enMode.update:
                    return await _UpdateTransactions();
            }
            return false;
        }

        private async Task<bool> _UpdateTransactions()
        {
            // Call DataAccess Layer
            return await clsTransactionsData.UpdateTransactions(this.TransactionID, this.ProductID, this.UserID, this.Type, this.Quantity, this.TransactionDate) ?? false;
        }

    }

}

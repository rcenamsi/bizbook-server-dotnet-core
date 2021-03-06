using Model.Model;
using Model.Model.Sales;
using Model.Model.Transactions;

namespace ViewModel.Sales
{
    public class SaleStateViewModel : BaseViewModel<SaleState>
    {
        public SaleStateViewModel(SaleState x) : base(x)
        {
            Remarks = x.Remarks;
            SaleId = x.SaleId;
            ShopId = x.ShopId;
            State = x.State;
        }

        public string ShopId { get; set; }

        public string SaleId { get; set; }

        public string State { get; set; }

        public string Remarks { get; set; }
    }

    public class SaleDetailViewModel : BaseViewModel<SaleDetail>
    {
        public SaleDetailViewModel(SaleDetail x) : base(x)
        {
            Quantity = x.Quantity;
            CostPricePerUnit = x.CostPricePerUnit;
            CostTotal = x.CostTotal;
            SalePricePerUnit = x.SalePricePerUnit;
            DiscountTotal = x.DiscountTotal;
            Total = x.Total;
            PaidAmount = x.PaidAmount;
            DueAmount = x.DueAmount;
            ProductSerialNumber = x.ProductSerialNumber;
            SaleId = x.SaleId;
            ProductDetailId = x.ProductDetailId;
            ProductSerialNumber = x.ProductSerialNumber;
            IsReturned = x.IsReturned;
            ReturnReason = x.ReturnReason;
            Remarks = x.Remarks;
            if (x.Sale != null)
            {
                SaleOrderNo = x.Sale.OrderNumber;
            }

            if (x.ProductDetail != null)
            {
                ProductDetailName = x.ProductDetail.Name;
                ProductId = x.ProductDetail.ProductCategoryId;
            }

            ShopId = x.ShopId;
        }

        public double PaidAmount { get; set; }

        public double DueAmount { get; set; }

        public string ReturnReason { get; set; }

        public bool IsReturned { get; set; }

        public string ShopId { get; set; }

        public string ProductId { get; set; }

        public double Quantity { get; set; }
        public double CostPricePerUnit { get; set; }
        public double CostTotal { get; set; }
        public double SalePricePerUnit { get; set; }
        public double SalePriceTotal { get; set; }
        public double DiscountTotal { get; set; } = 0;
        public double Total { get; set; }
        public string ProductSerialNumber { get; set; }
        public string SaleId { get; set; }
        public string SaleOrderNo { get; set; }
        public string ProductDetailId { get; set; }
        public string ProductDetailName { get; set; }
        public string Remarks { get; set; }
    }

    public class SaleTransactionViewModel : BaseViewModel<Transaction>
    {
        public SaleTransactionViewModel(Transaction x) : base(x)
        {
            TransactionNumber = x.TransactionNumber;
            Amount = x.Amount;
            Created = x.Created;
            ContactPersonName = x.ContactPersonName;
        }

        public double Amount { get; set; }
        public string ContactPersonName { get; set; }
        public string TransactionNumber { get; set; }

    }
}
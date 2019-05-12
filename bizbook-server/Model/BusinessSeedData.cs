﻿
using System.Linq;
using Model.Constants;
using Model.Model;
using Model.Shops;

namespace Model
{
    using System;
    using System.Collections.Generic;

    public class BusinessSeedData
    {
        public static void AddBrand(string shopId, BizBookInventoryContext context, string name = "Code Coopers")
        {
            Brand b = GetDefaults<Brand>(shopId);
            b.Name = name;
            b.Phone = shopId;

            if (!context.Brands.Any(x => x.Id == shopId))
            {
                context.Brands.Add(b);
                context.SaveChanges();
            }
        }

        public static void AddSupplier(string shopId, BizBookInventoryContext context, string name = "Code Coopers")
        {
            Supplier supplier = GetDefaults<Supplier>(shopId);
            supplier.Name = name;
            supplier.Phone = shopId;

            if (!context.Suppliers.Any(x => x.Id == shopId))
            {
                context.Suppliers.Add(supplier);
                context.SaveChanges();
            }
        }

        public static List<string> GetAccountHeads()
        {
            List<string> names = new List<string>()
            {
                "Purchase",
                "Sale",
                "House Rent",
                "Salary",
                "Utility",
                "Conveyance",
                "Other"
            };
            return names;
        }

        public static Shop GetSysShop()
        {
            Shop sysShop = new Shop()
            {
                Id = "00000000-0000-0000-0000-000000000000",
                Name = "System",
                Phone = "system",
                Created = DateTime.UtcNow,
                Modified = DateTime.UtcNow,
                CreatedBy = "foyzulkarim@gmail.com",
                ModifiedBy = "foyzulkarim@gmail.com",
                CreatedFrom = "Browser",
                RegistrationDate = new DateTime(2017, 1, 1),
                ExpiryDate = new DateTime(2025, 1, 1),
                IsVerified = true,
                TotalUsers = 1,
                District = "Dhaka",
                Website = "http://www.bizbook365.com"
            };
            return sysShop;
        }

        //public static void AddDemo1Shop(BusinessDbContext context, string shop1Id, string name)
        //{
        //    if (ShopNotExists(context, new Shop() { Id = shop1Id, Name = name }))
        //    {
        //        AddShop(context, shop1Id, name);
        //    }
        //}

        public static void AddSysShop(BizBookInventoryContext context)
        {
            Shop sysShop = GetSysShop();
            var shopNotExists = ShopNotExists(context, sysShop);
            if (shopNotExists)
            {
                context.Shops.Add(sysShop);
                context.SaveChanges();
            }
        }

        private static bool ShopNotExists(BizBookInventoryContext context, Shop shop)
        {
            bool notExists = !context.Shops.Any(x => x.Name == shop.Name);
            return notExists;
        }

        public static void AddShop(BizBookInventoryContext context, string shop1Id, string name)
        {
            Shop demo1 = new Shop()
            {
                Id = shop1Id,
                Name = name,
                Phone = name,
                Created = DateTime.Now,
                Modified = DateTime.Now,
                CreatedBy = "foyzulkarim@gmail.com",
                ModifiedBy = "foyzulkarim@gmail.com",
                CreatedFrom = "Browser",
                RegistrationDate = new DateTime(2016, 1, 1),
                ExpiryDate = new DateTime(2020, 1, 1),
                IsVerified = true,
                IsActive = true,
                TotalUsers = 1,
                District = "Dhaka",
                Website = "http://www.bizbook365.com",
            };

            if (ShopNotExists(context, demo1))
            {
                context.Shops.Add(demo1);
                context.SaveChanges();
            }
        }

        public static void AddAccountHeads(BizBookInventoryContext context, string shopId)
        {
            var heads = GetAccountHeads();
            foreach (string head in heads)
            {
                if (context.AccountHeads.Any(x => x.Name.ToUpper().Contains(head) && x.ShopId == shopId))
                {
                    continue;
                }

                AccountHead accountHead = GetDefaults<AccountHead>(shopId);
                accountHead.Id = Guid.NewGuid().ToString();
                accountHead.Name = head;
                context.AccountHeads.Add(accountHead);
                context.SaveChanges();
            }
        }

        public static void AddAccountInfo(BizBookInventoryContext context, string shopId)
        {
            bool exists = context.AccountInfoes.Any(x => x.AccountTitle == "Cash" && x.ShopId == shopId);
            if (!exists)
            {
                AccountInfo info = GetDefaults<AccountInfo>(shopId);
                info.Id = Guid.NewGuid().ToString();
                info.AccountTitle = "Cash";
                info.AccountInfoType = AccountInfoType.Cash;
                info.AccountNumber = "Cash";

                context.AccountInfoes.Add(info);
                context.SaveChanges();
            }
        }

        public static void AddSupplier(BizBookInventoryContext context, string shopId)
        {
            Supplier supplier = GetDefaults<Supplier>(shopId);
            supplier.Name = "Food Valley";
            supplier.Phone = "ph123";
            var any = context.Suppliers.Any(x => x.ShopId == shopId && x.Name == supplier.Name);
            if (!any)
            {
                context.Suppliers.Add(supplier);
                context.SaveChanges();
            }
        }

        public static void AddProducts(BizBookInventoryContext context, string shopId)
        {
            ProductGroup group = GetDefaults<ProductGroup>(shopId);
            group.Name = "Food";
            bool any = context.ProductGroups.Any(x => x.ShopId == shopId && x.Name == @group.Name);
            if (!any)
            {
                context.ProductGroups.Add(group);
                context.SaveChanges();
            }

            ProductCategory category = GetDefaults<ProductCategory>(shopId);
            category.ProductGroupId = group.Id;
            category.Name = "Snacks";

            any = context.ProductCategories.Any(x => x.ShopId == shopId && x.Name == category.Name);
            if (!any)
            {
                context.ProductCategories.Add(category);
                context.SaveChanges();
            }

            ProductDetail detail = GetDefaults<ProductDetail>(shopId);
            detail.ProductCategoryId = category.Id;
            detail.Name = "Samosa";
            detail.BarCode = Guid.NewGuid().ToString();
            detail.BrandId = shopId;
            detail.CostPrice = 20.00;
            detail.SalePrice = 50;
            // new added
            detail.ProductCode = Guid.NewGuid().ToString();


            any = context.ProductDetails.Any(x => x.ShopId == shopId && x.Name == detail.Name);
            if (!any)
            {
                context.ProductDetails.Add(detail);
                context.SaveChanges();
            }
        }

        private static T GetDefaults<T>(string shopId) where T : ShopChild, new()
        {
            T defaults = new T()
            {
                Id = shopId,
                Created = DateTime.UtcNow,
                Modified = DateTime.UtcNow,
                CreatedBy = "foyzulkarim@gmail.com",
                ModifiedBy = "foyzulkarim@gmail.com",
                CreatedFrom = "Browser",
                ShopId = shopId,
            };

            return defaults;
        }

        public static void AddCustomer(BizBookInventoryContext context, string shopId)
        {
            Customer customer = GetDefaults<Customer>(shopId);
            customer.Phone = "0";
            customer.Name = "Anon";
            customer.MembershipCardNo = shopId;
            bool any = context.Customers.Any(x => x.ShopId == shopId && x.Phone == customer.Phone);
            if (!any)
            {
                context.Customers.Add(customer);
                context.SaveChanges();
            }
        }

        public static void AddWarehouses(BizBookInventoryContext context)
        {
            var shopIds = context.Shops.Select(x => x.Id).ToList();
            foreach (var shopId in shopIds)
            {
                Warehouse warehouse = context.Warehouses.FirstOrDefault(x => x.ShopId == shopId && x.Name == "Default");
                if (warehouse == null)
                {
                    warehouse = new Warehouse()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Created = DateTime.Now,
                        Modified = DateTime.Now,
                        ShopId = shopId,
                        CreatedBy = "System",
                        ModifiedBy = "System",
                        CreatedFrom = "System",
                        Name = "Default",
                        IsActive = true,
                        Area = "Default",
                        District = "",
                        IsMain = true,
                    };
                    context.Warehouses.Add(warehouse);
                    context.SaveChanges();
                }
            }
        }

        public static void UpdateWarehouseRelatedData(BizBookInventoryContext context)
        {
            List<string> ids = context.Shops.Where(x => x.IsActive).Select(x => x.Id).ToList();
            foreach (var shopId in ids)
            {
                var whId = context.Warehouses.First(x => x.ShopId == shopId).Id;
                var purchases = context.Purchases.Where(x => x.ShopId == shopId && x.WarehouseId == null).ToList();
                foreach (var item in purchases)
                {
                    item.WarehouseId = whId;
                }

                context.SaveChanges();
                var purchaseDetails = context.PurchaseDetails.Where(x => x.ShopId == shopId && x.WarehouseId == null)
                    .ToList();
                foreach (var item in purchaseDetails)
                {
                    item.WarehouseId = whId;
                }

                context.SaveChanges();

                var sales = context.Sales.Where(x => x.ShopId == shopId && x.WarehouseId == null).ToList();
                foreach (var item in sales)
                {
                    item.WarehouseId = whId;
                }

                context.SaveChanges();

                var saleDetails = context.SaleDetails.Where(x => x.ShopId == shopId && x.WarehouseId == null).ToList();
                foreach (var item in saleDetails)
                {
                    item.WarehouseId = whId;
                }

                var productDetails = context.ProductDetails.Where(x => x.ShopId == shopId && x.IsActive).ToList();
                foreach (var item in productDetails)
                {
                    WarehouseProduct whProduct = context.WarehouseProducts.FirstOrDefault(
                        x => x.ShopId == shopId && x.ProductDetailId == item.Id && x.WarehouseId == whId);
                    if (whProduct != null)
                    {
                        continue;
                    }

                    whProduct = new WarehouseProduct()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Created = DateTime.Now,
                        Modified = DateTime.Now,
                        ShopId = shopId,
                        CreatedBy = "System",
                        ModifiedBy = "System",
                        CreatedFrom = "System",
                        IsActive = true,
                        ProductDetailId = item.Id,
                        StartingInventory = item.StartingInventory,
                        Purchased = item.Purchased,
                        Sold = item.Sold,
                        OnHand = item.OnHand,
                        MinimumStockToNotify =
                            item.MinimumStockToNotify,
                        TransferredIn = 0,
                        TransferredOut = 0,
                        WarehouseId = whId
                    };

                    context.WarehouseProducts.Add(whProduct);
                    context.SaveChanges();
                }
            }
        }
    }
}
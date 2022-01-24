using System;
using System.Configuration;
using System.Data;
using System.IO;
using System.Net;
using System.Text;
using log4net;
using IntergrationHaravan.Model;
using System.Collections.Generic;

namespace IntergrationHaravan.Business
{
    public class IvendRetailService
    {
        static ILog log = LogManager.GetLogger(typeof(IvendRetailService));
        static string useApi = ConfigurationManager.AppSettings["iVend_User"];
        static string pwdApi = ConfigurationManager.AppSettings["iVend_Password"];

        /// <summary>
        /// Lấy danh sách sản phẩm mới tạo hoặc cập nhật từ iVend
        /// </summary>
        public static List<Product> GetProductFromiVend()
        {
            try
            {
                #region Lấy/Cập nhật thời gian quét dữ liệu
                string timeRequest = "";
                bool isFirst = true;
                string contentUpdate = "";
                timeRequest = Utils.GetContentFile(out contentUpdate, out isFirst);
                Utils.UpdateContentFile(contentUpdate);
                #endregion

                #region Gọi API lấy danh sách sản phẩm
                string query = "";
                if (isFirst)
                    query = string.Format(@"SELECT * FROM InvProduct");
                else
                    query = string.Format(@"SELECT * FROM InvProduct where Modified >= '{0}'", timeRequest);

                List<Product> product = new List<Product>();
                DataSet dataSets = new DataSet();
                string urls = string.Concat("http://localhost/iVendAPI/iVendAPI.svc/WebAPI/GetQueryResult/?queryText=", System.Web.HttpUtility.UrlEncode(query));
                HttpWebRequest requests = (HttpWebRequest)WebRequest.Create(urls);
                requests.Method = "GET";
                requests.ContentType = "application/xml";
                requests.Headers.Add("username", useApi);
                requests.Headers.Add("password", pwdApi);
                using (WebResponse response = (HttpWebResponse)requests.GetResponse())
                {
                    using (StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                    {
                        dataSets.ReadXml(sr);
                    }
                }
                if (dataSets != null)
                {
                    DataTable data = dataSets.Tables[0];
                    for (int i = 0; i < data.Rows.Count; i++)
                    {
                        Product pd = new Product();
                        pd.Id = data.Rows[i]["Id"].ToString();
                        pd.Description = data.Rows[i]["Description"].ToString();
                        pd.Description2 = data.Rows[i]["Description2"].ToString();
                        pd.BasePrice = !string.IsNullOrWhiteSpace(data.Rows[i]["BasePrice"].ToString()) ? Convert.ToDecimal(data.Rows[i]["BasePrice"].ToString()) : 0;
                        pd.UPC = data.Rows[i]["UPC"].ToString();
                        product.Add(pd);
                    }
                }
                return product;
                #endregion
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Lấy danh sách nhóm sản phẩm mới tạo hoặc cập nhật từ iVend
        /// </summary>
        public static List<ProductGroup> GetProductGroupFromiVend()
        {
            try
            {
                #region Lấy/Cập nhật thời gian quét dữ liệu
                string timeRequest = "";
                bool isFirst = true;
                string contentUpdate = "";
                timeRequest = Utils.GetContentFile(out contentUpdate, out isFirst);
                Utils.UpdateContentFile(contentUpdate);
                #endregion

                #region Gọi API lấy danh sách sản phẩm
                string query = "";
                if (isFirst)
                    query = string.Format(@"SELECT * from InvProductGroup");
                else
                    query = string.Format(@"SELECT * from InvProductGroup where Modified >= '{0}'", timeRequest);

                List<ProductGroup> productgroup = new List<ProductGroup>();
                DataSet dataSets = new DataSet();
                string urls = string.Concat("http://localhost/iVendAPI/iVendAPI.svc/WebAPI/GetQueryResult/?queryText=", System.Web.HttpUtility.UrlEncode(query));
                HttpWebRequest requests = (HttpWebRequest)WebRequest.Create(urls);
                requests.Method = "GET";
                requests.ContentType = "application/xml";
                requests.Headers.Add("username", useApi);
                requests.Headers.Add("password", pwdApi);
                using (WebResponse response = (HttpWebResponse)requests.GetResponse())
                {
                    using (StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                    {
                        dataSets.ReadXml(sr);
                    }
                }
                if (dataSets != null)
                {
                    DataTable data = dataSets.Tables[0];
                    for (int i = 0; i < data.Rows.Count; i++)
                    {
                        ProductGroup pdg = new ProductGroup();
                        pdg.Id = data.Rows[i]["Id"].ToString();
                        pdg.Description = data.Rows[i]["DescriptionProductGroup"].ToString();
                        productgroup.Add(pdg);
                    }
                }
                return productgroup;
                #endregion
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Lấy danh sách danh mục sản phẩm mới tạo hoặc cập nhật từ iVend
        /// </summary>
        public static List<ProductCategory> GetProductCategoryFromiVend()
        {
            try
            {
                #region Lấy/Cập nhật thời gian quét dữ liệu
                string timeRequest = "";
                bool isFirst = true;
                string contentUpdate = "";
                timeRequest = Utils.GetContentFile(out contentUpdate, out isFirst);
                Utils.UpdateContentFile(contentUpdate);
                #endregion

                #region Gọi API lấy danh sách sản phẩm
                string query = "";
                if (isFirst)
                    query = string.Format(@"SELECT * from InvProductCategory");
                else
                    query = string.Format(@"SELECT * from InvProductCategory where Modified >= '{0}'", timeRequest);

                List<ProductCategory> productcategory = new List<ProductCategory>();
                DataSet dataSets = new DataSet();
                string urls = string.Concat("http://localhost/iVendAPI/iVendAPI.svc/WebAPI/GetQueryResult/?queryText=", System.Web.HttpUtility.UrlEncode(query));
                HttpWebRequest requests = (HttpWebRequest)WebRequest.Create(urls);
                requests.Method = "GET";
                requests.ContentType = "application/xml";
                requests.Headers.Add("username", useApi);
                requests.Headers.Add("password", pwdApi);
                using (WebResponse response = (HttpWebResponse)requests.GetResponse())
                {
                    using (StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                    {
                        dataSets.ReadXml(sr);
                    }
                }
                if (dataSets != null)
                {
                    DataTable data = dataSets.Tables[0];
                    for (int i = 0; i < data.Rows.Count; i++)
                    {
                        ProductCategory pdc = new ProductCategory();
                        pdc.Id = data.Rows[i]["Id"].ToString();
                        pdc.Description = data.Rows[i]["DescriptionProductCategory"].ToString();
                        productcategory.Add(pdc);
                    }
                }
                return productcategory;
                #endregion
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Lấy danh sách khách hàng mới tạo hoặc cập nhật từ iVend
        /// </summary>
        public static List<Customer> GetCustomerFromiVend()
        {
            try
            {
                #region Lấy/Cập nhật thời gian quét dữ liệu
                string timeRequest = "";
                bool isFirst = true;
                string contentUpdate = "";
                timeRequest = Utils.GetContentFile(out contentUpdate, out isFirst);
                Utils.UpdateContentFile(contentUpdate);
                #endregion

                #region Gọi API lấy danh sách sản phẩm
                string query = "";
                if (isFirst)
                    query = string.Format(@"SELECT * FROM CusCustomer");
                else
                    query = string.Format(@"SELECT * FROM CusCustomer where Modified >= '{0}'", timeRequest);

                List<Customer> customer = new List<Customer>();
                DataSet dataSets = new DataSet();
                string urls = string.Concat("http://localhost/iVendAPI/iVendAPI.svc/WebAPI/GetQueryResult/?queryText=", System.Web.HttpUtility.UrlEncode(query));
                HttpWebRequest requests = (HttpWebRequest)WebRequest.Create(urls);
                requests.Method = "GET";
                requests.ContentType = "application/xml";
                requests.Headers.Add("username", useApi);
                requests.Headers.Add("password", pwdApi);
                using (WebResponse response = (HttpWebResponse)requests.GetResponse())
                {
                    using (StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                    {
                        dataSets.ReadXml(sr);
                    }
                }
                if (dataSets != null)
                {
                    DataTable data = dataSets.Tables[0];
                    for (int i = 0; i < data.Rows.Count; i++)
                    {
                        Customer cus = new Customer();
                        cus.Id = data.Rows[i]["Id"].ToString();
                        cus.FirstName = data.Rows[i]["FirstName"].ToString();
                        cus.MiddleName = data.Rows[i]["MiddleName"].ToString();
                        cus.LastName = data.Rows[i]["LastName"].ToString();
                        cus.PhoneNumber = data.Rows[i]["PhoneNumber"].ToString();
                        cus.BillingAddressKey = data.Rows[i]["BillingAddressKey"].ToString();
                        cus.ShippingAddressKey = data.Rows[i]["ShippingAddressKey"].ToString();
                        cus.Email = data.Rows[i]["Email"].ToString();
                        cus.LoyalityId = data.Rows[i]["LoyalityId"].ToString();
                        cus.BirthDate = Convert.ToDateTime(data.Rows[i]["BirthDate"].ToString());
                        customer.Add(cus);
                    }
                }
                return customer;
                #endregion
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Lấy danh sách nhóm khách hàng mới tạo hoặc cập nhật từ iVend
        /// </summary>
        public static List<CustomerGroup> GetCustomerGroupFromiVend()
        {
            try
            {
                #region Lấy/Cập nhật thời gian quét dữ liệu
                string timeRequest = "";
                bool isFirst = true;
                string contentUpdate = "";
                timeRequest = Utils.GetContentFile(out contentUpdate, out isFirst);
                Utils.UpdateContentFile(contentUpdate);
                #endregion

                #region Gọi API lấy danh sách sản phẩm
                string query = "";
                if (isFirst)
                    query = string.Format(@"SELECT * from CusCustomerGroup");
                else
                    query = string.Format(@"SELECT * from CusCustomerGroup where Modified >= '{0}'", timeRequest);

                List<CustomerGroup> customergroup = new List<CustomerGroup>();
                DataSet dataSets = new DataSet();
                string urls = string.Concat("http://localhost/iVendAPI/iVendAPI.svc/WebAPI/GetQueryResult/?queryText=", System.Web.HttpUtility.UrlEncode(query));
                HttpWebRequest requests = (HttpWebRequest)WebRequest.Create(urls);
                requests.Method = "GET";
                requests.ContentType = "application/xml";
                requests.Headers.Add("username", useApi);
                requests.Headers.Add("password", pwdApi);
                using (WebResponse response = (HttpWebResponse)requests.GetResponse())
                {
                    using (StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                    {
                        dataSets.ReadXml(sr);
                    }
                }
                if (dataSets != null)
                {
                    DataTable data = dataSets.Tables[0];
                    for (int i = 0; i < data.Rows.Count; i++)
                    {
                        CustomerGroup cusg = new CustomerGroup();
                        cusg.Id = data.Rows[i]["Id"].ToString();
                        cusg.Description = data.Rows[i]["DescriptionCustomerGroup"].ToString();
                        customergroup.Add(cusg);
                    }
                }
                return customergroup;
                #endregion
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Lấy danh sách nhà sản xuất mới tạo hoặc cập nhật từ iVend
        /// </summary>
        public static List<Manufacturer> GetManufacturerFromiVend()
        {
            try
            {
                #region Lấy/Cập nhật thời gian quét dữ liệu
                string timeRequest = "";
                bool isFirst = true;
                string contentUpdate = "";
                timeRequest = Utils.GetContentFile(out contentUpdate, out isFirst);
                Utils.UpdateContentFile(contentUpdate);
                #endregion

                #region Gọi API lấy danh sách sản phẩm
                string query = "";
                if (isFirst)
                    query = string.Format(@"SELECT * from InvManufacturer");
                else
                    query = string.Format(@"SELECT * from InvManufacturer where Modified >= '{0}'", timeRequest);

                List<Manufacturer> manufacurer = new List<Manufacturer>();
                DataSet dataSets = new DataSet();
                string urls = string.Concat("http://localhost/iVendAPI/iVendAPI.svc/WebAPI/GetQueryResult/?queryText=", System.Web.HttpUtility.UrlEncode(query));
                HttpWebRequest requests = (HttpWebRequest)WebRequest.Create(urls);
                requests.Method = "GET";
                requests.ContentType = "application/xml";
                requests.Headers.Add("username", useApi);
                requests.Headers.Add("password", pwdApi);
                using (WebResponse response = (HttpWebResponse)requests.GetResponse())
                {
                    using (StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                    {
                        dataSets.ReadXml(sr);
                    }
                }
                if (dataSets != null)
                {
                    DataTable data = dataSets.Tables[0];
                    for (int i = 0; i < data.Rows.Count; i++)
                    {
                        Manufacturer mf = new Manufacturer();
                        mf.Id = data.Rows[i]["Id"].ToString();
                        mf.Description = data.Rows[i]["DescriptionManufacturer"].ToString();
                        manufacurer.Add(mf);
                    }
                }
                return manufacurer;
                #endregion
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Lấy dữ liệu tồn kho sản phẩm từ iVend Retail
        /// </summary>
        public static List<InventoryItem> GetInventoryFromiVend()
        {
            try
            {
                #region Lấy/Cập nhật thời gian quét dữ liệu
                string timeRequest = "";
                bool isFirst = true;
                string contentUpdate = "";
                timeRequest = Utils.GetContentFile(out contentUpdate, out isFirst);
                Utils.UpdateContentFile(contentUpdate);
                #endregion

                #region Gọi API lấy danh sách sản phẩm
                string query = "";
                if (isFirst)
                    query = string.Format(@"SELECT InvProduct.Id AS ProductId, InvProduct.Description AS ProductName, InvProduct.BasePrice, 
                                                    InvWarehouse.Description as  Warehouse,  InvInventoryItem.InStockQuantity AS Quantity
                                                    FROM InvInventoryItem
                                                    inner join InvProduct ON InvInventoryItem.ProductKey = InvProduct.ProductKey
                                                    inner join InvWarehouse ON InvInventoryItem.WarehouseKey = InvWarehouse.WarehouseKey");
                else
                    query = string.Format(@"SELECT InvProduct.Id AS ProductId, InvProduct.Description AS ProductName, InvProduct.BasePrice, 
                                                    InvWarehouse.Description as  Warehouse,  InvInventoryItem.InStockQuantity AS Quantity
                                                    FROM InvInventoryItem
                                                    inner join InvProduct ON InvInventoryItem.ProductKey = InvProduct.ProductKey
                                                    inner join InvWarehouse ON InvInventoryItem.WarehouseKey = InvWarehouse.WarehouseKey
                                                    where InvInventoryItem.Modified >= '{0}'", timeRequest);

                List<InventoryItem> inventory = new List<InventoryItem>();
                DataSet dataSets = new DataSet();
                string urls = string.Concat("http://localhost/iVendAPI/iVendAPI.svc/WebAPI/GetQueryResult/?queryText=", System.Web.HttpUtility.UrlEncode(query));
                HttpWebRequest requests = (HttpWebRequest)WebRequest.Create(urls);
                requests.Method = "GET";
                requests.ContentType = "application/xml";
                requests.Headers.Add("username", useApi);
                requests.Headers.Add("password", pwdApi);
                using (WebResponse response = (HttpWebResponse)requests.GetResponse())
                {
                    using (StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                    {
                        dataSets.ReadXml(sr);
                    }
                }
                if (dataSets != null)
                {
                    DataTable data = dataSets.Tables[0];
                    for (int i = 0; i < data.Rows.Count; i++)
                    {
                        InventoryItem inv = new InventoryItem();
                        inv.ProductId = data.Rows[i]["ProductId"].ToString();
                        inv.ProductName = data.Rows[i]["ProductName"].ToString();
                        decimal BasePrice;
                        decimal.TryParse(data.Rows[i]["BasePrice"].ToString(), out BasePrice);
                        inv.BasePrice = BasePrice;
                        inv.Warehouse = data.Rows[i]["Warehouse"].ToString();
                        int Quantity;
                        int.TryParse(data.Rows[i]["Quantity"].ToString(), out Quantity);
                        inv.Quantity = Quantity;
                        inventory.Add(inv);
                    }
                }
                return inventory;
                #endregion
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Lấy dữ liệu cửa hàng từ iVend Retail
        /// </summary>
        public static List<Store> GetStoreFromiVend()
        {
            try
            {
                #region Lấy/Cập nhật thời gian quét dữ liệu
                string timeRequest = "";
                bool isFirst = true;
                string contentUpdate = "";
                timeRequest = Utils.GetContentFile(out contentUpdate, out isFirst);
                Utils.UpdateContentFile(contentUpdate);
                #endregion

                #region Gọi API lấy danh sách sản phẩm
                string query = "";
                if (isFirst)
                    query = string.Format(@"SELECT RtlStore.Id AS StoreId, RtlStore.Description AS StoreName, CfgAddress.Address1, CfgAddress.Address2,
                                                    RtlStore.Email, CfgAddress.PhoneNumber,
                                                    TaxTaxCode.Rate AS PrurchaseTaxCode, T1.Rate AS SaleTaxCode, PrcPriceList.Id AS PriceList
                                                    from RtlStore
                                                    inner join CfgAddress on RtlStore.AddressKey = CfgAddress.AddressKey
                                                    inner join TaxTaxCode on RtlStore.PurchaseTaxCodeKey = TaxTaxCode.TaxCodeKey
                                                    inner join TaxTaxCode T1 ON RtlStore.SalesTaxCodeKey = T1.TaxCodeKey
                                                    inner join PrcPriceList on RtlStore.PriceListKey = PrcPriceList.PriceListKey");
                else
                    query = string.Format(@"SELECT RtlStore.Id AS StoreId, RtlStore.Description AS StoreName, CfgAddress.Address1, CfgAddress.Address2,
                                                    RtlStore.Email, CfgAddress.PhoneNumber,
                                                    TaxTaxCode.Rate AS PrurchaseTaxCode, T1.Rate AS SaleTaxCode, PrcPriceList.Id AS PriceList
                                                    from RtlStore
                                                    inner join CfgAddress on RtlStore.AddressKey = CfgAddress.AddressKey
                                                    inner join TaxTaxCode on RtlStore.PurchaseTaxCodeKey = TaxTaxCode.TaxCodeKey
                                                    inner join TaxTaxCode T1 ON RtlStore.SalesTaxCodeKey = T1.TaxCodeKey
                                                    inner join PrcPriceList on RtlStore.PriceListKey = PrcPriceList.PriceListKey
                                                    where RtlStore.Modified >= '{0}'", timeRequest);

                List<Store> store = new List<Store>();
                DataSet dataSets = new DataSet();
                string urls = string.Concat("http://localhost/iVendAPI/iVendAPI.svc/WebAPI/GetQueryResult/?queryText=", System.Web.HttpUtility.UrlEncode(query));
                HttpWebRequest requests = (HttpWebRequest)WebRequest.Create(urls);
                requests.Method = "GET";
                requests.ContentType = "application/xml";
                requests.Headers.Add("username", useApi);
                requests.Headers.Add("password", pwdApi);
                using (WebResponse response = (HttpWebResponse)requests.GetResponse())
                {
                    using (StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                    {
                        dataSets.ReadXml(sr);
                    }
                }
                if (dataSets != null)
                {
                    DataTable data = dataSets.Tables[0];
                    for (int i = 0; i < data.Rows.Count; i++)
                    {
                        Store st = new Store();
                        st.StoreId = data.Rows[i]["StoreId"].ToString();
                        st.StoreName = data.Rows[i]["StoreName"].ToString();
                        st.Address1 = data.Rows[i]["Address1"].ToString();
                        st.Address2 = data.Rows[i]["Address2"].ToString();
                        st.Email = data.Rows[i]["Email"].ToString();
                        st.PhoneNumber = data.Rows[i]["PhoneNumber"].ToString();
                        decimal PrurchaseTaxCode;
                        decimal.TryParse(data.Rows[i]["PrurchaseTaxCode"].ToString(), out PrurchaseTaxCode);
                        decimal SaleTaxCode;
                        decimal.TryParse(data.Rows[i]["SaleTaxCode"].ToString(), out SaleTaxCode);
                        st.PriceList = data.Rows[i]["PriceList"].ToString();
                        store.Add(st);
                    }
                }
                return store;
                #endregion
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                return null;
            }
        }
    }
}

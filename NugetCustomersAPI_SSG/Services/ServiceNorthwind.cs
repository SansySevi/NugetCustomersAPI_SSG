using Newtonsoft.Json;
using NugetCustomersAPI_SSG.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NugetCustomersAPI_SSG.Services
{
    public class ServiceNorthwind
    {
        public async Task<CustomerList> GetCustomersListAsync()
        {
            WebClient client = new WebClient();
            client.Headers["content-type"] = "application/json";
            string url = "https://services.odata.org/V4/Northwind/Northwind.svc/Customers";
            string dataJson = await client.DownloadStringTaskAsync(url);
            CustomerList customers = JsonConvert.DeserializeObject<CustomerList>(dataJson);
            return customers;
        }

        public async Task<Customer> FindCustomerAsync(string id)
        {
            CustomerList customerList =
                await this.GetCustomersListAsync();

            Customer customer =
                customerList.Customers.FirstOrDefault(x => x.IdCustomer == id);
            return customer;
        }

        public async Task<OrderList> GetOrdersListAsync()
        {
            WebClient client = new WebClient();
            client.Headers["content-type"] = "application/json";
            string url = "https://services.odata.org/V4/Northwind/Northwind.svc/Orders";
            string dataJson = await client.DownloadStringTaskAsync(url);
            OrderList orders = JsonConvert.DeserializeObject<OrderList>(dataJson);
            return orders;
        }

        public async Task<List<Order>> GetOrdersByCustomer(string id)
        {
            OrderList ordersList =
                await this.GetOrdersListAsync();

            List<Order> ordersCustomer =
                ordersList.Orders.Where(x=>x.IdCustomer == id).ToList();
            return ordersCustomer;
        }


    }
}

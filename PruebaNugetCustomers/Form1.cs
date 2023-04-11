using NugetCustomersAPI_SSG.Models;
using NugetCustomersAPI_SSG.Services;

namespace PruebaNugetCustomers
{
    public partial class Form1 : Form
    {
        ServiceNorthwind service;
        List<string> ListIdsCustomers;


        public Form1()
        {
            InitializeComponent();
            this.service = new ServiceNorthwind();
            this.ListIdsCustomers = new List<string>();

        }

        private async void btnCargar_Click(object sender, EventArgs e)
        {
            CustomerList customersList = await
                this.service.GetCustomersListAsync();
            foreach (Customer c in customersList.Customers)
            {
                this.lstClientes.Items.Add(c.Contact);
                this.ListIdsCustomers.Add(c.IdCustomer);
            }

        }

        private async void lstClientes_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = this.lstClientes.SelectedIndex;
            string idCustomer = this.ListIdsCustomers[index];
            Customer customer =
                await this.service.FindCustomerAsync(idCustomer);
            this.txtIdCliente.Text = idCustomer;
            this.txtCompany.Text = customer.Company;
            this.txtAddress.Text = customer.Address;
            this.txtCity.Text = customer.City;

            this.lstOrders.Items.Clear();

            List<Order> orders = await this.service.GetOrdersByCustomer(idCustomer);
            foreach (Order order in orders)
            {
                this.lstOrders.Items.Add(order.ShipName + " | " + order.ShipDate);
            }
        }

    }
}
using Autofac;
using FoodOrderManagement.DAL.Models.Entities;
using FoodOrderManagement.DAL.Repositories.Interfaces;
using FoodOrderManagement.UI;
using FoodOrderManagement.UI.Forms.CustomerManagement.UserControlsOfCustomer;
using FoodOrderManagement.UI.Forms.MenuManagement;
using FoodOrderManagement.UI.Forms.OrderManagement.UserControlOfOrder;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Formats.Asn1.AsnWriter;
namespace FoodOrderManagement.AdminControl
{

    public partial class FormCustomer : Form
    {
        private readonly ILifetimeScope _scope;
        private readonly ICustomersRepository _customersRepository;
        UC_AddCustomer _ucAddCustomer;
        OverlayBackground _overlayBackground;
        public FormCustomer(ILifetimeScope scope, ICustomersRepository customersRepository)
        {
            InitializeComponent();
            _scope = scope;
            _customersRepository = customersRepository;
            _overlayBackground = new OverlayBackground(); 
            LoadCustomerList();
        }

        //Sự kiện nút thêm khách hàng được nhấn:
        private void AddCustomerButton_Click(object sender, EventArgs e)
        {
            _overlayBackground.Show(this);
            _ucAddCustomer = _scope.Resolve<UC_AddCustomer>();
            _ucAddCustomer.OnCustomerAdded += (s, newCustomerData) =>
            {
                UC_CustomerItem newItem = new UC_CustomerItem();
                newItem.SetCustomerData(newCustomerData);
                newItem.OnEditClicked += HandleEditCustomer;
                newItem.OnDeleteClicked += HandleDeleteCustomer;
                FlowLayoutCustomer.Controls.Add(newItem);
                FlowLayoutCustomer.Controls.SetChildIndex(newItem, 0);
                HandleClosePopup(_ucAddCustomer);
            };
            _ucAddCustomer.Disposed += (s, args) =>
            {
                _overlayBackground.Hide(this);
            };
            this.Controls.Add(_ucAddCustomer);
            _ucAddCustomer.Location = new Point(
                (this.Width - _ucAddCustomer.Width) / 2,
                (this.Height - _ucAddCustomer.Height) / 2);

            _ucAddCustomer.BringToFront();
        }

        //Sự kiện khi thanh tìm kiếm có thay đổi
        private void SearchCustomer_TextChanged(object sender, EventArgs e)
        {
            string keyword = SearchCustomer.Text.Trim().ToLower();

            if (string.IsNullOrEmpty(keyword))
            {
                RenderCustomerList(_originalCustomerList);
            }
            else
            {
                var filteredList = _originalCustomerList
                    .Where(c => c.FullName.ToLower().Contains(keyword) ||
                                c.PhoneNumber.Contains(keyword))
                    .ToList();
                RenderCustomerList(filteredList);
            }
        }
    }
}

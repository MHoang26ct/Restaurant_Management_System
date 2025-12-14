using Autofac;
using FoodOrderManagement.DAL.Models.Entities;
using FoodOrderManagement.DAL.Repositories.Implementations;
using FoodOrderManagement.DAL.Repositories.Interfaces;
using FoodOrderManagement.UI;
using FoodOrderManagement.UI.Forms.OrderManagement.UserControlOfOrder;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FoodOrderManagement.AdminControl
{
    public partial class FormOrder : Form
    {
        private readonly ILifetimeScope _scope;
        private readonly IOrdersRepository _ordersRepository;
        private readonly IOrderDetailsRepository _orderDetailsRepository;
        UC_CreateOrder _ucCreateOrder;
        UC_ViewDetails _ucViewDetails;
        public UC_OrderItem _uc_OrderItem;
        OverlayBackground _overlayBackground;
        public FormOrder(ILifetimeScope scope, IOrdersRepository ordersRepository, IOrderDetailsRepository orderDetailsRepository)
        {
            InitializeComponent();
            _scope = scope;
            _ordersRepository = ordersRepository;
            SetupFilterControls();
            LoadAllOrders();
            _orderDetailsRepository = orderDetailsRepository;
            _overlayBackground = new OverlayBackground();
        }
        private void CreateOrderButton_Click(object sender, EventArgs e)
        {
            _overlayBackground.Show(this);
            _ucCreateOrder = _scope.Resolve<UC_CreateOrder>();
            _ucCreateOrder.OnOrderCreated += (s, newOrder) =>
            {
                LoadAllOrders();
                HandleClosePopup(_ucCreateOrder);
            };
            _ucCreateOrder.Disposed += (s, args) =>
            {
                _overlayBackground.Hide(this);
            };
            this.Controls.Add(_ucCreateOrder);
            _ucCreateOrder.Location = new Point(
                 (this.Width - _ucCreateOrder.Width) / 2,
                 (this.Height - _ucCreateOrder.Height) / 2
            );
            _ucCreateOrder.BringToFront();
        }
        private void HandleOrderCreated(object sender, Orders orderData)
        {
            UC_OrderItem orderItem = _scope.Resolve<UC_OrderItem>();
            orderItem.SetOrderData(orderData); 
            orderItem.OnViewDetailsClicked += HandleViewDetailsClicked;

            FlowLayoutOrder.Controls.Add(orderItem);

            Control ctrl = sender as Control;

            if (ctrl != null)
            {
                this.Controls.Remove(ctrl);   
                ctrl.Dispose(); 
            }
        }
        private async void HandleViewDetailsClicked(object sender, Orders orderData)
        {
            _overlayBackground.Show(this);

            _ucViewDetails = new UC_ViewDetails();
            var listMonAn = await _orderDetailsRepository.GetDetailsByOrderIdAsync(orderData.Id);
            _ucViewDetails.LoadDetailData(orderData, listMonAn);

            this.Controls.Add(_ucViewDetails);
            _ucViewDetails.Location = new Point(
                 (this.Width - _ucViewDetails.Width) / 2,
                 (this.Height - _ucViewDetails.Height) / 2
            );
            _ucViewDetails.BringToFront();
            _ucViewDetails.Disposed += (s, e) =>
            {
                _overlayBackground.Hide(this);
            };
        }
        private async void HandleAddFoodClicked(object sender, Orders orderData)
        {
            foreach (Control ctrl in this.Controls.OfType<UC_CreateOrder>().ToList())
            {
                this.Controls.Remove(ctrl);
                ctrl.Dispose();
            }
            _overlayBackground.Show(this); 

            var ucAddMore = _scope.Resolve<UC_CreateOrder>();

            ucAddMore.SetModeAddFood(orderData);
            ucAddMore.OnOrderCreated += (s, updatedOrder) =>
            {
                LoadAllOrders(); 
                HandleClosePopup(ucAddMore);
            };
            ucAddMore.Disposed += (s, e) =>
            {
                _overlayBackground.Hide(this); 
            };
            this.Controls.Add(ucAddMore);
            ucAddMore.BringToFront();

            ucAddMore.Location = new Point(
                (this.Width - ucAddMore.Width) / 2,
                (this.Height - ucAddMore.Height) / 2
            );

        }

        private void HandleClosePopup(Control popup)
        {
            this.Controls.Remove(popup);
            popup.Dispose();
            _overlayBackground.Hide(this);
        }

        private void SearchOrderTBox1_Enter(object sender, EventArgs e)
        {
            if (SearchOrderTBox1.PlaceholderText == "Số bàn...")
            {
                SearchOrderTBox1.Text = "";
            }
        }

        private void SearchOrderTBox1_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(SearchOrderTBox1.Text))
            {
                SearchOrderTBox1.PlaceholderText = "Số bàn...";
            }
        }

        private void SearchOrderTBox1_TextChanged(object sender, EventArgs e)
        {
            ApplyFilters();
        }

        private void StatusCBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyFilters();
        }
    }
}

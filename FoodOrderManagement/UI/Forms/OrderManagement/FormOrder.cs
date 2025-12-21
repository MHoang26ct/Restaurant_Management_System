using Autofac;
using FoodOrderManagement.DAL.Models.Entities;
using FoodOrderManagement.DAL.Repositories.Implementations;
using FoodOrderManagement.DAL.Repositories.Interfaces;
using FoodOrderManagement.UI;
using FoodOrderManagement.UI.Forms.MenuManagement;
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
        private readonly IReservationsRepository _reservationsRepository;
        UC_CreateOrder _ucCreateOrder;
        UC_ViewDetails _ucViewDetails;
        public UC_OrderItem _uc_OrderItem;
        OverlayBackground _overlayBackground;
        public FormOrder(ILifetimeScope scope, IOrdersRepository ordersRepository, IOrderDetailsRepository orderDetailsRepository, IReservationsRepository reservationsRepository)
        {
            InitializeComponent();
            _scope = scope;
            _ordersRepository = ordersRepository;
            SetupFilterControls();
            LoadAllOrders();
            _orderDetailsRepository = orderDetailsRepository;
            _overlayBackground = new OverlayBackground();
            _reservationsRepository = reservationsRepository;
        }

        //Sự kiện nhấn nút đơn mới
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
            Helper.BoGoc(_ucCreateOrder, 20, true, true , true, true);
            _ucCreateOrder.Location = new Point(
                 (this.Width - _ucCreateOrder.Width) / 2,
                 (this.Height - _ucCreateOrder.Height) / 2
            );
            _ucCreateOrder.BringToFront();
        }

        //Xử lý sự kiện nhấn nút xem chi tiết
        private async void HandleViewDetailsClicked(object sender, Orders orderData)
        {
            _overlayBackground.Show(this);
            FlowLayoutOrder.Enabled = false;
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
                FlowLayoutOrder.Enabled = true;
                _overlayBackground.Hide(this);               
            };
        }

        //Xử lý sự kiện thêm món mới
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
            Helper.BoGoc(ucAddMore, 20, true, true, true, true);
            ucAddMore.BringToFront();

            ucAddMore.Location = new Point(
                (this.Width - ucAddMore.Width) / 2,
                (this.Height - ucAddMore.Height) / 2
            );

        }
        
        //Xử lý sự kiện đóng Pop up
        private void HandleClosePopup(Control popup)
        {
            this.Controls.Remove(popup);
            popup.Dispose();
            _overlayBackground.Hide(this);
        }

        //Xử lý sự kiện chọn hay rời thanh tìm kiếm
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

        //Xử lý sự kiện lọc, tìm kiếm đơn
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

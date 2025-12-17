using Autofac;
using FoodOrderManagement.DAL.Models.Entities;
using FoodOrderManagement.DAL.Repositories.Interfaces;
using FoodOrderManagement.UI;
using FoodOrderManagement.UI.Forms.MenuManagement;
using FoodOrderManagement.UI.Forms.OrderManagement.UserControlOfOrder;
using FoodOrderManagement.UI.Forms.ReservationManagement;
using FoodOrderManagement.UI.Forms.ReservationManagement.UserControlOfReservation;
using static System.Formats.Asn1.AsnWriter;
namespace FoodOrderManagement.UI.Forms.ReservationManagement.UserControlOfReservation
{
    public partial class UC_CreateReservation : UserControl
    {
        public event Action<string, string, Reservations, List<orderDetail>> OnCreateClicked;
        public event EventHandler OnExitClicked;

        private readonly ILifetimeScope _scope;

        public UC_CreateReservation(ILifetimeScope scope)
        {
            InitializeComponent();
            _scope = scope;
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            LoadTimeSlots();
        }

        private void LoadTimeSlots()
        {
            TimeReservationCBox.Items.Clear();
            for (int i = 9; i <= 21; i++) { TimeReservationCBox.Items.Add($"{i}:00"); TimeReservationCBox.Items.Add($"{i}:30"); }
        }

        // Nút + Thêm món
        private void btnAddFood_Click(object sender, EventArgs e)
        {
            if (ListFoodFlowLayout == null) { MessageBox.Show("Thiếu FlowLayoutPanel 'pnlFoodList' trên giao diện!"); return; }

            var row = _scope.Resolve<UC_AddFoodOrder>();
            row.Width = ListFoodFlowLayout.Width - 25;
            ListFoodFlowLayout.Controls.Add(row);
        }

        private void CreateOrderButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(CustomerNameTBox.Text) || string.IsNullOrEmpty(PhoneNumberTBox.Text))
            {
                MessageBox.Show("Vui lòng nhập Tên và SĐT!"); return;
            }
            if (TimeReservationCBox.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn khung giờ!"); return;
            }
            string name = CustomerNameTBox.Text;
            string phone = PhoneNumberTBox.Text;
            DateTime fullDate = DateReservation.Value.Date + TimeSpan.Parse(TimeReservationCBox.SelectedItem.ToString());

            Reservations res = new Reservations
            {
                TableId = (int)TableID_NBox.Value,
                NumberOfGuests = (int)NumberOfGuest.Value,
                ReservationTime = fullDate,
                ComingTime = fullDate,
                Status = "Pending",
                customerId = 0
            };
            List<orderDetail> foodList = new List<orderDetail>();

            // Duyệt qua các dòng chọn món trong panel
            if (ListFoodFlowLayout != null)
            {
                foreach (Control c in ListFoodFlowLayout.Controls)
                {
                    if (c is UC_AddFoodOrder row && row.SelectedFoodId > 0 && row.Quantity > 0)
                    {
                        foodList.Add(new orderDetail
                        {
                            FoodId = row.SelectedFoodId,
                            Quantity = row.Quantity
                        });
                    }
                }
            }
            OnCreateClicked?.Invoke(name, phone, res, foodList);
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            OnExitClicked?.Invoke(this, EventArgs.Empty);
            this.Dispose();
        }

        private void PhoneNumberTBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
    }
}

namespace FoodOrderManagement.AdminControl
{
    public partial class FormReservation : Form
    {
        private readonly ILifetimeScope _scope;
        private readonly IReservationsRepository _reservationsRepository;
        private readonly ICustomersRepository _customersRepository;
        private readonly IOrdersRepository _ordersRepository;
        private readonly IOrderDetailsRepository _orderDetailsRepository;
        private OverlayBackground _overlayBackground;
        private UC_CreateReservation uc_CreateReservation;
        private List<ReservationViewModel> _originalList = new List<ReservationViewModel>();
        public FormReservation(ILifetimeScope scope,
                               IReservationsRepository reservationsRepository,
                               ICustomersRepository customersRepository,
                               IOrdersRepository ordersRepository,
                               IOrderDetailsRepository orderDetailsRepository)
        {
            InitializeComponent();
            _scope = scope;
            _reservationsRepository = reservationsRepository;
            _customersRepository = customersRepository;
            _ordersRepository = ordersRepository;
            _orderDetailsRepository = orderDetailsRepository;
            _overlayBackground = new OverlayBackground();
            SearchReservationTBox1.TextChanged += (s, e) => ApplyFilter();
            DateTimePickerSearch.ValueChanged += (s, e) => ApplyFilter();
            LoadReservationList();
        }
        public async void LoadReservationList()
        {
            try
            {
                var list = await _reservationsRepository.GetAllReservationsAsync();
                _originalList = await _reservationsRepository.GetAllReservationsAsync();
                dgvReservations.DataSource = null;
                dgvReservations.DataSource = list;
                ApplyFilter();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách: " + ex.Message);
            }
        }
        private void ApplyFilter()
        {
            if (_originalList == null || _originalList.Count == 0) return;
            string keyword = SearchReservationTBox1.Text.Trim().ToLower();
            DateTime selectedDate = DateTimePickerSearch.Value.Date;
            var filteredList = _originalList.Where(r =>
            {
                bool matchDate = r.ReservationTime.Date == selectedDate;
                bool matchKeyword = true; 
                if (!string.IsNullOrEmpty(keyword) && keyword != "tìm kiếm...") 
                {
                    matchKeyword = r.PhoneNumber.Contains(keyword) ||       
                                   r.TableId.ToString().Contains(keyword) || 
                                   r.Id.ToString().Contains(keyword) ||     
                                   r.CustomerName.ToLower().Contains(keyword); 
                }
                return matchDate && matchKeyword;

            }).ToList();
            RenderGrid(filteredList);
        }

        private void RenderGrid(List<ReservationViewModel> list)
        {
            dgvReservations.DataSource = null;
            dgvReservations.DataSource = list;

            if (dgvReservations.Columns["Id"] != null) dgvReservations.Columns["Id"].HeaderText = "Mã Đơn";
            if (dgvReservations.Columns["CustomerName"] != null) dgvReservations.Columns["CustomerName"].HeaderText = "Khách Hàng";
            if (dgvReservations.Columns["PhoneNumber"] != null) dgvReservations.Columns["PhoneNumber"].HeaderText = "SĐT";
            if (dgvReservations.Columns["TableId"] != null) dgvReservations.Columns["TableId"].HeaderText = "Bàn";
            if (dgvReservations.Columns["ReservationTime"] != null)
            {
                dgvReservations.Columns["ReservationTime"].HeaderText = "Thời Gian";
                dgvReservations.Columns["ReservationTime"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
            }
            if (dgvReservations.Columns["Status"] != null) dgvReservations.Columns["Status"].HeaderText = "Trạng Thái";
            if (dgvReservations.Columns["NumberOfGuests"] != null) dgvReservations.Columns["NumberOfGuests"].HeaderText = "Số khách hàng";
        }
        private void CreateReservationButton_Click(object sender, EventArgs e)
        {
            _overlayBackground.Show(this);

            uc_CreateReservation = new UC_CreateReservation(_scope);

            uc_CreateReservation.OnCreateClicked += async (name, phone, resData, foodList) =>
            {
                try
                {
                    var customer = await _customersRepository.GetCustomerByNameAndPhoneAsync(name, phone);
                    int cusId = 0;
                    if (customer != null)
                    {
                        cusId = customer.Id;
                    }
                    else
                    {
                        var newCus = new Customers { FullName = name, PhoneNumber = phone, Email = phone + "@res.com" };
                        cusId = await _customersRepository.AddCustomerAsync(newCus);
                    }
                    resData.customerId = cusId;

                    int newResId = await _reservationsRepository.AddReservationAsync(resData);

                    if (foodList != null && foodList.Count > 0)
                    {
                        Orders newOrder = new Orders
                        {
                            CustomerId = cusId,
                            TableId = resData.TableId,
                            ReservationId = newResId, 
                            OrderTime = DateTime.Now,
                            TotalAmount = 0,
                            NumberOfGuests = resData.NumberOfGuests
                        };
                        int newOrderId = await _ordersRepository.AddOrderAsync(newOrder);
                        foreach (var item in foodList)
                        {
                            item.OrderId = newOrderId;
                        }
                        await _orderDetailsRepository.AddListOrderDetailAsync(foodList);
                    }

                    MessageBox.Show("Đặt bàn thành công!");
                    ClosePopup();
                    LoadReservationList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            };

            // Sự kiện thoát
            uc_CreateReservation.OnExitClicked += (s, args) => ClosePopup();

            // Hiển thị
            this.Controls.Add(uc_CreateReservation);
            Helper.BoGoc(uc_CreateReservation, 5, true, true, true, true);
            uc_CreateReservation.BringToFront();

            // Căn giữa
            uc_CreateReservation.Location = new Point(
                 (this.ClientSize.Width - uc_CreateReservation.Width) / 2,
                 (this.ClientSize.Height - uc_CreateReservation.Height) / 2
            );
        }

        private void ClosePopup()
        {
            if (uc_CreateReservation != null)
            {
                this.Controls.Remove(uc_CreateReservation);
                uc_CreateReservation.Dispose();
            }
            _overlayBackground.Hide(this);
        }
    }
}
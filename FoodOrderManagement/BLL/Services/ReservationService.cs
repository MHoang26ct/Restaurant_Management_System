using Autofac;
using FoodOrderManagement.DAL.Models.Entities;
using FoodOrderManagement.DAL.Repositories.Implementations;
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
        public event Action<int, string, string, Reservations> OnUpdateClicked;
        private int _editingReservationId = 0;
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
            string selectedTime = TimeReservationCBox.SelectedItem.ToString();
            DateTime datePart = DateReservation.Value.Date;
            TimeSpan timePart = TimeSpan.Parse(selectedTime);
            DateTime fullDate = datePart.Add(timePart);
            Reservations res = new Reservations
            {
                Id = _editingReservationId,
                TableId = (int)TableID_NBox.Value,
                NumberOfGuests = (int)NumberOfGuest.Value,
                ReservationTime = fullDate,
                ComingTime = fullDate,
                Status = "Pending",
                customerId = 0
            };
            if (_editingReservationId == 0)
            {
                List<orderDetail> foodList = new List<orderDetail>();
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
            else
            {
                OnUpdateClicked?.Invoke(_editingReservationId, name, phone, res);
            }
        }
        public void SetReservationData(Reservations res, string cusName, string cusPhone)
        {
            _editingReservationId = res.Id; // Đánh dấu là đang sửa ID này

            // Điền thông tin khách
            CustomerNameTBox.Text = cusName;
            PhoneNumberTBox.Text = cusPhone;

            // Điền thông tin bàn
            TableID_NBox.Value = res.TableId;
            NumberOfGuest.Value = res.NumberOfGuests;

            // Xử lý ngày giờ
            DateReservation.Value = res.ReservationTime;

            // Tìm và chọn giờ trong ComboBox (VD: 14:30)
            string timeString = res.ReservationTime.ToString("H:mm");
            // Cần format H:mm để khớp với chuỗi "9:30", "14:00" trong ComboBox

            int index = TimeReservationCBox.FindStringExact(timeString);
            if (index != -1)
            {
                TimeReservationCBox.SelectedIndex = index;
            }

            // Đổi tên nút
            CreateReservationButton.Text = "Cập Nhật Đặt Bàn";
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
            AddActionButtons();
            DecorDataGridView(dgvReservations);
            StyleActionHeader(dgvReservations);

            // 4. Gắn sự kiện vẽ (để xóa vạch ngăn cách)
            dgvReservations.CellPainting += dgvReservations_CellPainting;
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
            if (dgvReservations.Columns["NumberOfGuests"] != null) dgvReservations.Columns["NumberOfGuests"].HeaderText = "Lượng khách";
            AdjustColumnWidths(dgvReservations);
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

            uc_CreateReservation.OnExitClicked += (s, args) => ClosePopup();
            this.Controls.Add(uc_CreateReservation);
            Helper.BoGoc(uc_CreateReservation, 5, true, true, true, true);
            uc_CreateReservation.BringToFront();
            uc_CreateReservation.Location = new Point(
                 (this.ClientSize.Width - uc_CreateReservation.Width) / 2,
                 (this.ClientSize.Height - uc_CreateReservation.Height) / 2
            );
        }
        private async void dgvReservations_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            int reservationId = Convert.ToInt32(dgvReservations.Rows[e.RowIndex].Cells["Id"].Value);

            if (dgvReservations.Columns[e.ColumnIndex].Name == "btnEdit")
            {
                HandleEditBooking(reservationId);
            }

            if (dgvReservations.Columns[e.ColumnIndex].Name == "btnDelete")
            {
                if (MessageBox.Show("Bạn có chắc chắn muốn xóa đơn đặt bàn này?", "Xác nhận",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    try
                    {
                        await _reservationsRepository.DeleteReservationAsync(reservationId);
                        LoadReservationList();
                        MessageBox.Show("Xóa thành công!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi xóa: " + ex.Message);
                    }
                }
            }
        }
        private async void HandleEditBooking(int reservationId)
        {
            var booking = await _reservationsRepository.GetReservationByReservationIdAsync(reservationId);
            if (booking == null) return;

            var customer = await _customersRepository.GetCustomerByIdAsync(booking.customerId);
            string cusName = customer != null ? customer.FullName : "";
            string cusPhone = customer != null ? customer.PhoneNumber : "";

            _overlayBackground.Show(this);
            uc_CreateReservation = new UC_CreateReservation(_scope);

            uc_CreateReservation.SetReservationData(booking, cusName, cusPhone);

            uc_CreateReservation.OnUpdateClicked += async (id, name, phone, updatedRes) =>
            {
                try
                {
                    if (customer != null)
                    {
                        customer.FullName = name;
                        customer.PhoneNumber = phone;
                        await _customersRepository.UpdateCustomerInfoAsync(customer);
                    }

                    await _reservationsRepository.UpdateReservationAsync(updatedRes);

                    MessageBox.Show("Cập nhật thành công!");
                    ClosePopup();
                    LoadReservationList(); 
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi cập nhật: " + ex.Message);
                }
            };

            uc_CreateReservation.OnExitClicked += (s, args) => ClosePopup();
            ShowPopupUC(uc_CreateReservation);
        }
        private void AddActionButtons()
        {
            if (dgvReservations.Columns["btnEdit"] == null)
            {
                DataGridViewButtonColumn btnEdit = new DataGridViewButtonColumn();
                btnEdit.Name = "btnEdit";
                btnEdit.HeaderText = "";
                btnEdit.Text = "Sửa";
                btnEdit.UseColumnTextForButtonValue = true; 
                btnEdit.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dgvReservations.Columns.Add(btnEdit);
            }
            if (dgvReservations.Columns["btnDelete"] == null)
            {
                DataGridViewButtonColumn btnDelete = new DataGridViewButtonColumn();
                btnDelete.Name = "btnDelete";
                btnDelete.HeaderText = "";
                btnDelete.Text = "Xóa";
                btnDelete.UseColumnTextForButtonValue = true; 
                btnDelete.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                btnDelete.DefaultCellStyle.ForeColor = Color.Red;
                btnDelete.DefaultCellStyle.SelectionForeColor = Color.Red;

                dgvReservations.Columns.Add(btnDelete);
            }
        }
        private void DecorDataGridView(DataGridView dgv)
        {
            dgv.BorderStyle = BorderStyle.None;
            dgv.BackgroundColor = Color.White;
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersHeight = 50;
            dgv.RowTemplate.Height = 60;
            dgv.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(100, 88, 255);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(231, 229, 255);
            dgv.DefaultCellStyle.SelectionForeColor = Color.FromArgb(71, 69, 94);
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 245, 255);
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            if (dgv.Columns.Contains("btnEdit"))
                dgv.Columns["btnEdit"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            if (dgv.Columns.Contains("btnDelete"))
                dgv.Columns["btnDelete"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            if (dgv.Columns.Contains("Id"))
                dgv.Columns["Id"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
        }
        private void StyleActionHeader(DataGridView dgv)
        {
            Color blueColor = Color.FromArgb(0, 122, 204);

            if (dgv.Columns.Contains("btnEdit"))
            {
                dgv.Columns["btnEdit"].HeaderCell.Style.BackColor = blueColor;
                dgv.Columns["btnEdit"].HeaderCell.Style.ForeColor = Color.White;
                dgv.Columns["btnEdit"].HeaderText = ""; 
            }

            if (dgv.Columns.Contains("btnDelete"))
            {
                dgv.Columns["btnDelete"].HeaderCell.Style.BackColor = blueColor;
                dgv.Columns["btnDelete"].HeaderCell.Style.ForeColor = Color.White;
                dgv.Columns["btnDelete"].HeaderText = "";
            }
        }
        private void AdjustColumnWidths(DataGridView dgv)
        {
            // === 1. CÁC CỘT CẦN GIÃN RA (Dùng Fill và chia tỷ lệ) ===

            // Tên Khách: Cho chiếm nhiều nhất (khoảng 35%)
            if (dgv.Columns.Contains("CustomerName"))
            {
                dgv.Columns["CustomerName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgv.Columns["CustomerName"].FillWeight = 35;
            }

            // SĐT: Cho giãn ra vừa phải (20%)
            if (dgv.Columns.Contains("PhoneNumber"))
            {
                dgv.Columns["PhoneNumber"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgv.Columns["PhoneNumber"].FillWeight = 20;
            }

            // Thời gian: Cần rộng để hiện đủ ngày giờ (25%)
            if (dgv.Columns.Contains("ReservationTime"))
            {
                dgv.Columns["ReservationTime"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgv.Columns["ReservationTime"].FillWeight = 25;
            }

            // Trạng thái: Giãn nốt phần còn lại (20%)
            if (dgv.Columns.Contains("Status"))
            {
                dgv.Columns["Status"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgv.Columns["Status"].FillWeight = 20;
            }

            // === 2. CÁC CỘT SỐ NHỎ (Giữ gọn gàng - AllCells) ===

            // Cột Bàn
            if (dgv.Columns.Contains("TableId"))
            {
                dgv.Columns["TableId"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                // Căn giữa nội dung cột bàn cho đẹp
                dgv.Columns["TableId"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dgv.Columns["TableId"].DefaultCellStyle.Padding = new Padding(10, 0, 0, 0);
            }

            // Cột Lượng khách
            if (dgv.Columns.Contains("NumberOfGuests"))
            {
                dgv.Columns["NumberOfGuests"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dgv.Columns["NumberOfGuests"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dgv.Columns["NumberOfGuests"].DefaultCellStyle.Padding = new Padding(50, 0, 0, 0);
            }

            // === 3. XỬ LÝ CỘT MÃ ĐƠN (Khoảng cách với nút bấm) ===
            if (dgv.Columns.Contains("Id"))
            {
                dgv.Columns["Id"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dgv.Columns["Id"].DefaultCellStyle.Padding = new Padding(20, 0, 0, 0); // Cách ra 20px
            }
        }
        private void dgvReservations_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                if (dgvReservations.Columns[e.ColumnIndex].Name == "btnEdit")
                {
                    Rectangle rect = e.CellBounds;
                    rect.Width += dgvReservations.Columns["btnDelete"].Width;
                    var oldClip = e.Graphics.Clip;
                    e.Graphics.SetClip(e.CellBounds.IntersectsWith(new Rectangle(0, 0, dgvReservations.Width, dgvReservations.Height))
                        ? new Rectangle(0, 0, dgvReservations.Width, dgvReservations.Height) : e.CellBounds);
                    using (Brush brush = new SolidBrush(Color.FromArgb(0, 122, 204)))
                    {
                        e.Graphics.FillRectangle(brush, rect);
                    }
                    using (Brush textBrush = new SolidBrush(Color.White))
                    {
                        StringFormat sf = new StringFormat();
                        sf.Alignment = StringAlignment.Center;     
                        sf.LineAlignment = StringAlignment.Center;  
                        e.Graphics.DrawString("Thao tác", new Font("Segoe UI", 12F, FontStyle.Bold), textBrush, rect, sf);
                    }
                    e.Graphics.Clip = oldClip;
                    e.Handled = true;
                }
                else if (dgvReservations.Columns[e.ColumnIndex].Name == "btnDelete")
                {
                    e.Handled = true;
                }
            }
        }
        private void ShowPopupUC(UserControl uc)
        {
            this.Controls.Add(uc);
            uc.BringToFront();
            uc.Location = new Point(
                 (this.ClientSize.Width - uc.Width) / 2,
                 (this.ClientSize.Height - uc.Height) / 2
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
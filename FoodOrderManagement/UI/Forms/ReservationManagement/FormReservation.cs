using Autofac;
using FoodOrderManagement.DAL.Models.Entities;
using FoodOrderManagement.DAL.Repositories.Interfaces;
using FoodOrderManagement.UI;
using FoodOrderManagement.UI.Forms.MenuManagement;
using FoodOrderManagement.UI.Forms.OrderManagement.UserControlOfOrder;
using FoodOrderManagement.UI.Forms.ReservationManagement;
using FoodOrderManagement.UI.Forms.ReservationManagement.UserControlOfReservation;
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
    public partial class FormReservation : Form

    {
        private readonly ILifetimeScope _scope;
        private readonly IReservationsRepository _reservationsRepository;
        private readonly ICustomersRepository _customersRepository;
        private readonly IOrdersRepository _ordersRepository;
        private readonly IOrderDetailsRepository _orderDetailsRepository;
        private readonly ITablesRepository _tablesRepository;
        private readonly FormTable _formTable;
        private OverlayBackground _overlayBackground;
        private UC_CreateReservation uc_CreateReservation;
        private List<ReservationViewModel> _originalList = new List<ReservationViewModel>();
        public FormReservation(ILifetimeScope scope, IReservationsRepository reservationsRepository, ICustomersRepository customersRepository,
                               IOrdersRepository ordersRepository, IOrderDetailsRepository orderDetailsRepository, ITablesRepository tablesRepository, FormTable formTable)
        {
            InitializeComponent();
            _scope = scope;
            _reservationsRepository = reservationsRepository;
            _customersRepository = customersRepository;
            _ordersRepository = ordersRepository;
            _orderDetailsRepository = orderDetailsRepository;
            _tablesRepository = tablesRepository;

            _formTable = formTable;

            _overlayBackground = new OverlayBackground();
            AddActionButtons();
            DecorDataGridView(dgvReservations);
            StyleActionHeader(dgvReservations);
            dgvReservations.CellPainting += dgvReservations_CellPainting;
            SearchReservationTBox1.TextChanged += (s, e) => ApplyFilter();
            DateTimePickerSearch.ValueChanged += (s, e) => ApplyFilter();
            LoadReservationList();
        }
        private void FormReservation_Load(object sender, EventArgs e)
        {
            DecorDataGridView(dgvReservations);
        }

        //For every tick of the timer, check the list of reservations,
        //if any reservation time will occur within next 1 hour, update the status of the
        //table to "Reserved", then show a notification to the admin and reload the table forms.
        private async void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            try
            {
                var reservationList = await _reservationsRepository.GetAllReservationsEntityAsync();
                var currentTime = DateTime.Now;
                bool ifChanged = false;
                List<int> upcomingIds = new List<int>();

                foreach (var reservation in reservationList)
                {
                    var timeDiff = (reservation.ReservationTime - currentTime).TotalMinutes;
                    if (reservation.Status == "Pending" && timeDiff <= 60 && timeDiff > 0)
                    {
                        ifChanged = true;
                        upcomingIds.Add(reservation.Id);
                        await _tablesRepository.UpdateTableStatusAndOpenTimeAsync(reservation.TableId, "Reserved", null);
                        reservation.Status = "Upcoming";
                        await _reservationsRepository.UpdateReservationAsync(reservation);
                    }
                }

                if (ifChanged)
                {
                    string msg = $"Có {upcomingIds.Count} đơn đặt bàn sắp đến trong 1 tiếng tới (Mã: {string.Join(", ", upcomingIds)})";
                    MessageBox.Show(msg, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await LoadReservationList();
                    await _formTable.LoadTableList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi Timer: {ex.Message}");
            }
            finally
            {
                timer1.Start();
            }
        }

        //Thêm nút Sửa, Xóa
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
                btnEdit.DisplayIndex = dgvReservations.Columns.Count - 1;
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
                btnDelete.DisplayIndex = dgvReservations.Columns.Count - 1;
            }
        }

        //
        // Decor datagridview
        //
        private void DecorDataGridView(DataGridView dgv)
        {
            dgv.BorderStyle = BorderStyle.None;
            dgv.BackgroundColor = Color.White;
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersHeight = 50;
            dgv.RowTemplate.Height = 50;
            dgv.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 12F);
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.AntiqueWhite;
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.Maroon;
            dgv.DefaultCellStyle.SelectionBackColor = Color.White;
            dgv.DefaultCellStyle.SelectionForeColor = Color.Black;
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
            if (dgv.Columns.Contains("btnEdit"))
            {
                dgv.Columns["btnEdit"].HeaderCell.Style.BackColor = Color.AntiqueWhite;
                dgv.Columns["btnEdit"].HeaderCell.Style.ForeColor = Color.Maroon;
                dgv.Columns["btnEdit"].HeaderText = "";
            }

            if (dgv.Columns.Contains("btnDelete"))
            {
                dgv.Columns["btnDelete"].HeaderCell.Style.BackColor = Color.AntiqueWhite;
                dgv.Columns["btnDelete"].HeaderCell.Style.ForeColor = Color.Maroon;
                dgv.Columns["btnDelete"].HeaderText = "";
            }
        }
        private void AdjustColumnWidths(DataGridView dgv)
        {
            if (dgv.Columns.Contains("CustomerName"))
            {
                dgv.Columns["CustomerName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgv.Columns["CustomerName"].FillWeight = 35;
            }
            if (dgv.Columns.Contains("PhoneNumber"))
            {
                dgv.Columns["PhoneNumber"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgv.Columns["PhoneNumber"].FillWeight = 20;
            }
            if (dgv.Columns.Contains("ReservationTime"))
            {
                dgv.Columns["ReservationTime"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgv.Columns["ReservationTime"].FillWeight = 25;
            }
            if (dgv.Columns.Contains("Status"))
            {
                dgv.Columns["Status"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgv.Columns["Status"].FillWeight = 20;
                dgv.Columns["Status"].DefaultCellStyle.Padding = new Padding(10, 0, 0, 0);
            }
            if (dgv.Columns.Contains("TableId"))
            {
                dgv.Columns["TableId"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dgv.Columns["TableId"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dgv.Columns["TableId"].DefaultCellStyle.Padding = new Padding(10, 0, 0, 0);
            }
            if (dgv.Columns.Contains("NumberOfGuests"))
            {
                dgv.Columns["NumberOfGuests"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dgv.Columns["NumberOfGuests"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dgv.Columns["NumberOfGuests"].DefaultCellStyle.Padding = new Padding(50, 0, 0, 0);
            }
            if (dgv.Columns.Contains("Id"))
            {
                dgv.Columns["Id"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dgv.Columns["Id"].DefaultCellStyle.Padding = new Padding(20, 0, 0, 0);
            }
        }
        private void dgvReservations_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                if (dgvReservations.Columns[e.ColumnIndex].Name == "btnEdit")
                {
                    if (!dgvReservations.Columns.Contains("btnDelete")) return;
                    Rectangle rect = e.CellBounds;
                    rect.Width += dgvReservations.Columns["btnDelete"].Width;
                    var oldClip = e.Graphics.Clip;
                    e.Graphics.SetClip(e.CellBounds.IntersectsWith(new Rectangle(0, 0, dgvReservations.Width, dgvReservations.Height))
                        ? new Rectangle(0, 0, dgvReservations.Width, dgvReservations.Height) : e.CellBounds);
                    using (Brush brush = new SolidBrush(Color.AntiqueWhite))
                    {
                        e.Graphics.FillRectangle(brush, rect);
                    }
                    using (Brush textBrush = new SolidBrush(Color.Maroon))
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
    }
}

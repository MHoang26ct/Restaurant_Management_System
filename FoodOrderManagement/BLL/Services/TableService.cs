using Autofac;
using FoodOrderManagement.DAL.Models.Entities;
using FoodOrderManagement.DAL.Repositories.Interfaces;
using FoodOrderManagement.UI;
using FoodOrderManagement.UI.Forms.TableManagement;
using FoodOrderManagement.UI.Forms.TableManagement.UserControlOfTable;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace FoodOrderManagement.UI.Forms.TableManagement.UserControlOfTable
{
    public partial class UC_AddTable : UserControl
    {
        private ITablesRepository _tablesRepository;

        // Sự kiện báo thêm thành công
        public event EventHandler OnTableAdded;
        // Hàm nhận Repo (Dependency Injection thủ công)
        public void SetRepository(ITablesRepository repo)
        {
            _tablesRepository = repo;
        }

        private async void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // Giả sử bạn có NumericUpDown tên numericCapacity
                // Nếu chưa có, hãy vào Designer thêm vào nhé
                int capacity = (int)CapacityNBox.Value;

                if (_tablesRepository != null)
                {
                    await _tablesRepository.AddTableAsync(capacity, "Available");
                    MessageBox.Show("Thêm bàn mới thành công!");

                    // Bắn sự kiện để Form cha load lại
                    OnTableAdded?.Invoke(this, EventArgs.Empty);

                    // Đóng popup
                    this.Parent.Controls.Remove(this);
                    this.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.Parent.Controls.Remove(this);
            this.Dispose();
        }
    }
}

//------------------------------------------------------------------------------------------------------------------------------------------------------------------

namespace FoodOrderManagement.UI.Forms.TableManagement.UserControlOfTable
{
    public partial class UC_TableItem : UserControl
    {
        // Biến lưu dữ liệu bàn hiện tại
        public TableData CurrentData { get; private set; }

        // Sự kiện Click truyền data ra ngoài
        public event EventHandler<TableData> OnTableClicked;

        private void TriggerClick(object sender, EventArgs e)
        {
            OnTableClicked?.Invoke(this, CurrentData);
        }

        public void SetData(TableData data)
        {
            CurrentData = data;

            NumberCircleLabel.Text = data.TableId.ToString();
            NumberTableLabel.Text = data.TableName; // Hoặc "Bàn " + data.TableId
            CapacityLabel.Text = data.Capacity.ToString();

            // Logic đổi màu (Giữ nguyên code giao diện cũ của bạn ở đây)
            UpdateUIByStatus(data.Status);
        }

        private void UpdateUIByStatus(string status)
        {
            // ... Copy đoạn switch-case đổi màu GunaUI của bạn vào đây ...
            // Ví dụ:
            switch (status)
            {
                case "Available":
                    this.NumberCircleLabel.FillColor = Color.FromArgb(80, 200, 120);
                    this.NumberCircleLabel.HoverState.FillColor = Color.FromArgb(80, 200, 120);
                    this.StatusPanel.FillColor = Color.FromArgb(220, 255, 220);
                    this.StatusPanel.FillColor2 = Color.FromArgb(220, 255, 220);
                    this.StatusPanel.BorderColor = Color.FromArgb(150, 255, 150);
                    this.StatusMiniPanel.FillColor = Color.FromArgb(150, 255, 150);
                    this.StatusText.FillColor = Color.FromArgb(220, 255, 220);
                    this.StatusText.FillColor2 = Color.FromArgb(220, 255, 220);
                    this.StatusText.HoverState.FillColor = Color.FromArgb(220, 255, 220);
                    this.StatusText.HoverState.FillColor2 = Color.FromArgb(220, 255, 220);
                    this.StatusText.ForeColor = Color.DarkGreen;
                    this.StatusText.Text = status;
                    break;
                case "Occupied":
                    this.NumberCircleLabel.FillColor = Color.Red;
                    this.NumberCircleLabel.HoverState.FillColor = Color.Red;
                    this.StatusPanel.FillColor = Color.FromArgb(255, 192, 192);
                    this.StatusPanel.FillColor2 = Color.FromArgb(255, 192, 192);
                    this.StatusPanel.BorderColor = Color.FromArgb(255, 128, 128);
                    this.StatusMiniPanel.FillColor = Color.FromArgb(255, 128, 128);
                    this.StatusText.FillColor = Color.FromArgb(255, 192, 192);
                    this.StatusText.FillColor2 = Color.FromArgb(255, 192, 192);
                    this.StatusText.HoverState.FillColor = Color.FromArgb(255, 192, 192);
                    this.StatusText.HoverState.FillColor2 = Color.FromArgb(255, 192, 192);
                    this.StatusText.ForeColor = Color.DarkRed;
                    this.StatusText.Text = status;
                    break;
                case "Reserved":
                    this.NumberCircleLabel.FillColor = Color.Gold;
                    this.NumberCircleLabel.HoverState.FillColor = Color.Gold;
                    this.StatusPanel.FillColor = Color.FromArgb(255, 255, 128);
                    this.StatusPanel.FillColor2 = Color.FromArgb(255, 255, 128);
                    this.StatusPanel.BorderColor = Color.Gold;
                    this.StatusMiniPanel.FillColor = Color.Gold;
                    this.StatusText.FillColor = Color.FromArgb(255, 255, 128);
                    this.StatusText.FillColor2 = Color.FromArgb(255, 255, 128);
                    this.StatusText.HoverState.FillColor = Color.FromArgb(255, 255, 128);
                    this.StatusText.HoverState.FillColor2 = Color.FromArgb(255, 255, 128);
                    this.StatusText.ForeColor = Color.SaddleBrown;
                    this.StatusText.Text = status;
                    break;
            }
        }
    }
}




namespace FoodOrderManagement.UI.Forms.TableManagement.UserControlOfTable
{
    public partial class UC_UpdateStatus : UserControl
    {
        private readonly ILifetimeScope _scope;
        private readonly ITablesRepository _tablesRepository;
        public event EventHandler OnStatusChanged;

        private async void UpdateStatus(string newStatus, DateTime? openTime)
        {
            if (_tablesRepository == null) return;
            try
            {
                // Gọi Repo với tham số được truyền vào
                await _tablesRepository.UpdateTableStatusAndOpenTimeAsync(_currentTableId, newStatus, openTime);

                MessageBox.Show("Cập nhật trạng thái thành công!");

                OnStatusChanged?.Invoke(this, EventArgs.Empty);

                ExitButton_Click(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }
    }
}




namespace FoodOrderManagement.AdminControl
{
    public partial class FormTable : Form
    {
        private async void LoadTableList()
        {
            try
            {
                // 1. Xóa sạch màn hình trước khi vẽ
                FlowLayoutTable.Controls.Clear();

                // 2. Tạo Card "Thêm Bàn" (Dấu cộng)
                var addCard = _scope.Resolve<UC_AddTableCard>();
                // Hoặc: var addCard = new UC_AddTableCard();

                addCard.OnCardClicked += (s, e) => ShowAddTablePopup();

                FlowLayoutTable.Controls.Add(addCard);

                // 3. Load danh sách bàn thật từ Database
                var tables = await _tablesRepository.GetAllTablesAsync();
                UpdateStatisticsUI(tables);
                foreach (var t in tables)
                {
                    // Tạo item hiển thị bàn
                    var item = _scope.Resolve<UC_TableItem>();

                    // Chuyển dữ liệu từ DB sang DTO hiển thị
                    var data = new TableData
                    {
                        TableId = t.Id,
                        TableName = "Bàn " + t.Id.ToString(),
                        Status = t.Status,
                        Capacity = t.Capacity
                    };

                    item.SetData(data);

                    // Gán sự kiện Click vào bàn -> Sửa trạng thái
                    item.OnTableClicked += (sender, tableData) => ShowUpdateStatusPopup(tableData);

                    FlowLayoutTable.Controls.Add(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách: " + ex.Message);
            }
        }

        // --- LOGIC HIỆN POPUP THÊM BÀN ---
        private void ShowAddTablePopup()
        {
            _overlayBackground.Show(this);

            var ucAdd = _scope.Resolve<UC_AddTable>();
            ucAdd.SetRepository(_tablesRepository); // Nếu bạn chưa dùng DI Constructor cho UC

            // Khi thêm xong -> Load lại danh sách (Để hiện bàn mới lên ngay)
            ucAdd.OnTableAdded += (s, args) =>
            {
                LoadTableList(); // 👈 Quan trọng: Load lại từ DB
                _overlayBackground.Hide(this);
            };

            // Khi bấm hủy hoặc đóng
            ucAdd.Disposed += (s, args) => _overlayBackground.Hide(this);

            // Căn giữa màn hình
            this.Controls.Add(ucAdd);
            ucAdd.Location = new Point(
                (this.Width - ucAdd.Width) / 2,
                (this.Height - ucAdd.Height) / 2
            );
            ucAdd.BringToFront();
        }

        // --- LOGIC HIỆN POPUP SỬA TRẠNG THÁI ---
        private void ShowUpdateStatusPopup(TableData data)
        {
            _overlayBackground.Show(this);

            var ucUpdate = _scope.Resolve<UC_UpdateStatus>();
            ucUpdate.LoadTableData(data);

            ucUpdate.OnStatusChanged += (s, args) =>
            {
                LoadTableList(); // 👈 Load lại để cập nhật màu sắc mới
                _overlayBackground.Hide(this);
            };

            ucUpdate.Disposed += (s, args) => _overlayBackground.Hide(this);

            this.Controls.Add(ucUpdate);
            ucUpdate.Location = new Point(
                (this.Width - ucUpdate.Width) / 2,
                (this.Height - ucUpdate.Height) / 2
            );
            ucUpdate.BringToFront();
        }
        private void UpdateStatisticsUI(List<Tables> tables)
        {
            if (tables == null) return;

            // 1. Tính toán
            int total = tables.Count;
            int available = tables.Count(t => t.Status == "Available");
            int occupied = tables.Count(t => t.Status == "Occupied");
            int reserved = tables.Count(t => t.Status == "Reserved");

            // 2. Hiển thị lên Label (Đảm bảo bạn đã đặt tên Label đúng như Bước 1)
            TotalTableLabel.Text = total.ToString();
            AvailableLabel.Text = available.ToString();
            OccupiedLabel.Text = occupied.ToString();
            ReservedLabel.Text = reserved.ToString();
        }
    }
}

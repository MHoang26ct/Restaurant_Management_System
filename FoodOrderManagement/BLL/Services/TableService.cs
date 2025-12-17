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

        public event EventHandler OnTableAdded;
        public void SetRepository(ITablesRepository repo)
        {
            _tablesRepository = repo;
        }

        private async void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                int capacity = (int)CapacityNBox.Value;

                if (_tablesRepository != null)
                {
                    await _tablesRepository.AddTableAsync(capacity, "Available");
                    MessageBox.Show("Thêm bàn mới thành công!");

                    OnTableAdded?.Invoke(this, EventArgs.Empty);
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
        public TableData CurrentData { get; private set; }
        public event EventHandler<TableData> OnTableClicked;

        private void TriggerClick(object sender, EventArgs e)
        {
            OnTableClicked?.Invoke(this, CurrentData);
        }

        public void SetData(TableData data)
        {
            CurrentData = data;

            NumberCircleLabel.Text = data.TableId.ToString();
            NumberTableLabel.Text = data.TableName; 
            CapacityLabel.Text = data.Capacity.ToString();
            UpdateUIByStatus(data.Status);
        }

        private void UpdateUIByStatus(string status)
        {
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
                FlowLayoutTable.Controls.Clear();
                var addCard = _scope.Resolve<UC_AddTableCard>();
                addCard.OnCardClicked += (s, e) => ShowAddTablePopup();
                FlowLayoutTable.Controls.Add(addCard);
                var tables = await _tablesRepository.GetAllTablesAsync();
                UpdateStatisticsUI(tables);
                foreach (var t in tables)
                {                    
                    var item = _scope.Resolve<UC_TableItem>();             
                    var data = new TableData
                    {
                        TableId = t.Id,
                        TableName = "Bàn " + t.Id.ToString(),
                        Status = t.Status,
                        Capacity = t.Capacity
                    };

                    item.SetData(data);
                    item.OnTableClicked += (sender, tableData) => ShowUpdateStatusPopup(tableData);
                    FlowLayoutTable.Controls.Add(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách: " + ex.Message);
            }
        }


        private void ShowAddTablePopup()
        {
            _overlayBackground.Show(this);

            var ucAdd = _scope.Resolve<UC_AddTable>();
            ucAdd.SetRepository(_tablesRepository); 

            ucAdd.OnTableAdded += (s, args) =>
            {
                LoadTableList(); 
                _overlayBackground.Hide(this);
            };
            ucAdd.Disposed += (s, args) => _overlayBackground.Hide(this);
            this.Controls.Add(ucAdd);
            ucAdd.Location = new Point(
                (this.Width - ucAdd.Width) / 2,
                (this.Height - ucAdd.Height) / 2
            );
            ucAdd.BringToFront();
        }

        private void ShowUpdateStatusPopup(TableData data)
        {
            _overlayBackground.Show(this);

            var ucUpdate = _scope.Resolve<UC_UpdateStatus>();
            ucUpdate.LoadTableData(data);

            ucUpdate.OnStatusChanged += (s, args) =>
            {
                LoadTableList(); 
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

            int total = tables.Count;
            int available = tables.Count(t => t.Status == "Available");
            int occupied = tables.Count(t => t.Status == "Occupied");
            int reserved = tables.Count(t => t.Status == "Reserved");

            TotalTableLabel.Text = total.ToString();
            AvailableLabel.Text = available.ToString();
            OccupiedLabel.Text = occupied.ToString();
            ReservedLabel.Text = reserved.ToString();
        }
    }
}

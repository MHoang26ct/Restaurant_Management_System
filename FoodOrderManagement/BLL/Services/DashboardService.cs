namespace FoodOrderManagement.AdminControl
{
    public partial class FormDashboard : Form
    {
        private async void FormHome_Load(object sender, EventArgs e)
        {
            await LoadDashboardData();
        }

        private async Task LoadDashboardData()
        {
            try
            {
                var todayStats = await _statisticsRepository.GetDashboardStatsByDateAsync(DateTime.Now);
                var yesterdayStats = await _statisticsRepository.GetDashboardStatsByDateAsync(DateTime.Now.AddDays(-1));


                LabelOfOrder2.Text = todayStats.TotalOrders.ToString();
                LabelOfReport2.Text = todayStats.TotalRevenue.ToString("N0") + " VND"; 
                LabelOfTable2.Text = todayStats.TotalReservations.ToString();
                CustomerOfLabel2.Text = todayStats.TotalCustomers.ToString();

                UpdateGrowthLabel(LabelOfOrder3, OrderIcon2,todayStats.TotalOrders, yesterdayStats.TotalOrders);
                UpdateGrowthLabel(LabelOfReport3, ReportIcon2,todayStats.TotalRevenue, yesterdayStats.TotalRevenue);
                UpdateGrowthLabel(LabelOfTable3, TableIcon2,todayStats.TotalReservations, yesterdayStats.TotalReservations);
                UpdateGrowthLabel(CustomerOfLabel3, CustomerIcon2,todayStats.TotalCustomers, yesterdayStats.TotalCustomers);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải Dashboard: " + ex.Message);
            }
        }

        private void UpdateGrowthLabel(Label lblPercent, PictureBox picTrend, decimal current, decimal past)
        {
            picTrend.SizeMode = PictureBoxSizeMode.Zoom;
            if (past == 0)
            {
                if (current > 0)
                {
                    lblPercent.Text = "+100%";
                    lblPercent.ForeColor = Color.Lime;
                    picTrend.Image = Properties.Resources.increase; 
                    picTrend.Visible = true; 
                }
                else
                {
                    lblPercent.Text = "0%";
                    lblPercent.ForeColor = Color.Black;
                    picTrend.Image = null;
                }
                return;
            }
            decimal growth = ((current - past) / past) * 100;
            lblPercent.Text = $"{Math.Abs(growth):F1}% (so với hôm qua)";

            if (growth >= 0)
            {
                picTrend.Image = Properties.Resources.increase;
                lblPercent.ForeColor = Color.Lime;
            }
            else
            {
                picTrend.Image = Properties.Resources.decrease;
                lblPercent.ForeColor = Color.Crimson;
            }
            picTrend.Visible = true;
        }
    }
}

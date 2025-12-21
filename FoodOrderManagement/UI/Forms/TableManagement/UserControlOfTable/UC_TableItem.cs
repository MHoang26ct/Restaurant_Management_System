using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TheArtOfDevHtmlRenderer.Adapters.Entities;

namespace FoodOrderManagement.UI.Forms.TableManagement.UserControlOfTable
{
    public partial class UC_TableItem : UserControl
    {
        public UC_TableItem()
        {
            InitializeComponent();
            // Gán click cho tất cả control con để bấm đâu cũng ăn
            this.Click += TriggerClick;
            NumberCircleLabel.Click += TriggerClick;
            NumberTableLabel.Click += TriggerClick;
            StatusPanel.Click += TriggerClick;
            StatusText.Click += TriggerClick;
        }
        //Click vào table item
        private void TriggerClick(object sender, EventArgs e)
        {
            OnTableClicked?.Invoke(this, CurrentData);
        }
    }
}

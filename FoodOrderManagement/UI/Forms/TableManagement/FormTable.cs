using FoodOrderManagement.UI.Forms.OrderManagement.UserControlOfOrder;
using FoodOrderManagement.UI.Forms.TableManagement.UserControlOfTable;
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
    
    public partial class FormTable : Form
    {
        UC_AddTableCard uc_AddTableCard;
        public FormTable()
        {
            InitializeComponent();
            FlowLayoutLoad();
        }

        private void FlowLayoutLoad()
        {
            uc_AddTableCard = new UC_AddTableCard();
            FlowLayoutTable.Controls.Add(uc_AddTableCard);
            
        }

        private void HandlerAddTableClicked()
        {
            UC_AddTable uc_AddTable = new UC_AddTable();
            this.Controls.Add(uc_AddTable);
            uc_AddTable.Location = new Point(
                 (this.Width - uc_AddTable.Width) / 2,
                 (this.Height - uc_AddTable.Height) / 2
            );
            uc_AddTable.BringToFront();
        }
    }
}

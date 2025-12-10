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
        UC_AddTable uc_AddTable;
        private int TableCount = 0;
        public FormTable()
        {
            InitializeComponent();
            SetupUI();
        }

        private void SetupUI()
        {
            //AddTableCard
            uc_AddTableCard = new UC_AddTableCard();
            uc_AddTableCard.OnCardClicked += (s, e) => ShowAddTable(); // hiện bảng nhập liệu

            FlowLayoutTable.Controls.Add(uc_AddTableCard);
            //AddTable
            uc_AddTable = new UC_AddTable();
            this.Controls.Add(uc_AddTable);
            uc_AddTable.Visible = false;

            uc_AddTable.OnSaveTable += (s, Capacity) =>
            {
                AddNewTable(Capacity);
                uc_AddTable.Visible = false;
            };

        }
        private void ShowAddTable()
        {
            uc_AddTable.ResetData();
            uc_AddTable.Location = new Point(
                (this.Width - uc_AddTable.Width) / 2,
                (this.Height - uc_AddTable.Height) / 2
            );
            uc_AddTable.Visible = true;
            uc_AddTable.BringToFront();
        }

        private void AddNewTable(int Capacity)
        {
            TableCount++;
            UC_TableItem NewTable = new UC_TableItem();
            NewTable.SetData(TableCount, Capacity, "Available");

            FlowLayoutTable.Controls.Add(NewTable);
            FlowLayoutTable.Controls.SetChildIndex(uc_AddTableCard, FlowLayoutTable.Controls.Count - 1);
        }

    }
}

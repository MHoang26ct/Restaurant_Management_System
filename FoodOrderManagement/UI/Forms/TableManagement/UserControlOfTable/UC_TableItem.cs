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
        }

        public void SetData(int TableID, int Capacity, string Status)
        {
            NumberCircleLabel.Text = TableID.ToString();
            NumberTableLabel.Text = "Bàn " + TableID.ToString();
            CapacityLabel.Text = Capacity.ToString();
            switch (Status)
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
                    this.StatusText.Text = Status;
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
                    this.StatusText.Text = Status;
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
                    this.StatusText.Text = Status;
                    break;
            }
        }
    }
}

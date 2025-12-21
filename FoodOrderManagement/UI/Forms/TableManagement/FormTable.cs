using Autofac;
using FoodOrderManagement.DAL.Repositories.Implementations;
using FoodOrderManagement.DAL.Repositories.Interfaces;
using FoodOrderManagement.UI;
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
using static System.Formats.Asn1.AsnWriter;
namespace FoodOrderManagement.AdminControl
{

    public partial class FormTable : Form
    {   
        UC_AddTableCard uc_AddTableCard;
        UC_AddTable uc_AddTable;
        private int TableCount = 0;
        private readonly ILifetimeScope _scope;
        private readonly ITablesRepository _tablesRepository;
        private OverlayBackground _overlayBackground = new OverlayBackground();
        public FormTable(ILifetimeScope scope, ITablesRepository tablesRepository)
        {
            InitializeComponent();
            _scope = scope;
            _tablesRepository = tablesRepository;

            LoadTableList();
        }
    }
}

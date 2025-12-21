using Autofac;
using FoodOrderManagement.DAL.Repositories.Interfaces;
using FoodOrderManagement.UI;
using FoodOrderManagement.UI.Forms;
using FoodOrderManagement.UI.Forms.CustomerManagement.UserControlsOfCustomer;
using FoodOrderManagement.UI.Forms.EmployeManagement;
using FoodOrderManagement.UI.Forms.EmployeManagement.UserControlOfEmployee;
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
    public partial class FormEmployee : Form
    {
        OverlayBackground _overlayBackground;
        ILifetimeScope _scope;
        IEmployeesRepository _employeesRepository;
        public FormEmployee(ILifetimeScope scope, IEmployeesRepository employeesRepository)
        {
            InitializeComponent();
            _scope = scope;
            _employeesRepository = employeesRepository;
            _overlayBackground = new OverlayBackground();
        }

        private void FlowLayoutEmployee_Resize(object sender, EventArgs e)
        {
            foreach (Control item in FlowLayoutEmployee.Controls)
            {
                item.Width = FlowLayoutEmployee.ClientSize.Width;
            }
        }
    }
}

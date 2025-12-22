using Autofac;
using FoodOrderManagement.DAL.Repositories.Implementations;
using FoodOrderManagement.DAL.Repositories.Interfaces;
using FoodOrderManagement.UI.Forms;
using FoodOrderManagement.UI.Forms.CustomerManagement.UserControlsOfCustomer;
using FoodOrderManagement.UI.Forms.MenuManagement;
using FoodOrderManagement.UI.Forms.OrderManagement.UserControlOfOrder;
using FoodOrderManagement.UI.Forms.TableManagement.UserControlOfTable;

namespace FoodOrderManagement.AdminControl {
    internal static class Program {
        [STAThread]
        static void Main() {
            Application.SetHighDpiMode(HighDpiMode.PerMonitorV2);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // --- Cấu hình Autofac ---
            var builder = new ContainerBuilder();

            // 1. Đăng ký các Repository với vòng đời Singleton
            builder.RegisterType<CustomersRepository>().As<ICustomersRepository>().SingleInstance();
            builder.RegisterType<EmployeesRepository>().As<IEmployeesRepository>().SingleInstance();
            builder.RegisterType<FoodsRepository>().As<IFoodsRepository>().SingleInstance();
            builder.RegisterType<OrderDetailsRepository>().As<IOrderDetailsRepository>().SingleInstance();
            builder.RegisterType<OrdersRepository>().As<IOrdersRepository>().SingleInstance();
            builder.RegisterType<ReservationsRepository>().As<IReservationsRepository>().SingleInstance();
            builder.RegisterType<StatisticsRepository>().As<IStatisticsRepository>().SingleInstance();
            builder.RegisterType<TablesRepository>().As<ITablesRepository>().SingleInstance();
            builder.RegisterType<UsersRepository>().As<IUsersRepository>().SingleInstance();

            // 2. Đăng ký Form chính (Rất quan trọng)
            // Autofac cần biết cách tạo MainForm
            builder.RegisterType<FormLogin>();
            builder.RegisterType<FormMain>().SingleInstance();
            builder.RegisterType<FormDashboard>().SingleInstance();
            builder.RegisterType<FrmMenu>().SingleInstance();
            builder.RegisterType<FormOrder>().SingleInstance();
            builder.RegisterType<FormTable>().SingleInstance();
            builder.RegisterType<FormCustomer>().SingleInstance();
            builder.RegisterType<FormReservation>().SingleInstance();
            builder.RegisterType<FormEmployee>().SingleInstance();
            builder.RegisterType<FormCustomer>().SingleInstance();
            builder.RegisterType<UC_AddFood>();
            builder.RegisterType<UC_FoodItem>();
            builder.RegisterType<UC_CreateOrder>();
            builder.RegisterType<UC_AddFoodOrder>();
            builder.RegisterType<UC_OrderItem>();
            builder.RegisterType<UC_ViewDetails>();
            builder.RegisterType<UC_TableItem>();
            builder.RegisterType<UC_AddTable>();
            builder.RegisterType<UC_UpdateStatus>();
            builder.RegisterType<UC_AddTableCard>();
            builder.RegisterType<UC_AddCustomer>();
            builder.RegisterType<UC_CustomerItem>();

            // 3. Xây dựng Container
            var container = builder.Build();

            // 4. Chạy ứng dụng, sử dụng Container để khởi tạo Form
            // Autofac sẽ tự động inject các dependency vào constructor của MainForm
            Application.Run(container.Resolve<FormLogin>());
        }
    }
}
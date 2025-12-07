using Autofac;
using FoodOrderManagement.UI.Forms;
using FoodOrderManagement.DAL.Repositories.Implementations;
using FoodOrderManagement.DAL.Repositories.Interfaces;
using FoodOrderManagement.UI.Forms.MenuManagement;

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
            builder.RegisterType<FormMain>();
            builder.RegisterType<FormDashboard>();
            builder.RegisterType<FrmMenu>();
            builder.RegisterType<FormOrder>();
            builder.RegisterType<FormTable>();
            builder.RegisterType<FormCustomer>();
            builder.RegisterType<FormReport>();
            builder.RegisterType<FormEmployee>();
            builder.RegisterType<UC_AddFood>();
            builder.RegisterType<UC_FoodItem>();

            // 3. Xây dựng Container
            var container = builder.Build();

            // 4. Chạy ứng dụng, sử dụng Container để khởi tạo Form
            // Autofac sẽ tự động inject các dependency vào constructor của MainForm
            Application.Run(container.Resolve<FormLogin>());
        }
    }
}
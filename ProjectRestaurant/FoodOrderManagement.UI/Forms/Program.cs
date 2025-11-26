using Autofac;
using ProjectRestaurant.DAL.Repositories.Implementations;
using ProjectRestaurant.DAL.Repositories.Interfaces;
using ProjectRestaurant.FoodOrderManagement.UI.Forms.MenuManagement;

namespace ProjectRestaurant.AdminControl {
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
            builder.RegisterType<frmMain>();
            builder.RegisterType<FormDashboard>();
            builder.RegisterType<FrmMenu>();
            builder.RegisterType<FormOrder>();
            builder.RegisterType<FormTable>();
            builder.RegisterType<FormCustomer>();
            builder.RegisterType<FormReport>();
            builder.RegisterType<FormEmployee>();


            // 3. Xây dựng Container
            var container = builder.Build();

            // 4. Chạy ứng dụng, sử dụng Container để khởi tạo Form
            // Autofac sẽ tự động inject các dependency vào constructor của MainForm
            Application.Run(container.Resolve<FormLogin>());
        }
    }
}
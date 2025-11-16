namespace ProjectRestaurant
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            // Bật DPI aware CHỈ KHI RUNTIME
            Application.SetHighDpiMode(HighDpiMode.PerMonitorV2);

            // Config mặc định của WinForms (EnableVisualStyles + default font)
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormLogin());
        }
    }
}

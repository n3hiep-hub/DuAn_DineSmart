using DuAn_DineSmart.DAL;
using DuAn_DineSmart.Forms;

namespace DuAn_DineSmart
{
    internal static class Program
    {
        private static readonly string LogPath = Path.Combine(
            AppDomain.CurrentDomain.BaseDirectory, "error.log");

        [STAThread]
        static void Main()
        {
            Application.ThreadException += OnThreadException;
            AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);

            ApplicationConfiguration.Initialize();

            try
            {
                using var db = new AppDbContext();
                db.Database.EnsureCreated();
                DataSeeder.SeedIfEmpty(db);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Không thể kết nối database:\n{ex.Message}\n\n" +
                    "Kiểm tra:\n" +
                    "• MySQL đang chạy (XAMPP/WAMP)\n" +
                    "• Connection string trong AppDbContext.cs\n" +
                    "• Database 'DineSmart' tồn tại",
                    "Lỗi khởi động", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Application.Run(new frmLogin());
        }

        private static void OnThreadException(object sender, ThreadExceptionEventArgs e)
        {
            LogError(e.Exception);
            MessageBox.Show(
                $"Có lỗi xảy ra:\n{e.Exception.Message}\n\nXem file error.log để biết chi tiết.",
                "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private static void OnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            if (e.ExceptionObject is Exception ex)
                LogError(ex);
        }

        private static void LogError(Exception ex)
        {
            try
            {
                string msg = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {ex.GetType().Name}: {ex.Message}\n{ex.StackTrace}\n\n";
                File.AppendAllText(LogPath, msg);
            }
            catch { }
        }
    }
}

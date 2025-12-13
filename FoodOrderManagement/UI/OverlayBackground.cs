using System;
using System.Drawing; // Quan trọng
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace FoodOrderManagement.UI
{
    public class OverlayBackground
    {
        private Guna2Panel _overlay;
        private Bitmap _screenshot; // Biến lưu ảnh chụp màn hình

        public void Show(Form parent)
        {
            if (_overlay != null) return;

            //Chụp lại giao diện hiện tại của Form cha
            _screenshot = new Bitmap(parent.ClientRectangle.Width, parent.ClientRectangle.Height);
            parent.DrawToBitmap(_screenshot, parent.ClientRectangle);

            //Phủ một lớp màu đen bán trong suốt lên tấm ảnh vừa chụp
            using (Graphics g = Graphics.FromImage(_screenshot))
            {
                // Màu đen, độ trong suốt 100 (khoảng 40%)
                using (Brush brush = new SolidBrush(Color.FromArgb(100, 0, 0, 0)))
                {
                    g.FillRectangle(brush, 0, 0, _screenshot.Width, _screenshot.Height);
                }
            }

            //Tạo Panel Overlay với nền là tấm ảnh đã xử lý
            _overlay = new Guna2Panel
            {
                Dock = DockStyle.Fill,
                BackgroundImage = _screenshot, // Gán ảnh làm nền
                Name = "OverlayPanel"
            };

            //Thêm vào Form
            parent.Controls.Add(_overlay);
            _overlay.BringToFront();
        }

        public void Hide(Form parent)
        {
            if (_overlay == null) return;

            parent.Controls.Remove(_overlay);
            _overlay.Dispose();
            _overlay = null;

            // Giải phóng bộ nhớ ảnh chụp
            if (_screenshot != null)
            {
                _screenshot.Dispose();
                _screenshot = null;
            }
        }
    }
}
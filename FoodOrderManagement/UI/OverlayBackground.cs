using Guna.UI2.WinForms;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace FoodOrderManagement.UI
{
    public class OverlayBackground
    {
        private Guna2Panel _overlay;
        private Bitmap _screenshot;

        public void Show(Form parent)
        {
            FormMain.instance.LockNavigationButtons(parent);
            // Nếu đang hiện rồi thì thôi
            if (_overlay != null) return;
            if (parent == null || parent.IsDisposed) return;

            // --- BƯỚC 1: CỐ GẮNG CHỤP MÀN HÌNH (TRONG VÒNG BẢO VỆ) ---
            try
            {
                // Đảm bảo kích thước luôn >= 1 để không bị lỗi "Parameter invalid"
                int width = Math.Max(1, parent.ClientRectangle.Width);
                int height = Math.Max(1, parent.ClientRectangle.Height);

                // Tạo ảnh bitmap
                _screenshot = new Bitmap(width, height);

                // Chụp giao diện Form cha
                parent.DrawToBitmap(_screenshot, new Rectangle(0, 0, width, height));

                // Phủ lớp màu đen mờ lên ảnh vừa chụp
                using (Graphics g = Graphics.FromImage(_screenshot))
                {
                    using (Brush brush = new SolidBrush(Color.FromArgb(100, 0, 0, 0))) // Màu đen mờ 40%
                    {
                        g.FillRectangle(brush, 0, 0, width, height);
                    }
                }
            }
            catch (Exception)
            {
                // Nếu chụp thất bại (do máy lag, form lỗi...), bỏ qua bước chụp ảnh.
                // Giải phóng biến ảnh nếu lỡ tạo ra rồi mà lỗi
                if (_screenshot != null)
                {
                    _screenshot.Dispose();
                    _screenshot = null;
                }
            }

            // --- BƯỚC 2: TẠO PANEL OVERLAY ---
            _overlay = new Guna2Panel
            {
                Dock = DockStyle.Fill,
                Name = "OverlayPanel"
            };

            // Nếu chụp thành công -> Dùng ảnh nền (Nhìn xuyên thấu)
            if (_screenshot != null)
            {
                _overlay.BackgroundImage = _screenshot;
            }
            else
            {
                // Nếu chụp thất bại -> Dùng màu đen thuần (Fallback an toàn)
                _overlay.BackColor = Color.Black;
            }

            // --- BƯỚC 3: HIỂN THỊ ---
            // Thêm vào Form cha và đưa lên trên (nhưng nằm dưới Popup)
            parent.Controls.Add(_overlay);
            _overlay.BringToFront();
        }

        public void Hide(Form parent)
        {
            if (_overlay == null) return;

            // Xóa overlay khỏi Form
            parent.Controls.Remove(_overlay);

            // Hủy đối tượng Overlay
            _overlay.Dispose();
            _overlay = null;

            // Hủy ảnh chụp màn hình để giải phóng RAM
            if (_screenshot != null)
            {
                _screenshot.Dispose();
                _screenshot = null;
            }
            FormMain.instance.UnlockNavigationButtons();
        }
    }
}
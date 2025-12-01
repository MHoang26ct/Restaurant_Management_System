using System.Windows.Forms;

namespace FoodOrderManagement
{
    public class DoubleBufferedTLP : TableLayoutPanel // Kế thừa từ TableLayoutPanel
    {
        public DoubleBufferedTLP()
        {
            // Bật DoubleBuffered cho chính nó
            this.DoubleBuffered = true; // Lưu nền cũ của tablelayout vào buffer 
        }
     }
}
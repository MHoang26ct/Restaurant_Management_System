using System.Windows.Forms;

namespace ProjectRestaurant
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
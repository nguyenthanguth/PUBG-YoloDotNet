using System.Runtime.InteropServices;

namespace GetKeys
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Task.Run(StartKeyboardEvent);
        }

        [DllImport("user32.dll")]
        private static extern short GetAsyncKeyState(Keys vKey);
        private async Task StartKeyboardEvent()
        {
            while (true)
            {
                // Duyệt qua tất cả các phím trong enum Keys
                foreach (Keys key in Enum.GetValues(typeof(Keys)))
                {
                    // Nếu phím đang được nhấn
                    if ((GetAsyncKeyState(key) & 0x8000) != 0)
                    {
                        Console.WriteLine($"Bạn vừa nhấn: {key}");

                        // Thoát nếu là phím Escape
                        if (key == Keys.Escape)
                            return;

                        //Thread.Sleep(200); // Chống spam nhiều lần liên tiếp
                    }
                }

                Thread.Sleep(10); // Giảm tải CPU
            }
        }
    }
}

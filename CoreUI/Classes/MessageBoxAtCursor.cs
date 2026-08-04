using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Day2eEditor
{
    public class MessageBoxAtCursor
    {
        [DllImport("user32.dll")]
        static extern bool SetWindowPos(
            IntPtr hWnd,
            IntPtr hWndInsertAfter,
            int X,
            int Y,
            int cx,
            int cy,
            uint uFlags);

        const uint SWP_NOSIZE = 0x0001;
        const uint SWP_NOZORDER = 0x0004;

        public static DialogResult Show(string text, string caption)
        {
            Point pos = Cursor.Position;

            System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
            timer.Interval = 10;
            timer.Tick += (s, e) =>
            {
                IntPtr hWnd = FindWindow("#32770", caption);
                if (hWnd != IntPtr.Zero)
                {
                    timer.Stop();

                    SetWindowPos(
        hWnd,
        IntPtr.Zero,
        pos.X,
        pos.Y,
        0,
        0,
        SWP_NOSIZE | SWP_NOZORDER);
                }
            };


            timer.Start();

            return MessageBox.Show(text, caption);
        }


        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
    }
}

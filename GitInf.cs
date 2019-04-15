using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Newbe.Mahua.Plugins.TestSerch
{
    public class GitInf : IGetInf
    {
        #region 获取另一系统文本框值
        [DllImport("User32.dll", EntryPoint = "FindWindow")]
        public extern static IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("User32.dll", EntryPoint = "FindWindowEx")]
        public static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpClassName, string lpWindowName);

        [DllImport("User32.dll", EntryPoint = "FindEx")]
        public static extern IntPtr FindEx(IntPtr hwnd, IntPtr hwndChild, string lpClassName, string lpWindowName);

        //[DllImport("user32.dll", EntryPoint = "GetWindowText")]
        //public static extern int GetWindowText(int hwnd, string lpString, int cch);



        [DllImport("User32.dll", EntryPoint = "SendMessage")]
        private static extern int SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, string lParam);

        [DllImport("user32.dll ", EntryPoint = "GetDlgItem")]
        public static extern IntPtr GetDlgItem(IntPtr hParent, int nIDParentItem);

        [DllImport("user32.dll", EntryPoint = "GetWindowText")]
        public static extern int GetWindowText(IntPtr hwnd, StringBuilder lpString, int cch);

        #endregion

        public Task StartAsync()
        {

            return Task.Run(() => watinf());
        }

        public Task StopAsync()
        {

            return Task.FromResult(0);
        }

        public Task MysqlStartAsync()
        {

            return Task.FromResult(0);
        }

        public void watinf()
        {
            IntPtr maindHwnd = FindWindow(null, "Form1"); //获得句柄   
            int i = 0;
            if (maindHwnd != IntPtr.Zero)
            {
                int controlId = 0x000003F4;
                IntPtr EdithWnd = GetDlgItem(maindHwnd, controlId);

                SendMessage(EdithWnd, i, (IntPtr)0, string.Format("当前时间是:{0}", DateTime.Now));

                StringBuilder stringBuilder = new StringBuilder(512);
                GetWindowText(EdithWnd, stringBuilder, stringBuilder.Capacity);
            }
        }
    }
}

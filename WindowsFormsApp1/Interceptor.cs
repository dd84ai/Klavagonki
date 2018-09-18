using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    using System;
    using System.Diagnostics;
    using System.Windows.Forms;
    using System.Runtime.InteropServices;


    //Low-Level Keyboard Hook in C#
    //http://blogs.msdn.com/toub/archive/2006/05/03/589423.aspx?CommentPosted=true#commentmessage

    //WindowsMessages (Enums)
    //http://www.pinvoke.net/default.aspx/Enums/WindowsMessages.html

    class Interceptor
    {
        private const int WH_KEYBOARD_LL = 13;
        private const int WM_KEYDOWN = 0x0100;
        private static LowLevelKeyboardProc _proc = HookCallback;
        private static IntPtr _hookID = IntPtr.Zero;

        public static void Start()
        {
            _hookID = SetHook(_proc);
            //Application.Run();
            //UnhookWindowsHookEx(_hookID);
        }

        private static IntPtr SetHook(LowLevelKeyboardProc proc)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                return SetWindowsHookEx(WH_KEYBOARD_LL, proc,
                    GetModuleHandle(curModule.ModuleName), 0);
            }
        }

        private delegate IntPtr LowLevelKeyboardProc(
            int nCode, IntPtr wParam, IntPtr lParam);

        private static IntPtr HookCallback(
            int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (LastThingToDo)
            {
                LastThingToDo = false;
                //System.Windows.Forms.SendKeys.SendWait("{Enter}");
            }

            if (nCode >= 0 && wParam == (IntPtr)WM_KEYDOWN)
            {
                int vkCode = Marshal.ReadInt32(lParam);
                Keys pressed = (Keys)vkCode;
                if (pressed == Keys.Space)
                    Console.Write(" ");
                //else if (pressed == Keys.Enter)
                //{
                //    Console.Write(Environment.NewLine);


                //}
                else if (pressed >= Keys.A && pressed <= Keys.Z)
                {
                    Console.Write(pressed);
                    if (pressed == Keys.Z)
                    {

                    }
                }
                else if (pressed == Keys.LShiftKey && Button.IsKeyLocked(Keys.Capital))
                {
                    Console.Write(pressed);
                    //System.Windows.Forms.SendKeys.SendWait("{Backspace}");
                    string text = "стройка постройка строка Кастро ростра растр створ раствор расстройство Корсика косвенно пристройка стрелка стремно страз паства Страдивари ответственность женственность листва Марсель гарсон трос торс рост сорт.";
                    //text = "";
                    foreach (var symbol in text)
                    {
                        System.Windows.Forms.SendKeys.SendWait(symbol.ToString());
                        System.Threading.Thread.Sleep(17);
                    }
                    LastThingToDo = true;
                    //System.Windows.Forms.SendKeys.SendWait(".");
                    //System.Windows.Forms.SendKeys.Send("{.}");
                    //System.Windows.Forms.SendKeys.SendWait("{Dot}");
                    System.Windows.Forms.SendKeys.SendWait("{Enter}");

                }
                else
                {
                    //Console.WriteLine(pressed.ToString());
                }
            }
            return CallNextHookEx(_hookID, nCode, wParam, lParam);
        }
        static bool LastThingToDo = false;
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook,
            LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode,
            IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);
    }
}

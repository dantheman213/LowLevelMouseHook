//
// MouseInputManager
//
// Author: Daniel Gillespie (2015)
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.InteropServices;

namespace MouseInputManager
{
    public class Win32
    {
        // DELEGATES
        public delegate IntPtr LowLevelMouseProcDelegate(Int32 nCode, IntPtr wParam, IntPtr lParam);

        // Globals / Constants
        public const Int32 WH_MOUSE_LL = 14;

        // Low-level hook imports

        // CallNextHookEx
        // https://msdn.microsoft.com/en-us/library/windows/desktop/ms644974(v=vs.85).aspx
        // Passes the hook information to the next hook procedure in the current hook chain. A hook procedure can call this function either before or after processing the hook information.
        [DllImport("User32.dll")]
        public static extern IntPtr CallNextHookEx(IntPtr hHook, Int32 nCode, IntPtr wParam, IntPtr lParam);

        // UnhookWindowsHookEx
        // https://msdn.microsoft.com/en-us/library/windows/desktop/ms644993(v=vs.85).aspx
        // Removes a hook procedure installed in a hook chain by the SetWindowsHookEx function
        [DllImport("User32.dll")]
        public static extern IntPtr UnhookWindowsHookEx(IntPtr hHook);

        // SetWinodwsHookEx
        // https://msdn.microsoft.com/en-us/library/windows/desktop/ms644990%28v=vs.85%29.aspx
        // Installs an application-defined hook procedure into a hook chain. You would install a hook procedure to monitor the system for certain types of events. These events are associated either with a specific thread or with all threads in the same desktop as the calling thread.
        [DllImport("User32.dll")]
        public static extern IntPtr SetWindowsHookEx(Int32 idHook, LowLevelMouseProcDelegate lpfn, IntPtr hmod, Int32 dwThreadId);

        // GetModuleHandle
        // https://msdn.microsoft.com/en-us/library/windows/desktop/ms683199%28v=vs.85%29.aspx
        // Retrieves a module handle for the specified module. The module must have been loaded by the calling process.
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr GetModuleHandle(string lpModuleName);

        // STRUCTURES

        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int X;
            public int Y;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct MSLLHOOKSTRUCT
        {
            public POINT pt;
            public int mouseData; // be careful, this must be ints, not uints
            public int flags;
            public int time;
            public UIntPtr dwExtraInfo;
        }

        // ENUMS

        public enum MouseMessages
        {
            WM_LBUTTONDOWN = 0x0201,
            WM_LBUTTONUP = 0x0202,
            WM_MOUSEMOVE = 0x0200,
            WM_MOUSEWHEEL = 0x020A,
            WM_RBUTTONDOWN = 0x0204,
            WM_RBUTTONUP = 0x0205
        }
    }
}

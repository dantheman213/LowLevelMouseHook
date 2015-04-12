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

using System.ComponentModel;
using System.Runtime.InteropServices;

namespace MouseInputManager
{
    public class MouseInput : IDisposable
    {
        public event EventHandler<MouseEventArgs> MouseMoved;

        private Win32.LowLevelMouseProcDelegate mouseEventDelegate;
        private IntPtr mouseHandle;

        private bool isDisposed = false;

        public MouseInput()
        {
            mouseEventDelegate = LowLevelMouseProc; // assign delegate event handler function
            mouseHandle = Win32.SetWindowsHookEx(Win32.WH_MOUSE_LL, mouseEventDelegate, Win32.GetModuleHandle("user32"), 0);

            if (mouseHandle == IntPtr.Zero)
                throw new Win32Exception("Unable to set mouse hook!");

        }

        // This is the event handler that is called from the low level API hook
        private IntPtr LowLevelMouseProc(Int32 nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode < 0)
                return Win32.CallNextHookEx(mouseHandle, nCode, wParam, lParam);

            if (MouseMoved != null && (Win32.MouseMessages)wParam == Win32.MouseMessages.WM_MOUSEMOVE) {
                // Only fire this event if the calling function assigned an event handler
                // also only fire event if a mouse move event was made
                Win32.MSLLHOOKSTRUCT hookStruct = (Win32.MSLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(Win32.MSLLHOOKSTRUCT));

                MouseEventArgs mouseArgs = new MouseEventArgs();
                mouseArgs.X = hookStruct.pt.X;
                mouseArgs.Y = hookStruct.pt.Y;

                MouseMoved(this, mouseArgs);
            }

            return Win32.CallNextHookEx(mouseHandle, nCode, wParam, lParam);
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!isDisposed) {
                if (mouseHandle != IntPtr.Zero)
                    Win32.UnhookWindowsHookEx(mouseHandle);

                isDisposed = true;
            }
        }

        ~MouseInput()
        {
            Dispose(false);
        }
    }
}

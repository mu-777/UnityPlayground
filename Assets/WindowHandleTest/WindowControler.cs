using System;
using System.Runtime.InteropServices;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowControler : MonoBehaviour
{
//    public static void ForceMoveWindow(Vector2Int leftUpPositionAtScreen, Vector2Int resolution)
//    {
//        var hWnd = GetWindowHandle();
//        var activeHWnd = User32.GetActiveWindow();

//#if !UNITY_EDITOR

//        User32.MoveWindow(hWnd,
//                          leftUpPositionAtScreen.x, leftUpPositionAtScreen.y,
//                          resolution.x, resolution.y, true);
//        //Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
//        //Screen.fullScreen = true;
//#endif
//    }

//    private static IntPtr GetWindowHandle()
//    {
//        var thisProcess = System.Diagnostics.Process.GetCurrentProcess();
//        var hWnd = User32.GetTopWindow(IntPtr.Zero);

//        while (hWnd != IntPtr.Zero)
//        {
//            if (User32.GetWindowLong(hWnd, -6) != 0)
//            {
//                continue;
//            }

//            int processId;
//            User32.GetWindowThreadProcessId(hWnd, out processId);
//            if (processId == thisProcess.Id)
//            {
//                return hWnd;
//            }
//            hWnd = User32.GetWindow(hWnd, 2);
//        }

//        return IntPtr.Zero;
//    }

//    private struct User32
//    {
//        [DllImport("user32.dll")]
//        public static extern IntPtr GetActiveWindow();

//        [DllImport("user32.dll")]
//        public static extern IntPtr GetTopWindow(IntPtr hWnd);

//        [DllImport("user32.dll")]
//        public static extern IntPtr GetWindow(IntPtr hWnd, uint wCmd);

//        [DllImport("user32")]
//        public static extern int GetWindowLong(IntPtr hWnd, int nIndex);

//        [DllImport("user32.dll", SetLastError = true)]
//        public static extern int GetWindowThreadProcessId(IntPtr hWnd, out int lpdwProcessId);

//        [DllImport("user32.dll", SetLastError = true)]
//        public static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);
//    }
}

using System;
using System.Runtime.InteropServices;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowHandleTestManager : MonoBehaviour
{
    void Awake()
    {
        var position = new Vector2Int(100, 100);
        var resolution = new Vector2Int(500, 500);

#if !UNITY_EDITOR
        ForceMoveWindow(position, resolution);
#endif
    }

    void Start()
    {
        ShowAllWindowHandle();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log(String.Format("{0}: {1}", "Active", User32.GetActiveWindow()));
            Debug.Log(String.Format("{0}: {1}", "GetSelf", GetSelfWindowHandle()));

            //ShowAllWindowHandle();
            //ShowAllWindowHandleWithEnumWindow();
        }
    }

    private void ShowWindowHandles(int targetProcessId)
    {
        Debug.Log(String.Format("{0}: {1}", "Active", User32.GetActiveWindow()));

        var cnt = 0;
        var hWnd = User32.GetTopWindow(IntPtr.Zero);
        while(hWnd != IntPtr.Zero)
        {
            int processId;
            User32.GetWindowThreadProcessId(hWnd, out processId);
            if(processId == targetProcessId)
            {
                var style = User32.GetWindowLong(hWnd, -16);
                var exStyle = User32.GetWindowLong(hWnd, -20);
                var parent = User32.GetWindowLong(hWnd, -8);
                Debug.Log(String.Format("{0}: {1}\n  Style: {2:X}, ExStyle: {3:X}, Parent: {4:X}",
                                        cnt++, hWnd, style, exStyle, parent));
            }
            hWnd = User32.GetWindow(hWnd, 2);
        }
    }


    private void ForceMoveWindow(Vector2Int leftUpPositionAtScreen, Vector2Int resolution)
    {
        var hWnd = GetSelfWindowHandle();
        var activeHWnd = User32.GetActiveWindow();

        var intHWnd = (int)hWnd;
        var intAciveHWnd = (int)activeHWnd;

        Debug.Log(String.Format("{0}, {1}", intHWnd, intAciveHWnd));

        Screen.fullScreen = false;
        Screen.fullScreenMode = FullScreenMode.Windowed;

        //User32.ShowWindow(hWnd, 5);
        //User32.MoveWindow(hWnd,
        //                  leftUpPositionAtScreen.x, leftUpPositionAtScreen.y,
        //                  resolution.x, resolution.y, true);
        User32.SetWindowPos(hWnd, -1,
                            leftUpPositionAtScreen.x, leftUpPositionAtScreen.y,
                            resolution.x, resolution.y, 0x0040);
        //Screen.fullScreen = true;
    }

    public static IntPtr GetSelfWindowHandle()
    {
        var wsVisible = 0x10000000;
        var thisProcess = System.Diagnostics.Process.GetCurrentProcess();
        var hWnd = User32.GetTopWindow(IntPtr.Zero);

        while(hWnd != IntPtr.Zero)
        {
            int processId;
            User32.GetWindowThreadProcessId(hWnd, out processId);
            if(processId == thisProcess.Id)
            {
                if((User32.GetWindowLong(hWnd, -16) & wsVisible) != 0)
                {
                    return hWnd;
                }
            }
            hWnd = User32.GetWindow(hWnd, 2);
        }
        return IntPtr.Zero;
    }

    private void ShowAllWindowHandle()
    {
        Debug.Log(String.Format("{0}: {1}", "Active", User32.GetActiveWindow()));

        var wsVisible = 0x10000000;
        var cnt = 0;
        var thisProcess = System.Diagnostics.Process.GetCurrentProcess();
        var hWnd = User32.GetTopWindow(IntPtr.Zero);

        while(hWnd != IntPtr.Zero)
        {
            int processId;
            User32.GetWindowThreadProcessId(hWnd, out processId);
            if(processId == thisProcess.Id)
            {
                var style = User32.GetWindowLong(hWnd, -16);
                var exStyle = User32.GetWindowLong(hWnd, -20);
                var parent = User32.GetWindowLong(hWnd, -8);
                Debug.Log(String.Format("n{0}: {1}\n  Style: {2:X}, ExStyle: {3:X}, Parent: {4:X}, IsVisible: {5}",
                                        cnt++, hWnd, style, exStyle, parent, style & wsVisible));
            }
            hWnd = User32.GetWindow(hWnd, 2);
        }
    }


    private void ShowAllWindowHandleWithEnumWindow()
    {
        User32.EnumWindows(new User32.EnumWindowsDelegate(EnumWindowCallBack), IntPtr.Zero);
    }

    private static bool EnumWindowCallBack(IntPtr hWnd, IntPtr lparam)
    {
        var thisProcess = System.Diagnostics.Process.GetCurrentProcess();
        var wsVisible = 0x10000000;

        int processId;
        User32.GetWindowThreadProcessId(hWnd, out processId);
        if(processId == thisProcess.Id)
        {
            var style = User32.GetWindowLong(hWnd, -16);
            var exStyle = User32.GetWindowLong(hWnd, -20);
            var parent = User32.GetWindowLong(hWnd, -8);
            Debug.Log(String.Format("{0}: {1}\n  Style: {2:X}, ExStyle: {3:X}, Parent: {4:X}, IsVisible: {5}",
                                    "EnumWindow", hWnd, style, exStyle, parent, style & wsVisible));
        }

        //次のウィンドウを検索
        return true;
    }

    private struct User32
    {
        [DllImport("user32.dll")]
        public static extern IntPtr GetActiveWindow();

        [DllImport("user32.dll")]
        public static extern IntPtr GetTopWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern IntPtr GetWindow(IntPtr hWnd, uint wCmd);

        [DllImport("user32")]
        public static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern int GetWindowThreadProcessId(IntPtr hWnd, out int lpdwProcessId);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll")]
        public static extern bool SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int x, int y, int cx, int cy, int uFlags);

        public delegate bool EnumWindowsDelegate(IntPtr hWnd, IntPtr lparam);

        [DllImport("user32.dll")]
        public extern static bool EnumWindows(EnumWindowsDelegate lpEnumFunc, IntPtr lparam);
    }

}

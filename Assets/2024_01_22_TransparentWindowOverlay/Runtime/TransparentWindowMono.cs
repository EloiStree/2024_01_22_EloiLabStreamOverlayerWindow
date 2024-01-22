using System;
using System.Runtime.InteropServices;
using UnityEngine;

public class TransparentWindowMono : MonoBehaviour
{

    [DllImport("user32.dll", SetLastError = true)]
    private static extern int GetWindowLong(IntPtr hWnd, int nIndex);


    [DllImport("user32.dll")]
    private static extern IntPtr GetActiveWindow();

    [DllImport("user32.dll")]
    private static extern int SetWindowLong(IntPtr hWnd, int nIndex, uint dwNewLong);

    [DllImport("user32.dll", SetLastError = true)]
    private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

    [DllImport("user32.dll")]
    private static extern int SetLayeredWindowAttributes(IntPtr hwnd, uint crKey, byte bAlpha, uint dwFlags);

    private const int GWL_EXSTYLE = -20;
    private const uint WS_EX_LAYERED = 0x00080000;
    private const uint WS_EX_TRANSPARENT = 0x00000020;
    private const uint LWA_COLORKEY = 0x00000001;
    const int LWA_ALPHA = 0x2;

    private IntPtr hWnd;

    public int m_leftPixel = 200;
    public int m_topPixel = 200;
    public int m_width = 512;
    public int m_height = 512;
    private const int GWL_STYLE = -16;


    private void Start()
    {
#if !UNITY_EDITOR_
        hWnd = GetActiveWindow();

        // Set the window style to layered and transparent
        SetWindowLong(hWnd, GWL_EXSTYLE, WS_EX_LAYERED | WS_EX_TRANSPARENT);

        // Set the window position to topmost
        SetWindowPos(hWnd, new IntPtr(-1), 0, 0, 0, 0, 0);

        // Set the transparency (adjust the alpha value as needed)
        SetLayeredWindowAttributes(hWnd, 0, 128, LWA_COLORKEY);
#endif
    }


    private void Update()
    {
        // Add your logic here for handling mouse interactions or other updates
    }



    // Import necessary functions from user32.dll
    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

   
    // Define the RECT structure for GetWindowRect function
    [StructLayout(LayoutKind.Sequential)]
    public struct RECT
    {
        public int Left;
        public int Top;
        public int Right;
        public int Bottom;
    }

    // Constants for SetWindowPos function
    const uint SWP_NOSIZE = 0x0001;
    const uint SWP_NOZORDER = 0x0004;
    const uint SWP_SHOWWINDOW = 0x0040;



    public string m_sourceVideo = "https://www.youtube.com/watch?v=RqgsGaMPZTw";

    [TextArea(0, 10)]
    public string m_devNote = "Don't forget too: "
        + "\n- Disable DXGI flip D3D11 in the Resolution andd presentation Player menu"
        + "\n- Set the camera color to 0 0 0 0 in the background";

    [ContextMenu("Open the video online of Code Monkey")]
    public void OpenVideoHelpSource() { Application.OpenURL(m_sourceVideo); }

}

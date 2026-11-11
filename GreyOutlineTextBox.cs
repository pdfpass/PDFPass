using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace PDFPass;

// Flat #d3d3d3 outline from the input-box reference.
// The reference photo is skewed, so the outline is a uniform 1px frame.
internal sealed class GreyOutlineTextBox : TextBox
{
    private const int WmNcPaint = 0x0085;
    private const int WmPaint = 0x000F;
    private static readonly Color Outline = ColorTranslator.FromHtml("#d3d3d3");

    public GreyOutlineTextBox()
    {
        BorderStyle = BorderStyle.FixedSingle;
    }

    protected override void WndProc(ref Message m)
    {
        base.WndProc(ref m);
        if (m.Msg is WmNcPaint or WmPaint)
            DrawOutline();
    }

    private void DrawOutline()
    {
        if (!IsHandleCreated || BorderStyle != BorderStyle.FixedSingle)
            return;

        var hdc = GetWindowDC(Handle);
        if (hdc == IntPtr.Zero)
            return;

        try
        {
            if (!GetWindowRect(Handle, out var window))
                return;

            var width = window.Right - window.Left;
            var height = window.Bottom - window.Top;
            if (width <= 1 || height <= 1)
                return;

            using var graphics = Graphics.FromHdc(hdc);
            graphics.SmoothingMode = SmoothingMode.None;
            using var brush = new SolidBrush(Outline);
            graphics.FillRectangle(brush, 0, 0, width, 1);
            graphics.FillRectangle(brush, 0, height - 1, width, 1);
            graphics.FillRectangle(brush, 0, 0, 1, height);
            graphics.FillRectangle(brush, width - 1, 0, 1, height);
        }
        finally
        {
            ReleaseDC(Handle, hdc);
        }
    }

    [DllImport("user32.dll")]
    private static extern IntPtr GetWindowDC(IntPtr hWnd);

    [DllImport("user32.dll")]
    private static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

    [DllImport("user32.dll")]
    private static extern bool GetWindowRect(IntPtr hWnd, out Rect lpRect);

    [StructLayout(LayoutKind.Sequential)]
    private struct Rect
    {
        public int Left;
        public int Top;
        public int Right;
        public int Bottom;
    }
}

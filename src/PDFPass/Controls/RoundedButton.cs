using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace PDFPass.Controls;

internal sealed class RoundedButton : Button
{
    private bool _hovered;
    private bool _pressed;

    public RoundedButton()
    {
        SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint
                                         | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        var graphics = e.Graphics;
        graphics.Clear(Parent?.BackColor ?? SystemColors.Control);
        graphics.SmoothingMode = SmoothingMode.AntiAlias;
        var bounds = new RectangleF(0.5f, 0.5f, Width - 1f, Height - 1f);
        using var outline = new GraphicsPath();
        outline.AddArc(bounds.Left, bounds.Top, bounds.Height, bounds.Height, 90, 180);
        outline.AddArc(bounds.Right - bounds.Height, bounds.Top, bounds.Height, bounds.Height, 270, 180);
        outline.CloseFigure();

        var fill = !Enabled ? SystemColors.Control
            : _pressed ? ControlPaint.Dark(BackColor, 0.08f)
            : _hovered ? ControlPaint.Light(BackColor, 0.08f) : BackColor;
        using var brush = new SolidBrush(fill);
        using var border = new Pen(ColorTranslator.FromHtml("#dcd9d2"));
        graphics.FillPath(brush, outline);
        graphics.DrawPath(border, outline);

        var flags = TextFormatFlags.SingleLine | TextFormatFlags.VerticalCenter;
        if (!ShowKeyboardCues)
            flags |= TextFormatFlags.HidePrefix;
        var textSize = TextRenderer.MeasureText(graphics, Text, Font, Size.Empty, flags);
        var gap = Image == null || string.IsNullOrEmpty(Text) ? 0 : 4;
        var imageWidth = Image?.Width ?? 0;
        var left = (Width - textSize.Width - imageWidth - gap) / 2;
        if (Image != null)
        {
            if (Enabled)
                graphics.DrawImageUnscaled(Image, left, (Height - Image.Height) / 2);
            else
                ControlPaint.DrawImageDisabled(graphics, Image, left, (Height - Image.Height) / 2, fill);
        }

        var textBounds = new Rectangle(left + imageWidth + gap, 0, textSize.Width, Height);
        TextRenderer.DrawText(graphics, Text, Font, textBounds, Enabled ? ForeColor : SystemColors.GrayText, flags);
        if (Focused && ShowFocusCues)
            ControlPaint.DrawFocusRectangle(graphics, Rectangle.Inflate(ClientRectangle, -8, -8), ForeColor, fill);
    }

    protected override void OnMouseEnter(System.EventArgs e)
    {
        _hovered = true;
        Invalidate();
        base.OnMouseEnter(e);
    }

    protected override void OnMouseLeave(System.EventArgs e)
    {
        _hovered = false;
        Invalidate();
        base.OnMouseLeave(e);
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Left)
            _pressed = true;
        Invalidate();
        base.OnMouseDown(e);
    }

    protected override void OnMouseUp(MouseEventArgs e)
    {
        _pressed = false;
        Invalidate();
        base.OnMouseUp(e);
    }

    protected override void OnKeyDown(KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Space)
        {
            _pressed = true;
            Invalidate();
        }

        base.OnKeyDown(e);
    }

    protected override void OnKeyUp(KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Space)
        {
            _pressed = false;
            Invalidate();
        }

        base.OnKeyUp(e);
    }

    protected override void OnLostFocus(System.EventArgs e)
    {
        _pressed = false;
        Invalidate();
        base.OnLostFocus(e);
    }
}
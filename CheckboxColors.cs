using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace PDFPass
{
    internal static class CheckboxColors
    {
        internal static void PaintCheckedBox(object? sender, PaintEventArgs e)
        {
            var checkBox = (CheckBox)sender!;
            if (!checkBox.Checked || !checkBox.Enabled)
                return;

            var size = CheckBoxRenderer.GetGlyphSize(e.Graphics, CheckBoxState.CheckedNormal);
            var bounds = new Rectangle(0, (checkBox.ClientSize.Height - size.Height) / 2, size.Width, size.Height);
            using var background = new SolidBrush(ColorTranslator.FromHtml("#f2b829"));
            using var pen = new Pen(ColorTranslator.FromHtml("#15130f"), Math.Max(1f, size.Width / 10f));
            e.Graphics.FillRectangle(background, bounds);
            e.Graphics.DrawLines(pen, new[]
            {
                new Point(bounds.Left + size.Width / 4, bounds.Top + size.Height / 2),
                new Point(bounds.Left + size.Width * 2 / 5, bounds.Top + size.Height * 2 / 3),
                new Point(bounds.Left + size.Width * 3 / 4, bounds.Top + size.Height / 3)
            });
        }
    }
}

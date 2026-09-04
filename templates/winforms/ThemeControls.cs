using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;
using System.ComponentModel;

namespace UiConceptDesign;

/// <summary>
/// ThemeControls.cs — 验证过的组件骨架（PillButton / CardPanel / SegmentedControl /
/// BadgeLabel / ToastPanel / OverlayDialog）。所有颜色/圆角/动效取自 <see cref="Theme"/>，
/// 禁止另写一套画法；改业务不改视觉。
/// </summary>
public static class ThemeControls
{
    public static readonly Color DangerBgHover = Color.FromArgb(0xFE, 0xF2, 0xF2); // #FEF2F2
    public static readonly Color SecondaryPressed = Color.FromArgb(0xD8, 0xD6, 0xD0);
}

/// <summary>胶囊按钮（Primary 炭黑实心 / Secondary track 灰底 / Ghost 描边 / Danger 白底红字）。</summary>
[ToolboxItem(true)]
public class PillButton : Control
{
    public enum ButtonKind { Primary, Secondary, Ghost, Danger }

    private ButtonKind _kind = ButtonKind.Primary;
    private bool _hover;
    private bool _pressed;
    private readonly System.Windows.Forms.Timer _anim = new() { Interval = 15 };
    private float _animT; // 0..1 悬停/按下过渡

    private Color _ghostBorder = Color.FromArgb(153, 255, 255, 255); // 白 60%

    public PillButton()
    {
        SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint |
                 ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
        Size = new Size(140, 48);
        Font = Theme.Button();
        ForeColor = Color.White;
        Cursor = Cursors.Hand;
        Text = "Button";
        _anim.Tick += (_, _) =>
        {
            _animT += 15f / Theme.TransitionMs; // 15ms/150ms
            if (_animT >= 1f) { _animT = 1f; _anim.Stop(); }
            Invalidate();
        };
    }

    [Category("外观")] public ButtonKind Kind { get => _kind; set { _kind = value; Invalidate(); } }

    protected override void OnMouseEnter(EventArgs e) { _hover = true; _anim.Start(); base.OnMouseEnter(e); }
    protected override void OnMouseLeave(EventArgs e) { _hover = false; _pressed = false; _anim.Start(); base.OnMouseLeave(e); }
    protected override void OnMouseDown(MouseEventArgs e) { _pressed = true; Invalidate(); base.OnMouseDown(e); }
    protected override void OnMouseUp(MouseEventArgs e) { _pressed = false; Invalidate(); base.OnMouseUp(e); }
    protected override void OnEnabledChanged(EventArgs e) { Invalidate(); base.OnEnabledChanged(e); }

    /// <summary>当前颜色（含 hover/pressed 过渡插值）。</summary>
    private (Color bg, Color fg, Color border) Palette()
    {
        var (bg, fg, border) = _kind switch
        {
            ButtonKind.Primary => (Theme.Ink, Color.White, Color.Transparent),
            ButtonKind.Secondary => (Theme.Track, Theme.Ink, Color.Transparent),
            ButtonKind.Ghost => (Color.Transparent, Color.White, _ghostBorder),
            _ => (Theme.Card, Theme.Error, Color.Transparent), // Danger = 白底红字
        };

        if (!_hover && !_pressed) return (bg, fg, border);

        (Color, Color, Color) h = (_kind) switch
        {
            ButtonKind.Primary => (_pressed ? Theme.InkPressed : Theme.InkHover, Color.White, Color.Transparent),
            ButtonKind.Secondary => (_pressed ? ThemeControls.SecondaryPressed : Theme.TrackHover, Theme.Ink, Color.Transparent),
            ButtonKind.Ghost => (_pressed ? Color.FromArgb(40, 255, 255, 255) : Color.FromArgb(20, 255, 255, 255), Color.White, Color.White),
            _ => (_pressed ? Theme.Track : ThemeControls.DangerBgHover, Theme.Error, Color.Transparent),
        };
        return (Theme.Lerp(bg, h.Item1, _animT), Theme.Lerp(fg, h.Item2, _animT), Theme.Lerp(border, h.Item3, _animT));
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        var g = e.Graphics;
        g.SmoothingMode = SmoothingMode.AntiAlias;
        g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

        var rect = new RectangleF(0, 0, Width - 1, Height - 1);
        var (bg, fg, border) = Palette();
        using var path = Theme.Capsule(rect);

        using (var b = new SolidBrush(bg)) g.FillPath(b, path);
        if (_kind == ButtonKind.Ghost && border.A > 0)
            using (var p = new Pen(border, 1.5f))
                g.DrawPath(p, path);
        if (!Enabled)
        {
            TextRenderer.DrawText(g, Text, Font, Rectangle.Round(rect), Theme.Weak,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
            return;
        }
        TextRenderer.DrawText(g, Text, Font, Rectangle.Round(rect), fg,
            TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
    }
}

/// <summary>白卡 r-24（无描边，唯一轻阴影档）。</summary>
[ToolboxItem(true)]
public class CardPanel : Panel
{
    public CardPanel()
    {
        SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint |
                 ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
        BackColor = Color.White;
        Padding = new Padding(32);
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        var g = e.Graphics;
        g.SmoothingMode = SmoothingMode.AntiAlias;
        var rect = new RectangleF(8, 6, Width - 17, Height - 13);
        using var path = Theme.RoundRect(rect, Theme.RadiusCard);
        using var s = new Pen(Theme.Shadow, 2f) { LineJoin = LineJoin.Round };
        g.DrawPath(s, path);
    }
}

/// <summary>分段控件：track 底大胶囊（高 56），选中项白胶囊浮起（微阴影）。</summary>
[ToolboxItem(true)]
public class SegmentedControl : Control
{
    public class Item
    {
        public string Text { get; set; } = "";
        public string Glyph { get; set; } = ""; // Segoe MDL2 Assets 字形，可空
    }

    private readonly List<Item> _items = new();
    private int _selected;

    public SegmentedControl()
    {
        SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint |
                 ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
        Size = new Size(360, 56);
        Font = new Font(Theme.FontCJK, Theme.Pt(12), FontStyle.Bold);
        Cursor = Cursors.Hand;
    }

    [Browsable(false)] public IReadOnlyList<Item> Items => _items;
    [Category("外观")] public int SelectedIndex { get => _selected; set { _selected = value; Invalidate(); } }

    public void AddItem(string text, string glyph = "")
    {
        _items.Add(new Item { Text = text, Glyph = glyph });
        Invalidate();
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        var g = e.Graphics;
        g.SmoothingMode = SmoothingMode.AntiAlias;
        g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

        var rect = new RectangleF(0, 0, Width, Height);
        using (var track = Theme.Capsule(rect))
        using (var b = new SolidBrush(Theme.Track))
            g.FillPath(b, track);

        int n = _items.Count;
        if (n == 0) return;
        float w = rect.Width / n;
        for (int i = 0; i < n; i++)
        {
            var cell = new RectangleF(rect.X + i * w + 4, rect.Y + 4, w - 8, rect.Height - 8);
            if (i == _selected)
            {
                using var pill = Theme.Capsule(cell);
                using (var b = new SolidBrush(Theme.Card)) g.FillPath(b, pill);
                using var s = new Pen(Theme.ShadowFloat, 1.5f) { LineJoin = LineJoin.Round };
                g.DrawPath(s, pill);
            }
            var fg = i == _selected ? Theme.Ink : Theme.Sub;
            string text = string.IsNullOrEmpty(_items[i].Glyph)
                ? _items[i].Text
                : _items[i].Glyph + " " + _items[i].Text;
            TextRenderer.DrawText(g, text, Font, Rectangle.Round(cell), fg,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
        }
    }

    protected override void OnMouseUp(MouseEventArgs e)
    {
        if (_items.Count > 0)
        {
            int idx = (int)(e.X / ((float)Width / _items.Count));
            if (idx >= 0 && idx < _items.Count) SelectedIndex = idx;
        }
        base.OnMouseUp(e);
    }
}

/// <summary>标签：浅色卡 = 胶囊；深色卡 <paramref name="chip"/> = 12px 圆角（同色系提亮由调用方传色）。</summary>
[ToolboxItem(true)]
public class BadgeLabel : Control
{
    private bool _chip;
    private Color _bg = Color.FromArgb(0xE8, 0xED, 0xFF); // #E8EDFF
    private Color _fg = Theme.Accent;

    public BadgeLabel()
    {
        SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint |
                 ControlStyles.OptimizedDoubleBuffer, true);
        Font = new Font(Theme.FontCJK, Theme.Pt(11), FontStyle.Bold);
        Size = new Size(72, 24);
    }

    [Category("外观")] public bool Chip { get => _chip; set { _chip = value; Invalidate(); } }
    [Category("外观")] public Color BadgeBackColor { get => _bg; set { _bg = value; Invalidate(); } }
    [Category("外观")] public Color BadgeForeColor { get => _fg; set { _fg = value; Invalidate(); } }

    protected override void OnPaint(PaintEventArgs e)
    {
        var g = e.Graphics;
        g.SmoothingMode = SmoothingMode.AntiAlias;
        g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
        var rect = new RectangleF(0, 0, Width, Height);
        using var path = Theme.RoundRect(rect, _chip ? Theme.RadiusChip : rect.Height / 2);
        using (var b = new SolidBrush(_bg)) g.FillPath(b, path);
        TextRenderer.DrawText(g, Text, Font, Rectangle.Round(rect), _fg,
            TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
    }
}

/// <summary>Toast：白底 pill r-16，左侧彩色圆点 + ink 文字，2.5s 自动关闭。</summary>
public sealed class ToastPanel : Form
{
    private readonly Color _dot;

    public ToastPanel(string message, Color dot)
    {
        _dot = dot;
        FormBorderStyle = FormBorderStyle.None;
        ShowInTaskbar = false;
        BackColor = Theme.Card;
        DoubleBuffered = true;
        var sz = TextRenderer.MeasureText(message, Theme.Body());
        Size = new Size(sz.Width + 48, 44);
        Text = message;
        var life = new System.Windows.Forms.Timer { Interval = Theme.ToastMs };
        life.Tick += (_, _) => { life.Stop(); Close(); };
        life.Start();
    }

    protected override void OnShown(EventArgs e)
    {
        base.OnShown(e);
        Region = new Region(Theme.RoundRect(
            new RectangleF(0, 0, Width - 1, Height - 1), 16));
        // 右上角浮出
        if (Owner != null)
            Location = new Point(Owner.Right - Width - 24, Owner.Top + 24);
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        var g = e.Graphics;
        g.SmoothingMode = SmoothingMode.AntiAlias;
        g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
        using (var b = new SolidBrush(_dot))
            g.FillEllipse(b, 16, Height / 2f - 4, 8, 8);
        TextRenderer.DrawText(g, Text, Theme.Body(),
            new Rectangle(32, 0, Width - 40, Height), Theme.Ink,
            TextFormatFlags.VerticalCenter | TextFormatFlags.NoPadding);
    }
}

/// <summary>
/// 遮罩弹窗：半透明遮罩（近白）+ 白卡 r-24 居中。
/// 用法：var dlg = new OverlayDialog(); dlg.BodyPanel.Controls.Add(...); dlg.ShowDialog(this);
/// </summary>
public class OverlayDialog : Form
{
    private readonly Panel _body = new();

    public OverlayDialog()
    {
        FormBorderStyle = FormBorderStyle.None;
        StartPosition = FormStartPosition.CenterScreen;
        ShowInTaskbar = false;
        DoubleBuffered = true;
        // 半透明遮罩：顶层用 97% 近白（rgba(245,244,241,.7) 的 WinForms 近似实现）
        BackColor = Theme.OverlayLight;
        Opacity = 0.97;

        _body.BackColor = Theme.Card;
        _body.Size = new Size(420, 280);
        _body.Paint += (_, pe) =>
        {
            var g = pe.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            using var path = Theme.RoundRect(
                new RectangleF(0, 0, _body.Width - 1, _body.Height - 1), Theme.RadiusCard);
            using (var b = new SolidBrush(Theme.Card)) g.FillPath(b, path);
            using var s = new Pen(Theme.Shadow, 2f) { LineJoin = LineJoin.Round };
            g.DrawPath(s, path);
        };
        Controls.Add(_body);
        Resize += (_, _) => CenterBody();
    }

    public Panel BodyPanel => _body;

    protected override void OnLayout(LayoutEventArgs levent) { base.OnLayout(levent); CenterBody(); }

    private void CenterBody()
    {
        _body.Location = new Point((ClientSize.Width - _body.Width) / 2,
                                   (ClientSize.Height - _body.Height) / 2);
    }
}




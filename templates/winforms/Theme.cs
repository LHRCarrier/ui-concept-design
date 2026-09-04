using System.Drawing;
using System.Drawing.Drawing2D;

namespace UiConceptDesign;

/// <summary>
/// Theme.cs — 色板 / 字体 / 尺寸令牌（tokens/design-tokens.md 的唯一代码来源）。
/// 所有值来自本仓库内置设计参考帧；禁止在此自造颜色。
/// 单位约定：WinForms 坐标 = px（96dpi）；Font 用 pt（px ÷ 96 × 72 = pt）。
/// </summary>
public static class Theme
{
    // ── §1 色彩（浅色为默认主题）──────────────────────────────
    public static readonly Color Paper = Hex("#F5F4F1"); // 应用主背景（米白纸面）
    public static readonly Color PaperGrid = Hex("#EAE8E3"); // 背景极淡网格线（可选肌理）
    public static readonly Color Card = Hex("#FFFFFF"); // 卡片面，纯白
    public static readonly Color Ink = Hex("#1C1C1A"); // 主文字 + Primary 按钮实心（炭黑）
    public static readonly Color InkPressed = Hex("#000000"); // Primary 按下态
    public static readonly Color InkHover = Hex("#333330"); // Primary 悬停态
    public static readonly Color Sub = Hex("#6F6F6A"); // 次级文字
    public static readonly Color Weak = Hex("#A6A6A0"); // 弱文字（占位/时间戳/标签）
    public static readonly Color Track = Hex("#ECEAE5"); // 分段控件/Ghost 按钮浅灰轨道底
    public static readonly Color TrackHover = Hex("#E2E0DA"); // Secondary 悬停底
    public static readonly Color Accent = Hex("#2F6BFF"); // 唯一点缀色：价格/链接/关键数字
    public static readonly Color Star = Hex("#FFC83D"); // 收藏/评分星标
    public static readonly Color Success = Hex("#16A34A"); // 成功态
    public static readonly Color Error = Hex("#DC2626"); // 错误/危险
    public static readonly Color Divider = Hex("#EFEDE8"); // 极浅分隔线（尽量不用）

    // 线稿插画点缀绿（landing hero 的气球吊篮 / 日历色块，仅用于插画，禁止用于按钮/文字）
    public static readonly Color PopGreen = Hex("#A8E05F");

    // ── 有色深色卡片（每屏最多 1 张）───────────────────────────
    public static readonly Color DarkNavy = Hex("#232044");
    public static readonly Color DarkPurple = Hex("#3A2440");
    public static readonly Color DarkGreen = Hex("#1E2B26");
    public static readonly Color DarkTeal = Hex("#16303A");

    // 深色卡 badge = 卡片色提亮同色系：底 / 字
    public static readonly Color DarkBadgeBg = Hex("#4C3A6E");
    public static readonly Color DarkBadgeText = Hex("#B9A8FF");

    // 遮罩
    public static readonly Color OverlayLight = Color.FromArgb(178, 245, 244, 241); // rgba(245,244,241,.7)
    public static readonly Color OverlayDark = Color.FromArgb(89, 28, 28, 26); // rgba(28,28,26,.35)

    // ── §2 圆角 ────────────────────────────────────────────
    public const int RadiusCard = 24;
    public const int RadiusInner = 18;
    public const int RadiusChip = 12;

    // ── §5 阴影：唯一一档，可省略 ─────────────────────────────
    public static readonly Color Shadow = Color.FromArgb(15, 28, 28, 26); // 0 8 24 rgba(28,28,26,.06)
    public static readonly Color ShadowFloat = Color.FromArgb(20, 28, 28, 26); // 0 2 8 rgba(28,28,26,.08)

    // ── §6 动效 ────────────────────────────────────────────
    public const int TransitionMs = 150; // hover/pressed 过渡 120–150ms
    public const int ToastMs = 2500; // toast 淡出

    // ── §3 字体（西文 Segoe UI / 中文 微软雅黑）─────────────────
    public const string FontLatin = "Segoe UI";
    public const string FontCJK = "Microsoft YaHei UI";

    /// <summary>px → pt（WinForms Font 用 pt）。</summary>
    public static float Pt(float px) => px * 72f / 96f;

    public static Font Display() => new(FontCJK, Pt(32), FontStyle.Bold, GraphicsUnit.Point); // 28–34pt
    public static Font Title() => new(FontCJK, Pt(17), FontStyle.Bold, GraphicsUnit.Point); // 16–18pt
    public static Font Body() => new(FontCJK, Pt(10), FontStyle.Regular, GraphicsUnit.Point); // 9.5–10.5pt
    public static Font Caption() => new(FontCJK, Pt(8.5f), FontStyle.Regular, GraphicsUnit.Point); // 8–9pt
    public static Font Stat() => new(FontLatin, Pt(22), FontStyle.Bold, GraphicsUnit.Point); // 20–24pt
    public static Font Button() => new(FontCJK, Pt(10), FontStyle.Bold, GraphicsUnit.Point);

    // ── helpers ─────────────────────────────────────────────
    public static Color Hex(string hex)
    {
        return ColorTranslator.FromHtml(hex);
    }

    /// <summary>线性插值（hover 150ms 颜色过渡用的基本插值器）。</summary>
    public static Color Lerp(Color a, Color b, float t)
    {
        t = Math.Clamp(t, 0f, 1f);
        return Color.FromArgb(
            (int)(a.A + (b.A - a.A) * t),
            (int)(a.R + (b.R - a.R) * t),
            (int)(a.G + (b.G - a.G) * t),
            (int)(a.B + (b.B - a.B) * t));
    }

    /// <summary>圆角路径（卡片/按钮/胶囊共用）。</summary>
    public static GraphicsPath RoundRect(RectangleF r, float radius)
    {
        var path = new GraphicsPath();
        float d = radius * 2f;
        path.AddArc(r.X, r.Y, d, d, 180, 90);
        path.AddArc(r.Right - d, r.Y, d, d, 270, 90);
        path.AddArc(r.Right - d, r.Bottom - d, d, d, 0, 90);
        path.AddArc(r.X, r.Bottom - d, d, d, 90, 90);
        path.CloseFigure();
        return path;
    }

    /// <summary>胶囊路径（r = 高/2，全圆角）。</summary>
    public static GraphicsPath Capsule(RectangleF r)
    {
        return RoundRect(r, r.Height / 2f);
    }
}

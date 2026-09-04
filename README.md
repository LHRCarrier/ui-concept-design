# UI Concept Design

模板驱动的现代桌面 UI 设计系统 —— 明亮通透、大圆角、胶囊按钮风。

设计参考帧（`references/`）、精确设计令牌（`tokens/`）与代码模板（`templates/`、`examples/`）三位一体：
**只凭抽象原则写出来的 UI 一定平庸，必须对着参考帧和 tokens 做。**

## 效果展示

使用本系统实现的完整页面（源码见 `examples/web/`，单文件可运行）：

**Landing 页** —— 双侧手绘线稿插画 hero、胶囊按钮（hover 颜色插值）、信任条、统计数据行、分段控件、点线时间轴列表卡、toast 微交互

![Landing 示例](https://github.com/LHRCarrier/ui-concept-design/raw/master/assets/screenshot-landing.png)

**Speaking Practice 页** —— 56px 分段控件、深藏青推荐卡（chip badge + 幽灵按钮 + 光晕）、场景卡网格（实色图标块）、统计行

![Practice 示例](https://github.com/LHRCarrier/ui-concept-design/raw/master/assets/screenshot-practice.png)

## 特性

- **精确设计令牌**：色板 / 圆角 / 字号 / 间距 / 组件规格全部查表取值，禁止"差不多"。
- **7 张内置参考帧**：覆盖首页英雄区、分段控件、统计卡、时间线卡、图片卡、深色展示卡等主要界面形态，动手前必看。
- **组件决策树**：要做什么 → 用哪个模板 / 参考帧 / 组件，减少空白区的即兴发挥。
- **WinForms 代码模板**：`Theme.cs`（唯一色板来源）+ 组件骨架（PillButton / CardPanel / SegmentedControl / Badge / Toast / 遮罩弹窗），已通过编译与运行时渲染验证。
- **完整源码示例**：`examples/web/` 下两个单文件页面（landing / practice），含微交互动效，全部值来自 tokens；`examples/README.md` 提供图标库与动效库的开源选型。
- **验收清单**：8 条可自检的问答，全部答"是"才算完成。

## 目录结构

```
├── SKILL.md                  # 设计规范主体：硬约束 / 概念速查 / 工作流程 / 决策树 / 验收清单
├── references/               # 7 张内置设计参考帧（视觉正本）
├── tokens/
│   └── design-tokens.md      # 精确色值 / 圆角 / 字号 / 间距 / 组件规格
├── templates/
│   └── winforms/
│       ├── Theme.cs          # 色板与字体令牌代码（WinForms 唯一来源）
│       └── ThemeControls.cs  # 组件代码骨架（PillButton / CardPanel / SegmentedControl / Badge 等）
├── examples/
│   ├── README.md             # 图标库选型（Lucide/Tabler/Phosphor）与动效库（Motion/Magic UI/Hover.css）
│   └── web/
│       ├── landing.html      # 单文件可运行的 Landing 示例
│       └── practice.html     # 单文件可运行的练习页示例
└── assets/                   # README 效果展示截图
```

## 快速上手

1. **看参考帧**：打开 `references/` 中与当前界面最像的 1-2 张，记住圆角、留白、按钮形状、文字层级。
2. **锁 tokens**：从 `tokens/design-tokens.md` 抄色板 / 圆角 / 字号（WinForms 抄到 `Theme.cs`，Web 写成 CSS 变量），不改值。
3. **画视觉锚点**：先画线稿插画 / 大幅图形锚点（气球、日历、星星…），再在上面摆组件。
4. **套组件模板**：WinForms 从 `templates/winforms/` 复制组件骨架起步；Web 直接按 tokens 写 CSS。
5. **跑起来截图**，与参考帧并排对比（圆角够大吗？按钮是胶囊吗？有插画锚点吗？留白够吗？）。
6. **不达标就回到第 1 步**。

## 参考帧说明

本项目 `references/` 下的图片**仅作为本设计系统的内置风格参考**，展示了本系统追求的视觉语言

（米白纸面、白色大圆角卡片、炭黑胶囊按钮、克制的蓝黄点缀、手绘线稿插画）。这些帧为公开设计素材的

**局部截取，仅用于风格学习与对照**，不包含任何第三方品牌标识。若你的项目需要发布或再分发这些图片，

请自行确认原素材的许可与合规性；如不放心，可在发布时移除 `references/` 目录（设计令牌与规范不受影响）。

## License

[MIT](LICENSE)

---

*由 [LHRCarrier](https://github.com/LHRCarrier) 维护。*

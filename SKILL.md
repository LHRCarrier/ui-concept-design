---
name: "ui-concept-design"
description: "模板驱动的现代 UI 设计系统（桌面/移动/Web 原型通用）：以内置设计参考帧为视觉正本，配合精确 design tokens 与可选的代码模板（WinForms），生成明亮通透、大圆角胶囊风的界面。构建/改版任何 UI 原型时必须使用。"
---

# UI Concept Design — 现代界面品味法典

模板驱动的现代 UI 设计系统，**适用于桌面 / 移动 / Web 任何形态的界面原型**（`references/` 提供内置视觉参考帧）。**只凭抽象原则写出来的 UI 一定平庸，必须对着参考帧和 tokens 做。**

## 零、硬约束（违反任意一条必须返工）

1. **必须先打开 `references/` 里的参考帧看，再动手。** 它们是视觉正本：浅色纸面、白色大圆角卡片、炭黑胶囊按钮、克制的点缀色。做完后必须截图对比参考帧自检。
2. **所有取值必须来自 `tokens/design-tokens.md`。** 色值、圆角、字号、间距都查表，禁止自造"差不多的"颜色或圆角。特别禁止：小圆角（≤8px）按钮、直角卡片、彩色渐变、粗描边堆叠、深色灰蓝"程序员主题"。
3. **帧里的大幅线稿插画不是装饰，是视觉锚点。** 每个主视图必须至少有一处手绘线稿插画/大幅图形（气球、日历、星星那种 2.5px 黑线 + 单色填涂），这是"像设计成品"和"像普通后台"的分界线。插画用 ink 线 + pop-green 点缀，禁止彩色渐变插画。
4. **尺度宁大勿小。** 参考帧的留白和字号都往"大气"走：Display 至少 48px（Web 56px）、统计数字 32px+、按钮高 48px、分段控件高 56px；页面边距 40px+。做出来小里小气 = 不及格。
5. **代码模板只是可选项。** `templates/winforms/` 仅供 WinForms/C# 项目套用（`Theme.cs` 是唯一色板来源，`PillButton`/`CardPanel`/`SegmentedControl`/`BadgeLabel` 是验证过的组件骨架，允许扩展禁止另写一套画法）；**Web/移动/其他平台直接照 tokens 写 CSS/Styles，不套 WinForms 代码**——模板与平台无关，tokens 才是唯一真源。
6. **默认浅色主题。** 深色只允许两种形式：图片预览浮层（近黑衬托内容）、有色深色展示卡（深藏青/深紫/深绿，每屏最多一张）。禁止大面积灰蓝深色界面。

## 一、设计概念速查（11 个核心概念 + 补记规则 → 落地方式）

| 概念 | 落地规则 | 参考帧 |
|---|---|---|
| 可供性 Affordance | 按钮=胶囊实心/灰底，输入框=胶囊浅灰底，可点必 hovered 变色 | ref-segmented-pill |
| 视觉层级 Hierarchy | Display 大标题 → Title → Body → Caption；统计数字 Stat Bold 最大 | ref-profile-card-stats |
| 网格间距 Spacing | 8px 网格，页面边距 32，卡内边距 24，**用留白分组不用线** | ref-landing-hero-light |
| 排版 Typography | 双字体栈（Segoe UI + 微软雅黑），每视图 ≤3 种字号字重 | tokens §3 |
| 色彩 Color | paper/card/ink 三级中性 + accent 蓝唯一点缀 + star 黄收藏 | tokens §1 |
| 深色模式 Dark | 有色深色展示卡，同色系提亮 badge，每屏 ≤1 张 | ref-dark-colored-cards |
| 阴影 Shadow | 只有 `0 8 24 rgba(28,28,26,.06)` 一档，可省略；禁止明显投影 | ref-card-light-timeline |
| 图标按钮 Button | 线性图标+文字；图标-only 按钮必须带 Tooltip；全部 pill | ref-segmented-pill |
| 反馈状态 States | Hover/Pressed/Disabled/Loading/Focus 五态齐全，120-150ms 过渡 | templates |
| 微交互 Motion | 悬停颜色插值、白胶囊选中浮起；动画 ≤200ms 不弹跳 | templates |
| 遮罩 Overlay | 浅色半透明遮罩 + 白卡 r-24 弹窗；预览图可用近黑浮层 | ref-image-card-overlay |
| **(补) 插画 Illustration** | 手绘线稿（2.5px ink 线，圆头）为视觉锚点：气球/日历/星星/旋律线；单色填涂 pop-green，每屏 1-2 处；英雄区两侧对称构图 | ref-landing-hero-light |
| **(补) 尺度 Scale** | Display 48-56px、Stat 32px+、按钮 48px 高、分段 56px 高；卡片内边距 32；留白"多到怀疑人生"为止 | ref-portfolio-grid |
| **(补) 信任条 Trust bar** | hero 下方一排灰色品牌字（弱化 60%）+ caption 文案，视觉压舱 | ref-landing-hero-light |
| **(补) 点线时间轴 Dotted timeline** | 列表卡/记录卡用竖向点线串联图标块，点=当前位置，线=轨迹 | ref-card-light-timeline |

## 二、工作流程（每次构建/改版都走这六步）

1. **看参考帧**：打开 `references/` 中与当前界面最像的 1-2 张，记住它的圆角、留白、按钮形状、文字层级。
2. **锁 tokens**：从 `tokens/design-tokens.md` 抄色板/圆角/字号到代码（WinForms 抄到 `Theme.cs`，Web/移动写成 CSS 变量），不改值。
3. **画视觉锚点**：先画插画/图形锚点（线稿气球、日历、星星…），再在上面摆组件——**插画先于布局**，没有锚点的页面不许定稿。
4. **套组件模板**：从 `templates/winforms/ThemeControls.cs` 复制 PillButton、CardPanel 等骨架，改业务不改视觉。
5. **组页面**：先放主焦点（每屏一个主操作），再按 8px 网格摆间距，最后检查留白是否够"空"。
6. **跑起来截图**，和参考帧并排对比：圆角够大吗？按钮是胶囊吗？有插画锚点吗？点缀色克制吗？留白够吗？
7. **不达标就回到第 1 步**。验收标准见第四节。

## 三、组件决策树（要做什么 → 用哪个模板/参考帧）

- **主操作按钮**（生成、保存）→ PillButton Primary（炭黑实心）｜ref-profile-card-stats
- **次操作**（取消、引用）→ **纸面上 = 白底 1.5px ink 描边胶囊**（"View Leaderboard"）｜ref-landing-hero-light；**白卡上 = track 灰底**（"Message"）｜ref-profile-card-stats —— 两种都对，按底色选
- **视图切换 / 尺寸切换**（2-5 个互斥项）→ SegmentedControl 白胶囊浮起，**高 56px、字号 16px+、图标 24px**（参考帧的分段控件很大，别做小）｜ref-segmented-pill
- **列表卡片**（素材、项目）→ CardPanel r-24 白卡，**左图标块 = 品牌色实底 + 白色线性图标**（Burrito Bowl 的深棕块，不是浅色底彩图标！）+ 粗标题 + sub 副文 + 右侧关键值｜ref-card-light-timeline
- **统计数字**（数量、评分）→ Caption weak 标签在上 + Stat Bold 大数字在下，**数字 32px+**｜ref-profile-card-stats
- **状态标签**（new、已完成）→ BadgeLabel 彩色 15% 底 + 同色深字，**浅色卡上 = 胶囊；深色卡上 = 12px 圆角 chip（不是胶囊）**｜ref-dark-colored-cards
- **图片展示** → 大图 r-18 内嵌圆角，hover 显示操作；大图预览=近黑浮层+幽灵描边按钮｜ref-image-card-overlay
- **深色展示卡**（仅封面/重点推荐）→ tokens §1 有色深色卡，同色系 badge（chip）+ 微光晕，**卡内插图用产品照片或插画，别空着**｜ref-dark-colored-cards
- **危险操作**（删除）→ PillButton 白底 error 红字（不是红底！），hover 才 `#FEF2F2` 浅红底
- **空状态** → 线性简笔图标（weak 色 1.5px 线）+ Title 提示 + Caption 引导语，垂直居中
- **多个有先后关系的记录/步骤** → 竖向点线时间轴：图标块搁在点线上，下一条连接｜ref-card-light-timeline
- **hero/首页** → 两侧对称大幅线稿插画 + 居中大标题 + 纸面白描边副按钮 + 底部品牌信任条｜ref-landing-hero-light

## 四、验收清单（全部答"是"才算完成）

1. 主背景是 `paper` 米白，卡片纯白 r-24，无描边或极轻阴影？
2. 每个按钮都是胶囊（全圆角）？主按钮是炭黑不是彩色？
3. 彩色只有 accent 蓝 / star 黄 / 语义色，且总面积 <10%？
4. 文字层级 ≤3 级，大标题够大够粗（Display ≥48px / Stat ≥32px）？
5. 间距全是 8 的倍数，相关元素近、无关元素远？
6. 每个可交互元素有 hover/pressed/disabled 反馈，过渡 ≤150ms？
7. **有手绘线稿插画/图形锚点吗？图标块是实色底白图标吗？**（参考帧的核心特征，不是"干干净净"就够）
8. 截图和参考帧放一起看，风格一致、不"程序员风"？

## 五、文件索引

- `references/*.webp` — 内置设计参考帧（视觉正本，动手前必看）
- `tokens/design-tokens.md` — 精确色值/圆角/字号/间距/组件规格
- `templates/winforms/Theme.cs` — 色板与字体令牌代码（**WinForms 专用可选模板**；其他平台直接照 tokens 写 CSS）
- `templates/winforms/ThemeControls.cs` — PillButton / CardPanel / SegmentedControl / Badge / Toast / 遮罩弹窗 代码骨架（**WinForms 专用可选模板**；其他平台跳过）
- `examples/README.md` — 开源素材索引：图标库选型（Lucide/Tabler/Phosphor）与动效库（Motion/Magic UI/Hover.css）选用规则
- `examples/web/landing.html` — **完整 Web 源码示例**（单文件可运行）：hero 插画 + 胶囊按钮 + 分段控件 + 统计行 + 列表卡 + toast，全部值来自 tokens

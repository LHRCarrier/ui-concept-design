# UI Concept Design

模板驱动的现代 UI 设计系统（桌面 / 移动 / Web 原型通用）—— 明亮通透、大圆角、胶囊按钮风。

设计参考帧（`references/`）、精确设计令牌（`tokens/`）与代码示例（`examples/`、`templates/`）三位一体：
**只凭抽象原则写出来的 UI 一定平庸，必须对着参考帧和 tokens 做。**

> 定位说明：本系统是**面向原型设计的**——不管你用 Web、移动端还是桌面端做原型，视觉语言都是一套；
> WinForms 只是附带的可选代码模板之一，不是本系统的范围限定。

## 效果展示

使用本系统实现的完整页面（源码见 `examples/web/`，单文件可运行）：

**Landing 页** —— 双侧手绘线稿插画 hero、胶囊按钮（hover 颜色插值）、信任条、统计数据行、分段控件、点线时间轴列表卡、toast 微交互

![Landing 示例](https://github.com/LHRCarrier/ui-concept-design/raw/master/assets/screenshot-landing.png)

**Speaking Practice 页** —— 56px 分段控件、深藏青推荐卡（chip badge + 幽灵按钮 + 光晕）、场景卡网格（实色图标块）、统计行

![Practice 示例](https://github.com/LHRCarrier/ui-concept-design/raw/master/assets/screenshot-practice.png)

**Fitness 运动打卡仪表盘** —— 本页由**独立子代理仅凭 `SKILL.md` + `tokens/design-tokens.md` 生成**（无其他提示），
作为 skill 效果验证：双侧线稿插画 hero、统计行、56px 分段控件、实色图标块运动卡网格、深青推荐卡（chip + 光晕 + 幽灵按钮）、点线时间轴活动记录

![Fitness 示例](https://github.com/LHRCarrier/ui-concept-design/raw/master/assets/screenshot-fitness.png)

**App 移动端页面** —— 底部浮动 Tab 栏、全宽分段控件、对话气泡 + 语言点 chip、录音大按钮（波纹动效）、深紫评分卡、四维统计行（源码见 `examples/app/`，三个手机页面并排）：

![App 示例](https://github.com/LHRCarrier/ui-concept-design/raw/master/assets/app-screens.png)

## 核心设计理念（为 AI 代理设定）

本系统不是给人类设计师看的参考集，而是**写给 AI 代理的行为规范**——直接作为 skill 加载到
AI 编码代理（Claude / GPT / DeepSeek 等）的工作流中。以下理念是它的灵魂，违反任何一条都算失败：

**1. 先看，再动手。**
AI 最容易犯的错是"凭抽象原则生成 UI"——原则是对的，产出却是平庸的。本系统强制：
打开 `references/` 参考帧 → 记住它的圆角、留白、按钮形状 → 才开始写代码。参考帧是视觉正本，
不是装饰。

**2. 查表，禁止自造。**
所有色值、圆角、字号、间距、阴影都来自 `tokens/design-tokens.md`，一次都不要"差不多"。
自查口令：这个颜色查过表吗？这个圆角是 24 吗？这个按钮是胶囊吗？

**3. 每屏一个视觉锚点。**
大幅线稿插画（气球、日历、星星——2.5px 黑线 + 单色填涂）不是装饰，是"像设计成品"与
"像普通后台"的分界线。没有插画锚点的页面不许定稿。

**4. 尺度宁大勿小。**
Display ≥48px、统计数字 ≥32px、按钮高 48px、分段控件高 56px、页面边距 40px+。
做完把页面缩到 50% 跟参考帧并排：你的标题显小 = 不够大。

**5. 永远"程序员风"检查。**
小圆角按钮、直角卡片、彩色渐变、粗描边堆叠、深灰蓝主题、浅色底彩图标——这些机器生成的
"标准后台样式"全部禁止。标准答案：米白纸面、白卡 r-24 无描边、炭黑胶囊主按钮、克制的蓝黄点缀。

**6. 验收清单是底线。**
每页完成必须过 8 条自检（见 `SKILL.md` 第四节），全部答"是"才算完成；截图与参考帧并排对比，
不达标就回到第 1 步重来。

## 特性

- **多端通用**：一套视觉语言覆盖 Web / 移动 / 桌面原型（参考帧全部尺寸无关，tokens 按 px 直接落各端 CSS）。
- **精确设计令牌**：色板 / 圆角 / 字号 / 间距 / 组件规格全部查表取值，禁止"差不多"。
- **7 张内置参考帧**：覆盖首页英雄区、分段控件、统计卡、时间线卡、图片卡、深色展示卡等主要界面形态，动手前必看。
- **组件决策树**：要做什么 → 用哪个模板 / 参考帧 / 组件，减少空白区的即兴发挥。
- **完整源码示例**：`examples/web/`（landing / practice 两个单文件页面）+ `examples/app/`（home / speaking / report 三个手机页面），含微交互动效，全部值来自 tokens；`examples/README.md` 提供图标库与动效库的开源选型。
- **可选 WinForms 模板**：`templates/winforms/`（`Theme.cs` + 组件骨架），仅供 C# 桌面项目套用，已通过编译与运行时渲染验证；其他平台跳过即可。
- **验收清单**：8 条可自检的问答，全部答"是"才算完成。

## 目录结构

```
├── SKILL.md                  # 设计规范主体：硬约束 / 概念速查 / 工作流程 / 决策树 / 验收清单
├── references/               # 7 张内置设计参考帧（视觉正本）
├── tokens/
│   └── design-tokens.md      # 精确色值 / 圆角 / 字号 / 间距 / 组件规格
├── templates/
│   └── winforms/               # 可选：仅供 C#/WinForms 项目（其他平台跳过）
│       ├── Theme.cs           # 色板与字体令牌代码
│       └── ThemeControls.cs   # 组件代码骨架（PillButton / CardPanel / SegmentedControl / Badge 等）
├── examples/
│   ├── README.md             # 图标库选型（Lucide/Tabler/Phosphor）与动效库（Motion/Magic UI/Hover.css）
│   ├── web/
│   │   ├── landing.html      # 单文件可运行的 Landing 示例
│   │   ├── practice.html     # 单文件可运行的练习页示例
│   │   └── fitness.html      # 运动打卡仪表盘（子代理按 SKILL.md 生成，skill 效果验证）
│   └── app/                  # 移动端 App 示例（390px 手机视口）
│       ├── home.html         # 今日学习主页（底部浮动 Tab 栏）
│       ├── speaking.html     # 场景对话（气泡 + 录音大按钮）
│       └── report.html       # 唱歌评分报告（深色卡 + 统计行）
└── assets/                   # README 效果展示截图
```

## 快速上手

1. **看参考帧**：打开 `references/` 中与当前界面最像的 1-2 张，记住圆角、留白、按钮形状、文字层级。
2. **锁 tokens**：从 `tokens/design-tokens.md` 抄色板 / 圆角 / 字号（WinForms 抄到 `Theme.cs`，其他平台写成 CSS 变量），不改值。
3. **画视觉锚点**：先画线稿插画 / 大幅图形锚点（气球、日历、星星…），再在上面摆组件。
4. **套示例起步**：Web / 移动从 `examples/web/`、`examples/app/` 的单文件页面克隆改造；WinForms 从 `templates/winforms/` 复制组件骨架。任何平台都直接按 tokens 写样式。
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

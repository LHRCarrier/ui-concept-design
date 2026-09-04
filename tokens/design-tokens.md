# Design Tokens — 从内置参考帧提取的精确值

> 来源：`references/` 下 7 张设计参考帧。所有 UI 必须用这里的值，禁止自造。
> 单位约定：**所有尺寸以 px 为准，与平台无关**（Web px = 移动 CSS px = WinForms px，96dpi 基准）
> WinForms `Font` 用 pt，换算见文末 §7。

## 1. 色彩（浅色为默认主题）

| Token | 值 | 用途 | 出处 |
|---|---|---|---|
| `paper` | `#F5F4F1` | 应用主背景（米白纸面） | ref-landing-hero-light |
| `paper-grid` | `#EAE8E3` | 背景极淡网格线（可选肌理） | ref-card-light-timeline |
| `card` | `#FFFFFF` | 卡片面，纯白 | 全部浅色帧 |
| `ink` | `#1C1C1A` | 主文字 + Primary 按钮实心（炭黑，不是纯黑） | ref-profile-card-stats |
| `ink-pressed`| `#000000` | Primary 按下态 | — |
| `sub` | `#6F6F6A` | 次级文字（副标题、说明） | ref-profile-card-stats |
| `weak` | `#A6A6A0` | 弱文字（占位、时间戳、标签小字） | ref-profile-card-stats |
| `track` | `#ECEAE5` | 分段控件/Ghost 按钮的浅灰轨道底 | ref-segmented-pill |
| `accent` | `#2F6BFF` | 唯一点缀色：价格、链接、关键数字 | ref-card-light-timeline |
| `star` | `#FFC83D` | 收藏/评分星标 | ref-profile-card-stats |
| `pop-green` | `#A8E05F` | **线稿插画的单色填涂**（气球吊篮/日历色块），仅限插画，禁止用于按钮/文字 | ref-landing-hero-light |
| `success` | `#16A34A` | 成功态 | — |
| `error` | `#DC2626` | 错误/危险 | — |
| `divider` | `#EFEDE8` | 极浅分隔线（尽量不用，优先留白分组） | — |

### 有色深色卡片（仅限需要图片/产品衬托的展示卡，每屏最多 1 张）
深藏青 `#232044` / 深紫 `#3A2440` / 深绿 `#1E2B26` / 深青 `#16303A`。
深色卡上的 badge = 卡片色的提亮同色系（如紫卡配 `#4C3A6E` 底 `#B9A8FF` 字）。
**禁止纯黑 `#000` 和灰色系深色卡。参见 ref-dark-colored-cards。**

### Badge（标签胶囊）
彩色底 15% 透明度 + 同色高饱和文字。**浅色卡上 = pill 全圆角**，字号 11px SemiBold；
**深色卡上 = 12px 圆角 chip（不是胶囊）**，用卡片色的提亮同色系。
例：new = 底 `#E8EDFF` 字 `#2F6BFF`；成功 = 底 `#DCFCE7` 字 `#15803D`。

### 线稿插画（视觉锚点 · 每屏至少 1 处）
手绘感：ink `#1C1C1A`、2.5px 圆头线条、留白呼吸感；单色填涂只允许 pop-green `#A8E05F`（可少量弱化版 `#C6EF8D`）。
典型母题：热气球、日历、星星、音符、旋律线。英雄区左右对称构图，插画尺寸 ≈ 屏高 1/3。
**禁止：彩色渐变插画、照片滤镜、纯色剪影块。**

## 2. 圆角（现代感的核心，宁大勿小）

| Token | 值 | 用途 |
|---|---|---|
| `r-card` | 24px | 卡片 |
| `r-inner` | 18px | 卡片内嵌图片/缩略图 |
| `r-pill` | 999px（=高/2） | 按钮、badge、分段控件、输入框 |
| `r-chip` | 12px | 小标签块 |

**禁止直角和 ≤8px 的小圆角按钮。所有按钮都是胶囊。**

## 3. 排版（中西文双字体栈）

西文/数字：`Segoe UI`；中文：`Microsoft YaHei UI`（微软雅黑）。

| 层级 | 大小（px 为主 / WinForms pt） | 字重 | 颜色 | 用途 |
|---|---|---|---|---|
| Display | 34pt / **48-56px** | Bold | ink | 页面大标题（"Track deadlines."）——**宁大勿小** |
| Title | 18pt / 22-24px | Bold | ink | 卡片标题、区块标题 |
| Body | 10.5pt / 15-16px | Regular | ink/sub | 正文 |
| Caption | 9pt / 13px | Regular | weak | 辅助说明、时间戳 |
| Stat | 24pt / **32-36px** | Bold | ink | 统计大数字（标签用 Caption weak 在上） |

规则：标题写结论/内容名，不写控件类型名；数字用 Bold + 西文字体；每视图 ≤3 种字号字重组合。
尺度自检：做完把页面缩到 50% 截图，跟参考帧并排——**参考帧的标题占比明显更大，若你的标题"显小"就是不够大。**

## 4. 间距（8px 网格， generous）

页面边距 **40-56px**；卡片内边距 **32px**；卡片间距 24-32px；相关元素 8-12px；无关元素 ≥32px。
**用留白分组，不用分隔线。** 按钮水平内边距 ≥32px。
参考帧里的留白"多到怀疑人生"，如果觉得"有点空"，那就对了。

## 5. 组件规格（对照参考帧）

### Primary 按钮（ref-profile-card-stats "Send offer"）
pill 全圆角，ink 实心，白字 11pt SemiBold，**高 48px**，水平 padding 32px。
Hover：稍亮 `#333330`；Pressed：`#000`。**禁止彩色渐变按钮。**

### Secondary 按钮（两种，按底色选）
- **纸面上**（ref-landing-hero-light "View Leaderboard"）：pill，**白底 + 1.5px ink 描边**，ink 字，hover 底 `#FAFAF8`（描边转 60% 弱化）；
- **白卡上**（ref-profile-card-stats "Message"）：pill，`track` 浅灰底，ink 字，hover 底 `#E2E0DA`。

### Ghost / 深色卡上的按钮（ref-image-card-overlay "Buy now"）
pill 描边：1.5px 白色 60% 透明描边，透明底，白字。

### 分段控件（ref-segmented-pill）
`track` 底大胶囊（**高 56px**，r-pill），选中项 = 白色浮起小胶囊（带 0 2 8 rgba(28,28,26,.08) 微阴影），未选中 = 透明底 sub 色字。
图标+文字，间距 12px；**字号 16px、图标 24px——参考帧的分段控件很大，宁大勿小。**

### 卡片（ref-card-light-timeline）
白底 r-24，**无描边**，阴影仅 `0 8 24 rgba(28,28,26,0.06)`（极轻，可省略）。
结构：**左图标块 = 品牌色实底（深棕/深蓝那种实色，不是浅色底！）+ 白色线性图标 r-14** + 粗标题 + sub 副标题 + 右侧 accent 色关键值。
记录型卡片用**竖向点线时间轴**串联：图标块搁在点线上，点=当前项，线=轨迹。

### 统计行（ref-profile-card-stats）
上：Caption weak 标签；下：**Stat Bold 大数字（32px+）**。多列均分，间距 24px。

### 图片卡（ref-image-card-overlay）
大图铺满 r-24，文字压图底部，配 pill 描边幽灵按钮。

### 插画/线稿（ref-landing-hero-light 母题）
2.5px ink 圆头线 + pop-green 填涂；气球（吊篮涂绿）、日历（一格涂绿）、星星点缀。
插画与文字**分区不穿帮**：hero 两侧对称，尺寸 ≈ 屏高 1/3。

## 6. 反馈与动效

- Hover 过渡 120-150ms ease-out；卡片 hover = 轻微上浮感（阴影加深到 .10）或底变 `#FAFAF8`。
- 选中态：白胶囊浮起 / 左侧不要指示条（那是旧风格），用「底变 card + 字变 ink」。
- Toast：白底 pill 卡片（r-16），左侧彩色圆点 + ink 文字，顶部中央或右上，2.5s 淡出。
- 遮罩：`rgba(245,244,241,0.7)` 浅色遮罩 或 `rgba(28,28,26,0.35)`，弹窗白卡 r-24。
- Loading：按钮内旋转弧（ink 色），不加花哨进度条。

## 7. pt↔px 换算（WinForms）

WinForms `Font` 用 pt。`px ÷ 96 × 72 = pt`。
常用：13px≈9.75pt / 14px≈10.5pt / 16px≈12pt / 20px≈15pt / 24px≈18pt / 32px≈24pt。

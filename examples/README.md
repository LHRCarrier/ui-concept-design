# Examples · 图标与动效素材索引

> 本目录收录可直接配套本设计系统使用的**开源素材**（均含宽松许可），
> 以及 Web（`web/`）与移动 App（`app/`）两个平台的完整源码示例（单文件可双击打开）。

## 一、图标库选型（线性风格，与本系统 1.5-2px 描边一致）

| 库 | 许可 | 规格 | 适配建议 |
|---|---|---|---|
| [Lucide](https://lucide.dev) | ISC（宽松，可商用） | 24×24 viewBox，默认 stroke 2，圆头 | **首选**：与本系统线性图标最接近；按本系统改 `stroke-width=1.5` |
| [Tabler Icons](https://tabler.io/icons) | MIT | 24×24，默认 stroke 2 | 备选：图标数量大（4800+） |
| [Phosphor](https://phosphoricons.com) | MIT | 256×256，有 Regular/Bold 档 | 备选：有机风格 |

使用规则（skill 决策树）：
- 图标必须配文字（图标-按钮 = 图标 + label）；图标-only 按钮必须带 Tooltip；
- 统一取 `stroke-width: 1.5; stroke-linecap/linejoin: round`，禁止混用多套图标。

## 二、动效参考（微交互素材）

| 动效 | 时长 | 实现要点（本系统 tokens §6） |
|---|---|---|
| 胶囊按钮 hover | 120-150ms | 背景色插值（`Theme.Lerp` / CSS `transition: background-color 150ms ease-out`） |
| 分段控件选中浮起 | ≤200ms | 白胶囊 + 微阴影 `0 2 8 rgba(28,28,26,.08)`，无弹跳 |
| 卡片 hover 上浮 | 150ms | `translateY(-1px)` + 阴影加深（.06 → .10） |
| Toast 淡出 | 2.5s | 白底 pill r-16 + 左侧彩色圆点 |
| 线稿插画微动 | 200ms | hover 时 `translateY(-4px)`，无弹跳 |

### 开源动效库（可选引入）

- [Motion (Framer Motion)](https://motion.dev)（MIT）— Web 动画原语，做 Stagger/进出场；
- [Magic UI](https://magicui.design)（MIT）— 可复制粘贴的组件级动效（字幕、边栏、震感效果等）；
- [Hover.css](https://ianlunn.github.io/Hover/)（MIT）— hover 效果集，挑选 2-3 个与本系统契合的用即可。

> 用动效库只取「符合本系统」的部分：**≤200ms、无弹跳、颜色插值优先**。
> 弹跳/夸张入场不属于本设计系统的「微交互」概念。

## 三、完整源码示例

- `web/landing.html` — 单文件 HTML+CSS 示例：hero（双侧线稿插画 + 大标题 + 胶囊按钮）
  + 信任条 + 分段控件 + 统计行 + 列表卡，全部取值来自 `tokens/design-tokens.md`；
  浏览器直接双击打开即可，无构建依赖。
- `web/practice.html` — 单文件示例 ②：56px 分段控件 + 深藏青推荐卡（chip + 幽灵按钮 + 光晕）
  + 场景卡网格（实色图标块）+ 统计行。
- `app/home.html` — 移动端示例 ①（390px 手机视口）：问候头部、打卡徽章、统计卡、
  全宽分段控件、任务列表（点线时间轴）、底部浮动 Tab 栏（白卡 pill + 居中主按钮）。
- `app/speaking.html` — 移动端示例 ②：场景对话页——AI/用户对话气泡、语言点 chip、
  底部 ink 实心圆形录音按钮（外圈波纹脉冲动效）。
- `app/report.html` — 移动端示例 ③：唱歌评分报告——深紫评分卡（光晕 + chip + 音符插画
  + 幽灵按钮）、四维统计行、逐句评分点线时间轴列表。

# Stride Animation System

为 [Stride Engine](https://github.com/stride3d/stride) 构建的完整动画系统，达到 Unity Mecanim 水准。

> **引擎版本**: Stride 4.4.0+ &nbsp;|&nbsp; **许可**: MIT

---

## 目录

- [项目概述](#项目概述)
- [子系统导航](#子系统导航)
- [快速开始](#快速开始)
- [功能总览](#功能总览)
- [源码导航](#源码导航)

---

## 项目概述

本项目为 Stride Engine 补齐了 4 个核心动画子系统：

| 子系统 | 目录 | 对标 Unity | 文档数 |
|--------|------|-----------|:------:|
| **动画窗口** | [动画窗口/](动画窗口/) | Animation Window | 14 篇 |
| **状态机编辑器** | [动画系统/](动画系统/) | Animator Controller | 10 篇 |
| **IK 系统** | [IK系统/](IK系统/) | Animation Rigging | 11 篇 |
| **骨骼重定向** | [骨骼重定向/](骨骼重定向/) | Humanoid Avatar | 4 篇 |

---

## 子系统导航

### 📐 动画窗口 (Animation Window)

> 引擎内创作动画。录制 Entity Transform + 骨骼动画，Dopesheet 时间线编辑，曲线编辑器，保存为 `.sdanimclip` 资产。

**核心功能**：录制 → 编辑 → 预览 → 保存 → 运行时播放

📖 [动画窗口文档目录](动画窗口/README.md)

| 文档 | 内容 |
|------|------|
| [01-系统概述](动画窗口/01-系统概述.md) | 架构、布局、数据模型、最新功能 |
| [02-快速入门](动画窗口/02-快速入门.md) | 5 分钟创建第一个动画 |
| [03-录制模式](动画窗口/03-录制模式.md) | 手动录制 (+Key) / 自动录制 (●REC) |
| [04-Dopesheet 时间线](动画窗口/04-Dopesheet-时间线.md) | 关键帧操作、拖拽、删除、洋葱皮 |
| [05-曲线编辑器](动画窗口/05-曲线编辑器.md) | 曲线视图、子分量拖拽、插值切换 |
| [06-播放预览](动画窗口/06-播放预览.md) | 编辑器预览、实体同步 |
| [07-保存与加载](动画窗口/07-保存与加载.md) | .sdanimclip 资产、Asset View 集成 |
| [08-运行时播放](动画窗口/08-运行时播放.md) | PlayAnimationClip 脚本组件 |
| [09-复制粘贴与撤销](动画窗口/09-复制粘贴与撤销.md) | Ctrl+C/V、Ctrl+Z |
| [10-洋葱皮](动画窗口/10-洋葱皮.md) | 前后帧 Dopesheet 高亮 |
| [11-AnimatorController 集成](动画窗口/11-AnimatorController-集成.md) | 动画用于状态机 |
| [12-API 参考](动画窗口/12-API-参考.md) | 完整 C# API |
| [13-故障排查](动画窗口/13-故障排查.md) | 常见问题解决 |
| [14-界面布局](动画窗口/14-界面布局.md) | 窗口各区域命名与说明 |

---

### 🎮 动画系统 / 状态机编辑器 (AnimatorController)

> 可视化状态机编辑。拖拽状态节点、连线过渡、BlendTree 混合空间、参数驱动。

**核心功能**：状态机设计 → BlendTree 配置 → 参数条件 → 多层管理

📖 [动画系统文档目录](动画系统/README.md)

| 文档 | 内容 |
|------|------|
| [01-GettingStarted](动画系统/01-GettingStarted.md) | 入门指南 |
| [02-VisualEditor](动画系统/02-VisualEditor.md) | 可视化编辑器详解（含最新功能） |
| [03-Parameters](动画系统/03-Parameters.md) | Float/Int/Bool/Trigger 参数 |
| [04-StatesAndTransitions](动画系统/04-StatesAndTransitions.md) | 状态与过渡 |
| [05-BlendTrees](动画系统/05-BlendTrees.md) | 1D/2D/Direct 混合树（含 2D 可视化） |
| [06-LayersAndMasks](动画系统/06-LayersAndMasks.md) | 动画层与遮罩 |
| [07-EventsAndBehaviours](动画系统/07-EventsAndBehaviours.md) | 事件与 StateMachineBehaviour |
| [08-Scripting](动画系统/08-Scripting.md) | 脚本控制 |
| [09-Examples](动画系统/09-Examples.md) | 完整示例 |
| [10-CheatSheet](动画系统/10-CheatSheet.md) | 速查表 |

---

### 🦴 IK 系统 (Inverse Kinematics)

> 5 种 IK 求解器。运行时 + 编辑器预览、Gizmo 可视化、AnimatorController IK Pass 集成。

📖 [IK 系统文档目录](IK系统/README.md)

| 文档 | 内容 |
|------|------|
| [01-系统概述](IK系统/01-系统概述.md) | 架构、组件、速查表 |
| [02-TwoBoneIK](IK系统/02-TwoBoneIK.md) | 二骨 IK（手臂/腿部） |
| [03-LookAt IK](IK系统/03-LookAt-IK.md) | 头部注视 |
| [04-CCD](IK系统/04-CCD.md) | 循环坐标下降（尾巴/触手） |
| [05-FABRIK](IK系统/05-FABRIK.md) | 前后传递 IK（长链） |
| [06-MultiAim](IK系统/06-MultiAim.md) | 武器瞄准约束 |
| [07-AnimatorController 集成](IK系统/07-AnimatorController-集成.md) | IK Pass + OnStateIK |
| [08-Gizmo 可视化](IK系统/08-Gizmo-可视化.md) | IkTargetGizmo 场景拖拽 |
| [09-API 参考](IK系统/09-API-参考.md) | 完整 API |
| [10-故障排查](IK系统/10-故障排查.md) | 常见问题 |

---

### 🔄 骨骼重定向 (Humanoid Avatar)

> 标准人形骨骼映射。自动名称匹配、T-Pose 检测、Muscle 空间转换、实时 + 离线重定向。

📖 [骨骼重定向文档目录](骨骼重定向/README.md)

| 文档 | 内容 |
|------|------|
| [01-QuickStart](骨骼重定向/01-QuickStart.md) | 快速入门 |
| [02-Concepts](骨骼重定向/02-Concepts.md) | 概念说明（Bone/Muscle/Space/Map） |
| [03-API](骨骼重定向/03-API.md) | API 参考（含 BakeRetargetedClip） |
| [04-Examples](骨骼重定向/04-Examples.md) | 代码示例 |

---

## 功能总览

```
┌─────────────────────────────────────────────────────────────────┐
│                     Stride 动画系统                              │
├─────────────┬──────────────┬──────────────┬─────────────────────┤
│  动画窗口    │  状态机编辑器  │   IK 系统    │   骨骼重定向         │
│             │              │              │                     │
│ · Dopesheet │ · 拖拽节点    │ · TwoBoneIK  │ · Auto BoneMap      │
│ · 曲线编辑器  │ · 连线过渡    │ · LookAt     │ · T-Pose 检测       │
│ · 自动录制    │ · BlendTree  │ · CCD        │ · Muscle 空间       │
│ · 骨架录制    │   1D/2D 面板  │ · FABRIK     │ · 实时重定向         │
│ · 3D 洋葱皮   │ · 条件标签    │ · MultiAim   │ · 离线烘焙           │
│ · 复制粘贴    │ · 多层管理    │ · IK Pass    │ · .sdavatar         │
│ · .sdanimclip│ · 复制粘贴    │ · Gizmo      │                     │
│ · 运行时播放  │ · Undo/Redo  │ · 编辑器预览  │                     │
└─────────────┴──────────────┴──────────────┴─────────────────────┘
```

---

## 快速开始

### 1. 创建动画

```
场景中选中 Entity → Ctrl+F12 → ●REC → 移动实体 → +Key → ▶ 预览 → 💾 保存
```

→ 详见 [动画窗口/02-快速入门](动画窗口/02-快速入门.md)

### 2. 配置状态机

```
Asset View → Add → Animator Controller → 双击打开 → +State → 连线过渡 → 设条件
```

→ 详见 [动画系统/02-VisualEditor](动画系统/02-VisualEditor.md)

### 3. 添加 IK

```
选中角色 → Add Component → IK → Chains → TwoBone → 填骨骼名 → 设目标 → F5
```

→ 详见 [IK系统/01-系统概述](IK系统/01-系统概述.md)

### 4. 创建 Avatar

```
Asset View → Add → Humanoid Avatar → 选 Skeleton → Auto Configure → 赋给 AnimatorComponent
```

→ 详见 [骨骼重定向/01-QuickStart](骨骼重定向/01-QuickStart.md)

---

## 源码导航

```
stride/sources/
├── engine/Stride.Engine/Animations/
│   ├── Animator/          — AnimatorController 运行时 (状态机/混合树/参数/层)
│   ├── IK/                — IK 求解器 + IkComponent + IkTargetGizmo
│   └── Humanoid/          — HumanoidAvatar + BoneMap + MuscleSpace + RetargetEngine
├── engine/Stride.Assets.Models/
│   ├── AnimationClipAsset.cs        — .sdanimclip 资产定义
│   ├── AnimationClipAssetCompiler.cs — .sdanimclip 编译器
│   ├── HumanoidAvatarAsset.cs       — .sdavatar 资产定义
│   └── HumanoidAvatarAssetCompiler.cs — .sdavatar 编译器
└── editor/Stride.Assets.Presentation/
    ├── AnimationWindow/             — 动画窗口 (ViewModel + View + Code-behind)
    ├── AssetEditors/AnimatorControllerEditor/ — 状态机可视化编辑器
    └── Preview/AnimationClipPreview.cs — .sdanimclip 资产预览
```

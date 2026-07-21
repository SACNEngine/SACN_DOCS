# Stride Animator System — 完整文档

## 概述

Stride Animator System 是一个**状态机驱动的动画系统**，对标 Unity Animator Controller（Mecanim）。

### 核心能力

- **状态机**：可视化的动画状态与过渡逻辑
- **混合树**：1D/2D 参数化混合空间
- **参数系统**：Float/Int/Bool/Trigger 驱动过渡
- **分层动画**：多层独立状态机 + AvatarMask 骨骼遮罩
- **动画事件**：时间线标记回调
- **状态行为**：OnStateEnter/Exit/Update 生命周期
- **Override Controller**：共享逻辑，替换 Clip

### 文档索引

| 文档 | 内容 |
|------|------|
| [01-快速开始](01-GettingStarted.md) | 5 分钟上手 |
| [02-可视化编辑器](02-VisualEditor.md) | 编辑器操作完整指南 |
| [03-参数系统](03-Parameters.md) | Float/Int/Bool/Trigger |
| [04-状态与过渡](04-StatesAndTransitions.md) | 状态机核心 |
| [05-混合树](05-BlendTrees.md) | 1D/2D/Direct 混合 |
| [06-层与遮罩](06-LayersAndMasks.md) | 分层动画 + AvatarMask |
| [07-事件与行为](07-EventsAndBehaviours.md) | AnimationEvent + StateMachineBehaviour |
| [08-脚本API](08-Scripting.md) | C# API 参考 |
| [09-完整示例](09-Examples.md) | 第三人称角色控制器 |

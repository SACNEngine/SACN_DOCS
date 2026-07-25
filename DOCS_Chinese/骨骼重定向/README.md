# Stride Humanoid Avatar — 骨骼重定向系统

## 概述

当你有一个角色的动画（如 Mixamo 导出的瘦子走路），想在另一个骨架结构完全不同的角色（如 Blender 做的胖子）上播放时，传统的"复制骨骼名"方法行不通——因为两个骨架的骨骼名、层级、比例都不一样。

Humanoid Avatar 解决了这个问题：它把任意人形骨架映射到一套**统一的内部标准**，动画在这个标准空间中表达，再映射回目标骨架。

```
源骨架 ──→ Muscle Space (统一) ──→ 目标骨架
   "LeftArm"     LeftArmDownUp=0.5       "arm_L"
   (45 bones)     (44个标准肌肉值)       (32 bones)
```

## 核心概念

| 概念 | 说明 |
|------|------|
| **HumanoidBone** | 17个标准人形骨骼（Hips, Spine, LeftArm...） |
| **Muscle Space** | 44个归一化肌肉值（-1到1），0=T-Pose |
| **BoneMap** | 标准骨骼 → 实际骨架节点索引的映射 |
| **T-Pose** | 参考姿势，所有肌肉值为0 |
| **Muscle Limit** | 每个关节的活动范围 |
| **Retarget Engine** | 运行时动画重定向引擎 |

## 文档索引

| 文档 | 内容 |
|------|------|
| [01-快速开始](01-QuickStart.md) | 5分钟完成骨骼重定向 |
| [02-核心概念](02-Concepts.md) | 深入理解 Muscle Space |
| [03-API参考](03-API.md) | 完整 C# API |
| [04-完整示例](04-Examples.md) | 多角色重定向 |

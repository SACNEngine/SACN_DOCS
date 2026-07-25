# Stride Humanoid Avatar — Skeleton Retargeting System

## Overview

When you have a character's animation (such as a slim character walking, exported from Mixamo) and want to play it on another character whose skeleton structure is completely different (such as a fat character made in Blender), the traditional "copy the bone names" approach does not work — because the two skeletons differ in bone names, hierarchy, and proportions.

Humanoid Avatar solves this problem: it maps any humanoid skeleton to a **unified internal standard**, expresses the animation in this standard space, and then maps it back to the target skeleton.

```
Source skeleton ──→ Muscle Space (unified) ──→ Target skeleton
   "LeftArm"        LeftArmDownUp=0.5            "arm_L"
   (45 bones)       (44 standard muscle values) (32 bones)
```

## Core concepts

| Concept | Description |
|---------|-------------|
| **HumanoidBone** | 17 standard humanoid bones (Hips, Spine, LeftArm...) |
| **Muscle Space** | 44 normalized muscle values (-1 to 1), 0 = T-Pose |
| **BoneMap** | Mapping from standard bones → actual skeleton node indices |
| **T-Pose** | Reference pose where all muscle values are 0 |
| **Muscle Limit** | The range of motion for each joint |
| **Retarget Engine** | The runtime animation retargeting engine |

## Documentation index

| Document | Content |
|----------|---------|
| [01-QuickStart](01-QuickStart.md) | Complete skeleton retargeting in 5 minutes |
| [02-Concepts](02-Concepts.md) | Deep dive into Muscle Space |
| [03-API](03-API.md) | Complete C# API |
| [04-Examples](04-Examples.md) | Multi-character retargeting |

# 02 — 核心概念

## HumanoidBone（标准人形骨骼）

17 个必需骨骼 + 可选骨骼和手指。所有骨骼编号遵循固定规则：左侧 `10-19/30-39/100-199`，右侧 `20-29/40-49/200-299`。

```
Hips (0)
├── Spine (1)
│   └── Chest (2)
│       ├── UpperChest (50, 可选)
│       ├── Neck (3) → Head (4)
│       ├── LeftShoulder (60) → LeftUpperArm (30) → LeftLowerArm (31) → LeftHand (32)
│       └── RightShoulder (61) → RightUpperArm (40) → RightLowerArm (41) → RightHand (42)
├── LeftUpperLeg (10) → LeftLowerLeg (11) → LeftFoot (12) → LeftToes (13)
└── RightUpperLeg (20) → RightLowerLeg (21) → RightFoot (22) → RightToes (23)
```

## Muscle Space（肌肉空间）

核心创新：用 44 个归一化浮点值描述人形角色在任意时刻的姿态。

### 原理

```
源角色 LeftArm 旋转(45°, -10°, 0°)   目标角色 arm_L 旋转(38°, -12°, 0°)
          ↓                                      ↑
     Muscle: LeftArmDownUp = 0.5  ──────────→  相同的 Muscle 值
     Muscle: LeftArmFrontBack = -0.2 ────────→  
     Muscle: LeftArmTwistInOut = 0.0 ────────→
```

每个 Muscle 对应一个关节的一个自由度：

| Muscle ID | 控制 | 值含义 |
|-----------|------|--------|
| `LeftArmDownUp` | 左臂上下摆动 | -1=下, 0=T-Pose, +1=上 |
| `LeftArmFrontBack` | 左臂前后摆动 | -1=后, 0=T-Pose, +1=前 |
| `LeftArmTwistInOut` | 左臂扭转 | -1=内旋, 0=T-Pose, +1=外旋 |
| `SpineFrontBack` | 脊椎前后弯曲 | -1=后仰, 0=T-Pose, +1=前倾 |

### Muscle ↔ Bone 转换

```csharp
// 骨骼旋转 → Muscle 值
float mv = HumanoidMuscleSpace.BoneToMuscle(
    HumanoidMuscleId.LeftArmDownUp,   // 哪个 muscle
    boneRotation,                      // 当前骨骼旋转
    tPoseRotation,                     // T-Pose 参考
    limit);                            // 范围限制

// Muscle 值 → 骨骼旋转
Quaternion rot = HumanoidMuscleSpace.MuscleToBone(
    HumanoidMuscleId.LeftArmDownUp,
    0.5f,                              // Muscle 值
    tPoseRotation, limit);
```

## Muscle Limit（活动范围）

防止重定向后关节出现异常弯曲：

```csharp
// 手臂上下：-1.2 到 1.2（宽范围）
{ LeftArmDownUp: { Min: -1.2, Max: 1.2 } }

// 前臂拉伸：几乎为 0（肘关节不能拉伸）
{ LeftForearmStretch: { Min: -0.1, Max: 0.1 } }
```

## T-Pose（参考姿势）

所有 muscle 值为 0 时的标准姿势：
- 身体直立，面朝 +Z
- 手臂水平伸出（+X 为左，-X 为右）
- 手掌向下
- 双腿伸直向下

`HumanoidTPose.ExtractReferencePose()` 从任意 rest pose 计算到 T-Pose 的旋转偏移。

## Retarget Engine（重定向引擎）

```csharp
// 创建引擎
var engine = srcHumanoid.CreateRetargetEngine(dstHumanoid);

// 每帧应用
engine.ApplyRetarget(srcSkeletonUpdater, dstSkeletonUpdater);

// 单骨骼重定向
Quaternion dstRot = engine.RetargetBone(
    HumanoidBone.LeftUpperArm, srcRotation);
```

### 数据流

```
src SkeletonUpdater
  │ NodeTransformations[i].Transform.Rotation
  ▼
RetargetBone(bone, srcRot)
  │ BoneToMuscle → 44个肌肉值
  │ MuscleToBone → 目标骨骼旋转
  ▼
dst SkeletonUpdater
  │ NodeTransformations[j].Transform.Rotation = dstRot
```

## 骨骼自动映射

`HumanoidBoneMap.AutoMap()` 支持 6 种常见命名风格：

| 风格 | LeftArm 示例 |
|------|-------------|
| Unity/FBX 默认 | `LeftArm`, `LeftForeArm` |
| Blender | `Arm_L`, `Forearm_L` |
| Maya | `L_Arm`, `l_forearm` |
| 3dsMax | `Bip01 L UpperArm` |
| Mixamo | `mixamorig:LeftArm` |

两阶段匹配：
1. **精确匹配**：名称完全相同或结尾匹配
2. **模糊匹配**：名称包含关键字

# 06 — 动画层与骨骼遮罩

## 动画层 (AnimatorControllerLayer)

每一层有**独立的状态机**，按权重叠加：

```csharp
ctrl.Layers.Add(new AnimatorControllerLayer
{
    Name = "Base Layer",
    StateMachine = locomotionSM,
    DefaultWeight = 1f,
    BlendingMode = AnimatorLayerBlendingMode.Override,
});

ctrl.Layers.Add(new AnimatorControllerLayer
{
    Name = "Upper Body",
    StateMachine = aimingSM,
    AvatarMask = upperBodyMask,    // 仅影响上半身
    DefaultWeight = 0.5f,
    BlendingMode = AnimatorLayerBlendingMode.Override,
});
```

### 属性

| 属性 | 类型 | 默认值 | 说明 |
|------|------|--------|------|
| `Name` | string | null | 层名称 |
| `StateMachine` | AnimatorStateMachine | null | 本层的状态机 |
| `AvatarMask` | AvatarMask | null | 骨骼遮罩 |
| `DefaultWeight` | float | 1.0 | 默认混合权重 |
| `BlendingMode` | enum | Override | Override / Additive |
| `IkPass` | bool | false | IK 通过是否在此层后执行 |
| `AlwaysUpdate` | bool | false | 权重为 0 时是否仍更新 |

### 混合模式

| 模式 | 行为 |
|------|------|
| `Override` | 上层替换下层（受 AvatarMask 限制的骨骼） |
| `Additive` | 上层动画叠加到下层之上 |

### 运行时控制层权重

```csharp
// 覆盖第 1 层的权重
animator.LayerWeightOverrides[1] = 0.8f;
```

---

## AvatarMask（骨骼遮罩）

定义哪些骨骼受某一层的影响。

### 手动构建

```csharp
var mask = new AvatarMask();
mask.BoneWeights["Spine"]       = 1f;
mask.BoneWeights["RightArm"]    = 1f;
mask.BoneWeights["RightForeArm"] = 1f;
mask.BoneWeights["RightHand"]   = 1f;
mask.BoneWeights["LeftArm"]     = 1f;
mask.BoneWeights["LeftForeArm"]  = 1f;
mask.BoneWeights["LeftHand"]    = 1f;
```

### 人体部位预设

```csharp
var armsMask = AvatarMask.CreateBodyPart(
    AvatarMask.AvatarMaskBodyPart.RightArm,
    AvatarMask.AvatarMaskBodyPart.LeftArm,
    AvatarMask.AvatarMaskBodyPart.RightHand,
    AvatarMask.AvatarMaskBodyPart.LeftHand
);

var legsMask = AvatarMask.CreateBodyPart(
    AvatarMask.AvatarMaskBodyPart.RightLeg,
    AvatarMask.AvatarMaskBodyPart.LeftLeg,
    AvatarMask.AvatarMaskBodyPart.RightFoot,
    AvatarMask.AvatarMaskBodyPart.LeftFoot
);
```

### 可用部位

| 部位 | 包含骨骼 |
|------|---------|
| `Head` | Head, Neck |
| `Spine` | Hips, Spine, Spine1, Spine2 |
| `LeftArm` | LeftShoulder, LeftArm, LeftForeArm |
| `RightArm` | RightShoulder, RightArm, RightForeArm |
| `LeftHand` | LeftHand + 手指 |
| `RightHand` | RightHand + 手指 |
| `LeftLeg` | LeftUpLeg, LeftLeg |
| `RightLeg` | RightUpLeg, RightLeg |
| `LeftFoot` | LeftFoot, LeftToeBase |
| `RightFoot` | RightFoot, RightToeBase |
| `FullBody` | 全部骨骼 |

### 运行时优化

`AvatarMask.Resolve(Skeleton)` 将骨骼名解析为节点索引并缓存，后续用 `GetNodeWeight(int)` 高效查询。

---

## 分层动画示例

### FPS 武器层

```csharp
// 基础层：全身移动
ctrl.Layers.Add(new AnimatorControllerLayer
{
    Name = "Base",
    StateMachine = locomotionSM,
    DefaultWeight = 1f,
});

// 武器层：仅手臂
var weaponSM = new AnimatorStateMachine { Name = "Weapon", DefaultStateId = idleState.Id };
weaponSM.States.Add(idleState);
weaponSM.States.Add(reloadState);
weaponSM.States.Add(fireState);

ctrl.Layers.Add(new AnimatorControllerLayer
{
    Name = "Weapon",
    StateMachine = weaponSM,
    AvatarMask = AvatarMask.CreateBodyPart(
        AvatarMask.AvatarMaskBodyPart.RightArm,
        AvatarMask.AvatarMaskBodyPart.LeftArm),
    DefaultWeight = 1f,
    BlendingMode = AnimatorLayerBlendingMode.Override,
});
```

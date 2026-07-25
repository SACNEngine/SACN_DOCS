# 06 — Animation Layers and Bone Masks

## Animation layer (AnimatorControllerLayer)

Each layer has its **own state machine**, stacked by weight:

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
    AvatarMask = upperBodyMask,    // Affects only the upper body
    DefaultWeight = 0.5f,
    BlendingMode = AnimatorLayerBlendingMode.Override,
});
```

### Properties

| Property | Type | Default | Description |
|----------|------|---------|-------------|
| `Name` | string | null | Layer name |
| `StateMachine` | AnimatorStateMachine | null | State machine for this layer |
| `AvatarMask` | AvatarMask | null | Bone mask |
| `DefaultWeight` | float | 1.0 | Default blend weight |
| `BlendingMode` | enum | Override | Override / Additive |
| `IkPass` | bool | false | Whether the IK pass executes after this layer |
| `AlwaysUpdate` | bool | false | Whether to keep updating when weight is 0 |

### Blending modes

| Mode | Behavior |
|------|----------|
| `Override` | Upper layer replaces the lower layer (for bones limited by AvatarMask) |
| `Additive` | Upper layer animation is added on top of the lower layer |

### Runtime layer weight control

```csharp
// Override the weight of layer 1
animator.LayerWeightOverrides[1] = 0.8f;
```

---

## AvatarMask (bone mask)

Defines which bones are affected by a given layer.

### Manual construction

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

### Human body part presets

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

### Available parts

| Part | Bones included |
|------|----------------|
| `Head` | Head, Neck |
| `Spine` | Hips, Spine, Spine1, Spine2 |
| `LeftArm` | LeftShoulder, LeftArm, LeftForeArm |
| `RightArm` | RightShoulder, RightArm, RightForeArm |
| `LeftHand` | LeftHand + fingers |
| `RightHand` | RightHand + fingers |
| `LeftLeg` | LeftUpLeg, LeftLeg |
| `RightLeg` | RightUpLeg, RightLeg |
| `LeftFoot` | LeftFoot, LeftToeBase |
| `RightFoot` | RightFoot, RightToeBase |
| `FullBody` | All bones |

### Runtime optimization

`AvatarMask.Resolve(Skeleton)` resolves bone names to node indices and caches them; subsequently use `GetNodeWeight(int)` for efficient lookup.

---

## Layered animation example

### FPS weapon layer

```csharp
// Base layer: full-body movement
ctrl.Layers.Add(new AnimatorControllerLayer
{
    Name = "Base",
    StateMachine = locomotionSM,
    DefaultWeight = 1f,
});

// Weapon layer: arms only
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

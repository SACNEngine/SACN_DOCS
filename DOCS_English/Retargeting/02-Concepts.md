# 02 — Core concepts

## HumanoidBone (standard humanoid bones)

17 required bones + optional bones and fingers. All bone indices follow fixed rules: left side `10-19/30-39/100-199`, right side `20-29/40-49/200-299`.

```
Hips (0)
├── Spine (1)
│   └── Chest (2)
│       ├── UpperChest (50, optional)
│       ├── Neck (3) → Head (4)
│       ├── LeftShoulder (60) → LeftUpperArm (30) → LeftLowerArm (31) → LeftHand (32)
│       └── RightShoulder (61) → RightUpperArm (40) → RightLowerArm (41) → RightHand (42)
├── LeftUpperLeg (10) → LeftLowerLeg (11) → LeftFoot (12) → LeftToes (13)
└── RightUpperLeg (20) → RightLowerLeg (21) → RightFoot (22) → RightToes (23)
```

## Muscle Space

The core innovation: a humanoid character's pose at any moment is described using 44 normalized floating-point values.

### Principle

```
Source character LeftArm rotation (45°, -10°, 0°)    Target character arm_L rotation (38°, -12°, 0°)
          ↓                                                   ↑
     Muscle: LeftArmDownUp = 0.5  ──────────→   same Muscle value
     Muscle: LeftArmFrontBack = -0.2 ────────→
     Muscle: LeftArmTwistInOut = 0.0 ────────→
```

Each Muscle corresponds to one degree of freedom of one joint:

| Muscle ID | Controls | Value meaning |
|-----------|----------|---------------|
| `LeftArmDownUp` | Left arm up/down swing | -1 = down, 0 = T-Pose, +1 = up |
| `LeftArmFrontBack` | Left arm front/back swing | -1 = back, 0 = T-Pose, +1 = front |
| `LeftArmTwistInOut` | Left arm twist | -1 = inward, 0 = T-Pose, +1 = outward |
| `SpineFrontBack` | Spine front/back bend | -1 = lean back, 0 = T-Pose, +1 = lean forward |

### Muscle ↔ Bone conversion

```csharp
// Bone rotation → Muscle value
float mv = HumanoidMuscleSpace.BoneToMuscle(
    HumanoidMuscleId.LeftArmDownUp,   // which muscle
    boneRotation,                      // current bone rotation
    tPoseRotation,                     // T-Pose reference
    limit);                            // range limit

// Muscle value → Bone rotation
Quaternion rot = HumanoidMuscleSpace.MuscleToBone(
    HumanoidMuscleId.LeftArmDownUp,
    0.5f,                              // Muscle value
    tPoseRotation, limit);
```

## Muscle Limit (range of motion)

Prevents joints from bending abnormally after retargeting:

```csharp
// Arm up/down: -1.2 to 1.2 (wide range)
{ LeftArmDownUp: { Min: -1.2, Max: 1.2 } }

// Forearm stretch: nearly 0 (elbow cannot stretch)
{ LeftForearmStretch: { Min: -0.1, Max: 0.1 } }
```

## T-Pose (reference pose)

The standard pose when all muscle values are 0:
- Body upright, facing +Z
- Arms extended horizontally (+X is left, -X is right)
- Palms facing down
- Legs straight down

`HumanoidTPose.ExtractReferencePose()` computes the rotation offset to T-Pose from any rest pose.

## Retarget Engine

```csharp
// Create the engine
var engine = srcHumanoid.CreateRetargetEngine(dstHumanoid);

// Apply every frame
engine.ApplyRetarget(srcSkeletonUpdater, dstSkeletonUpdater);

// Single-bone retargeting
Quaternion dstRot = engine.RetargetBone(
    HumanoidBone.LeftUpperArm, srcRotation);
```

### Data flow

```
src SkeletonUpdater
  │ NodeTransformations[i].Transform.Rotation
  ▼
RetargetBone(bone, srcRot)
  │ BoneToMuscle → 44 muscle values
  │ MuscleToBone → target bone rotation
  ▼
dst SkeletonUpdater
  │ NodeTransformations[j].Transform.Rotation = dstRot
```

## Automatic bone mapping

`HumanoidBoneMap.AutoMap()` supports 6 common naming conventions:

| Style | LeftArm example |
|-------|-----------------|
| Unity/FBX default | `LeftArm`, `LeftForeArm` |
| Blender | `Arm_L`, `Forearm_L` |
| Maya | `L_Arm`, `l_forearm` |
| 3dsMax | `Bip01 L UpperArm` |
| Mixamo | `mixamorig:LeftArm` |

Two-phase matching:
1. **Exact match**: name is identical or matches at the end
2. **Fuzzy match**: name contains a keyword

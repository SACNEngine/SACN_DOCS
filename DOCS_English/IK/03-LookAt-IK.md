# 03 — LookAt IK (gaze IK)

## Purpose

Single-bone rotation to face a target. Character head gazing at the player, NPC eye tracking, automatic camera orientation.

## Algorithm: quaternion axis rotation

```
     Head
       \
        \ Forward (local Z)
         \
          ● Target

1. Compute the Head→Target world direction
2. Current Forward direction in world space
3. Rotate from current→target direction
4. Optional maxAngle clamp
```

## Configuration

```
Type:    LookAt
Bone:    Head              (single bone name)
TargetPosition:  X:0 Y:2 Z:3   (gaze target)
Weight:  0.7                (0 = no head turn, 1 = fully faces target)
```

## Parameter notes

| Parameter | Value | Description |
|-----------|-------|-------------|
| Forward | (0,0,1) | Bone's local forward direction (hardcoded) |
| Up | (0,1,0) | Local up direction (hardcoded) |
| clampAngle | 0 (default) | Maximum rotation angle (radians), 0 = no limit |

## Usage scenarios

| Scenario | Weight | Target |
|----------|--------|--------|
| Gaze at player | 0.5-0.8 | Player head position |
| Reading a book | 1.0 | Book position |
| Looking around | 0.3 | Random sway |

## Code invocation

```csharp
TwoBoneIKSolver.SolveLookAt(
    skeleton,      // SkeletonUpdater
    boneIndex,     // bone index (int)
    target,        // Vector3 world target
    forward,       // Vector3 local forward direction
    up,            // Vector3 local up direction
    clampAngle,    // float maximum angle
    weight         // float 0-1
);
```

## Complete example

```csharp
public class HeadLookAt : SyncScript
{
    public Entity Player;

    public override void Update()
    {
        var ik = Entity.Components.Get<IkComponent>();
        if (ik?.Chains.Count > 1)
        {
            var playerHead = Player.Transform.WorldMatrix.TranslationVector + Vector3.UnitY * 1.7f;
            ik.Chains[1].TargetPosition = playerHead;  // Chain[1] = LookAt
        }
    }
}
```

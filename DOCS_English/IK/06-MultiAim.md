# 06 — MultiAim (weapon aiming constraint)

## Purpose

Single-bone rotation so that its local axis points at a target. Weapon muzzle, flashlight direction, camera aiming.

## Algorithm: axis rotation

```
    Weapon (bone)
       |
       | AimAxis (local Z = barrel direction)
       ↓
       ● Target

Rotate the bone so that AimAxis points at Target
```

## Configuration

```
Type:    MultiAim
Bone:    Weapon_R            (gun-holding bone)
TargetPosition:  X:5 Y:1 Z:0    (aim target)
Weight:  1
```

## Hardcoded axes (modifiable in code)

| Axis | Default | Description |
|------|---------|-------------|
| AimAxis | (0,0,1) | Aim direction (barrel points) |
| UpAxis | (0,1,0) | Up direction (prevents roll) |
| WorldUp | (0,1,0) | World up reference |

## Usage scenarios

| Scenario | Aim bone |
|----------|----------|
| FPS muzzle direction | Weapon_R |
| Flashlight pointing | Flashlight |
| Camera tracking | CameraBone |
| Finger pointing | IndexFinger_R |

## Code invocation

```csharp
TwoBoneIKSolver.SolveMultiAim(
    skeleton,      // SkeletonUpdater
    boneIndex,     // bone index (int)
    target,        // Vector3 world target
    aimAxis,       // Vector3 local aim axis
    upAxis,        // Vector3 local up axis
    worldUp,       // Vector3 world up direction
    weight         // float 0-1
);
```

## Complete example

```csharp
public class WeaponAimIK : SyncScript
{
    public Entity Crosshair;  // crosshair position

    public override void Update()
    {
        var ik = Entity.Components.Get<IkComponent>();
        if (ik?.Chains.Count > 0)
        {
            ik.Chains[0].TargetPosition =
                Crosshair.Transform.WorldMatrix.TranslationVector;
        }
    }
}
```

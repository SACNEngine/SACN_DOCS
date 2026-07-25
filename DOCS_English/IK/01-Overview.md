# 01 — IK system overview

## Architecture

```
Per-frame render pipeline:
  AnimationProcessor (-500)     → evaluate animation on skeleton
  AnimatorProcessor (-500)      → state machine + IK Pass
  IkProcessor (-499)            → IK solving corrects skeleton
  Rendering                     → GPU skinning
```

## Core components

### IkComponent

```csharp
// Property panel → Add Component → search for "IK"
public class IkComponent : EntityComponent
{
    public List<IkChainSetup> Chains { get; }
}
```

### IkChainSetup

```csharp
public enum IkType { TwoBone, LookAt, CCD, FABRIK, MultiAim }

public class IkChainSetup
{
    public IkType Type;             // IK type
    public string Bone;             // LookAt/MultiAim single bone name
    public string RootBone;         // TwoBone/CCD/FABRIK root bone
    public string MidBone;          // TwoBone middle bone
    public string TipBone;          // TwoBone/CCD/FABRIK end bone
    public Vector3 TargetPosition;  // world-space target
    public Vector3 HintPosition;    // TwoBone bend direction
    public float Weight;            // 0-1 blend weight
    public int CcdIterations;       // CCD/FABRIK iteration count
}
```

## Five IK types at a glance

| Type | Bone count | Configured bones | Use |
|------|-----------|------------------|-----|
| TwoBone | 3 | Root + Mid + Tip | Arms, legs |
| LookAt | 1 | Bone | Head gaze |
| CCD | Many | Root + Tip | Tails, tentacles |
| FABRIK | Many | Root + Tip | Long chains, fast convergence |
| MultiAim | 1 | Bone | Weapon aiming |

## Finding the bone names

1. Select the character Entity.
2. ModelComponent → Skeleton → Nodes.
3. Expand each entry and read Node.Name.
4. **Copy exactly, preserving case and spaces.**

## Quick configuration in three steps

1. Select the character Entity → Add Component → **IK**.
2. Chains → **+** → choose Type → fill in bone name → set TargetPosition.
3. F5 or use the editor's live preview.

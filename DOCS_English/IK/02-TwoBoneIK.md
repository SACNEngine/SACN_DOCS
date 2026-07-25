# 02 — TwoBoneIK (two-bone IK)

## Purpose

End-effector position control for arms (shoulder → elbow → hand) and legs (hip → knee → foot).

## Algorithm: law of cosines

```
       Mid (elbow)
      /  \
   a /    \ b
    /      \
 Root       Tip ← Target

c = distance(Root, Target), clamped to [|a-b|, a+b]
cos(θ) = (a² + b² - c²) / (2ab)
→ Mid joint bends by θ degrees
→ Root rotates so that Tip points at Target
→ Hint point determines the bend direction
```

## Configuration

```
Type:        TwoBone
RootBone:    LeftUpperArm     (upper arm, near the body)
MidBone:     LeftForearm      (forearm, middle joint)
TipBone:     LeftHand         (hand, end effector)
TargetPosition:   X:2 Y:1.5 Z:1    (where the hand should go)
HintPosition:     X:2 Y:0   Z:2.5  (which way the elbow bends)
Weight:      1                  (0 = no effect, 1 = fully follows)
```

## Hint position notes

Hint controls which direction the joint bends. For an arm:
- `Hint Z > Tip Z` → elbow bends forward.
- `Hint Z < Tip Z` → elbow bends backward.
- `Hint Y > Tip Y` → elbow bends upward.

Usually set Hint = Target + a small offset.

## Usage scenarios

| Scenario | Configuration |
|----------|---------------|
| Character grabs an object | Hand TwoBone, Target = object position |
| Feet conform to the ground | Foot TwoBone, Target = ground point from raycast |
| Push-a-door motion | Hand TwoBone, Target = door handle position |
| Climbing | Hands and feet TwoBone at the same time, Target = climb point |

## Code invocation

```csharp
// Direct call (without using IkComponent)
TwoBoneIKSolver.Solve(
    skeleton,          // SkeletonUpdater
    rootIndex,         // root bone index (int)
    midIndex,          // middle bone index (int)
    tipIndex,          // end bone index (int)
    targetPosition,    // Vector3 world coordinates
    hintPosition,      // Vector3 bend direction
    weight             // float 0-1
);
```

## Complete example

```csharp
public class HandGrabIK : SyncScript
{
    public Entity TargetObject;

    public override void Update()
    {
        var ik = Entity.Components.Get<IkComponent>();
        if (ik?.Chains.Count > 0)
        {
            ik.Chains[0].TargetPosition =
                TargetObject.Transform.WorldMatrix.TranslationVector;
        }
    }
}
```

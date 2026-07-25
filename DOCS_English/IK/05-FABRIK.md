# 05 — FABRIK (Forward And Backward Reaching IK)

## Purpose

Fast convergence for long bone chains. Tails, tentacle-like creatures, and multi-segment spines of 10+ segments.

## Algorithm: forward and backward passes

```
Forward pass (tip→root):
  ① Tip = Target
  ② Preserving bone lengths, adjust positions level by level toward the root

Backward pass (root→tip):
  ③ Root = OriginalRoot (restore original position)
  ④ Preserving bone lengths, adjust positions level by level toward the tip

Repeat N times (usually 3-5 is enough)
```

## Why it is better than CCD

- Each iteration adjusts all bones at once (instead of bone by bone).
- Motion is evenly distributed and looks more natural.
- Converges extremely fast; 3-5 iterations ≈ 10-15 for CCD.

## Configuration

```
Type:    FABRIK
RootBone:   Spine_01         (chain start)
TipBone:    Spine_10         (chain end)
TargetPosition:  X:0 Y:1 Z:3
CcdIterations:  4            (FABRIK converges fast, 3-5 is enough)
Weight:  1
```

## Parameter suggestions

| Bone count | CcdIterations |
|------------|---------------|
| 3-5 | 3 |
| 6-10 | 4 |
| 10+ | 5 |

## Code invocation

```csharp
TwoBoneIKSolver.SolveFABRIK(
    skeleton,      // SkeletonUpdater
    chainStart,    // chain start index (int)
    chainEnd,      // chain end index (int)
    target,        // Vector3 world target
    weight,        // float 0-1
    iterations     // int iteration count (3-5)
);
```

## Notes

- FABRIK operates directly on positions, so after solving an extra step is needed to convert the position changes back into rotations.
- If the bone chain cannot reach the target, it straightens out pointing at the target.
- When blending with animation, Weight < 1.0 is recommended.

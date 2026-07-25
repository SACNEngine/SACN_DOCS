# 04 — CCD (Cyclic Coordinate Descent)

## Purpose

End-effector position control for multi-bone chains. Tails, tentacles, multi-segment spines.

## Algorithm: per-bone iterative rotation

```
Chain: [B0]→[B1]→[B2]→[B3]→[Tip]  Target ●

Each iteration:
  For i = Tip downto Root:
    ① Compute Bi world position
    ② Bi→Tip direction vs Bi→Target direction
    ③ Rotate Bi to align the two

Repeat N times until convergence
```

## Characteristics

- End bones converge first (the tip arrives first).
- Root bones move the most (may look unnatural).
- Simple to implement, good robustness.

## Configuration

```
Type:    CCD
RootBone:   Tail_01          (chain start bone)
TipBone:    Tail_06          (chain end bone)
TargetPosition:  X:1 Y:0.5 Z:2
CcdIterations:  8            (iteration count, 5-10 recommended)
Weight:  1
```

## Parameter suggestions

| Bone count | CcdIterations |
|------------|---------------|
| 3-4 | 5 |
| 5-7 | 8 |
| 8+ | 10-15 |

## vs FABRIK

| | CCD | FABRIK |
|---|-----|--------|
| Convergence speed | Slow (needs more iterations) | Fast (3-5) |
| Motion distribution | Concentrated at the end | Even and natural |
| Recommended scenario | Short chains (3-4 bones) | Long chains (5+ bones) |

## Code invocation

```csharp
TwoBoneIKSolver.SolveCCD(
    skeleton,      // SkeletonUpdater
    chainStart,    // chain start index (int)
    chainEnd,      // chain end index (int)
    target,        // Vector3 world target
    weight,        // float 0-1
    iterations     // int iteration count
);
```

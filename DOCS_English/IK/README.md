# Stride IK system — Index

## Documentation index

| Document | Content |
|----------|---------|
| [01 - System overview](01-Overview.md) | Architecture, execution flow, IkComponent configuration |
| [02 - TwoBoneIK](02-TwoBoneIK.md) | Two-bone IK (arms/legs) |
| [03 - LookAt IK](03-LookAt-IK.md) | Head gaze, eye tracking |
| [04 - CCD](04-CCD.md) | Cyclic coordinate descent (tails/tentacles) |
| [05 - FABRIK](05-FABRIK.md) | Forward-and-backward IK (long chains, fast convergence) |
| [06 - MultiAim](06-MultiAim.md) | Weapon aiming constraint |
| [07 - AnimatorController integration](07-AnimatorControllerIntegration.md) | IK Pass, OnStateIK callback |
| [08 - Gizmo visualization](08-GizmoVisualization.md) | IkTargetGizmo scene dragging |
| [09 - API reference](09-APIReference.md) | Full API signatures and parameters |
| [10 - Troubleshooting](10-Troubleshooting.md) | Common issue resolution |

## Quick start

1. Select the character Entity → add **IkComponent**.
2. Follow [01 - System overview](01-Overview.md) to find the bone names.
3. Pick the relevant document and configure the corresponding IK type.
4. Press F5 to test.

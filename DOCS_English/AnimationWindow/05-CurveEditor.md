# 05 — Curve Editor

## Interface

Right-side panel. Select a property row on the left → the curve for that property is displayed.

```
          value axis
           │
    max ───┼────────────────────────
           │    🔴──────🔴
           │   ╱         ╲
           │  ╱           ╲────🔵
    min ───┼────────────────────────
           └── 0s ── 2s ── 4s ─── time axis

    🔴 X (red)   🟢 Y (green)   🔵 Z (blue)   🟡 W (yellow)
```

## Sub-component colors

| Component | Color | Description |
|-----------|-------|-------------|
| X | 🔴 Red | Position.X / Rotation.X / Scale.X |
| Y | 🟢 Green | Position.Y / Rotation.Y / Scale.Y |
| Z | 🔵 Blue | Position.Z / Rotation.Z / Scale.Z |
| W | 🟡 Yellow | Rotation.W (Quaternion only) |

## Operations

| Operation | Method |
|-----------|--------|
| Drag sub-component point | Hold the dot and drag → only changes that component's value |
| Click empty space | Add a new keyframe |
| Right-click the dot | Delete keyframe |

## Interpolation type

Bottom dropdown box:

| Type | Effect |
|------|--------|
| **Linear** | Straight-line connection (default) |
| **Cubic** | Catmull-Rom smooth curve |
| **Constant** | Step jump |

## Grid

Y axis: 5 horizontal lines + value labels (auto-scaled)
X axis: one vertical line per second + time labels

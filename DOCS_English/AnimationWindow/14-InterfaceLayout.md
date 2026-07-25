# 14 — Animation Window Interface Layout

```
┌──────────────────────────────────────────────────────────────────────────┐
│ ① Toolbar                                                              │
│ ▶ ⏸ ⏹ ●REC 🧅 🎯 [EntityName] t=0.00/5.00s ──●── +Key +Evt ↩ ↪ 📂 💾 │
├─────────────┬────────────────────────────────────┬───────────────────────┤
│ ② Properties │ ③ Dopesheet Timeline              │ ④ Curve Editor         │
│(Properties) │ (Dopesheet Timeline)              │ (Curve Editor)         │
│             │                                    │                       │
│ Properties  │  0──1──2──3──4──5──6──7──│        │       value axis       │
│             │  │     ◆        ◆     ◆   │        │   max ─┬───────────   │
│ Position ◆──│──◆─────◆────────◆────────│        │        │    🔴──🔴    │
│ Rotation ◆──│──◆─────◆────────◆────────│        │        │   ╱      ╲   │
│ Scale    ◆──│──◆─────◆────────◆────────│        │   min ─┼───────────   │
│ ─────────   │  │     ◆        ◆     ◆   │        │        └──time axis   │
│             │                                    │                       │
│ ┌────────┐ │                                    │ ⑤ Interpolation Selector │
│ │Input   │+│                                    │ Interp: [Linear ▾]    │
│ └────────┘ │                                    │                       │
│ Events     │  [Footstep] [Land]                 │                       │
│             │                                    │                       │
└─────────────┴────────────────────────────────────┴───────────────────────┘
```

## Detailed breakdown of each area

### ① Toolbar

| Control | Name | Function |
|---------|------|----------|
| ▶ | **Play** | Play the animation |
| ⏸ | **Pause** | Pause playback |
| ⏹ | **Stop** | Stop and restore original position |
| ●REC | **Record** | Recording mode (auto-detect changes) |
| 🧅 | **Onion Skin** | 3D onion skin toggle |
| 🎯 | **Pick Entity** | Pick an entity from the scene |
| `[EntityName]` | **Target** | Name of the current recording target entity |
| `t=0.00/5.00s` | **Time Display** | Current time / total duration |
| `──●──` | **Time Slider** | Time slider |
| +Key | **Add Keyframe** | Manually add a keyframe |
| +Evt | **Add Event** | Add an animation event |
| ↩ | **Undo** | Undo (Ctrl+Z) |
| ↪ | **Redo** | Redo (Ctrl+Y) |
| 📂 | **Load** | Load animation |
| 💾 | **Save** | Save animation |

### ② Properties Panel

| Area | Description |
|------|-------------|
| **Properties** title | Property list title |
| 🟠 Orange row | Currently selected property |
| **Position** | Root position (X/Y/Z) |
| **Rotation** | Root rotation (X/Y/Z/W) |
| **Scale** | Root scale (X/Y/Z) |
| **Hips, Spine...** | Bone rotations (if any) |
| **Input box + + button** | Add a custom property |
| **Events** title | Animation event list title |
| Event row | Event name + delete button |

### ③ Dopesheet Timeline

| Element | Description |
|---------|-------------|
| Time ticks (0s, 1s...) | Horizontal second ruler |
| Vertical grid lines | One guide line per second |
| ◆ Gray diamond | Normal keyframe |
| 🔴 Pink diamond | Onion skin: previous keyframe |
| 🔵 Blue diamond | Onion skin: next keyframe |
| 🟠 Orange diamond | Keyframe of the selected property |
| ── Red vertical line (Playhead) | Current playhead position |

### ④ Curve Editor

| Element | Description |
|---------|-------------|
| Grid | Y axis: 5 horizontal lines + X axis: one vertical line per second |
| Y axis labels | Value range (min ~ max) |
| X axis labels | Time range (0s, 1s...) |
| 🔴 Red curve | X component curve |
| 🟢 Green curve | Y component curve |
| 🔵 Blue curve | Z component curve |
| 🟡 Yellow curve | W component curve (Quaternion) |
| ● Solid dot | Keyframe value point (color matches the component) |
| ╱ Semi-transparent tangent | Cubic tangent guide line |

### ⑤ Interpolation Selector

| Option | Effect |
|--------|--------|
| **Linear** | Straight-line connection |
| **Cubic** | Catmull-Rom smoothing |
| **Constant** | Step-wise |

## Keyboard shortcuts

| Shortcut | Function |
|----------|----------|
| Ctrl+F12 | Open the animation window |
| Ctrl+C | Copy keyframes |
| Ctrl+V | Paste keyframes |
| Ctrl+Z | Undo (↩ button) |
| Ctrl+Y | Redo (↪ button) |

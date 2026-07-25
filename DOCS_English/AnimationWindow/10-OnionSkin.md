# 10 — Onion Skin

## Feature

While dragging the time slider, the Dopesheet auto-highlights the **previous** and **next** keyframes.

## Colors

| Diamond color | Meaning |
|---------------|---------|
| 🔴 Pink | **Previous** keyframe (previous frame) |
| 🔵 Blue | **Next** keyframe (next frame) |
| ⚪ Gray | Other keyframes |
| 🟠 Orange | Keyframe of the currently selected property |

## Visual characteristics

Previous/next keyframe diamonds:
- Slightly larger (12px vs 10px)
- White border (2px vs 1px)

## Purpose

Helps animators:
- See the keyframe positions before and after the current time
- Judge the animation's rhythm and spacing
- Without dragging the time slider back and forth to check

## Note

Onion skin is currently limited to **Dopesheet visual highlighting**; there is no 3D semi-transparent entity rendering like in Unity.

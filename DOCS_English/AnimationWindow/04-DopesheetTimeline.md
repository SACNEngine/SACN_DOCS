# 04 — Dopesheet Timeline

## Interface

```
    0s     1s     2s     3s     4s     5s
    |      |      |      |      |      |
Position ──◆──────────◆──────────◆──────
Rotation ──◆────◆─────◆────◆─────◆──────
Scale    ──◆──────────◆──────────◆──────
          ▲                          ▲
    red playhead                 keyframe diamond
```

## Keyframe operations

| Operation | Method |
|-----------|--------|
| Create | +Key / ●REC auto |
| Drag to move | Hold the diamond ◆ and drag left/right |
| Delete | Right-click the diamond ◆ |
| Click timeline | Jump to that time |

## Onion skin

Auto-highlights while dragging the time slider:
- 🔴 **Pink diamond** = previous keyframe
- 🔵 **Blue diamond** = next keyframe
- ⚪ Gray = other keyframes
- 🟠 Orange = keyframe of the selected property

The previous/next frame diamonds are slightly larger (12px) and have a white border.

## Red playhead

Follows `CurrentTime`. Move it by dragging the time slider or clicking the Dopesheet.

## PPS constant

`PPS = 80` (every 80 pixels = 1 second). Time ticks are marked once per second.

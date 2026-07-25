# 09 — Copy, Paste and Undo

## Copy and paste keyframes

| Operation | Shortcut | Behavior |
|-----------|----------|----------|
| Copy | **Ctrl+C** | Select a property → copy all keyframes of that property |
| Copy (all) | Ctrl+C (when no property selected) | Copy all keyframes of all properties |
| Paste | **Ctrl+V** | Offset and paste at the current time position |

### Paste logic

```
Clipboard: [{t:0, v1}, {t:1, v2}, {t:2, v3}]
Minimum time = 0
Current time = 3.5
Offset = 3.5 - 0 = 3.5

Paste result:
  {t:3.5, v1}, {t:4.5, v2}, {t:5.5, v3}
```

## Undo/Redo

| Button | Function |
|--------|----------|
| ↩ | Undo (up to 50 steps) |
| ↪ | Redo |

### Implementation

Before each keyframe operation, the full state is serialized (JSON) and pushed onto the Undo stack. On undo, the previous snapshot is restored.

Operations covered:
- +Key adds keyframe
- Dopesheet drag/delete
- Curve editor add/delete/drag
- +Evt adds event / deletes event
- Ctrl+V paste

### Note

Independent from Stride's global `IUndoRedoService`. Does not affect scene-editing Undo.

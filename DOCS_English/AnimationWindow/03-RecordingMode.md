# 03 — Recording Mode

## Two modes

| Mode | Action | Recording method |
|------|--------|------------------|
| **Manual** | +Key button | Click the button to take a snapshot |
| **Auto** | ●REC button | Moving the entity auto-adds frames |

## Manual recording

1. Drag the time slider to the target time
2. Move/rotate/scale the entity in the scene
3. Click **+Key** → the current pose is recorded as a keyframe
4. Repeat steps 1-3

## Automatic recording (●REC)

1. Click **●REC** to enter recording mode
2. Drag the time slider to the target time
3. Move the entity in the scene → **a keyframe is created automatically**
4. No need to click +Key manually
5. Click ●REC again to exit

### How automatic recording works

Checks entity Transform changes at 60fps, epsilon=0.0001:

```csharp
// Check every frame
if (_rec && TargetEntity != null)
{
    var pos = TargetEntity.Transform.Position;
    if (Math.Abs(_lastPos.X - pos.X) > 0.0001f || ...)
    {
        _lastPos = pos;
        AddKey();  // auto-record
    }
}
```

### Play and record simultaneously

Click ▶ while in ●REC state:
- Time advances automatically
- The animation plays
- New keyframes are recorded automatically when you move the entity
- No duplicate recording after looping (playback-driven changes are excluded)

## Keyframe data

Each recording captures 3 curves:
- `[TransformComponent.Key].Position` → Vector3
- `[TransformComponent.Key].Rotation` → Quaternion
- `[TransformComponent.Key].Scale` → Vector3

## Duration auto-expansion

When a keyframe time exceeds the current Duration, it auto-expands by +1s.

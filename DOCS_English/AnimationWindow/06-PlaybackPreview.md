# 06 — Playback Preview

## How it works

```
Animation Window ▶ button
  → DispatcherTimer 60fps
  → Stopwatch calculates actual frame interval
  → each frame Apply(currentTime):
      ① evaluate Position/Rotation/Scale from AnimationClipBuilder
      ② set Entity.Transform directly
      ③ scene view updates in real time
```

## Playback controls

| Button | Behavior |
|--------|----------|
| ▶ | Save current pose → start playing |
| ⏸ | Pause (keep current pose) |
| ⏹ | Stop → restore original pose |

## Time advancement

- Uses `Stopwatch` to calculate the actual delta time
- `DispatcherTimer` acts as the 60fps tick source
- Automatically loops after time reaches Duration

## Time slider

Drag the slider or click the Dopesheet → the `CurrentTime` setter immediately calls `Apply()` → the entity jumps to that frame's pose. You can see the entity change without pressing play.

## Difference from Asset Preview

| | Animation Window ▶ | Asset Preview |
|---|---|---|
| Drive method | Directly modifies Transform | Engine AnimationProcessor |
| Scope | Entity being edited | Independent preview scene |
| Purpose | Preview while editing | Asset inspection |

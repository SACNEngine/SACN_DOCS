# 08 — Runtime Playback (PlayAnimationClip)

## Component

```csharp
[Display("Animation/Play Animation Clip")]
public class PlayAnimationClip : SyncScript
{
    public string ClipPath;          // file name, e.g. "WalkAnim.sdanimclip"
    public bool PlayOnStart = true;  // auto-play on start
    public AnimationRepeatMode RepeatMode = LoopInfinite;
}
```

## How to use

1. Select entity → Add Component → **Play Animation Clip**
2. **ClipPath** = `"WalkAnim.sdanimclip"`
3. F5 → the animation plays automatically

## File lookup

Automatically searches the following paths (upward from the run directory):

1. Same directory as the executable
2. Up to the solution root `Assets/` folder
3. Recursively search all `Assets/` subdirectories

## Playback mechanism

- Reads `.sdclip.json` or `.sdanimclip` YAML directly
- Each frame `Update()` evaluates the curves → sets `Entity.Transform`
- Does not depend on `Content.Load<>()` → no asset compilation required

## Code invocation

```csharp
// Manually control playback
var player = Entity.Components.Get<PlayAnimationClip>();
player.Play();   // start
player.Pause();  // pause
player.Stop();   // stop and reset to zero
```

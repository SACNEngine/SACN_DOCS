# 01 — Quick start

## Skeleton retargeting in 5 minutes

### Scenario

Character A (slim Mixamo) plays a walk animation, and Character B (fat Blender) is retargeted automatically.

### Step 1: Create an Avatar for both characters

In GameStudio:

1. **Solution Explorer → Add → Animation → Humanoid Avatar**
2. Name it `Hero_Avatar.sdavatar`
3. Drag in Character A's Skeleton → click **Auto Configure** in the property panel
4. BoneMap is filled in automatically (e.g. `Hips→0, Spine→1, Chest→2...`)
5. Repeat to create `Enemy_Avatar.sdavatar` for Character B

### Step 2: Attach AnimatorComponent

Add both `AnimatorComponent` and `ModelComponent` to both characters.

### Step 3: Write the script

```csharp
using Stride.Engine;
using Stride.Animations.Humanoid;

public class RetargetSetup : StartupScript
{
    public Entity sourceEntity;  // Character A (has animation)
    public Entity targetEntity;  // Character B (to be retargeted onto)

    public override void Start()
    {
        var src = sourceEntity.Get<AnimatorComponent>();
        var dst = targetEntity.Get<AnimatorComponent>();

        // Set the source → automatic retargeting
        dst.RetargetSource = src;

        // If dst has no Controller of its own, you can leave it unset
        // dst will retarget the animation automatically from src
    }
}
```

### Step 4: Run

Character B automatically plays Character A's animation. Different bone names, different skeleton proportions — all adapted automatically through Muscle Space.

### If auto-mapping fails

Some non-standard bone names will be reported as missing. Set the correspondence manually in the property panel:

```
BoneMap:
  Hips → 0
  Spine → 2
  Chest → 3
  ...
```

### If the retargeting result is abnormal

Check:

1. **Are both characters humanoid** — non-humanoid characters cannot be mapped
2. **Is the T-Pose correct** — characters must be in T-Pose (arms horizontal, legs straight)
3. **Are the Muscle Limits too small** — adjust the range in the property panel

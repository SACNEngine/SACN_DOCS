# 01 — Getting Started

## Build your first animation state machine in 5 minutes

### Step 1: Prepare animation clips

Animation clips are imported from FBX/glTF models:

1. **Solution Explorer** → right-click → **Add → Asset → Animation → Animation**
2. Select the model file and configure the animation parameters
3. Compile to generate the runtime `.sdclip` file

### Step 2: Create an AnimatorController

1. **Solution Explorer** → right-click → **Add → Asset → Animation → Animator Controller**
2. Name it `HeroController.sdctrl`
3. **Double-click** to open the visual editor

### Step 3: Add parameters

Click the `+` button in the bottom bar of the editor to add parameters:

| Parameter name | Type | Purpose |
|----------------|------|---------|
| Speed | Float | Drives Idle↔Walk↔Run |
| Jump | Trigger | Triggers jump |
| Attack | Trigger | Triggers attack |

### Step 4: Add states

Click the `+ State` button in the toolbar to create states, and double-click a state node to rename it:

- `Idle`
- `Walk`
- `Jump`
- `Attack`

### Step 5: Create transitions

Right-click a state → **Make Transition** → click the target state:

| From | To | Condition | ExitTime |
|------|----|-----------|----------|
| Idle | Walk | — | — |
| Walk | Idle | — | — |
| Any State | Jump | Jump / If | — |
| Any State | Attack | Attack / If | — |
| Jump | Idle | — | 0.9 |
| Attack | Idle | — | 0.85 |

### Step 6: Set the default state

Right-click **Idle** → **Set as Default State** (appears with a green border)

### Step 7: Attach to the character

1. Select the character Entity
2. **Add Component → Animator**
3. Drag `Controller` onto `HeroController.sdctrl`

### Step 8: Write the control script

```csharp
using Stride.Engine;
using Stride.Input;
using Stride.Animations.Animator;

public class HeroControl : SyncScript
{
    private AnimatorComponent animator;

    public override void Start() => animator = Entity.Get<AnimatorComponent>();

    public override void Update()
    {
        if (animator == null) return;

        var input = Input;

        // Speed drives Idle/Walk
        float speed = 0f;
        if (input.IsKeyDown(Keys.W)) speed = input.IsKeyDown(Keys.LeftShift) ? 8f : 4f;
        animator.SetFloat("Speed", speed);

        // Jump
        if (input.IsKeyPressed(Keys.Space))
            animator.SetTrigger("Jump");

        // Attack
        if (input.IsMouseButtonPressed(MouseButton.Left))
            animator.SetTrigger("Attack");
    }
}
```

### Run

Press `W` → Idle transitions to Walk. Press `W+Shift` → accelerate to Run. Press `Space` → jump. Left mouse button → attack.

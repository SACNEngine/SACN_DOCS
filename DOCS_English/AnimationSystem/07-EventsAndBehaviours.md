# 07 — Animation Events and State Behaviours

## AnimationEvent

Fires a callback at a specific time point of an animation.

### Defining events on an AnimationClip

```csharp
clip.Events.Add(new AnimationEvent
{
    NormalizedTime = 0.3f,          // Fire at 30% of the animation
    FunctionName = "Footstep",      // Event name
    StringParameter = "Left",       // Optional string parameter
    FloatParameter = 1.0f,          // Optional float parameter
    IntParameter = 0,               // Optional integer parameter
    FireOncePerLoop = true,         // Fire only once per loop
});
```

### Properties

| Property | Type | Default | Description |
|----------|------|---------|-------------|
| `NormalizedTime` | float | 0 | Fire time [0, 1] |
| `FunctionName` | string | null | Event name (e.g. "Footstep") |
| `StringParameter` | string | null | Optional string |
| `FloatParameter` | float | 0 | Optional float |
| `IntParameter` | int | 0 | Optional integer |
| `ObjectReferenceParameter` | object | null | Optional object reference |
| `FireOncePerLoop` | bool | true | Fire only once per loop |

### Receiving events — Method 1: Event subscription

```csharp
animator.AnimationEventFired += (evt) =>
{
    switch (evt.FunctionName)
    {
        case "Footstep":
            Audio.PlayFootstep(evt.StringParameter);  // "Left" or "Right"
            break;
        case "Attack":
            EnableHitbox();
            break;
        case "Land":
            PlayLandingEffect();
            break;
    }
};
```

### Receiving events — Method 2: IAnimationEventReceiver interface

Implement the interface on any EntityComponent:

```csharp
public class FootstepHandler : SyncScript, IAnimationEventReceiver
{
    public void OnAnimationEvent(AnimationEvent evt)
    {
        if (evt.FunctionName == "Footstep")
            Console.WriteLine($"Footstep: {evt.StringParameter}");
    }
}
```

---

## StateMachineBehaviour

State lifecycle callbacks, similar to Unity's StateMachineBehaviour.

### Base class

```csharp
public abstract class StateMachineBehaviour
{
    public virtual void OnStateEnter(AnimatorComponent animator, AnimatorStateInfo info, int layer) { }
    public virtual void OnStateExit(AnimatorComponent animator, AnimatorStateInfo info, int layer) { }
    public virtual void OnStateUpdate(AnimatorComponent animator, AnimatorStateInfo info, int layer) { }
    public virtual void OnStateIK(AnimatorComponent animator, AnimatorStateInfo info, int layer) { }
    public virtual void OnStateMachineEnter(AnimatorComponent animator, int layer) { }
    public virtual void OnStateMachineExit(AnimatorComponent animator, int layer) { }
}
```

### Usage example: Attack behaviour

```csharp
public class AttackBehaviour : StateMachineBehaviour
{
    public override void OnStateEnter(AnimatorComponent animator, AnimatorStateInfo info, int layer)
    {
        // Entering attack state: enable weapon collider
        var weapon = animator.Entity.Get<WeaponComponent>();
        weapon?.EnableHitbox();

        // Play sword swing sound
        AudioSystem.Play("SwordSwing");
    }

    public override void OnStateExit(AnimatorComponent animator, AnimatorStateInfo info, int layer)
    {
        // Leaving attack state: disable weapon collider
        var weapon = animator.Entity.Get<WeaponComponent>();
        weapon?.DisableHitbox();
    }

    public override void OnStateUpdate(AnimatorComponent animator, AnimatorStateInfo info, int layer)
    {
        // Update every frame: e.g. detect hit during the second half of the attack
        if (info.NormalizedTime > 0.5f)
            CheckHit();
    }
}

// Attach
attackState.Behaviours.Add(new AttackBehaviour());
```

### Usage example: Landing detection

```csharp
public class LandBehaviour : StateMachineBehaviour
{
    public override void OnStateEnter(AnimatorComponent animator, AnimatorStateInfo info, int layer)
    {
        // Play effect on landing
        SpawnLandEffect(animator.Entity.Transform.Position);
    }
}

jumpState.Behaviours.Add(new LandBehaviour());
```

### Callback timing

```
Enter state   OnStateEnter ──→ OnStateUpdate (every frame) ──→ transition starts →
             OnStateExit ──→ target state OnStateEnter ──→ ...
```

# 07 — AnimatorController integration

## Enabling the IK Pass

1. Open the AnimatorController visual editor.
2. Select a Layer → property panel → **IkPass = true**.
3. AnimatorProcessor automatically calls `OnStateIK` every frame.

## OnStateIK callback

```csharp
public class MyIKBehaviour : StateMachineBehaviour
{
    public override void OnStateIK(
        AnimatorComponent animator,
        AnimatorStateInfo info,
        int layer)
    {
        var ik = animator.Entity.Components.Get<IkComponent>();
        if (ik == null) return;

        // Set the IK target
        ik.Chains[0].TargetPosition = GetTargetPosition();
    }
}
```

## Switching IK per state

```csharp
public class AimIKBehaviour : StateMachineBehaviour
{
    public override void OnStateEnter(...)
    {
        // Entering aim state, activate right-hand IK
        animator.Entity.Components.Get<IkComponent>().Chains[1].Weight = 1f;
    }

    public override void OnStateExit(...)
    {
        // Leaving aim state, disable right-hand IK
        animator.Entity.Components.Get<IkComponent>().Chains[1].Weight = 0f;
    }
}
```

## Execution order

```
AnimatorProcessor.Draw():
  ① Tick State Machines        → transition evaluation
  ② Compose Layers             → motion blending
  ③ AnimationUpdater.Update    → write to skeleton
  ④ IK Pass (this module)      → OnStateIK callback
  ⑤ EndFrameReset              → consume Trigger
```

## Notes

- `OnStateIK` is called **after** animation blending, so it can read the current bone pose directly.
- `info.NormalizedTime` gives the current state progress.
- The `layer` parameter distinguishes multi-layer IK.
- IK targets should be updated every frame; otherwise the previous frame's stale value is used.

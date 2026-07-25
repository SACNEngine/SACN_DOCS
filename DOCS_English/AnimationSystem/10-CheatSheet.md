# 10 — Cheat Sheet

## Visual editor shortcuts

| Action | Shortcut / Method |
|--------|-------------------|
| Create state | Toolbar `+ State` |
| Move state | Left-click drag |
| Rename state | Double-click node |
| Select state | Left-click |
| Multi-select states | Ctrl+click |
| Delete state | Delete key |
| Create transition | Right-click source state → Make Transition → click target |
| Delete transition | Right-click transition arrow |
| Set default state | Right-click state → Set as Default State |
| Zoom | Mouse wheel |
| Pan | Middle-click drag |
| Cancel | Esc |

## Parameter operations

```csharp
animator.SetFloat("Speed", 5f);
animator.SetBool("IsAiming", true);
animator.SetTrigger("Jump");
animator.SetInt("Weapon", 1);

float s = animator.GetFloat("Speed");
bool b  = animator.GetBool("IsAiming");
int i   = animator.GetInt("Weapon");
```

## State queries

```csharp
var info = animator.GetCurrentAnimatorStateInfo(0);
// info.StateName, info.NormalizedTime, info.LoopCount

bool trans = animator.IsInTransition(0);
var tInfo  = animator.GetTransitionInfo(0);
```

## Condition modes

| Mode | Float | Bool | Trigger |
|------|-------|------|---------|
| `If` | >0 | true | fired |
| `IfNot` | <0 | false | — |
| `Greater` | >thresh | — | — |
| `Less` | <thresh | — | — |
| `Equals` | ==thresh | ==thresh | — |
| `NotEqual` | !=thresh | !=thresh | — |

## Transition easing

| Mode | Curve |
|------|-------|
| `Linear` | Constant speed |
| `EaseIn` | Slow → fast |
| `EaseOut` | Fast → slow |
| `EaseInOut` | Slow → fast → slow |

## File extensions

| Extension | Description |
|-----------|-------------|
| `.sdctrl` | AnimatorController asset |
| `.sdanim` | Animation import settings |
| `.sdclip` | Compiled runtime AnimationClip |
| `.sdskel` | Skeleton bone definition |

## Common transition durations

| Transition | Duration |
|------------|----------|
| Idle↔Walk | 0.2s |
| Walk↔Run | 0.15s |
| Move→Jump | 0.1s |
| Jump→Land | 0.15s |
| →Attack | 0.05s |
| Attack→Idle | 0.15s |

## Layer blending modes

| Mode | Use case |
|------|----------|
| `Override` | Upper-body weapon, aiming |
| `Additive` | Breathing animation, hurt shake |

## Motion types

| Type | Use case |
|------|----------|
| `AnimationClipMotion` | Single animation |
| `BlendTree1D` | Speed-driven movement |
| `BlendTree2D` | Direction-driven movement |
| `BlendTreeDirect` | Script-controlled weights |

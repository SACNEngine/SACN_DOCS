# 03 — Parameter System

## Four parameter types

| Type | Purpose | Typical scenarios |
|------|---------|-------------------|
| **Float** | Continuous value | Speed, direction, HP percentage |
| **Int** | Integer | Weapon type, combo count, level |
| **Bool** | On/off state | Whether grounded, whether aiming, whether holding a shield |
| **Trigger** | One-shot signal | Jump, attack, hit, reload |

## Important Trigger characteristics

A Trigger is **auto-consumed**: after `SetTrigger("Jump")`, the parameter is true for the **current frame**, and is reset to false immediately after being read by a transition condition.

```csharp
// ✅ Correct usage: check input each frame, trigger on press
if (Input.IsKeyPressed(Keys.Space))
    animator.SetTrigger("Jump");

// ❌ Incorrect usage: holding continuously re-triggers every frame
if (Input.IsKeyDown(Keys.Space))
    animator.SetTrigger("Jump");  // Triggers every frame!
```

## Adding parameters in the visual editor

1. Click the `+` button in the bottom bar
2. The parameter is named `P1` by default, with the Float type
3. Modify the name, type, and default value in the property panel

## Defining parameters in code

```csharp
var ctrl = new AnimatorController();

// Float parameter
var speed = new AnimatorParameter
{
    Name = "Speed",
    Type = AnimatorParameterType.Float,
    DefaultFloat = 0f
};

// Trigger parameter
var jump = new AnimatorParameter
{
    Name = "Jump",
    Type = AnimatorParameterType.Trigger
};

// Bool parameter
var grounded = new AnimatorParameter
{
    Name = "IsGrounded",
    Type = AnimatorParameterType.Bool,
    DefaultBool = true
};

// Int parameter
var weapon = new AnimatorParameter
{
    Name = "WeaponType",
    Type = AnimatorParameterType.Int,
    DefaultInt = 0
};

ctrl.Parameters.Add(speed);
ctrl.Parameters.Add(jump);
ctrl.Parameters.Add(grounded);
ctrl.Parameters.Add(weapon);
```

## Reading and writing parameters in scripts

```csharp
var animator = Entity.Get<AnimatorComponent>();

// Write
animator.SetFloat("Speed", 5.0f);
animator.SetInt("WeaponType", 2);
animator.SetBool("IsGrounded", true);
animator.SetTrigger("Jump");

// Read
float speed = animator.GetFloat("Speed");
int weapon = animator.GetInt("WeaponType");
bool grounded = animator.GetBool("IsGrounded");
```

## Parameter storage mechanism

`AnimatorParameterStore` is the runtime parameter storage container:

- Internally uses `Dictionary<Guid, T>` to store values separately by type
- Thread-safe: reads are safe, writes must be on the main thread
- Triggers consumed at the end of each frame are automatically reset
- Clears already-read Triggers via `EndFrameReset()`

## Parameters driving transitions

Transition conditions use parameters to decide when to fire:

```csharp
new AnimatorTransition
{
    DestinationStateId = runState.Id,
    Conditions =
    {
        new AnimatorCondition
        {
            Parameter = speedParam,         // Check the Speed parameter
            Mode = AnimatorConditionMode.Greater,  // > threshold
            FloatThreshold = 3.0f           // Transition when Speed > 3.0
        }
    }
}
```

### Condition mode reference

| Mode | Float/Int | Bool | Trigger |
|------|-----------|------|---------|
| `If` | value > threshold | value == true | true when fired |
| `IfNot` | value < threshold | value == false | — |
| `Greater` | value > threshold | — | — |
| `Less` | value < threshold | — | — |
| `Equals` | value == threshold | value == threshold | — |
| `NotEqual` | value != threshold | value != threshold | — |

## Parameters driving blend trees

BlendTree also uses parameters to determine the blend position:

```csharp
var blend = new BlendTree1D
{
    BlendParameter = speedParam,    // Speed parameter drives the blend
    Thresholds = { 0f, 3f, 7f },
    Motions = { idleClip, walkClip, runClip }
};
```

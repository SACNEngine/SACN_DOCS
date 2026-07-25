# 04 — States and Transitions

## State machine structure

```
AnimatorController
└── Layers[]
    └── StateMachine
        ├── DefaultStateId     ← The first state activated when entering the state machine
        ├── States[]           ← States + sub-state machines
        │   ├── AnimatorState
        │   │   ├── Motion     ← Animation played (Clip or BlendTree)
        │   │   ├── Transitions[] ← Outgoing transitions
        │   │   ├── Speed      ← Playback speed
        │   │   └── Behaviours[] ← State behaviour callbacks
        │   └── AnimatorStateMachine  ← Nested sub-state machine
        └── AnyStateTransitions[] ← Transitions that can fire from any state
```

## State (AnimatorState)

### Properties

| Property | Type | Default | Description |
|----------|------|---------|-------------|
| `Name` | string | null | State name (shown in the editor) |
| `Motion` | Motion | null | Animation played (AnimationClipMotion or BlendTree) |
| `Speed` | float | 1.0 | Playback speed multiplier |
| `SpeedMultiplier` | AnimatorParameter | null | Optional parameter-driven speed multiplier |
| `CycleOffset` | float | 0.0 | Normalized start offset when entering the state |
| `Mirror` | bool | false | Mirror the animation |
| `WriteDefaults` | bool | false | Whether to write defaults to non-animated channels |
| `Transitions` | List\<AnimatorTransition\> | [] | List of outgoing transitions |
| `Behaviours` | List\<StateMachineBehaviour\> | [] | State lifecycle callbacks |
| `Tags` | List\<string\> | [] | Tags (for script lookup) |

### Creating a state

**Visual editor**: Click the `+ State` button

**Code**:
```csharp
var state = new AnimatorState
{
    Name = "Idle",
    Motion = new AnimationClipMotion(idleClip),
    Speed = 1f,
    CycleOffset = 0f,
};
```

### Sub-state machine

```csharp
var subSm = new AnimatorStateMachine
{
    Name = "Combat",
    DefaultStateId = combatIdleState.Id,
};
subSm.States.Add(combatIdleState);
subSm.States.Add(attackState);

// Add to the parent state machine
parentSM.States.Add(subSm);
```

---

## Transition (AnimatorTransition)

### Properties

| Property | Type | Default | Description |
|----------|------|---------|-------------|
| `DestinationStateId` | Guid | Empty | Target state Id |
| `Name` | string | null | Transition name (optional) |
| `HasExitTime` | bool | true | Whether exit time must be satisfied to fire |
| `ExitTime` | float | 0.75 | Normalized exit time [0, 1] |
| `FixedDuration` | bool | false | Ignore ExitTime, fire immediately when conditions are met |
| `TransitionDuration` | float | 0.25 | Cross-fade time (seconds) |
| `TransitionOffset` | float | 0.0 | Start offset of the target state |
| `InterruptionSource` | enum | None | Interruption policy |
| `RequireAllConditions` | bool | true | Conditions AND(true) / OR(false) |
| `Conditions` | List\<AnimatorCondition\> | [] | List of firing conditions |
| `Easing` | enum | Linear | Easing curve |
| `IsMute` | bool | false | Mute the source layer during the transition |

### ExitTime details

```
ExitTime=0.75  → The current state must play to at least 75% before transition conditions are evaluated
HasExitTime=false → Transition fires immediately when conditions are met
FixedDuration=true → Ignore ExitTime, fire immediately when conditions are met
```

### Transition easing

```csharp
new AnimatorTransition
{
    Easing = TransitionEasing.EaseInOut,  // Smooth start and end
}
```

| Mode | Curve |
|------|-------|
| `Linear` | Constant speed |
| `EaseIn` | Slow → fast |
| `EaseOut` | Fast → slow |
| `EaseInOut` | Slow → fast → slow |

### Condition (AnimatorCondition)

```csharp
new AnimatorCondition
{
    Parameter = speedParam,               // Parameter to check
    Mode = AnimatorConditionMode.Greater, // Comparison mode
    FloatThreshold = 3.0f,               // Threshold
}
```

### Interruption policy (InterruptionSource)

| Value | Behavior |
|-------|----------|
| `None` | Current transition cannot be interrupted |
| `Source` | Can be interrupted by the source state's transitions |
| `Destination` | Can be interrupted by the target state's transitions |
| `SourceThenDestination` | Ordered interruption |

### Any State transition

A transition that can fire from any state:

```csharp
sm.AnyStateTransitions.Add(new AnimatorTransition
{
    DestinationStateId = deathState.Id,
    HasExitTime = false,
    Conditions = { new AnimatorCondition { Parameter = healthZeroParam, Mode = AnimatorConditionMode.If } }
});
```

### Code example: Complete state machine

```csharp
// States
var idle = new AnimatorState { Name = "Idle", Motion = idleClip };
var walk = new AnimatorState { Name = "Walk", Motion = walkClip };
var jump = new AnimatorState { Name = "Jump", Motion = jumpClip };

// Transitions
idle.Transitions.Add(new AnimatorTransition
{
    DestinationStateId = walk.Id,
    Conditions = { new AnimatorCondition { Parameter = speed, Mode = AnimatorConditionMode.Greater, FloatThreshold = 0.1f } }
});

walk.Transitions.Add(new AnimatorTransition
{
    DestinationStateId = idle.Id,
    Conditions = { new AnimatorCondition { Parameter = speed, Mode = AnimatorConditionMode.Less, FloatThreshold = 0.1f } }
});

jump.Transitions.Add(new AnimatorTransition
{
    DestinationStateId = idle.Id,
    HasExitTime = true, ExitTime = 0.9f,
    TransitionDuration = 0.15f,
});

// State machine
var sm = new AnimatorStateMachine { Name = "Base", DefaultStateId = idle.Id };
sm.States.Add(idle); sm.States.Add(walk); sm.States.Add(jump);

// Layer
ctrl.Layers.Add(new AnimatorControllerLayer { Name = "Base", StateMachine = sm, DefaultWeight = 1f });
```

## Runtime queries

```csharp
// Current state
var info = animator.GetCurrentAnimatorStateInfo(0);
// info.StateName, info.NormalizedTime, info.Speed, info.LoopCount

// Whether in transition
bool trans = animator.IsInTransition(0);

// Transition info
var tInfo = animator.GetTransitionInfo(0);

// State change event
animator.StateChanged += (layer, oldState, newState) =>
    Console.WriteLine($"{oldState.StateName} → {newState.StateName}");
```

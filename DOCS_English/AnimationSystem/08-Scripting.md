# 08 — Scripting API Reference

## AnimatorComponent

The runtime animation component, attached to an Entity.

### Parameter operations

```csharp
// Write
void SetFloat(string name, float value)
void SetInt(string name, int value)
void SetBool(string name, bool value)
void SetTrigger(string name)

// Read
float GetFloat(string name)
int   GetInt(string name)
bool  GetBool(string name)
```

### State queries

```csharp
// Current state info
AnimatorStateInfo GetCurrentAnimatorStateInfo(int layerIndex)

// Next state info (valid during transition)
AnimatorStateInfo GetNextAnimatorStateInfo(int layerIndex)

// Transition info
AnimatorTransitionInfo GetTransitionInfo(int layerIndex)

// Whether in transition
bool IsInTransition(int layerIndex)

// Current state name
string GetCurrentStateName(int layerIndex)
```

### Events

```csharp
// Animation event callback
event Action<AnimationEvent> AnimationEventFired

// State change callback
event Action<int, AnimatorStateInfo, AnimatorStateInfo> StateChanged

// Root Motion callback
event Action<AnimatorComponent> OnAnimatorMove
```

### Properties

| Property | Type | Default | Description |
|----------|------|---------|-------------|
| `Controller` | AnimatorController | null | The animation controller |
| `Skeleton` | Skeleton | null | Skeleton (auto-detected) |
| `ApplyRootMotion` | bool | false | Extract root motion |
| `UpdateMode` | AnimatorUpdateMode | Normal | Update mode |
| `CullingMode` | AnimatorCullingMode | AlwaysAnimate | Culling mode |
| `LayerWeightOverrides` | Dictionary | {} | Layer weight overrides |

### AnimatorUpdateMode

| Value | Description |
|-------|-------------|
| `Normal` | Update every frame |
| `AnimatePhysics` | Update at fixed timestep |
| `UnscaledTime` | Manual update |

### AnimatorCullingMode

| Value | Description |
|-------|-------------|
| `AlwaysAnimate` | Always update |
| `CullUpdateTransforms` | Stop transform updates when off-screen |
| `CullCompletely` | Stop completely when off-screen |

---

## AnimatorController

The top-level container holding parameters and layers.

### Properties

| Property | Type | Description |
|----------|------|-------------|
| `Parameters` | List\<AnimatorParameter\> | List of parameters |
| `Layers` | List\<AnimatorControllerLayer\> | List of layers |

### Methods

```csharp
AnimatorParameter FindParameter(string name)
AnimatorParameter FindParameterById(Guid id)
AnimatorControllerLayer FindLayer(string name)
List<string> Validate()
AnimatorController Clone()
```

---

## AnimatorState

### Properties

```csharp
Guid Id                    // Unique identifier
string Name                // State name
Motion Motion              // Animation played (Clip or BlendTree)
float Speed                // Playback speed multiplier (default 1.0)
AnimatorParameter SpeedMultiplier  // Parameter-driven speed multiplier
float CycleOffset          // Start offset when entering the state [0,1]
bool Mirror                // Mirror the animation
bool WriteDefaults         // Write defaults
List<AnimatorTransition> Transitions  // Outgoing transitions
List<StateMachineBehaviour> Behaviours // State behaviours
List<string> Tags          // Tags
```

---

## AnimatorTransition

### Properties

```csharp
Guid DestinationStateId     // Target state Id
string Name                 // Transition name
bool HasExitTime            // Whether there is an exit time
float ExitTime              // Normalized exit time [0,1]
bool FixedDuration          // Ignore ExitTime
float TransitionDuration    // Transition duration (seconds)
float TransitionOffset      // Target state offset
TransitionInterruptionSource InterruptionSource  // Interruption policy
bool RequireAllConditions   // AND/OR conditions
List<AnimatorCondition> Conditions  // List of conditions
TransitionEasing Easing     // Easing curve
bool IsMute                 // Mute during transition
```

---

## BlendTree1D

```csharp
AnimatorParameter BlendParameter   // Driving parameter
List<float> Thresholds             // Threshold array
List<IMotion> Motions             // Child animation list
```

## BlendTree2D

```csharp
AnimatorParameter BlendParameterX  // X-axis parameter
AnimatorParameter BlendParameterY  // Y-axis parameter
List<Vector2> Positions           // Sample point positions
List<IMotion> Motions            // Child animation list
```

## BlendTreeDirect

```csharp
float[] Weights                   // Direct weight array
List<IMotion> Motions            // Child animation list
```

---

## AnimatorStateInfo

```csharp
string StateName        // State name
Guid StateId            // State Id
float NormalizedTime    // Normalized time [0,1]
float LengthSeconds     // Clip duration (seconds)
float Speed             // Current speed
float SpeedMultiplier   // Speed multiplier
int LoopCount           // Loop count
string Tag              // First tag
bool IsValid            // Whether valid
float ElapsedSeconds    // Seconds played
bool IsDone             // Whether playback finished
```

## AnimatorTransitionInfo

```csharp
AnimatorStateInfo FromState   // Source state
AnimatorStateInfo ToState     // Target state
bool IsTransitioning          // Whether in transition
float Progress                // Transition progress [0,1]
float DurationSeconds         // Transition duration
```

---

## AnimatorOverrideController

```csharp
AnimatorController BaseController        // Base controller
List<ClipOverridePair> Overrides         // List of override pairs

// Apply overrides
AnimatorController ApplyOverrides()
```

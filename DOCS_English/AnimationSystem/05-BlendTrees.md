# 05 — Blend Trees

## Overview

Blend trees use **parameters to smoothly blend between multiple animations**, replacing discrete animation switches.

## BlendTree1D — One-dimensional blend space

Use a single Float parameter to smoothly transition between multiple animations.

### Code example

```csharp
var locomotion = new BlendTree1D
{
    BlendParameter = speedParam,    // Driving parameter
    Thresholds = { 0f, 3f, 7f },   // Threshold array (must equal the number of Motions)
    Motions =
    {
        new AnimationClipMotion(idleClip),  // Animation at Speed=0
        new AnimationClipMotion(walkClip),  // Animation at Speed=3
        new AnimationClipMotion(runClip),   // Animation at Speed=7
    }
};
```

### Blend logic

| Speed value | Blend result |
|-------------|--------------|
| 0 | 100% Idle |
| 1.5 | 50% Idle + 50% Walk |
| 3 | 100% Walk |
| 5 | 50% Walk + 50% Run |
| 7 | 100% Run |

## BlendTree2D — Two-dimensional blend space

Use **two Float parameters** to blend on a 2D plane. Suitable for character movement direction.

### Code example

```csharp
var directionBlend = new BlendTree2D
{
    BlendParameterX = velXParam,   // X-axis parameter
    BlendParameterY = velZParam,   // Y-axis parameter
    Positions =
    {
        new Vector2( 0f,  1f),   // Forward
        new Vector2( 0f, -1f),   // Backward
        new Vector2(-1f,  0f),   // Strafe left
        new Vector2( 1f,  0f),   // Strafe right
        new Vector2( 0f,  0f),   // Idle
    },
    Motions =
    {
        new AnimationClipMotion(walkForward),
        new AnimationClipMotion(walkBack),
        new AnimationClipMotion(strafeLeft),
        new AnimationClipMotion(strafeRight),
        new AnimationClipMotion(idle),
    }
};
```

### Blend algorithm

**Gradient Band Blending**: weight = `1 / distance²`, then normalized. The closer the parameter is to a sample point, the greater that point's animation weight.

## BlendTreeDirect — Direct weights

The script directly sets the weight of each child animation, without parameter driving.

```csharp
var direct = new BlendTreeDirect();
// In Update:
direct.Weights = new float[] { 0.2f, 0.3f, 0.5f };
```

## Nested blend trees

Blend trees can be nested — a BlendTree's child can be another BlendTree:

```csharp
var outerBlend = new BlendTree1D { /* ... */ };
outerBlend.Motions.Add(new BlendTree2D { /* child blend tree */ });
```

## Motion types

All blend tree child nodes are of the `Motion` type:

| Type | Description |
|------|-------------|
| `AnimationClipMotion` | A single animation Clip |
| `BlendTree1D` | One-dimensional blend space |
| `BlendTree2D` | Two-dimensional blend space |
| `BlendTreeDirect` | Direct weight blending |

`Motion` is an abstract base class implementing the `IMotion` interface, supporting `[DataContract(Inherited=true)]` polymorphic serialization.

## Using blend trees in states

```csharp
var moveState = new AnimatorState
{
    Name = "Locomotion",
    Motion = locomotionBlend,   // Blend tree as Motion
    Speed = 1f,
};
```

## Comparison vs Unity

| Feature | Unity | Stride |
|---------|-------|--------|
| 1D BlendTree | ✅ | ✅ |
| 2D Simple Directional | ✅ | ✅ Gradient band |
| 2D Freeform Directional | ✅ Delaunay triangulation | ⚠️ Gradient band (not triangulated) |
| 2D Freeform Cartesian | ✅ | ✅ Gradient band |
| Direct BlendTree | ✅ | ✅ |

## BlendTree2D (new)

A 2D blend tree blends animations on a two-dimensional plane based on two Float parameters.

### Visual editor

Select a BlendTree2D-type state → the bottom of the state machine editor shows:
- X/Y parameter dropdowns
- 10×10 grid canvas
- Orange dot = blend point (draggable)
- +Point adds a point; click the canvas to position it

### Configuration

```
Type:         BlendTree2D
BlendParameterX:  Speed    (X-axis parameter)
BlendParameterY:  Angle    (Y-axis parameter)
Positions:    [(0.3,0.5), (0.7,0.8), ...]
Motions:      [Walk, Run, ...]
```

### Runtime behavior

Based on the values of BlendParameterX and BlendParameterY, gradient blending is applied to neighboring points on the 2D plane.

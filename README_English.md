# Stride Animation System

A complete animation system built for the [Stride Engine](https://github.com/stride3d/stride), matching the level of Engine Mecanim.

> **Engine Version**: Stride 4.4.0+ &nbsp;|&nbsp; **License**: MIT

---

## Table of Contents

- [Project Overview](#project-overview)
- [Subsystem Navigation](#subsystem-navigation)
- [Quick Start](#quick-start)
- [Feature Overview](#feature-overview)
- [Source Navigation](#source-navigation)

---

## Project Overview

This project adds 4 core animation subsystems to the Stride Engine:

| Subsystem | Directory | Engine Equivalent | Docs |
|-----------|-----------|------------------|:----:|
| **Animation Window** | [AnimationWindow/](AnimationWindow/) | Animation Window | 14 |
| **State Machine Editor** | [AnimationSystem/](AnimationSystem/) | Animator Controller | 10 |
| **IK System** | [IK/](IK/) | Animation Rigging | 11 |
| **Humanoid Retargeting** | [Retargeting/](Retargeting/) | Humanoid Avatar | 4 |

---

## Subsystem Navigation

### 📐 Animation Window

> Create animations inside the engine. Record Entity Transform + skeletal animation, edit on a Dopesheet timeline, use the curve editor, and save as `.sdanimclip` assets.

**Core Workflow**: Record → Edit → Preview → Save → Runtime Playback

📖 [Animation Window Index](AnimationWindow/README.md)

| Document | Content |
|----------|---------|
| [01-Overview](AnimationWindow/01-Overview.md) | Architecture, layout, data model, latest features |
| [02-QuickStart](AnimationWindow/02-QuickStart.md) | Create your first animation in 5 minutes |
| [03-RecordingMode](AnimationWindow/03-RecordingMode.md) | Manual recording (+Key) / Auto recording (●REC) |
| [04-DopesheetTimeline](AnimationWindow/04-DopesheetTimeline.md) | Keyframe operations, dragging, deletion, onion skin |
| [05-CurveEditor](AnimationWindow/05-CurveEditor.md) | Curve view, sub-component dragging, interpolation switching |
| [06-PlaybackPreview](AnimationWindow/06-PlaybackPreview.md) | Editor preview, entity synchronization |
| [07-SaveAndLoad](AnimationWindow/07-SaveAndLoad.md) | .sdanimclip assets, Asset View integration |
| [08-RuntimePlayback](AnimationWindow/08-RuntimePlayback.md) | PlayAnimationClip script component |
| [09-CopyPasteUndo](AnimationWindow/09-CopyPasteUndo.md) | Ctrl+C/V, Ctrl+Z |
| [10-OnionSkin](AnimationWindow/10-OnionSkin.md) | Adjacent-frame Dopesheet highlighting |
| [11-AnimatorControllerIntegration](AnimationWindow/11-AnimatorControllerIntegration.md) | Using animations in state machines |
| [12-APIReference](AnimationWindow/12-APIReference.md) | Complete C# API |
| [13-Troubleshooting](AnimationWindow/13-Troubleshooting.md) | Common issues and solutions |
| [14-InterfaceLayout](AnimationWindow/14-InterfaceLayout.md) | Named regions and description of the window |

---

### 🎮 Animation System (State Machine Editor / AnimatorController)

> Visual state machine editing. Drag state nodes, connect transitions, configure BlendTree blend spaces, drive with parameters.

**Core Workflow**: State machine design → BlendTree configuration → Parameter conditions → Multi-layer management

📖 [Animation System Index](AnimationSystem/README.md)

| Document | Content |
|----------|---------|
| [01-GettingStarted](AnimationSystem/01-GettingStarted.md) | Getting started guide |
| [02-VisualEditor](AnimationSystem/02-VisualEditor.md) | In-depth visual editor (with latest features) |
| [03-Parameters](AnimationSystem/03-Parameters.md) | Float/Int/Bool/Trigger parameters |
| [04-StatesAndTransitions](AnimationSystem/04-StatesAndTransitions.md) | States and transitions |
| [05-BlendTrees](AnimationSystem/05-BlendTrees.md) | 1D/2D/Direct blend trees (with 2D visualization) |
| [06-LayersAndMasks](AnimationSystem/06-LayersAndMasks.md) | Animation layers and masks |
| [07-EventsAndBehaviours](AnimationSystem/07-EventsAndBehaviours.md) | Events and StateMachineBehaviour |
| [08-Scripting](AnimationSystem/08-Scripting.md) | Script control |
| [09-Examples](AnimationSystem/09-Examples.md) | Complete examples |
| [10-CheatSheet](AnimationSystem/10-CheatSheet.md) | Cheat sheet |

---

### 🦴 IK System (Inverse Kinematics)

> 5 IK solvers. Runtime + editor preview, Gizmo visualization, AnimatorController IK Pass integration.

📖 [IK System Index](IK/README.md)

| Document | Content |
|----------|---------|
| [01-Overview](IK/01-Overview.md) | Architecture, components, cheat sheet |
| [02-TwoBoneIK](IK/02-TwoBoneIK.md) | Two-bone IK (arm/leg) |
| [03-LookAt-IK](IK/03-LookAt-IK.md) | Head look-at |
| [04-CCD](IK/04-CCD.md) | Cyclic Coordinate Descent (tail/tentacle) |
| [05-FABRIK](IK/05-FABRIK.md) | Forward And Backward Reaching IK (long chains) |
| [06-MultiAim](IK/06-MultiAim.md) | Weapon aiming constraint |
| [07-AnimatorControllerIntegration](IK/07-AnimatorControllerIntegration.md) | IK Pass + OnStateIK |
| [08-GizmoVisualization](IK/08-GizmoVisualization.md) | IkTargetGizmo scene dragging |
| [09-APIReference](IK/09-APIReference.md) | Complete API |
| [10-Troubleshooting](IK/10-Troubleshooting.md) | Common issues |

---

### 🔄 Humanoid Retargeting (Humanoid Avatar)

> Standard humanoid skeleton mapping. Automatic name matching, T-Pose detection, Muscle space conversion, real-time + offline retargeting.

📖 [Humanoid Retargeting Index](Retargeting/README.md)

| Document | Content |
|----------|---------|
| [01-QuickStart](Retargeting/01-QuickStart.md) | Quick start |
| [02-Concepts](Retargeting/02-Concepts.md) | Concepts (Bone/Muscle/Space/Map) |
| [03-API](Retargeting/03-API.md) | API reference (incl. BakeRetargetedClip) |
| [04-Examples](Retargeting/04-Examples.md) | Code examples |

---

## Feature Overview

```
┌──────────────────┬────────────────┬──────────────┬──────────────────────┐
│                         Stride Animation System                         │
├──────────────────┼────────────────┼──────────────┼──────────────────────┤
│Animation         │State Machine   │IK System     │Humanoid              │
│Window            │Editor          │              │Retargeting           │
│                  │                │              │                      │
│· Dopesheet       │· Drag Nodes    │· TwoBoneIK   │· Auto BoneMap        │
│· Curve Editor    │· Transitions   │· LookAt      │· T-Pose Detection    │
│· Auto Record     │· BlendTree     │· CCD         │· Muscle Space        │
│· Skeleton Rec    │· 1D/2D Panel   │· FABRIK      │· Real-time Retarget  │
│· 3D Onion Skin   │· Cond. Tags    │· MultiAim    │· Offline Bake        │
│· Copy/Paste      │· Multi-layer   │· IK Pass     │· .sdavatar           │
│· .sdanimclip     │· Copy/Paste    │· Gizmo       │                      │
│· Runtime Play    │· Undo/Redo     │· Editor      │                      │
│  back            │                │  Preview     │                      │
└──────────────────┴────────────────┴──────────────┴──────────────────────┘
```

---

## Quick Start

### 1. Create an Animation

```
Select Entity in scene → Ctrl+F12 → ●REC → Move entity → +Key → ▶ Preview → 💾 Save
```

→ See [AnimationWindow/02-QuickStart](AnimationWindow/02-QuickStart.md)

### 2. Configure the State Machine

```
Asset View → Add → Animator Controller → Double-click to open → +State → Connect transition → Set condition
```

→ See [AnimationSystem/02-VisualEditor](AnimationSystem/02-VisualEditor.md)

### 3. Add IK

```
Select character → Add Component → IK → Chains → TwoBone → Fill in bone name → Set target → F5
```

→ See [IK/01-Overview](IK/01-Overview.md)

### 4. Create an Avatar

```
Asset View → Add → Humanoid Avatar → Select Skeleton → Auto Configure → Assign to AnimatorComponent
```

→ See [Retargeting/01-QuickStart](Retargeting/01-QuickStart.md)

---

## Source Navigation

```
stride/sources/
├── engine/Stride.Engine/Animations/
│   ├── Animator/          — AnimatorController runtime (state machine/blend tree/parameters/layers)
│   ├── IK/                — IK solvers + IkComponent + IkTargetGizmo
│   └── Humanoid/          — HumanoidAvatar + BoneMap + MuscleSpace + RetargetEngine
├── engine/Stride.Assets.Models/
│   ├── AnimationClipAsset.cs        — .sdanimclip asset definition
│   ├── AnimationClipAssetCompiler.cs — .sdanimclip compiler
│   ├── HumanoidAvatarAsset.cs       — .sdavatar asset definition
│   └── HumanoidAvatarAssetCompiler.cs — .sdavatar compiler
└── editor/Stride.Assets.Presentation/
    ├── AnimationWindow/             — Animation Window (ViewModel + View + Code-behind)
    ├── AssetEditors/AnimatorControllerEditor/ — State machine visual editor
    └── Preview/AnimationClipPreview.cs — .sdanimclip asset preview
```

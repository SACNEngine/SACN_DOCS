# 11 — AnimatorController Integration

## Animation used in state machines

### Method 1: PlayAnimationClip component

The simplest. Attach the script directly to an entity and fill in the ClipPath.

### Method 2: AnimatorController State Motion

1. Create in the animation window → save `.sdanimclip`
2. **Build the project** (Ctrl+Shift+B) → compile the clip
3. Open the AnimatorController visual editor
4. Select State → property panel
5. Motion → choose `AnimationClipMotion`
6. Clip → select your animation from the dropdown

```
State: Walk
  Motion: AnimationClipMotion
    Clip: WalkAnim  ← compiled AnimationClip
```

## Why compilation is necessary

`.sdanimclip` → `Content.Load<>()` requires compilation first. Building the project (Ctrl+Shift+B) performs the compilation.

## Full flow

```
Animation Window → make animation → 💾 save .sdanimclip
                        ↓
              Ctrl+Shift+B build
                        ↓
              Content database has AnimationClip
                        ↓
              AnimatorController State.Motion = AnimationClipMotion(Clip)
                        ↓
              AnimatorComponent.Controller = .sdctrl
                        ↓
              F5 → state machine plays the animation
```

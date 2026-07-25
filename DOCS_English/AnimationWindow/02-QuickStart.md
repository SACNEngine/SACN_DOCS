# 02 — Quick Start

## Create your first animation in 5 minutes

### Step 1: Select an entity

Click any Entity in the scene (cube, sphere, character...)

### Step 2: Open the animation window

**Ctrl+F12** → the animation window appears at the bottom. The top shows the selected entity name in yellow text.

The left Properties panel shows 3 rows: Position / Rotation / Scale.

### Step 3: Set keyframes

| Action | Method |
|--------|--------|
| Drag time to 0s | Click the timeline |
| Move entity | Drag with the Gizmo in the scene |
| **+Key** | Manually take a snapshot |
| Drag time to 2s | Click the timeline |
| Move entity | Drag in the scene |
| **+Key** | Take a snapshot |
| Drag time to 4s | Same as above |
| Move entity | Same as above |
| **+Key** | Same as above |

### Step 4: Preview

- Click **▶** → the entity moves according to the keyframes
- Click **⏹** → stops, the entity returns to its original position
- Or **manually drag the time slider** → the entity follows the current frame pose

### Step 5: Save

- Click **💾** → choose the project `Assets/` directory
- File name: `MyAnim.sdanimclip`
- Asset View refreshes automatically

### Toolbar quick reference

| Button | Function |
|--------|----------|
| ▶ | Play |
| ⏸ | Pause |
| ⏹ | Stop (return to original position) |
| ●REC | Recording mode (auto-detect changes) |
| +Key | Manually add keyframe |
| +Evt | Add animation event |
| ↩ | Undo |
| ↪ | Redo |
| 📂 | Load animation |
| 💾 | Save animation |

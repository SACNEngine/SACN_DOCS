# 02 — Visual Editor

## Opening the editor

Double-click the `.sdctrl` file to open the visual state machine editor. You can also click "Open this asset in editor" in the property panel.

## Layout

```
┌─ Toolbar: [+ State] [+ Param] [Del] ──────────┐
│                                                 │
│  Entry ◉────(green dashed)────▶┌──────┐         │
│                             │ Idle │────▶┌────┐  │
│  Any State                  └──────┘     │Walk│  │
│                             ┌──────┐     └────┘  │
│                             │ Jump │            │
│                             └──────┘            │
│                                                 │
├─ Params: [Speed(Float)✕] [Jump(Trigger)✕] [+] ─┤
├─ State: Idle  Speed: 1.0 ──────────────────────┤
├─ Transition: Idle→Walk  [+Cond] ───────────────┤
└─────────────────────────────────────────────────┘
```

## Quick reference

| Action | Method |
|--------|--------|
| **Create state** | Toolbar `+ State` or right-click canvas → Add State |
| **Move state** | Left-click and hold the state node, drag to target position |
| **Rename state** | **Double-click** the state node → enter name → Enter to confirm |
| **Select state** | Single-click the state node (Ctrl+click for multi-select) |
| **Delete state** | Select → `Delete` key |
| **Create transition** | **Right-click** source state → Make Transition → click target state |
| **Delete transition** | **Right-click** the transition arrow |
| **Any State transition** | **Right-click** the Any State node → click target state |
| **Set as default state** | **Right-click** state → Set as Default State (green border + entry arrow) |
| **Zoom canvas** | **Mouse wheel** (0.2x ~ 5.0x) |
| **Pan canvas** | **Hold middle mouse button and drag** |
| **Cancel action** | `Esc` key |

## Parameter panel

The `Params:` area in the bottom bar:

- `+` button: Add a new parameter (Float type by default, named `P1`, `P2`...)
- `✕` button: Delete parameter
- Click a parameter name to edit its type and default value in the property panel

## Transition condition editor

1. **Click** the transition arrow to select it
2. The bottom bar shows the transition info: `Idle → Walk  Duration: 0.25s  ExitTime: 0.75`
3. Click `+Cond` to add a condition
4. Each condition can be configured:
   - **Parameter dropdown**: Select the bound parameter
   - **Mode dropdown**: Greater / Less / Equals / NotEqual / If / IfNot
   - **Threshold input**: Float comparison value
   - **✕ button**: Delete condition

## State properties

After clicking a state node, the bottom bar shows:
- `State: Idle  Speed: 1.0  Motion: (none)`
- Speed can be modified in the property panel

## Visual feedback for selected states

| State | Border effect |
|-------|---------------|
| Default state | **Green border** + Entry arrow pointing to it |
| Selected state | **Thick blue border** |
| Transition source state | **Orange border** (in Make Transition mode) |
| Normal state | Gray border |

## File saving

- All operations are supported by **Undo/Redo** (Ctrl+Z / Ctrl+Y)
- **Ctrl+S** saves to the `.sdctrl` YAML file
- Closing and reopening the project preserves the edited content

## New features (2026-07)

### Transition condition labels

The connection displays a summary of the conditions in the middle (e.g. `Speed > 0.5, IsRunning == true`).

### Motion type selection

Bottom bar dropdown: `(none)/AnimationClip/BlendTree1D/BlendTree2D/BlendTreeDirect`.

### BlendTree1D/2D visualization panel

When a BlendTree state is selected, a visual blend-space editor appears at the bottom.

### Multi-layer switching

Toolbar ◀ ▶ to switch layers / +Layer to add a new layer.

### Entry node context menu

Click Entry → a list of all states pops up → select the default entry state.

### Copy and paste states

Toolbar 📋 📄 buttons to copy/paste state nodes.

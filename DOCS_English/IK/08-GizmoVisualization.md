# 08 — Gizmo visualization

## IkTargetGizmo component

A draggable IK target sphere in the scene. Drag the sphere → the IK follows in real time.

## Adding a Gizmo

1. Under the character Entity, **create a child Entity**.
2. Add Component → **IK/Target Gizmo**.
3. Set **ChainIndex** = the index of the corresponding IkComponent.Chains entry.

```
Player (Entity)
├── SkeletonRoot
├── IK_LeftHand      ← child entity with IkTargetGizmo
│   ChainIndex: 0    ← corresponds to Chains[0]
├── IK_RightHand
│   ChainIndex: 1
└── IK_LookAt
    ChainIndex: 4
```

## Properties

| Property | Type | Description |
|----------|------|-------------|
| ChainIndex | int | Index within IkComponent.Chains |
| SphereColor | Color3 | Gizmo color (green by default) |

## Workflow

1. Create a child Entity → add IkTargetGizmo → set ChainIndex.
2. Select the Gizmo in the scene → press W to move it.
3. The IK follows the Gizmo position in real time.
4. Save the scene → the Gizmo position is saved.

## Code implementation

```csharp
public class IkTargetGizmo : SyncScript
{
    public int ChainIndex = 0;
    public Color3 SphereColor = new(0.2f, 0.9f, 0.3f);

    public override void Update()
    {
        var parent = Entity.GetParent();
        var ik = parent?.Components.Get<IkComponent>();
        if (ik?.Chains.Count > ChainIndex)
            ik.Chains[ChainIndex].TargetPosition = Entity.Transform.Position;
    }
}
```

## Multiple Gizmos example

| Gizmo entity | ChainIndex | Color | Purpose |
|--------------|------------|-------|---------|
| IK_LeftHand | 0 | Green | Left-hand target |
| IK_RightHand | 1 | Red | Right-hand target |
| IK_LeftFoot | 2 | Blue | Left-foot target |
| IK_LookAt | 4 | Yellow | Gaze target |

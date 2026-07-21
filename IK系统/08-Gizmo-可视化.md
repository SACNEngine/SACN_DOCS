# 08 — Gizmo 可视化

## IkTargetGizmo 组件

场景中可拖拽的 IK 目标球体。拖拽球体 → IK 实时跟随。

## 添加 Gizmo

1. 在角色实体下**创建子 Entity**
2. Add Component → **IK/Target Gizmo**
3. 设置 **ChainIndex** = 对应 IkComponent.Chains 的索引

```
Player (Entity)
├── SkeletonRoot
├── IK_LeftHand      ← 子实体，挂 IkTargetGizmo
│   ChainIndex: 0    ← 对应 Chains[0]
├── IK_RightHand
│   ChainIndex: 1
└── IK_LookAt
    ChainIndex: 4
```

## 属性

| 属性 | 类型 | 说明 |
|------|------|------|
| ChainIndex | int | IkComponent.Chains 中的索引 |
| SphereColor | Color3 | Gizmo 颜色（默认绿色） |

## 工作流

1. 创建子 Entity → 添加 IkTargetGizmo → 设 ChainIndex
2. 场景中选中 Gizmo → W 键移动
3. IK 实时跟随 Gizmo 位置
4. 保存场景 → Gizmo 位置保存

## 代码实现

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

## 多 Gizmo 示例

| Gizmo 实体 | ChainIndex | 颜色 | 用途 |
|-----------|------------|------|------|
| IK_LeftHand | 0 | 绿色 | 左手目标 |
| IK_RightHand | 1 | 红色 | 右手目标 |
| IK_LeftFoot | 2 | 蓝色 | 左脚目标 |
| IK_LookAt | 4 | 黄色 | 注视目标 |

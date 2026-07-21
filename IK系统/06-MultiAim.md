# 06 — MultiAim（武器瞄准约束）

## 用途

单骨骼旋转使其局部轴线指向目标。武器枪口、手电筒方向、摄像机瞄准。

## 算法：轴向旋转

```
    Weapon (bone)
       |
       | AimAxis (local Z = 枪管方向)
       ↓
       ● Target

旋转骨骼使 AimAxis 指向 Target
```

## 配置

```
Type:    MultiAim
Bone:    Weapon_R            (持枪骨骼)
TargetPosition:  X:5 Y:1 Z:0    (瞄准目标)
Weight:  1
```

## 硬编码轴（可通过代码修改）

| 轴 | 默认值 | 说明 |
|-----|--------|------|
| AimAxis | (0,0,1) | 瞄准方向（枪管指向） |
| UpAxis | (0,1,0) | 上方向（防翻滚） |
| WorldUp | (0,1,0) | 世界上方向参考 |

## 使用场景

| 场景 | 瞄准骨骼 |
|------|---------|
| FPS 枪口方向 | Weapon_R |
| 手电筒指向 | Flashlight |
| 摄像机追踪 | CameraBone |
| 手指指向 | IndexFinger_R |

## 代码调用

```csharp
TwoBoneIKSolver.SolveMultiAim(
    skeleton,      // SkeletonUpdater
    boneIndex,     // 骨骼索引 (int)
    target,        // Vector3 世界目标
    aimAxis,       // Vector3 局部瞄准轴
    upAxis,        // Vector3 局部上轴
    worldUp,       // Vector3 世界上方向
    weight         // float 0-1
);
```

## 完整示例

```csharp
public class WeaponAimIK : SyncScript
{
    public Entity Crosshair;  // 准星位置

    public override void Update()
    {
        var ik = Entity.Components.Get<IkComponent>();
        if (ik?.Chains.Count > 0)
        {
            ik.Chains[0].TargetPosition =
                Crosshair.Transform.WorldMatrix.TranslationVector;
        }
    }
}
```

# 02 — TwoBoneIK（二骨 IK）

## 用途

手臂（肩→肘→手）和腿部（髋→膝→脚）的末端位置控制。

## 算法：余弦定理

```
       Mid (elbow)
      /  \
   a /    \ b
    /      \
 Root       Tip ← Target

c = distance(Root, Target), clamped to [|a-b|, a+b]
cos(θ) = (a² + b² - c²) / (2ab)
→ Mid 关节弯曲 θ 度
→ Root 旋转使 Tip 指向 Target
→ Hint 点决定弯曲方向
```

## 配置

```
Type:        TwoBone
RootBone:    LeftUpperArm     (上臂，靠近身体)
MidBone:     LeftForearm      (前臂，中间关节)
TipBone:     LeftHand         (手，末端)
TargetPosition:   X:2 Y:1.5 Z:1    (手要去的位置)
HintPosition:     X:2 Y:0   Z:2.5  (肘部朝哪边弯)
Weight:      1                  (0=不动，1=完全跟随)
```

## Hint 位置说明

Hint 控制关节向哪个方向弯曲。对于手臂：
- `Hint Z > Tip Z` → 肘部朝前
- `Hint Z < Tip Z` → 肘部朝后
- `Hint Y > Tip Y` → 肘部朝上

通常设置 Hint = Target + 一个偏移量。

## 使用场景

| 场景 | 配置 |
|------|------|
| 角色抓取物体 | 手部 TwoBone，Target = 物体位置 |
| 脚步贴合地面 | 脚部 TwoBone，Target = 射线检测地面点 |
| 推门动作 | 手部 TwoBone，Target = 门把手位置 |
| 攀爬 | 手脚同时 TwoBone，Target = 攀爬点 |

## 代码调用

```csharp
// 直接调用（不使用 IkComponent）
TwoBoneIKSolver.Solve(
    skeleton,          // SkeletonUpdater
    rootIndex,         // 根骨索引 (int)
    midIndex,          // 中骨索引 (int)
    tipIndex,          // 末端骨索引 (int)
    targetPosition,    // Vector3 世界坐标
    hintPosition,      // Vector3 弯曲方向
    weight             // float 0-1
);
```

## 完整示例

```csharp
public class HandGrabIK : SyncScript
{
    public Entity TargetObject;

    public override void Update()
    {
        var ik = Entity.Components.Get<IkComponent>();
        if (ik?.Chains.Count > 0)
        {
            ik.Chains[0].TargetPosition =
                TargetObject.Transform.WorldMatrix.TranslationVector;
        }
    }
}
```

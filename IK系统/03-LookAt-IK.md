# 03 — LookAt IK（注视 IK）

## 用途

单骨骼旋转朝向目标。角色头部注视玩家、NPC 眼球跟踪、摄像机自动朝向。

## 算法：Quaternion 轴向旋转

```
     Head
       \
        \ Forward (local Z)
         \
          ● Target

1. 计算 Head→Target 世界方向
2. 当前 Forward 在世界的方向
3. 从当前→目标的方向旋转
4. 可设 maxAngle 钳制
```

## 配置

```
Type:    LookAt
Bone:    Head              (单骨名)
TargetPosition:  X:0 Y:2 Z:3   (注视目标)
Weight:  0.7                (0=不转头，1=完全朝向)
```

## 参数说明

| 参数 | 值 | 说明 |
|------|-----|------|
| Forward | (0,0,1) | 骨骼局部前方向（硬编码） |
| Up | (0,1,0) | 局部上方向（硬编码） |
| clampAngle | 0（默认） | 最大旋转角度（弧度），0=不限制 |

## 使用场景

| 场景 | Weight | 目标 |
|------|--------|------|
| 注视玩家 | 0.5-0.8 | 玩家头部位置 |
| 阅读书本 | 1.0 | 书本位置 |
| 环顾四周 | 0.3 | 随机摆动 |

## 代码调用

```csharp
TwoBoneIKSolver.SolveLookAt(
    skeleton,      // SkeletonUpdater
    boneIndex,     // 骨骼索引 (int)
    target,        // Vector3 世界目标
    forward,       // Vector3 局部前方向
    up,            // Vector3 局部上方向
    clampAngle,    // float 最大角度
    weight         // float 0-1
);
```

## 完整示例

```csharp
public class HeadLookAt : SyncScript
{
    public Entity Player;

    public override void Update()
    {
        var ik = Entity.Components.Get<IkComponent>();
        if (ik?.Chains.Count > 1)
        {
            var playerHead = Player.Transform.WorldMatrix.TranslationVector + Vector3.UnitY * 1.7f;
            ik.Chains[1].TargetPosition = playerHead;  // Chain[1] = LookAt
        }
    }
}
```

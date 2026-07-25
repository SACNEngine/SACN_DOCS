# 05 — FABRIK（Forward And Backward Reaching IK）

## 用途

长骨骼链的快速收敛。10+ 段的尾巴、触手类生物、多段脊柱。

## 算法：前后传递

```
正向传递 (tip→root):
  ① Tip = Target
  ② 保持骨长，逐级向根部调整位置

反向传递 (root→tip):
  ③ Root = OriginalRoot（恢复原位）
  ④ 保持骨长，逐级向末端调整位置

重复 N 次（通常 3-5 次即可）
```

## 为什么比 CCD 好

- 每次迭代同时调整所有骨骼（而不是逐骨）
- 运动分布均匀，看起来更自然
- 收敛极快，3-5 次迭代 ≈ CCD 的 10-15 次

## 配置

```
Type:    FABRIK
RootBone:   Spine_01         (链起始)
TipBone:    Spine_10         (链末端)
TargetPosition:  X:0 Y:1 Z:3
CcdIterations:  4            (FABRIK 收敛快，3-5 即可)
Weight:  1
```

## 参数建议

| 骨骼数 | CcdIterations |
|--------|---------------|
| 3-5 | 3 |
| 6-10 | 4 |
| 10+ | 5 |

## 代码调用

```csharp
TwoBoneIKSolver.SolveFABRIK(
    skeleton,      // SkeletonUpdater
    chainStart,    // 链起始索引 (int)
    chainEnd,      // 链末端索引 (int)
    target,        // Vector3 世界目标
    weight,        // float 0-1
    iterations     // int 迭代次数 (3-5)
);
```

## 注意事项

- FABRIK 直接操作位置，求解后需要额外步骤将位置变化转回旋转
- 如果骨骼链超出目标范围，会伸直指向目标
- 与动画混合时建议 Weight < 1.0

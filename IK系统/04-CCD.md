# 04 — CCD（Cyclic Coordinate Descent）

## 用途

多骨骼链的末端位置控制。尾巴、触手、多段脊柱。

## 算法：逐骨迭代旋转

```
Chain: [B0]→[B1]→[B2]→[B3]→[Tip]  Target ●

每个迭代:
  For i = Tip downto Root:
    ① 计算 Bi 世界位置
    ② Bi→Tip 方向 vs Bi→Target 方向
    ③ 旋转 Bi 使二者对齐

重复 N 次直到收敛
```

## 特点

- 末端骨骼优先收敛（尖端先到达）
- 根部骨骼运动大（可能看起来不自然）
- 实现简单，鲁棒性好

## 配置

```
Type:    CCD
RootBone:   Tail_01          (链起始骨骼)
TipBone:    Tail_06          (链末端骨骼)
TargetPosition:  X:1 Y:0.5 Z:2
CcdIterations:  8            (迭代次数，5-10推荐)
Weight:  1
```

## 参数建议

| 骨骼数 | CcdIterations |
|--------|---------------|
| 3-4 | 5 |
| 5-7 | 8 |
| 8+ | 10-15 |

## vs FABRIK

| | CCD | FABRIK |
|---|-----|--------|
| 收敛速度 | 慢（需更多迭代） | 快（3-5次） |
| 运动分布 | 末端集中 | 均匀自然 |
| 推荐场景 | 短链 (3-4骨) | 长链 (5+骨) |

## 代码调用

```csharp
TwoBoneIKSolver.SolveCCD(
    skeleton,      // SkeletonUpdater
    chainStart,    // 链起始索引 (int)
    chainEnd,      // 链末端索引 (int)
    target,        // Vector3 世界目标
    weight,        // float 0-1
    iterations     // int 迭代次数
);
```

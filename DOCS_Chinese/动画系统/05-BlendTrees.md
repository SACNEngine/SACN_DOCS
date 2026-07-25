# 05 — 混合树 (Blend Trees)

## 概述

混合树用**参数驱动多个动画之间的平滑混合**，替代离散的动画切换。

## BlendTree1D — 一维混合空间

用一个 Float 参数在多个动画之间平滑过渡。

### 代码示例

```csharp
var locomotion = new BlendTree1D
{
    BlendParameter = speedParam,    // 驱动参数
    Thresholds = { 0f, 3f, 7f },   // 阈值数组（需等于 Motions 数量）
    Motions =
    {
        new AnimationClipMotion(idleClip),  // Speed=0 时的动画
        new AnimationClipMotion(walkClip),  // Speed=3 时的动画
        new AnimationClipMotion(runClip),   // Speed=7 时的动画
    }
};
```

### 混合逻辑

| Speed 值 | 混合结果 |
|----------|---------|
| 0 | 100% Idle |
| 1.5 | 50% Idle + 50% Walk |
| 3 | 100% Walk |
| 5 | 50% Walk + 50% Run |
| 7 | 100% Run |

## BlendTree2D — 二维混合空间

用**两个 Float 参数**在 2D 平面上混合。适用于角色移动方向。

### 代码示例

```csharp
var directionBlend = new BlendTree2D
{
    BlendParameterX = velXParam,   // X 轴参数
    BlendParameterY = velZParam,   // Y 轴参数
    Positions =
    {
        new Vector2( 0f,  1f),   // 前进
        new Vector2( 0f, -1f),   // 后退
        new Vector2(-1f,  0f),   // 左移
        new Vector2( 1f,  0f),   // 右移
        new Vector2( 0f,  0f),   // 静止
    },
    Motions =
    {
        new AnimationClipMotion(walkForward),
        new AnimationClipMotion(walkBack),
        new AnimationClipMotion(strafeLeft),
        new AnimationClipMotion(strafeRight),
        new AnimationClipMotion(idle),
    }
};
```

### 混合算法

**梯度带混合 (Gradient Band Blending)**：权重 = `1 / distance²`，然后归一化。参数越靠近某个采样点，该采样点的动画权重越大。

## BlendTreeDirect — 直接权重

脚本直接设置每个子动画的权重，不走参数驱动。

```csharp
var direct = new BlendTreeDirect();
// 在 Update 中设置：
direct.Weights = new float[] { 0.2f, 0.3f, 0.5f };
```

## 嵌套混合树

混合树可以嵌套——一个 BlendTree 的 child 可以是另一个 BlendTree：

```csharp
var outerBlend = new BlendTree1D { /* ... */ };
outerBlend.Motions.Add(new BlendTree2D { /* 子混合树 */ });
```

## Motion 类型

所有混合树的子节点都是 `Motion` 类型：

| 类型 | 说明 |
|------|------|
| `AnimationClipMotion` | 单个动画 Clip |
| `BlendTree1D` | 一维混合空间 |
| `BlendTree2D` | 二维混合空间 |
| `BlendTreeDirect` | 直接权重混合 |

`Motion` 是抽象基类，实现了 `IMotion` 接口，支持 `[DataContract(Inherited=true)]` 多态序列化。

## 在状态中使用混合树

```csharp
var moveState = new AnimatorState
{
    Name = "Locomotion",
    Motion = locomotionBlend,   // 混合树作为 Motion
    Speed = 1f,
};
```

## vs Unity 对比

| 功能 | Unity | Stride |
|------|-------|--------|
| 1D BlendTree | ✅ | ✅ |
| 2D Simple Directional | ✅ | ✅ 梯度带 |
| 2D Freeform Directional | ✅ Delaunay 三角剖分 | ⚠️ 梯度带（非三角剖分） |
| 2D Freeform Cartesian | ✅ | ✅ 梯度带 |
| Direct BlendTree | ✅ | ✅ |

## BlendTree2D (新增)

2D 混合树根据两个 Float 参数在二维平面上混合动画。

### 可视化编辑器

选中 BlendTree2D 类型状态 → 状态机编辑器底部显示：
- X/Y 参数下拉框
- 10×10 网格画布
- 橙色圆点 = 混合点（可拖动）
- +Point 添加点，点击画布定位

### 配置

```
Type:         BlendTree2D
BlendParameterX:  Speed    (X轴参数)
BlendParameterY:  Angle    (Y轴参数)
Positions:    [(0.3,0.5), (0.7,0.8), ...]
Motions:      [走路, 跑步, ...]
```

### 运行时行为

根据 BlendParameterX 和 BlendParameterY 的值，在二维平面上对相邻点进行梯度混合。

# 04 — 状态与过渡

## 状态机结构

```
AnimatorController
└── Layers[]
    └── StateMachine
        ├── DefaultStateId     ← 进入状态机时首先激活的状态
        ├── States[]           ← 状态 + 子状态机
        │   ├── AnimatorState
        │   │   ├── Motion     ← 播放的动画（Clip 或 BlendTree）
        │   │   ├── Transitions[] ← 出站过渡
        │   │   ├── Speed      ← 播放速度
        │   │   └── Behaviours[] ← 状态行为回调
        │   └── AnimatorStateMachine  ← 嵌套子状态机
        └── AnyStateTransitions[] ← 从任意状态都能触发的过渡
```

## 状态 (AnimatorState)

### 属性

| 属性 | 类型 | 默认值 | 说明 |
|------|------|--------|------|
| `Name` | string | null | 状态名称（在编辑器中显示） |
| `Motion` | Motion | null | 播放的动画（AnimationClipMotion 或 BlendTree） |
| `Speed` | float | 1.0 | 播放速度倍率 |
| `SpeedMultiplier` | AnimatorParameter | null | 可选参数驱动的速度倍率 |
| `CycleOffset` | float | 0.0 | 进入状态时的起始归一化偏移 |
| `Mirror` | bool | false | 镜像动画 |
| `WriteDefaults` | bool | false | 是否写入默认值到未动画化通道 |
| `Transitions` | List\<AnimatorTransition\> | [] | 出站过渡列表 |
| `Behaviours` | List\<StateMachineBehaviour\> | [] | 状态生命周期回调 |
| `Tags` | List\<string\> | [] | 标签（用于脚本查找） |

### 创建状态

**可视化编辑器**：点击 `+ State` 按钮

**代码**：
```csharp
var state = new AnimatorState
{
    Name = "Idle",
    Motion = new AnimationClipMotion(idleClip),
    Speed = 1f,
    CycleOffset = 0f,
};
```

### 子状态机（Sub-State Machine）

```csharp
var subSm = new AnimatorStateMachine
{
    Name = "Combat",
    DefaultStateId = combatIdleState.Id,
};
subSm.States.Add(combatIdleState);
subSm.States.Add(attackState);

// 添加到父状态机
parentSM.States.Add(subSm);
```

---

## 过渡 (AnimatorTransition)

### 属性

| 属性 | 类型 | 默认值 | 说明 |
|------|------|--------|------|
| `DestinationStateId` | Guid | Empty | 目标状态 Id |
| `Name` | string | null | 过渡名称（可选） |
| `HasExitTime` | bool | true | 是否需要满足退出时间才能触发 |
| `ExitTime` | float | 0.75 | 归一化退出时间 [0, 1] |
| `FixedDuration` | bool | false | 忽略 ExitTime，条件满足立即触发 |
| `TransitionDuration` | float | 0.25 | 交叉淡入时间（秒） |
| `TransitionOffset` | float | 0.0 | 目标状态起始偏移 |
| `InterruptionSource` | enum | None | 中断策略 |
| `RequireAllConditions` | bool | true | 条件 AND(true) / OR(false) |
| `Conditions` | List\<AnimatorCondition\> | [] | 触发条件列表 |
| `Easing` | enum | Linear | 缓动曲线 |
| `IsMute` | bool | false | 过渡期间静音源层 |

### ExitTime 详解

```
ExitTime=0.75  → 当前状态至少播放到 75% 才能评估过渡条件
HasExitTime=false → 条件满足时立即过渡
FixedDuration=true → 忽略 ExitTime，条件满足立即触发
```

### 过渡缓动

```csharp
new AnimatorTransition
{
    Easing = TransitionEasing.EaseInOut,  // 平滑起止
}
```

| 模式 | 曲线 |
|------|------|
| `Linear` | 匀速 |
| `EaseIn` | 慢→快 |
| `EaseOut` | 快→慢 |
| `EaseInOut` | 慢→快→慢 |

### 条件 (AnimatorCondition)

```csharp
new AnimatorCondition
{
    Parameter = speedParam,               // 检查的参数
    Mode = AnimatorConditionMode.Greater, // 比较模式
    FloatThreshold = 3.0f,               // 阈值
}
```

### 中断策略 (InterruptionSource)

| 值 | 行为 |
|----|------|
| `None` | 当前过渡不可中断 |
| `Source` | 可被源状态的过渡打断 |
| `Destination` | 可被目标状态的过渡打断 |
| `SourceThenDestination` | 有序中断 |

### Any State 过渡

从任何状态都可触发的过渡：

```csharp
sm.AnyStateTransitions.Add(new AnimatorTransition
{
    DestinationStateId = deathState.Id,
    HasExitTime = false,
    Conditions = { new AnimatorCondition { Parameter = healthZeroParam, Mode = AnimatorConditionMode.If } }
});
```

### 代码示例：完整状态机

```csharp
// 状态
var idle = new AnimatorState { Name = "Idle", Motion = idleClip };
var walk = new AnimatorState { Name = "Walk", Motion = walkClip };
var jump = new AnimatorState { Name = "Jump", Motion = jumpClip };

// 过渡
idle.Transitions.Add(new AnimatorTransition
{
    DestinationStateId = walk.Id,
    Conditions = { new AnimatorCondition { Parameter = speed, Mode = AnimatorConditionMode.Greater, FloatThreshold = 0.1f } }
});

walk.Transitions.Add(new AnimatorTransition
{
    DestinationStateId = idle.Id,
    Conditions = { new AnimatorCondition { Parameter = speed, Mode = AnimatorConditionMode.Less, FloatThreshold = 0.1f } }
});

jump.Transitions.Add(new AnimatorTransition
{
    DestinationStateId = idle.Id,
    HasExitTime = true, ExitTime = 0.9f,
    TransitionDuration = 0.15f,
});

// 状态机
var sm = new AnimatorStateMachine { Name = "Base", DefaultStateId = idle.Id };
sm.States.Add(idle); sm.States.Add(walk); sm.States.Add(jump);

// 层
ctrl.Layers.Add(new AnimatorControllerLayer { Name = "Base", StateMachine = sm, DefaultWeight = 1f });
```

## 运行时查询

```csharp
// 当前状态
var info = animator.GetCurrentAnimatorStateInfo(0);
// info.StateName, info.NormalizedTime, info.Speed, info.LoopCount

// 是否在过渡中
bool trans = animator.IsInTransition(0);

// 过渡信息
var tInfo = animator.GetTransitionInfo(0);

// 状态变化事件
animator.StateChanged += (layer, oldState, newState) =>
    Console.WriteLine($"{oldState.StateName} → {newState.StateName}");
```

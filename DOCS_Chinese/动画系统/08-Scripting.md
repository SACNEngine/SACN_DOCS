# 08 — 脚本 API 参考

## AnimatorComponent

运行时动画组件，挂载在 Entity 上。

### 参数操作

```csharp
// 写入
void SetFloat(string name, float value)
void SetInt(string name, int value)
void SetBool(string name, bool value)
void SetTrigger(string name)

// 读取
float GetFloat(string name)
int   GetInt(string name)
bool  GetBool(string name)
```

### 状态查询

```csharp
// 当前状态信息
AnimatorStateInfo GetCurrentAnimatorStateInfo(int layerIndex)

// 下一个状态信息（过渡中时有效）
AnimatorStateInfo GetNextAnimatorStateInfo(int layerIndex)

// 过渡信息
AnimatorTransitionInfo GetTransitionInfo(int layerIndex)

// 是否在过渡中
bool IsInTransition(int layerIndex)

// 当前状态名
string GetCurrentStateName(int layerIndex)
```

### 事件

```csharp
// 动画事件回调
event Action<AnimationEvent> AnimationEventFired

// 状态变化回调
event Action<int, AnimatorStateInfo, AnimatorStateInfo> StateChanged

// Root Motion 回调
event Action<AnimatorComponent> OnAnimatorMove
```

### 属性

| 属性 | 类型 | 默认 | 说明 |
|------|------|------|------|
| `Controller` | AnimatorController | null | 动画控制器 |
| `Skeleton` | Skeleton | null | 骨骼（自动检测） |
| `ApplyRootMotion` | bool | false | 提取根运动 |
| `UpdateMode` | AnimatorUpdateMode | Normal | 更新模式 |
| `CullingMode` | AnimatorCullingMode | AlwaysAnimate | 剔除模式 |
| `LayerWeightOverrides` | Dictionary | {} | 层权重覆盖 |

### AnimatorUpdateMode

| 值 | 说明 |
|----|------|
| `Normal` | 每帧更新 |
| `AnimatePhysics` | 固定时间步长更新 |
| `UnscaledTime` | 手动更新 |

### AnimatorCullingMode

| 值 | 说明 |
|----|------|
| `AlwaysAnimate` | 始终更新 |
| `CullUpdateTransforms` | 屏幕外停止变换更新 |
| `CullCompletely` | 屏幕外完全停止 |

---

## AnimatorController

顶层容器，包含参数和层。

### 属性

| 属性 | 类型 | 说明 |
|------|------|------|
| `Parameters` | List\<AnimatorParameter\> | 参数列表 |
| `Layers` | List\<AnimatorControllerLayer\> | 层列表 |

### 方法

```csharp
AnimatorParameter FindParameter(string name)
AnimatorParameter FindParameterById(Guid id)
AnimatorControllerLayer FindLayer(string name)
List<string> Validate()
AnimatorController Clone()
```

---

## AnimatorState

### 属性

```csharp
Guid Id                    // 唯一标识
string Name                // 状态名
Motion Motion              // 播放的动画（Clip 或 BlendTree）
float Speed                // 播放速度倍率（默认 1.0）
AnimatorParameter SpeedMultiplier  // 参数驱动的速度倍率
float CycleOffset          // 进入状态时的起始偏移 [0,1]
bool Mirror                // 镜像动画
bool WriteDefaults         // 写入默认值
List<AnimatorTransition> Transitions  // 出站过渡
List<StateMachineBehaviour> Behaviours // 状态行为
List<string> Tags          // 标签
```

---

## AnimatorTransition

### 属性

```csharp
Guid DestinationStateId     // 目标状态 Id
string Name                 // 过渡名称
bool HasExitTime            // 是否有退出时间
float ExitTime              // 归一化退出时间 [0,1]
bool FixedDuration          // 忽略 ExitTime
float TransitionDuration    // 过渡时长（秒）
float TransitionOffset      // 目标状态偏移
TransitionInterruptionSource InterruptionSource  // 中断策略
bool RequireAllConditions   // AND/OR 条件
List<AnimatorCondition> Conditions  // 条件列表
TransitionEasing Easing     // 缓动曲线
bool IsMute                 // 过渡期间静音
```

---

## BlendTree1D

```csharp
AnimatorParameter BlendParameter   // 驱动参数
List<float> Thresholds             // 阈值数组
List<IMotion> Motions             // 子动画列表
```

## BlendTree2D

```csharp
AnimatorParameter BlendParameterX  // X 轴参数
AnimatorParameter BlendParameterY  // Y 轴参数
List<Vector2> Positions           // 采样点位置
List<IMotion> Motions            // 子动画列表
```

## BlendTreeDirect

```csharp
float[] Weights                   // 直接权重数组
List<IMotion> Motions            // 子动画列表
```

---

## AnimatorStateInfo

```csharp
string StateName        // 状态名
Guid StateId            // 状态 Id
float NormalizedTime    // 归一化时间 [0,1]
float LengthSeconds     // Clip 时长（秒）
float Speed             // 当前速度
float SpeedMultiplier   // 速度倍率
int LoopCount           // 循环次数
string Tag              // 第一个标签
bool IsValid            // 是否有效
float ElapsedSeconds    // 已播放秒数
bool IsDone             // 是否播放完毕
```

## AnimatorTransitionInfo

```csharp
AnimatorStateInfo FromState   // 源状态
AnimatorStateInfo ToState     // 目标状态
bool IsTransitioning          // 是否在过渡中
float Progress                // 过渡进度 [0,1]
float DurationSeconds         // 过渡时长
```

---

## AnimatorOverrideController

```csharp
AnimatorController BaseController        // 基础控制器
List<ClipOverridePair> Overrides         // 替换对列表

// 应用替换
AnimatorController ApplyOverrides()
```

# 03 — 参数系统

## 四种参数类型

| 类型 | 用途 | 典型场景 |
|------|------|---------|
| **Float** | 连续数值 | 速度、方向、HP 百分比 |
| **Int** | 整数 | 武器类型、连击数、等级 |
| **Bool** | 开关状态 | 是否着地、是否瞄准、是否持盾 |
| **Trigger** | 一次性信号 | 跳跃、攻击、受击、换弹 |

## Trigger 的重要特性

Trigger **自动消耗**：`SetTrigger("Jump")` 后，参数在**当前帧**为 true，被过渡条件读取后立即重置为 false。

```csharp
// ✅ 正确用法：每帧检查输入，按下时触发
if (Input.IsKeyPressed(Keys.Space))
    animator.SetTrigger("Jump");

// ❌ 错误用法：持续按住会每帧触发
if (Input.IsKeyDown(Keys.Space))
    animator.SetTrigger("Jump");  // 每帧都触发！
```

## 在可视化编辑器中添加参数

1. 点击底部栏 `+` 按钮
2. 参数默认名为 `P1`，类型为 Float
3. 在属性面板修改名称、类型、默认值

## 在代码中定义参数

```csharp
var ctrl = new AnimatorController();

// Float 参数
var speed = new AnimatorParameter
{
    Name = "Speed",
    Type = AnimatorParameterType.Float,
    DefaultFloat = 0f
};

// Trigger 参数
var jump = new AnimatorParameter
{
    Name = "Jump",
    Type = AnimatorParameterType.Trigger
};

// Bool 参数
var grounded = new AnimatorParameter
{
    Name = "IsGrounded",
    Type = AnimatorParameterType.Bool,
    DefaultBool = true
};

// Int 参数
var weapon = new AnimatorParameter
{
    Name = "WeaponType",
    Type = AnimatorParameterType.Int,
    DefaultInt = 0
};

ctrl.Parameters.Add(speed);
ctrl.Parameters.Add(jump);
ctrl.Parameters.Add(grounded);
ctrl.Parameters.Add(weapon);
```

## 在脚本中读写参数

```csharp
var animator = Entity.Get<AnimatorComponent>();

// 写入
animator.SetFloat("Speed", 5.0f);
animator.SetInt("WeaponType", 2);
animator.SetBool("IsGrounded", true);
animator.SetTrigger("Jump");

// 读取
float speed = animator.GetFloat("Speed");
int weapon = animator.GetInt("WeaponType");
bool grounded = animator.GetBool("IsGrounded");
```

## 参数存储机制

`AnimatorParameterStore` 是运行时参数存储容器：

- 内部用 `Dictionary<Guid, T>` 按类型分别存储
- 线程安全：读安全，写需主线程
- 每帧结束时消耗的 Trigger 自动重置
- 通过 `EndFrameReset()` 清理已读 Trigger

## 参数驱动过渡

过渡条件使用参数来决定何时触发：

```csharp
new AnimatorTransition
{
    DestinationStateId = runState.Id,
    Conditions =
    {
        new AnimatorCondition
        {
            Parameter = speedParam,         // 检查 Speed 参数
            Mode = AnimatorConditionMode.Greater,  // > 阈值
            FloatThreshold = 3.0f           // Speed > 3.0 时过渡
        }
    }
}
```

### 条件模式对照

| 模式 | Float/Int | Bool | Trigger |
|------|-----------|------|---------|
| `If` | value > threshold | value == true | 触发时为 true |
| `IfNot` | value < threshold | value == false | — |
| `Greater` | value > threshold | — | — |
| `Less` | value < threshold | — | — |
| `Equals` | value == threshold | value == threshold | — |
| `NotEqual` | value != threshold | value != threshold | — |

## 参数驱动混合树

BlendTree 也使用参数来确定混合位置：

```csharp
var blend = new BlendTree1D
{
    BlendParameter = speedParam,    // Speed 参数驱动混合
    Thresholds = { 0f, 3f, 7f },
    Motions = { idleClip, walkClip, runClip }
};
```

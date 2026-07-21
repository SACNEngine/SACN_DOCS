# 07 — 动画事件与状态行为

## AnimationEvent（动画事件）

在动画的特定时间点触发回调。

### 在 AnimationClip 上定义事件

```csharp
clip.Events.Add(new AnimationEvent
{
    NormalizedTime = 0.3f,          // 动画 30% 处触发
    FunctionName = "Footstep",      // 事件名
    StringParameter = "Left",       // 可选字符串参数
    FloatParameter = 1.0f,          // 可选浮点参数
    IntParameter = 0,               // 可选整数参数
    FireOncePerLoop = true,         // 每循环只触发一次
});
```

### 属性

| 属性 | 类型 | 默认 | 说明 |
|------|------|------|------|
| `NormalizedTime` | float | 0 | 触发时间 [0, 1] |
| `FunctionName` | string | null | 事件名（如 "Footstep"） |
| `StringParameter` | string | null | 可选字符串 |
| `FloatParameter` | float | 0 | 可选浮点数 |
| `IntParameter` | int | 0 | 可选整数 |
| `ObjectReferenceParameter` | object | null | 可选对象引用 |
| `FireOncePerLoop` | bool | true | 同一循环只触发一次 |

### 接收事件 — 方式一：事件订阅

```csharp
animator.AnimationEventFired += (evt) =>
{
    switch (evt.FunctionName)
    {
        case "Footstep":
            Audio.PlayFootstep(evt.StringParameter);  // "Left" or "Right"
            break;
        case "Attack":
            EnableHitbox();
            break;
        case "Land":
            PlayLandingEffect();
            break;
    }
};
```

### 接收事件 — 方式二：IAnimationEventReceiver 接口

在任何 EntityComponent 上实现接口：

```csharp
public class FootstepHandler : SyncScript, IAnimationEventReceiver
{
    public void OnAnimationEvent(AnimationEvent evt)
    {
        if (evt.FunctionName == "Footstep")
            Console.WriteLine($"Footstep: {evt.StringParameter}");
    }
}
```

---

## StateMachineBehaviour（状态行为回调）

状态生命周期回调，类似 Unity 的 StateMachineBehaviour。

### 基类

```csharp
public abstract class StateMachineBehaviour
{
    public virtual void OnStateEnter(AnimatorComponent animator, AnimatorStateInfo info, int layer) { }
    public virtual void OnStateExit(AnimatorComponent animator, AnimatorStateInfo info, int layer) { }
    public virtual void OnStateUpdate(AnimatorComponent animator, AnimatorStateInfo info, int layer) { }
    public virtual void OnStateIK(AnimatorComponent animator, AnimatorStateInfo info, int layer) { }
    public virtual void OnStateMachineEnter(AnimatorComponent animator, int layer) { }
    public virtual void OnStateMachineExit(AnimatorComponent animator, int layer) { }
}
```

### 使用示例：攻击行为

```csharp
public class AttackBehaviour : StateMachineBehaviour
{
    public override void OnStateEnter(AnimatorComponent animator, AnimatorStateInfo info, int layer)
    {
        // 进入攻击状态：启用武器碰撞体
        var weapon = animator.Entity.Get<WeaponComponent>();
        weapon?.EnableHitbox();

        // 播放挥刀音效
        AudioSystem.Play("SwordSwing");
    }

    public override void OnStateExit(AnimatorComponent animator, AnimatorStateInfo info, int layer)
    {
        // 退出攻击状态：禁用武器碰撞体
        var weapon = animator.Entity.Get<WeaponComponent>();
        weapon?.DisableHitbox();
    }

    public override void OnStateUpdate(AnimatorComponent animator, AnimatorStateInfo info, int layer)
    {
        // 每帧更新：例如在攻击的后半段检测命中
        if (info.NormalizedTime > 0.5f)
            CheckHit();
    }
}

// 挂载
attackState.Behaviours.Add(new AttackBehaviour());
```

### 使用示例：落地检测

```csharp
public class LandBehaviour : StateMachineBehaviour
{
    public override void OnStateEnter(AnimatorComponent animator, AnimatorStateInfo info, int layer)
    {
        // 落地时播放特效
        SpawnLandEffect(animator.Entity.Transform.Position);
    }
}

jumpState.Behaviours.Add(new LandBehaviour());
```

### 回调时序

```
进入状态   OnStateEnter ──→ OnStateUpdate (每帧) ──→ 过渡开始 →
          OnStateExit ──→ 目标状态 OnStateEnter ──→ ...
```

# 07 — AnimatorController 集成

## IK Pass 启用

1. 打开 AnimatorController 可视化编辑器
2. 选中 Layer → 属性面板 → **IkPass = true**
3. AnimatorProcessor 每帧自动调用 `OnStateIK`

## OnStateIK 回调

```csharp
public class MyIKBehaviour : StateMachineBehaviour
{
    public override void OnStateIK(
        AnimatorComponent animator,
        AnimatorStateInfo info,
        int layer)
    {
        var ik = animator.Entity.Components.Get<IkComponent>();
        if (ik == null) return;

        // 设置 IK 目标
        ik.Chains[0].TargetPosition = GetTargetPosition();
    }
}
```

## 按状态切换 IK

```csharp
public class AimIKBehaviour : StateMachineBehaviour
{
    public override void OnStateEnter(...)
    {
        // 进入瞄准状态，激活右手 IK
        animator.Entity.Components.Get<IkComponent>().Chains[1].Weight = 1f;
    }

    public override void OnStateExit(...)
    {
        // 离开瞄准状态，关闭右手 IK
        animator.Entity.Components.Get<IkComponent>().Chains[1].Weight = 0f;
    }
}
```

## 执行时序

```
AnimatorProcessor.Draw():
  ① Tick State Machines        → 过渡求值
  ② Compose Layers             → 运动混合
  ③ AnimationUpdater.Update    → 写入骨骼
  ④ IK Pass (本模块)           → OnStateIK 回调
  ⑤ EndFrameReset              → 消耗 Trigger
```

## 注意事项

- `OnStateIK` 在动画混合**之后**调用，可直接读当前骨骼姿态
- `info.NormalizedTime` 可获取当前状态进度
- `layer` 参数区分多层 IK
- IK 目标应在每帧更新，否则使用上一帧的旧值

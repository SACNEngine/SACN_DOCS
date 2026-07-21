# 09 — 完整示例

## 示例 1：第三人称角色动画控制器

### 状态机结构

```
                    ┌──────────┐
                    │  Entry   │
                    └────┬─────┘
                         │ default
                         ▼
Speed=0 ┌──────┐     ┌──────┐     ┌──────┐ Speed>7
        │ Idle │◄───►│ Walk │◄───►│ Run  │
        └──┬───┘     └──┬───┘     └──┬───┘
           │            │            │
Jump trig  │   Atk trig │            │
           ▼            ▼            │
        ┌──────┐    ┌──────┐         │
        │ Jump │    │Attack│         │
        │(0.9s)│    │(0.85s│         │
        └──┬───┘    └──┬───┘         │
           └─────┬──────┘            │
                 ▼                   │
          回到 Idle/Walk ◄───────────┘
```

### 动画设置脚本

```csharp
// HeroAnimSetup.cs — StartupScript
public class HeroAnimSetup : StartupScript
{
    public AnimationClip Idle, Walk, Run, Jump, Attack;

    public override void Start()
    {
        var ctrl = new AnimatorController();

        var spd = new AnimatorParameter { Name="Speed",  Type=AnimatorParameterType.Float };
        var jmp = new AnimatorParameter { Name="Jump",   Type=AnimatorParameterType.Trigger };
        var atk = new AnimatorParameter { Name="Attack", Type=AnimatorParameterType.Trigger };
        ctrl.Parameters.Add(spd); ctrl.Parameters.Add(jmp); ctrl.Parameters.Add(atk);

        var move = new BlendTree1D
        {
            BlendParameter = spd,
            Thresholds = { 0f, 3f, 7f },
            Motions = { new AnimationClipMotion(Idle), new AnimationClipMotion(Walk), new AnimationClipMotion(Run) }
        };

        var moveSt = new AnimatorState { Name="Move",  Motion=move };
        var jumpSt = new AnimatorState { Name="Jump",  Motion=new AnimationClipMotion(Jump) };
        var atkSt  = new AnimatorState { Name="Attack",Motion=new AnimationClipMotion(Attack) };

        jumpSt.Transitions.Add(new AnimatorTransition { DestinationStateId=moveSt.Id, HasExitTime=true, ExitTime=0.9f, TransitionDuration=0.15f });
        atkSt.Transitions.Add(new AnimatorTransition  { DestinationStateId=moveSt.Id, HasExitTime=true, ExitTime=0.85f,TransitionDuration=0.1f  });

        var sm = new AnimatorStateMachine { Name="Base", DefaultStateId=moveSt.Id };
        sm.States.Add(moveSt); sm.States.Add(jumpSt); sm.States.Add(atkSt);

        sm.AnyStateTransitions.Add(new AnimatorTransition { DestinationStateId=jumpSt.Id, Conditions={new AnimatorCondition{Parameter=jmp,Mode=AnimatorConditionMode.If}} });
        sm.AnyStateTransitions.Add(new AnimatorTransition { DestinationStateId=atkSt.Id,  Conditions={new AnimatorCondition{Parameter=atk,Mode=AnimatorConditionMode.If}} });

        ctrl.Layers.Add(new AnimatorControllerLayer { Name="Base", StateMachine=sm, DefaultWeight=1f });

        var anim = Entity.Get<AnimatorComponent>();
        if (anim != null) anim.Controller = ctrl;
        else Entity.Add(new AnimatorComponent { Controller = ctrl });
    }
}
```

### 控制脚本

```csharp
// AnimationController.cs — SyncScript（纯动画控制版）
public class AnimationController : SyncScript
{
    private AnimatorComponent animator;

    public override void Start() => animator = Entity.Get<AnimatorComponent>();

    public override void Update()
    {
        if (animator == null) return;
        var input = Input;

        float speed = 0f;
        if (input.IsKeyDown(Keys.W)) speed = input.IsKeyDown(Keys.LeftShift) ? 8f : 4f;
        animator.SetFloat("Speed", speed);

        if (input.IsKeyPressed(Keys.Space)) animator.SetTrigger("Jump");
        if (input.IsMouseButtonPressed(MouseButton.Left)) animator.SetTrigger("Attack");

        var info = animator.GetCurrentAnimatorStateInfo(0);
        if (info.IsValid) DebugText.Print($"State: {info.StateName}", new Int2(10, 10));
    }
}
```

---

## 示例 2：分层动画（FPS 武器）

```csharp
public class FPSAnimSetup : StartupScript
{
    public AnimationClip IdlePose, Reload, Fire;

    public override void Start()
    {
        var ctrl = new AnimatorController();

        var reloadP = new AnimatorParameter { Name="Reload", Type=AnimatorParameterType.Trigger };
        var fireP   = new AnimatorParameter { Name="Fire",   Type=AnimatorParameterType.Trigger };
        ctrl.Parameters.Add(reloadP); ctrl.Parameters.Add(fireP);

        var weaponSM = new AnimatorStateMachine { Name="Weapon" };
        var idleSt  = new AnimatorState { Name="Idle",   Motion=new AnimationClipMotion(IdlePose) };
        var reloadSt= new AnimatorState { Name="Reload", Motion=new AnimationClipMotion(Reload) };
        var fireSt  = new AnimatorState { Name="Fire",   Motion=new AnimationClipMotion(Fire) };
        weaponSM.States.Add(idleSt); weaponSM.States.Add(reloadSt); weaponSM.States.Add(fireSt);
        weaponSM.DefaultStateId = idleSt.Id;

        weaponSM.AnyStateTransitions.Add(new AnimatorTransition { DestinationStateId=reloadSt.Id, Conditions={new AnimatorCondition{Parameter=reloadP,Mode=AnimatorConditionMode.If}} });
        weaponSM.AnyStateTransitions.Add(new AnimatorTransition { DestinationStateId=fireSt.Id,   Conditions={new AnimatorCondition{Parameter=fireP,Mode=AnimatorConditionMode.If}} });

        ctrl.Layers.Add(new AnimatorControllerLayer { Name="Base", StateMachine=/* 移动状态机 */, DefaultWeight=1f });
        ctrl.Layers.Add(new AnimatorControllerLayer
        {
            Name = "Weapon",
            StateMachine = weaponSM,
            AvatarMask = AvatarMask.CreateBodyPart(AvatarMask.AvatarMaskBodyPart.RightArm, AvatarMask.AvatarMaskBodyPart.LeftArm),
            DefaultWeight = 1f,
            BlendingMode = AnimatorLayerBlendingMode.Override,
        });

        Entity.Add(new AnimatorComponent { Controller = ctrl });
    }
}
```

---

## 示例 3：使用 Override Controller

```csharp
// 男性和女性共享同一个状态机逻辑，只替换动画 clip
var overrideCtrl = new AnimatorOverrideController
{
    BaseController = sharedController,
    Overrides =
    {
        new AnimatorOverrideController.ClipOverridePair { OriginalClip=maleIdle, ReplacementClip=femaleIdle },
        new AnimatorOverrideController.ClipOverridePair { OriginalClip=maleWalk, ReplacementClip=femaleWalk },
        new AnimatorOverrideController.ClipOverridePair { OriginalClip=maleJump, ReplacementClip=femaleJump },
    }
};

var runtimeController = overrideCtrl.ApplyOverrides();
Entity.Add(new AnimatorComponent { Controller = runtimeController });
```

---

## 示例 4：纯代码构建（不使用 .sdctrl 文件）

```csharp
public class FullCodeSetup : StartupScript
{
    public AnimationClip idle, walk, run, jump;

    public override void Start()
    {
        // 1. 创建控制器
        var ctrl = new AnimatorController();

        // 2. 参数
        var speed = new AnimatorParameter { Name="Speed", Type=AnimatorParameterType.Float };
        var jmp   = new AnimatorParameter { Name="Jump",  Type=AnimatorParameterType.Trigger };
        ctrl.Parameters.Add(speed); ctrl.Parameters.Add(jmp);

        // 3. 混合树
        var moveMotion = new BlendTree1D { BlendParameter=speed, Thresholds={0f,3f,7f},
            Motions={new AnimationClipMotion(idle), new AnimationClipMotion(walk), new AnimationClipMotion(run)} };

        // 4. 状态
        var moveSt = new AnimatorState { Name="Move", Motion=moveMotion };
        var jumpSt = new AnimatorState { Name="Jump", Motion=new AnimationClipMotion(jump) };

        // 5. 过渡
        jumpSt.Transitions.Add(new AnimatorTransition { DestinationStateId=moveSt.Id, HasExitTime=true, ExitTime=0.9f });

        // 6. 状态机
        var sm = new AnimatorStateMachine { Name="Base", DefaultStateId=moveSt.Id };
        sm.States.Add(moveSt); sm.States.Add(jumpSt);
        sm.AnyStateTransitions.Add(new AnimatorTransition { DestinationStateId=jumpSt.Id,
            Conditions={new AnimatorCondition{Parameter=jmp,Mode=AnimatorConditionMode.If}} });

        // 7. 层 + 组件
        ctrl.Layers.Add(new AnimatorControllerLayer { Name="Base", StateMachine=sm, DefaultWeight=1f });
        Entity.Add(new AnimatorComponent { Controller = ctrl });
    }
}
```

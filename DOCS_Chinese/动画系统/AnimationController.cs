// AnimationController.cs — 纯动画控制脚本（自建控制器 + clip 拖入）
// 挂到带 AnimatorComponent 的 Entity 上
// ⚠️ 在 GameStudio 属性面板把 5 个 animation clip 拖入对应属性

using Stride.Engine;
using Stride.Input;
using Stride.Animations;
using Stride.Animations.Animator;
using Stride.Core.Mathematics;
using System;

namespace MyGame
{
    public class AnimationController : SyncScript
    {
        // ═══ 拖入 animation clip ═══
        public AnimationClip IdleClip { get; set; }
        public AnimationClip WalkClip { get; set; }
        public AnimationClip RunClip { get; set; }
        public AnimationClip JumpClip { get; set; }
        public AnimationClip AttackClip { get; set; }

        private AnimatorComponent animator;

        public override void Start()
        {
            animator = Entity.Get<AnimatorComponent>();
            if (animator == null) { animator = new AnimatorComponent(); Entity.Add(animator); }

            // 构建 AnimatorController（不需要 .sdctrl 文件）
            animator.Controller = BuildController();
        }

        private AnimatorController BuildController()
        {
            var c = new AnimatorController();

            // 参数
            var spd = new AnimatorParameter { Name = "Speed", Type = AnimatorParameterType.Float };
            var jmp = new AnimatorParameter { Name = "Jump", Type = AnimatorParameterType.Trigger };
            var atk = new AnimatorParameter { Name = "Attack", Type = AnimatorParameterType.Trigger };
            c.Parameters.Add(spd); c.Parameters.Add(jmp); c.Parameters.Add(atk);

            // 状态
            var idle = NewState("Idle", IdleClip);
            var walk = NewState("Walk", WalkClip);
            var run = NewState("Run", RunClip);
            var jump = NewState("Jump", JumpClip);
            var attack = NewState("Attack", AttackClip);

            // Idle ↔ Walk (Speed)
            idle.Transitions.Add(Trans(walk.Id, spd, AnimatorConditionMode.Greater, 0.1f));
            walk.Transitions.Add(Trans(idle.Id, spd, AnimatorConditionMode.Less, 0.1f));

            // Walk ↔ Run (Speed)
            walk.Transitions.Add(Trans(run.Id, spd, AnimatorConditionMode.Greater, 3f));
            run.Transitions.Add(Trans(walk.Id, spd, AnimatorConditionMode.Less, 3f));

            // Jump → Idle / Attack → Idle
            jump.Transitions.Add(new AnimatorTransition { DestinationStateId = idle.Id, HasExitTime = true, ExitTime = 0.9f, TransitionDuration = 0.15f });
            attack.Transitions.Add(new AnimatorTransition { DestinationStateId = idle.Id, HasExitTime = true, ExitTime = 0.85f, TransitionDuration = 0.15f });

            // 状态机
            var sm = new AnimatorStateMachine { Name = "Base", DefaultStateId = idle.Id };
            sm.States.Add(idle); sm.States.Add(walk); sm.States.Add(run);
            sm.States.Add(jump); sm.States.Add(attack);

            // Any State
            sm.AnyStateTransitions.Add(new AnimatorTransition
            {
                DestinationStateId = jump.Id,
                Conditions = { new AnimatorCondition { Parameter = jmp, Mode = AnimatorConditionMode.If } }
            });
            sm.AnyStateTransitions.Add(new AnimatorTransition
            {
                DestinationStateId = attack.Id,
                Conditions = { new AnimatorCondition { Parameter = atk, Mode = AnimatorConditionMode.If } }
            });

            c.Layers.Add(new AnimatorControllerLayer { Name = "Base", StateMachine = sm, DefaultWeight = 1f });
            return c;
        }

        static AnimatorState NewState(string name, AnimationClip clip)
            => new() { Name = name, Motion = clip != null ? new AnimationClipMotion(clip) : null };

        static AnimatorTransition Trans(Guid dst, AnimatorParameter p, AnimatorConditionMode mode, float thresh)
            => new()
            {
                DestinationStateId = dst,
                HasExitTime = false,
                Conditions = { new AnimatorCondition { Parameter = p, Mode = mode, FloatThreshold = thresh } }
            };

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
}
# 01 — 快速开始

## 5 分钟搭建第一个动画状态机

### 第一步：准备动画 Clip

动画 Clip 从 FBX/glTF 模型导入：

1. **Solution Explorer** → 右键 → **Add → Asset → Animation → Animation**
2. 选择模型文件，设置动画参数
3. 编译生成运行时 `.sdclip` 文件

### 第二步：创建 AnimatorController

1. **Solution Explorer** → 右键 → **Add → Asset → Animation → Animator Controller**
2. 命名为 `HeroController.sdctrl`
3. **双击**打开可视化编辑器

### 第三步：添加参数

在编辑器底部栏点击 `+` 按钮，添加参数：

| 参数名 | 类型 | 用途 |
|--------|------|------|
| Speed | Float | 驱动 Idle↔Walk↔Run |
| Jump | Trigger | 触发跳跃 |
| Attack | Trigger | 触发攻击 |

### 第四步：添加状态

点击工具栏 `+ State` 创建状态，双击状态节点重命名：

- `Idle`
- `Walk`
- `Jump`
- `Attack`

### 第五步：创建过渡

右键状态 → **Make Transition** → 点击目标状态：

| 从 | 到 | 条件 | ExitTime |
|----|----|------|----------|
| Idle | Walk | — | — |
| Walk | Idle | — | — |
| Any State | Jump | Jump / If | — |
| Any State | Attack | Attack / If | — |
| Jump | Idle | — | 0.9 |
| Attack | Idle | — | 0.85 |

### 第六步：设默认状态

右键 **Idle** → **Set as Default State**（出现绿色边框）

### 第七步：挂载到角色

1. 选中角色 Entity
2. **Add Component → Animator**
3. `Controller` 拖入 `HeroController.sdctrl`

### 第八步：写控制脚本

```csharp
using Stride.Engine;
using Stride.Input;
using Stride.Animations.Animator;

public class HeroControl : SyncScript
{
    private AnimatorComponent animator;

    public override void Start() => animator = Entity.Get<AnimatorComponent>();

    public override void Update()
    {
        if (animator == null) return;

        var input = Input;

        // 速度驱动 Idle/Walk
        float speed = 0f;
        if (input.IsKeyDown(Keys.W)) speed = input.IsKeyDown(Keys.LeftShift) ? 8f : 4f;
        animator.SetFloat("Speed", speed);

        // 跳跃
        if (input.IsKeyPressed(Keys.Space))
            animator.SetTrigger("Jump");

        // 攻击
        if (input.IsMouseButtonPressed(MouseButton.Left))
            animator.SetTrigger("Attack");
    }
}
```

### 运行

按 `W` → Idle 过渡到 Walk。按 `W+Shift` → 加速到 Run。按 `Space` → 跳跃。鼠标左键 → 攻击。

# 01 — 快速开始

## 5 分钟完成骨骼重定向

### 场景

角色 A（瘦子 Mixamo）播放 walk 动画，角色 B（胖子 Blender）自动重定向。

### 第一步：为两个角色创建 Avatar

在 GameStudio 中：

1. **Solution Explorer → Add → Animation → Humanoid Avatar**
2. 命名 `Hero_Avatar.sdavatar`
3. 拖入角色 A 的 Skeleton → 属性面板点击 **Auto Configure**
4. BoneMap 自动填充（如 `Hips→0, Spine→1, Chest→2...`）
5. 重复为角色 B 创建 `Enemy_Avatar.sdavatar`

### 第二步：挂载 AnimatorComponent

两个角色都加 `AnimatorComponent` 和 `ModelComponent`。

### 第三步：写脚本

```csharp
using Stride.Engine;
using Stride.Animations.Humanoid;

public class RetargetSetup : StartupScript
{
    public Entity sourceEntity;  // 角色A（有动画）
    public Entity targetEntity;  // 角色B（要重定向到它）

    public override void Start()
    {
        var src = sourceEntity.Get<AnimatorComponent>();
        var dst = targetEntity.Get<AnimatorComponent>();

        // 设置来源 → 自动重定向
        dst.RetargetSource = src;

        // 如果 dst 没有自己的 Controller，可以不设
        // dst 会从 src 自动重定向动画
    }
}
```

### 第四步：运行

角色 B 自动播放角色 A 的动画。不同的骨骼命名、不同的骨架比例——通过 Muscle Space 自动适配。

### 如果自动映射失败

某些非标准命名的骨骼会上报 missing。在属性面板手动设置对应关系：

```
BoneMap:
  Hips → 0
  Spine → 2
  Chest → 3
  ...
```

### 如果重定向结果异常

检查：

1. **两个角色是否都是人形**——非人形角色无法映射
2. **T-Pose 是否正确**——角色必须在 T-Pose（手臂水平、腿直立）
3. **Muscle Limits 是否过小**——在属性面板调整范围

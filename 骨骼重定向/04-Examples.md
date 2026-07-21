# 04 - 完整示例

## 示例 1: 两个角色共享动画

```csharp
using Stride.Engine;
using Stride.Animations.Humanoid;

public class SharedAnimationSetup : StartupScript
{
    public Entity heroEntity;   // 主角 (有动画 + Avatar)
    public Entity npcEntity;    // NPC (只有 Avatar, 无动画)

    public override void Start()
    {
        var hero = heroEntity.Get<AnimatorComponent>();
        var npc   = npcEntity.Get<AnimatorComponent>();

        // 主角播放正常动画
        hero.Controller = heroController;

        // NPC 从主角重定向
        npc.RetargetSource = hero;

        // 不需要给 NPC 设 Controller
        // 处理器自动从 hero 重定向到 npc
    }
}
```

## 示例 2: 手动控制重定向

```csharp
public class ManualRetarget : SyncScript
{
    public Entity sourceEntity;
    public Entity targetEntity;
    private HumanoidRetargetEngine engine;
    private ModelComponent srcModel, dstModel;

    public override void Start()
    {
        var srcHumanoid = new Humanoid
        {
            Skeleton = sourceEntity.Get<AnimatorComponent>().Skeleton,
            Avatar = sourceEntity.Get<AnimatorComponent>().Avatar,
        };
        srcHumanoid.AutoConfigure();

        var dstHumanoid = new Humanoid
        {
            Skeleton = targetEntity.Get<AnimatorComponent>().Skeleton,
            Avatar = targetEntity.Get<AnimatorComponent>().Avatar,
        };
        dstHumanoid.AutoConfigure();

        engine = srcHumanoid.CreateRetargetEngine(dstHumanoid);
        srcModel = sourceEntity.Get<ModelComponent>();
        dstModel = targetEntity.Get<ModelComponent>();
    }

    public override void Update()
    {
        if (engine == null || srcModel?.Skeleton == null || dstModel?.Skeleton == null)
            return;

        var srcUpdater = srcModel.Skeleton as SkeletonUpdater;
        var dstUpdater = dstModel.Skeleton as SkeletonUpdater;
        engine.ApplyRetarget(srcUpdater, dstUpdater);
    }
}
```

## 示例 3: 使用模板复用配置

```csharp
// 角色A: 保存映射为模板
var template = HumanoidTemplate.FromBoneMap(
    "Mixamo Standard Rig",
    avatarA.BoneMap,
    skeletonA);

// 角色C (也是 Mixamo 导出): 直接套用
var avatarC = new HumanoidAvatar();
avatarC.BoneMap = template.Apply(skeletonC);
avatarC.IsValid = true;
```

## 示例 4: 一键自动配置

```csharp
// 创建并自动配置
var humanoid = new Humanoid { Skeleton = mySkeleton };
var missingBones = humanoid.AutoConfigure();

if (missingBones.Count == 0)
    Console.WriteLine("All 17 required bones mapped successfully");
else
    Console.WriteLine($"Missing: {string.Join(", ", missingBones)}");

// 保存 Avatar 到资产
avatarAsset.BoneMap = humanoid.Avatar.BoneMap;
avatarAsset.MuscleLimits = humanoid.Avatar.MuscleLimits;
```

## 示例 5: 单骨骼重定向

```csharp
// 只重定向左臂，不碰其他骨骼
var engine = srcHumanoid.CreateRetargetEngine(dstHumanoid);

// 读取源角色的左臂旋转
var srcBoneIdx = srcAvatar.BoneMap[HumanoidBone.LeftUpperArm];
var srcRot = srcUpdater.NodeTransformations[srcBoneIdx].Transform.Rotation;

// 重定向到目标角色
var dstRot = engine.RetargetBone(HumanoidBone.LeftUpperArm, srcRot);

// 写入目标
var dstBoneIdx = dstAvatar.BoneMap[HumanoidBone.LeftUpperArm];
dstUpdater.NodeTransformations[dstBoneIdx].Transform.Rotation = dstRot;
```

## 示例 6: 验证骨骼映射

```csharp
// 自动映射
HumanoidBoneMap.AutoMap(nodeNames, out var boneMap);

// 验证
var errors = HumanoidBoneValidator.Validate(boneMap, skeleton);

if (errors.Count > 0)
{
    Console.WriteLine("Validation failed:");
    foreach (var e in errors)
        Console.WriteLine($"  - {e}");
}
else
{
    Console.WriteLine("All required bones mapped and valid.");
}
```

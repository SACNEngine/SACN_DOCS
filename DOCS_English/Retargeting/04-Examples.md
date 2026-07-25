# 04 - Complete examples

## Example 1: Two characters sharing an animation

```csharp
using Stride.Engine;
using Stride.Animations.Humanoid;

public class SharedAnimationSetup : StartupScript
{
    public Entity heroEntity;   // Hero (has animation + Avatar)
    public Entity npcEntity;    // NPC (only has Avatar, no animation)

    public override void Start()
    {
        var hero = heroEntity.Get<AnimatorComponent>();
        var npc   = npcEntity.Get<AnimatorComponent>();

        // Hero plays normal animation
        hero.Controller = heroController;

        // NPC retargets from hero
        npc.RetargetSource = hero;

        // No need to set a Controller for the NPC
        // The processor automatically retargets from hero to npc
    }
}
```

## Example 2: Manually controlling retargeting

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

## Example 3: Reusing configuration with templates

```csharp
// Character A: save the mapping as a template
var template = HumanoidTemplate.FromBoneMap(
    "Mixamo Standard Rig",
    avatarA.BoneMap,
    skeletonA);

// Character C (also Mixamo-exported): apply directly
var avatarC = new HumanoidAvatar();
avatarC.BoneMap = template.Apply(skeletonC);
avatarC.IsValid = true;
```

## Example 4: One-click auto-configuration

```csharp
// Create and auto-configure
var humanoid = new Humanoid { Skeleton = mySkeleton };
var missingBones = humanoid.AutoConfigure();

if (missingBones.Count == 0)
    Console.WriteLine("All 17 required bones mapped successfully");
else
    Console.WriteLine($"Missing: {string.Join(", ", missingBones)}");

// Save Avatar to asset
avatarAsset.BoneMap = humanoid.Avatar.BoneMap;
avatarAsset.MuscleLimits = humanoid.Avatar.MuscleLimits;
```

## Example 5: Single-bone retargeting

```csharp
// Retarget only the left arm, leave other bones untouched
var engine = srcHumanoid.CreateRetargetEngine(dstHumanoid);

// Read the source character's left arm rotation
var srcBoneIdx = srcAvatar.BoneMap[HumanoidBone.LeftUpperArm];
var srcRot = srcUpdater.NodeTransformations[srcBoneIdx].Transform.Rotation;

// Retarget to the target character
var dstRot = engine.RetargetBone(HumanoidBone.LeftUpperArm, srcRot);

// Write to the target
var dstBoneIdx = dstAvatar.BoneMap[HumanoidBone.LeftUpperArm];
dstUpdater.NodeTransformations[dstBoneIdx].Transform.Rotation = dstRot;
```

## Example 6: Validating bone mapping

```csharp
// Auto mapping
HumanoidBoneMap.AutoMap(nodeNames, out var boneMap);

// Validate
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

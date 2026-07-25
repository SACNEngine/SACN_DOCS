# 03 — API 参考

## HumanoidBone（标准骨骼枚举）

```csharp
// 必需骨骼（17个）
HumanoidBone.Hips          // 髋
HumanoidBone.Spine         // 脊椎
HumanoidBone.Chest         // 胸部
HumanoidBone.Neck          // 颈部
HumanoidBone.Head          // 头部
HumanoidBone.LeftUpperLeg  // 左大腿
HumanoidBone.LeftLowerLeg  // 左小腿
HumanoidBone.LeftFoot      // 左脚
HumanoidBone.RightUpperLeg // 右大腿
HumanoidBone.RightLowerLeg // 右小腿
HumanoidBone.RightFoot     // 右脚
HumanoidBone.LeftUpperArm  // 左上臂
HumanoidBone.LeftLowerArm  // 左前臂
HumanoidBone.LeftHand      // 左手
HumanoidBone.RightUpperArm // 右上臂
HumanoidBone.RightLowerArm // 右前臂
HumanoidBone.RightHand     // 右手

// 可选骨骼
HumanoidBone.UpperChest    // 上胸部
HumanoidBone.LeftShoulder  // 左肩
HumanoidBone.RightShoulder // 右肩
HumanoidBone.LeftToes      // 左脚趾
HumanoidBone.RightToes     // 右脚趾
// + 30 个手指骨骼 (LeftThumb1..RightLittle3)
```

## HumanoidMuscleId（肌肉枚举）

```csharp
// 脊椎 (3)
SpineFrontBack, SpineLeftRight, SpineTwistLeftRight

// 胸部 (3)
ChestFrontBack, ChestLeftRight, ChestTwistLeftRight

// 颈部 (3)
NeckNodDownUp, NeckTiltLeftRight, NeckTurnLeftRight

// 头部 (3)
HeadNodDownUp, HeadTiltLeftRight, HeadTurnLeftRight

// 左臂 (3) + 左前臂 (2) + 左手 (2)
LeftArmDownUp, LeftArmFrontBack, LeftArmTwistInOut
LeftForearmStretch, LeftForearmTwistInOut
LeftHandDownUp, LeftHandInOut

// 右臂 (3) + 右前臂 (2) + 右手 (2)
RightArmDownUp, RightArmFrontBack, RightArmTwistInOut
RightForearmStretch, RightForearmTwistInOut
RightHandDownUp, RightHandInOut

// 左腿 (7)
LeftUpperLegFrontBack, LeftUpperLegInOut, LeftUpperLegTwistInOut
LeftLowerLegStretch, LeftLowerLegTwistInOut
LeftFootUpDown, LeftFootTwistInOut

// 右腿 (7)
RightUpperLegFrontBack, RightUpperLegInOut, RightUpperLegTwistInOut
RightLowerLegStretch, RightLowerLegTwistInOut
RightFootUpDown, RightFootTwistInOut
```

## HumanoidAvatar

```csharp
// 属性
Dictionary<HumanoidBone, int> BoneMap           // 骨骼→节点索引
Dictionary<HumanoidMuscleId, MuscleLimit> Limits // 肌肉范围
Dictionary<HumanoidBone, Quaternion> ReferencePose // T-Pose参考
bool IsValid                                     // 所有必需骨骼是否已映射

// 方法
MuscleLimit GetMuscleLimit(HumanoidMuscleId)  // 获取肌肉范围
HumanoidBone GetBoneForIndex(int nodeIndex)    // 节点→标准骨骼
bool HasBone(HumanoidBone)                     // 骨骼是否已映射
static HumanoidBone[] RequiredBones           // 必需骨骼列表
```

## Humanoid（顶层包装）

```csharp
var humanoid = new Humanoid
{
    Skeleton = mySkeleton,    // 骨骼引用
    Avatar = myAvatar,        // Avatar 配置
};

// 方法
humanoid.AutoConfigure()                        // 一键自动配置
humanoid.CreateRetargetEngine(target)           // 创建重定向引擎
bool IsValid                                    // 是否有效
```

## HumanoidRetargetEngine

```csharp
// 创建
var engine = srcHumanoid.CreateRetargetEngine(dstHumanoid);

// 方法
Quaternion RetargetBone(HumanoidBone bone, Quaternion srcRot)
    // 单个骨骼重定向

void ApplyRetarget(SkeletonUpdater src, SkeletonUpdater dst)
    // 整个骨架重定向（每帧调用）

int RetargetableBoneCount  // 可重定向的骨骼数
```

## HumanoidBoneMap

```csharp
// 静态方法
List<HumanoidBone> AutoMap(string[] nodeNames, out Dictionary<HumanoidBone, int> map)
    // 自动名称匹配

string GetDisplayName(HumanoidBone bone)
    // 骨骼枚举 → 人类可读名称
```

## HumanoidBoneValidator

```csharp
// 静态方法
List<string> Validate(
    Dictionary<HumanoidBone, int> boneMap,
    Skeleton skeleton)
    // 验证：父子关系、链连通性、对称性
    // 返回空列表 = 验证通过
```

## HumanoidTemplate

```csharp
// 保存映射配置
var template = HumanoidTemplate.FromBoneMap("MyRig", boneMap, skeleton);

// 应用到新骨架
var newMap = template.Apply(skeleton2);

// 属性
string Name              // 模板名称
Dictionary<HumanoidBone, string> BoneNameMap  // 骨骼→名称映射
```

## HumanoidMuscleSpace

```csharp
// 静态方法
float BoneToMuscle(muscleId, boneRot, tPoseRot, limit?)
    // 骨骼旋转 → Muscle 值

Quaternion MuscleToBone(muscleId, muscleValue, tPoseRot, limit?)
    // Muscle 值 → 骨骼旋转

Dictionary<HumanoidMuscleId, float> BoneToAllMuscles(bone, rot, tPose)
    // 提取一个骨骼的所有 muscle 值

Quaternion AllMusclesToBone(bone, muscleValues, tPose)
    // 从所有 muscle 值重建骨骼旋转

bool IsValidMuscle(muscleId)     // 是否是有效 muscle
HumanoidBone GetMuscleBone(muscleId)  // muscle 控制哪个骨骼
```

## HumanoidTPose

```csharp
// 静态方法
Dictionary<HumanoidBone, Quaternion> ExtractReferencePose(avatar, skeleton)
    // 提取 T-Pose 参考旋转

float CheckTPoseConfidence(avatar, skeleton)
    // 检测骨架离 T-Pose 的偏差 [0,1]

Dictionary<HumanoidMuscleId, MuscleLimit> ComputeDefaultLimits(avatar)
    // 计算默认肌肉范围
```

## AnimatorComponent（重定向相关）

```csharp
// 属性
HumanoidAvatar Avatar                  // 此角色的 Avatar
AnimatorComponent RetargetSource      // 从哪个角色重定向
HumanoidRetargetEngine RetargetEngine  // 自动创建的重定向引擎

// 方法
Humanoid GetHumanoid()                          // 构建 Humanoid 包装
HumanoidRetargetEngine CreateRetargetEngine(target) // 创建引擎
```

## BakeRetargetedClip (新增)

```csharp
// HumanoidRetargetEngine
AnimationClip BakeRetargetedClip(
    AnimationClip sourceClip,   // 源骨架的动画
    float sampleRate = 30f      // 采样帧率 (30=每秒30帧)
)
```

### 使用

```csharp
var engine = srcHumanoid.CreateRetargetEngine(dstHumanoid);
var retargetedClip = engine.BakeRetargetedClip(sourceAnim, 30f);
// retargetedClip 可直接保存或在 AnimatorController 中使用
```

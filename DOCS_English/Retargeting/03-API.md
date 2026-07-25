# 03 — API reference

## HumanoidBone (standard bone enum)

```csharp
// Required bones (17)
HumanoidBone.Hips          // Hips
HumanoidBone.Spine         // Spine
HumanoidBone.Chest         // Chest
HumanoidBone.Neck          // Neck
HumanoidBone.Head          // Head
HumanoidBone.LeftUpperLeg  // Left upper leg
HumanoidBone.LeftLowerLeg  // Left lower leg
HumanoidBone.LeftFoot      // Left foot
HumanoidBone.RightUpperLeg // Right upper leg
HumanoidBone.RightLowerLeg // Right lower leg
HumanoidBone.RightFoot     // Right foot
HumanoidBone.LeftUpperArm  // Left upper arm
HumanoidBone.LeftLowerArm  // Left lower arm
HumanoidBone.LeftHand      // Left hand
HumanoidBone.RightUpperArm // Right upper arm
HumanoidBone.RightLowerArm // Right lower arm
HumanoidBone.RightHand     // Right hand

// Optional bones
HumanoidBone.UpperChest    // Upper chest
HumanoidBone.LeftShoulder  // Left shoulder
HumanoidBone.RightShoulder // Right shoulder
HumanoidBone.LeftToes      // Left toes
HumanoidBone.RightToes     // Right toes
// + 30 finger bones (LeftThumb1..RightLittle3)
```

## HumanoidMuscleId (muscle enum)

```csharp
// Spine (3)
SpineFrontBack, SpineLeftRight, SpineTwistLeftRight

// Chest (3)
ChestFrontBack, ChestLeftRight, ChestTwistLeftRight

// Neck (3)
NeckNodDownUp, NeckTiltLeftRight, NeckTurnLeftRight

// Head (3)
HeadNodDownUp, HeadTiltLeftRight, HeadTurnLeftRight

// Left arm (3) + left forearm (2) + left hand (2)
LeftArmDownUp, LeftArmFrontBack, LeftArmTwistInOut
LeftForearmStretch, LeftForearmTwistInOut
LeftHandDownUp, LeftHandInOut

// Right arm (3) + right forearm (2) + right hand (2)
RightArmDownUp, RightArmFrontBack, RightArmTwistInOut
RightForearmStretch, RightForearmTwistInOut
RightHandDownUp, RightHandInOut

// Left leg (7)
LeftUpperLegFrontBack, LeftUpperLegInOut, LeftUpperLegTwistInOut
LeftLowerLegStretch, LeftLowerLegTwistInOut
LeftFootUpDown, LeftFootTwistInOut

// Right leg (7)
RightUpperLegFrontBack, RightUpperLegInOut, RightUpperLegTwistInOut
RightLowerLegStretch, RightLowerLegTwistInOut
RightFootUpDown, RightFootTwistInOut
```

## HumanoidAvatar

```csharp
// Properties
Dictionary<HumanoidBone, int> BoneMap           // Bone → node index
Dictionary<HumanoidMuscleId, MuscleLimit> Limits // Muscle range
Dictionary<HumanoidBone, Quaternion> ReferencePose // T-Pose reference
bool IsValid                                     // Whether all required bones are mapped

// Methods
MuscleLimit GetMuscleLimit(HumanoidMuscleId)  // Get muscle range
HumanoidBone GetBoneForIndex(int nodeIndex)    // Node → standard bone
bool HasBone(HumanoidBone)                     // Whether bone is mapped
static HumanoidBone[] RequiredBones           // List of required bones
```

## Humanoid (top-level wrapper)

```csharp
var humanoid = new Humanoid
{
    Skeleton = mySkeleton,    // Skeleton reference
    Avatar = myAvatar,        // Avatar configuration
};

// Methods
humanoid.AutoConfigure()                        // One-click auto-configure
humanoid.CreateRetargetEngine(target)           // Create retargeting engine
bool IsValid                                    // Whether valid
```

## HumanoidRetargetEngine

```csharp
// Create
var engine = srcHumanoid.CreateRetargetEngine(dstHumanoid);

// Methods
Quaternion RetargetBone(HumanoidBone bone, Quaternion srcRot)
    // Single-bone retargeting

void ApplyRetarget(SkeletonUpdater src, SkeletonUpdater dst)
    // Whole-skeleton retargeting (call each frame)

int RetargetableBoneCount  // Number of retargetable bones
```

## HumanoidBoneMap

```csharp
// Static methods
List<HumanoidBone> AutoMap(string[] nodeNames, out Dictionary<HumanoidBone, int> map)
    // Automatic name matching

string GetDisplayName(HumanoidBone bone)
    // Bone enum → human-readable name
```

## HumanoidBoneValidator

```csharp
// Static methods
List<string> Validate(
    Dictionary<HumanoidBone, int> boneMap,
    Skeleton skeleton)
    // Validation: parent-child relationships, chain connectivity, symmetry
    // Returns empty list = validation passed
```

## HumanoidTemplate

```csharp
// Save mapping configuration
var template = HumanoidTemplate.FromBoneMap("MyRig", boneMap, skeleton);

// Apply to a new skeleton
var newMap = template.Apply(skeleton2);

// Properties
string Name              // Template name
Dictionary<HumanoidBone, string> BoneNameMap  // Bone → name mapping
```

## HumanoidMuscleSpace

```csharp
// Static methods
float BoneToMuscle(muscleId, boneRot, tPoseRot, limit?)
    // Bone rotation → Muscle value

Quaternion MuscleToBone(muscleId, muscleValue, tPoseRot, limit?)
    // Muscle value → Bone rotation

Dictionary<HumanoidMuscleId, float> BoneToAllMuscles(bone, rot, tPose)
    // Extract all muscle values of one bone

Quaternion AllMusclesToBone(bone, muscleValues, tPose)
    // Reconstruct bone rotation from all muscle values

bool IsValidMuscle(muscleId)     // Whether it is a valid muscle
HumanoidBone GetMuscleBone(muscleId)  // Which bone the muscle controls
```

## HumanoidTPose

```csharp
// Static methods
Dictionary<HumanoidBone, Quaternion> ExtractReferencePose(avatar, skeleton)
    // Extract T-Pose reference rotation

float CheckTPoseConfidence(avatar, skeleton)
    // Detect skeleton deviation from T-Pose [0,1]

Dictionary<HumanoidMuscleId, MuscleLimit> ComputeDefaultLimits(avatar)
    // Compute default muscle range
```

## AnimatorComponent (retargeting related)

```csharp
// Properties
HumanoidAvatar Avatar                  // This character's Avatar
AnimatorComponent RetargetSource      // Which character to retarget from
HumanoidRetargetEngine RetargetEngine  // Automatically created retargeting engine

// Methods
Humanoid GetHumanoid()                          // Build Humanoid wrapper
HumanoidRetargetEngine CreateRetargetEngine(target) // Create engine
```

## BakeRetargetedClip (new)

```csharp
// HumanoidRetargetEngine
AnimationClip BakeRetargetedClip(
    AnimationClip sourceClip,   // Animation of the source skeleton
    float sampleRate = 30f      // Sampling frame rate (30 = 30 frames per second)
)
```

### Usage

```csharp
var engine = srcHumanoid.CreateRetargetEngine(dstHumanoid);
var retargetedClip = engine.BakeRetargetedClip(sourceAnim, 30f);
// retargetedClip can be saved directly or used in an AnimatorController
```

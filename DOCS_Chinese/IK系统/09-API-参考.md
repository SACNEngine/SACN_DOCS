# 09 — API 参考

## IkComponent

```csharp
namespace Stride.Animations.IK;

[DataContract("IkComponent")]
[Display("IK")]
[DefaultEntityComponentProcessor(typeof(IkProcessor),
    ExecutionMode = ExecutionMode.Runtime
                  | ExecutionMode.Thumbnail
                  | ExecutionMode.Preview)]
[ComponentOrder(2101)]
public class IkComponent : EntityComponent
{
    public List<IkChainSetup> Chains { get; }
}
```

## IkChainSetup

```csharp
public enum IkType { TwoBone, LookAt, CCD, FABRIK, MultiAim }

[DataContract]
public class IkChainSetup
{
    [DataMember(0)]  public IkType Type = IkType.TwoBone;
    [DataMember(5)]  public string Bone;         // LookAt/MultiAim
    [DataMember(10)] public string RootBone;     // TwoBone/CCD/FABRIK
    [DataMember(15)] public string MidBone;      // TwoBone only
    [DataMember(20)] public string TipBone;      // TwoBone/CCD/FABRIK
    [DataMember(30)] public Vector3 TargetPosition;
    [DataMember(40)] public Vector3 HintPosition; // TwoBone
    [DataMember(50)] public float Weight = 1f;
    [DataMember(60)] public int CcdIterations = 5; // CCD/FABRIK
}
```

## TwoBoneIKSolver

```csharp
public static class TwoBoneIKSolver
{
    // 二骨 IK
    public static void Solve(
        SkeletonUpdater skeleton,
        int rootIndex, int midIndex, int tipIndex,
        Vector3 targetPosition, Vector3 hintPosition,
        float weight = 1f);

    // 注视 IK
    public static void SolveLookAt(
        SkeletonUpdater skeleton, int boneIndex,
        Vector3 target, Vector3 forward, Vector3 up,
        float clampAngle = 0f, float weight = 1f);

    // CCD
    public static void SolveCCD(
        SkeletonUpdater skeleton,
        int chainStart, int chainEnd,
        Vector3 target, float weight = 1f,
        int maxIterations = 5);

    // FABRIK
    public static void SolveFABRIK(
        SkeletonUpdater skeleton,
        int chainStart, int chainEnd,
        Vector3 target, float weight = 1f,
        int iterations = 5);

    // 瞄准约束
    public static void SolveMultiAim(
        SkeletonUpdater skeleton, int boneIndex,
        Vector3 target, Vector3 aimAxis,
        Vector3 upAxis, Vector3 worldUp,
        float weight = 1f);
}
```

## IkTargetGizmo

```csharp
[DataContract("IkTargetGizmo")]
[Display("IK/Target Gizmo")]
public class IkTargetGizmo : SyncScript
{
    [DataMember(0)]  public int ChainIndex = 0;
    [DataMember(10)] public Color3 SphereColor = new(0.2f, 0.9f, 0.3f);
}
```

## IkProcessor

```csharp
public class IkProcessor : EntityProcessor<IkComponent>
{
    public IkProcessor();      // Order = -499
}
```

## 文件位置

```
sources/engine/Stride.Engine/Animations/IK/
├── TwoBoneIKSolver.cs
├── IkComponent.cs
└── IkTargetGizmo.cs
```

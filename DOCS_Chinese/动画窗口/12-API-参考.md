# 12 — API 参考

## AnimationWindowViewModel

```csharp
// 属性
public float CurrentTime { get; set; }    // 当前播放时间 (秒)
public float Duration { get; set; }        // 动画总时长 (秒)
public bool IsPlaying { get; set; }        // 播放中
public bool IsRecording { get; set; }      // 录制模式
public Entity TargetEntity { get; set; }   // 录制目标实体
public PropertyRowViewModel SelectedProperty { get; set; }
public AnimationCurveInterpolationType SelInterp { get; set; }
public bool CanUndo { get; }
public bool CanRedo { get; }

// 命令
public ICommandBase PlayCommand { get; }
public ICommandBase PauseCommand { get; }
public ICommandBase StopCommand { get; }
public ICommandBase RecordCommand { get; }
public ICommandBase AddKeyframeCommand { get; }
public ICommandBase AddEventCommand { get; }
public ICommandBase SaveCommand { get; }
public ICommandBase LoadCommand { get; }
public ICommandBase UndoCommand { get; }
public ICommandBase RedoCommand { get; }
public ICommandBase CopyCommand { get; }
public ICommandBase PasteCommand { get; }

// 方法
public void SetTargetEntity(Entity entity);       // 设置录制目标
public AnimationClip GetBuiltClip();              // 构建 AnimationClip
internal void MoveKey(PropertyRowViewModel prop, float oldTime, float newTime);
internal void DeleteKey(PropertyRowViewModel prop, float time);
internal void AddKeyAt(string path, float time, object value);
```

## AnimationClipBuilder

```csharp
public class AnimationClipBuilder
{
    public void AddKeyframe(string path, CompressedTimeSpan time, float value);
    public void AddKeyframe(string path, CompressedTimeSpan time, Vector3 value);
    public void AddKeyframe(string path, CompressedTimeSpan time, Quaternion value);
    public bool RemoveKeyframeAtTime(string path, CompressedTimeSpan time);
    public float Evaluate(string path, CompressedTimeSpan time);
    public Vector3 EvaluateVector3(string path, CompressedTimeSpan time);
    public Quaternion EvaluateQuaternion(string path, CompressedTimeSpan time);
    public AnimationClip Build(bool optimize = true);
    public void LoadFromClip(AnimationClip clip);
}
```

## PlayAnimationClip

```csharp
[Display("Animation/Play Animation Clip")]
public class PlayAnimationClip : SyncScript
{
    public string ClipPath;              // 文件名
    public bool PlayOnStart = true;
    public AnimationRepeatMode RepeatMode = LoopInfinite;
    public void Play();
    public void Stop();
    public void Pause();
}
```

## AnimProp（常量）

```csharp
public static class AnimProp
{
    public const string Position = "[TransformComponent.Key].Position";
    public const string Rotation = "[TransformComponent.Key].Rotation";
    public const string Scale    = "[TransformComponent.Key].Scale";
}
```

## AnimationClipAsset

```csharp
[AssetDescription(".sdanimclip")]
[AssetContentType(typeof(AnimationClip))]
public class AnimationClipAsset : Asset
{
    public float Duration;
    public List<ClipCurveEntry> Curves;
    public List<ClipEventEntry> Events;
    public Skeleton Skeleton;
    public Model PreviewModel;
}
```

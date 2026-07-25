# 12 — API Reference

## AnimationWindowViewModel

```csharp
// Properties
public float CurrentTime { get; set; }    // current playback time (seconds)
public float Duration { get; set; }        // total animation duration (seconds)
public bool IsPlaying { get; set; }        // playing
public bool IsRecording { get; set; }      // recording mode
public Entity TargetEntity { get; set; }   // recording target entity
public PropertyRowViewModel SelectedProperty { get; set; }
public AnimationCurveInterpolationType SelInterp { get; set; }
public bool CanUndo { get; }
public bool CanRedo { get; }

// Commands
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

// Methods
public void SetTargetEntity(Entity entity);       // set recording target
public AnimationClip GetBuiltClip();              // build AnimationClip
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
    public string ClipPath;              // file name
    public bool PlayOnStart = true;
    public AnimationRepeatMode RepeatMode = LoopInfinite;
    public void Play();
    public void Stop();
    public void Pause();
}
```

## AnimProp (constants)

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

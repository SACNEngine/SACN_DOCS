# 07 — Save and Load

## Save (.sdanimclip)

### Operation

1. 💾 Click save
2. Choose the project `Assets/` directory
3. Enter a file name (e.g. `WalkAnim`)

### Output files

| File | Format | Purpose |
|------|--------|---------|
| `WalkAnim.sdanimclip` | YAML | Stride asset (shown in Asset View) |
| `WalkAnim.sdclip.json` | JSON | Read directly at runtime (PlayAnimationClip) |

### Asset registration

After saving, automatically:
1. Scan for a matching Package
2. Call `Package.LoadTemporaryAssets()` to re-scan the disk
3. Add a new `AssetItem` to `Package.Assets`
4. Call `CheckConsistency()` to trigger a UI refresh
5. Asset View displays it immediately

### YAML format

```yaml
!AnimationClipAsset
Duration: 5.0
Curves:
  - Path: '[TransformComponent.Key].Position'
    Interpolation: Linear
    Keys:
      - Time: 0.0
        X: 1.0
        Y: 2.0
        Z: 0.0
        ValueType: Vector3
```

## Load

### Load from file

1. 📂 Click load
2. Select the `.sdanimclip` file
3. Properties, keyframes, and events are fully restored

### Double-click in Asset View

1. Double-click `.sdanimclip` in Asset View
2. The animation window opens automatically
3. Data loads automatically

## Compile to runtime

```
.sdanimclip (YAML)
     ↓ AssetFileSerializer.Save
  disk file
     ↓ double-click in Asset View
  AnimationClipAssetCompiler
     ↓ build AnimationClip
  ContentManager.Save(url, clip)
     ↓
  Content.Load<AnimationClip>(url) → available at runtime
```

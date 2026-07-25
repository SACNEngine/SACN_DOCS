# 13 — Troubleshooting

## Entity does not follow the animation

| Possible cause | Check | Solution |
|---------------|-------|----------|
| No entity selected | TargetName shows "(select entity)" | Click the entity in the scene |
| No keyframes | No diamonds in the Dopesheet | Add keyframes with +Key |
| Not playing | ▶ button not pressed | Click ▶ or drag the time slider |

## ▶ Does not play

| Possible cause | Solution |
|---------------|----------|
| No keyframes | Add at least one with +Key |
| Duration=0 | Set Duration > 0 in the property panel |

## 💾 Asset View does not show after saving

| Possible cause | Solution |
|---------------|----------|
| File is outside the Assets folder | Save under the project `Assets/` |
| Not refreshed | Right-click Assets → Reload |
| Stride file monitoring did not trigger | Restart the project |

## .sdanimclip console error

```
Failed to load asset: assetType is null
```
→ An old JSON-format file used the `.sdanimclip` extension
→ Delete the file and re-save from the animation window

## Keyframes empty after loading

| Possible cause | Solution |
|---------------|----------|
| Loaded an old-format JSON file | Delete the old file and re-save |
| YAML deserialization failed | Check whether the file is complete |

## Double-clicking .sdanimclip does nothing

→ Check whether the latest code is compiled correctly
→ `AssetEditorsManager.OpenAssetEditorWindow` has an `AnimationClipAsset` branch

## Keyframe values look wrong

| Problem | Cause |
|---------|-------|
| All values identical | The entity was not moved while recording |
| Values jump | Keyframe times overlap |
| Scale abnormal | The Scale value may have been modified |

## File locations

| File | Path |
|------|------|
| Project Assets | `MyGame3.Game/Assets/*.sdanimclip` |
| Editor source | `sources/editor/Stride.Assets.Presentation/AnimationWindow/` |
| Engine source | `sources/engine/Stride.Engine/Animations/` |

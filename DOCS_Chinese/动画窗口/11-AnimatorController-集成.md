# 11 — AnimatorController 集成

## 动画用于状态机

### 方式一：PlayAnimationClip 组件

最简单。直接将脚本挂实体上，填入 ClipPath。

### 方式二：AnimatorController State Motion

1. 动画窗口制作 → 保存 `.sdanimclip`
2. **构建项目**（Ctrl+Shift+B）→ 编译 clip
3. 打开 AnimatorController 可视化编辑器
4. 选中 State → 属性面板
5. Motion → 选 `AnimationClipMotion`
6. Clip → 下拉框选你的动画

```
State: Walk
  Motion: AnimationClipMotion
    Clip: WalkAnim  ← 编译后的 AnimationClip
```

## 编译必要性

`.sdanimclip` → `Content.Load<>()` 需要先编译。构建项目（Ctrl+Shift+B）执行编译。

## 完整流程

```
动画窗口 → 做动画 → 💾 保存 .sdanimclip
                        ↓
              Ctrl+Shift+B 构建
                        ↓
              Content 数据库有 AnimationClip
                        ↓
              AnimatorController State.Motion = AnimationClipMotion(Clip)
                        ↓
              AnimatorComponent.Controller = .sdctrl
                        ↓
              F5 → 状态机播放动画
```

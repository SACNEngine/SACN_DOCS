# 10 — 速查表

## 可视化编辑器快捷键

| 操作 | 快捷键/方式 |
|------|------------|
| 创建状态 | 工具栏 `+ State` |
| 移动状态 | 左键拖拽 |
| 重命名状态 | 双击节点 |
| 选择状态 | 左键单击 |
| 多选状态 | Ctrl+单击 |
| 删除状态 | Delete 键 |
| 创建过渡 | 右键源状态 → Make Transition → 点击目标 |
| 删除过渡 | 右键过渡箭头 |
| 设默认状态 | 右键状态 → Set as Default State |
| 缩放 | 滚轮 |
| 平移 | 中键拖拽 |
| 取消 | Esc |

## 参数操作

```csharp
animator.SetFloat("Speed", 5f);
animator.SetBool("IsAiming", true);
animator.SetTrigger("Jump");
animator.SetInt("Weapon", 1);

float s = animator.GetFloat("Speed");
bool b  = animator.GetBool("IsAiming");
int i   = animator.GetInt("Weapon");
```

## 状态查询

```csharp
var info = animator.GetCurrentAnimatorStateInfo(0);
// info.StateName, info.NormalizedTime, info.LoopCount

bool trans = animator.IsInTransition(0);
var tInfo  = animator.GetTransitionInfo(0);
```

## 条件模式

| 模式 | Float | Bool | Trigger |
|------|-------|------|---------|
| `If` | >0 | true | fired |
| `IfNot` | <0 | false | — |
| `Greater` | >thresh | — | — |
| `Less` | <thresh | — | — |
| `Equals` | ==thresh | ==thresh | — |
| `NotEqual` | !=thresh | !=thresh | — |

## 过渡缓动

| 模式 | 曲线 |
|------|------|
| `Linear` | 匀速 |
| `EaseIn` | 慢→快 |
| `EaseOut` | 快→慢 |
| `EaseInOut` | 慢→快→慢 |

## 文件扩展名

| 扩展名 | 说明 |
|--------|------|
| `.sdctrl` | AnimatorController 资产 |
| `.sdanim` | Animation 导入设置 |
| `.sdclip` | 编译后运行时 AnimationClip |
| `.sdskel` | Skeleton 骨骼定义 |

## 常用过渡时长

| 过渡 | Duration |
|------|----------|
| Idle↔Walk | 0.2s |
| Walk↔Run | 0.15s |
| Move→Jump | 0.1s |
| Jump→Land | 0.15s |
| →Attack | 0.05s |
| Attack→Idle | 0.15s |

## Layer 混合模式

| 模式 | 使用场景 |
|------|---------|
| `Override` | 上半身武器、瞄准 |
| `Additive` | 呼吸动画、受伤抖动 |

## Motion 类型

| 类型 | 使用场景 |
|------|---------|
| `AnimationClipMotion` | 单个动画 |
| `BlendTree1D` | 速度驱动的移动 |
| `BlendTree2D` | 方向驱动的移动 |
| `BlendTreeDirect` | 脚本控制权重 |

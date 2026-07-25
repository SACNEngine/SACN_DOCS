# Stride IK 系统 — 目录

## 文档索引

| 文档 | 内容 |
|------|------|
| [01 - 系统概述](01-系统概述.md) | 架构、执行流程、IkComponent 配置 |
| [02 - TwoBoneIK](02-TwoBoneIK.md) | 二骨 IK（手臂/腿部） |
| [03 - LookAt IK](03-LookAt-IK.md) | 头部注视、眼球跟踪 |
| [04 - CCD](04-CCD.md) | 循环坐标下降（尾巴/触手） |
| [05 - FABRIK](05-FABRIK.md) | 前后传递 IK（长链快速收敛） |
| [06 - MultiAim](06-MultiAim.md) | 武器瞄准约束 |
| [07 - AnimatorController 集成](07-AnimatorController-集成.md) | IK Pass、OnStateIK 回调 |
| [08 - Gizmo 可视化](08-Gizmo-可视化.md) | IkTargetGizmo 场景拖拽 |
| [09 - API 参考](09-API-参考.md) | 完整 API 签名和参数 |
| [10 - 故障排查](10-故障排查.md) | 常见问题解决 |

## 快速开始

1. 选中角色实体 → 添加 **IkComponent**
2. 对照 [01-系统概述](01-系统概述.md) 找到骨骼名
3. 按需求选文档配置对应 IK 类型
4. F5 测试

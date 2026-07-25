# 10 — Troubleshooting

## IK has no effect at all

| Possible cause | How to check | Solution |
|----------------|-------------|----------|
| Bone name mismatch | Compare against the exact Skeleton.Nodes names | Copy the bone name exactly (including case and spaces) |
| Weight = 0 | Check Chains.Weight in the property panel | Set to 1.0 to test |
| Entity has no Skeleton | Added IkComponent but the Entity has no ModelComponent | Confirm the model is imported and attached |
| IkComponent not added | Check the property panel | Add Component → IK |

## Bone name mismatch

Common mistakes:
```
✗ "UpperArm_L"     → actual is "LeftUpperArm"
✗ "Left Upper Arm" → actual is "LeftUpperArm" (no spaces)
✗ "leftupperarm"   → actual is "LeftUpperArm" (case-sensitive)
✗ "Hand_L"         → actual is "LeftHand"
```

**Correct approach**: ModelComponent → Skeleton → Nodes, inspect each entry and copy exactly.

## IK causes weird bending

| Problem | Cause | Solution |
|---------|-------|----------|
| Elbow bends the wrong way | HintPosition is wrong | Adjust the Hint position, usually set to Target + a forward offset |
| Arm rotates too much | Weight too high | Lower to 0.5-0.7 |
| Joint jitter | Too few CCD iterations | Increase CcdIterations |
| Root moves too much | Using CCD instead of FABRIK | Switch long chains to FABRIK |

## Animation and IK conflict

```
If the animation already defines a hand position, and IK also changes that same position:
  Weight < 1.0 → animation dominates, IK fine-tunes
  Weight = 1.0 → IK fully overrides the animation
```

## Performance

- Multiple characters running IK at the same time is fine (O(n) in bone count).
- Set unneeded Chains to `Weight=0` to skip computation.
- FABRIK 3 iterations is usually enough.
- Keep CCD within 5-8 iterations.

## No IK preview in the editor

Confirm that the IkComponent's Processor registers the Preview mode:
```csharp
ExecutionMode.Runtime | ExecutionMode.Thumbnail | ExecutionMode.Preview
```

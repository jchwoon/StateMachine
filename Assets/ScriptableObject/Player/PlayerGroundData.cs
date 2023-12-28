using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerGroundData 
{
    [field: SerializeField] [field: Range(0f, 25f)] public float BaseSpeed { get; set; } = 5.0f;
    [field: SerializeField][field: Range(0f, 25f)] public float BaseRotationDamping { get; set; } = 1.0f;

    [field: Header("WalkData")]
    [field: SerializeField][field: Range(0f, 2.0f)] public float WalkSpeedModifier { get; set; } = 0.225f;
    [field: Header("RunData")]
    [field: SerializeField][field: Range(0, 2.0f)] public float RunSpeedModifier { get; set; } = 1.0f;
}

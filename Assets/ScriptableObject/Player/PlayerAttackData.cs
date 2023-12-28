using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AttackInfoData
{
    [field: SerializeField] public string AttackName { get; set; }
    [field: SerializeField] public int ComboStateIndex { get; set; }
    [field: SerializeField] public int Damage { get; set; }
    [field: SerializeField][field: Range(0.0f, 1.0f)] public float ComboTransitionTime { get; set; }
    [field: SerializeField][field: Range(0.0f, 3.0f)] public float ForceTransitionTime { get; set; }
    [field: SerializeField][field: Range(-10.0f, 10.0f)] public float Force { get; set; }
}

[Serializable]
public class PlayerAttackData
{
    [field: SerializeField] public List<AttackInfoData> AttackInfoDatas { get; set; }

    public int GetAttackInfoCount()
    {
        return AttackInfoDatas.Count;
    }

    public AttackInfoData GetAttackInfo(int index)
    {
        return AttackInfoDatas[index];
    }

}

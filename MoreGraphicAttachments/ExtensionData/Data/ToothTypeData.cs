using MBMScripts;
using System;
using UnityEngine;

namespace MoreGraphicAttachments.ExtensionData.Data;

[Serializable]
public class ToothTypeData : ExtensionCharacterData
{
    public string m_CharacterClass = null!;

    public int[] m_ToothTypeList = [0];

    public float[] m_ToothTypeChanceList = null!;

    public int GetInitialToothType(int defaultIndex)
    {
        if (m_ToothTypeList == null || m_ToothTypeList.Length == 0)
            return 0;

        if (m_ToothTypeChanceList == null)
            return m_ToothTypeList[Mathf.Clamp(defaultIndex, 0, m_ToothTypeList.Length - 1)];

        return m_ToothTypeList[SeqUtil.GetIndexOfChance(m_ToothTypeChanceList)];
    }
}

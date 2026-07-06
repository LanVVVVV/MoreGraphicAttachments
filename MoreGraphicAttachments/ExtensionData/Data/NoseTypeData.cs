using MBMScripts;
using System;
using UnityEngine;

namespace MoreGraphicAttachments.ExtensionData.Data;

[Serializable]
public class NoseTypeData : ExtensionCharacterData
{
    public string m_CharacterClass = null!;

    public int[] m_NoseTypeList = [0];

    public float[] m_NoseTypeChanceList = null!;

    public int GetInitialNoseType(int defaultIndex)
    {
        if (m_NoseTypeList == null || m_NoseTypeList.Length == 0)
            return 0;

        if (m_NoseTypeChanceList == null)
            return m_NoseTypeList[Mathf.Clamp(defaultIndex, 0, m_NoseTypeList.Length - 1)];

        return m_NoseTypeList[SeqUtil.GetIndexOfChance(m_NoseTypeChanceList)];
    }
}

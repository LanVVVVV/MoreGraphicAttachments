using MBMScripts;
using MoreGraphicAttachments.Core;
using MoreGraphicAttachments.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MoreGraphicAttachments.ExtensionData.Data;

[Serializable]
public class HairBundleTypeData : ExtensionCharacterData
{
    public string m_CharacterClass = null!;

    public int[] m_HairBundleTypeList = [ 0 ];

    public float[] m_HairBundleTypeChanceList = null!;

    public int GetInitialHairBundleType(int defaultIndex)
    {
        if (m_HairBundleTypeList == null || m_HairBundleTypeList.Length == 0)
            return 0;

        if (m_HairBundleTypeChanceList == null)
            return m_HairBundleTypeList[Mathf.Clamp(defaultIndex, 0, m_HairBundleTypeList.Length - 1)];

        return m_HairBundleTypeList[SeqUtil.GetIndexOfChance(m_HairBundleTypeChanceList)];
    }

    public int[] HairBundleTypeList => m_HairBundleTypeList;

    public static List<int> GlobalHairBundleTypeList { get; set; } = [];

    public static void BuildGlobalHairBundleTypeList()
    {
        List<int> allTypes = [];

        foreach (var type in CharacterClassifier.SpeciesTypes)
        {
            var data = CharacterExtensionDataMap<HairBundleTypeData>.Get(type);
            if (data?.HairBundleTypeList != null)
            {
                allTypes.AddRange(data.HairBundleTypeList);
            }
        }

        GlobalHairBundleTypeList = allTypes
            .Distinct()
            .OrderBy(x => x)
            .ToList();

        ModEntry.Log($"[HairBundleTypeData] GlobalHairBundleTypeList = [{string.Join(", ", GlobalHairBundleTypeList)}]");
    }
}

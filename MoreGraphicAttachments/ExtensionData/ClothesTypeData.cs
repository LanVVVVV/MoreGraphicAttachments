using MBMScripts;
using MoreGraphicAttachments.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MoreGraphicAttachments.ExtensionData;

[Serializable]
public class ClothesTypeData : ExtensionCharacterData
{
    public string m_CharacterClass = null!;

    public int[] m_ClothesTypeList = [1];

    public float[] m_ClothesTypeChanceList = null!;

    public int GetInitialClothesType(int defaultIndex)
    {
        if (m_ClothesTypeList == null || m_ClothesTypeList.Length == 0)
            return 0;

        if (m_ClothesTypeChanceList == null)
            return m_ClothesTypeList[Mathf.Clamp(defaultIndex, 0, m_ClothesTypeList.Length - 1)];

        return m_ClothesTypeList[SeqUtil.GetIndexOfChance(m_ClothesTypeChanceList)];
    }

    public int[] ClothesTypeList => m_ClothesTypeList;

    public static List<int> GlobalClothesTypeList { get; set; } = [];

    public static void BuildGlobalClothesTypeList()
    {
        List<int> allTypes = [];

        foreach (var type in CharacterClassifier.SpeciesTypes)
        {
            var data = CharacterExtensionDataMap<ClothesTypeData>.Get(type);
            if (data?.ClothesTypeList != null)
            {
                allTypes.AddRange(data.ClothesTypeList);
            }
        }

        GlobalClothesTypeList = allTypes
            .Distinct()
            .OrderBy(x => x)
            .ToList();

        ModEntry.Log($"[ClothesTypeData] GlobalClothesTypeList = [{string.Join(", ", GlobalClothesTypeList)}]");
    }
}

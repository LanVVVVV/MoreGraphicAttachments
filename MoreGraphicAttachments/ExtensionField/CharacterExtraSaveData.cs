using MBMScripts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace MoreGraphicAttachments.ExtensionField;

[Serializable]
public class CharacterExtraSaveData
{
    public int UnitId;
    public string ClothColorHex = null!;

    [JsonConstructor]
    public CharacterExtraSaveData(){ }

    public CharacterExtraSaveData(Character character, CharacterExtra extra)
    {
        UnitId = character.UnitId;
        ClothColorHex = "#" + ColorUtility.ToHtmlStringRGBA(extra.m_ClothColor);
    }
}

[Serializable]
public class CharacterExtraSaveDataWrapper
{
    public List<CharacterExtraSaveData> Characters = new();
}
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
    public string ClothesColorHex = null!;

    [JsonConstructor]
    public CharacterExtraSaveData(){ }

    public CharacterExtraSaveData(Character character, CharacterExtra extra)
    {
        UnitId = character.UnitId;
        ClothesColorHex = "#" + ColorUtility.ToHtmlStringRGBA(extra.m_ClothesColor);
    }
}

[Serializable]
public class CharacterExtraSaveDataWrapper
{
    public List<CharacterExtraSaveData> Characters = new();
}
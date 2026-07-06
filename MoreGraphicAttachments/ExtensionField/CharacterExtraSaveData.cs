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
    public int HairBundleType = 0;


    [JsonConstructor]
    public CharacterExtraSaveData(){ }

    public CharacterExtraSaveData(Character character, CharacterExtra extra)
    {
        UnitId = character.UnitId;
        if(extra.ClothesColorNeedSave())
            ClothesColorHex = "#" + ColorUtility.ToHtmlStringRGBA(extra.ClothesColor);
        if (extra.HairBundleTypeNeedSave())
            HairBundleType = extra.HairBundleType;
    }
}

[Serializable]
public class CharacterExtraSaveDataWrapper
{
    public List<CharacterExtraSaveData> Characters = new();
}
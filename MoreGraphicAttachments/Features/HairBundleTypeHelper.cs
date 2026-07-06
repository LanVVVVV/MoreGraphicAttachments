using MBMScripts;
using MoreGraphicAttachments.ExtensionData;
using MoreGraphicAttachments.ExtensionData.Data;
using MoreGraphicAttachments.ExtensionField;
using System;
using System.Collections.Generic;

namespace MoreGraphicAttachments.Features;

public static class HairBundleTypeHelper
{
    public static void ChangeHairBundleTypeRight(this Character character)
    {
        int[] list = CharacterExtensionDataMap<HairBundleTypeData>.Get(character).HairBundleTypeList;
        int index = Array.IndexOf(list, character.Extra().HairBundleType);
        if (index == -1)
            index = 0;
        else
            index = (index + 1) % list.Length;
        character.Extra().HairBundleType = list[index];
    }

    public static void ChangeHairBundleTypeLeft(this Character character)
    {
        int[] list = CharacterExtensionDataMap<HairBundleTypeData>.Get(character).HairBundleTypeList;
        int index = Array.IndexOf(list, character.Extra().HairBundleType);
        if (index == -1) 
            index = 0;
        else
            index = (index - 1 + list.Length) % list.Length;
        character.Extra().HairBundleType = list[index];
    }

    public static void ChangeHairBundleTypeRightInAll(this Character character)
    {
        List<int> list = HairBundleTypeData.GlobalHairBundleTypeList;
        int index = list.IndexOf(character.Extra().HairBundleType);
        if (index == -1)
            index = 0;
        else
            index = (index + 1) % list.Count;
        character.Extra().HairBundleType = list[index];
    }

    public static void ChangeHairBundleTypeLeftInAll(this Character character)
    {
        List<int> list = HairBundleTypeData.GlobalHairBundleTypeList;
        int index = list.IndexOf(character.Extra().HairBundleType);
        if (index == -1)
            index = 0;
        else
            index = (index - 1 + list.Count) % list.Count;
        character.Extra().HairBundleType = list[index];
    }
}
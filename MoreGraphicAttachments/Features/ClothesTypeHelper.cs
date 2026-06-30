using HarmonyLib;
using MBMScripts;
using MoreGraphicAttachments.ExtensionData;
using MoreGraphicAttachments.ExtensionData.Data;
using System;
using System.Collections.Generic;

namespace MoreGraphicAttachments.Features;

public static class ClothesTypeHelper
{
    private static readonly AccessTools.FieldRef<Character, int> ClothesTypeRef =
        AccessTools.FieldRefAccess<Character, int>("m_ClothesType");

    public static bool ClothesTypeGetterPatch(Character __instance, out int __result, int ___m_ClothesType)
    {
        SeqDataBinding.Instance.RegisterFlag(__instance, "ClothesType");
        __result = ___m_ClothesType < 0 ? 0 : ___m_ClothesType;
        return false;
    }

    public static int GetClothesType(this Character character)
    {
        SeqDataBinding.Instance.RegisterFlag(character, "ClothesType");
        var rawClothesType = character.RawClothesType();
        return rawClothesType == 0 ? 1 : Math.Abs(rawClothesType);
    }

    public static void SetClothesType(this Character character, int clothesType)
    {
        if(character.RawClothesType() <= 0)
            clothesType = -clothesType;

        if(clothesType == -1)
            clothesType = 0;

        character.ClothesType = clothesType;
    }

    public static int RawClothesType(this Character character)
    {
        return ClothesTypeRef(character);
    }

    public static int ToggleClothes(Character character)
    {
        int clothesType = character.RawClothesType();

        if (clothesType == 0)
            return 1;
        else if (clothesType == 1)
            return 0;
        else
            return -clothesType;
    }

    public static void ChangeClothesTypeRight(this Character character)
    {
        int[] list = CharacterExtensionDataMap<ClothesTypeData>.Get(character).ClothesTypeList;
        int index = Array.IndexOf(list, character.GetClothesType());
        if (index == -1) 
            index = 0;
        else 
            index = (index + 1) % list.Length;
        character.SetClothesType(list[index]);
    }

    public static void ChangeClothesTypeLeft(this Character character)
    {
        int[] list = CharacterExtensionDataMap<ClothesTypeData>.Get(character).ClothesTypeList;
        int index = Array.IndexOf(list, character.GetClothesType());
        if (index == -1) 
            index = 0;
        else
            index = (index - 1 + list.Length) % list.Length;
        character.SetClothesType(list[index]);
    }

    public static void ChangeClothesTypeRightInAll(this Character character)
    {
        List<int> list = ClothesTypeData.GlobalClothesTypeList;
        int index = list.IndexOf(character.GetClothesType());
        if (index == -1)
            index = 0;
        else
            index = (index + 1) % list.Count;
        character.SetClothesType(list[index]);
    }

    public static void ChangeClothesTypeLeftInAll(this Character character)
    {
        List<int> list = ClothesTypeData.GlobalClothesTypeList;
        int index = list.IndexOf(character.GetClothesType());
        if (index == -1)
            index = 0;
        else
            index = (index - 1 + list.Count) % list.Count;
        character.SetClothesType(list[index]);
    }

    public static void ChangeBusinessPartnerType(this Female businessPartner)
    {
        businessPartner.ClothesType = ((businessPartner.ClothesType == 1) ? 2 : 1);
    }
}
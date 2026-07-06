using HarmonyLib;
using MBMScripts;
using MoreGraphicAttachments.ExtensionField;
using MoreGraphicAttachments.ExtensionSpine;
using MoreGraphicAttachments.ExtensionSpineData;
using System.Collections.Generic;

namespace MoreGraphicAttachments.Patches.UnitPatches;

[HarmonyPatch(typeof(Unit))]
public static class UnitPatch
{
    [HarmonyPatch(nameof(Unit.GetPartListCollection))]
    [HarmonyPostfix]
    public static void GetPartListCollectionPostfix(SpineData spineData, Character female, Character male, ref Unit.PartListCollection __result)
    {
        var extra = __result.Extra();

        if (extra.ClothesColorSlotNameList is not null)
            return;

        extra.ClothesColorSlotNameList = spineData.Extra().SlaveClothesColorPartList;
    }

    [HarmonyPatch("NewShowPartList")]
    [HarmonyPostfix]
    public static void NewShowPartListPostfix(SpineData spineData, Character female, Character male, List<string> partList, List<List<string>> partDictionary, ref List<string> __result)
    {
        for (int i = 0; i < __result.Count; i++)
        {
            string text = __result[i];

            if (female != null)
            {
                text = text.Replace("{HairBundleType}", $"{female.Extra().HairBundleType}");
            }

            __result[i] = text;
        }
    }
}
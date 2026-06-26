using HarmonyLib;
using MBMScripts;
using MoreGraphicAttachments.ExtensionSpine;
using MoreGraphicAttachments.ExtensionSpineData;

namespace MoreGraphicAttachments.Patches.UnitPatches;

[HarmonyPatch(typeof(Unit), nameof(Unit.GetPartListCollection))]
public static class UnitPatch
{
    static void Postfix(SpineData spineData, Character female, Character male, ref Unit.PartListCollection __result)
    {
        var extra = __result.Extra();

        if (extra.ClothesColorSlotNameList is not null)
            return;

        extra.ClothesColorSlotNameList = spineData.Extra().SlaveClothesColorPartList;
    }
}
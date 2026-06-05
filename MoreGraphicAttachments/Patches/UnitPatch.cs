using HarmonyLib;
using MBMScripts;
using MoreGraphicAttachments.ExtensionSpine;
using MoreGraphicAttachments.ExtensionSpineData;

[HarmonyPatch(typeof(Unit), nameof(Unit.GetPartListCollection))]
public static class UnitPatch
{
    static void Postfix(SpineData spineData, Character female, Character male, ref Unit.PartListCollection __result)
    {
        var extra = __result.Extra();

        if (extra.ClothColorSlotNameList is not null)
            return;

        extra.ClothColorSlotNameList = spineData.Extra().SlaveClothColorPartList;
    }
}
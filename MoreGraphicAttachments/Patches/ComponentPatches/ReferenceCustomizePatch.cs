using HarmonyLib;
using MBMScripts;

namespace MoreGraphicAttachments.Patches.ComponentPatches;

[HarmonyPatch(typeof(ReferenceCustomize))]
public static class ReferenceCustomizePatch
{
    private const int ClothesEDataType = 7;

    [HarmonyPatch(nameof(ReferenceCustomize.GetString))]
    [HarmonyPrefix]
    public static bool GetStringPrefix(ReferenceCustomize __instance, ref string __result, int ___m_DataType)
    {
        if (__instance.Updater.TargetUnit?.Unit is not Character character)
        {
            __result = string.Empty;
            return false;
        }

        if (___m_DataType == ClothesEDataType)
        {
            __result = character.ClothesType == 0 ? "X" : "O";
            return false;
        }
        return true;
    }
}
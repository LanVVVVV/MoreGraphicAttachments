using HarmonyLib;
using MBMScripts;

namespace MoreGraphicAttachments.Patches.ComponentPatches;

[HarmonyPatch(typeof(ReferenceCharacterLook))]
public static class ReferenceCharacterLookPatch
{
    private const int ClothesEDataType = 72;

    [HarmonyPatch(nameof(ReferenceCharacterLook.GetBool))]
    [HarmonyPrefix]
    static bool GetBoolPrefix(ReferenceCharacterLook __instance, ref bool __result, int ___m_DataType)
    {
        if (__instance.Updater.TargetUnit?.Unit is not Character character)
        {
            __result = false;
            return false;
        }

        if (___m_DataType == ClothesEDataType)
        {
            __result = character.ClothesType != 0;
            return false;
        }
        return true;
    }
}
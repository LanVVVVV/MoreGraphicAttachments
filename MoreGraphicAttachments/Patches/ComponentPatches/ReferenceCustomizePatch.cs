using HarmonyLib;
using MBMScripts;

namespace MoreGraphicAttachments.Patches.ComponentPatches
{
    [HarmonyPatch(typeof(ReferenceCustomize))]
    public static class ReferenceCustomizePatch
    {
        private const int HairFrontType = 1;
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

            switch (___m_DataType)
            {
                case HairFrontType:
                    string text;
                    try
                    {
                        text = character.HairFrontType.ToString();
                    }
                    catch
                    {
                        text = string.Empty;
                    }
                    __result = string.Format(SeqLocalization.Localize("#TypeFormat"), text);
                    return false;

                case ClothesEDataType:
                    __result = character.ClothesType == 0 ? "X" : "O";
                    return false;

                default:
                    return true;
            }
        }
    }
}
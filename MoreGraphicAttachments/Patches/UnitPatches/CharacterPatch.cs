using HarmonyLib;
using MBMScripts;
using MoreGraphicAttachments.Features;

namespace MoreGraphicAttachments.Patches.UnitPatches;

[HarmonyPatch(typeof(Character))]
public class CharacterPatch
{
    [HarmonyPatch(nameof(Character.ClothesType), MethodType.Getter)]
    [HarmonyPrefix]
    public static bool ClothesTypeGetterPrefix(Character __instance, ref int __result, int ___m_ClothesType)
    {
        return ClothesTypeHelper.ClothesTypeGetterPatch(__instance, out __result, ___m_ClothesType);
    }
}
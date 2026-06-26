using HarmonyLib;
using MBMScripts;

namespace MoreGraphicAttachments.Patches.UnitPatches;

[HarmonyPatch(typeof(Character))]
public class CharacterPatch
{
    [HarmonyPatch(nameof(Character.ClothesType), MethodType.Getter)]
    [HarmonyPrefix]
    public static bool ClothesTypeGetterPrefix(Character __instance, ref int __result, int ___m_ClothesType)
    {
        SeqDataBinding.Instance.RegisterFlag(__instance, "ClothesType");
        __result = ___m_ClothesType < 0 ? 0 : ___m_ClothesType;
        return false;
    }
}
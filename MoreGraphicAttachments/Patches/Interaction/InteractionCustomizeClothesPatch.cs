using HarmonyLib;
using MBMScripts;
using MoreGraphicAttachments.Features;

namespace MoreGraphicAttachments.Patches
{
    [HarmonyPatch(typeof(InteractionCustomize))]
    public static class InteractionCustomizeClothesPatch
    {
        [HarmonyPatch(nameof(InteractionCustomize.ChangeClothes))]
        [HarmonyPrefix]
        public static bool ChangeClothesPrefix(InteractionCustomize __instance)
        {
            TargetUnit componentInParent = __instance.GetComponentInParent<TargetUnit>();
            if (!(componentInParent == null) && componentInParent.Unit is Female female)
            {
                female.ClothesType = ClothesTypeHelper.ToggleClothes(female);
            }
            return false;
        }
    }
}
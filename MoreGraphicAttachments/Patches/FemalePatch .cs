using HarmonyLib;
using MBMScripts;
using MoreGraphicAttachments.ExtensionData;
using MoreGraphicAttachments.ExtensionField;

namespace MoreGraphicAttachments.Patches;

[HarmonyPatch(typeof(Female))]
public class FemalePatch
{
    [HarmonyPatch(nameof(Female.InitializeAppearance))]
    [HarmonyPostfix]
    public static void InitializeAppearancePostfix(Female __instance)
    {
        __instance.Extra().ClothesColor = CharacterExtensionDataMap<ClothesColorData>.Get(__instance).GetInitialClothesColor(0);
        //ModEntry.Log($"{__instance.GetType().Name} {"#" + ColorUtility.ToHtmlStringRGBA(__instance.Extra().ClothesColor)}");
    }
}

using HarmonyLib;
using MBMScripts;
using MoreGraphicAttachments.Core;
using MoreGraphicAttachments.ExtensionData;
using MoreGraphicAttachments.ExtensionData.Data;
using MoreGraphicAttachments.ExtensionField;

namespace MoreGraphicAttachments.Patches.UnitPatches;

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

    [HarmonyPatch("Initialize", [typeof(Character), typeof(Character)])]
    [HarmonyPostfix]
    public static void InitializePostfix(Female __instance, Character female, Character male)
    {
        if (!ModConfig.EnableClothesColorInherit) return;
        __instance.Extra().ClothesColor = female.Extra().ClothesColor;
    }
}

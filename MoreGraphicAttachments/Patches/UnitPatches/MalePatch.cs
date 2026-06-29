using HarmonyLib;
using MBMScripts;
using MoreGraphicAttachments.ExtensionData;
using MoreGraphicAttachments.ExtensionData.Data;

namespace MoreGraphicAttachments.Patches.UnitPatches;

[HarmonyPatch(typeof(Male))]
public class MalePatch
{
    [HarmonyPatch(nameof(Male.InitializeAppearance))]
    [HarmonyPostfix]
    public static void InitializeAppearancePostfix(Male __instance)
    {
        __instance.NoseType = CharacterExtensionDataMap<NoseTypeData>.Get(__instance).GetInitialNoseType(0);
        __instance.ToothType = CharacterExtensionDataMap<ToothTypeData>.Get(__instance).GetInitialToothType(0);
    }
}

using HarmonyLib;
using MBMScripts;

namespace MoreGraphicAttachments.Patches;

[HarmonyPatch(typeof(PlayData))]
public static class SelectedUnitPatch
{
    [HarmonyPatch(nameof(PlayData.SelectedUnit), MethodType.Setter)]
    [HarmonyPostfix]
    public static void SelectedUnitPostfix(Unit value)
    {
        if (value is null) return;

        if (value is not Amilia && value is not Flora && value is not Niel && value is not SenaLena && value is not Barbara)
        {
            return;
        }
        GameManager.Instance.OpenWindow(EGameWindow.FemaleInformation);
    }
}
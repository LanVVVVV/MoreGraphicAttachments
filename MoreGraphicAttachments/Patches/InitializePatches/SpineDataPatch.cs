using HarmonyLib;
using MBMScripts;
using System;

namespace MoreGraphicAttachments.Patches.InitializePatches;

[HarmonyPatch(typeof(SpineData), nameof(SpineData.Initialize))]
public static class SpineDataPatch
{
    public static event Action? AfterSpineDataInitialized;

    public static void Postfix()
    {
        AfterSpineDataInitialized?.Invoke();
    }
}
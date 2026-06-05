using HarmonyLib;
using MBMScripts;
using System;

[HarmonyPatch(typeof(SpineData), nameof(SpineData.Initialize))]
public static class SpineDataPatch
{
    public static event Action? AfterSpineDataInitialized;

    static void Postfix()
    {
        AfterSpineDataInitialized?.Invoke();
    }
}
using HarmonyLib;
using MBMScripts;
using System;

namespace MoreGraphicAttachments.Patches.InitializePatches;

[HarmonyPatch(typeof(SeqObjectPoolManager), nameof(SeqObjectPoolManager.Initialize))]
public class SeqObjectPoolManagerPatch
{
    public static event Action? AfterGameInitialized;

    [HarmonyPostfix]
    public static void InitializePostfix()
    {
        AfterGameInitialized?.Invoke();
    }
}
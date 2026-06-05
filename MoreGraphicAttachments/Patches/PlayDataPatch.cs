using HarmonyLib;
using MBMScripts;
using System;

namespace MoreGraphicAttachments.Patches;

[HarmonyPatch(typeof(PlayData), nameof(PlayData.OnEnable))]
public static class PlayDataPatch
{
    public static event Action? AfterSaveDataInitialized;

    static void Postfix()
    {
        AfterSaveDataInitialized?.Invoke();
    }
}
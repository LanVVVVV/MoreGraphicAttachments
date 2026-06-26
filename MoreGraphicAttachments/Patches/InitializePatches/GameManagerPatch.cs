using HarmonyLib;
using MBMScripts;
using System;

namespace MoreGraphicAttachments.Patches.InitializePatches;

[HarmonyPatch(typeof(GameManager), "InitializeData")]
public static class GameManagerPatch
{
    public static event Action? AfterDataInitialized;

    static void Postfix()
    {
        AfterDataInitialized?.Invoke();
    }
}
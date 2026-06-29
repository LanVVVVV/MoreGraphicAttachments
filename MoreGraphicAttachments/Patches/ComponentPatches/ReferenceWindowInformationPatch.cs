using HarmonyLib;
using MBMScripts;
using MoreGraphicAttachments.Features;
using UnityEngine;

namespace MoreGraphicAttachments.Patches.ComponentPatches;

[HarmonyPatch(typeof(ReferenceWindowInformation))]
public class ReferenceWindowInformationPatch
{
    private const int SizeEDataType = 2;

    [HarmonyPatch(nameof(ReferenceWindowInformation.GetVector2))]
    [HarmonyPostfix]
    public static void GetVector2Postfix(ref Vector2 __result, int ___m_DataType, EGameWindow ___m_GameWindow)
    {
        if (___m_DataType != SizeEDataType) return;
        if (___m_GameWindow != EGameWindow.FemaleInformationLook) return;

        //FemaleInformationLook: (524 838)
        __result = InformationLookWindowHeight.AdjustHeight(__result, 1, 1);
    }
}
using HarmonyLib;
using MBMScripts;
using MoreGraphicAttachments.Features;
using UnityEngine;

namespace MoreGraphicAttachments.Patches.InteractionPatches;

[HarmonyPatch(typeof(InteractionUnit))]
public static class InteractionUnitOverPatch
{
    [HarmonyPatch(nameof(InteractionUnit.Over))]
    [HarmonyPostfix]
    public static void OverPostfix(TargetUnit ___m_TargetUnit)
    {
        if (___m_TargetUnit?.Unit is not Female { IsDisabled: false } female) return;
        if (!female.IsBusinessPartnerMainMansionTypes()) return;

        if (Input.GetKeyDown(KeyCode.K))
        {
            female.ChangeBusinessPartnerType();
        }
    }
}
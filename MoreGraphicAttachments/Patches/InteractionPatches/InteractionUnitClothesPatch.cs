using HarmonyLib;
using MBMScripts;
using MoreGraphicAttachments.Features;
using UnityEngine;

namespace MoreGraphicAttachments.Patches.InteractionPatches;

[HarmonyPatch(typeof(InteractionUnit))]
public static class InteractionUnitClothesPatch
{
    [HarmonyPatch(nameof(InteractionUnit.Clothes))]
    [HarmonyPrefix]
    public static bool ClothesPrefix(TargetUnit ___m_TargetUnit)
    {
        if (___m_TargetUnit?.Unit is not Female { IsDisabled: false } female) return false;


        if (Input.GetKey(KeyCode.LeftAlt))
        {
            SeqList<Unit> unitList = GameManager.Instance.PlayerData.GetUnitList(ESector.Female);

            int clothesType = ClothesTypeHelper.ToggleClothes(female);
            foreach (Unit unit in unitList)
            {
                if (unit is Female { IsDisabled: false } femaleInList)
                {
                    femaleInList.ClothesType = clothesType;
                }
            }
            return false;
        }

        female.ClothesType = ClothesTypeHelper.ToggleClothes(female);
        return false;
    }
}
using MBMScripts;
using MoreGraphicAttachments.Core;
using MoreGraphicAttachments.Features;
using MoreGraphicAttachments.Properties;
using MoreGraphicAttachments.UIComponents;
using SystemExtensionLib.Systems;
using SystemExtensionLib.Tools;
using UnityEngine;

namespace MoreGraphicAttachments.UI;

public static class ClothesTypeSlotUI
{
    private static bool _isInjected = false;

    public static void InjectSlot()
    {
        if (_isInjected) return;

        var clothTypeSlot = ExtendedInfoSlotSystem.RegisterFemaleExtendedChangeSlot(
            ModEntry.ModName, "Clothes Type",
            () => Strings.Slot_ClothesType,
            (arrowLeft) => OnLeftArrowClick(arrowLeft),
            (arrowRight) => OnRightArrowClick(arrowRight),
            out var typeValue);

        #region TypeValue
        var referenceClothesType = typeValue!.gameObject.AddComponent<ReferenceClothes>();
        referenceClothesType.DataType = ReferenceClothes.EDataType.Type;

        BinderTextMeshProText binderText = typeValue.GetComponent<BinderTextMeshProText>();
        binderText.SetReferenceArray([referenceClothesType]);
        #endregion

        _isInjected = true;
    }

    private static void OnLeftArrowClick(GameObject arrow)
    {
        if (arrow.GetComponentInParent<TargetUnit>().Unit is Character ch)
            ch.ChangeClothesTypeLeft();
    }

    private static void OnRightArrowClick(GameObject arrow)
    {
        if (arrow.GetComponentInParent<TargetUnit>().Unit is Character ch)
            ch.ChangeClothesTypeRight();
    }
}

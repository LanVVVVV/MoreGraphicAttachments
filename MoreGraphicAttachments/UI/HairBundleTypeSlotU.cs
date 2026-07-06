using MBMScripts;
using MoreGraphicAttachments.Core;
using MoreGraphicAttachments.Features;
using MoreGraphicAttachments.Properties;
using MoreGraphicAttachments.UIComponents;
using SystemExtensionLib.Systems;
using SystemExtensionLib.Tools;
using UnityEngine;

namespace MoreGraphicAttachments.UI;

public static class HairBundleTypeSlotUI
{
    private static bool _isInjected = false;

    public static void InjectSlot()
    {
        if (_isInjected) return;

        var clothTypeSlot = ExtendedInfoSlotSystem.RegisterFemaleExtendedChangeSlot(
            ModEntry.ModName, "HairBundle Type",
            () => Strings.Slot_HairBundleType,
            (arrowLeft) => OnLeftArrowClick(arrowLeft),
            (arrowRight) => OnRightArrowClick(arrowRight),
            out var typeValue);

        #region TypeValue
        var referenceHairBundleType = typeValue!.gameObject.AddComponent<ReferenceHairBundleType>();
        BinderTextMeshProText binderText = typeValue.GetComponent<BinderTextMeshProText>();
        binderText.SetReferenceArray([referenceHairBundleType]);
        #endregion

        _isInjected = true;
    }

    private static void OnLeftArrowClick(GameObject arrow)
    {
        if (arrow.GetComponentInParent<TargetUnit>().Unit is Character ch)
            ch.ChangeHairBundleTypeLeft();
    }

    private static void OnRightArrowClick(GameObject arrow)
    {
        if (arrow.GetComponentInParent<TargetUnit>().Unit is Character ch)
            ch.ChangeHairBundleTypeRight();
    }
}

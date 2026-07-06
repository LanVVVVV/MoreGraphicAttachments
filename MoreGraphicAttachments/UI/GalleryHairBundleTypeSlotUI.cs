using MBMScripts;
using MoreGraphicAttachments.Core;
using MoreGraphicAttachments.Features;
using MoreGraphicAttachments.Properties;
using MoreGraphicAttachments.UIComponents;
using SystemExtensionLib.Systems;
using SystemExtensionLib.Tools;
using UnityEngine;

namespace MoreGraphicAttachments.UI;

public static class GalleryHairBundleTypeSlotUI
{
    private static bool _isInjected = false;

    public static void InjectSlot()
    {
        if (_isInjected) return;

        var HairBundleTypeSlots = ExtendedGallerySlotSystem.RegisterFemaleGalleryChangeSlot(
            ModEntry.ModName, "HairBundle Type",
            AppLayout.Right, 
            () => Strings.Slot_HairBundleType,
            (arrowLeft) => OnLeftArrowClick(arrowLeft),
            (arrowRight) => OnRightArrowClick(arrowRight),
            out var typeValue);

        foreach (GameObject gameObject in typeValue.ToArray())
        {
            var referenceHairBundleType = gameObject.AddComponent<ReferenceHairBundleType>();
            BinderTextMeshProText component = gameObject.GetComponent<BinderTextMeshProText>();
            component.SetReferenceArray([referenceHairBundleType]);
        }

        ExtendedGallerySlotSystem.Insert(HairBundleTypeSlots, InsertPoint.Last);

        _isInjected = true;
    }

    private static void OnLeftArrowClick(GameObject arrow)
    {
        if (arrow.GetComponentInParent<TargetUnit>().Unit is Character ch)
            if (ch.IsSpeciesType())
                ch.ChangeHairBundleTypeLeftInAll();
            else ch.ChangeHairBundleTypeLeft();
    }

    private static void OnRightArrowClick(GameObject arrow)
    {
        if (arrow.GetComponentInParent<TargetUnit>().Unit is Character ch)
            if (ch.IsSpeciesType())
                ch.ChangeHairBundleTypeRightInAll();
            else ch.ChangeHairBundleTypeRight();
    }
}

using MBMScripts;
using MoreGraphicAttachments.Core;
using MoreGraphicAttachments.Features;
using MoreGraphicAttachments.Properties;
using MoreGraphicAttachments.UIComponents;
using SystemExtensionLib.Systems;
using SystemExtensionLib.Tools;
using UnityEngine;

namespace MoreGraphicAttachments.UI;

public static class GalleryClothesTypeSlotUI
{
    private static bool _isInjected = false;

    public static void InjectSlot()
    {
        if (_isInjected) return;

        var clothesTypeSlots = ExtendedGallerySlotSystem.RegisterFemaleGalleryChangeSlot(
            ModEntry.ModName, "Clothes Type",
            AppLayout.Left, 
            () => Strings.Slot_ClothesType,
            (arrowLeft) => OnLeftArrowClick(arrowLeft),
            (arrowRight) => OnRightArrowClick(arrowRight),
            out var typeValue);

        foreach (GameObject gameObject in typeValue.ToArray())
        {
            ReferenceClothes referenceClothes = gameObject.AddComponent<ReferenceClothes>();
            referenceClothes.DataType = ReferenceClothes.EDataType.Type;
            BinderTextMeshProText component = gameObject.GetComponent<BinderTextMeshProText>();
            component.SetReferenceArray([referenceClothes]);
        }

        ExtendedGallerySlotSystem.Insert(clothesTypeSlots, InsertPoint.Below, "Clothes");

        _isInjected = true;
    }

    private static void OnLeftArrowClick(GameObject arrow)
    {
        if (arrow.GetComponentInParent<TargetUnit>().Unit is Character ch)
            if (ch.IsSpeciesType())
                ch.ChangeClothesTypeLeftInAll();
            else ch.ChangeClothesTypeLeft();
    }

    private static void OnRightArrowClick(GameObject arrow)
    {
        if (arrow.GetComponentInParent<TargetUnit>().Unit is Character ch)
            if (ch.IsSpeciesType())
                ch.ChangeClothesTypeRightInAll();
            else ch.ChangeClothesTypeRight();
    }
}

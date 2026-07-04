using MoreGraphicAttachments.Core;
using MoreGraphicAttachments.Properties;
using MoreGraphicAttachments.UIComponents;
using SystemExtensionLib.Systems;

namespace MoreGraphicAttachments.UI;

public static class GalleryClothesColorSlotUI
{
    private static bool _isInjected = false;

    public static void InjectSlot()
    {
        if (_isInjected) return;

        var clothesColorSlots = ExtendedGallerySlotSystem.RegisterFemaleGalleryColorSlot(
            ModEntry.ModName, "Clothes Color",
            AppLayout.Color,
            () => Strings.Slot_ClothesColor,
            out var ColorPickers);


        foreach (var (clothesColorSlotObj, ColorPickerObj) in clothesColorSlots.PairWithPickers(ColorPickers))
        {
            var interaction = clothesColorSlotObj.AddComponent<InteractionClothesColor>();
            ColorPickerObj.onColorChange.AddListener(interaction.ChangeClothesColor);
        }

        ExtendedGallerySlotSystem.Insert(clothesColorSlots, InsertPoint.First);

        _isInjected = true;
    }
}
namespace MoreGraphicAttachments.UI;

internal static class UIRegister
{
    internal static void Initialize()
    {
        GalleryClothesColorSlotUI.InjectSlot();
        GalleryClothesTypeSlotUI.InjectSlot();
        ClothesColorSlotUI.InjectSlot();
        ClothesTypeSlotUI.InjectSlot();
    }
}
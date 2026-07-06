using MoreGraphicAttachments.Core;
using MoreGraphicAttachments.ExtensionData;
using MoreGraphicAttachments.ExtensionData.Data;
using SystemExtensionLib.Systems;

namespace MoreGraphicAttachments.UI;

internal static class UIRegister
{
    internal static void Initialize()
    {
        GalleryClothesColorSlotUI.InjectSlot();
        GalleryClothesTypeSlotUI.InjectSlot();
        GalleryHairBundleTypeSlotUI.InjectSlot();


        ClothesColorSlotUI.InjectSlot();
        
        ClothesTypeSlotUI.InjectSlot();
        ExtendedInfoSlotSystem.RegisterFemaleExtendedSlotVisibilityCondition(
            ModEntry.ModName, "Clothes Type",
            (character) => CharacterExtensionDataMap<ClothesTypeData>.Get(character).ClothesTypeList.Length > 1);

        HairBundleTypeSlotUI.InjectSlot();
    }
}
using MoreGraphicAttachments.ExtensionData.Data;

namespace MoreGraphicAttachments.ExtensionData;

public static class LoadData
{
    public static void Initialize()
    {
        CharacterExtensionDataMap<NoseTypeData>.Init();
        CharacterExtensionDataMap<ToothTypeData>.Init();
        CharacterExtensionDataMap<ClothesColorData>.Init();
        CharacterExtensionDataMap<ClothesTypeData>.Init();
        CharacterExtensionDataMap<HairBundleTypeData>.Init();

        ClothesTypeData.BuildGlobalClothesTypeList();
        HairBundleTypeData.BuildGlobalHairBundleTypeList();
    }
}
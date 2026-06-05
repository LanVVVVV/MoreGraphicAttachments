namespace MoreGraphicAttachments.ExtensionData;


public static class LoadData
{
    public static void Initialize()
    {
        CharacterExtensionDataMap<NoseTypeData>.Init();
        CharacterExtensionDataMap<ToothTypeData>.Init();
        CharacterExtensionDataMap<ClothColorData>.Init();
    }
}
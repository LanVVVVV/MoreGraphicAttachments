using HarmonyLib;
using MBMScripts;

namespace MoreGraphicAttachments.Features;

public static class ClothesTypeHelper
{
    public static int TransformClothesType(Character character)
    {
        int clothesType = character.RawClothesType();

        if (clothesType == 0)
            return 1;
        else if (clothesType == 1)
            return 0;
        else
            return -clothesType;
    }

    private static readonly AccessTools.FieldRef<Character, int> ClothesTypeRef =
    AccessTools.FieldRefAccess<Character, int>("m_ClothesType");

    public static int RawClothesType(this Character character)
    {
        return ClothesTypeRef(character);
    }
}
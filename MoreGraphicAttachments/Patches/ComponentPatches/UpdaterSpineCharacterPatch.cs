using HarmonyLib;
using MBMScripts;
using MoreGraphicAttachments.ExtensionData.Data;
using MoreGraphicAttachments.ExtensionField;
using MoreGraphicAttachments.ExtensionSpine;
using Spine;
using Spine.Unity;
using UnityEngine;

namespace MoreGraphicAttachments.Patches.ComponentPatches;

[HarmonyPatch(typeof(UpdaterSpineCharacter), "OnChangeSpine")]
public static class UpdaterSpineCharacterPatch
{
    public static void Postfix(UpdaterSpineCharacter __instance, Skeleton ___m_Skeleton)
    {
        if (__instance.Female == null)
            return;

        var extra = __instance.Female.Extra();
        Color clothColor = extra.ClothesColor;
        Color finalColor;

        //finalColor = ApplyBrightnessCompensation(clothColor);
        finalColor = CalculateCompensationColor(clothColor, ClothesColorData.BaseColor);

        var plc = Unit.GetPartListCollection(__instance.SpineData, __instance.Female, __instance.Male);

        foreach (string slotName in plc.Extra().ClothesColorSlotNameList)
        {
            ___m_Skeleton.FindSlot(slotName)?.SetColor(finalColor);
        }
    }

    //static public Color ApplyBrightnessCompensation(Color clothColor, float textureBrightness = 0.6f)
    //{
    //    if (clothColor == ClothesColorData.BaseColor)
    //    {
    //        return ClothesColorData.BaseColor;
    //    }
    //    if (textureBrightness < 0.01f)
    //        textureBrightness = 0.01f;

    //    Color.RGBToHSV(clothColor, out float h, out float s, out float v);

    //    v = Mathf.Clamp01(v / textureBrightness);

    //    return Color.HSVToRGB(h, s, v);
    //}

    public static Color CalculateCompensationColor(Color targetColor, Color originalBaseColor)
    {
        const float epsilon = 0.001f;

        float r = originalBaseColor.r > epsilon
            ? Mathf.Clamp(targetColor.r / originalBaseColor.r, 0f, 1f)
            : targetColor.r;

        float g = originalBaseColor.g > epsilon
            ? Mathf.Clamp(targetColor.g / originalBaseColor.g, 0f, 1f)
            : targetColor.g;

        float b = originalBaseColor.b > epsilon
            ? Mathf.Clamp(targetColor.b / originalBaseColor.b, 0f, 1f)
            : targetColor.b;

        float a = targetColor.a;

        return new Color(r, g, b, a);
    }
}

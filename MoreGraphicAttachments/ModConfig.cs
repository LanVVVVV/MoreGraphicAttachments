using MBM.ModLoader.Settings;
using MoreGraphicAttachments.Properties;
using System.Collections.Generic;

namespace MoreGraphicAttachments;

public static class ModConfig
{
    private const string NameClothesColorChanceMode = "Clothes Color Chance Mode";
    private const string NameCustomClothesColorChance = "Custom Clothes Color Chance";

    private static string[] ClothesColorChanceModeLabels => [Strings.Label_Default, Strings.Label_AlwaysColored, Strings.Label_Custom];
    public static int ClothesColorChanceModeIndex { get; set; }

    public static float CustomClothesColorChance { get; set; }

    public static void ModSettingRegister()
    {
        ClothesColorChanceModeRegister();

        CustomClothesColorChanceRegister();

        SetVisible();
    }

    private static void ClothesColorChanceModeRegister()
    {
        ModSettings.RegisterDropdown(ModEntry.ModName, NameClothesColorChanceMode, ClothesColorChanceModeLabels, 0, Strings.Config_ClothesColorChanceMode, "ClothesColor");
        ClothesColorChanceModeIndex = ModSettings.GetDropdown(ModEntry.ModName, NameClothesColorChanceMode);
        ModSettings.OnChanged(ModEntry.ModName, NameClothesColorChanceMode, v =>
        {
            ClothesColorChanceModeIndex = (int)v;
            ModEntry.Log($"{NameClothesColorChanceMode} = {ClothesColorChanceModeLabels[ClothesColorChanceModeIndex]}");
        });
    }

    private static void CustomClothesColorChanceRegister()
    {
        ModSettings.RegisterFloat(ModEntry.ModName, NameCustomClothesColorChance, 0.2f, Strings.Config_CustomClothesColorChance, "ClothesColor", "ClothesColorChance");
        CustomClothesColorChance = ModSettings.GetFloat(ModEntry.ModName, NameCustomClothesColorChance);
        ModSettings.OnChanged(ModEntry.ModName, NameCustomClothesColorChance, v =>
        {
            if (CustomClothesColorChance == (float)v) return;
            CustomClothesColorChance = (float)v;
            ModEntry.Log($"{NameCustomClothesColorChance} = {CustomClothesColorChance}");
        });
    }

    private static void SetVisible()
    {
        ModSettings.SetVisibleWhen(ModEntry.ModName, NameClothesColorChanceMode,
            new Dictionary<string, string[]>
            {
                { "2", new[] { "ClothesColorChance" } }
            });
    }

    public static void OnLanguageChanged()
    {
        ModSettings.RegisterDropdown(ModEntry.ModName, NameClothesColorChanceMode, ClothesColorChanceModeLabels, ClothesColorChanceModeIndex, Strings.Config_ClothesColorChanceMode, "ClothesColor");
        ModSettings.SetDescription(ModEntry.ModName, NameCustomClothesColorChance, Strings.Config_CustomClothesColorChance);
    }


}


using MBM.ModLoader.Settings;
using MoreGraphicAttachments.Properties;
using System.Collections.Generic;

namespace MoreGraphicAttachments;

internal static class ModConfig
{
    private static string[] ClothColorChanceModeLabels => [Strings.Label_Default, Strings.Label_AlwaysColored, Strings.Label_Custom];

    internal static void ModSettingRegister()
    {
        ModSettings.RegisterDropdown(ModEntry.ModName, "Cloth Color Chance Mode", ClothColorChanceModeLabels, 0, Strings.Config_ClothColorChanceMode, "ClothColor");
        ClothColorChanceModeIndex = ModSettings.GetDropdown(ModEntry.ModName, "Cloth Color Chance Mode");
        ModSettings.OnChanged(ModEntry.ModName, "Cloth Color Chance Mode", v =>
        {
            ClothColorChanceModeIndex = (int)v;
            ModEntry.Log($"Cloth Color Chance Mode = {ClothColorChanceModeLabels[ClothColorChanceModeIndex]}");
        });

        ModSettings.RegisterFloat(ModEntry.ModName, "Custom Cloth Color Chance", 0.2f, Strings.Config_CustomClothColorChance, "ClothColor", "ClothColorChance");
        CustomClothColorChance = ModSettings.GetFloat(ModEntry.ModName, "Custom Cloth Color Chance");
        ModSettings.OnChanged(ModEntry.ModName, "Custom Cloth Color Chance", v =>
        {
            if (CustomClothColorChance == (float)v) return;
            CustomClothColorChance = (float)v;
            ModEntry.Log($"Custom Cloth Color Chance = {CustomClothColorChance}");
        });

        ModSettings.SetVisibleWhen(ModEntry.ModName, "Cloth Color Chance Mode",
            new Dictionary<string, string[]>
            {
                { "2", new[] { "ClothColorChance" } }
            });
    }

    internal static void OnLanguageChanged()
    {
        ModSettings.RegisterDropdown(ModEntry.ModName, "Cloth Color Chance Mode", ClothColorChanceModeLabels, ClothColorChanceModeIndex, Strings.Config_ClothColorChanceMode, "ClothColor");
        ModSettings.SetDescription(ModEntry.ModName, "Custom Cloth Color Chance", Strings.Config_CustomClothColorChance);
    }

    public static int ClothColorChanceModeIndex { get; set; }

    public static float CustomClothColorChance { get; set; }
}


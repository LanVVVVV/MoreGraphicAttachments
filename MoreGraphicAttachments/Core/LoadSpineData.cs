using MBM.ModLoader.Core;
using MoreGraphicAttachments.ExtensionSpineData;

namespace MoreGraphicAttachments.Core;

public static class LoadSpineData
{
    public static bool Initialized = false;

    public static void Initialize()
    {
        if (Initialized) return;

        string[] jsonNames;

        if (Loader.IsModLoaded("TitsMod"))
        {
            ModEntry.Log("Detected: TitsMod enabled.");
            jsonNames = ["TitsModSpineDataWoman", "TitsModSpineDataGirl"];
        }
        else
        {
            jsonNames = ["SpineDataWoman", "SpineDataGirl"];
        }

        ModEntry.Log($"Preparing to load config file list: {string.Join(", ", jsonNames)}.");
        SpineDataExtraSystem.Init(jsonNames);

        Initialized = true;
    }
}
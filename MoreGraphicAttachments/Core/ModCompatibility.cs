using MBM.ModLoader.Core;
using System;

namespace MoreGraphicAttachments.Core;

internal static class ModCompatibility
{
    internal static bool IsTitsModLoaded = false;

    internal static void ModCompatibilityCheck()
    {
        CheckTitsModLoad();
        CheckTYPEtoNumberIDLoad();
    }

    private static void CheckTitsModLoad()
    {
        if (Loader.IsModLoaded("TitsMod"))
        {
            ModEntry.Log("Detected: TitsMod enabled.");
            IsTitsModLoaded = true;
        }
    }

    private static void CheckTYPEtoNumberIDLoad()
    {
        if (Loader.IsModLoaded("TYPEtoNumberID"))
        {
            string message = 
                "MoreGraphicAttachments has already integrated TYPEtoNumberID. " +
                "Do not use them together to prevent duplication or conflicts.";
            ModEntry.LogError(message);
        }
    }
}

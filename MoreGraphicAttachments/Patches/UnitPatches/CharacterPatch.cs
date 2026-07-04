using HarmonyLib;
using MBMScripts;
using MoreGraphicAttachments.Core;
using MoreGraphicAttachments.Features;

namespace MoreGraphicAttachments.Patches.UnitPatches;

[HarmonyPatch(typeof(Character))]
public static class CharacterPatch
{
    [HarmonyPatch(nameof(Character.ClothesType), MethodType.Getter)]
    [HarmonyPrefix]
    public static bool ClothesTypeGetterPrefix(Character __instance, ref int __result, int ___m_ClothesType)
    {
        return ClothesTypeHelper.ClothesTypeGetterPatch(__instance, out __result, ___m_ClothesType);
    }

    #region HairFront
    [HarmonyPatch(nameof(Character.ChangeHairFrontLeftInAll))]
    [HarmonyPrefix]
    public static bool ChangeHairFrontLeftInAllPrefix(Character __instance)
    {
        var list = GlobalCharacterData.AllPartTypes["HairFront"];
        int index = list.IndexOf(__instance.HairFrontType);

        index = (index - 1 + list.Count) % list.Count;

        __instance.HairFrontType = list[index];
        return false;
    }

    [HarmonyPatch(nameof(Character.ChangeHairFrontRightInAll))]
    [HarmonyPrefix]
    public static bool ChangeHairFrontRightInAllPrefix(Character __instance)
    {
        var list = GlobalCharacterData.AllPartTypes["HairFront"];
        int index = list.IndexOf(__instance.HairFrontType);

        index = (index + 1) % list.Count;

        __instance.HairFrontType = list[index];
        return false;
    }
    #endregion


    #region HairSide
    [HarmonyPatch(nameof(Character.ChangeHairSideLeftInAll))]
    [HarmonyPrefix]
    public static bool ChangeHairSideLeftInAllPrefix(Character __instance)
    {
        var list = GlobalCharacterData.AllPartTypes["HairSide"];
        int index = list.IndexOf(__instance.HairSideType);

        index = (index - 1 + list.Count) % list.Count;

        __instance.HairSideType = list[index];
        return false;
    }

    [HarmonyPatch(nameof(Character.ChangeHairSideRightInAll))]
    [HarmonyPrefix]
    public static bool ChangeHairSideRightInAllPrefix(Character __instance)
    {
        var list = GlobalCharacterData.AllPartTypes["HairSide"];
        int index = list.IndexOf(__instance.HairSideType);

        index = (index + 1) % list.Count;

        __instance.HairSideType = list[index];
        return false;
    }
    #endregion


    #region HairBack
    [HarmonyPatch(nameof(Character.ChangeHairBackLeftInAll))]
    [HarmonyPrefix]
    public static bool ChangeHairBackLeftInAllPrefix(Character __instance)
    {
        var list = GlobalCharacterData.AllPartTypes["HairBack"];
        int index = list.IndexOf(__instance.HairBackType);

        index = (index - 1 + list.Count) % list.Count;

        __instance.HairBackType = list[index];
        return false;
    }

    [HarmonyPatch(nameof(Character.ChangeHairBackRightInAll))]
    [HarmonyPrefix]
    public static bool ChangeHairBackRightInAllPrefix(Character __instance)
    {
        var list = GlobalCharacterData.AllPartTypes["HairBack"];
        int index = list.IndexOf(__instance.HairBackType);

        index = (index + 1) % list.Count;

        __instance.HairBackType = list[index];
        return false;
    }
    #endregion


    #region Eye (EFigure.Girl)
    [HarmonyPatch(nameof(Character.ChangeEyeLeftInAll))]
    [HarmonyPrefix]
    public static bool ChangeEyeLeftInAllPrefix(Character __instance)
    {
        var list = GlobalCharacterData.AllPartTypes["Eye"];
        int index = list.IndexOf(__instance.EyeType);

        index = (index - 1 + list.Count) % list.Count;

        __instance.EyeType = list[index];
        return false;
    }

    [HarmonyPatch(nameof(Character.ChangeEyeRightInAll))]
    [HarmonyPrefix]
    public static bool ChangeEyeRightInAllPrefix(Character __instance)
    {
        var list = GlobalCharacterData.AllPartTypes["Eye"];
        int index = list.IndexOf(__instance.EyeType);

        index = (index + 1) % list.Count;

        __instance.EyeType = list[index];
        return false;
    }
    #endregion

    #region EyeBall
    [HarmonyPatch(nameof(Character.ChangeEyeBallLeftInAll))]
    [HarmonyPrefix]
    public static bool ChangeEyeBallLeftInAllPrefix(Character __instance)
    {
        var list = GlobalCharacterData.AllPartTypes["EyeBall"];
        int index = list.IndexOf(__instance.EyeBallType);

        index = (index - 1 + list.Count) % list.Count;

        __instance.EyeBallType = list[index];
        return false;
    }

    [HarmonyPatch(nameof(Character.ChangeEyeBallRightInAll))]
    [HarmonyPrefix]
    public static bool ChangeEyeBallRightInAllPrefix(Character __instance)
    {
        var list = GlobalCharacterData.AllPartTypes["EyeBall"];
        int index = list.IndexOf(__instance.EyeBallType);

        index = (index + 1) % list.Count;

        __instance.EyeBallType = list[index];
        return false;
    }
    #endregion

    #region Tits
    [HarmonyPatch(nameof(Character.ChangeTitsLeftInAll))]
    [HarmonyPrefix]
    public static bool ChangeTitsLeftInAllPrefix(Character __instance)
    {
        if (ModCompatibility.IsTitsModLoaded)
            return false;

        var list = GlobalCharacterData.AllPartTypes["Tits"];
        int t = __instance.TitsType;

        if (t == 0)
        {
            __instance.TitsType = list[list.Count - 1];
            return false;
        }

        int index = list.IndexOf(t);
        index--;
        if (index < 0)
        {
            __instance.TitsType = 0;
            return false;
        }

        __instance.TitsType = list[index];
        return false;
    }

    [HarmonyPatch(nameof(Character.ChangeTitsRightInAll))]
    [HarmonyPrefix]
    public static bool ChangeTitsRightInAllPrefix(Character __instance)
    {
        if (ModCompatibility.IsTitsModLoaded)
            return false;

        var list = GlobalCharacterData.AllPartTypes["Tits"];
        int t = __instance.TitsType;

        if (t == 0)
        {
            __instance.TitsType = list[0];
            return false;
        }

        int index = list.IndexOf(t);
        index++;
        if (index >= list.Count)
        {
            __instance.TitsType = 0;
            return false;
        }

        __instance.TitsType = list[index];
        return false;
    }
    #endregion


    #region PubicHair
    [HarmonyPatch(nameof(Character.ChangePubicHairLeftInAll))]
    [HarmonyPrefix]
    public static bool ChangePubicHairLeftInAllPrefix(Character __instance)
    {
        var list = GlobalCharacterData.AllPartTypes["PubicHair"];
        int index = list.IndexOf(__instance.PubicHairType);
        index = (index - 1 + list.Count) % list.Count;
        __instance.PubicHairType = list[index];
        return false;
    }

    [HarmonyPatch(nameof(Character.ChangePubicHairRightInAll))]
    [HarmonyPrefix]
    public static bool ChangePubicHairRightInAllPrefix(Character __instance)
    {
        var list = GlobalCharacterData.AllPartTypes["PubicHair"];
        int index = list.IndexOf(__instance.PubicHairType);
        index = (index + 1) % list.Count;
        __instance.PubicHairType = list[index];
        return false;
    }
    #endregion

    #region HairBody (EFigure.Girl)
    [HarmonyPatch(nameof(Character.ChangeHairBodyLeftInAll))]
    [HarmonyPrefix]
    public static bool ChangeHairBodyLeftInAllPrefix(Character __instance)
    {
        var list = GlobalCharacterData.AllPartTypes["HairBody"];
        int index = list.IndexOf(__instance.HairBodyType);
        index = (index - 1 + list.Count) % list.Count;
        __instance.HairBodyType = list[index];
        return false;
    }

    [HarmonyPatch(nameof(Character.ChangeHairBodyRightInAll))]
    [HarmonyPrefix]
    public static bool ChangeHairBodyRightInAllPrefix(Character __instance)
    {
        var list = GlobalCharacterData.AllPartTypes["HairBody"];
        int index = list.IndexOf(__instance.HairBodyType);
        index = (index + 1) % list.Count;
        __instance.HairBodyType = list[index];
        return false;
    }
    #endregion

    #region Horn (EFigure.Girl) (Double List)
    [HarmonyPatch(nameof(Character.ChangeHornLeftInAll))]
    [HarmonyPrefix]
    public static bool ChangeHornLeftInAllPrefix(Character __instance)
    {
        var horn = GlobalCharacterData.AllPartTypes["Horn"];
        var dragon = GlobalCharacterData.AllPartTypes["HornDragon"];

        int hornType = __instance.HornType;
        int hornDragonType = __instance.HornDragonType;

        if (hornType == 0 && hornDragonType == 0)
        {
            __instance.HornDragonType = dragon[dragon.Count - 1];
            return false;
        }

        if (hornDragonType > 0)
        {
            int index = dragon.IndexOf(hornDragonType);
            index--;
            if (index < 0)
            {
                __instance.HornDragonType = 0;
                __instance.HornType = horn[horn.Count - 1];
                return false;
            }

            __instance.HornDragonType = dragon[index];
            return false;
        }

        if (hornType > 0)
        {
            int index = horn.IndexOf(hornType);
            index--;
            if (index < 0)
            {
                __instance.HornType = 0;
                __instance.HornDragonType = 0;
                return false;
            }

            __instance.HornType = horn[index];
            return false;
        }

        return false;
    }

    [HarmonyPatch(nameof(Character.ChangeHornRightInAll))]
    [HarmonyPrefix]
    public static bool ChangeHornRightInAllPrefix(Character __instance)
    {
        var horn = GlobalCharacterData.AllPartTypes["Horn"];
        var dragon = GlobalCharacterData.AllPartTypes["HornDragon"];

        int hornType = __instance.HornType;
        int hornDragonType = __instance.HornDragonType;

        if (hornType == 0 && hornDragonType == 0)
        {
            __instance.HornType = horn[0];
            return false;
        }

        if (hornType > 0)
        {
            int index = horn.IndexOf(hornType);
            index++;
            if (index >= horn.Count)
            {
                __instance.HornType = 0;
                __instance.HornDragonType = dragon[0];
                return false;
            }

            __instance.HornType = horn[index];
            return false;
        }

        if (hornDragonType > 0)
        {
            int index = dragon.IndexOf(hornDragonType);
            index++;
            if (index >= dragon.Count)
            {
                __instance.HornDragonType = 0;
                __instance.HornType = 0;
                return false;
            }

            __instance.HornDragonType = dragon[index];
            return false;
        }

        return false;
    }
    #endregion

    #region Ear

    [HarmonyPatch(nameof(Character.ChangeEarLeftInAll))]
    [HarmonyPrefix]
    public static bool ChangeEarLeftInAllPrefix(Character __instance)
    {
        var list = GlobalCharacterData.AllPartTypes["Ear"];
        int index = list.IndexOf(__instance.EarType);
        index = (index - 1 + list.Count) % list.Count;
        __instance.EarType = list[index];
        return false;
    }

    [HarmonyPatch(nameof(Character.ChangeEarRightInAll))]
    [HarmonyPrefix]
    public static bool ChangeEarRightInAllPrefix(Character __instance)
    {
        var list = GlobalCharacterData.AllPartTypes["Ear"];
        int index = list.IndexOf(__instance.EarType);
        index = (index + 1) % list.Count;
        __instance.EarType = list[index];
        return false;
    }

    #endregion

    #region EarHair (EFigure.Girl)

    [HarmonyPatch(nameof(Character.ChangeEarHairLeftInAll))]
    [HarmonyPrefix]
    public static bool ChangeEarHairLeftInAllPrefix(Character __instance)
    {
        var list = GlobalCharacterData.AllPartTypes["EarHair"];
        int index = list.IndexOf(__instance.EarHairType);
        index = (index - 1 + list.Count) % list.Count;
        __instance.EarHairType = list[index];
        return false;
    }

    [HarmonyPatch(nameof(Character.ChangeEarHairRightInAll))]
    [HarmonyPrefix]
    public static bool ChangeEarHairRightInAllPrefix(Character __instance)
    {
        var list = GlobalCharacterData.AllPartTypes["EarHair"];
        int index = list.IndexOf(__instance.EarHairType);
        index = (index + 1) % list.Count;
        __instance.EarHairType = list[index];
        return false;
    }
    #endregion

    #region Tail (Double List)
    [HarmonyPatch(nameof(Character.ChangeTailLeftInAll))]
    [HarmonyPrefix]
    public static bool ChangeTailLeftInAllPrefix(Character __instance)
    {
        var tail = GlobalCharacterData.AllPartTypes["Tail"];
        var dragon = GlobalCharacterData.AllPartTypes["TailDragon"];

        int tailType = __instance.TailType;
        int tailDragonType = __instance.TailDragonType;

        if (tailType == 0 && tailDragonType == 0)
        {
            __instance.TailDragonType = dragon[dragon.Count - 1];
            return false;
        }

        if (tailDragonType > 0)
        {
            int index = dragon.IndexOf(tailDragonType);
            index--;
            if (index < 0)
            {
                __instance.TailDragonType = 0;
                __instance.TailType = tail[tail.Count - 1];
                return false;
            }

            __instance.TailDragonType = dragon[index];
            return false;
        }

        if (tailType > 0)
        {
            int index = tail.IndexOf(tailType);
            index--;
            if (index < 0)
            {
                __instance.TailType = 0;
                __instance.TailDragonType = 0;
                return false;
            }

            __instance.TailType = tail[index];
            return false;
        }

        return false;
    }

    [HarmonyPatch(nameof(Character.ChangeTailRightInAll))]
    [HarmonyPrefix]
    public static bool ChangeTailRightInAllPrefix(Character __instance)
    {
        var tail = GlobalCharacterData.AllPartTypes["Tail"];
        var dragon = GlobalCharacterData.AllPartTypes["TailDragon"];

        int tailType = __instance.TailType;
        int tailDragonType = __instance.TailDragonType;

        if (tailType == 0 && tailDragonType == 0)
        {
            __instance.TailType = tail[0];
            return false;
        }

        if (tailType > 0)
        {
            int index = tail.IndexOf(tailType);
            index++;
            if (index >= tail.Count)
            {
                __instance.TailType = 0;
                __instance.TailDragonType = dragon[0];
                return false;
            }

            __instance.TailType = tail[index];
            return false;
        }

        if (tailDragonType > 0)
        {
            int index = dragon.IndexOf(tailDragonType);
            index++;
            if (index >= dragon.Count)
            {
                __instance.TailDragonType = 0;
                __instance.TailType = 0;
                return false;
            }

            __instance.TailDragonType = dragon[index];
            return false;
        }

        return false;
    }
    #endregion
}
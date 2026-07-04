using HarmonyLib;
using MBMScripts;
using MoreGraphicAttachments.Features;

namespace MoreGraphicAttachments.Patches.ComponentPatches;

[HarmonyPatch(typeof(ReferenceCharacterLook))]
public static class ReferenceCharacterLookPatch
{
    private const int EyeType = 16;
    private const int BodyType = 18;
    private const int HairFrontType = 22;
    private const int HairSideType = 24;
    private const int HairBackType = 25;
    private const int PubicHairType = 27;
    private const int SkinColorType = 29;
    private const int HornDragonType = 31;
    private const int HairBodyType = 33;
    private const int EyeBallType = 41;
    private const int EarType = 45;
    private const int EarHairType = 46;
    private const int TailType = 48;
    private const int HornType = 55;
    private const int BeardType = 57;
    private const int ClothesEDataType = 72;

    [HarmonyPatch(nameof(ReferenceCharacterLook.GetString))]
    [HarmonyPrefix]
    public static bool GetStringPrefix(ReferenceCharacterLook __instance, ref string __result, int ___m_DataType)
    {
        if(__instance.Updater.TargetUnit?.Unit is not Character character)
        {
            __result = string.Empty;
            return false;
        }

        string text;
        switch(___m_DataType)
        {
            case EyeType:
                __result = character.EyeType.ToString();
                return false;

            case BodyType:
                __result = character.LowerBodyType.ToString();
                return false;

            case HairFrontType:
                try
                {
                    text = character.HairFrontType.ToString();
                } catch
                {
                    text = string.Empty;
                }
                break;

            case HairSideType:
                if(character.HairSideType == 0)
                {
                    __result = SeqLocalization.Localize("#None");
                    return false;
                }
                try
                {
                    text = character.HairSideType.ToString();
                } catch
                {
                    text = string.Empty;
                }
                break;

            case HairBackType:
                __result = character.HairBackType.ToString();
                return false;

            case PubicHairType:
                if(character.PubicHairType == 0)
                {
                    __result = SeqLocalization.Localize("#None");
                    return false;
                }
                try
                {
                    text = character.PubicHairType.ToString();
                } catch
                {
                    text = string.Empty;
                }
                break;

            case SkinColorType:
                if(character.SkinColorType == 100)
                {
                    __result = "O";
                    return false;
                }
                __result = (character.SkinColorType + 1).ToString();
                return false;

            case HornDragonType:
                if(character.HornType == 0 && character.HornDragonType == 0)
                {
                    __result = SeqLocalization.Localize("#None");
                    return false;
                }
                if(character.HornType > 0)
                {
                    try
                    {
                        text = character.HornType.ToString();
                    } catch
                    {
                        text = string.Empty;
                    }
                    break;
                }
                try
                {
                    var list = GlobalCharacterData.AllPartTypes["Horn"];
                    text = (character.HornDragonType + list[list.Count - 1]).ToString();
                } catch
                {
                    text = string.Empty;
                }
                break;

            case HairBodyType:
                if(character.HairBodyType == 0)
                {
                    __result = SeqLocalization.Localize("#None");
                    return false;
                }
                try
                {
                    text = character.HairBodyType.ToString();
                } catch
                {
                    text = string.Empty;
                }
                break;

            case EyeBallType:
                __result = character.RawEyeBallType.ToString();
                return false;

            case EarType:
                try
                {
                    text = character.EarType.ToString();
                } catch
                {
                    text = string.Empty;
                }
                break;

            case EarHairType:
                if(character.EarHairType == 0)
                {
                    __result = SeqLocalization.Localize("#None");
                    return false;
                }
                try
                {
                    text = character.EarHairType.ToString();
                } catch
                {
                    text = string.Empty;
                }
                break;

            case TailType:
                if(character.TailType == 0 && character.TailDragonType == 0)
                {
                    __result = SeqLocalization.Localize("#None");
                    return false;
                }
                if(character.TailType > 0)
                {
                    try
                    {
                        text = character.TailType.ToString();
                    } catch
                    {
                        text = string.Empty;
                    }
                    break;
                }
                try
                {
                    var list = GlobalCharacterData.AllPartTypes["Tail"];
                    text = (character.TailDragonType + list[list.Count - 1]).ToString();
                } catch
                {
                    text = string.Empty;
                }
                break;

            case HornType:
                if(character.HornType == 0)
                {
                    __result = SeqLocalization.Localize("#None");
                    return false;
                }
                try
                {
                    text = character.HornType.ToString();
                } catch
                {
                    text = string.Empty;
                }
                break;

            case BeardType:
                if(character.BeardType == 0)
                {
                    __result = SeqLocalization.Localize("#None");
                    return false;
                }
                try
                {
                    text = character.BeardType.ToString();
                } catch
                {
                    text = string.Empty;
                }
                break;

            default:
                return true;
        }

        __result = string.Format(SeqLocalization.Localize("#TypeFormat"), text);
        return false;
    }

    [HarmonyPatch(nameof(ReferenceCharacterLook.GetBool))]
    [HarmonyPrefix]
    static bool GetBoolPrefix(ReferenceCharacterLook __instance, ref bool __result, int ___m_DataType)
    {
        if(__instance.Updater.TargetUnit?.Unit is not Character character)
        {
            __result = false;
            return false;
        }

        if(___m_DataType == ClothesEDataType)
        {
            __result = character.ClothesType != 0;
            return false;
        }
        return true;
    }
}
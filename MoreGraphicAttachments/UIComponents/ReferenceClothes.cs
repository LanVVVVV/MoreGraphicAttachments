using MBMScripts;
using MoreGraphicAttachments.ExtensionData;
using MoreGraphicAttachments.ExtensionField;
using MoreGraphicAttachments.Features;
using UnityEngine;

namespace MoreGraphicAttachments.UIComponents;

public class ReferenceClothes : Reference
{
    [SerializeField]
    private EDataType m_DataType;

    public EDataType DataType
    {
        get
        {
            return this.m_DataType;
        }
        set
        {
            this.m_DataType = value;
        }
    }

    public override void Initialize()
    {
        switch (m_DataType)
        {
            case EDataType.ColorText:
                ReferenceType = EReferenceType.String;
                break;
            case EDataType.Color:
                ReferenceType = EReferenceType.Color;
                break;
            case EDataType.Type:
                ReferenceType = EReferenceType.String;
                break;
        }
    }

    public override Color GetColor()
    {
        TargetUnit targetUnit = base.Updater.TargetUnit;
        Character? character = (targetUnit?.Unit) as Character;
        if (character == null) return ClothesColorData.BaseColor;

        return character.Extra().ClothesColor;
    }

    public override string GetString()
    {
        TargetUnit targetUnit = base.Updater.TargetUnit;
        Character? character = (targetUnit?.Unit) as Character;
        if (character == null) return string.Empty;

        switch (m_DataType)
        {
            case EDataType.ColorText:
                return SeqLocalization.Localize(GetColorText(character.Extra().ClothesColor));
            case EDataType.Type:
                return string.Format(SeqLocalization.Localize("#TypeFormat"), character.GetClothesType().ToString());
            default: return string.Empty;
        }
    }

    public static string GetColorText(Color color)
    {
        Color.RGBToHSV(color, out var H, out var S, out var V);
        if (V < 0.1f)
        {
            return "#Color_Black";
        }

        if (S < 0.17f)
        {
            return "#Color_Gray";
        }

        if (H < 0.0305555f)
        {
            return "#Color_Red";
        }

        if (H < 0.125f)
        {
            if (V > 0.75f)
            {
                return "#Color_Orange";
            }

            return "#Color_Brown";
        }

        if (H < 0.1777777f)
        {
            return "#Color_Yellow";
        }

        if (H < 0.4166666f)
        {
            return "#Color_Green";
        }

        if (H < 0.5f)
        {
            return "#Color_Cyan";
        }

        if (H < 17f / 24f)
        {
            return "#Color_Blue";
        }

        if (H < 31f / 36f)
        {
            return "#Color_Purple";
        }

        if (H < 17f / 18f)
        {
            return "#Color_Pink";
        }

        if (H < 0.975f)
        {
            if (V > 0.7f)
            {
                return "#Color_Red";
            }

            return "#Color_Pink";
        }

        return "#Color_Red";
    }

    public enum EDataType
    {
        ColorText,
        Color,
        Type
    }
}
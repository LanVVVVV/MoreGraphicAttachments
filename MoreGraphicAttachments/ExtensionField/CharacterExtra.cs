using MBMScripts;
using MoreGraphicAttachments.ExtensionData.Data;
using UnityEngine;

namespace MoreGraphicAttachments.ExtensionField;

public class CharacterExtra
{
    private readonly Character m_Character;

    private Color m_ClothesColor = ClothesColorData.BaseColor;

    private int m_HairBundleType = 0;

    public Color ClothesColor
    {
        get
        {
            SeqDataBinding.Instance.RegisterFlag(m_Character);
            return m_ClothesColor;
        }
        set
        {
            if (!(m_ClothesColor == value))
            {
                SeqDataBinding.Instance.DirtyFlag(m_Character);
                m_ClothesColor = value;
                Unit.RemovePartData(m_Character);
            }
        }
    }

    public int HairBundleType
    {
        get
        {
            SeqDataBinding.Instance.RegisterFlag(m_Character);
            return m_HairBundleType;
        }
        set
        {
            if (!(m_HairBundleType == value))
            {
                SeqDataBinding.Instance.DirtyFlag(m_Character);
                m_HairBundleType = value;
                Unit.RemovePartData(m_Character);
            }
        }
    }

    public CharacterExtra(Character character)
    {
        m_Character = character;
    }

    public CharacterExtra(Character character, CharacterExtraSaveData data)
    {
        m_Character = character;

        if (!string.IsNullOrEmpty(data.ClothesColorHex) &&
            ColorUtility.TryParseHtmlString(data.ClothesColorHex, out var col))
        {
            m_ClothesColor = col;
        }
        else
        {
            m_ClothesColor = ClothesColorData.BaseColor;
        }

        HairBundleType = data.HairBundleType;
    }

    public bool NeedSave()
    {
        return ClothesColorNeedSave() || HairBundleTypeNeedSave();
    }

    public bool ClothesColorNeedSave()
    {
        return ClothesColor != ClothesColorData.BaseColor;
    }

    public bool HairBundleTypeNeedSave()
    {
        return HairBundleType != 0;
    }
}
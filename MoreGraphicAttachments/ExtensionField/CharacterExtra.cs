using MBMScripts;
using MoreGraphicAttachments.ExtensionData.Data;
using System;
using UnityEngine;

namespace MoreGraphicAttachments.ExtensionField;

[Serializable]
public class CharacterExtra
{
    private readonly Character m_Character;

    private Color m_ClothesColor = ClothesColorData.BaseColor;

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

    public CharacterExtra(Character character)
    {
        m_Character = character;
    }

    public CharacterExtra(Character character, CharacterExtraSaveData data)
    {
        m_Character = character;

        if (ColorUtility.TryParseHtmlString(data.ClothesColorHex, out var col))
        {
            m_ClothesColor = col;
        }
        else
        {
            m_ClothesColor = ClothesColorData.BaseColor;
        }
    }

    public bool NotNeedSave()
    {
        return ClothesColorNotNeedSave();
    }

    public bool ClothesColorNotNeedSave()
    {
        return ClothesColor == ClothesColorData.BaseColor;
    }
}
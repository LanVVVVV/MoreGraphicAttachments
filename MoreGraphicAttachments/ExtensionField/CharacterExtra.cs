using MBMScripts;
using MoreGraphicAttachments.ExtensionData;
using System;
using UnityEngine;

namespace MoreGraphicAttachments.ExtensionField;

[Serializable]
public class CharacterExtra
{
    public Character m_Character;

    public Color m_ClothColor = ClothColorData.BaseColor;

    public Color ClothColor
    {
        get
        {
            SeqDataBinding.Instance.RegisterFlag(m_Character);
            return m_ClothColor;
        }
        set
        {
            if (!(m_ClothColor == value))
            {
                SeqDataBinding.Instance.DirtyFlag(m_Character);
                m_ClothColor = value;
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

        if (ColorUtility.TryParseHtmlString(data.ClothColorHex, out var col))
        {
            m_ClothColor = col;
        }
        else
        {
            m_ClothColor = ClothColorData.BaseColor;
        }
    }

    public bool NotNeedSave()
    {
        return ClothColorNotNeedSave();
    }

    private bool ClothColorNotNeedSave()
    {
        return ClothColor == ClothColorData.BaseColor;
    }
}
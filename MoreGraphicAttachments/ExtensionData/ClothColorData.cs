using MBMScripts;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace MoreGraphicAttachments.ExtensionData;

[Serializable]
public class ClothColorData : ExtensionCharacterData
{
    public string m_CharacterClass = null!;

    public List<string> m_ClothColorStringList = null!;

    public float[] m_ClothColorStringChanceList = null!;

    // ---
    public List<Color> m_ClothColorList = null!;

    private static int ClothColorChanceModeIndex => ModConfig.ClothColorChanceModeIndex;

    private static float CustomClothColorChance => ModConfig.CustomClothColorChance;

    public static Color BaseColor => new Color(195f / 255f, 175f / 255f, 164f / 255f); //"#C3AFA4"

    public List<Color> ClothColorList
    {
        get
        {
            if(this.m_ClothColorList != null)
            {
                return this.m_ClothColorList;
            }
            this.m_ClothColorList = new List<Color>(this.m_ClothColorStringList.Count);
            for(int i = 0; i < this.m_ClothColorStringList.Count; i++)
            {
                Color color;
                this.m_ClothColorList
                    .Add(ColorUtility.TryParseHtmlString(this.m_ClothColorStringList[i], out color) ? color : BaseColor);
            }
            return this.m_ClothColorList;
        }
    }

    public Color GetInitialClothColor(int defaultIndex)
    {
        if(ClothColorChanceModeIndex == 1)
            return this.GetClothRandomColor();

        if (ClothColorChanceModeIndex == 2)
        {
            if (UnityEngine.Random.value < CustomClothColorChance)
            {
                return this.GetClothRandomColor();
            }
            else
            {
                return BaseColor;
            }
        }
            

        if (this.m_ClothColorStringList == null || this.m_ClothColorStringList.Count == 0)
        {
            return BaseColor;
        }
        if(this.m_ClothColorStringChanceList == null)
        {
            if(!(this.m_ClothColorStringList[defaultIndex] == "RANDOM"))
            {
                return this.ClothColorList[Mathf.Clamp(defaultIndex, 0, this.ClothColorList.Count - 1)];
            }
            return this.GetClothRandomColor();
        } else
        {
            int indexOfChance = SeqUtil.GetIndexOfChance(this.m_ClothColorStringChanceList);
            if(!(this.m_ClothColorStringList[indexOfChance] == "RANDOM"))
            {
                return this.ClothColorList[indexOfChance];
            }
            return this.GetClothRandomColor();
        }
    }

    public Color GetClothRandomColor()
    {
        float h = UnityEngine.Random.value;
        float s = UnityEngine.Random.Range(0.65f, 0.9f);
        float v = UnityEngine.Random.Range(0.55f, 0.8f);
        return Color.HSVToRGB(h, s, v);
    }
}

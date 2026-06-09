using MBMScripts;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace MoreGraphicAttachments.ExtensionData;

[Serializable]
public class ClothesColorData : ExtensionCharacterData
{
    public string m_CharacterClass = null!;

    public List<string> m_ClothesColorStringList = null!;

    public float[] m_ClothesColorStringChanceList = null!;

    // ---
    public List<Color> m_ClothesColorList = null!;

    private static int ClothesColorChanceModeIndex => ModConfig.ClothesColorChanceModeIndex;

    private static float CustomClothesColorChance => ModConfig.CustomClothesColorChance;

    public static Color BaseColor => new Color(195f / 255f, 175f / 255f, 164f / 255f); //"#C3AFA4"

    public List<Color> ClothesColorList
    {
        get
        {
            if(this.m_ClothesColorList != null)
            {
                return this.m_ClothesColorList;
            }
            this.m_ClothesColorList = new List<Color>(this.m_ClothesColorStringList.Count);
            for(int i = 0; i < this.m_ClothesColorStringList.Count; i++)
            {
                Color color;
                this.m_ClothesColorList
                    .Add(ColorUtility.TryParseHtmlString(this.m_ClothesColorStringList[i], out color) ? color : BaseColor);
            }
            return this.m_ClothesColorList;
        }
    }

    public Color GetInitialClothesColor(int defaultIndex)
    {
        if(ClothesColorChanceModeIndex == 1)
            return this.GetClothesRandomColor();

        if (ClothesColorChanceModeIndex == 2)
        {
            if (UnityEngine.Random.value < CustomClothesColorChance)
            {
                return this.GetClothesRandomColor();
            }
            else
            {
                return BaseColor;
            }
        }
            

        if (this.m_ClothesColorStringList == null || this.m_ClothesColorStringList.Count == 0)
        {
            return BaseColor;
        }
        if(this.m_ClothesColorStringChanceList == null)
        {
            if(!(this.m_ClothesColorStringList[defaultIndex] == "RANDOM"))
            {
                return this.ClothesColorList[Mathf.Clamp(defaultIndex, 0, this.ClothesColorList.Count - 1)];
            }
            return this.GetClothesRandomColor();
        } else
        {
            int indexOfChance = SeqUtil.GetIndexOfChance(this.m_ClothesColorStringChanceList);
            if(!(this.m_ClothesColorStringList[indexOfChance] == "RANDOM"))
            {
                return this.ClothesColorList[indexOfChance];
            }
            return this.GetClothesRandomColor();
        }
    }

    public Color GetClothesRandomColor()
    {
        float h = UnityEngine.Random.value;
        float s = UnityEngine.Random.Range(0.65f, 0.9f);
        float v = UnityEngine.Random.Range(0.55f, 0.8f);
        return Color.HSVToRGB(h, s, v);
    }
}

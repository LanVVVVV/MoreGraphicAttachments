using MBM.ModLoader.Core;
using MBMScripts;
using MoreGraphicAttachments.Properties;
using MoreGraphicAttachments.UIComponents;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MoreGraphicAttachments.UI;

public class GalleryClothesColorSlotUI
{
    private static List<ReferenceFormattingText> LabelRfyList { get; set; } = [];

    private static bool _isInjected = false;

    public static void OnLanguageChanged()
    {
        foreach (var labelRfy in LabelRfyList)
        {
            labelRfy.Value = Strings.Slot_ClothesColor;
        }
    }

    public static void InjectSlot()
    {
        if (_isInjected) return;

        var slaveLayout = GameObject.Find("Galley/Canvas/LetterBox/Frame/Slave Customize/Layout").transform;
        var color0 = slaveLayout.Find("Color");
        var color1 = slaveLayout.Find("Slave2/Color (1)");

        foreach (var color in new List<Transform> { color0, color1 })
        {
            var colorContent = color.transform.Find("Content/");
            var referenceSlot = colorContent.GetChild(0);

            var clothColorSlot = Object.Instantiate(referenceSlot.gameObject, colorContent);
            clothColorSlot.name = "ClothesColor";
            clothColorSlot.transform.SetSiblingIndex(0);

            #region Increase the height of the parent container
            float slotHeight = 0f;
            var slotLE = referenceSlot.GetComponent<LayoutElement>();
            if (slotLE != null && slotLE.preferredHeight > 0)
                slotHeight = slotLE.preferredHeight;
            else
                slotHeight = ((RectTransform)referenceSlot).rect.height;

            if (slotHeight > 0)
            {

                var parentVLG = colorContent.GetComponent<VerticalLayoutGroup>();
                if (parentVLG != null)
                    slotHeight += parentVLG.spacing;

                Transform t = colorContent;
                for (int depth = 0; t != null && depth < 4; depth++, t = t.parent)
                {
                    var rt = t as RectTransform ?? t.GetComponent<RectTransform>();
                    if (rt != null && rt.sizeDelta.y > 0)
                    {
                        rt.sizeDelta = new Vector2(rt.sizeDelta.x, rt.sizeDelta.y + slotHeight);
                    }
                }

                LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)colorContent);
            }
            #endregion

            #region Label
            var labelRfy = clothColorSlot.GetComponentInChildren<ReferenceFormattingText>(true);
            labelRfy.Value = Strings.Slot_ClothesColor;
            LabelRfyList.Add(labelRfy);
            #endregion

            foreach (var mb in clothColorSlot.GetComponentsInChildren<MonoBehaviour>(true))
            {
                if (mb is ColorPickerInitialization || mb is InteractionCustomize)
                    Object.DestroyImmediate(mb);
            }

            clothColorSlot.AddComponent<ClothesColorPickerInitialization>();

            var flexibleColorPicker = clothColorSlot.GetComponent<FlexibleColorPicker>();
            var interaction = clothColorSlot.AddComponent<InteractionClothesColor>();

            flexibleColorPicker.onColorChange.RemoveAllListeners();
            flexibleColorPicker.onColorChange.AddListener(interaction.ChangeClothesColor);

            if (UIExtraction.ClothesColorPicker == null)
            {
                UIExtraction.ClothesColorPicker = clothColorSlot;
            }
            
            _isInjected = true;
        }
    }
}

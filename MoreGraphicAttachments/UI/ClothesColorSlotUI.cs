using MBMScripts;
using MoreGraphicAttachments.Patches;
using MoreGraphicAttachments.Properties;
using MoreGraphicAttachments.UIComponents;
using System.Collections.Generic;
using UnityEngine;

namespace MoreGraphicAttachments.UI;

public class ClothesColorSlotUI
{
    private static List<ReferenceString> LabelRsList { get; set; } = [];

    private static List<ReferenceFormattingText> LabelRftList { get; set; } = [];

    private static GameObject ColorPickerPanel { get; set; } = null!;

    public static void OnLanguageChanged()
    {
        foreach (var labelRf in LabelRsList)
        {
            labelRf.Value = Strings.Slot_ClothesColor;
        }
        foreach (var labelRs in LabelRftList)
        {
            labelRs.Value = Strings.Slot_ClothesColor;
        }
    }

    public static void InjectSlot()
    {
        var canvas = GameObject.Find("Window Female Information (Window)/Canvas");
        canvas.SetActive(false);

        var contentLeft = canvas.transform.Find("LetterBox/Frame/Window (1)/Content/Upper Left").transform;
        var referenceSlot = contentLeft.Find("Eye Ball Color");

        var clothColorSlot = Object.Instantiate(referenceSlot.gameObject, contentLeft);
        clothColorSlot.name = "Clothes Color";
        clothColorSlot.transform.SetSiblingIndex(referenceSlot.GetSiblingIndex() + 1);

        #region Label
        var labelRs = clothColorSlot.GetComponentInChildren<ReferenceString>(true);
        labelRs.Value = Strings.Slot_ClothesColor;
        LabelRsList.Add(labelRs);
        #endregion

        #region Value
        GameObject obj = null!;
        foreach (var mb in clothColorSlot.GetComponentsInChildren<MonoBehaviour>(true))
        {
            if (mb is ReferenceCharacterLook reference)
            {
                obj = reference.gameObject;
                Object.DestroyImmediate(mb);
            }
        }

        var referenceClothesColor = obj.AddComponent<ReferenceClothesColor>();
        referenceClothesColor.DataType = ReferenceClothesColor.EDataType.Color;
        var referenceClothesColorText = obj.AddComponent<ReferenceClothesColor>();
        referenceClothesColorText.DataType = ReferenceClothesColor.EDataType.ColorText;
        ComponentTools.
                SetReferenceArray(obj?.GetComponent<BinderTextMeshPro>(), [referenceClothesColor!]);
        ComponentTools.SetReferenceArray(obj?.GetComponent<BinderTextMeshProText>(), [referenceClothesColorText!]);
        #endregion

        ColorPickerPanel = AddClothesColorPanel(clothColorSlot);

        AddClothesColorButton(clothColorSlot);

        PlayDataPatch.AfterSaveDataInitialized += CloseColorPickerPanel;
    }
    private static GameObject AddClothesColorPanel(GameObject clothColorSlot)
    {
        var colorpanel = UIExtraction.Panel!.transform;
        colorpanel.SetParent(clothColorSlot.transform, false);

        var panelRT = colorpanel.GetComponent<RectTransform>();
        panelRT.anchorMin = new Vector2(0, 1);
        panelRT.anchorMax = new Vector2(0, 1);
        panelRT.pivot = new Vector2(1, 0.5f);
        panelRT.anchoredPosition = new Vector2(-13, 0);
        panelRT.sizeDelta = new Vector2(300, 250);

        var label = colorpanel.Find("Label").gameObject;
        var labelRft = label.GetComponentInChildren<ReferenceFormattingText>(true);
        var tmp = label.GetComponentInChildren<TMPro.TextMeshProUGUI>(true);
        tmp.fontSizeMax = 24f;
        tmp.enableAutoSizing = true;
        labelRft.Value = Strings.Slot_ClothesColor;
        LabelRftList.Add(labelRft);

        var exitIcon = colorpanel.Find("Exit Button").gameObject;
        ComponentTools.AddClickEvent(exitIcon, CloseColorPickerPanel);

        // === ColorPicker ===
        var body = colorpanel.Find("Body");
        var colorPicker = UIExtraction.ClothesColorPicker!.transform;
        colorPicker.SetParent(body.transform, false);

        colorPicker.gameObject.SetActive(true);

        return colorpanel.gameObject;
    }
    private static GameObject AddClothesColorButton(GameObject clothColorSlot)
    {
        var colorModifyButton = UIExtraction.ColorModifyButton!;
        colorModifyButton.transform.SetParent(clothColorSlot.transform, false);

        var rt = colorModifyButton.GetComponent<RectTransform>();
        rt.anchorMin = new Vector2(0.5f, 0.5f);
        rt.anchorMax = new Vector2(0.5f, 0.5f);
        rt.pivot = new Vector2(0.5f, 0.5f);
        rt.sizeDelta = new Vector2(28, 28);
        rt.anchoredPosition = new Vector2(60, -10);
        ComponentTools.AddClickEvent(colorModifyButton, ToggleColorPickerPanel);

        colorModifyButton.SetActive(true);

        return colorModifyButton;
    }

    private static void ToggleColorPickerPanel()
    {
        bool isActive = ColorPickerPanel.activeSelf;
        ColorPickerPanel.SetActive(!isActive);
    }

    private static void CloseColorPickerPanel()
    {
        ColorPickerPanel.SetActive(false);
    }
}

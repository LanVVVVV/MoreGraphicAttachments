using MBMScripts;
using MoreGraphicAttachments.Patches.InitializePatches;
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

    private static bool _isInjected = false;

    private static bool _isGlobalEventRegistered = false;

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
        if (_isInjected) return;

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
        BinderTextMeshPro binderText = null!;
        BinderTextMeshProText binderTextText = null!;
        foreach (var mb in clothColorSlot.GetComponentsInChildren<MonoBehaviour>(true))
        {
            if (mb is ReferenceCharacterLook reference)
            {
                obj = reference.gameObject;
                binderText = obj.GetComponent<BinderTextMeshPro>();
                binderTextText = obj.GetComponent<BinderTextMeshProText>();
                Object.DestroyImmediate(mb);
            }
        }

        if (obj != null)
        {
            var referenceClothesColor = obj.AddComponent<ReferenceClothesColor>();
            referenceClothesColor.DataType = ReferenceClothesColor.EDataType.Color;

            var referenceClothesColorText = obj.AddComponent<ReferenceClothesColor>();
            referenceClothesColorText.DataType = ReferenceClothesColor.EDataType.ColorText;

            if (binderText != null) ComponentTools.SetReferenceArray(binderText, [referenceClothesColor]);
            if (binderTextText != null) ComponentTools.SetReferenceArray(binderTextText, [referenceClothesColorText]);
        }
        #endregion

        ColorPickerPanel = AddClothesColorPanel(clothColorSlot);

        AddClothesColorButton(clothColorSlot, ColorPickerPanel);

        if (!_isGlobalEventRegistered)
        {
            PlayDataPatch.AfterSaveDataInitialized += CloseColorPickerPanel;
            _isGlobalEventRegistered = true;
        }

        _isInjected = true;
    }

    private static GameObject AddClothesColorPanel(GameObject clothColorSlot)
    {
        var colorpanel = Object.Instantiate(UIExtraction.Panel, clothColorSlot.transform);
        colorpanel!.name = "ClothesColorPanel";

        var panelRT = colorpanel.GetComponent<RectTransform>();
        panelRT.anchorMin = new Vector2(0, 1);
        panelRT.anchorMax = new Vector2(0, 1);
        panelRT.pivot = new Vector2(1, 0.5f);
        panelRT.anchoredPosition = new Vector2(-13, 0);
        panelRT.sizeDelta = new Vector2(300, 250);

        var label = colorpanel.transform.Find("Label").gameObject;
        var labelRft = label.GetComponentInChildren<ReferenceFormattingText>(true);
        var tmp = label.GetComponentInChildren<TMPro.TextMeshProUGUI>(true);
        tmp.fontSizeMax = 24f;
        tmp.enableAutoSizing = true;
        labelRft.Value = Strings.Slot_ClothesColor;
        LabelRftList.Add(labelRft);

        var exitIcon = colorpanel.transform.Find("Exit Button").gameObject;
        ComponentTools.AddClickEvent(exitIcon, () => colorpanel.SetActive(false));

        // === ColorPicker ===
        var body = colorpanel.transform.Find("Body");

        var colorPicker = Object.Instantiate(UIExtraction.ClothesColorPicker, body);
        colorPicker!.name = "ClothesColorPickerInstance";

        var flexibleColorPicker = colorPicker.GetComponent<FlexibleColorPicker>();
        var interaction = colorPicker.GetComponent<InteractionClothesColor>();
        flexibleColorPicker.onColorChange.RemoveAllListeners();
        flexibleColorPicker.onColorChange.AddListener(interaction.ChangeClothesColor);

        colorPicker.SetActive(true);

        return colorpanel;
    }

    private static GameObject AddClothesColorButton(GameObject clothColorSlot, GameObject targetPanel)
    {
        var colorModifyButton = Object.Instantiate(UIExtraction.ColorModifyButton, clothColorSlot.transform);
        colorModifyButton!.name = "ColorModifyButton";

        colorModifyButton.transform.SetParent(clothColorSlot.transform, false);

        var rt = colorModifyButton.GetComponent<RectTransform>();
        rt.anchorMin = new Vector2(0.5f, 0.5f);
        rt.anchorMax = new Vector2(0.5f, 0.5f);
        rt.pivot = new Vector2(0.5f, 0.5f);
        rt.sizeDelta = new Vector2(28, 28);
        rt.anchoredPosition = new Vector2(60, -10);
        ComponentTools.AddClickEvent(colorModifyButton, () => targetPanel.SetActive(!targetPanel.activeSelf));

        colorModifyButton.SetActive(true);

        return colorModifyButton;
    }

    private static void CloseColorPickerPanel()
    {
        ColorPickerPanel.SetActive(false);
    }
}

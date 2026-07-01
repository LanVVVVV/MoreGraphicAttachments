using MBMScripts;
using MoreGraphicAttachments.Core;
using MoreGraphicAttachments.Patches.InitializePatches;
using MoreGraphicAttachments.Properties;
using MoreGraphicAttachments.UIComponents;
using System.Collections.Generic;
using SystemExtensionLib.Systems;
using SystemExtensionLib.Tools;
using UnityEngine;

namespace MoreGraphicAttachments.UI;

public class ClothesColorSlotUI
{
    private static List<ReferenceFormattingText> LabelRftList { get; set; } = [];

    private static GameObject ColorPickerPanel { get; set; } = null!;

    private static bool _isInjected = false;

    private static bool _isGlobalEventRegistered = false;

    public static void OnLanguageChanged()
    {
        foreach (var labelRs in LabelRftList)
        {
            labelRs.Value = Strings.Slot_ClothesColor;
        }
    }

    public static void InjectSlot()
    {
        if (_isInjected) return;

        var clothColorSlot = ExtendedInfoSlotSystem.RegisterFemaleExtendedColorSlot(
            ModEntry.ModName, "Clothes Color",
            () => Strings.Slot_ClothesColor,
            out var typeValue);

        #region TypeValue
        var binder = typeValue!.GetComponent<BinderTextMeshPro>();
        var referenceClothesColor = typeValue.AddComponent<ReferenceClothes>();
        referenceClothesColor.DataType = ReferenceClothes.EDataType.Color;
        ComponentTools.SetReferenceArray(binder, [referenceClothesColor]);

        var binderText = typeValue.GetComponent<BinderTextMeshProText>();
        var referenceClothesColorText = typeValue.AddComponent<ReferenceClothes>();
        referenceClothesColorText.DataType = ReferenceClothes.EDataType.ColorText;
        ComponentTools.SetReferenceArray(binderText, [referenceClothesColorText]);

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

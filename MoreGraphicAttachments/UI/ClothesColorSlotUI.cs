using MBMScripts;
using MoreGraphicAttachments.Core;
using MoreGraphicAttachments.Patches.InitializePatches;
using MoreGraphicAttachments.Properties;
using MoreGraphicAttachments.Sprites;
using MoreGraphicAttachments.UIComponents;
using SystemExtensionLib.Systems;
using SystemExtensionLib.Tools;
using SystemExtensionLib.Utils;
using TMPro;
using UnityEngine;

namespace MoreGraphicAttachments.UI;

public static class ClothesColorSlotUI
{
    private static GameObject ColorPickerPanel { get; set; } = null!;

    private static bool _isInjected = false;

    private static bool _isGlobalEventRegistered = false;

    public static void InjectSlot()
    {
        if (_isInjected) return;

        var clothColorSlot = ExtendedInfoSlotSystem.RegisterFemaleExtendedColorSlot(
            ModEntry.ModName, "Clothes Color",
            () => Strings.Slot_ClothesColor,
            out var typeValue);

        #region TypeValue
        var referenceClothesColor = typeValue!.AddComponent<ReferenceClothes>();
        referenceClothesColor.DataType = ReferenceClothes.EDataType.Color;

        var binder = typeValue.GetComponent<BinderTextMeshPro>();
        binder.SetReferenceArray([referenceClothesColor]);

        var referenceClothesColorText = typeValue.AddComponent<ReferenceClothes>();
        referenceClothesColorText.DataType = ReferenceClothes.EDataType.ColorText;

        var binderText = typeValue.GetComponent<BinderTextMeshProText>();
        binderText.SetReferenceArray([referenceClothesColorText]);
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
        var colorpanel = UIExtraction.ExtractionPanelWindow(
            out GameObject body,
            out TextMeshProUGUI tmp,
            out ReferenceFormattingText labelRft,
            out GameObject exitIcon);
        colorpanel.transform.SetParent(clothColorSlot.transform, false);
        colorpanel.name = "ClothesColorPanel";

        var panelRT = colorpanel.GetComponent<RectTransform>();
        panelRT.pivot = new Vector2(1, 0.5f);
        panelRT.anchorMin = new Vector2(0, 1);
        panelRT.anchorMax = new Vector2(0, 1);
        panelRT.anchoredPosition = new Vector2(-13, 0);
        panelRT.sizeDelta = new Vector2(300, 250);

        AddClothesColorPicker(body);

        labelRft.SetLabel(() => Strings.Slot_ClothesColor);

        ComponentTools.AddClickEvent(exitIcon, () => colorpanel.SetActive(false));

        return colorpanel;
    }

    private static void AddClothesColorPicker(GameObject body)
    {
        var colorPicker = GalleryExtraction.OnlyColorPicker(out var flexibleColorPicker);
        colorPicker.transform.SetParent(body.transform, false);
        colorPicker.name = "ClothesColorPicker";

        var referenceClothesColor = colorPicker.AddComponent<ReferenceClothes>();
        referenceClothesColor.DataType = ReferenceClothes.EDataType.Color;
        var updaterColorPicker = colorPicker.AddComponent<UpdaterColorPicker>();
        updaterColorPicker.SetReferenceArray([referenceClothesColor]);

        var interaction = colorPicker.AddComponent<InteractionClothesColor>();
        flexibleColorPicker.onColorChange.AddListener(interaction.ChangeClothesColor);

        colorPicker.SetActive(true);
    }

    private static GameObject AddClothesColorButton(GameObject clothColorSlot, GameObject targetPanel)
    {
        var colorModifyButton = UIExtraction.ExtractionButton(out var image);
        colorModifyButton.transform.SetParent(clothColorSlot.transform, false);
        colorModifyButton!.name = "ColorModifyButton";

        var rt = colorModifyButton.GetComponent<RectTransform>();
        rt.anchoredPosition = new Vector2(60, -10);

        image.sprite = UISpriteLoad.SpriteButtonColorModify;

        ComponentTools.AddClickEvent(colorModifyButton, () => targetPanel.SetActive(!targetPanel.activeSelf));

        colorModifyButton.SetActive(true);
        return colorModifyButton;
    }

    private static void CloseColorPickerPanel()
    {
        ColorPickerPanel.SetActive(false);
    }
}

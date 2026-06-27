using MBMScripts;
using MoreGraphicAttachments.Sprites;
using MoreGraphicAttachments.UIComponents;
using UnityEngine;
using UnityEngine.UI;

namespace MoreGraphicAttachments.UI;

public class UIExtraction
{
    // After GalleryClothesColorSlotUI
    public static GameObject? ClothesColorPicker { get; set; }

    public static GameObject? ColorModifyButton { get; set; }

    public static GameObject? Panel { get; set; }

    public static void Initialize() 
    {
        GalleryClothesColorSlotUI.InjectSlot();
        AllForClothesColorSlotUI();
        ClothesColorSlotUI.InjectSlot();

        Clear();
    }

    public static void Clear()
    {
        GameObject.DestroyImmediate(ClothesColorPicker);
        GameObject.DestroyImmediate(ColorModifyButton);
        GameObject.DestroyImmediate(Panel);
    }

    public static void AllForClothesColorSlotUI()
    {
        ExtractionClothesColorPicker();
        ExtractionColorModifyButton();
        ExtractionPanelWindow();
    }

    private static void ExtractionClothesColorPicker()
    {
        var colorPickerSource = ClothesColorPicker;
        colorPickerSource!.SetActive(false);
        var colorPicker = GameObject.Instantiate(colorPickerSource!);
        colorPickerSource.SetActive(true);

        colorPicker.name = "Color Picker";
        var colorPickerRT = colorPicker.GetComponent<RectTransform>();
        colorPickerRT.anchorMin = Vector2.zero;
        colorPickerRT.anchorMax = Vector2.one;
        colorPickerRT.pivot = Vector2.up;
        colorPickerRT.anchoredPosition = Vector2.zero;
        colorPickerRT.offsetMin = Vector2.zero;
        colorPickerRT.offsetMax = Vector2.zero;
        GameObject.DestroyImmediate(colorPicker.GetComponent<Image>());
        GameObject.DestroyImmediate(colorPickerRT.Find("Text (TMP)").gameObject);
        GameObject.DestroyImmediate(colorPicker.GetComponent<ClothesColorPickerInitialization>());

        var referenceClothesColor = colorPicker.AddComponent<ReferenceClothesColor>();
        referenceClothesColor.DataType = ReferenceClothesColor.EDataType.Color;

        colorPicker.AddComponent<UpdaterColorPicker>();
        ComponentTools.SetReferenceArray(colorPicker.GetComponent<UpdaterColorPicker>(), [referenceClothesColor!]);

        var flexibleColorPicker = colorPicker.GetComponent<FlexibleColorPicker>();
        var interaction = colorPicker.GetComponent<InteractionClothesColor>();
        flexibleColorPicker.onColorChange.RemoveAllListeners();
        flexibleColorPicker.onColorChange.AddListener(interaction.ChangeClothesColor);

        ClothesColorPicker = colorPicker;
    }

    private static void ExtractionColorModifyButton()
    {
        var renameButton = GameObject.Find("Window Female Information (Window)/Canvas/LetterBox/Frame/Window (0)/Favorite/Rename");
        var colorModifyButton = GameObject.Instantiate(renameButton);

        colorModifyButton.name = "ColorModify";
        var image = colorModifyButton.GetComponent<Image>();
        image.sprite = UISpriteLoad.SpriteButtonColorModify;
        GameObject.DestroyImmediate(colorModifyButton.GetComponent<InteractionWindow>());
        ComponentTools.RemoveClickEvent(colorModifyButton);

        colorModifyButton.SetActive(false);

        ColorModifyButton = colorModifyButton;
    }

    private static void ExtractionPanelWindow()
    {
        var canvas = GameObject.Find("Unit List (Window)/Canvas");
        canvas.SetActive(false);

        var root = canvas.transform.Find("LetterBox/Frame/Window/Background").transform;

        // === Panel ===
        GameObject panel = new GameObject("ColorSelectPanel", typeof(RectTransform));
        var panelRT = panel.GetComponent<RectTransform>();

        panelRT.sizeDelta = new Vector2(400,300);
        panel.SetActive(false);

        // === Body ===
        var rootbody = root.Find("Background (Translucent)").gameObject;
        var body = GameObject.Instantiate(rootbody, panelRT);
        body.name = "Body";
        var bodyRT = body.GetComponent<RectTransform>();
        bodyRT.offsetMax = new Vector2(-6, -16);

        // === Border ===
        var rootBorder = root.Find("Border").gameObject;
        var border = GameObject.Instantiate(rootBorder, panelRT);
        border.name = "Border";

        // === Label ===
        var rootLabel = root.Find("Label").gameObject;
        var label = GameObject.Instantiate(rootLabel, panelRT);
        label.name = "Label";
        var labelRfy = label.GetComponentInChildren<ReferenceFormattingText>(true);
        labelRfy.Value = "CustomLabel";

        // === Exit Button ===
        var rootExitIcon = root.Find("Exit Button").gameObject;
        var exitIcon = GameObject.Instantiate(rootExitIcon, panelRT);
        exitIcon.name = "Exit Button";
        GameObject.DestroyImmediate(exitIcon.GetComponent<InteractionClickEvent>());
        GameObject.DestroyImmediate(exitIcon.GetComponent<InteractionWindow>());
        ComponentTools.RemoveClickEvent(exitIcon);

        Panel = panel;
    }
}
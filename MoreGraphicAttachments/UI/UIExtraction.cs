using MBMScripts;
using MoreGraphicAttachments.Sprites;
using MoreGraphicAttachments.UIComponents;
using UnityEngine;
using UnityEngine.UI;

namespace MoreGraphicAttachments.UI;

public class UIExtraction
{
    // After GalleryClothColorSlotUI
    public static GameObject? ClothColorPicker { get; set; }

    public static GameObject? ColorModifyButton { get; set; }

    public static GameObject? Panel { get; set; }

    public static void AllForClothColorSlotUI()
    {
        ExtractionClothColorPicker();
        ExtractionColorModifyButton();
        ExtractionPanelWindow();
    }

    private static void ExtractionClothColorPicker()
    {
        ClothColorPicker!.SetActive(false);
        var colorPicker = GameObject.Instantiate(ClothColorPicker!);
        ClothColorPicker.SetActive(true);
        colorPicker.name = "Color Picker";
        var colorPickerRT = colorPicker.GetComponent<RectTransform>();
        colorPickerRT.anchorMin = new Vector2(0, 0);
        colorPickerRT.anchorMax = new Vector2(1, 1);
        colorPickerRT.pivot = new Vector2(0, 1);
        colorPickerRT.anchoredPosition = new Vector2(0, 0);
        colorPickerRT.offsetMax = new Vector2(0, 0);
        colorPickerRT.offsetMin = new Vector2(0, 0);
        UnityEngine.Object.Destroy(colorPicker.GetComponent<Image>());
        GameObject.Destroy(colorPickerRT.Find("Text (TMP)").gameObject);

        var flexibleColorPicker = colorPicker.GetComponent<FlexibleColorPicker>();
        var interaction = colorPicker.GetComponent<InteractionClothColor>();
        flexibleColorPicker.onColorChange.RemoveAllListeners();
        flexibleColorPicker.onColorChange.AddListener(interaction.ChangeClothColor);

        UnityEngine.Object.Destroy(colorPicker.GetComponent<ClothColorPickerInitialization>());
        var referenceClothColor = colorPicker.AddComponent<ReferenceClothColor>();
        referenceClothColor.DataType = ReferenceClothColor.EDataType.Color;
        colorPicker.AddComponent<UpdaterColorPicker>();
        ComponentTools.SetReferenceArray(colorPicker?.GetComponent<UpdaterColorPicker>(), [referenceClothColor!]);

        ClothColorPicker = colorPicker;
    }

    private static void ExtractionColorModifyButton()
    {
        var renameButton = GameObject.Find("Window Female Information (Window)/Canvas/LetterBox/Frame/Window (0)/Favorite/Rename");
        var colorModifyButton = GameObject.Instantiate(renameButton);

        colorModifyButton.name = "ColorModify";
        var image = colorModifyButton.GetComponent<Image>();
        image.sprite = UISpriteLoad.SpriteButtonColorModify;
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
        ComponentTools.RemoveClickEvent(exitIcon);

        Panel = panel;
    }
}
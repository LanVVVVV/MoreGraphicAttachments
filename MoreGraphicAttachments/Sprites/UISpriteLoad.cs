using System.IO;
using System.Reflection;
using UnityEngine;

namespace MoreGraphicAttachments.Sprites;

public static class UISpriteLoad
{
    private static readonly Assembly assembly = Assembly.GetExecutingAssembly();

    public static Sprite? SpriteButtonColorModify { get; set; }

    public static void LoadSprite()
    {
        SpriteButtonColorModify = GetEmbeddedSprite("button_colorModify.png");
    }

    public static Sprite? GetEmbeddedSprite(string embeddedResourceName)
    {
        using (Stream stream = assembly.GetManifestResourceStream(embeddedResourceName))
        {
            if (stream == null) 
            {
                ModEntry.LogError($"Embedded resource not found: {embeddedResourceName}");
                return null;
            }

            byte[] imageData = new byte[stream.Length];
            stream.Read(imageData, 0, imageData.Length);

            Texture2D texture = new Texture2D(1, 1);
            if (texture.LoadImage(imageData))
            {
                return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100f);
            }
            else
            {
                ModEntry.LogError($"Failed to parse embedded resource: {embeddedResourceName}");
                return null;
            }
        }
    }
}

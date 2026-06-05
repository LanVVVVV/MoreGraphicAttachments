using System.IO;
using System.Linq;
using UnityEngine;

namespace MoreGraphicAttachments;

internal static class JsonTool
{
    public static TextAsset? LoadEmbeddedJson(string name)
    {
        var asm = typeof(ModEntry).Assembly;
        var resourceName = asm.GetManifestResourceNames().FirstOrDefault(n => n.EndsWith("." + name + ".json"));

        if (resourceName == null)
        {
            ModEntry.LogWarning($"[EmbeddedJsonLoader] Resource not found: {resourceName}");
            return null;
        }
        ModEntry.Log($"[EmbeddedJsonLoader] Resource found: {resourceName}");

        using (var stream = asm.GetManifestResourceStream(resourceName))
        using (var reader = new StreamReader(stream))
        {
            string json = reader.ReadToEnd();

            //Debug.Log($"[DEBUG JSON CONTENT] {json}");

            return new TextAsset(json);
        }
    }
}
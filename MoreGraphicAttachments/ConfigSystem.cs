using System;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace MoreGraphicAttachments;

public static class ConfigSystem
{
    private static readonly string RootDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config");

    public static void Initialize()
    {
        if (!Directory.Exists(RootDir))
            Directory.CreateDirectory(RootDir);
    }

    public static void ExportEmbeddedFile(string modName, string fileName)
    {
        string modDir = EnsureModDir(modName);
        string filePath = Path.Combine(modDir, fileName);

        var asm = Assembly.GetCallingAssembly();
        var resourceName = asm.GetManifestResourceNames()
            .FirstOrDefault(n => n.EndsWith("." + fileName, StringComparison.Ordinal));

        if (resourceName == null)
        {
            Debug.LogWarning($"[ConfigSystem] Embedded resource not found: {fileName}");
            return;
        }

        if (!File.Exists(filePath))
        {
            using var stream = asm.GetManifestResourceStream(resourceName);
            using var reader = new StreamReader(stream);
            File.WriteAllText(filePath, reader.ReadToEnd());

            string relativeDir = Path.Combine("Config", modName);
            Debug.Log($"[ConfigSystem] Exported embedded {fileName} to {relativeDir}");
        }
        else
        {
            string relativePath = Path.Combine("Config", modName, fileName);
            Debug.Log($"[ConfigSystem] External file already exists: {relativePath}, skipped export.");
        }
    }

    public static void ExportAllEmbeddedFile(string modName, string? extensionFilter = null)
    {
        string modDir = EnsureModDir(modName);

        var asm = Assembly.GetCallingAssembly();
        var resources = asm.GetManifestResourceNames();

        if (!string.IsNullOrEmpty(extensionFilter))
            resources = resources.Where(n => n.EndsWith(extensionFilter, StringComparison.Ordinal)).ToArray();

        foreach (var res in resources)
        {
            string[] parts = res.Split('.');
            string fileName = string.Join(".", parts.Skip(parts.Length - 2));

            string filePath = Path.Combine(modDir, fileName);

            if (!File.Exists(filePath))
            {
                using var stream = asm.GetManifestResourceStream(res);
                using var reader = new StreamReader(stream);
                File.WriteAllText(filePath, reader.ReadToEnd());

                string relativeDir = Path.Combine("Config", modName);
                Debug.Log($"[ConfigSystem] Exported embedded {fileName} to {relativeDir}");
            }
            else
            {
                string relativePath = Path.Combine("Config", modName, fileName);
                Debug.Log($"[ConfigSystem] External file already exists: {relativePath}, skipped export.");
            }
        }
    }

    public static TextAsset? LoadExternalFile(string modName, string fileName)
    {
        string modDir = EnsureModDir(modName);
        string filePath = Path.Combine(modDir, fileName);

        if (File.Exists(filePath))
        {
            string content = File.ReadAllText(filePath);
            return new TextAsset(content);
        }

        Debug.LogWarning($"[ConfigSystem] External file not found: {fileName}");
        return null;
    }

    public static TextAsset? LoadEmbeddedFile(string fileName)
    {
        var asm = Assembly.GetCallingAssembly();
        var resourceName = asm.GetManifestResourceNames()
            .FirstOrDefault(n => n.EndsWith("." + fileName, StringComparison.Ordinal));

        if (resourceName == null)
        {
            Debug.LogWarning($"[ConfigSystem] Embedded resource not found: {fileName}");
            return null;
        }

        using var stream = asm.GetManifestResourceStream(resourceName);
        using var reader = new StreamReader(stream);
        return new TextAsset(reader.ReadToEnd());
    }

    private static string EnsureModDir(string modName)
    {
        if (!Directory.Exists(RootDir))
            Directory.CreateDirectory(RootDir);

        string modDir = Path.Combine(RootDir, modName);
        if (!Directory.Exists(modDir))
            Directory.CreateDirectory(modDir);

        return modDir;
    }
}

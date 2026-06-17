using MBMScripts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MoreGraphicAttachments.ExtensionData;

public static class CharacterExtensionDataMap<T> where T : ExtensionCharacterData, new()
{
    private static Dictionary<Type, T>? _map = null!;

    private static bool initialized = false;

    private static readonly T Default = new T();

    public static void Init()
    {
        if(initialized)
            return;

        TextAsset? jsonAsset = ConfigSystem.LoadExternalFile(ModEntry.ModName, typeof(T).Name + ".json");

        if (jsonAsset == null)
        {
            ModEntry.LogWarning($"[ExtensionDataMap<{typeof(T).Name}>] No JSON found. Skipping.");
            initialized = true;
            return;
        }

        _map = new Dictionary<Type, T>();

        Wrapper<T>? wrapper = null;

        try
        {
            wrapper = JsonConvert.DeserializeObject<Wrapper<T>>(jsonAsset.text);
        } catch(Exception ex)
        {
            ModEntry.LogError($"[ExtensionDataMap<{typeof(T).Name}>] JSON parse error: {ex}");
        }

        if(wrapper == null || wrapper.list == null)
        {
            ModEntry.LogWarning($"[ExtensionDataMap<{typeof(T).Name}>] JSON is null.");
            initialized = true;
            return;
        }

        if(wrapper.list.Count == 0)
        {
            ModEntry.LogWarning($"[ExtensionDataMap<{typeof(T).Name}>] JSON found but empty.");
            initialized = true;
            return;
        }

        foreach(var data in wrapper.list)
        {
            string className = (string)data.GetType().GetField("m_CharacterClass").GetValue(data);

            var CharacterType = AppDomain.CurrentDomain
                .GetAssemblies()
                .Select(a => a.GetType(className))
                .FirstOrDefault(t => t != null);

            if(CharacterType != null)
            {
                _map[CharacterType] = data;
                //ModEntry.Log($"[ExtensionDataMap<{typeof(T).Name}>] Loaded for {CharacterType.Name} → {FormatData(data)}");
            } 
            else
            {
                ModEntry.LogWarning($"[ExtensionDataMap<{typeof(T).Name}>] CharacterClass \"{className}\" not found!");
            }
        }

        initialized = true;
        ModEntry.Log($"[ExtensionDataMap<{typeof(T).Name}>] Initialized with {_map.Count} entries.");
    }
    private static string FormatData(T data)
    {
        var fields = data.GetType().GetFields();
        List<string> parts = new();

        foreach (var f in fields)
        {
            var value = f.GetValue(data);

            // Null
            if (value == null)
            {
                parts.Add($"    {f.Name}: null");
                continue;
            }

            // Array
            if (value is Array arr)
            {
                parts.Add($"    {f.Name}: [{string.Join(", ", arr.Cast<object>())}]");
                continue;
            }

            // List<T>
            if (value is System.Collections.IEnumerable enumerable && !(value is string))
            {
                var listItems = new List<string>();
                foreach (var item in enumerable)
                    listItems.Add(item?.ToString() ?? "null");

                parts.Add($"    {f.Name}: [{string.Join(", ", listItems)}]");
                continue;
            }

            // Other types
            parts.Add($"    {f.Name}: {value}");
        }

        return "{\n" + string.Join("\n", parts) + "\n}";
    }

    public static T Get(Character character)
    {
        if(!initialized)
        {
            ModEntry.LogError($"ExtensionDataMap<{typeof(T).Name}> used before Init()!");
            return Default;
        }

        if(character == null)
            return Default;

        if(_map!.TryGetValue(character.GetType(), out var extensionData))
            return extensionData;

        return Default;
    }
}
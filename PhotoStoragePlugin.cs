using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using System;

namespace PhotoStorage
{
    [BepInPlugin(GUID, Name, Version)]
    internal class PhotoStoragePlugin : BaseUnityPlugin
    {
        public static int MaxPhotos => _maxPhotos.Value;
        private static ConfigEntry<int> _maxPhotos;
        private const string GUID = "com.LazyDuchess.BRC.PhotoStorage";
        private const string Name = "PhotoStorage";
        private const string Version = "1.0.0";
        private void Awake()
        {
            try
            {
                Configure();
                var harmony = new Harmony(GUID);
                harmony.PatchAll();
                Logger.LogInfo($"Plugin {Name} {Version} is loaded!");
            }
            catch(Exception e)
            {
                Logger.LogInfo($"Plugin {Name} {Version} failed to load! {e}");
            }
        }

        private void Configure()
        {
            _maxPhotos = Config.Bind("General",
                "MaxPhotos",
                1000,
                "Maximum amount of photos."
                );
        }
    }
}

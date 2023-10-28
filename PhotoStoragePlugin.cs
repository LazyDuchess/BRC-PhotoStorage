using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using System;

namespace PhotoStorage
{
    [BepInPlugin(GUID, Name, Version)]
    internal class PhotoStoragePlugin : BaseUnityPlugin
    {
        public static bool DisableCloud => _disableCloud.Value;
        public static int MaxPhotos => _maxPhotos.Value;
        private static ConfigEntry<int> _maxPhotos;
        private static ConfigEntry<bool> _disableCloud;
        private const string GUID = "com.LazyDuchess.BRC.PhotoStorage";
        private const string Name = "PhotoStorage";
        private const string Version = "1.1.0";
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
            _disableCloud = Config.Bind("General",
                "DisableCloud",
                false,
                "Disables Cloud saving/loading. Game data will be saved to and loaded from AppData/LocalLow/Team Reptile/Bomb Rush Cyberfunk, under the subfolders /SaveData/ and /Photos/ for the phone camera photos. This can help circumvent disk space errors that happen due to the low Steam Cloud storage size the game has."
                );

            _maxPhotos = Config.Bind("General",
                "MaxPhotos",
                1000,
                "Maximum amount of photos."
                );
        }
    }
}

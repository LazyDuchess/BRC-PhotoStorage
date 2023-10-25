using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using Reptile;

namespace PhotoStorage.Patches
{
    [HarmonyPatch(typeof(PhotosAlbumMenu))]
    internal static class PhotosAlbumMenuPatch
    {
        [HarmonyPrefix]
        [HarmonyPatch(nameof(PhotosAlbumMenu.RefreshTotalPhotoCountDisplayText))]
        private static bool RefreshTotalPhotoCountDisplayText_Prefix(PhotosAlbumMenu __instance, int updatedPhotoCount)
        {
            __instance.photoCountDisplayText.text = string.Format("{0}/{1}", updatedPhotoCount, PhotoStoragePlugin.MaxPhotos);
            return false;
        }
    }
}

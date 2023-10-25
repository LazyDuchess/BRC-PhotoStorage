using Reptile;
using HarmonyLib;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Reflection;

namespace PhotoStorage.Patches
{
    [HarmonyPatch(typeof(PhotosManager))]
    internal static class PhotosManagerPatch
    {
        [HarmonyPrefix]
        [HarmonyPatch(nameof(PhotosManager.IsPhotoStorageFull))]
        private static bool IsPhotoStorageFull_Prefix(ref bool __result, PhotosManager __instance)
        {
            __result = __instance.availablePhotosCount >= PhotoStoragePlugin.MaxPhotos;
            return false;
        }

        // Fixing this via prefix/postfix wasn't really pretty as I would have had to basically override the whole function just to change 1 line of code.
        // So instead I'm using a transpiler to replace the opcode that loads the 50 max photos with our custom photo amount.

        private static MethodInfo MaxPhotosGet = AccessTools.PropertyGetter(typeof(PhotoStoragePlugin), nameof(PhotoStoragePlugin.MaxPhotos));

        [HarmonyTranspiler]
        [HarmonyPatch(nameof(PhotosManager.SavePhoto))]
        private static IEnumerable<CodeInstruction> SavePhoto_Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            foreach (var instruction in instructions)
            {
                if (instruction.LoadsConstant(50))
                {
                    yield return new CodeInstruction(OpCodes.Call, MaxPhotosGet);
                }
                else
                    yield return instruction;
            }
        }
    }
}

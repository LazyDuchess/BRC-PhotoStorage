using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using Reptile;

namespace PhotoStorage.Patches
{
    [HarmonyPatch(typeof(AFileTransaction))]
    internal static class AFileTransactionPatch
    {
        [HarmonyPrefix]
        [HarmonyPatch(nameof(AFileTransaction.IsLocal), MethodType.Getter)]
        private static bool IsLocal_Prefix(ref bool __result)
        {
            if (PhotoStoragePlugin.DisableCloud)
            {
                __result = true;
                return false;
            }
            return true;
        }
    }
}

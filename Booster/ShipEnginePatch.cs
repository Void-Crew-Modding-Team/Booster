using Gameplay.Ship;
using HarmonyLib;
using UnityEngine;

namespace Booster
{
    [HarmonyPatch(typeof(ShipEngine))]
    internal class ShipEnginePatch
    {
        public static int BoostersActive {  get; private set; }

        [HarmonyPrefix]
        [HarmonyPatch("SetBooster")]
        static void SetBooster()
        {
            BoostersActive++;
        }

        [HarmonyPrefix]
        [HarmonyPatch("ClearBooster")]
        static bool ClearBooster()
        {
            BoostersActive--;
            if (BoostersActive == 0) return true;
            return false;
        }

        [HarmonyPrefix]
        [HarmonyPatch("ApplyForce")]
        static void ApplyForcePrefix(ShipEngine __instance, out Vector3 __state)
        {
            __state = __instance.BoosterThrustPower;
            __instance.BoosterThrustPower = __instance.EngineThrustPower + BoostersActive*(__instance.BoosterThrustPower - __instance.EngineThrustPower);
        }

        [HarmonyPostfix]
        [HarmonyPatch("ApplyForce")]
        static void ApplyForcePostfix(ShipEngine __instance, Vector3 __state)
        {
            __instance.BoosterThrustPower = __state;
        }
    }
}

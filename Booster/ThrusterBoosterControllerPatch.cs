using CG.Ship.Modules;
using HarmonyLib;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace Booster
{
    [HarmonyPatch(typeof(ThrusterBoosterController))]
    internal class ThrusterBoosterControllerPatch
    {
        [HarmonyPostfix]
        [HarmonyPatch("GetReadiedBooster")]
        static void GetReadiedBooster(List<ThrusterBooster> ____thrusterBoosters, ref int index, ref ThrusterBooster __result)
        {
            if (__result != null) return;

            for (int i = 0; i < ____thrusterBoosters.Count; i++)
            {
                if (____thrusterBoosters[i].GetCurrentState() == ThrusterBoosterState.Charged)
                {
                    index = i;
                    __result = ____thrusterBoosters[i];
                    return;
                }
            }
        }
    }
}

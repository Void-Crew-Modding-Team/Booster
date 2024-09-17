using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using Photon.Pun;
using System;
using System.Reflection;

namespace Booster
{
    [BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
    [BepInProcess("Void Crew.exe")]
    [BepInDependency(VoidManager.MyPluginInfo.PLUGIN_GUID)]
    public class BepinPlugin : BaseUnityPlugin
    {
        public static bool HostHasMod { get; private set; } = false;

        internal static ManualLogSource Log;
        private void Awake()
        {
            Log = Logger;
            Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), MyPluginInfo.PLUGIN_GUID);
            VoidManager.Events.Instance.JoinedRoom += UpdateHost;
            VoidManager.Events.Instance.ClientModlistRecieved += UpdateHost;
            VoidManager.Events.Instance.MasterClientSwitched += UpdateHost;
            Logger.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} is loaded!");
        }

        private void UpdateHost(object sender, EventArgs e)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                HostHasMod = true;
                return;
            }
            HostHasMod = VoidManager.MPModChecks.NetworkedPeerManager.Instance.NetworkedPeerHasMod(PhotonNetwork.MasterClient, MyPluginInfo.PLUGIN_GUID);
        }
    }
}
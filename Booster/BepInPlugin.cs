using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using Photon.Pun;
using System;
using System.Reflection;

namespace Booster
{
    internal static class MyPluginInfo
    {
        internal const string PLUGIN_GUID = "id107.booster";
        internal const string PLUGIN_NAME = "Booster";
        internal const string PLUGIN_VERSION = "0.0.0";
    }

    [BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
    [BepInProcess("Void Crew.exe")]
    [BepInDependency("VoidManager")]
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
            HostHasMod = VoidManager.MPModChecks.MPModCheckManager.Instance.NetworkedPeerHasMod(PhotonNetwork.MasterClient, MyPluginInfo.PLUGIN_GUID);
        }
    }
}
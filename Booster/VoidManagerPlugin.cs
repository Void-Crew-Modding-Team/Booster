using VoidManager.MPModChecks;

namespace Booster
{
    public class VoidManagerPlugin : VoidManager.VoidPlugin
    {
        public static bool Enabled { get; private set; }

        public override MultiplayerType MPType => MultiplayerType.Host;

        public override string Author => MyPluginInfo.PLUGIN_AUTHORS;

        public override string Description => MyPluginInfo.PLUGIN_DESCRIPTION;

        public override string ThunderstoreID => MyPluginInfo.PLUGIN_THUNDERSTORE_ID;

        public override VoidManager.SessionChangedReturn OnSessionChange(VoidManager.SessionChangedInput input)
        {
            Enabled = input.HostHasMod;
            return new VoidManager.SessionChangedReturn() { SetMod_Session = true };
        }
    }
}

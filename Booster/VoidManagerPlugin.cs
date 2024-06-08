using VoidManager.MPModChecks;

namespace Booster
{
    public class VoidManagerPlugin : VoidManager.VoidPlugin
    {
        public override MultiplayerType MPType => MultiplayerType.Host;

        public override string Author => "18107";

        public override string Description => "Allows multiple boosts to be activated at the same time with diminishing returns";
    }
}

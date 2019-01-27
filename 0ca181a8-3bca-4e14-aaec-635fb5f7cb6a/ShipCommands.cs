using System.Collections.Generic;

namespace _0ca181a8_3bca_4e14_aaec_635fb5f7cb6a
{
    class ShipCommands
    {
        public List<double> LeftEngineToggles { get; }
        public List<double> RightEngineToggles { get; }
        public List<double> WeaponShots { get; }

        public ShipCommands(List<double> leftToggles, List<double> rightToggles, List<double> weaponsShots)
        {
            LeftEngineToggles = leftToggles;
            RightEngineToggles = rightToggles;
            WeaponShots = weaponsShots;
        }
    }
}

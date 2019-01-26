using System.Collections.Generic;

namespace _0ca181a8_3bca_4e14_aaec_635fb5f7cb6a
{
    class ShipCommands
    {
        public List<double> LeftEngineToggles { get; }
        public List<double> RightEngineToggles { get; }

        public ShipCommands(List<double> leftToggles, List<double> rightToggles)
        {
            LeftEngineToggles = leftToggles;
            RightEngineToggles = rightToggles;
        }
    }
}

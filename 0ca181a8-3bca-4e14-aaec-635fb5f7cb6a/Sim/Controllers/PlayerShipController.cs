using System.Linq;

namespace _0ca181a8_3bca_4e14_aaec_635fb5f7cb6a.Sim.Controllers
{
    class PlayerShipController : IShipController
    {
        public const double TurnLength = 3.0;

        private readonly ShipCommands _commands;
        private double _timePassed;

        public PlayerShipController(ShipCommands commands)
        {
            _commands = commands;
        }

        public bool RightEngineEnabled => _commands.RightEngineToggles.Count(t => t * TurnLength < _timePassed) % 2 == 1;
        public bool LeftEngineEnabled => _commands.LeftEngineToggles.Count(t => t * TurnLength < _timePassed) % 2 == 1;
        public bool GunEnabled => false;

        public void Update(World world, Ship ship, double dTime)
        {
            _timePassed += dTime;
        }
    }
}

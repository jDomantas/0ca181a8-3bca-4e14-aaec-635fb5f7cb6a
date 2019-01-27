using System.Linq;

namespace _0ca181a8_3bca_4e14_aaec_635fb5f7cb6a.Sim.Controllers
{
    class PlayerShipController : IShipController
    {
        private readonly ShipCommands _commands;
        private double _timePassed;
        
        public PlayerShipController(ShipCommands commands)
        {
            _commands = commands;
        }

        public bool RightEngineEnabled => _commands.RightEngineToggles.Count(t => t * World.TurnLength < _timePassed) % 2 == 1;
        public bool LeftEngineEnabled => _commands.LeftEngineToggles.Count(t => t * World.TurnLength < _timePassed) % 2 == 1;
        public bool GunEnabled { get; private set; }

        public void Update(World world, Ship ship, double dTime)
        {
            GunEnabled = false;
            foreach (var shot in _commands.WeaponShots)
            {
                var t = shot * World.TurnLength;
                if (t >= _timePassed && t < _timePassed + dTime)
                    GunEnabled = true;
            }
            _timePassed += dTime;
        }
    }
}

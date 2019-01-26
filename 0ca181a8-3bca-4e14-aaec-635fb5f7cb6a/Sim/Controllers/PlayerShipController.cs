using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0ca181a8_3bca_4e14_aaec_635fb5f7cb6a.Sim.Controllers
{
    class PlayerShipController : IShipController
    {
        public bool RightEngineEnabled { get; private set; }

        public bool LeftEngineEnabled { get; private set; }

        public bool GunEnabled { get; private set; }

        public void Update(World world, Ship ship, double dTime)
        {
            throw new NotImplementedException();
        }
    }
}

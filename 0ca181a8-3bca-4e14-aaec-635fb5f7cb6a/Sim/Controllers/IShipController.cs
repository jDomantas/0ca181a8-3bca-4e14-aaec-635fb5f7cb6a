using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0ca181a8_3bca_4e14_aaec_635fb5f7cb6a.Sim.Controllers
{
    interface IShipController
    {
        void Update(World world, Ship ship, double dTime);
        bool RightEngineEnabled { get; }
        bool LeftEngineEnabled { get; }
        bool GunEnabled { get; }
    }
}

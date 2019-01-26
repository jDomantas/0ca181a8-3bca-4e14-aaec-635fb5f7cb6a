using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0ca181a8_3bca_4e14_aaec_635fb5f7cb6a.Sim.Controllers
{
    class KeyboardShipController : IShipController
    {
        public bool RightEngineEnabled { get; private set; }

        public bool LeftEngineEnabled { get; private set; }

        public bool GunEnabled { get; private set; }

        public void Update(World world, Ship ship, double dTime)
        {
            LeftEngineEnabled = RightEngineEnabled = GunEnabled = false;
            var kState = Keyboard.GetState();
            if (kState.IsKeyDown(Keys.Left) || kState.IsKeyDown(Keys.A))
                LeftEngineEnabled = true;
            if (kState.IsKeyDown(Keys.Right) || kState.IsKeyDown(Keys.D))
                RightEngineEnabled = true;
            if (kState.IsKeyDown(Keys.Space))
                GunEnabled = true;
        }
    }
}

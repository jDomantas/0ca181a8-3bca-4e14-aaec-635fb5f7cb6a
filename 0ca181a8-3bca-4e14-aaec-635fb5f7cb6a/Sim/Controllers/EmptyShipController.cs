namespace _0ca181a8_3bca_4e14_aaec_635fb5f7cb6a.Sim.Controllers
{
    class EmptyShipController : IShipController
    {
        public bool RightEngineEnabled => false;
        public bool LeftEngineEnabled => false;
        public bool GunEnabled => false;

        public void Update(World world, Ship ship, double dTime) { }
    }
}

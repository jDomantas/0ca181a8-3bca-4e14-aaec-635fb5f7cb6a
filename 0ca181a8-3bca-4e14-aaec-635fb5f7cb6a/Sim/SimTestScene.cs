using Microsoft.Xna.Framework.Graphics;

namespace _0ca181a8_3bca_4e14_aaec_635fb5f7cb6a.Sim
{
    class SimTestScene : IScene
    {
        private World _world;

        public SimTestScene()
        {
            _world = new World();
            _world.Planets.Add(new Planet(new Vector(300, 400), 80));
            _world.Planets.Add(new Planet(new Vector(600, 200), 60));

            _world.Ships.Add(new Ship(new Vector(50, 80)));
            _world.Ships.Add(new Ship(new Vector(550, 380)));
        }

        public void Update()
        {
            _world.Update(1 / 60.0);
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Begin();
            _world.Draw(sb);
            sb.End();
        }
    }
}

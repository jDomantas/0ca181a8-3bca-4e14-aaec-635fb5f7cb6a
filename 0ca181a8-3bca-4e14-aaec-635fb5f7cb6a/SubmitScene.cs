using System;
using System.Collections.Generic;
using _0ca181a8_3bca_4e14_aaec_635fb5f7cb6a.Sim;
using _0ca181a8_3bca_4e14_aaec_635fb5f7cb6a.Sim.Controllers;

namespace _0ca181a8_3bca_4e14_aaec_635fb5f7cb6a
{
    class SubmitScene : PreviewScene
    {
        private readonly PlaybackManager _playbackManager;
        public SubmitScene(ISceneHost host, IScene previousScene, World world, Dictionary<Guid, IShipController> controllers, PlaybackManager playbackManager)
            : base(host, previousScene, world, controllers)
        {
            _playbackManager = playbackManager;
        }

        public override void Update()
        {
            base.Update();
            _playbackManager.Frames.Add(_world.Clone());
        }
    }
}

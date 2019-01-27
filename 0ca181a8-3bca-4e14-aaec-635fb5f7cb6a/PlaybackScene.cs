using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0ca181a8_3bca_4e14_aaec_635fb5f7cb6a
{
    class PlaybackScene : IScene
    {
        private const int Margin = 50;
        private readonly ISceneHost _host;
        private readonly IScene _previousScene;
        private readonly PlaybackManager _playbackManager;
        private int _currentFrame;

        public PlaybackScene(ISceneHost host, IScene previousScene, PlaybackManager playbackManager)
        {
            _host = host;
            this._previousScene = previousScene;
            _playbackManager = playbackManager;
            _currentFrame = 0;
        }

        public virtual void Update()
        {
            _currentFrame++;
            if (_currentFrame >= _playbackManager.Frames.Count)
            {
                _host.SetScene(_previousScene);
                return;
            }
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Begin();
            _playbackManager.Frames[_currentFrame].Draw(sb);
            int w = _host.ScreenWidth - 2*Margin;
            double fraction = (double)_currentFrame / _playbackManager.Frames.Count;
            sb.Draw(
                Resources.PlayButton,
                new Rectangle(Margin, _host.ScreenHeight - 100, w, 50),
                Color.White);
            sb.Draw(
                Resources.PlayButtonHover,
                new Rectangle(Margin, _host.ScreenHeight - 100, (int)(w*fraction), 50),
                new Rectangle(0, 0, (int)(fraction * Resources.PlayButtonHover.Width), Resources.PlayButton.Height),
                Color.White);
            sb.End();
        }
    }
}

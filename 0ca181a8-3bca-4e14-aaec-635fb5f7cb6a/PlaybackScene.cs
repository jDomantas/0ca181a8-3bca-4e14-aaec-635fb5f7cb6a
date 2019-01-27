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
        private const int BarHeight = 50;
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
            _playbackManager.Frames[_currentFrame].Draw(sb);
            sb.Begin();
            sb.Draw(
                Resources.Background,
                new Rectangle(0, 0, 1600, 900),
                new Rectangle(0, 0, Resources.Background.Width, Resources.Background.Width * 9 / 16),
                Color.LightGray);
            sb.Draw(Game1.WorldRenderTarget, new Rectangle(0, 0, 1600, 900), Color.White);
            int barWidth = Resources.BarEmpty.Width / Resources.BarEmpty.Height * BarHeight;
            double fraction = (double)_currentFrame / _playbackManager.Frames.Count;
            sb.Draw(
                Resources.BarEmpty,
                new Rectangle(Margin, _host.ScreenHeight - BarHeight - Margin, barWidth, BarHeight),
                Color.White);
            sb.Draw(
                Resources.BarFull,
                new Rectangle(Margin, _host.ScreenHeight - BarHeight - Margin, (int)(barWidth*fraction), BarHeight),
                new Rectangle(0, 0, (int)(fraction * Resources.BarFull.Width), Resources.BarFull.Height),
                Color.White);
            sb.End();
        }
    }
}

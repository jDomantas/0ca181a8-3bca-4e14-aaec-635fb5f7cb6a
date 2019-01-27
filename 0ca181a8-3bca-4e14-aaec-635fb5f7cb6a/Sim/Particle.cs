using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace _0ca181a8_3bca_4e14_aaec_635fb5f7cb6a.Sim
{
    class Particle
    {
        public Vector Position { get; }
        public List<Texture2D> Frames { get; }
        public bool IsFinished
        {
            get
            {
                return _currentFrame >= Frames.Count;
            }
        }
        private int _currentFrame;

        public Particle(Vector position, List<Texture2D> frames)
        {
            Position = position;
            Frames = frames;
            _currentFrame = 0;
        }

        public void Update()
        {
            _currentFrame++;
        }

        public void Draw(SpriteBatch sb)
        {
            if(!IsFinished)
            {
                const int Size = 70;
                sb.Draw(
                    Frames[_currentFrame],
                    new Rectangle((int)Position.X, (int)Position.Y, Size, Size),
                    null,
                    Color.White,
                    0,
                    new Vector2(Frames[_currentFrame].Width/2, Frames[_currentFrame].Height/2),
                    SpriteEffects.None,
                    0
                );
            }
        }

        public Particle Clone()
        {
            return new Particle(Position, Frames)
            {
                _currentFrame = _currentFrame
            };
        }
    }
}

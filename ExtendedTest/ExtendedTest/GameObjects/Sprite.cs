using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ExtendedTest
{

    public class Sprite
    {
        public Texture2D _Texture;
        public Vector2 _Position;
        public Game1 theGame;
        public bool _Draw = true;
        public int _HP;
        public bool _LockInScreen = false;
        public float speed = 0f;
        public int midpoint = 160;
        public int startHP = 1;
        public float lifeTime = 2.0f;
        //for inheritance
        public Sprite parent = null;
        public List<Sprite> _ChildrenList;
        public bool enemy = false;
        //for animation
        private float timeElapsed = 0;
        private int frameNum = 0;
        public int frameWidth;
        public int frameHeight;
        int FPS = 1;
        int Frames = 1;
        int StateNum;
        bool animLooping = false;
        public bool _FlipX = false;
        public bool _FlipY = false;
        public float _zOrder;
        public float _Scale = 1.0f;
        public Color _MyColor = Color.White;
        public float _Rotation = 0.0f;
        public bool isAnimated = false;

        public enum SpriteState
        {
            kStateActive,
            kStateInActive
        }

        public SpriteState _CurrentState = SpriteState.kStateInActive;

        public enum SpriteType
        {
            kPlayerType,
            kTreeType,
            kNoneType
        }

        public SpriteType _Tag = SpriteType.kNoneType;

        public Vector2 _Center
        {
            get
            {
                return new Vector2(frameWidth / 2, frameHeight / 2);
            }
        }

        public Rectangle _BoundingBox
        {
            get
            {
                return new Rectangle((int)_Position.X, (int)_Position.Y, frameWidth, frameHeight);
            }
        }

        public virtual void LoadContent(string path, Game1 tehGame, bool animated = false)
        {
            theGame = tehGame;
            _Texture = theGame.Content.Load<Texture2D>(path);
            if (!animated)
            {
                SetupAnimation(1, 1, 1, false);
            }
        }

        public virtual void Update(GameTime gameTime, List<Sprite> gameObjectList)
        {
            if (_CurrentState == SpriteState.kStateActive)
            {
                if (_ChildrenList != null)
                {
                    if (_ChildrenList.Count >= 1)
                    {
                        foreach (Sprite child in _ChildrenList)
                        {
                            child.Update(gameTime, gameObjectList);
                        }
                    }
                }

                if (_LockInScreen)
                {
                    LockInBounds();
                }
            }
            timeElapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        public void SetupAnimation(int frames, int fps, int states, bool looping)
        {
            Frames = frames;
            FPS = fps;
            frameWidth = _Texture.Width / frames;
            frameHeight = _Texture.Height / states;
            animLooping = looping;
        }

        public void Animate(int stateNum)
        {
            float TPF = 1.0f / FPS;
            if (timeElapsed >= TPF)
            {
                frameNum++;
                if (animLooping && frameNum > Frames)
                {
                    frameNum = 0;
                }
                else
                {

                }
                frameNum %= Frames;
                timeElapsed -= TPF;
            }
            StateNum = stateNum;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (_Draw)
            {
                Rectangle sr = new Rectangle((frameWidth * frameNum), (frameHeight * StateNum), frameWidth, frameHeight);
                if (!_FlipX && !_FlipY)
                {
                    spriteBatch.Draw(_Texture, _Position, sr, _MyColor, _Rotation, _Center, _Scale, SpriteEffects.None, 0f);
                }
                else if (_FlipX)
                {
                    spriteBatch.Draw(_Texture, _Position, sr, _MyColor, _Rotation, _Center, _Scale, SpriteEffects.FlipHorizontally, 0f);
                }
                else if (_FlipY)
                {
                    spriteBatch.Draw(_Texture, _Position, sr, _MyColor, _Rotation, _Center, _Scale, SpriteEffects.FlipVertically, 0f);
                }
                else if (_FlipX && _FlipY)
                {
                    spriteBatch.Draw(_Texture, _Position, sr, _MyColor, (_Rotation + (float)Math.PI), _Center, _Scale, SpriteEffects.None, 0f);
                }

                if (_ChildrenList != null)
                {
                    if (_ChildrenList.Count >= 1)
                    {
                        foreach (Sprite child in _ChildrenList)
                        {
                            child.Draw(spriteBatch);
                        }
                    }
                }
            }

        }

        public virtual void ReceiveDamage(int amt)
        {
            _HP -= amt;
            if (_HP <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            _CurrentState = SpriteState.kStateInActive;
            _Draw = false;
        }

        public void AddChild(Sprite child)
        {
            child.parent = this;
            _ChildrenList.Add(child);
        }

        public virtual void LockInBounds()
        {
            if ((_Position.X - (frameWidth / 2)) <= 0)
            {
                _Position.X = frameWidth / 2;
            }
            if ((_Position.X + (frameWidth / 2)) > 320)
            {
                _Position.X = 320 - (frameWidth / 2);
            }
        }
        public void ChangeColor(Color searchColor, Color toColor)
        {
            Color[] data = new Color[_Texture.Width * _Texture.Height];
            _Texture.GetData(data);
            for (int i = 0; i < data.Length; i++)
            {
                if (data[i] == searchColor)
                {
                    data[i] = toColor;
                }
            }

            _Texture.SetData(data);
        }

        public virtual void ResetSelf()
        {
            _Texture = null;
            _Position = Vector2.Zero;
            _Draw = true;
            _CurrentState = SpriteState.kStateActive;
            _Tag = SpriteType.kNoneType;
            _Rotation = 0.0f;
            _Scale = 1.0f;
            _FlipX = false;
            _FlipY = false;
            _LockInScreen = false;
            if (_ChildrenList != null)
            {
                if (_ChildrenList.Count >= 1)
                {
                    _ChildrenList.Clear();
                }
            }
            _MyColor = Color.White;
            parent = null;
            speed = 0f;
            midpoint = 160;
            Setup();
        }

        public virtual void Setup()
        {

        }

        public virtual void Activate()
        {
            _CurrentState = SpriteState.kStateActive;
            _Draw = true;
        }

        public virtual void Activate(Vector2 pos)
        {
            _HP = startHP;
            _CurrentState = SpriteState.kStateActive;
            _Draw = true;

            _Position = pos;
        }
    }
}

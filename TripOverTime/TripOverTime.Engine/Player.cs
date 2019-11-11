using System;
using System.Collections.Generic;
using System.Text;

namespace TripOverTime.EngineNamespace
{
    class Player
    {
        internal const string PLAYER_ID = "PLAYER420";
        internal float pw;
        internal float ph;
        internal const float JUMPING_SPEED = 0.2f;
        internal const float GRAVITY_SPEED = 0.2f;
        internal const float JUMPING_LIMIT = 1.5f;
        internal const float PPLAYER_MOVE = 0.15f;

        readonly Game _context;
        readonly Checkpoint _checkpoint;
        readonly String _name;
        Position _position; // Graphical position
        Position _realPosition;
        Life _life;
        int _attack;
        Sprite _sprite;
        bool _isJumping;
        Position _origin;
        string _orientation;

        internal Player(Game context, String name, Position position, Life life, int attack, string imgPath)
        {
            _context = context;
            _name = name;
            _position = position;
            _realPosition = new Position(_position.X, _position.Y);
            _life = life;
            _attack = attack;
            _isJumping = false;
            _sprite = new Sprite(PLAYER_ID, _name, imgPath, true, _context.GetMapObject, false, true);
            _orientation = "right";

            pw = _sprite.GetSprite.TextureRect.Width;
            ph = _sprite.GetSprite.TextureRect.Height;
        }

        internal void Jump()
        {
            if (!_isJumping)
            {
                _origin = new Position(_position.X, _position.Y);
                _realPosition.Y += JUMPING_SPEED;
                _position.Y += JUMPING_SPEED;
                _isJumping = true;

                _sprite.JumpAnimation();
            }
        }

        internal void Gravity()
        {
            if(_isJumping && _origin != null && _position.Y <= _origin.Y + JUMPING_LIMIT) // Jump
            {
                _realPosition.Y += JUMPING_SPEED;
                _position.Y += JUMPING_SPEED;
            }
            else // Fall
            {
                _origin = null;
                _realPosition.Y -= GRAVITY_SPEED;
                _position.Y -= GRAVITY_SPEED;
            }
        }

        internal void RoundY()
        {
            _position.Y = (int)Math.Round(_position.Y);
            _realPosition.Y = (int)Math.Round(_realPosition.Y);
        }

        internal void RoundX() // To recalibrate X (because precision of float)
        {
            _realPosition.X = (float)Math.Round(_realPosition.X, 2);
            _position.X = (float)Math.Round(_position.X, 2);
        }

        internal SFML.System.Vector2f MoveRight(SFML.Window.VideoMode videoMode)
        {
            
            // ORIENTATION
            if (_orientation != "right")
            {
                //Sprite to right
                _sprite.GetSprite.TextureRect = new SFML.Graphics.IntRect(0, 0, (int)PLAYER_WIDTH, (int)PLAYER_HEIGHT);
                _orientation = "right";
            }

            // ANIMATION
            _sprite.WalkAnimation();

            // MOVE
            SFML.System.Vector2f moveTheMapOf = new SFML.System.Vector2f(0, 0);

            if (_realPosition.X >= _context.GetMapObject.GetLimitMax.X) Console.WriteLine("Border of the map");
            else
            {
                Sprite s = null;
                if (_context.GetMapObject.GetMap.TryGetValue(new Position((float)Math.Round(_realPosition.X + PLAYER_MOVE, MidpointRounding.ToPositiveInfinity), (float)Math.Round(_realPosition.Y, MidpointRounding.ToNegativeInfinity)), out s)) // Block is solid?
                {
                    if (s.IsSolid) ;
                    // If player centered on screen, move the map now and not the player
                    else if (_sprite.GetSprite.Position.X < videoMode.Width / 2)
                    {
                        // Player move
                        _position.X += PLAYER_MOVE;
                        _realPosition.X += PLAYER_MOVE;
                    }
                    else
                    {
                        //Map move
                        moveTheMapOf = new SFML.System.Vector2f(128 / (1 / PLAYER_MOVE), 0);
                        _realPosition.X += PLAYER_MOVE;
                    }
                }
            }
            if (_context.GetMapObject.GetCheckpointPosition.Contains(_context.GetPlayer.Position) == true) _checkpoint.LastActivatedCheckpoint = _context.GetPlayer.Position;

            return moveTheMapOf;
        }

        internal SFML.System.Vector2f MoveLeft(SFML.Window.VideoMode videoMode)
        {
            // ORIENTATION
            if(_orientation != "left")
            {
                //Sprite to left
                _sprite.GetSprite.TextureRect = new SFML.Graphics.IntRect((int)PLAYER_WIDTH, 0, (int)-PLAYER_WIDTH, (int)PLAYER_HEIGHT);
                _orientation = "left";
            }

            // ANIMATION
            _sprite.WalkAnimation();

            // MOVE
            SFML.System.Vector2f moveTheMapOf = new SFML.System.Vector2f(0, 0);

            if (_realPosition.X <= _context.GetMapObject.GetLimitMin.X) Console.WriteLine("Border of the map");
            else
            {
                Sprite s = null;
                if (_context.GetMapObject.GetMap.TryGetValue(new Position((float)Math.Round(_realPosition.X - PLAYER_MOVE, MidpointRounding.ToNegativeInfinity), (float)Math.Round(_realPosition.Y, MidpointRounding.ToNegativeInfinity)), out s)) // Block is solid?
                {
                    if (s.IsSolid) ;
                    // If player left on screen, move the map now and not the player
                    else if (_sprite.GetSprite.Position.X > videoMode.Width / 5)
                    {
                        // Player move
                        _position.X -= PLAYER_MOVE; //0.25f
                        _realPosition.X -= PLAYER_MOVE;
                    }
                    else
                    {
                        //Map move
                        moveTheMapOf = new SFML.System.Vector2f(128 / (1 / PLAYER_MOVE), 0);
                        _realPosition.X -= PLAYER_MOVE;
                    }
                }
            }

            return moveTheMapOf;
        }

        internal bool IsJumping
        {
            get => _isJumping;
            set => _isJumping = value;
        }
        internal bool IsAlive
        {
            get => _life.CurrentPoint > 0;
        }

        internal Position Position
        {
            get => _position;
            set
            {
                _position = value;
            }
        }

        internal Position RealPosition
        {
            get => _realPosition;
            set { _realPosition = value; }
        }

        internal string Orientation
        {
            get => _orientation;
        }
        internal Sprite GetPlayerSprite
        {
            get => _sprite;
        }

        internal float PLAYER_WIDTH
        {
            get => pw;
        }

        internal float PLAYER_HEIGHT
        {
            get => ph;
        }

        internal float PLAYER_MOVE
        {
            get => PPLAYER_MOVE;
        }
    }
}

using Engine;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

    class PowerUp : SpriteGameObject
    {
    Level level;
    protected float bounce;
    Vector2 startPosition;
    bool timerIsTicking;
    float Time = 5f;
    float timeLeft;

    public PowerUp(Level level, Vector2 startPosition) : base("Sprites/LevelObjects/spr_powerup", TickTick.Depth_LevelObjects)
    {
        this.level = level;
        this.startPosition = startPosition;

        SetOriginToCenter();

        Reset();
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        double t = gameTime.TotalGameTime.TotalSeconds * 3.0f + LocalPosition.X;
        bounce = (float)Math.Sin(t) * 0.2f;
        localPosition.Y += bounce;

        if (timerIsTicking)
        {
            timeLeft -= (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        // check if the player collects this water drop
        if (Visible && level.Player.CanCollideWithObjects && HasPixelPreciseCollision(level.Player))
        {
            Visible = false;
            ExtendedGame.AssetManager.PlaySoundEffect("Sounds/snd_powerup");
            level.Player.speedMultiplier = 2f;
            timeLeft = Time;
            timerIsTicking = true;
        }

        // If player dies, stop speedboost
        if (!level.Player.IsAlive)
        {
            timeLeft = 0;
        }

        // if timer runs out, stop speedboost effetc
        if(timeLeft <= 0)
        {
            timerIsTicking = false;
            level.Player.speedMultiplier = 1f;
            timeLeft = Time;
        }

    }

    public override void Reset()
    {
        localPosition = startPosition;
        Visible = true;
        timeLeft = Time;
        timerIsTicking = false;
    }
}


using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class Camera
    {
        private static Camera instance;
        public Vector2 position;
        public Point cameraSize;
        public Point WorldSize;

        private Camera()
        {
            cameraSize = new Point(1368, 768);
        }

        // Create a static instance of the camera
        public static Camera Instance
        {
            get
            {
                if (instance == null) instance = new Camera();
                return instance;
            }
        }
        
        public void UpdatePosition(Vector2 playerPosition)
        {
            // Update the position of the camera based on the player's position and apply smoothing
            position = Vector2.Lerp(position, playerPosition - new Vector2(cameraSize.X / 2, cameraSize.Y / 2), 0.1f);

            // Keep camera position withing bounds
            ClampToWorldBorder();
        }

        private void ClampToWorldBorder()
        {
            position.X = Math.Clamp(position.X, 0, WorldSize.X - cameraSize.X);
            position.Y = Math.Clamp(position.Y, 0, WorldSize.Y - cameraSize.Y);
        }
    }

}

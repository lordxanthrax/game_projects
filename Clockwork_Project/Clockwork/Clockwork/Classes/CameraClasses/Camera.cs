﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Clockwork.Classes.CameraClasses
{
    public class Camera
    {
        public Camera(Viewport viewport)
        {
            Origin = new Vector2(viewport.Width / 2.0f, viewport.Height / 2.0f);
            Zoom = 1.0f;
            BaseViewport = viewport;
        }

        public Camera(Viewport viewport, Texture2D playerTexture)
        {
            Origin = new Vector2((viewport.Width / 2.0f) + (playerTexture.Width / 2.0f), (viewport.Height / 2.0f) + (playerTexture.Height / 2.0f));
            Zoom = 1.0f;
            BaseViewport = viewport;
        }

        public Viewport BaseViewport { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Origin { get; set; }
        public float Zoom { get; set; }
        public float Rotation { get; set; }

        public Matrix GetViewMatrix(Vector2 parallax)
        {
            // To add parallax, simply multiply it by the position
            return Matrix.CreateTranslation(new Vector3(-Position * parallax, 0.0f)) *
                // The next line has a catch. See note below.
                Matrix.CreateTranslation(new Vector3(-Origin, 0.0f)) *
                Matrix.CreateRotationZ(Rotation) *
                Matrix.CreateScale(Zoom, Zoom, 1) *
                Matrix.CreateTranslation(new Vector3(Origin, 0.0f));
        }

        public void LookAt(Vector2 position)
        {
            Position = position - new Vector2(BaseViewport.Width / 2.0f, BaseViewport.Height / 2.0f);
        }
    }
}

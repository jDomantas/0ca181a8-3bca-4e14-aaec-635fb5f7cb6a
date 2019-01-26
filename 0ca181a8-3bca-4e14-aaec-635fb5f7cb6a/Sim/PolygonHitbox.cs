﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Linq;

namespace _0ca181a8_3bca_4e14_aaec_635fb5f7cb6a.Sim
{
    class PolygonHitbox
    {
        public Vector[] Points { get; }
        public double BoundingRadius { get; }

        public PolygonHitbox(params Vector[] points)
        {
            Points = points;
            BoundingRadius = Points.Max(pt => pt.Length);
        }

        public bool IntersectsPlanet(Planet planet, Vector position, double rot)
        {
            var xAxis = Vector.AtAngle(rot);
            foreach (var pt in Points)
            {
                var p = pt.Translate(xAxis) + position;
                if ((p - planet.Position).LengthSquared <= planet.Radius * planet.Radius)
                    return true;
            }
            return false;
        }

        public bool IntersectsOther(PolygonHitbox box, Vector myPos, double myRot, Vector otherPos, double otherRot)
        {
            var dist = (myPos - otherPos).LengthSquared;
            var distLim = BoundingRadius + box.BoundingRadius;
            if (dist > distLim * distLim)
                return false;

            var xAxis1 = Vector.AtAngle(myRot);
            var xAxis2 = Vector.AtAngle(otherRot);

            for (int i = 0; i < Points.Length; i++)
            {
                for (int j = 0; j < Points.Length; j++)
                {
                    var a = Points[i].Translate(xAxis1) + myPos;
                    var b = Points[(i + 1) % Points.Length].Translate(xAxis1) + myPos;
                    var c = Points[j].Translate(xAxis2) + otherPos;
                    var d = Points[(j + 1) % Points.Length].Translate(xAxis2) + otherPos;
                    if (LineLineIntersect(a, b, c, d))
                        return true;
                }
            }
            return false;
        }

        private static bool LineLineIntersect(Vector a, Vector b, Vector c, Vector d)
        {
            var p = (c - a).Cross(d - c);
            var q = (b - a).Cross(d - c);
            if (Math.Abs(q) < 0.0001) return false;

            p /= q;
            if (p < 0 || p > 1) return false;

            var r = (b - a).Cross(a - c);
            r /= q;
            if (r < 0 || r > 1) return false;

            return true;
        }

        public void DrawDebug(SpriteBatch sb, Vector position, double rot)
        {
            var xAxis = Vector.AtAngle(rot);

            for (int i = 0; i < Points.Length; i++)
            {
                var a = Points[i];
                var b = Points[(i + 1) % Points.Length];
                DrawLine(sb, position + a.Translate(xAxis), position + b.Translate(xAxis));
            }
        }

        private void DrawLine(SpriteBatch sb, Vector a, Vector b)
        {
            var d = b - a;
            var angle = Math.Atan2(d.Y, d.X);
            sb.Draw(
                Resources.Pixel,
                new Rectangle((int)Math.Round(a.X), (int)Math.Round(a.Y), (int)Math.Round(d.Length), 2),
                null,
                Color.Red,
                (float)angle,
                new Vector2(0, 0.5f),
                SpriteEffects.None,
                0);
        }

        public static PolygonHitbox DefaultBox = new PolygonHitbox(
            new Vector(30, 0),
            new Vector(-20, 20),
            new Vector(-10, 5),
            new Vector(-10, -5),
            new Vector(-20, -20));
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using TutorialMod;
using static alglib;

namespace TutorialMod
{
    public class ModTargeting
    {
        public static Vector2 LinearAdvancedTargeting(Vector2 sourcepos, Vector2 targetpos, Vector2 targetvel, float shotspeed, ref float t)
        {
            Vector2 pos = targetpos - sourcepos;
            Vector2 v = targetvel;
            Vector2 w;                              //the output aka the velocity of the proj

            if (shotspeed < targetvel.Length())
            {
                v = Vector2.Zero;
            }
            
            t = (float)System.Math.Sqrt(System.Math.Pow(shotspeed, 2) * (System.Math.Pow(pos.Length(), 2)) - System.Math.Pow(v.Y * pos.X - v.X * pos.Y, 2)) + v.Y * pos.Y + v.X * pos.X;
            t =  t / (float)(System.Math.Pow(shotspeed, 2) - System.Math.Pow(v.Length(), 2));
            w = pos / t + v;
            return w;
        }
        public static Vector2 LinearAdvancedTargeting(Vector2 sourcepos, Vector2 targetpos, Vector2 targetvel, float shotspeed)
        {
            Vector2 pos = targetpos - sourcepos;
            Vector2 v = targetvel;
            Vector2 w;                              //the output aka the velocity of the proj
            float t;

            if (shotspeed < targetvel.Length())
            {
                v = Vector2.Zero;
            }

            t = (float)System.Math.Sqrt(System.Math.Pow(shotspeed, 2) * (System.Math.Pow(pos.Length(), 2)) - System.Math.Pow(v.Y * pos.X - v.X * pos.Y, 2)) + v.Y * pos.Y + v.X * pos.X;
            t = t / (float)(System.Math.Pow(shotspeed, 2) - System.Math.Pow(v.Length(), 2));
            w = pos / t + v;
            return w;
        }
        public static Vector2 LinearAdvancedTargetingA(Vector2 sourcepos, Vector2 targetpos, Vector2 targetvel, Vector2 targetacc, float shotspeed)
        {
            Vector2 pos = targetpos - sourcepos;                                                                                //rename the variables for easier math
            Vector2 v = targetvel;          
            Vector2 a = targetacc;                  
            Vector2 w;                                                                                                          //the output aka the velocity of the proj
            float t;                                                                                                            //time aka frames. we use this to determine the exact point of contact and treat at as a time in the calculation           
            double a0 = Math.Pow(pos.Length(), 2);                                                                              //coefficients for the quadric
            double a1 = 2 * (v.X * pos.X + v.Y * pos.Y);
            double a2 = Math.Pow(v.Length(), 2) + 2 * (a.X * pos.X + a.Y * pos.Y) - Math.Pow(shotspeed, 2);
            double a3 = 2 * (a.Y * v.Y + a.X * v.X);
            double a4 = Math.Pow(a.Length(), 2);
            double[] coef = { a0, a1, a2, a3, a4 };
            double[] RET = new double[4];                                                                                       //Real part of the complex T array
            alglib.complex[] T = new alglib.complex[4];                                                                         //complex array since we only expect real solutions we define RET and discard the imaginary part
            alglib.polynomialsolverreport rep;

            if (a.Length() == 0)                                                                                                //!!important the polysolver only works if the leading coefficient is not 0
            {
                return LinearAdvancedTargeting(sourcepos, targetpos, targetvel, shotspeed);
            }

            if (shotspeed < targetvel.Length())
            {
                v = Vector2.Zero;
            }
            alglib.polynomialsolve(coef, 4, out T, out rep);
            for (int j = 0; j<=3; j++)
            {
                RET[j] = T[j].x;
            }
            Array.Sort(RET);
            t = (float)RET[2];
            w = pos / t + v + t*a;
            return w;
        }

        public static Vector2 TargetPosition(Vector2 targetPos, Vector2 npcPos, float projVel)
        {
            Vector2 temp = targetPos - npcPos;
            temp.Normalize();
            temp *= projVel;
            return temp;
        }
        
        public static void FallingTargeting(NPC npc, Player target, Vector2 Yoffset, int ShotSpeed ,ref float delay, ref Vector2 projectileVel )
        {
            int temp = 1;
            if (target.velocity.Y > 0) //only check when falling
            {
                while (temp <= delay)   //simulate movement and get the intersectionpoint of the player block collision
                {
                    Vector2 predPos = target.Center + target.velocity * temp; //movement equation constant velocity
                    Point tileLoc = predPos.ToTileCoordinates();    
                    Tile tile = Framing.GetTileSafely(tileLoc.X, tileLoc.Y);
                    if (Main.tileSolidTop[tile.type] || (tile.active() && Main.tileSolid[tile.type]))
                    {
                        break;
                    }
                    temp++;
                }
                if (temp < delay)   //if collision happened set new speed
                {
                    projectileVel = ModTargeting.TargetPosition(target.Center + Yoffset + target.velocity * temp, npc.Center, ShotSpeed);
                }
            }
        }


    }
}

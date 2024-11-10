using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using StorybrewCommon.Mapset;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;
using StorybrewCommon.Storyboarding.Util;
using StorybrewCommon.Subtitles;
using StorybrewCommon.Util;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StorybrewScripts
{
    public class GuitarSolo2 : StoryboardObjectGenerator
    {
        internal int StartTime = 228917;
        internal int EndTime = 255571;
        internal StoryboardLayer Layer;
        internal double BeatDuration;
        public override void Generate()
        {
            Layer = GetLayer("Guitar Solo 2 With Vocals");
            BeatDuration = Beatmap.GetTimingPointAt(StartTime).BeatDuration;
            NoBlur(StartTime, 230994, 60);
            Zoom(230994, 232610, -0.2, 0.1, 200);
            NoBlur(232610, 234687, -50);
            Zoom(234687, 236302, 0.3, 0.15, -150);
            NoBlur(236302, 239879, 100, true);
            Blur(239879, 243571);
            CreateFadeinSprites(243571, 247264);
            CreateEnd(247264, 251994);
        }
        internal void Blur(int start, int end)
        {
            OsbSprite bg = Layer.CreateSprite("sb/bg/blur/bg1background.jpg", OsbOrigin.Centre);
            bg.Fade(start, 1);
            bg.Fade(end, 0);
            bg.Rotate(start, 0);
            bg.Scale(start, 0.6);

            OsbSprite vignette = Layer.CreateSprite("sb/vignette.png", OsbOrigin.Centre);
            vignette.Fade(start, 0.5);
            vignette.Fade(end, 0);
            vignette.Scale(start, 0.5);
            vignette.Color(start, Color4.Black);

            Flash(start, end);

        }
        internal void CreateEnd(int start, int end)
        {
            
            OsbSprite bg = Layer.CreateSprite("sb/bg/bg2background.jpg", OsbOrigin.Centre);
            bg.Fade(start, 0.6);
            bg.Fade(end, 0);
            bg.Scale(OsbEasing.OutSine, start, end, 0.5, 0.45);

            
            P2ParticleEffect(250, 0.4, start, end);
            
            OsbSprite foreground = Layer.CreateSprite("sb/bg/bg2fullfront.png", OsbOrigin.Centre);
            foreground.Fade(start, 1);
            foreground.Fade(end, 0);
            foreground.Scale(OsbEasing.OutSine, start, end, 0.55, 0.45);

            OsbSprite girl = Layer.CreateSprite("sb/girl/character_unsaturated.png", OsbOrigin.Centre);
            girl.Fade(start, 1);
            girl.Fade(end, 0);
            girl.Scale(OsbEasing.OutSine, start, end, 0.6, 0.45);
            girl.MoveY(start, 360);

            P2ParticleEffect(250, 0.4, start, end);

            OsbAnimation noise = Layer.CreateAnimation("sb/noise/fc/n.jpg", 8, BeatDuration*0.5, OsbLoopType.LoopForever);
            noise.Fade(start, 0.1);
            noise.Fade(end, 0);
            OsbSprite cover = Layer.CreateSprite("sb/1px.png", OsbOrigin.Centre);
            cover.Fade(start, 0.2);
            cover.Fade(end, 0);
            cover.ScaleVec(start, 854, 480);
            cover.Color(start, Color4.Black);

            OsbSprite flash = Layer.CreateSprite("sb/1px.png", OsbOrigin.Centre);
            flash.Fade(start, start + BeatDuration, 0.8, 0);
            flash.ScaleVec(start, 854, 480);
            flash.Fade(250148, 251994, 0, 0.8);
            flash.Fade(end, 253840, 1, 0);
        }

        internal void P2ParticleEffect(int ct, double factor, double start, double end)
        {
            for (int i = 0; i < ct; i++)
            {
                OsbSprite p = Layer.CreateSprite("sb/p.png", OsbOrigin.Centre);
                double time = Random(800, 4000);
                double scale = 1000/time * factor;
                int x = Random(-127, 767);
                int dx = Random(20, 70);
                p.Fade(start, start, 0, 1);
                p.Fade(end, end, 1, 0);
                p.ScaleVec(start, scale*Random(0.1, 0.5), scale);
                p.Rotate(start, Math.Atan2(x < 320 ? -dx:dx, 480));
                p.StartLoopGroup(start - Random(0, time), (int)((end - start) / time) + 2);
                    p.Move(0, time, x, 0, x + (x < 320 ? dx:-dx), 480);
                p.EndGroup();
                p.Additive(start);
            }
        }

        internal void Zoom(int start, int end, double dr, double dscale, int dx)
        {
            OsbSprite bg = Layer.CreateSprite("sb/bg/blur/bg1background.jpg", OsbOrigin.Centre);
            bg.Fade(start, 1);
            bg.Fade(end, 0);
            bg.Rotate(OsbEasing.OutCirc, start, start + BeatDuration*2, 0, dr*0.5);
            bg.Scale(OsbEasing.OutCirc, start, end, 0.6, 0.6 + dscale);

            OsbSprite vignette = Layer.CreateSprite("sb/vignette.png", OsbOrigin.Centre);
            vignette.Fade(start, 0.5);
            vignette.Fade(end, 0);
            vignette.Scale(start, 0.5);
            vignette.Color(start, Color4.Black);



            OsbAnimation snowflake = Layer.CreateAnimation("sb/snowflakes/snowflake.png", 25, BeatDuration*0.5, OsbLoopType.LoopForever);
            snowflake.Fade(start, 0.5);
            snowflake.Fade(end, 0);
            snowflake.Scale(start, 0.3);
            snowflake.Move(start, Random(200, 400), Random(200, 340));
            OsbSprite girl = Layer.CreateSprite("sb/girl/character_full.png", OsbOrigin.Centre);
            girl.Fade(start, 1);
            girl.Fade(end, 0);
            girl.Rotate(OsbEasing.OutCirc, start, start + BeatDuration*2, 0, dr);
            girl.Scale(OsbEasing.OutCirc, start, end, 0.45, 0.45+ dscale*2);
            girl.MoveX(OsbEasing.OutCirc, start, start + BeatDuration*2, 320, 320 + dx);
            if (dx < 0) girl.FlipH(start);



            Flash(start, end);

        }
        internal void Flash(int start, int end)
        {
            OsbSprite flash = Layer.CreateSprite("sb/1px.png", OsbOrigin.Centre);
            flash.Fade(start, start + BeatDuration, 0.6, 0);
            flash.ScaleVec(start, 854, 480);
            flash.Fade(end - BeatDuration*2, end, 0, 0.4);
        }
        
        internal void NoBlur(int start, int end, int dx, bool IncludeGirl = false)
        {
            int y = 170;
            OsbSprite bg = Layer.CreateSprite("sb/bg/bg1sky.jpg", OsbOrigin.Centre);
            bg.Fade(start, 1);
            bg.Fade(end, 0);
            bg.MoveX(OsbEasing.OutSine, start, end, 320 - dx*0.5, 320 + dx*0.5); 
            bg.Scale(start, 0.5);

            OsbSprite mtn = Layer.CreateSprite("sb/bg/bg1mountains.png", OsbOrigin.Centre);
            mtn.Fade(start,1 );
            mtn.Fade(end, 0);
            mtn.MoveX(OsbEasing.OutSine, start, end, 320 - dx*0.6, 320 + dx*0.6);
            mtn.Scale(start, 0.6);
            mtn.MoveY(start, y);

            OsbSprite haze = Layer.CreateSprite("sb/bg/haze.png", OsbOrigin.Centre);
            haze.Fade(start, 1);
            haze.Fade(end, 0);
            haze.MoveX(OsbEasing.OutSine, start, end, 320 - dx*0.6, 320 + dx*0.6);
            haze.Scale(start, 0.58);
            haze.MoveY(start, y);
            OsbSprite fore = Layer.CreateSprite("sb/bg/bg1foreground.png", OsbOrigin.Centre);
            fore.Fade(start, 1);
            fore.Fade(end, 0);
            fore.MoveX(OsbEasing.OutSine, start, end, 320 - dx*0.7, 320 + dx*0.7);
            fore.Scale(start, 0.62);
            fore.MoveY(start, y);

            if (IncludeGirl)
            {
                OsbSprite girl = Layer.CreateSprite("sb/girl/character_full.png", OsbOrigin.Centre);
                girl.Fade(start, 1);
                girl.Fade(end, 0);
                girl.MoveX(OsbEasing.OutSine, start, end, 100 - dx*0.8, 100 + dx*0.8);
                girl.Scale(start, 0.55);
            }

            OsbSprite tree = Layer.CreateSprite("sb/bg/bg1fronttree.png", OsbOrigin.Centre);
            tree.Fade(start, 1);
            tree.Fade(end, 0);
            tree.MoveX(OsbEasing.OutSine, start, end, 220 - dx, 220 + dx);
            tree.MoveY(start, y);
            tree.Scale(start, 0.7);

            

            OsbAnimation over = Layer.CreateAnimation("sb/grain2/g.jpg", 6, BeatDuration*0.5, OsbLoopType.LoopForever);
            over.Fade(start, 0.1);
            over.Fade(end, 0);

            Flash(start, end);
            
        }

         internal void CreateFadeinSprites(int start, int end)
        {
            
            OsbSprite bg = Layer.CreateSprite("sb/bg/bg1sky.jpg", OsbOrigin.Centre);
            GiveDefaults(bg, start, end, 1, 0.5);

            OsbAnimation grunge = Layer.CreateAnimation("sb/grain/g.jpg", 10, BeatDuration*0.5, OsbLoopType.LoopForever);
            GiveDefaults(grunge, start, end, 0.5, 1);
            grunge.Additive(start);
            OsbSprite mtn = Layer.CreateSprite("sb/bg/bg1mountains.png", OsbOrigin.Centre);
            GiveDefaults(mtn, start, end, 1, 0.5);

            OsbSprite fore = Layer.CreateSprite("sb/bg/bg1foreground.png", OsbOrigin.Centre);
            GiveDefaults(fore, start, end, 1, 0.5);

            Flash(start, end);

        }
        internal void GiveDefaults(OsbSprite a, int start, int end, double opacity, double scale)
        {
            a.Fade(start, opacity);
            a.Fade(end, 0);
            a.Scale(start, scale);
        }
    }
}

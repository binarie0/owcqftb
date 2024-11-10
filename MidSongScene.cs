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
    public class MidSongScene : StoryboardObjectGenerator
    {
        internal int StartTime = 146994, EndTime = 168917, MidTime = 154148, SecondMidTime = 165225;
        internal StoryboardLayer Layer, GirlLayer;
        internal double BeatDuration;

        public override void Generate()
        {
            Layer = GetLayer("Mid Song Scene");
            GirlLayer = GetLayer("Mid Song Scene - Girl Overlay");
            BeatDuration = Beatmap.GetTimingPointAt(StartTime).BeatDuration;

            P1();
            P2();
            CreateEnd(SecondMidTime, EndTime);
        }
        internal void P2()
        {
            OsbSprite bg = Layer.CreateSprite("sb/bg/blur/bg1background.jpg", OsbOrigin.Centre);
            bg.Scale(MidTime, 0.9);
            bg.Fade(MidTime, 0.6);
            bg.Fade(SecondMidTime, 0);

            OsbAnimation anim = Layer.CreateAnimation("sb/grain/g.jpg", 10, BeatDuration*0.5, OsbLoopType.LoopForever);
            anim.Fade(MidTime, 0.1);
            anim.Fade(SecondMidTime, 0);

            OsbSprite girl = Layer.CreateSprite("sb/girl/character_full.png", OsbOrigin.Centre);
            girl.Fade(MidTime, 1);
            girl.Fade(SecondMidTime, 0);
            girl.MoveX(MidTime, SecondMidTime, 140, 100);
            girl.Scale(MidTime, 1);
            girl.MoveY(MidTime, 480);
            girl.Rotate(OsbEasing.OutCirc, MidTime, MidTime + BeatDuration, 0, 0.01);
            

        }
        internal void CreateEnd(int start, int end)
        {
            
            OsbSprite bg = Layer.CreateSprite("sb/bg/bg2background.jpg", OsbOrigin.Centre);
            bg.Fade(start, 0.6);
            bg.Fade(end, 0);
            bg.Scale(OsbEasing.OutSine, start, end, 0.5, 0.45);

            
            P2ParticleEffect(60, 0.4, start, end);
            
            OsbSprite foreground = Layer.CreateSprite("sb/bg/bg2fullfront.png", OsbOrigin.Centre);
            foreground.Fade(start, 1);
            foreground.Fade(end, 0);
            foreground.Scale(OsbEasing.OutSine, start, end, 0.55, 0.45);

            OsbSprite girl = Layer.CreateSprite("sb/girl/character_unsaturated.png", OsbOrigin.Centre);
            girl.Fade(start, 1);
            girl.Fade(end, 0);
            girl.Scale(OsbEasing.OutSine, start, end, 0.6, 0.45);
            girl.MoveY(start, 360);
            girl.FlipH(start);

            P2ParticleEffect(100, 0.4, start, end);

            OsbAnimation noise = Layer.CreateAnimation("sb/noise/fc/n.jpg", 8, BeatDuration*0.5, OsbLoopType.LoopForever);
            noise.Fade(start, 0.1);
            noise.Fade(end, 0);
            OsbSprite cover = Layer.CreateSprite("sb/1px.png", OsbOrigin.Centre);
            cover.Fade(start, 0.2);
            cover.Fade(end, 0);
            cover.ScaleVec(start, 854, 480);
            cover.Color(start, Color4.Black);

            OsbSprite flash = Layer.CreateSprite("sb/1px.png", OsbOrigin.Centre);
            flash.Fade(start, start + BeatDuration*4, 0.6, 0);
            flash.ScaleVec(start, 854, 480);
            
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

        internal void P1()
        {
            OsbSprite bg = Layer.CreateSprite("sb/bg/blur/bg1background.jpg", OsbOrigin.Centre);
            bg.Scale(StartTime, 0.5);
            bg.Fade(StartTime, 0.8);
            bg.Additive(StartTime);
            bg.Fade(MidTime, 0);

            OsbSprite flare = Layer.CreateSprite("sb/flares/1.png", OsbOrigin.Centre);
            flare.Fade(StartTime, 0.4);
            flare.Scale(StartTime, 0.45);
            flare.FlipH(StartTime);
            flare.Fade(MidTime, 0);
            flare.Additive(StartTime);

            OsbAnimation anim = Layer.CreateAnimation("sb/grain/g.jpg", 10, BeatDuration*0.5, OsbLoopType.LoopForever);
            anim.Fade(StartTime, 0.2);
            anim.Fade(MidTime, 0);

            
            OsbSprite girl = GirlLayer.CreateSprite("sb/girl/character_withhand.png", OsbOrigin.CentreLeft);
            girl.Move(OsbEasing.OutCirc, StartTime, StartTime + BeatDuration*8, -127, 240, -107, 240);
            girl.Move(OsbEasing.InSine,  StartTime + BeatDuration*8, MidTime, -107, 240, -117, 240);
            girl.Scale(StartTime, 0.3);
            girl.Fade(StartTime, 1);
            girl.Fade(MidTime, 0);

        }
    }
}

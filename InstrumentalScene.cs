using OpenTK;
using OpenTK.Graphics;
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
    public class InstrumentalScene : StoryboardObjectGenerator
    {
        [Configurable]
        public int StartTime = 0;

        [Configurable]
        public int EndTime = 0;

        [Configurable(DisplayName = "With Text Overlay")]
        public bool IncludeTextOverlay = false;
        internal StoryboardLayer Layer;

        internal double BeatDuration;

        internal double P1EndTime, P2EndTime;
        public override void Generate()
        {
            BeatDuration = Beatmap.GetTimingPointAt(StartTime).BeatDuration;

            P1EndTime = StartTime + BeatDuration*32;
            P2EndTime = P1EndTime + BeatDuration*32.5;
		    if (EndTime == 0 || StartTime == 0) throw new Exception();

            Layer = GetLayer($"Instrumental Scene ({StartTime} to {EndTime})");

            Part1();
            Part2();
            
            #region Overlays

            OsbAnimation noise = Layer.CreateAnimation("sb/noise/fc/n.jpg", 8, BeatDuration*0.25, OsbLoopType.LoopForever);
            noise.Fade(StartTime, 0.1);
            noise.Additive(StartTime);
            noise.Fade(EndTime, 0);

            OsbSprite brightness = Layer.CreateSprite("sb/1px.png", OsbOrigin.Centre);
            brightness.Fade(StartTime, 0.3);
            brightness.Fade(EndTime, 0);
            brightness.ScaleVec(StartTime, 854, 400);
            brightness.Color(StartTime, Color4.Black);

            
            #endregion
            #region Text Overlay
            if (IncludeTextOverlay)
            {
                OsbSprite logo = Layer.CreateSprite("sb/whiteout.png", OsbOrigin.Centre);
                logo.Scale(OsbEasing.OutCirc, StartTime, StartTime + BeatDuration, 0.6, 0.5);
                logo.Scale(OsbEasing.OutSine, StartTime + BeatDuration, StartTime + BeatDuration*21, 0.5, 0.45);
                
                logo.Additive(StartTime);
                logo.Fade(EndTime, EndTime, 1, 0);
            }
            #endregion


            OsbSprite transition = Layer.CreateSprite("sb/1px.png", OsbOrigin.Centre);
            transition.Fade(P1EndTime, P1EndTime + BeatDuration*4, 0.5, 0);
            transition.ScaleVec(P1EndTime, 854, 480);
        }
        internal void P1ParticleEffect(int ct, double factor)
        {
            for (int i = 0; i < ct; i++)
            {
                OsbSprite p = Layer.CreateSprite("sb/p.png", OsbOrigin.Centre);
                double time = Random(1000, 5000);
                double scale = 1000/time * factor;
                int x = Random(-257, 747);
                int dx = Random(50, 200);
                p.Fade(StartTime, StartTime, 0, 1);
                p.Fade(P1EndTime, P1EndTime, 1, 0);
                p.Scale(StartTime, scale);
                p.StartLoopGroup(StartTime - Random(0, time), (int)((P1EndTime - StartTime) / time) + 2);
                    p.Move(0, time, x, 0, x + dx, 480);
                p.EndGroup();
                p.Additive(StartTime);
            }
        }
        internal void P2ParticleEffect(int ct, double factor)
        {
            for (int i = 0; i < ct; i++)
            {
                OsbSprite p = Layer.CreateSprite("sb/p.png", OsbOrigin.Centre);
                double time = Random(800, 4000);
                double scale = 1000/time * factor;
                int x = Random(-127, 767);
                int dx = Random(20, 70);
                p.Fade(P1EndTime, P1EndTime, 0, 1);
                p.Fade(P2EndTime, P2EndTime, 1, 0);
                p.Scale(StartTime, scale);
                p.StartLoopGroup(P1EndTime - Random(0, time), (int)((P2EndTime - P1EndTime) / time) + 2);
                    p.Move(0, time, x, 0, x + (x < 320 ? dx:-dx), 480);
                p.EndGroup();
                p.Additive(StartTime);
            }
        }
        internal void Part2()
        {
            
            #region Background
            OsbSprite bg = Layer.CreateSprite("sb/bg/bg2background.jpg", OsbOrigin.Centre);
            bg.Fade(P1EndTime, 1);
            bg.Fade(P2EndTime, 0);
            bg.Scale(OsbEasing.OutSine, P1EndTime, P2EndTime, 0.5, 0.45);
            bg.MoveY(OsbEasing.OutSine, P1EndTime, P2EndTime, 260, 240);

            OsbSprite haze = Layer.CreateSprite("sb/bg/bg2haze.png", OsbOrigin.Centre);
            haze.Fade(P1EndTime, 1);
            haze.Fade(P2EndTime, 0);
            haze.Scale(OsbEasing.OutSine, P1EndTime, P2EndTime, 0.52, 0.45);
            
            P2ParticleEffect(50, 0.2);

            OsbSprite foreground = Layer.CreateSprite("sb/bg/bg2foreground.png", OsbOrigin.Centre);
            foreground.Fade(P1EndTime, 1);
            foreground.Fade(P2EndTime, 0);
            foreground.Scale(OsbEasing.OutSine, P1EndTime, P2EndTime, 0.65, 0.48);
            
            P2ParticleEffect(50, 0.3);

            OsbSprite girl = Layer.CreateSprite("sb/girl/character_unsaturated.png", OsbOrigin.Centre);
            girl.Fade(P1EndTime, 1);
            girl.Fade(P2EndTime, 0);
            girl.Scale(OsbEasing.OutSine, P1EndTime, P2EndTime, 0.6, 0.45);
            
            P2ParticleEffect(50, 0.3);

            OsbSprite trees = Layer.CreateSprite("sb/bg/background2trees.png", OsbOrigin.Centre);
            trees.Fade(P1EndTime, 1);
            trees.Fade(P2EndTime, 0);
            trees.Scale(OsbEasing.OutSine, P1EndTime, P2EndTime, 0.6, 0.5);

            #endregion
        }
        internal void Part1()
        {
            #region Background

            OsbSprite bg = Layer.CreateSprite("sb/bg/bg1background.png", OsbOrigin.Centre);
            bg.Fade(StartTime, 1);
            bg.Fade(P1EndTime, 0);
            bg.Scale(StartTime, 0.5);
            bg.MoveX(OsbEasing.OutSine, StartTime, P1EndTime, 310, 320);

            

            OsbSprite foreground = Layer.CreateSprite("sb/bg/bg1foreground.png", OsbOrigin.Centre);
            foreground.Fade(StartTime, 1);
            foreground.Fade(P1EndTime, 0);
            foreground.Scale(StartTime, 0.5);
            foreground.MoveX(OsbEasing.OutSine, StartTime, P1EndTime, 300, 330);

            P1ParticleEffect(240, 0.2);

            OsbSprite tree = Layer.CreateSprite("sb/bg/bg1fronttree.png", OsbOrigin.Centre);
            tree.Fade(StartTime, 1);
            tree.Fade(P1EndTime, 0);
            tree.Scale(StartTime, 0.5);
            tree.MoveX(OsbEasing.OutSine, StartTime, P1EndTime, 230, 330);

            P1ParticleEffect(50, 0.45);
            #endregion
        }
    }
}

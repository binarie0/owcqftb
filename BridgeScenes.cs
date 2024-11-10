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
using System.Diagnostics.Contracts;
using System.Linq;

namespace StorybrewScripts
{
    public class BridgeScenes : StoryboardObjectGenerator
    {
        [Configurable]
        public int StartTime = 0;
        [Configurable]
        public int EndTime = 0;

        internal double BeatDuration;

        internal StoryboardLayer Layer;

        internal List<OsbSprite> fadeinSprites;

        OsbSprite cover, bling, flash;
        public override void Generate()
        {
		    if (StartTime == 0 || EndTime == 0 || StartTime == EndTime) throw new Exception();
            BeatDuration = Beatmap.GetTimingPointAt(StartTime).BeatDuration;
            Layer = GetLayer($"Bridge ({StartTime} - {EndTime})");
            
            CreateFadeinSprites();
            CreateCover();
            FadeIn(StartTime, StartTime + BeatDuration*4.5, 0.2);
            FadeIn(StartTime + BeatDuration*8, StartTime + BeatDuration*12.5, 0.2);

            FadeInInstant(StartTime + BeatDuration*16, StartTime + BeatDuration*32, 0.7);

            FadeIn(StartTime + BeatDuration*32, StartTime + BeatDuration*36.5, 0.2);
            FadeIn(StartTime + BeatDuration*40, StartTime + BeatDuration*44.5, 0.2);

            FadeInInstant(StartTime + BeatDuration*48, StartTime + BeatDuration*64, 0.7);

            CreateBling();
            FlashBling(new Vector2(580, 110), StartTime + BeatDuration*4.5, StartTime + BeatDuration*8.5, 0.1);
            FlashBling(new Vector2(140, 340), StartTime + BeatDuration*12.5, StartTime + BeatDuration*16.5, -0.67);

            FlashBling(new Vector2(80, 50), StartTime + BeatDuration*36.5, StartTime + BeatDuration*40.5, -0.1);
            FlashBling(new Vector2(400, 270), StartTime + BeatDuration*44.5, StartTime + BeatDuration*48.5, 0.8);
            
            CreateEnd();
            CreateFlash();
            Flash(StartTime + BeatDuration*16, StartTime + BeatDuration*20, 0.6, 0);
            Flash(StartTime + BeatDuration*48, StartTime + BeatDuration*52, 0.6, 0);
            Flash(StartTime + BeatDuration*64, StartTime + BeatDuration*68, 0.8, 0);

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

        internal void CreateEnd()
        {
            double t1 = StartTime + BeatDuration*64;
            OsbSprite bg = Layer.CreateSprite("sb/bg/bg2background.jpg", OsbOrigin.Centre);
            bg.Fade(t1, 0.6);
            bg.Fade(EndTime, 0);
            bg.Scale(OsbEasing.OutSine, t1, EndTime, 0.5, 0.45);

            
            P2ParticleEffect(250, 0.4, t1, EndTime);
            
            OsbSprite foreground = Layer.CreateSprite("sb/bg/bg2fullfront.png", OsbOrigin.Centre);
            foreground.Fade(t1, 1);
            foreground.Fade(EndTime, 0);
            foreground.Scale(OsbEasing.OutSine, t1, EndTime, 0.55, 0.45);

            OsbSprite girl = Layer.CreateSprite("sb/girl/character_unsaturated.png", OsbOrigin.Centre);
            girl.Fade(t1, 1);
            girl.Fade(EndTime, 0);
            girl.Scale(OsbEasing.OutSine, t1, EndTime, 0.6, 0.45);
            girl.MoveY(t1, 360);

            P2ParticleEffect(250, 0.4, t1, EndTime);

            OsbAnimation noise = Layer.CreateAnimation("sb/noise/fc/n.jpg", 8, BeatDuration*0.5, OsbLoopType.LoopForever);
            noise.Fade(t1, 0.1);
            noise.Fade(EndTime, 0);
            OsbSprite cover = Layer.CreateSprite("sb/1px.png", OsbOrigin.Centre);
            cover.Fade(t1, 0.2);
            cover.Fade(EndTime, 0);
            cover.ScaleVec(t1, 854, 480);
            cover.Color(t1, Color4.Black);

            
        }
        internal void CreateFlash()
        {
            flash = Layer.CreateSprite("sb/1px.png", OsbOrigin.Centre);
            flash.ScaleVec(StartTime, 854, 480);
            flash.Fade(StartTime, 0);
            flash.Fade(EndTime, 0);
        }
        internal void Flash(double start, double end, double startOpacity, double endOpacity)
        {
            flash.Fade(start, end, startOpacity, endOpacity);
        }
        internal void FlashBling(Vector2 pos, double start,double end, double rotation)
        {
            bling.Move(start, pos);
            bling.Fade(OsbEasing.OutSine, start, end, 1, 0);
            bling.Rotate(start, rotation);
        }
        internal void CreateBling()
        {
            bling = Layer.CreateSprite("sb/star.png", OsbOrigin.Centre);
            bling.Scale(StartTime, 0.1);
            bling.Fade(StartTime, 0);
            bling.Fade(EndTime, 0);
        }
        internal void CreateCover()
        {
            cover = Layer.CreateSprite("sb/1px.png", OsbOrigin.Centre);
            cover.ScaleVec(StartTime, 854, 480);
            cover.Color(StartTime, Color4.Black);
            cover.Fade(EndTime, 0);
        }
        internal void FadeInInstant(double start, double end, double dOpacity)
        {
            cover.Fade(start, start, 1, 1 - dOpacity);
            cover.Fade(end, end, 1 - dOpacity, 1);
        }
        internal void FadeIn(double start, double end, double dOpacity)
        {
            
            cover.Fade(dOpacity != 1 ? OsbEasing.InBounce : OsbEasing.InSine, start, start + BeatDuration, 1, 1 - dOpacity);
            cover.Fade(dOpacity != 1 ? OsbEasing.InBounce : OsbEasing.InSine, end - BeatDuration, end, 1 - dOpacity, 1);
            
        }
        internal void CreateFadeinSprites()
        {
            fadeinSprites = new List<OsbSprite>();
            OsbSprite bg = Layer.CreateSprite("sb/bg/bg1sky.jpg", OsbOrigin.Centre);
            GiveDefaults(bg, 1, 0.5);

            OsbAnimation grunge = Layer.CreateAnimation("sb/grain/g.jpg", 10, BeatDuration*0.5, OsbLoopType.LoopForever);
            GiveDefaults(grunge, 0.5, 1);
            grunge.Additive(StartTime);
            OsbSprite mtn = Layer.CreateSprite("sb/bg/bg1mountains.png", OsbOrigin.Centre);
            GiveDefaults(mtn, 1, 0.5);

            OsbSprite fore = Layer.CreateSprite("sb/bg/bg1foreground.png", OsbOrigin.Centre);
            GiveDefaults(fore, 1, 0.5);

        }
        internal void GiveDefaults(OsbSprite a, double opacity, double scale)
        {
            a.Fade(StartTime, opacity);
            a.Fade(EndTime, 0);
            a.Scale(StartTime, scale);
        }
    }
}

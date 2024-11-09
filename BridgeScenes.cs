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

        OsbSprite cover, bling;
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
            bling.Scale(StartTime, 0.2);
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

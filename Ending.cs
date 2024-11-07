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
    public class Ending : StoryboardObjectGenerator
    {
        internal int StartTime = 309109;
        internal int FadeOutTime = 311071;
        internal int EndTime = 311994;
        internal StoryboardLayer Layer;
        public override void Generate()
        {
		    Layer = GetLayer("Ending");

            OsbSprite white = Layer.CreateSprite("sb/1px.png", OsbOrigin.Centre);
            white.Fade(StartTime, FadeOutTime, 1, 0.2);
            white.Fade(EndTime, 0);
            white.ScaleVec(StartTime, 854, 480);

            OsbSprite vignette = Layer.CreateSprite("sb/vignette.png", OsbOrigin.Centre);
            vignette.Fade(StartTime, 0.5);
            vignette.Fade(EndTime, 0);
            vignette.Scale(StartTime, 0.45);
            vignette.Color(StartTime, Color4.Black);

            OsbSprite whiteout = Layer.CreateSprite("sb/whiteout.png", OsbOrigin.Centre);
            whiteout.Scale(OsbEasing.OutSine, StartTime, FadeOutTime, 0.5, 0.45);
            whiteout.Fade(EndTime, EndTime, 1, 0);

            OsbSprite overlay = Layer.CreateSprite("sb/1px.png", OsbOrigin.Centre);
            overlay.Fade(FadeOutTime, EndTime, 0, 1);
            overlay.Color(FadeOutTime, Color4.Black);
            overlay.ScaleVec(FadeOutTime, 854, 480);

            OsbSprite owc = Layer.CreateSprite("sb/wc-diamond-2024.png", OsbOrigin.Centre);
            owc.Fade(EndTime, EndTime + Beatmap.GetTimingPointAt(EndTime).BeatDuration*4, 0, 1);
            owc.Scale(FadeOutTime, 0.5);
            owc.Fade(AudioDuration - Beatmap.GetTimingPointAt(EndTime).BeatDuration, AudioDuration, 1, 0);

            
        }
    }
}

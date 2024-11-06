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
        public override void Generate()
        {
            BeatDuration = Beatmap.GetTimingPointAt(StartTime).BeatDuration;
		    if (EndTime == 0 || StartTime == 0) throw new Exception();

            Layer = GetLayer($"Instrumental Scene ({StartTime} to {EndTime})");

            #region Background

            OsbSprite bg = Layer.CreateSprite("sb/bg/bg1background.png", OsbOrigin.Centre);
            bg.Fade(StartTime, 1);
            bg.Fade(EndTime, 0);
            bg.Scale(StartTime, 0.5);
            bg.MoveX(OsbEasing.OutSine, StartTime, StartTime + BeatDuration*16, 310, 320);

            OsbSprite foreground = Layer.CreateSprite("sb/bg/bg1foreground.png", OsbOrigin.Centre);
            foreground.Fade(StartTime, 1);
            foreground.Fade(EndTime, 0);
            foreground.Scale(StartTime, 0.5);
            foreground.MoveX(OsbEasing.OutSine, StartTime, StartTime + BeatDuration*16, 300, 330);

            OsbSprite tree = Layer.CreateSprite("sb/bg/bg1fronttree.png", OsbOrigin.Centre);
            tree.Fade(StartTime, 1);
            tree.Fade(EndTime, 0);
            tree.Scale(StartTime, 0.5);
            tree.MoveX(OsbEasing.OutSine, StartTime, StartTime + BeatDuration*16, 230, 330);
            #endregion

            #region Overlays

            OsbAnimation noise = Layer.CreateAnimation("sb/noise/fc/n.jpg", 8, BeatDuration*0.25, OsbLoopType.LoopForever);
            noise.Fade(StartTime, 0.05);
            noise.Additive(StartTime);
            noise.Fade(EndTime, 0);

            OsbSprite brightness = Layer.CreateSprite("sb/1px.png", OsbOrigin.Centre);
            brightness.Fade(StartTime, 0.2);
            brightness.Fade(EndTime, 0);
            brightness.ScaleVec(StartTime, 854, 400);
            brightness.Color(StartTime, Color4.Black);
            #endregion
            #region Text Overlay
            if (IncludeTextOverlay)
            {
                OsbSprite logo = Layer.CreateSprite("sb/whiteout.png", OsbOrigin.Centre);
                logo.Scale(OsbEasing.OutCirc, StartTime, StartTime + BeatDuration, 0.6, 0.5);
                logo.Scale(OsbEasing.OutSine, StartTime + BeatDuration, StartTime + BeatDuration*8, 0.5, 0.45);
                logo.Additive(StartTime);
                logo.Fade(StartTime + BeatDuration*32, StartTime + BeatDuration*32, 1, 0);
            }
            #endregion
        }
    }
}

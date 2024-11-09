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
    public class VocalsScenes : StoryboardObjectGenerator
    {
        [Configurable]
        public int StartTime = 0;
        [Configurable]
        public int EndTime = 0;

        internal StoryboardLayer Layer;
        internal double BeatDuration;
        public override void Generate()
        {
		    if (StartTime == 0 || EndTime == 0 || StartTime == EndTime) throw new Exception();
            BeatDuration = Beatmap.GetTimingPointAt(StartTime).BeatDuration;
            Layer = GetLayer($"Vocals Scenes ({StartTime} - {EndTime})");


            BlackAndWhiteScenes(StartTime, StartTime + BeatDuration*16, false);
            BlackAndWhiteScenes(StartTime + BeatDuration*32, StartTime + BeatDuration*48, true);

            
        }
        internal void BlackAndWhiteScenes(double start, double end, bool flip)
        {
            int dx = 20;
            OsbSprite sky = Layer.CreateSprite("sb/bg/bw/bg1sky.png", OsbOrigin.Centre);
            sky.Fade(start, 1);
            sky.Fade(end, 0);
            sky.Scale(start, 0.5);
            sky.MoveX(OsbEasing.OutSine, start, end, flip ? 320 : 320 + dx, flip ? 320 + dx : 320);
            if (flip) sky.FlipH(start);
            OsbSprite bg = Layer.CreateSprite("sb/bg/bw/bg1background.png", OsbOrigin.Centre);
            bg.Fade(start, 1);
            bg.Fade(end, 0);
            bg.Scale(start, 0.55);
            bg.MoveX(OsbEasing.OutSine, start, end, flip ? 320 : 320 + dx*3, flip ? 320 + dx*3 : 320);
            if (flip) bg.FlipH(start);

            OsbSprite haze = Layer.CreateSprite("sb/bg/haze.png", OsbOrigin.Centre);
            haze.Fade(start, 1);
            haze.Fade(end, 0);
            haze.Scale(start, 0.55);
            haze.MoveX(OsbEasing.OutSine, start, end, flip ? 320 : 320 + dx*4, flip ? 320 + dx*4 : 320);
            if (flip) haze.FlipH(start);

            OsbSprite girl = Layer.CreateSprite("sb/girl/character_bw.png", OsbOrigin.Centre);
            girl.Fade(start, 1);
            girl.Fade(end, 0);
            girl.Scale(start, 0.2);
            girl.MoveX(OsbEasing.OutSine, start, end, flip ? 320 : 320 + dx*4.5, flip ? 320 + dx*4.5 : 320);
            if (flip) girl.FlipH(start);
            
            OsbSprite tree = Layer.CreateSprite("sb/bg/bw/bg1fronttree.png", OsbOrigin.Centre);
            tree.Fade(start, 1);
            tree.Fade(end, 0);
            tree.Scale(start, 0.59);
            tree.MoveX(OsbEasing.OutSine, start, end, flip ? 280: 480 + dx*5.5, flip ? 280 + dx*5.5 : 480);

            

            OsbSprite vignette = Layer.CreateSprite("sb/vignette.png", OsbOrigin.Centre);
            vignette.Fade(start, 0.8);
            vignette.Fade(end, 0);
            vignette.Color(start, Color4.Black);

            OsbAnimation noise = Layer.CreateAnimation("sb/noise/mono/n.jpg", 8, BeatDuration*0.5, OsbLoopType.LoopForever);
            noise.Fade(start, 0.05);
            noise.Fade(end, 0);

            OsbSprite overlay = Layer.CreateSprite("sb/1px.png", OsbOrigin.Centre);
            overlay.Fade(start, start + BeatDuration*4, 0.7, 0);
            overlay.ScaleVec(start, 854, 480);
            overlay.Fade(end - BeatDuration*1.5, end, 0, 0.4);


        }
    }
}

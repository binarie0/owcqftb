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
    public class MidSongScene : StoryboardObjectGenerator
    {
        internal int StartTime = 146994, EndTime = 168917, MidTime = 154148;
        internal StoryboardLayer Layer, GirlLayer;
        internal double BeatDuration;

        public override void Generate()
        {
            Layer = GetLayer("Mid Song Scene");
            GirlLayer = GetLayer("Mid Song Scene - Girl Overlay");
            BeatDuration = Beatmap.GetTimingPointAt(StartTime).BeatDuration;

            OsbSprite bg = Layer.CreateSprite("sb/bg/blur/bg1background.jpg", OsbOrigin.Centre);
            bg.Scale(StartTime, 0.5);
            bg.Fade(StartTime, 0.8);
            bg.Fade(MidTime, 0);

            OsbSprite flare = Layer.CreateSprite("sb/flares/1.png", OsbOrigin.Centre);
            flare.Fade(StartTime, 0.4);
            flare.Scale(StartTime, 0.45);
            flare.FlipH(StartTime);
            flare.Fade(MidTime, 0);

            OsbAnimation anim = Layer.CreateAnimation("sb/grain/g.jpg", 10, BeatDuration*0.5, OsbLoopType.LoopForever);
            anim.Fade(StartTime, 0.2);
            anim.Fade(MidTime, 0);

            
            OsbSprite girl = GirlLayer.CreateSprite("sb/girl/character_withhand.png", OsbOrigin.CentreLeft);
            girl.Move(OsbEasing.OutCirc, StartTime, StartTime + BeatDuration*8, -127, 240, -107, 240);
            girl.Scale(StartTime, 0.3);
            girl.Fade(StartTime, 1);
            girl.Fade(MidTime, 0);

        }
    }
}

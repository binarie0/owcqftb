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
    public class Scene1 : StoryboardObjectGenerator
    {
        internal StoryboardLayer Layer;

        [Configurable(DisplayName = "Start Time")]
        public int StartTime = 0;

        [Configurable(DisplayName = "End Time")]
        public int EndTime = 0;

        public override void Generate()
        {
            if (StartTime == EndTime) return;
            float frameAnim = (float)Beatmap.GetTimingPointAt(StartTime).BeatDuration*0.5f;
            Layer = GetLayer("Scene 1");



            // OVERLAYS

            #region Overlays

            OsbSprite flare1 = Layer.CreateSprite("sb/flares/1.png", OsbOrigin.Centre);
            flare1.Fade(StartTime, 0.1);
            flare1.Additive(StartTime);
            flare1.Fade(EndTime, 0);
            flare1.Scale(StartTime, 0.5);
            flare1.MoveX(StartTime, EndTime, 320, 300);

            OsbSprite flare2 = Layer.CreateSprite("sb/flares/2.png", OsbOrigin.Centre);
            flare2.StartLoopGroup(StartTime, 9);
                flare2.Fade(0, (EndTime - StartTime) / 18, 0.04, 0.08);
                flare2.Fade((EndTime - StartTime) / 12, (EndTime - StartTime) / 9, 0.10, 0.06);
            flare2.EndGroup();

            flare2.StartLoopGroup(StartTime, 4);
                flare2.Color(0, (EndTime - StartTime) / 6, Color4.White, Color4.Red);
                flare2.Color((EndTime - StartTime) / 6, (EndTime - StartTime) / 4, Color4.Red, Color4.Aqua);
            flare2.EndGroup();
            
            flare2.Additive(StartTime);
            flare2.Scale(StartTime, EndTime, 0.45, 0.5);
            flare2.MoveX(StartTime, EndTime, 310, 335);
            flare2.MoveY(StartTime, EndTime, 220, 240);
            OsbAnimation noise = Layer.CreateAnimation("sb/noise/mono/n.jpg", 8, 
                        frameAnim, OsbLoopType.LoopForever);
            
            noise.Fade(StartTime, 0.05);
            noise.Fade(9225, EndTime, 0.05, 0.1);
            noise.Additive(StartTime);

            OsbAnimation scratches = Layer.CreateAnimation("sb/grain/g.jpg", 10,
                        frameAnim, OsbLoopType.LoopForever);
            
            scratches.Fade(StartTime, 0.4);
            scratches.Fade(EndTime, 0);
            scratches.Additive(StartTime);

            #endregion
            
            OsbSprite owclogo = Layer.CreateSprite("sb/wc-diamond-2024.png", OsbOrigin.Centre);
            owclogo.Scale(OsbEasing.OutSine, 9225, 12917, 0.4, 0.5);
            owclogo.Fade(EndTime, EndTime, 1, 0);
            
        }
    }
}

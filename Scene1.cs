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
        public int StartTime = 1840;

        [Configurable(DisplayName = "End Time")]
        public int EndTime = 16494;

        internal int TransitionTime = 14763;

        internal int BGChangeTime = 9225;

        
        public override void Generate()
        {
            //frame animation
            float frameAnim = (float)Beatmap.GetTimingPointAt(StartTime).BeatDuration*0.5f;
            //instantiate layer
            Layer = GetLayer("Scene 1");
            #region Circle Animation
            int[] times = {14763, 15110, 15456};
            double scale = 0.4;
            foreach (int time in times)
            {
                
                OsbSprite outercirc = Layer.CreateSprite("sb/circ.png", OsbOrigin.Centre);
                outercirc.Scale(OsbEasing.OutCirc, time, time + frameAnim*4, 0, scale);
                outercirc.Fade(time, 1);
                outercirc.Fade(time + frameAnim*4, 0);

                OsbSprite innercirc = Layer.CreateSprite("sb/circ.png", OsbOrigin.Centre);
                innercirc.Scale(OsbEasing.OutCirc, time + 50, time + frameAnim*4 + 50, 0, scale);
                innercirc.Fade(time, 1);
                innercirc.Fade(time + frameAnim*4, 0);
                innercirc.Color(time, Color4.Black);



                scale += 0.2;
            }
            
            #endregion

            #region Background
            OsbSprite bg = Layer.CreateSprite("sb/bg/background.jpg", OsbOrigin.Centre);
            bg.Fade(StartTime, 1);
            bg.Fade(BGChangeTime, 0);
            bg.Scale(OsbEasing.OutSine, StartTime, 5533, 0.5, 0.45);
            bg.MoveX(OsbEasing.OutCirc, StartTime, 5533, 370, 320); 

            bg = Layer.CreateSprite("sb/bg/background2.jpg", OsbOrigin.Centre);
            bg.Fade(BGChangeTime, 0.6);
            bg.Fade(EndTime, 0);
            bg.Scale(OsbEasing.OutSine, BGChangeTime, 12917, 0.6, 0.5);
            bg.Additive(BGChangeTime);

            bg.Scale(OsbEasing.OutSine, TransitionTime, 15687, 0.5, 0.55);

            OsbSprite trees = Layer.CreateSprite("sb/bg/background2trees.png", OsbOrigin.Centre);
            trees.Fade(BGChangeTime, 1);
            trees.Fade(EndTime, 0);
            trees.Scale(OsbEasing.OutSine, BGChangeTime, 12917, 0.6, 0.45);

            trees.Scale(OsbEasing.OutSine, TransitionTime, 15687, 0.45, 0.5);

            OsbSprite haze = Layer.CreateSprite("sb/bg/haze.png", OsbOrigin.Centre);
            haze.Fade(StartTime, Random(0.6, 1));
            haze.Scale(StartTime, 0.5);
            haze.Scale(BGChangeTime, 0);
            int i = StartTime, i2;
            float dO;
            while (i < BGChangeTime)
            {
                dO = Random(0f, 0.1f);
                i2 = Random(1000, 1500);
                if (haze.OpacityAt(i) > 0.8) dO *= -1;

                haze.Fade(i, i + i2, haze.OpacityAt(i), haze.OpacityAt(i) + dO);

                i += i2;
            }

            #endregion

            #region Particle Effects

            double x, dx, mediandx = 250, deviationx = 80;
            int dTime;
            for (int p = 0; p < 320; p++)
            {
                dTime = Random(1200, 1800);
                dx = Random(mediandx - deviationx, mediandx + deviationx);

                x = Random(-107, 747);
                OsbSprite particle = Layer.CreateSprite("sb/p.png", OsbOrigin.Centre);
                 

                particle.Fade(StartTime, Random(0.1, 1));
                particle.Fade(BGChangeTime, 0);
                particle.Scale(StartTime, Random(0.04, 0.15));
                particle.StartLoopGroup(StartTime, (BGChangeTime - StartTime) / dTime + 1);
                    particle.Move(0, dTime, x, 0, x + dx, 480);
                particle.EndGroup();
            }
            #endregion

            // OVERLAYS

            #region Overlays

            OsbSprite flare1 = Layer.CreateSprite("sb/flares/1.png", OsbOrigin.Centre);
            flare1.Fade(StartTime, 0.3);
            flare1.Additive(StartTime);
            flare1.Fade(EndTime, 0);
            flare1.Scale(StartTime, 0.5);
            flare1.MoveX(StartTime, TransitionTime, 320, 300);

            OsbSprite flare2 = Layer.CreateSprite("sb/flares/2.png", OsbOrigin.Centre);
            flare2.StartLoopGroup(StartTime, 9);
                flare2.Fade(0, (EndTime - StartTime) / 18, 0.54, 0.58);
                flare2.Fade((EndTime - StartTime) / 12, (EndTime - StartTime) / 9, 0.60, 0.56);
            flare2.EndGroup();

            flare2.StartLoopGroup(StartTime, 4);
                flare2.Color(0, (EndTime - StartTime) / 6, Color4.White, Color4.Red);
                flare2.Color((EndTime - StartTime) / 6, (EndTime - StartTime) / 4, Color4.Red, Color4.Aqua);
            flare2.EndGroup();
            
            flare2.Additive(StartTime);
            flare2.Scale(StartTime, TransitionTime, 0.45, 0.5);
            flare2.MoveX(StartTime, TransitionTime, 310, 335);
            flare2.MoveY(StartTime, TransitionTime, 220, 240);
            OsbAnimation noise = Layer.CreateAnimation("sb/noise/mono/n.jpg", 8, 
                        frameAnim, OsbLoopType.LoopForever);
            
            noise.Fade(StartTime, 0.05);
            noise.Fade(BGChangeTime, TransitionTime, 0.05, 0.1);
            noise.Additive(StartTime);
            noise.Fade(EndTime, 0);

            OsbAnimation scratches = Layer.CreateAnimation("sb/grain/g.jpg", 10,
                        frameAnim, OsbLoopType.LoopForever);
            
            scratches.Fade(StartTime, 0.4);
            scratches.Fade(EndTime, 0);
            scratches.Additive(StartTime);
            

            #endregion
            
            #region Transition
            OsbSprite left = Layer.CreateSprite("sb/1px.png", OsbOrigin.CentreLeft, new Vector2(-107, 240));
            left.ScaleVec(OsbEasing.OutCirc, 15456, 15687, 0, 480, 854/2, 480);
            left.Color(15456, Color4.Black);
            left.Fade(EndTime, EndTime, 1, 0);

            OsbSprite right = Layer.CreateSprite("sb/1px.png", OsbOrigin.CentreRight, new Vector2(747, 240));
            right.ScaleVec(OsbEasing.OutCirc, 15456, 15687, 0, 480, 854/2, 480);
            right.Color(15456, Color4.Black);
            right.Fade(EndTime, EndTime, 1, 0);

            OsbAnimation overlay = Layer.CreateAnimation("sb/grain2/g.jpg", 6, 15802 - 15687, OsbLoopType.LoopForever);
            overlay.Fade(15687, 1);
            overlay.Fade(EndTime, 0);
            overlay.Scale(16263, 16263, 1, 0);

            OsbSprite overlaypause = Layer.CreateSprite(overlay.GetTexturePathAt(16263));
            overlaypause.Fade(16263, 1);
            overlaypause.Fade(EndTime, 0);
            

            #endregion
            #region OWC Logo
            OsbSprite owclogo = Layer.CreateSprite("sb/wc-diamond-2024.png", OsbOrigin.Centre);
            owclogo.Scale(OsbEasing.OutSine, BGChangeTime, 12917, 0.4, 0.5);
            owclogo.Fade(13840, TransitionTime, 1, 0);

            #endregion
            
        }
    }
}

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

        [Configurable]
        public bool IncludeEndScene = true;

        internal StoryboardLayer Layer;
        internal double BeatDuration;
        public override void Generate()
        {
		    if (StartTime == 0 || EndTime == 0 || StartTime == EndTime) throw new Exception();
            BeatDuration = Beatmap.GetTimingPointAt(StartTime).BeatDuration;
            Layer = GetLayer($"Vocals Scenes ({StartTime} - {EndTime})");

            double t1 = StartTime + BeatDuration*16,
                    t2 = StartTime + BeatDuration*32,
                    t3 = StartTime + BeatDuration*48,
                    t4 = StartTime + BeatDuration*(64 + (IncludeEndScene ? 0.5:0));
            BlackAndWhiteScenes(StartTime, t1, false);
            ColorScenes(t1, t2, false);
            BlackAndWhiteScenes(t2, t3, true);
            ColorScenes(t3, t4, !IncludeEndScene, !IncludeEndScene);
            if (IncludeEndScene) EndingScene(t4, EndTime);
            
        }
        internal void EndingScene(double start, double end)
        {
            double frameAnim = BeatDuration*0.5;
            double[] times = {start, start + BeatDuration*1.5, start + BeatDuration*3};
            double scale = 0.4;
            foreach (double time in times)
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

            OsbSprite bg = Layer.CreateSprite("sb/bg/bg1sky.jpg", OsbOrigin.Centre);
            bg.Fade(start, 1);
            bg.Fade(end, 0);
            bg.Scale(start, 0.5);
            bg.Additive(start);
            
            EndParticles(start, end, 50);

            OsbSprite foreground = Layer.CreateSprite("sb/bg/bg2fullfront.png", OsbOrigin.Centre);
            foreground.Fade(start, 1);
            foreground.Fade(end, 0);
            foreground.Scale(OsbEasing.OutSine, start, end, 0.5, 0.55);

            EndParticles(start, end, 30);

            OsbSprite overlay = Layer.CreateSprite("sb/1px.png", OsbOrigin.Centre);
            overlay.Fade(start, start + BeatDuration*1.5, 0.1, 0);
            overlay.Fade(start + BeatDuration*1.5, start + BeatDuration*3, 0.1, 0);
            overlay.Fade(start + BeatDuration*3, start + BeatDuration*6, 0.4, 0);
            overlay.Fade(start + BeatDuration*6, end, 0, 0.3);
            overlay.ScaleVec(start, 854, 480);
        }
        internal void ColorScenes(double start, double end, bool noflashatend, bool stationary = false)
        {
            double swaptime = start + BeatDuration*8;
            double dscale = 0.1, scale = 0.45;

            //p1
            OsbSprite bg = Layer.CreateSprite("sb/bg/bg1sky.jpg", OsbOrigin.Centre);
            bg.Fade(start, 1);
            bg.Fade(swaptime, 0);
            
            bg.Scale(OsbEasing.OutSine, start, swaptime, scale, scale + dscale*0.25);

            OsbSprite mtn = Layer.CreateSprite("sb/bg/bg1mountains.png", OsbOrigin.Centre);
            mtn.Fade(start, 1);
            mtn.Fade(swaptime, 0);
            mtn.Scale(OsbEasing.OutSine, start, swaptime, scale, scale + dscale*0.6);

            ColorParticles(start, swaptime, 80);
            OsbSprite foreground = Layer.CreateSprite("sb/bg/bg1foreground.png", OsbOrigin.Centre);
            foreground.Fade(start, 1);
            foreground.Fade(swaptime, 0);
            foreground.Scale(OsbEasing.OutSine, start, swaptime, scale, scale + dscale);

            ColorParticles(start, swaptime, 60);
            OsbSprite cover = Layer.CreateSprite("sb/1px.png", OsbOrigin.Centre);
            cover.Fade(start, 0.2);
            cover.Fade(end, 0);
            cover.ScaleVec(start, 854, 480);
            cover.Color(start, Color4.Black);
            //p2

            OsbSprite back = Layer.CreateSprite("sb/bg/blur/bg1background.jpg", OsbOrigin.Centre);
            back.Fade(swaptime, 1);
            back.Fade(end, 0);
            back.Scale(swaptime, scale + dscale);
            back.Rotate(swaptime, -0.1);
            if (stationary)
            {
                back.MoveX(swaptime, 320);
            }
            else
            {
                back.MoveX(OsbEasing.OutSine, swaptime, end, 380, 300);
            }

            OsbSprite vignette = Layer.CreateSprite("sb/vignette.png", OsbOrigin.Centre);
            vignette.Fade(swaptime, 0.4);
            vignette.Fade(end, 0);
            vignette.Scale(swaptime, 0.5);
            vignette.Color(swaptime, Color4.Black);

            OsbAnimation anim = Layer.CreateAnimation("sb/grain/g.jpg", 10, BeatDuration*0.5, OsbLoopType.LoopForever);
            anim.Fade(swaptime, 0.1);
            anim.Fade(end, 0);

            if (stationary)
            {
                OsbSprite noise = Layer.CreateAnimation("sb/noise/fc/n.jpg", 8, BeatDuration*0.25, OsbLoopType.LoopForever);
                noise.Fade(swaptime, 0.2);
                noise.Fade(end, 0);
                noise.Additive(swaptime);
            }
            
            OsbSprite girl = Layer.CreateSprite("sb/girl/character_full.png", OsbOrigin.Centre);
            girl.Fade(swaptime, 0.8);
            girl.Fade(end, 0);
            girl.Scale(swaptime, 0.5);
            if (!stationary)
            {
                girl.MoveX(OsbEasing.OutSine, swaptime, end, 340, 300);
            }
            
            
            



            OsbSprite overlay = Layer.CreateSprite("sb/1px.png", OsbOrigin.Centre);
            overlay.Fade(start, start + BeatDuration*4, 0.7, 0);
            overlay.Fade(swaptime, swaptime + BeatDuration*2, 0.3, 0);
            overlay.ScaleVec(start, 854, 480);
            if (!noflashatend) overlay.Fade(end - BeatDuration*1.5, end, 0, 0.4);
            else
            {
                overlay.Color(end - BeatDuration*1.5, end - BeatDuration*1.5, Color4.White, Color4.Black);
                overlay.Fade(end - BeatDuration*1.5, end, 0, 1);
            }
        }

        private void ColorParticles(double start, double end, int ct)
        {
            double x, dx, scale;
            int time;
            for (int i = 0; i < ct; i++)
            {   
                scale = Random(0.05, 0.15);
                time = Random(1000, 3000);
                x = Random(-157, 747);
                dx = Random(50, 150);
                OsbSprite p = Layer.CreateSprite("sb/p.png", OsbOrigin.Centre);
                p.ScaleVec(start, scale*Random(0.1, 0.4), scale);
                p.Fade(start, start, 0, Random(0.5, 1));
                p.Fade(end, 0);
                p.StartLoopGroup(start - Random(0, time), (int)((end - start) / time) + 2);
                    p.Move(0, time, x, 0, x + dx, 480);
                p.EndGroup();
                p.Additive(start);

            }
        }

        private void EndParticles(double start, double end, int ct)
        {
            double x, dx, scale;
            int time;
            for (int i = 0; i < ct; i++)
            {
                x = Random(-127, 767);
                dx = Random(-20, 20);
                scale = Random(0.07, 0.1);
                time = Random(2000, 4000);

                OsbSprite p = Layer.CreateSprite("sb/p.png", OsbOrigin.Centre);
                p.Scale(start, scale);
                p.Fade(start, start, 0, Random(0.5, 1));
                p.Fade(end, 0);
                p.StartLoopGroup(start - Random(0, time), (int)((end - start) / time) + 2);
                    p.Move(0, time, x, 0, x + dx, 480);
                p.EndGroup();
                p.Additive(start);
            }
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
            girl.MoveX(OsbEasing.OutSine, start, end, flip ? 220 : 320 + dx*4.5, flip ? 220 + dx*4.5 : 320);
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

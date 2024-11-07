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
    public class TickingClockScene : StoryboardObjectGenerator
    {
        internal int StartTime = 168917, EndTime = 183687, MidTime = 176302;
        internal double BeatDuration;
        internal StoryboardLayer Layer;
        public override void Generate()
        {
            BeatDuration = Beatmap.GetTimingPointAt(StartTime).BeatDuration;
		    Layer = GetLayer("Ticking Clock Scene");

            //background
            OsbSprite color = Layer.CreateSprite("sb/bg/bg1color.png", OsbOrigin.Centre);
            color.Fade(StartTime, 0.23);
            color.Fade(EndTime, 0);
            color.Scale(StartTime, 0.45);

            
            OsbSprite overlay = Layer.CreateAnimation("sb/grain2/g.jpg", 6, BeatDuration, OsbLoopType.LoopForever);
            overlay.Fade(StartTime, 0.1);
            overlay.Fade(EndTime, 0);
            overlay.Additive(StartTime);
            
            //background clock

            Vector2 pos = new Vector2(320, 400);
            OsbSprite minute = Layer.CreateSprite("sb/clockhands/minute.png", OsbOrigin.BottomCentre, pos);
            minute.Fade(StartTime, 1);
            minute.Fade(EndTime, 0);
            minute.Scale(StartTime, 0.2);
            minute.Color(StartTime, 0.6, 0.6, 0.6);
            minute.Rotate(StartTime, -(2*Math.PI/60) * 4);
            
            

            OsbSprite hour = Layer.CreateSprite("sb/clockhands/hour.png", OsbOrigin.BottomCentre, pos);
            hour.Fade(StartTime, 1);
            hour.Fade(EndTime, 0);
            hour.Scale(StartTime, 0.2);
            hour.Color(StartTime, 0.8, 0.8, 0.8);
            hour.Rotate(StartTime, MidTime, -0.02, 0.02);

            OsbSprite grad = Layer.CreateSprite("sb/grad.png", OsbOrigin.Centre);
            grad.Fade(StartTime, 0.2);
            grad.ScaleVec(StartTime, 854, 2 );
            //grad.Fade(EndTime, 0);
            grad.ScaleVec(EndTime, EndTime, 854, 2, 0, 0);

            double minuterotation, hourrotation;
            //same time can be used for grad and minute hand
            for (double time = 169379; time < MidTime; time += BeatDuration*4)
            {
                minute.Rotate(time, time, minute.RotationAt(time), minute.RotationAt(time) + 2*Math.PI / 60.0d);
            }
            minuterotation = minute.RotationAt(MidTime);
            minute.Rotate(OsbEasing.InCirc, MidTime, EndTime, minuterotation, minuterotation - 10.5*Math.PI);
            hour.Rotate(OsbEasing.InCirc, MidTime, EndTime, hour.RotationAt(MidTime), hour.RotationAt(MidTime) - 10.5*(2*Math.PI / 60.0d));

            for (double time = 169379; time < MidTime; time += BeatDuration*4)
            {
                grad.Fade(time - BeatDuration*0.5, time, 0.2, 0.3);
                grad.Fade(time, time + BeatDuration*3.5, 0.3, 0.2);
            }
            for (double time = MidTime + BeatDuration*2; time < EndTime; time += BeatDuration*2)
            {
                grad.Fade(time - BeatDuration*0.25, time, 0.2, 0.5);
                grad.Fade(time, time + BeatDuration*1.75, 0.5, 0.2);
            }
            

            //foreground

            OsbSprite haze = Layer.CreateSprite("sb/bg/haze.png", OsbOrigin.Centre);
            haze.Fade(StartTime, 0.2);
            haze.Fade(EndTime, 0);
            haze.Scale(StartTime, 0.45);

            Particles(120, 0.03, 0.06, 0.2);
            OsbSprite foreground = Layer.CreateSprite("sb/bg/bg2fullfront.png", OsbOrigin.Centre);
            foreground.Fade(StartTime, 0.4);
            foreground.Fade(EndTime, 0);
            foreground.Scale(StartTime, 0.45);

            foreground.Scale(OsbEasing.InSine, EndTime - BeatDuration*2, EndTime, 0.45, 0.48);

            foreground = Layer.CreateSprite("sb/bg/bg2fullfront.png", OsbOrigin.Centre);
            foreground.Fade(StartTime, 1);
            foreground.Fade(EndTime, 0);
            foreground.Scale(StartTime, 0.48);

            foreground.Scale(OsbEasing.InSine, EndTime - BeatDuration*2, EndTime, 0.48, 0.52);

            

            //particles
            Particles(60, 0.01, 0.03, 1);

            //overlays
            
            OsbSprite vignette = Layer.CreateSprite("sb/vignette.png", OsbOrigin.Centre);
            vignette.Fade(StartTime, 0.5);
            vignette.Fade(EndTime, 0);
            vignette.Scale(StartTime, 0.45);
            vignette.Color(StartTime, Color4.Black);



            OsbSprite fadein = Layer.CreateSprite("sb/1px.png", OsbOrigin.Centre);
            fadein.Fade(StartTime, StartTime + BeatDuration*8, 1, 0);
            fadein.ScaleVec(StartTime, 854, 480);
            fadein.Color(StartTime, Color4.Black);

            fadein.Fade(EndTime - BeatDuration*2, EndTime, 0, 0.4);
            fadein.Color(EndTime - BeatDuration*2, Color4.White);
        }
    internal void Particles(int ct, double minscale, double maxscale, double opacity)
    {
        for (int i = 0; i < ct; i++)
            {
                OsbSprite p = Layer.CreateSprite("sb/p.png", OsbOrigin.Centre);
                p.Fade(StartTime, StartTime, 0, opacity);
                p.Scale(StartTime, Random(minscale, maxscale));
                p.Fade(EndTime, EndTime, opacity, 0);
                int x = Random(-107, 747);
                int time = Random(2000, 4000);
                p.StartLoopGroup(StartTime - Random(0, time), (EndTime - StartTime) / time + 2);
                    p.Move(0, time, x, 0, x + Random(-10, 10),480);
                p.EndGroup();
                
            }
    }
    }


}

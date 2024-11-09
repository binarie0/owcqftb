using OpenTK;
using OpenTK.Graphics;
using StorybrewCommon.Mapset;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;
using StorybrewCommon.Storyboarding.Commands;
using StorybrewCommon.Storyboarding.Util;
using StorybrewCommon.Subtitles;
using StorybrewCommon.Util;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StorybrewScripts
{
    public class AfterVocalsScenes : StoryboardObjectGenerator
    {
        [Configurable]
        public int StartTime = 0;
        [Configurable]
        public int EndTime = 0;

        internal double BeatDuration;

        internal StoryboardLayer Layer;
        public override void Generate()
        {
            
		    if (StartTime == 0 || EndTime == 0 || StartTime == EndTime) throw new Exception();

            BeatDuration = Beatmap.GetTimingPointAt(StartTime).BeatDuration;

            Layer = GetLayer($"After Vocals Scene ({StartTime} - {EndTime})");
            OsbSprite bg = Layer.CreateSprite("sb/bg/blur/bg1background.jpg");
            bg.Fade(StartTime, 1);
            bg.Color(StartTime, Color4.LightBlue);
            bg.Fade(EndTime, 0);
            bg.Scale(StartTime, EndTime, 0.5, 0.52);
            
            
            Vector2 pos = new Vector2(757, 10);
            double dAngle = 0.06;
            for (int i = 0; i < 28; i++)
            {
                OsbSprite line = Layer.CreateSprite("sb/1px.png", OsbOrigin.BottomLeft, pos);
                line.StartLoopGroup(StartTime - BeatDuration*i, 9);
                    line.Rotate(OsbEasing.InOutSine, 0, BeatDuration*4, Math.PI*0.5 + dAngle*i - 0.01, Math.PI*0.5 + dAngle*i + 0.01);
                    line.Rotate(OsbEasing.InOutSine, BeatDuration*4, BeatDuration*8, Math.PI*0.5 + dAngle*i + 0.01, Math.PI*0.5 + dAngle*i - 0.01);
                line.EndGroup();
                line.Fade(StartTime, StartTime, 0, 0.2);
                line.Fade(EndTime, 0);
                line.ScaleVec(StartTime, 12000, 1);
                
                
                
            }

            pos = new Vector2(-117, 490);
            for (int i = 0; i < 28; i++)
            {
                OsbSprite line = Layer.CreateSprite("sb/1px.png", OsbOrigin.BottomLeft, pos);
                line.StartLoopGroup(StartTime - BeatDuration*i*0.5, 9);
                    line.Rotate(OsbEasing.InOutSine, 0, BeatDuration*4, -Math.PI*0.5 + dAngle*i - 0.01, -Math.PI*0.5 + dAngle*i + 0.01);
                    line.Rotate(OsbEasing.InOutSine, BeatDuration*4, BeatDuration*8, -Math.PI*0.5 + dAngle*i + 0.01, -Math.PI*0.5 + dAngle*i - 0.01);
                line.EndGroup();
                line.Fade(StartTime, StartTime, 0, 0.2);


                line.Fade(EndTime, 0);
                line.ScaleVec(StartTime, 12000, 1);
            }

            OsbSprite vignette = Layer.CreateSprite("sb/vignette.png", OsbOrigin.Centre);
            vignette.Fade(StartTime, 0.6);
            vignette.Fade(EndTime, 0);
            vignette.Scale(StartTime, 0.5);
            vignette.Color(StartTime, Color4.Black);
            int time, mintime = 4000, maxtime = 8000, addtime;
            int x, y;
            for (int i = 0; i < 240; i++)
            {
                x = Random(-107, 747); y = Random(0, 480);
                OsbSprite p2 = Layer.CreateSprite("sb/bigp.png", OsbOrigin.Centre, new Vector2(x,y));
                OsbSprite p = Layer.CreateSprite("sb/p.png", OsbOrigin.Centre, new Vector2(x,y));
                time = StartTime - Random(mintime, maxtime);

                while (time < EndTime)
                {
                    addtime = Random(mintime, maxtime);
                    p.Move(OsbEasing.InOutSine, time, time + addtime, 
                                p.PositionAt(time).X, p.PositionAt(time).Y,
                                p.PositionAt(time).X + Random(-10, 10), p.PositionAt(time).Y + Random(-10, 10));
                    p2.Move(OsbEasing.InOutSine, time, time + addtime, p.PositionAt(time), p.PositionAt(time + addtime));
                    time += addtime;
                }
                p.Scale(StartTime, 0.1);
                p.Fade(StartTime, StartTime, 0, 1);
                p.Fade(EndTime, 0);
                p.Additive(StartTime);
                p2.Scale(StartTime, Random(0.04, 0.1));
                p2.Fade(StartTime, StartTime, 0, Random(0.1, 0.3));
                p2.Fade(EndTime, 0);
                p2.Additive(StartTime);




            }

            OsbAnimation grunge = Layer.CreateAnimation("sb/grain2/g.jpg", 6, BeatDuration*0.5, OsbLoopType.LoopForever);
            grunge.Fade(StartTime, 0.3);
            grunge.Fade(EndTime, 0);
            OsbSprite girl = Layer.CreateSprite("sb/girl/character_full.png", OsbOrigin.Centre);
            girl.Fade(StartTime, 1);
            girl.Fade(EndTime, 0);
            girl.Scale(StartTime, 0.5);
            
            

            OsbSprite flash = Layer.CreateSprite("sb/1px.png", OsbOrigin.Centre);
            flash.ScaleVec(StartTime, 854, 480);
            flash.Fade(StartTime, StartTime + BeatDuration*4, 0.4, 0);
        }
    }
}

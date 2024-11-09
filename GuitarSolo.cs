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
    public class GuitarSolo : StoryboardObjectGenerator
    {
        internal FontGenerator Generator;
        internal int StartTime = 183687, EndTime = 213225;
        List<OsuHitObject> CurrentObjects = new List<OsuHitObject>();
        internal double BeatDuration;
        internal StoryboardLayer Layer;
        public override void Generate()
        {
            Generator = LoadFont("sb/text/guitarsolo", new FontDescription()
            {
                FontPath = "bigletterfont.ttf",
                Color = Color4.Black,
                Padding = new Vector2(0, 0),
                FontSize = 100,
                TrimTransparency = true
            },
            new FontGlow()
            {
                Power = 1.0,
                Color = Color4.White,
                

            });
            Layer = GetLayer("Guitar Solo Scene");
            BeatDuration = Beatmap.GetTimingPointAt(StartTime).BeatDuration;
            //cache hitobjects
		    foreach (var HO in Beatmap.HitObjects)
            {
                if (HO.StartTime >= StartTime - 5 && HO.EndTime <= EndTime)
                {
                    CurrentObjects.Add(HO);
                }
            }

            //background
            OsbSprite bg = Layer.CreateSprite("sb/bg/background.jpg", OsbOrigin.Centre);
            bg.Fade(StartTime, 1);
            bg.Fade(EndTime, 0);
            bg.Scale(StartTime, 0.49);
            //bg.Scale(OsbEasing.OutSine, StartTime, StartTime + BeatDuration*4, 0.5, 0.45);
            bg.MoveX(OsbEasing.OutCirc, StartTime, StartTime + BeatDuration*4, 340, 320); 
            bg.MoveX(StartTime + BeatDuration*4, EndTime, 320, 310);

           

            OsbSprite girl = Layer.CreateSprite("sb/girl/character_full.png", OsbOrigin.Centre);
            girl.Fade(StartTime, 1);
            girl.Fade(StartTime, 0);
            girl.Scale(StartTime, 0.4);
            girl.MoveX(OsbEasing.OutSine, StartTime, StartTime + BeatDuration*4, 380, 360);
            girl.MoveX(StartTime + BeatDuration*4, EndTime, 360, 310);
            girl.FlipH(StartTime);


            OsbSprite tree = Layer.CreateSprite("sb/bg/bg1fronttree.png", OsbOrigin.Centre);
            tree.Fade(StartTime, 1);
            tree.Fade(EndTime, 0);
            tree.Scale(StartTime, 0.53);
            tree.MoveX(OsbEasing.OutSine, StartTime, StartTime + BeatDuration*4, 480, 400);
            tree.MoveX(StartTime + BeatDuration*4, EndTime, 400, 310);
            tree.FlipH(StartTime);

            

            OsbSprite dark = Layer.CreateSprite("sb/1px.png", OsbOrigin.Centre);
            dark.Fade(StartTime, 0.4);
            dark.Fade(EndTime, 0);
            dark.ScaleVec(StartTime, 854, 400);
            dark.Color(StartTime, Color4.Black);

             HitLighting();
            //overlay
            #region Text Overlays
            CreateScrollingText("WHITEOUT", 40, OsbOrigin.TopLeft, true);

            CreateScrollingText("GENKAKUARIA", 440, OsbOrigin.BottomRight, false);
            #endregion
             #region Overlays

            OsbSprite flare1 = Layer.CreateSprite("sb/flares/1.png", OsbOrigin.Centre);
            flare1.Fade(StartTime, 0.3);
            flare1.Additive(StartTime);
            flare1.Fade(EndTime, 0);
            flare1.Scale(StartTime, 0.5);
            flare1.MoveX(StartTime, EndTime, 320, 300);
        
            OsbSprite flare2 = Layer.CreateSprite("sb/flares/2.png", OsbOrigin.Centre);
            flare2.StartLoopGroup(StartTime, 9);
                flare2.Fade(0, (EndTime - StartTime) / 18, 0.54, 0.58);
                flare2.Fade((EndTime - StartTime) / 12, (EndTime - StartTime) / 9, 0.60, 0.56);
            flare2.EndGroup();
            flare2.FlipH(StartTime);

            flare2.StartLoopGroup(StartTime, 4);
                flare2.Color(0, (EndTime - StartTime) / 6, Color4.White, Color4.Red);
                flare2.Color((EndTime - StartTime) / 6, (EndTime - StartTime) / 4, Color4.Red, Color4.Aqua);
            flare2.EndGroup();
            
            flare2.Additive(StartTime);
            flare2.Scale(StartTime, EndTime, 0.47, 0.5);
            flare2.MoveX(StartTime, EndTime, 310, 335);
            flare2.MoveY(StartTime, EndTime, 220, 240);

            

            OsbAnimation noise = Layer.CreateAnimation("sb/noise/mono/n.jpg", 8, 
                        BeatDuration*0.5, OsbLoopType.LoopForever);
            
            noise.Fade(StartTime, 0.05);
            noise.Fade(EndTime, EndTime, 0.05, 0);
            noise.Additive(StartTime);
            noise.Fade(EndTime, 0);

            OsbAnimation scratches = Layer.CreateAnimation("sb/grain/g.jpg", 10,
                        BeatDuration*0.5, OsbLoopType.LoopForever);
            
            scratches.Fade(StartTime, 0.2);
            scratches.Fade(EndTime, 0);
            scratches.Additive(StartTime);
            
            
            #endregion
            
        }
        internal void HitLighting()
        {
            //hitlighting
            Vector2 pos;
            Color4 color = Color4.White;
            double time, scale = 0.15;
            foreach (var HO in CurrentObjects)
            {
                
                time = HO.StartTime;
                if (HO is OsuCircle)
                {
                    pos = HO.PositionAtTime(StartTime);
                    OsbSprite bigp = Layer.CreateSprite("sb/bigp.png", OsbOrigin.Centre);
                    bigp.Fade(HO.StartTime, HO.StartTime + BeatDuration*4, 0.2*2, 0);
                    bigp.Scale(HO.StartTime, scale);
                    bigp.Color(HO.StartTime, color);
                    bigp.Move(HO.StartTime, pos);
                }
                else
                {
                    for (time = HO.StartTime; time < HO.EndTime; time += BeatDuration/32d)
                    {
                        pos = HO.PositionAtTime(time);
                        OsbSprite bigp = Layer.CreateSprite("sb/bigp.png", OsbOrigin.Centre);
                        bigp.Fade(time, time + BeatDuration*4, 0.2, 0);
                        bigp.Scale(time, scale);
                        bigp.Color(time, color);
                        bigp.Move(time, pos);
                        //bigp.Additive(time);
                
                    }
                }
                
            }
        }
        internal void CreateScrollingText(string word, int height, OsbOrigin origin, bool toLeft)
        {
            double scale = 0.6;
            double x = toLeft ? 747 : -107;
            double time = 1000*word.Length;
            int loopct = (int)((EndTime - StartTime) / time + 1);
            FontTexture texture = Generator.GetTexture(word);
            double dx = texture.Width*scale;
            while (toLeft ? (x > -107 - dx) : (x < 747 + dx))
            {
                OsbSprite p = Layer.CreateSprite(texture.Path, origin);
                p.Fade(StartTime, 0.4);
                p.Fade(EndTime, 0);
                p.Additive(StartTime);
                p.StartLoopGroup(StartTime, loopct);
                    p.MoveX(0, time, x, toLeft ? x - dx : x + dx);
                p.EndGroup();
                p.Scale(StartTime, scale);
                p.MoveY(StartTime, height);
                x += toLeft ? -dx : dx;
            }

        }
    }
}

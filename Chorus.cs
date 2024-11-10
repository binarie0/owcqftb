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
using System.Net.NetworkInformation;

namespace StorybrewScripts
{
    public class Chorus : StoryboardObjectGenerator
    {
        static float ScreenWidth = OsuHitObject.WidescreenStoryboardSize.X;
        static float ScreenHeight = OsuHitObject.WidescreenStoryboardSize.Y;

        static float PlayfieldHeight = OsuHitObject.PlayfieldSize.Y;
        static float PlayfieldWidth = OsuHitObject.PlayfieldSize.X;

        static float ScreenBoundsLeft = OsuHitObject.WidescreenStoryboardBounds.Left;
        static float ScreenBoundsRight = OsuHitObject.WidescreenStoryboardBounds.Right;
        static float ScreenBoundsBottom = OsuHitObject.WidescreenStoryboardBounds.Bottom;
        static float ScreenBoundsTop = OsuHitObject.WidescreenStoryboardBounds.Top;

        static float PlayfieldBoundsTop = OsuHitObject.PlayfieldToStoryboardOffset.Y;
        static float PlayfieldBoundsBottom = PlayfieldHeight + OsuHitObject.PlayfieldToStoryboardOffset.Y;
        static float PlayfieldBoundsLeft = OsuHitObject.PlayfieldToStoryboardOffset.X;
        static float PlayfieldBoundsRight = PlayfieldWidth + OsuHitObject.PlayfieldToStoryboardOffset.X;
        public override void Generate()
        {
		    
            
            ChorusPart1(82956);
            
            RainbowParticle(82956, 90456);
            RainbowParticle(97725, 108917);

            fill1(95763);
            fill2(108917, 113417);
            ChorusPart1(255687, 1);

            fill1(268378);
            fill2(281532, 290648, true);
        }
        
        void fill2(int startTime, int endTime, bool includeNoise = false)
        {
            
            double BeatDuration = Beatmap.GetTimingPointAt(startTime).BeatDuration;
            StoryboardLayer layer = GetLayer("Chorus BACK");

            OsbSprite bg = layer.CreateSprite("sb/bg/background2.jpg", OsbOrigin.Centre);
            bg.Fade(startTime, 1);
            bg.Fade(endTime, 0);
            bg.Scale(startTime, 0.5);

            OsbAnimation grain = layer.CreateAnimation("sb/grain2/g.jpg", 6, BeatDuration*0.5, OsbLoopType.LoopForever);
            grain.Fade(startTime, 0.2);
            grain.Fade(endTime, 0);
            OsbSprite vignette = layer.CreateSprite("sb/vignette.png", OsbOrigin.Centre);
            vignette.Fade(startTime, 1);
            vignette.Fade(endTime, 0);
            vignette.Scale(startTime, 0.5);
            vignette.Color(startTime, Color4.Black);

            if (!includeNoise) return;
            OsbAnimation noise = layer.CreateAnimation("sb/noise/fc/n.jpg", 8, BeatDuration*0.25, OsbLoopType.LoopForever);
            noise.Fade(284301, 284417, 0.1, 0);
            noise.Fade(284417, 284417 + BeatDuration, 0.1, 0);
            noise.Fade(284648, 285224, 0.2, 0.2);

            OsbSprite cover = layer.CreateSprite("sb/1px.png", OsbOrigin.Centre);
            cover.Fade(289840, endTime, 0, 0.6);
            cover.ScaleVec(289840, 854, 480);
            
        }

        void fill1(int startTime){
            
            //Black screen sliding in 
            var checkpoint1 = startTime + 95994 - 95763;
            var checkpoint1point5 = startTime + 96456 - 95763;
            //1st crash
            var checkpoint2 = startTime + 96110 - 95763;
            //2nd crash
            var checkpoint3 = startTime + 96802 - 95763;
            //3rd crash
            var checkpoint4 = startTime + 97148 - 95763;
            var endpoint = startTime + 97725 - 95763;

            var layer = GetLayer("Chorus FRONT");
            var blackSlide = layer.CreateSprite("sb/1px.png", OsbOrigin.CentreLeft);
            blackSlide.MoveX(OsbEasing.OutCirc, startTime, checkpoint1, ScreenBoundsRight, ScreenBoundsLeft);
            blackSlide.ScaleVec(startTime, ScreenWidth, ScreenHeight);
            blackSlide.Color(startTime, Color4.Black);
            blackSlide.Fade(endpoint, endpoint + 500, 1, 0);
            blackSlide.Color(endpoint, Color4.White);
        

            var snow = layer.CreateAnimation("sb/snowflakes/snowflake.png", 25, 96225 - 96110, OsbLoopType.LoopForever, OsbOrigin.Centre);
            snow.Fade(checkpoint2, 1);
            snow.Fade(endpoint, 0);
            snow.Rotate(OsbEasing.OutCirc, checkpoint2, endpoint, Math.PI * 2, 0);
            snow.Scale(OsbEasing.OutBack,checkpoint2, checkpoint2 + 500, 0.4, 1);
            

            var puddle15 = layer.CreateSprite("sb/circ.png", OsbOrigin.Centre, new Vector2(Random(ScreenBoundsLeft, ScreenBoundsRight), Random(ScreenBoundsTop, ScreenBoundsBottom)));
            puddle15.Scale(OsbEasing.OutExpo, checkpoint1point5, checkpoint1point5 + 1000, 0, 0.4);
            puddle15.Fade(checkpoint1point5, 0.6);
            puddle15.Additive(checkpoint1point5, endpoint);
            puddle15.Color(checkpoint1point5, Color4.SkyBlue);


            var puddle1 = layer.CreateSprite("sb/circ.png", OsbOrigin.Centre, new Vector2(Random(ScreenBoundsLeft, ScreenBoundsRight), Random(ScreenBoundsTop, ScreenBoundsBottom)));
            puddle1.Scale(OsbEasing.OutExpo, checkpoint2, checkpoint2 + 1000, 0, 0.4);
            puddle1.Fade(checkpoint2, 0.6);
            puddle1.Additive(checkpoint1, endpoint);
            puddle1.Color(checkpoint1, Color4.SkyBlue);

            var puddle2 = layer.CreateSprite("sb/circ.png", OsbOrigin.Centre, new Vector2(Random(ScreenBoundsLeft, ScreenBoundsRight), Random(ScreenBoundsTop, ScreenBoundsBottom)));
            puddle2.Scale(OsbEasing.OutExpo, checkpoint3, checkpoint3 + 1000, 0, 0.4);
            puddle2.Fade(checkpoint2, 0.6);
            puddle2.Additive(checkpoint2, endpoint);
            puddle2.Color(checkpoint2, Color4.SkyBlue);

            var puddle3 = layer.CreateSprite("sb/circ.png", OsbOrigin.Centre, new Vector2(Random(ScreenBoundsLeft, ScreenBoundsRight), Random(ScreenBoundsTop, ScreenBoundsBottom)));
            puddle3.Scale(OsbEasing.OutExpo, checkpoint4, endpoint, 0, 0.4);
            puddle3.Fade(checkpoint3, 0.6);
            puddle3.Additive(checkpoint3, endpoint);
            puddle3.Color(checkpoint3, Color4.SkyBlue);


            
            


        }
        void RainbowParticle(int startTime, int endTime){
            var layer = GetLayer("Chorus Particle");
            var delay = 0;
            for(int i = 0; i < 50; i ++){

                var particle = layer.CreateSprite("sb/p.png");

                var radius = Random(ScreenWidth / 2 / 4, ScreenWidth / 2);
                var startingPosX = 320 + radius;
                var endingPosX = 320 - radius;
                var lowestPosY = 240 + 60;
                var highestPosY = 240 + 60 - radius;

                var numOfLoops = Random(6, 10);
                var perLoop = (endTime - startTime) / numOfLoops;
                particle.StartLoopGroup(startTime + delay, numOfLoops);
                particle.Rotate(0, perLoop, 0, - Math.PI);
                particle.MoveX(OsbEasing.InSine, 0, perLoop / 2, startingPosX, 320);
                particle.MoveX(OsbEasing.OutSine,perLoop / 2, perLoop, 320, endingPosX);
                particle.MoveY(OsbEasing.OutSine,0, perLoop / 2, lowestPosY, highestPosY);
                particle.MoveY(OsbEasing.InSine,perLoop / 2, perLoop, highestPosY, lowestPosY);
                particle.EndGroup();

                particle.ScaleVec(startTime, 0.1, 0.2);
                particle.Additive(startTime, endTime);
                particle.Fade(startTime, 1);
                particle.Fade(endTime, 0);

                delay += 100;
            }

            

        }

        


        void ChorusPart1(int startTime, int readjustTime = -1){
            
            var layer = GetLayer("Chorus FRONT");
            var layerBack = GetLayer("Chorus BACK");
            var bgSky = layerBack.CreateSprite("sb/bg/bg1sky.jpg");
            var mountains = layer.CreateSprite("sb/bg/bg1mountains.png");
            var foreground = layer.CreateSprite("sb/bg/bg2foreground.png");
            var girl = layer.CreateSprite("sb/girl/character_full.png");
            var tree = layer.CreateSprite("sb/bg/bw/bg1fronttree.png");
            var grain = layer.CreateAnimation("sb/grain/g.jpg",10, 120, OsbLoopType.LoopForever, OsbOrigin.Centre);

            var checkpoint1 = startTime + 90456 - 82956;
            var checkpoint2 = startTime + 95994 - 82956;
            var checkpoint3 = startTime + 97725 - 82956;
            var checkpoint4 = startTime + 105225 - 82956;
            var checkpoint5 = startTime + 108917 - 82956;
            
            checkpoint1 -= 116;
             checkpoint2 -= 116;
              checkpoint3 -= 116;
               checkpoint4 -= 116;
                checkpoint5 -= 116;
            


            //Simple fade
            girl.Fade(startTime, 1);
            girl.Fade(checkpoint5, 0);

            foreground.Fade(startTime, 1);
            foreground.Fade(checkpoint5, 0);

            bgSky.Fade(startTime, 1);
            bgSky.Fade(checkpoint5, 0);

            mountains.Fade(startTime, 1);
            mountains.Fade(checkpoint5, 0);

            tree.Fade(startTime, 1);
            tree.Fade(checkpoint5, 0);
            
            grain.Fade(startTime, 0.3);
            grain.Fade(checkpoint5, 0);
            grain.Additive(startTime, checkpoint5);

            //First scene
            girl.Scale(startTime, 480.0f / GetMapsetBitmap("sb/girl/character_full.png").Height);
            girl.MoveX(OsbEasing.OutCirc, startTime, startTime + 2000, PlayfieldBoundsRight - 50, PlayfieldBoundsRight);
            girl.MoveX(startTime + 2000, checkpoint1, PlayfieldBoundsRight, PlayfieldBoundsRight + 10);
            girl.MoveY(startTime, 240);

            foreground.Scale(startTime, 480.0f / GetMapsetBitmap("sb/bg/bg2foreground.png").Height);

            bgSky.Scale(startTime, 480.0f / GetMapsetBitmap("sb/bg/bg2foreground.png").Height);

            mountains.Scale(startTime, 480.0f / GetMapsetBitmap("sb/bg/bg1mountains.png").Height);

            tree.Scale(startTime, 480.0f / GetMapsetBitmap("sb/bg/bw/bg1fronttree.png").Height);

            tree.MoveX(startTime, checkpoint1, -100, -110);

            grain.Scale(startTime, 480.0f / GetMapsetBitmap("sb/grain/g0.jpg").Height);
            
            






            //second scene
            girl.Scale(checkpoint1, 1);
            girl.MoveY(checkpoint1, 500);
            girl.MoveX(checkpoint1, checkpoint2, PlayfieldBoundsLeft - 50, PlayfieldBoundsLeft - 70);
            girl.Fade(checkpoint3 - (97725 - 95994), 0);

            foreground.Fade(checkpoint1, 0);
            mountains.Fade(checkpoint1, 0);
            tree.Fade(checkpoint1, 0);
            
            bgSky.Scale(checkpoint1, 480.0f * 5 / GetMapsetBitmap("sb/bg/bg2foreground.png").Height);
            


            //Third scene
            girl.Fade(checkpoint3, 1);
            girl.Scale(checkpoint3, 480.0f / GetMapsetBitmap("sb/girl/character_full.png").Height);
            girl.MoveX(OsbEasing.OutCirc, checkpoint3, checkpoint3 + 2000, PlayfieldBoundsRight - 50, PlayfieldBoundsRight);
            girl.MoveX(checkpoint3 + 2000, checkpoint5, PlayfieldBoundsRight, PlayfieldBoundsRight + 10);
            girl.MoveY(checkpoint3, 240);

            tree.MoveX(checkpoint3, checkpoint5, -100, -110);

            foreground.Fade(checkpoint3, 1);
            mountains.Fade(checkpoint3, 1);
            tree.Fade(checkpoint3, 1);

            bgSky.Scale(checkpoint3, 480.0f / GetMapsetBitmap("sb/bg/bg2foreground.png").Height);

            P1ParticleEffect(80, 0.1, startTime, checkpoint5);




        }

        internal void P1ParticleEffect(int ct, double factor, double start, double end)
        {
            for (int i = 0; i < ct; i++)
            {
                OsbSprite p = GetLayer("Chorus FRONT").CreateSprite("sb/p.png", OsbOrigin.Centre);
                double time = Random(1000, 5000);
                double scale = 1000/time * factor;
                int x = Random(-257, 747);
                int dx = Random(50, 200);
                p.Fade(start, start, 0, 1);
                p.Fade(end, end, 1, 0);
                p.Scale(start, scale);
                p.StartLoopGroup(start - Random(0, time), (int)((end - start) / time) + 2);
                    p.Move(0, time, x, 0, x + dx, 480);
                p.EndGroup();
                p.Additive(start);
            }
        }
    }
} 

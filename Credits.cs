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
using System.Drawing;
using System.Linq;

namespace StorybrewScripts
{
    public class Credits : StoryboardObjectGenerator
    {
        FontGenerator fontGenerator;
        public override void Generate()
        {
		    /*
            Song Composer: Genkaku Aria (feat. KOKOMI)
            Mapper: KKipalt & Icekalt
            Hitsound: Vermasium
            Illustrator: Alptraum & Mimiliaa
            Graphic Design: AlexDunk
            Storyboard: binarie & Himada
            Video: Iyouka
            Design Coordinator: Sakura006
            Special Thanks: RedcXca
            */

            

            string path = "possiblefont1.ttf";
            int fontSize = 82;

            fontGenerator = LoadFont("sb/credits", new FontDescription()
            {
                FontPath = path,
                FontSize = fontSize,
                Color = Color4.White,
                Padding = new Vector2(0,0),
                FontStyle = FontStyle.Regular,
                TrimTransparency = true,
                EffectsOnly = false,
            });

            /*
            290648
            292494
            294340

            296186
            297224
            298032

            299878
            301724
            303571

            304609
            305532
            */
            

            generateCredit("Song Composer", "Genkaku Aria (feat. KOKOMI)", 290648, 292494);
            generateCredit("Mapper", "KKipalt & Icekalt", 292494, 294340);
            generateCredit("Hitsound", "Vermasium", 294340, 296186);

            generateCredit("Illustrator", "Alptraum & Mimiliaa", 296186, 297224);
            generateCredit("Graphic Design", "AlexDunk", 297224, 298032);





            generateCreditLeft("Storyboard", "binarie & Himada", 298032, 299878);
            generateCreditRight("Video", "Iyouka", 299878, 301724);
            generateCreditLeft("Design Coordinator", "Sakura006", 301724, 303571);
            generateCreditRight("Special Thanks", "RedcXca", 303571, 304609);


            fontGenerator = LoadFont("sb/Fullcredits", new FontDescription()
            {
                FontPath = path,
                FontSize = fontSize,
                Color = Color4.White,
                Padding = new Vector2(0,0),
                FontStyle = FontStyle.Regular,
                TrimTransparency = true,
                EffectsOnly = false,
            });


            mainCredits("Song Composer\nMapper\nHitsound", 305532, new Vector2(320 - 100, 240 - 50));
            mainCredits("Genkaku Aria (feat. KOKOMI)\nKKipalt & Icekalt\nVermasium", 305532, new Vector2(320 + 100, 240 - 50));

            mainCredits("Illustrator\nGraphic Design\nStoryboard", 306455, new Vector2(320 - 100, 240));
            mainCredits("Alptraum & Mimiliaa\nAlexDunk\nbinarie & Himada", 306455, new Vector2(320 + 100, 240));

            mainCredits("Video\nDesign Coordinator\nSpecial Thanks", 307378, new Vector2(320 - 100, 240 + 50));
            mainCredits("Iyouka\nCoordinator: Sakura006\nRedcXca", 307378, new Vector2(320 + 100, 240 + 50)); 


            
        }

        void mainCredits(string text, int start, Vector2 positions){

                var FontScale = 0.1f;
                var letterY = positions.Y;
                var layer = GetLayer("full credits");
                foreach (var line in text.Split('\n'))
                {
                    var lineWidth = 0f;
                    var lineHeight = 0f;
                    foreach (var letter in line)
                    {
                        var texture = fontGenerator.GetTexture(letter.ToString());
                        lineWidth += texture.BaseWidth * FontScale;
                        lineHeight = Math.Max(lineHeight, texture.BaseHeight * FontScale);
                    }

                     var letterX = positions.X; //- lineWidth * 0.5f;
                     var delay = 0;
                    foreach (var letter in line)
                    {
                        var texture = fontGenerator.GetTexture(letter.ToString());
                        if (!texture.IsEmpty)
                        {
                            var position = new Vector2(letterX, letterY)
                                + texture.OffsetFor(OsbOrigin.Centre) * FontScale;

                            var sprite = layer.CreateSprite(texture.Path, OsbOrigin.Centre, position);
                            sprite.Scale(start, FontScale);
                            sprite.Fade(start + delay, start + delay + 100, 0, 1);
                            sprite.Fade(309109, 309109, 1, 0);
                        }
                        letterX += texture.BaseWidth * FontScale;
                        delay += 20;
                    }
                    letterY += lineHeight;
                }
            

        }

        void generateCredit(string title, string name, int start, int end){
            var layer = GetLayer("credits");
        
            var mainSqare = layer.CreateSprite("sb/1px.png");

            var sqarePuddleUpperRight = layer.CreateSprite("sb/1px.png");
            var sqarePuddleUpperLeft = layer.CreateSprite("sb/1px.png");
            var sqarePuddleLowerRight = layer.CreateSprite("sb/1px.png");
            var sqarePuddleLowerLeft = layer.CreateSprite("sb/1px.png");

            var spriteTitle = layer.CreateSprite(fontGenerator.GetTexture(title).Path, OsbOrigin.Centre, new Vector2(320, 240 - 5));
            var spriteName = layer.CreateSprite(fontGenerator.GetTexture(name).Path, OsbOrigin.Centre, new Vector2(320, 240 + 5));

            spriteName.Fade(start, 1);
            spriteName.Fade(end, 0);
            spriteName.Scale(start, 0.1);

            spriteTitle.Fade(start, 1);
            spriteTitle.Fade(end, 0);
            spriteTitle.Scale(start, 0.07);

            var additiveUpperRight = layer.CreateSprite("sb/1px.png", OsbOrigin.TopCentre, new Vector2(320 + 30, 240 - 30));
            var additiveUpperLeft = layer.CreateSprite("sb/1px.png", OsbOrigin.CentreLeft, new Vector2(320 - 30, 240 - 30));
            var additiveLowerRight = layer.CreateSprite("sb/1px.png", OsbOrigin.CentreRight, new Vector2(320 + 30, 240 + 30));
            var additiveLowerLeft = layer.CreateSprite("sb/1px.png", OsbOrigin.BottomCentre, new Vector2(320 - 30, 240 + 30));

            var additives = new List<OsbSprite>(){additiveUpperRight, additiveUpperLeft, additiveLowerRight, additiveLowerLeft};


            foreach(var add in additives){

                add.Fade(start, 1);
                add.Fade(end, 0);
                add.Rotate(start, Math.PI/4);

                
                add.Color(start, Color4.Black);

            }

            additiveUpperRight.ScaleVec(OsbEasing.OutCirc, start, start + 500, 85, 85, 85, 0);
            additiveUpperLeft.ScaleVec(OsbEasing.OutCirc, start, start + 500, 85, 85, 0, 85);
            additiveLowerRight.ScaleVec(OsbEasing.OutCirc, start, start + 500, 85, 85, 0, 85);
            additiveLowerLeft.ScaleVec(OsbEasing.OutCirc, start, start + 500, 85, 85, 85, 0);


            mainSqare.Scale(OsbEasing.OutCirc, start, start + 500, 0, 50);
            mainSqare.Rotate(start, Math.PI/4);
            mainSqare.Color(start, Color4.SkyBlue);
            mainSqare.Fade(end, end, 0.1, 0);
            mainSqare.Scale(OsbEasing.InCirc, end - 100, end, 50, 0);
            
        }

        void generateCreditLeft(string title, string name, int start, int end){
            var layer = GetLayer("credits");

            var offset = -300;
        
            var mainSqare = layer.CreateSprite("sb/1px.png", OsbOrigin.Centre, new Vector2(320 + offset, 240));

            

            var spriteTitle = layer.CreateSprite(fontGenerator.GetTexture(title).Path, OsbOrigin.Centre, new Vector2(320 + offset, 240 - 5));
            var spriteName = layer.CreateSprite(fontGenerator.GetTexture(name).Path, OsbOrigin.Centre, new Vector2(320 + offset, 240 + 5));

            spriteName.Fade(start, 1);
            spriteName.Fade(end, 0);
            spriteName.Scale(start, 0.1);

            spriteTitle.Fade(start, 1);
            spriteTitle.Fade(end, 0);
            spriteTitle.Scale(start, 0.07);

            var additiveUpperRight = layer.CreateSprite("sb/1px.png", OsbOrigin.TopCentre, new Vector2(320 + 30 + offset, 240 - 30));
            var additiveUpperLeft = layer.CreateSprite("sb/1px.png", OsbOrigin.CentreLeft, new Vector2(320 - 30 + offset, 240 - 30));
            var additiveLowerRight = layer.CreateSprite("sb/1px.png", OsbOrigin.CentreRight, new Vector2(320 + 30 + offset, 240 + 30));
            var additiveLowerLeft = layer.CreateSprite("sb/1px.png", OsbOrigin.BottomCentre, new Vector2(320 - 30 + offset, 240 + 30));

            var additives = new List<OsbSprite>(){additiveUpperRight, additiveUpperLeft, additiveLowerRight, additiveLowerLeft};


            foreach(var add in additives){

                add.Fade(start, 1);
                add.Fade(end, 0);
                add.Rotate(start, Math.PI/4);

                
                add.Color(start, Color4.Black);

            }

            additiveUpperRight.ScaleVec(OsbEasing.OutCirc, start, start + 500, 85, 85, 85, 0);
            additiveUpperLeft.ScaleVec(OsbEasing.OutCirc, start, start + 500, 85, 85, 0, 85);
            additiveLowerRight.ScaleVec(OsbEasing.OutCirc, start, start + 500, 85, 85, 0, 85);
            additiveLowerLeft.ScaleVec(OsbEasing.OutCirc, start, start + 500, 85, 85, 85, 0);


            mainSqare.Scale(OsbEasing.OutCirc, start, start + 500, 0, 50);
            mainSqare.Rotate(start, Math.PI/4);
            mainSqare.Color(start, Color4.SkyBlue);
            mainSqare.Fade(end, end, 0.1, 0);
            mainSqare.Scale(OsbEasing.InCirc, end - 100, end, 50, 0);
            
        }

        void generateCreditRight(string title, string name, int start, int end){
            var layer = GetLayer("credits");

            var offset = 300;
        
            var mainSqare = layer.CreateSprite("sb/1px.png", OsbOrigin.Centre, new Vector2(320 + offset, 240));

            

            var spriteTitle = layer.CreateSprite(fontGenerator.GetTexture(title).Path, OsbOrigin.Centre, new Vector2(320 + offset, 240 - 5));
            var spriteName = layer.CreateSprite(fontGenerator.GetTexture(name).Path, OsbOrigin.Centre, new Vector2(320 + offset, 240 + 5));

            spriteName.Fade(start, 1);
            spriteName.Fade(end, 0);
            spriteName.Scale(start, 0.1);

            spriteTitle.Fade(start, 1);
            spriteTitle.Fade(end, 0);
            spriteTitle.Scale(start, 0.07);

            var additiveUpperRight = layer.CreateSprite("sb/1px.png", OsbOrigin.TopCentre, new Vector2(320 + 30 + offset, 240 - 30));
            var additiveUpperLeft = layer.CreateSprite("sb/1px.png", OsbOrigin.CentreLeft, new Vector2(320 - 30 + offset, 240 - 30));
            var additiveLowerRight = layer.CreateSprite("sb/1px.png", OsbOrigin.CentreRight, new Vector2(320 + 30 + offset, 240 + 30));
            var additiveLowerLeft = layer.CreateSprite("sb/1px.png", OsbOrigin.BottomCentre, new Vector2(320 - 30 + offset, 240 + 30));

            var additives = new List<OsbSprite>(){additiveUpperRight, additiveUpperLeft, additiveLowerRight, additiveLowerLeft};


            foreach(var add in additives){

                add.Fade(start, 1);
                add.Fade(end, 0);
                add.Rotate(start, Math.PI/4);

                
                add.Color(start, Color4.Black);

            }

            additiveUpperRight.ScaleVec(OsbEasing.OutCirc, start, start + 500, 85, 85, 85, 0);
            additiveUpperLeft.ScaleVec(OsbEasing.OutCirc, start, start + 500, 85, 85, 0, 85);
            additiveLowerRight.ScaleVec(OsbEasing.OutCirc, start, start + 500, 85, 85, 0, 85);
            additiveLowerLeft.ScaleVec(OsbEasing.OutCirc, start, start + 500, 85, 85, 85, 0);


            mainSqare.Scale(OsbEasing.OutCirc, start, start + 500, 0, 50);
            mainSqare.Scale(OsbEasing.InCirc, end - 100, end, 50, 0);
            mainSqare.Rotate(start, Math.PI/4);
            mainSqare.Color(start, Color4.SkyBlue);
            mainSqare.Fade(end, end, 0.1, 0);
            
        }
    }
}
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
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace StorybrewScripts
{
    public class Lyrics : StoryboardObjectGenerator
    {
        FontGenerator fontGenerator, fontGenerator2;
        internal StoryboardLayer Layer;
        internal double BeatDuration;
        public override void Generate()
        {
            Layer = GetLayer("Lyrics");
            BeatDuration = Beatmap.GetTimingPointAt(10000).BeatDuration;
            /*
                喉を締め付ける 35763 37610
                乾いた季節 37610 40090
                思いにふける 40090 43148
                脳裏に浮かぶ 43148 44937
                思い出の雪 44937 47475
                再会を予期してた 47475 52148
                Last sweet memory 52148 55379

                待つことに 64090 65994
                慣れすぎて 65994 67840
                痛みさえ感じないの 67840 71533
                麻痺してる 71533 73379
                焦がれてる 73379 75225
                冷え切った体温が 75225 78225
                また求めてる 78225 82148

                視界ゼロの中あなたと 82610 86302
                繋いだ手の温もりが恋しくて 86302 89994
                滔々と降り積もる雪に 89994 94379
                そっと手を伸ばした 94379 97379
                白い闇を愛せるように 97379 101071
                マイナスの世界で踊り続ける 101071 108917
                遠退く意識に 108917 110302
                悪魔の手招き 110302 111687
                Whiteout 111687 113533

                お揃いだったコロンの香り 132687 137071
                捨てられずにいる 137071 140071
                黒のコートも頭の中も 140071 144398
                もう真っ白で… 144398 146994

                I wish もう一度 146994 150687
                I wish 伝えたい 150687 154148
                あなた好みの服を着て 154148 155994
                あなた好みの趣味始めて 155994 157840
                あなた好みの日常で 157840 159456
                溢れているから 159456 164533
                さよなら 164533 165917
                言わないで 165917 168917

                足跡も消えて 230994 234629
                無に返すのなら 234629 236764
                いっそ記憶も消し去って 236764 241379
                今も 241379 243225
                冷え切った体温が 243225 246225
                まだ覚えてる 246225 250148

                視界ゼロの中あなたと 255225 258917
                繋いだ手の温もりが恋しくて 258917 262667
                滔々と降り積もる雪に 262667 266994
                そっと手を伸ばした 266994 269994
                白い闇を愛せるように 269994 273686
                マイナスの世界で踊り続ける 273686 281532
                一縷の望みも 281532 282917
                かき消す 282917 283609
                ノイズと 283609 285224
                遠退く意識に 285224 286609
                悪魔の手招き 286609 287994
                Whiteout 287994 289840
            */

            string path = "possiblefont1.ttf";
            int fontSize = 82;

            var font = LoadFont("sb/lyrics", new FontDescription()
            {
                FontPath = path,
                FontSize = fontSize,
                Color = Color4.White,
                Padding = new Vector2(0,0),
                FontStyle = FontStyle.Regular,
                TrimTransparency = true,
                EffectsOnly = false,
            });

            fontGenerator = font;
            
            var font2 = LoadFont("sb/lyrics_outline", new FontDescription()
            {
                FontPath = path,
                FontSize = fontSize*2,
                Color = Color4.Black,
                Padding = new Vector2(0,0),
                FontStyle = FontStyle.Regular,
                TrimTransparency = true,
                
            },
            new FontOutline()
            {
                Thickness = 2,
                Color = Color4.White
            });

            fontGenerator2 = font2;
            #region First Vocal Section
            generateBlackBackgroundLyrics("喉を締め付ける", 35763, 37610);

            generateFullColorLyrics("乾いた季節", 38648, 40090);
            generateBlackBackgroundLyrics("乾いた季節", 37610, 40090);
            
            generateFullColorLyrics("思いにふける", 40090, 42340);
            generateBlackBackgroundLyrics("思いにふける", 40090, 43148);


            generateBlackBackgroundLyrics("脳裏に浮かぶ", 43148, 44937);
            
            generateFullColorLyrics("思い出の雪", 46033, 47475);
            generateBlackBackgroundLyrics("思い出の雪", 44937, 47475);

            generateFullColorLyrics("再会を予期してた", 47475, 51687);
            generateBlackBackgroundLyrics("再会を予期してた", 47475, 51687); //changed from 52148 for better transition

            
            generateBackdroppedEnglishLyrics("Last sweet memory", 52148, 55379);

            #endregion
            //#region Chorus Buildup?
            generateBuildupLyrics("待つことに", 64494, 65533);
            generateBlackBackgroundLyrics("待つことに", 64090, 65994);

            generateBuildupLyrics("慣れすぎて", 66340, 67379);
            generateBlackBackgroundLyrics("慣れすぎて", 65994, 67840);

            generateFullColorLyrics("痛みさえ", 68187, 69917);
            generateFullColorLyrics("感じないの", 69917, 71533);
            generateBlackBackgroundLyrics("痛みさえ", 67840, 69917);
            generateBlackBackgroundLyrics("感じないの", 69917, 71533);

            generateBuildupLyrics("麻痺してる", 71879, 72917);
            generateBlackBackgroundLyrics("麻痺してる", 71533, 73379);

            generateBuildupLyrics("焦がれてる", 73725, 74763);
            generateBlackBackgroundLyrics("焦がれてる", 73379, 75225);

            generateFullColorLyrics("冷え切った体温が", 75571, 78225);
            generateBlackBackgroundLyrics("冷え切った体温が", 75225, 78225);

            generateFullColorLyrics("また求めてる", 78225, 82148);
            generateBlackBackgroundLyrics("また求めてる", 78225, 82148);


            //CHORUS
            generateLyricsChorus("視界ゼロの中あなたと", 83071, 86302);
            generateLyricsChorus("繋いだ手の温もりが恋しくて", 86302, 89994);
            generateLyricsChorus("滔々と降り積もる雪に", 89994, 94379);
            generateLyricsChorus("そっと手を伸ばした", 94379, 97379);
            generateLyricsChorus("白い闇を愛せるように", 97379, 101071);
            generateLyricsChorus("マイナスの世界で踊り続ける", 101071, 108917);
            
            
            
            fillLyrics("遠退く", 108917, 109148 - 108917, 109610);
            fillLyrics("意識に", 109610, 109148 - 108917, 110302);
            fillLyrics("悪魔の", 110302, 109148 - 108917, 110994);
            fillLyrics("手招き", 110994, 109148 - 108917, 111687);
            fillLyrics("Whiteout", 111687, 113533, 113417);

            //generateFullColorLyrics("お揃いだった", 132687, 134533);
            generateFullColorLyrics("コロンの香り", 135571, 137071);
            generateBlackBackgroundLyrics("お揃いだった", 132687, 134533);
            generateBlackBackgroundLyrics("コロンの香り", 134533, 137071);

            generateFullColorLyrics("捨てられずにいる", 137071, 139264);
            generateBlackBackgroundLyrics("捨てられずにいる", 137071, 140071);

            generateFullColorLyrics("黒のコートも頭の中も", 142956, 144398);
            generateBlackBackgroundLyrics("黒のコートも頭の中も", 140071, 144398);

            generateFullColorLyrics("もう真っ白で…", 144398, 144802);
            generateBlackBackgroundLyrics("もう真っ白で…", 144398, 146764); //changed from 146994 for better transition

            generateUnderlayLyrics("I wish", 146994, 154148);
            generateBlackBackgroundLyrics("もう一度", 146994, 150687);
            generateBlackBackgroundLyrics("伝えたい", 150687, 154148);

            generateBlackBackgroundLyrics("あなた好みの服を着て", 154148, 155994);
            generateBlackBackgroundLyrics("あなた好みの趣味始めて", 155994, 157840);
            generateBlackBackgroundLyrics("あなた好みの日常で", 157840, 159456);
            generateBlackBackgroundLyrics("溢れているから", 159456, 164533);
            generateBlackBackgroundLyrics("さよなら", 164533, 165917);
            generateBlackBackgroundLyrics("言わないで", 165917, 168917);

            generateFullColorLyrics("足跡も消えて", 230994, 234629);
            generateBlackBackgroundLyrics("足跡も消えて", 230994, 234629);
            generateFullColorLyrics("無に返すのなら", 234629, 236764);
            generateBlackBackgroundLyrics("無に返すのなら", 234629, 236764);
            generateFullColorLyrics("いっそ記憶も消し去って", 236764, 241379);
            generateBlackBackgroundLyrics("いっそ記憶も消し去って", 236764, 241379);

            generateFullColorLyrics("今も", 241379, 243225);
            generateFullColorLyrics("冷え切った体温が", 243225, 246225);
            generateFullColorLyrics("まだ覚えてる", 246225, 250148);

            generateBlackBackgroundLyrics("今も", 241379, 243225);
            generateBlackBackgroundLyrics("冷え切った体温が", 243225, 246225);
            generateBlackBackgroundLyrics("まだ覚えてる", 246225, 250148);

            generateLyricsChorus("視界ゼロの中あなたと", 255225, 258917);
            generateLyricsChorus("繋いだ手の温もりが恋しくて", 258917, 262667);
            generateLyricsChorus("滔々と降り積もる雪に", 262667, 266994);
            generateLyricsChorus("そっと手を伸ばした", 266994, 269994);
            generateLyricsChorus("白い闇を愛せるように", 269994, 273686);
            generateLyricsChorus("マイナスの世界で踊り続ける", 273686, 281532);


            fillLyrics("一縷の望みも", 281532, (109148 - 108917)/3, 282917, 40);
            fillLyrics("かき消す", 282917, (109148 - 108917)/2, 283609, 65);
            fillLyrics("ノイズと", 283609, (109148 - 108917)/2, 285224, 65);
            fillLyrics("遠退く意識に", 285224, (109148 - 108917)/2, 286609, 40);
            fillLyrics("悪魔の手招き", 286609, (109148 - 108917)/2, 287994, 40);
            fillLyrics("Whiteout", 287994, 113533, 289840);

            
        }

        void generateLyricsChorus(string text, int startTime, int endTime){

            

            
            GenerateBackgroundLyrics(text, startTime, endTime);
            GenerateFrontLyrics(text, startTime, endTime);
            

        }
        void generateUnderlayLyrics(string text, int startTime, int endTime)
        {
            StoryboardLayer mid = GetLayer("Mid Song Underlay");
            FontTexture texture1 = fontGenerator2.GetTexture(text + "--"),
                        texture2 = fontGenerator2.GetTexture("--" + text);
            
            OsbSprite p = mid.CreateSprite(texture1.Path, OsbOrigin.TopLeft);
            p.Fade(OsbEasing.InBounce, startTime, startTime + BeatDuration, 0, 0.4);
            p.Fade(OsbEasing.InBounce, endTime - BeatDuration, endTime, 0.4, 0);
            p.Scale(startTime, 0.4);
            p.Additive(startTime);
            p.Move(startTime, -107, 40);

            p = mid.CreateSprite(texture2.Path, OsbOrigin.BottomRight);
            p.Fade(OsbEasing.InBounce, startTime, startTime + BeatDuration, 0, 0.4);
            p.Fade(OsbEasing.InBounce, endTime - BeatDuration, endTime, 0.4, 0);
            p.Scale(startTime, 0.4);
            p.Additive(startTime);
            p.Move(startTime, 747, 440);

        }

        void GenerateFrontLyrics(string text, int StartTime, int EndTime){

            var layer = GetLayer("Chorus Lyrics FRONT");
            var blackBar = true;
            
            
                var letterX = 320;
                var delay = 0; 
                var i = 0;
                var verticalShift = 0;
                var FontScale = 0.2f;
                var dir = 1;
                foreach (var line in text.Split('\n'))
                {
                    var lineWidth = 0f;
                    var lineHeight = 0f;
                    foreach (var letter in line)
                    {
                        var texture = fontGenerator.GetTexture(letter.ToString());
                        lineWidth = Math.Max(lineWidth, texture.BaseWidth * FontScale);
                        lineHeight += texture.BaseHeight * FontScale;
                    }

                    
                    var letterY = 240 - lineHeight * 0.5f;

                    if(blackBar){
                        var p = layer.CreateSprite("sb/1px.png", OsbOrigin.TopCentre, new Vector2(letterX, letterY));
                        p.ScaleVec(OsbEasing.OutExpo, StartTime + delay, EndTime, lineWidth, 0, lineWidth, lineHeight);
                        p.Color(StartTime + delay, Color4.Black);
                        p.Fade(EndTime - 200, EndTime, 1, 0);
                    }
                    
                    
                    foreach (var letter in line)
                    {
                        var texture = fontGenerator.GetTexture(letter.ToString());
                        if (!texture.IsEmpty)
                        {
                            var position = new Vector2(letterX, letterY)
                                + texture.OffsetFor(OsbOrigin.Centre) * FontScale;

                            var sprite = layer.CreateSprite(texture.Path, OsbOrigin.Centre, position);
                            sprite.Scale(StartTime + delay, FontScale);
                            sprite.Fade(StartTime - 200 + delay, StartTime + delay, 0, 1);
                            sprite.MoveX(OsbEasing.OutExpo, StartTime - 200 + delay, EndTime, letterX, letterX + 1 * dir);
                            sprite.Rotate(OsbEasing.OutExpo, StartTime - 200 + delay, EndTime, 0, Math.PI/60 * dir);
                            sprite.Fade(EndTime - 200, EndTime, 1, 0);
                            sprite.Additive(StartTime - 200 + delay, EndTime);
                        }
                        letterY += (int)(texture.BaseHeight * FontScale);
                        delay += 20;
                        dir *= -1;
                    }
                    letterX -= (int)(lineWidth + 1f);
                    i++;
                }
            
        }

        void GenerateBackgroundLyrics(string text, int startTime, int endTime){

            var layer = GetLayer("Chorus Lyrics BACK");
            
            var FontScale = 0.5f;

            
                var letterY = 100f;
                foreach (var line in text.Split('\n'))
                {
                    var lineWidth = 0f;
                    var lineHeight = 0f;
                    foreach (var letter in line)
                    {
                        var texture = fontGenerator2.GetTexture(letter.ToString());
                        lineWidth += texture.BaseWidth * FontScale;
                        lineHeight = Math.Max(lineHeight, texture.BaseHeight * FontScale);
                    }

                    var letterX = 320 - lineWidth * 0.5f;
                    var dir = 1;
                    foreach (var letter in line)
                    {
                        var texture = fontGenerator2.GetTexture(letter.ToString());
                        if (!texture.IsEmpty)
                        {
                            var position = new Vector2(letterX, letterY)
                                + texture.OffsetFor(OsbOrigin.Centre) * FontScale;

                            var sprite = layer.CreateSprite(texture.Path, OsbOrigin.Centre, position);
                            sprite.Scale(startTime, FontScale);
                            sprite.Fade(startTime, startTime + 200, 0, 1);
                            sprite.Fade(endTime - 200, endTime, 1, 0);
                            sprite.MoveY(OsbEasing.OutCirc, startTime, startTime + 200, position.Y + 10 * dir, position.Y);
                            sprite.MoveY(OsbEasing.InCirc, endTime - 200, endTime, position.Y, position.Y + 10 * dir);
                            sprite.Additive(startTime, endTime + 200);
                        }
                        letterX += texture.BaseWidth * FontScale;
                        dir *= -1;
                    }
                    letterY += lineHeight;
                }

        }

        void fillLyrics(string text, int startTime, int perBeat, int endTime, int dy = 130){

            var delay = 0;
            var addedY = 0;
            var addedX = 1;
            if(text == "Whiteout"){
                var texture = fontGenerator2.GetTexture(text);
                var line = Layer.CreateSprite(texture.Path, OsbOrigin.Centre);
                
                line.Fade(startTime, 1);
                line.ScaleVec(startTime, 0.3, 0.3);
                line.Fade(endTime, 0);
                
            }else{
                foreach(var letter in text){
                    var texture = fontGenerator2.GetTexture(letter.ToString());
                    var line = Layer.CreateSprite(texture.Path, OsbOrigin.Centre, new Vector2(320 + 50 * addedX, 100 + addedY));
                    
                    line.Fade(startTime + delay, 1);
                    line.ScaleVec(startTime  + delay, 0.5, 0.5);
                    line.Fade(endTime, 0);
                    line.Rotate(startTime + delay, Random(-Math.PI / 10, Math.PI / 10));

                    delay += perBeat;
                    addedY += dy;
                    addedX *= -1;
                }
            }
            
            

        }
        internal void generateBuildupLyrics(string text, int startTime, int endTime)
        {
            LyricGroup group = GetLyricGroup(text, fontGenerator2);
            double scale = 0.5;
            double y = 0;
            double x;
            int i = 0;
            int time = 4000;
            bool goRight = true;
            while (y < 480)
            {
                x = -107 - (goRight ? group.TotalWidth*scale:0);
                i = Random(0, group.Textures.Length);
                while (x < 747)
                {
                    
                    for (;i < group.Textures.Length; i++)
                    {
                        OsbSprite p = Layer.CreateSprite(group.Textures[i].Path, OsbOrigin.TopLeft);
                        p.Additive(startTime);
                        p.Scale(startTime, scale);
                        p.Fade(OsbEasing.InBounce, startTime, startTime + BeatDuration, 0, 0.2);
                        p.Fade(OsbEasing.InBounce, endTime - BeatDuration, endTime, 0.2, 0);
                        p.StartLoopGroup(startTime, (endTime - startTime)/time + 1);
                            p.MoveX(0, time, x, x + group.TotalWidth*scale*(goRight ? 1:-1));
                        p.EndGroup();
                        p.MoveY(startTime, y);

                        x += group.Textures[i].Width * scale;
                        if (x > 747 + (goRight ? 0:group.TotalWidth *scale)) break;
                    }
                    i = 0;
                    
                }
                goRight = !goRight;
                y += group.MaxHeight*scale;
            }
            
        }
        internal void generateBackdroppedEnglishLyrics(string text, int startTime, int endTime)
        {
            
            generateBlackBackgroundEnglishLyrics(text, startTime + BeatDuration*0.125, endTime  + BeatDuration*0.125, 240, -10);
            
        }
        //internal 
        
        internal void generateBlackBackgroundEnglishLyrics(string text, double startTime, double endTime, int y, int dy)
        {
            double scale = 0.3;
            FontTexture texture = fontGenerator.GetTexture(text);
            char[] extensions = "qypg".ToCharArray();
            bool extendBottom = false;
            foreach (char c in extensions)
            {
                if (!extendBottom && text.Contains(c)) 
                {
                    extendBottom = true;

                }
            }

            double scaleX = texture.Width * 1.5 * scale,
                    scaleY = texture.Height * scale;
            
            
            
            OsbSprite backing = Layer.CreateSprite("sb/1px.png", OsbOrigin.Centre);
            backing.Fade(startTime, 1);
            backing.Fade(endTime, 0);
            backing.Color(startTime, Color4.Black);
            backing.ScaleVec(startTime, scaleX * 1.1, scaleY);
            backing.MoveY(OsbEasing.OutCirc, startTime, startTime + BeatDuration, y + dy, y);
            backing.MoveY(OsbEasing.InCirc, endTime - BeatDuration, endTime, y, y - dy);
            
            if (extendBottom) y += 5;
            OsbSprite a = Layer.CreateSprite(texture.Path);
            a.Fade(startTime, 1);
            a.Fade(endTime, 0);
            a.MoveY(OsbEasing.OutCirc, startTime, startTime + BeatDuration, y + dy, y);
            a.MoveY(OsbEasing.InCirc, endTime - BeatDuration, endTime, y, y - dy);
            a.ScaleVec(startTime, scale * 1.5, scale);
            //a.Color(startTime, Color4.Black);


        }
        internal void generateFullColorLyrics(string text, int startTime, int endTime)
        {
            
            double scale = 0.75;
            
            double height = 0;
            
            LyricGroup group = GetLyricGroup(text, fontGenerator2);
            double width = group.TotalWidth;
            width *= scale;
            
            
            
            double startx = 320 - width*0.5;
            double currentX = startx;
            int startDY = 4;
            for (int i = 0; i < text.Length; i++)
            {
                if (group.Textures[i] == null) continue;
                startDY *= -1;
                
                OsbSprite p = Layer.CreateSprite(group.Textures[i].Path, OsbOrigin.CentreLeft);
                p.MoveX(startTime, currentX);
                p.MoveY(OsbEasing.OutCirc, startTime, startTime + BeatDuration*2, 240 + startDY, 240);
                if (startTime + BeatDuration*2 < endTime - BeatDuration*2)
                    p.MoveY(OsbEasing.InCirc, endTime - BeatDuration*2, endTime, 240, 240 - startDY);
                p.Fade(startTime, 1);
                p.Fade(endTime, 0);
                p.Scale(startTime, scale);
                p.Additive(startTime);
                
                currentX += group.Textures[i].Width*scale;
            }
        }
        internal void generateBlackBackgroundLyrics(string text, int startTime, int endTime)
        {
            double scale = 0.5;
            int startDY = -4;
            double height = 55 * 0.4/scale;
            OsbSprite backing = Layer.CreateSprite("sb/1px.png", OsbOrigin.Centre);
            backing.Fade(startTime, 1);
            backing.Fade(endTime, 0);
            backing.Color(startTime, Color4.Black);
            
            LyricGroup group = GetLyricGroup(text, fontGenerator);

            double width = group.TotalWidth*0.75;
            FontTexture startTexture = group.Textures[0];
            

            
            backing.ScaleVec(OsbEasing.OutCirc, startTime, startTime + BeatDuration, width*1.05, height*1.25, width*1.1, height*1.25);
            double startx = 320 - width*0.5 + startTexture.Width*scale*0.75;
            
            for (int i = 0; i < text.Length; i++)
            {
                if (group.Textures[i] == null) continue;
                startDY *= -1;
                OsbSprite p = Layer.CreateSprite(group.Textures[i].Path, OsbOrigin.Centre);
                p.MoveX(startTime, startx + (width / (text.Length))*i);
                p.MoveY(OsbEasing.OutCirc, startTime, startTime + BeatDuration, 240 + startDY, 240);
                p.MoveY(OsbEasing.InCirc, endTime - BeatDuration, endTime, 240, 240 - startDY);
                p.Fade(startTime, 1);
                p.Fade(endTime, 0);
                p.Scale(startTime, scale);

                
            }
        }
        void generateLyrics(string text, int startTime, int endTime){

            
            var texture = fontGenerator.GetTexture(text);
            var line = Layer.CreateSprite(texture.Path, OsbOrigin.Centre);
            
            line.Fade(startTime, 1);
            line.ScaleVec(startTime, 0.4, 0.3);
            line.Fade(endTime, 0);

        }
        internal LyricGroup GetLyricGroup(string text, FontGenerator gen)
        {
            LyricGroup ret = new LyricGroup();
            ret.Text = text;
            double width = 0;
            double height = 0;
            FontTexture[] textureArray = new FontTexture[text.Length];
            for (int i = 0 ; i < text.Length; i++)
            {
                if (text[i] == ' ')
                {
                    textureArray[i] = null;
                    width += textureArray[i-1].Width;
                }
                else {
                    textureArray[i] = gen.GetTexture(text.Substring(i, 1));
                    width += textureArray[i].Width;
                    height = (textureArray[i].Height > height) ? textureArray[i].Height : height;
                }
            }
            ret.MaxHeight = height;
            ret.Textures = textureArray;
            ret.TotalWidth = width;

            return ret;
        }


        internal class LyricGroup
        {
            internal FontTexture[] Textures;
            internal double TotalWidth;
            internal string Text;

            internal double MaxHeight;

            
        }
    }
}

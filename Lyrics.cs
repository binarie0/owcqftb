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

            generateLyrics("視界ゼロの中あなたと", 82610, 86302);
            generateLyrics("繋いだ手の温もりが恋しくて", 86302, 89994);
            generateLyrics("滔々と降り積もる雪に", 89994, 94379);
            generateLyrics("そっと手を伸ばした", 94379, 97379);
            generateLyrics("白い闇を愛せるように", 97379, 101071);
            generateLyrics("マイナスの世界で踊り続ける", 101071, 108917);
            generateLyrics("遠退く意識に", 108917, 110302);
            generateLyrics("悪魔の手招き", 110302, 111687);
            generateLyrics("Whiteout", 111687, 113533);

            generateFullColorLyrics("お揃いだった", 132687, 134533);
            generateFullColorLyrics("コロンの香り", 134533, 137071);
            generateBlackBackgroundLyrics("お揃いだった", 132687, 134533);
            generateBlackBackgroundLyrics("コロンの香り", 134533, 137071);

            generateFullColorLyrics("捨てられずにいる", 137071, 139264);
            generateBlackBackgroundLyrics("捨てられずにいる", 137071, 140071);

            generateFullColorLyrics("黒のコートも頭の中も", 142956, 144398);
            generateBlackBackgroundLyrics("黒のコートも頭の中も", 140071, 144398);

            generateFullColorLyrics("黒のコートも頭の中も", 144398, 144802);
            generateBlackBackgroundLyrics("もう真っ白で…", 144398, 146764); //changed from 146994 for better transition

            generateLyrics("I wish もう一度", 146994, 150687);
            generateLyrics("I wish 伝えたい", 150687, 154148);
            generateLyrics("あなた好みの服を着て", 154148, 155994);
            generateLyrics("あなた好みの趣味始めて", 155994, 157840);
            generateLyrics("あなた好みの日常で", 157840, 159456);
            generateLyrics("溢れているから", 159456, 164533);
            generateLyrics("さよなら", 164533, 165917);
            generateLyrics("言わないで", 165917, 168917);

            generateLyrics("足跡も消えて", 230994, 234629);
            generateLyrics("無に返すのなら", 234629, 236764);
            generateLyrics("いっそ記憶も消し去って", 236764, 241379);
            generateLyrics("今も", 241379, 243225);
            generateLyrics("冷え切った体温が", 243225, 246225);
            generateLyrics("まだ覚えてる", 246225, 250148);

            generateLyrics("視界ゼロの中あなたと", 255225, 258917);
            generateLyrics("繋いだ手の温もりが恋しくて", 258917, 262667);
            generateLyrics("滔々と降り積もる雪に", 262667, 266994);
            generateLyrics("そっと手を伸ばした", 266994, 269994);
            generateLyrics("白い闇を愛せるように", 269994, 273686);
            generateLyrics("マイナスの世界で踊り続ける", 273686, 281532);
            generateLyrics("一縷の望みも", 281532, 282917);
            generateLyrics("かき消す", 282917, 283609);
            generateLyrics("ノイズと", 283609, 285224);
            generateLyrics("遠退く意識に", 285224, 286609);
            generateLyrics("悪魔の手招き", 286609, 287994);
            generateLyrics("Whiteout", 287994, 289840);

            
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

            double width = group.TotalWidth;
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

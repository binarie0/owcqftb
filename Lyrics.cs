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
                FontSize = fontSize,
                Color = Color4.Black,
                Padding = new Vector2(0,0),
                FontStyle = FontStyle.Regular,
                TrimTransparency = true,
                
            },
            new FontGlow()
            {
                Power = 1,
                Radius = 20,
                Color = Color4.White 
            });

            fontGenerator2 = font2;

            generateBlackBackgroundLyrics("喉を締め付ける", 35763, 37610);

            generateFullColorLyrics("乾いた季節", 38648, 40090);
            generateBlackBackgroundLyrics("乾いた季節", 37610, 40090);
            
            generateFullColorLyrics("思いにふける", 40090, 42340);
            generateBlackBackgroundLyrics("思いにふける", 40090, 43148);


            generateBlackBackgroundLyrics("脳裏に浮かぶ", 43148, 44937);
            
            generateFullColorLyrics("思い出の雪", 46033, 47475);
            generateBlackBackgroundLyrics("思い出の雪", 44937, 47475);
            generateFullColorLyrics("再会を予期してた", 47475, 52148);
            generateBlackBackgroundLyrics("再会を予期してた", 47475, 52148);

            generateLyrics("Last sweet memory", 52148, 55379);

            generateLyrics("待つことに", 64090, 65994);
            generateLyrics("慣れすぎて", 65994, 67840);
            generateLyrics("痛みさえ感じないの", 67840, 71533);
            generateLyrics("麻痺してる", 71533, 73379);
            generateLyrics("焦がれてる", 73379, 75225);
            generateLyrics("冷え切った体温が", 75225, 78225);
            generateLyrics("また求めてる", 78225, 82148);

            generateLyrics("視界ゼロの中あなたと", 82610, 86302);
            generateLyrics("繋いだ手の温もりが恋しくて", 86302, 89994);
            generateLyrics("滔々と降り積もる雪に", 89994, 94379);
            generateLyrics("そっと手を伸ばした", 94379, 97379);
            generateLyrics("白い闇を愛せるように", 97379, 101071);
            generateLyrics("マイナスの世界で踊り続ける", 101071, 108917);
            generateLyrics("遠退く意識に", 108917, 110302);
            generateLyrics("悪魔の手招き", 110302, 111687);
            generateLyrics("Whiteout", 111687, 113533);

            generateLyrics("お揃いだったコロンの香り", 132687, 137071);
            generateLyrics("捨てられずにいる", 137071, 140071);
            generateLyrics("黒のコートも頭の中も", 140071, 144398);
            generateLyrics("もう真っ白で…", 144398, 146994);

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
        internal void generateFullColorLyrics(string text, int startTime, int endTime)
        {
            
            double scale = 0.9;
            
            double width = 0, height = 0;
            
            
            FontTexture[] textureArray = new FontTexture[text.Length];
            for (int i = 0 ; i < text.Length; i++)
            {
                textureArray[i] = fontGenerator2.GetTexture(text.Substring(i, 1));
                width += textureArray[i].Width;
                height = textureArray[i].Height > height ? textureArray[i].Height : height;
            }

            width *= scale;
            height *= scale;
            
            double startx = 320 - width*0.5;
            double currentX = startx;
            for (int i = 0; i < text.Length; i++)
            {
                OsbSprite p = Layer.CreateSprite(textureArray[i].Path, OsbOrigin.CentreLeft);
                p.MoveX(startTime, endTime, currentX + 5, currentX - 5);
                p.Fade(startTime, 1);
                p.Fade(endTime, 0);
                p.Scale(startTime, scale);
                p.Additive(startTime);
                
                currentX += textureArray[i].Width*scale;
            }
        }
        internal void generateBlackBackgroundLyrics(string text, int startTime, int endTime)
        {
            double scale = 0.5;
            
            double width = 0, height = 0;
            OsbSprite backing = Layer.CreateSprite("sb/1px.png", OsbOrigin.Centre);
            backing.Fade(startTime, 1);
            backing.Fade(endTime, 0);
            backing.Color(startTime, Color4.Black);
            
            FontTexture[] textureArray = new FontTexture[text.Length];
            for (int i = 0 ; i < text.Length; i++)
            {
                textureArray[i] = fontGenerator.GetTexture(text.Substring(i, 1));
                width += textureArray[i].Width;
                height = textureArray[i].Height > height ? textureArray[i].Height : height;
            }

            width *= scale;
            height *= scale;
            backing.ScaleVec(startTime, width*1.01, height);
            double startx = 320 - width*0.5;
            double currentX = startx;
            for (int i = 0; i < text.Length; i++)
            {
                OsbSprite p = Layer.CreateSprite(textureArray[i].Path, OsbOrigin.CentreLeft);
                p.MoveX(startTime,currentX);
                p.Fade(startTime, 1);
                p.Fade(endTime, 0);
                p.Scale(startTime, scale);

                currentX += textureArray[i].Width*scale;
            }
        }
        void generateLyrics(string text, int startTime, int endTime){

            
            var texture = fontGenerator.GetTexture(text);
            var line = Layer.CreateSprite(texture.Path, OsbOrigin.Centre);
            
            line.Fade(startTime, 1);
            line.ScaleVec(startTime, 0.4, 0.3);
            line.Fade(endTime, 0);

        }
    }
}

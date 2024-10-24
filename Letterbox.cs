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
using SBImageLib;
namespace StorybrewScripts
{
    /*
    binarie
    20 October
    Make letterbox overlay.
    */
    public class Letterbox : StoryboardObjectGenerator
    {
        [Configurable(DisplayName = "Letterbox height (in px)")]
        public int LetterboxHeight = 40;

        [Configurable(DisplayName = "1PX Path")]
        public string OnePXPath = "sb/1px.png";

        [Configurable(DisplayName = "Start Time")]
        public int StartTime = 1840;
        [Configurable(DisplayName = "End Time")]
        public int EndTime = 311071;

        [Configurable(DisplayName = "Debug (for all black background)")]
        public bool Debug = false;

        internal StoryboardLayer layer;
        public override void Generate()
        {
            //initialize layer
		    layer = GetLayer("Letterbox Overlay");

            //create letterbox instances and sync so that there's no disconnect between the two
            OsbSprite topLetterbox = layer.CreateSprite(OnePXPath, OsbOrigin.TopCentre, new Vector2(320, 0));
            SyncLetterbox(topLetterbox);

            OsbSprite bottomLetterbox = layer.CreateSprite(OnePXPath, OsbOrigin.BottomCentre, new Vector2(320, 480));
            SyncLetterbox(bottomLetterbox);
            
        }
        internal void SyncLetterbox(OsbSprite letterbox)
        {
            int width = 854;

            //default characteristics
            letterbox.Fade(StartTime, 1);
            letterbox.Fade(EndTime, 0);
            letterbox.ScaleVec(StartTime, 854, LetterboxHeight);

            if (!Debug)
            {
                letterbox.Color(StartTime, Color4.Black);
            }

            

            //changes in height
            letterbox.ScaleVec(OsbEasing.OutSine, 9225, 12917, width, LetterboxHeight, width, LetterboxHeight*2);

            letterbox.ScaleVec(OsbEasing.OutCirc, 14763, 15687, width, LetterboxHeight*2, width, LetterboxHeight);

        }
    }
}

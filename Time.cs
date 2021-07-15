using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevatryFramework
{
    //A helper class to help about time
    public static class Time
    {





        /// <summary>
        /// A variable contains text, you can edit for translation purposes
        /// </summary>
        public static string[] text = new string[] { "years ago","thousand years ago","hundred years ago","days ago","minutes ago","seconds ago","miliseconds ago","months ago","hours ago","hours later","months later","miliseconds later","seconds later","minutes later","days later","hundred years later","thousand years later","years later"};//ago
        /// <summary>
        /// Returns time in human readable format, doesnt count leaps! Assumes every month is 30 days
        /// </summary>
        /// <param name="time">Time</param>
        /// <returns>String in human readable format</returns>
        public static string TimeAgo(DateTime time)
        {
            var sec = time.Second;
            var interval = sec / 31536000;
            string toReturn = "Error parsing time";
            if (interval > 1)
                toReturn = Math.Floor((double)interval) + text[0];
            interval = sec / 60 * 60 * 24 * 30;
            if (interval > 60 * 60 * 24 * 30)
                toReturn = Math.Floor((double)interval) + text[7];
            interval = sec / 60 * 60 * 24 ;
            if (interval > 60 * 60 * 24 )
                toReturn = Math.Floor((double)interval) + text[4];
            interval = sec / 60 * 60;
            if (interval > 60 * 60 )
                toReturn = Math.Floor((double)interval) + text[8];
            interval = sec / 60;
            if (interval > 60)
                toReturn = Math.Floor((double)interval) + text[6];
            return toReturn;
        }
    }
}

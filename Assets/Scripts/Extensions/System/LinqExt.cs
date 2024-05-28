using System.Collections.Generic;

namespace Extensions.System
{
    public static class LinqExt
    {
        public static T Random<T>(this List<T> thisList)
        {
            return thisList[UnityEngine.Random.Range(0, thisList.Count)];
        }
        
        public static T PullRandom<T>(this List<T> thisList)
        {
            int randomNumberofPowerUps = UnityEngine.Random.Range(0, thisList.Count);
            T pullRandom = thisList[randomNumberofPowerUps];
            thisList.Remove(pullRandom);
            return pullRandom;
        }
    }
}
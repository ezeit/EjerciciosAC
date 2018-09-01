using System;

namespace EPedrini.AdjacentMaxDistance
{
    public static class Solution
    {
        public static int solution(int[] a)
        {
            if (a.Length == 1) return -2; //If array contains only one element, then doesn't exist adyacent values

            Array.Sort(a); //Sort the array to have the adyacent values together

            var maxDistance = 0;

            for (var i = 1; i < a.Length; i++) //Iterates the sorted array comparing actual against previous element distance, and keep it if it's current maximum
            {
                var distance = a[i] - a[i - 1];
                if (distance > maxDistance)
                    maxDistance = distance;
            }

            return maxDistance;
        }
    }
}

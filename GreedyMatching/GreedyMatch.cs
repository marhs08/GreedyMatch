using GreedyMatching.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GreedyMatching
{
    public class GreedyMatch
    {
        private static IMatcher _matchingHelper;

        static void Main(string[] args)
        {
            var fragments = new List<string>
            {
                //"all is well",
                //"ell that en",
                //"hat end",
                //"t ends well"
                "all is well",
                "WELL THAT EN",
                "hat end",
                "T ends Well"
            };

            var mergedResult = GetMergedFragments(fragments);
            Console.WriteLine(mergedResult);
            Console.ReadLine();
        }

        public static string GetMergedFragments(List<string> fragments)
        {
            _matchingHelper = new MatchingHelper();

            string result = string.Empty;
            if (fragments.Count <= 0)
            {
                result = "[Error]: No fragments found.";
            }
            else
            {
                bool isInvalid = false;
                while (fragments.Count > 1)
                {
                    var overlappedLength = new int[fragments.Count - 1];
                    for (var ctr = 0; ctr < fragments.Count - 1; ctr++)
                    {
                        overlappedLength[ctr] = _matchingHelper.GetOverlappedStringLength(fragments[ctr], fragments[ctr + 1]);
                    }

                    if (overlappedLength.Contains(0))
                    {
                        isInvalid = true;
                        break;
                    }

                    var maxIndex = overlappedLength.ToList().IndexOf(overlappedLength.Max());
                    var newValue = _matchingHelper.ConcatWithOverlapping(fragments[maxIndex], fragments[maxIndex + 1]);
                    fragments[maxIndex] = newValue;
                    fragments.RemoveAt(maxIndex + 1);
                }

                if (isInvalid)
                {
                    result = "[Error]: Cannot merge fragments.";
                }
                else
                {
                    result = fragments.First();
                }
            }
            return result;
            
        }
    }
}

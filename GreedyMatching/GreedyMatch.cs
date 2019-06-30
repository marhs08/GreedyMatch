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
            //var fragments = new List<string>
            //{
            //    //"all is well",
            //    //"ell that en",
            //    //"hat end",
            //    //"t ends well"
            //    //"all is well",
            //    //"WELL THAT EN",
            //    //"hat end",
            //    //"T ends Well"

            //    "all is well",
            //    "ends well"
            //};
            var fragments = new List<string> {
            "m quaerat voluptatem.","pora incidunt ut labore et d",", consectetur, adipisci velit","olore magnam aliqua","idunt ut labore et dolore magn","uptatem.","i dolorem ipsum qu","iquam quaerat vol","psum quia dolor sit amet, consectetur, a","ia dolor sit amet, conse","squam est, qui do","Neque porro quisquam est, qu","aerat voluptatem.","m eius modi tem","Neque porro qui",", sed quia non numquam ei","lorem ipsum quia dolor sit amet","ctetur, adipisci velit, sed quia non numq","unt ut labore et dolore magnam aliquam qu","dipisci velit, sed quia non numqua","us modi tempora incid","Neque porro quisquam est, qui dolorem i","uam eius modi tem","pora inc","am al"};

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
                result = string.Empty;
            }
            else
            {
                while (fragments.Count > 1)
                {
                    var overlappedLength = new int[fragments.Count - 1];
                    for (var ctr = 0; ctr < fragments.Count - 1; ctr++)
                    {
                        overlappedLength[ctr] = _matchingHelper.GetOverlappedStringLength(fragments[ctr], fragments[ctr + 1]);
                    }

                    var maxMatchedIndex = overlappedLength.ToList().IndexOf(overlappedLength.Max());
                    var newValue = _matchingHelper.ConcatWithOverlapping(fragments[maxMatchedIndex], fragments[maxMatchedIndex + 1]);
                    fragments[maxMatchedIndex] = newValue;
                    fragments.RemoveAt(maxMatchedIndex + 1);
                }
                result = fragments.First();
            }
            return result;

        }
    }
}

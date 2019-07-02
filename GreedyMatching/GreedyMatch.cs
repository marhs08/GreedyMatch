using GreedyMatching.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GreedyMatching
{
    public class GreedyMatch
    {
        private static IMatcher _matchingHelper;
        private static List<string> _results;

        static void Main(string[] args)
        {

            var fragments = "m quaerat voluptatem.;pora incidunt ut labore et d;, consectetur, adipisci velit;olore magnam aliqua;idunt ut labore et dolore magn;uptatem.;i dolorem ipsum qu;iquam quaerat vol;psum quia dolor sit amet, consectetur, a;ia dolor sit amet, conse;squam est, qui do;Neque porro quisquam est, qu;aerat voluptatem.;m eius modi tem;Neque porro qui;, sed quia non numquam ei;lorem ipsum quia dolor sit amet;ctetur, adipisci velit, sed quia non numq;unt ut labore et dolore magnam aliquam qu;dipisci velit, sed quia non numqua;us modi tempora incid;Neque porro quisquam est, qui dolorem i;uam eius modi tem;pora inc;am al"

             .Split(';').ToList();
            //var fragments = new List<string>
            //{
            //"all is well",
            //"ell that en",
            //"hat end",
            //"t ends well"
            //    //"all is well",
            //    //"WELL THAT EN",
            //    //"hat end",
            //    //"T ends Well"

            //"all is well",
            //    "ends well"

            //"abc",
            //"def",
            //"ghi"

            //  "all is well",
            //"all is well"
            //};


            //var fragments = new List<string> {
            //"m quaerat voluptatem.","pora incidunt ut labore et d",", consectetur, adipisci velit","olore magnam aliqua","idunt ut labore et dolore magn","uptatem.","i dolorem ipsum qu","iquam quaerat vol","psum quia dolor sit amet, consectetur, a","ia dolor sit amet, conse","squam est, qui do","Neque porro quisquam est, qu","aerat voluptatem.","m eius modi tem","Neque porro qui",", sed quia non numquam ei","lorem ipsum quia dolor sit amet","ctetur, adipisci velit, sed quia non numq","unt ut labore et dolore magnam aliquam qu","dipisci velit, sed quia non numqua","us modi tempora incid","Neque porro quisquam est, qui dolorem i","uam eius modi tem","pora inc","am al"};

            var mergedResult = GetMergedFragments(fragments);
            Console.WriteLine("Below are the result/s:");
            foreach (var result in mergedResult)
            {
                Console.WriteLine(result);
            }
            Console.ReadLine();
        }

        public static List<string> GetMergedFragments(List<string> fragments)
        {
           
            _matchingHelper = new MatchingHelper();
            _results = new List<string>();

            if (fragments.Count > 0)
            {
                while (fragments.Count > 1)
                {
                    int[] overlappedLength = GetOverlappedLengths(fragments);
                    var nonOverlappingFragments = overlappedLength.Where(fl => fl == 0).Count();

                    if (nonOverlappingFragments == overlappedLength.Count())
                        break;

                    var maxMatchedValue = overlappedLength.Max();
                    var maxMatchedIndex = overlappedLength.ToList().IndexOf(maxMatchedValue);

                    var newValue = _matchingHelper.ConcatWithOverlapping(fragments[maxMatchedIndex], fragments[maxMatchedIndex + 1]);
                    fragments[maxMatchedIndex] = newValue;
                    fragments.RemoveAt(maxMatchedIndex + 1);
                }

                if (fragments.Count() == 1)
                {
                    _results.Add(fragments.First());
                }
                else
                {
                    GetVariations(fragments.ToArray(), 0, fragments.Count - 1);


                }
            }
            return _results;

        }

        private static int[] GetOverlappedLengths(List<string> fragments)
        {
            var overlappedLength = new int[fragments.Count - 1];
            for (var ctr = 0; ctr < fragments.Count - 1; ctr++)
            {
                overlappedLength[ctr] = _matchingHelper.GetOverlappedStringLength(fragments[ctr], fragments[ctr + 1]);
            }

            return overlappedLength;
        }

        public static void SwapValues(ref string a, ref string b)
        {
            string temp = a;
            a = b;
            b = temp;
        }
        public static void GetVariations(string[] list, int start, int end)
        {
            int i;
            if (start == end)
            {
                string mergedValue = string.Empty;
                for (i = 0; i <= end; i++)
                {
                    mergedValue = string.Concat(mergedValue, list[i]);
                }
                _results.Add(mergedValue);
            }
            else
                for (i = start; i <= end; i++)
                {
                    SwapValues(ref list[start], ref list[i]);
                    GetVariations(list, start + 1, end);
                    SwapValues(ref list[start], ref list[i]);
                }
        }

    }
}

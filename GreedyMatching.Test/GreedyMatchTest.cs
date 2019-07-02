using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GreedyMatching;
using System.Collections.Generic;
using System.Linq;

namespace GreedyMatching.Test
{
    [TestClass]
    public class GreedyMatchTest
    {
        [TestMethod]
        public void Test_Valid_Fragments()
        {
            var fragments = new List<string>
            {
                "all is well",
                "ell that en",
                "hat end",
                "t ends well"
            };

            var result = GreedyMatch.GetMergedFragments(fragments);

            Assert.IsTrue(result.Contains("all is well that ends well"));
        }


        [TestMethod]
        public void Test_NonOverlapping_Fragments()
        {
            var fragments = new List<string>
            {
                "all is well",
                "ends well"
            };

            var result = GreedyMatch.GetMergedFragments(fragments);
            var success = result.Contains("ends wellall is well") || 
                          result.Contains("all is wellends well");

            Assert.IsTrue(success, $"Expected 'ends wellall is well' or 'all is wellends well', actual '{result}'");
        }

        [TestMethod]
        public void Test_Empty_Fragments()
        {
            var fragments = new List<string>();

            var result = GreedyMatch.GetMergedFragments(fragments);
            Assert.IsTrue(result.Count == 0);
        }

        [TestMethod]
        public void Test_Similar_Fragments()
        {
            var fragments = new List<string>
            {
                "all is well",
                "all is well"
            };

            var result = GreedyMatch.GetMergedFragments(fragments);
            Assert.IsTrue(result.Count == 1 && result.Contains("all is well"));
        }

        [TestMethod]
        public void Test_Case_Sensitive()
        {
            var fragments = new List<string>
            {
                "all is well",
                "WELL THAT EN",
                "hat end",
                "T ends Well"
            };

            var result = GreedyMatch.GetMergedFragments(fragments);
            Assert.IsTrue(result.Count == 1 && result.Contains("all is well that ends well"));
        }


        [TestMethod]

        public void Aderant_Standard_Test()
        {
            var fragments = "m quaerat voluptatem.;pora incidunt ut labore et d;, consectetur, adipisci velit;olore magnam aliqua;idunt ut labore et dolore magn;uptatem.;i dolorem ipsum qu;iquam quaerat vol;psum quia dolor sit amet, consectetur, a;ia dolor sit amet, conse;squam est, qui do;Neque porro quisquam est, qu;aerat voluptatem.;m eius modi tem;Neque porro qui;, sed quia non numquam ei;lorem ipsum quia dolor sit amet;ctetur, adipisci velit, sed quia non numq;unt ut labore et dolore magnam aliquam qu;dipisci velit, sed quia non numqua;us modi tempora incid;Neque porro quisquam est, qui dolorem i;uam eius modi tem;pora inc;am al"

                .Split(';').ToList();

            var result = GreedyMatch.GetMergedFragments(fragments);

            Assert.IsTrue(result.Count() == 1 && result.Contains("Neque porro quisquam est, qui dolorem ipsum quia dolor sit amet, consectetur, adipisci velit, sed quia non numquam eius modi tempora incidunt ut labore et dolore magnam aliquam quaerat voluptatem.".ToLower()));

        }

        [TestMethod]
        public void Test_Multiple_Results()
        {
            var fragments = new List<string>
            {
                "abc",
                "def",
                "ghi"
            };

            var result = GreedyMatch.GetMergedFragments(fragments);
            Assert.IsTrue(result.Contains("abcghidef") 
                        && result.Contains("defabcghi") 
                        && result.Contains("defghiabc") 
                        && result.Contains("ghiabcdef") 
                        && result.Contains("ghidefabc") );
        }

    }
}

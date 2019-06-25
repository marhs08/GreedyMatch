using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GreedyMatching;
using System.Collections.Generic;

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
            Assert.AreEqual("all is well that ends well", result);
        }


        [TestMethod]
        public void Test_InValid_Fragments()
        {
            var fragments = new List<string>
            {
                "all is well",
                "ends well"
            };

            var result = GreedyMatch.GetMergedFragments(fragments);
            Assert.AreEqual("[Error]: Cannot merge fragments.", result);
        }

        [TestMethod]
        public void Test_Empty_Fragments()
        {
            var fragments = new List<string>();

            var result = GreedyMatch.GetMergedFragments(fragments);
            Assert.AreEqual("[Error]: No fragments found.", result);
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
            Assert.AreEqual("all is well", result);
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
            Assert.AreEqual("all is well that ends well", result);
        }

        [TestMethod]
        public void Test_Multiple_Fragments()
        {
            var fragments = new List<string>
            {
                "t",
                "the",
                "the quick",
                "the quick brown",
                "the quick brown fox",
                "the quick brown fox jumps",
                "jumps over",
                "jumps over the lazy",
                "jumps over the lazy dog"
            };

            var result = GreedyMatch.GetMergedFragments(fragments);
            Assert.AreEqual("the quick brown fox jumps over the lazy dog", result);
        }

    }
}

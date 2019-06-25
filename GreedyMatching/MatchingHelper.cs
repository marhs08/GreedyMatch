using GreedyMatching.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreedyMatching
{
    public class MatchingHelper: IMatcher
    {

        #region GettingMatchedLength
        /// <summary>
        /// Reference: https://stackoverflow.com/questions/1285434/efficient-algorithm-for-string-concatenation-with-overlap
        /// Modified to handle case sensitivity
        /// </summary>

        public int GetOverlappedStringLength(string s1, string s2)
        {
            if (!string.IsNullOrEmpty(s1))
                s1 = s1.ToLower();

            if (!string.IsNullOrEmpty(s2))
                s2 = s2.ToLower();


            //Trim s1 so it isn't longer than s2
            if (s1.Length > s2.Length) s1 = s1.Substring(s1.Length - s2.Length);

            int[] T = ComputeBackTrackTable(s2); //O(n)

            int m = 0;
            int i = 0;
            while (m + i < s1.Length)
            {
                if (s2[i] == s1[m + i])
                {
                    i += 1;
                    //<-- removed the return case here, because |s1| <= |s2|
                }
                else
                {
                    m += i - T[i];
                    if (i > 0) i = T[i];
                }
            }

            return i; //<-- changed the return here to return characters matched
        }

        int[] ComputeBackTrackTable(string s)
        {
            var T = new int[s.Length];
            int cnd = 0;
            T[0] = -1;
            T[1] = 0;
            int pos = 2;
            while (pos < s.Length)
            {
                if (s[pos - 1] == s[cnd])
                {
                    T[pos] = cnd + 1;
                    pos += 1;
                    cnd += 1;
                }
                else if (cnd > 0)
                {
                    cnd = T[cnd];
                }
                else
                {
                    T[pos] = 0;
                    pos += 1;
                }
            }

            return T;
        }
        #endregion

        public string ConcatWithOverlapping(string s1, string s2)
        {
            if (string.IsNullOrEmpty(s1))
            {
                return string.IsNullOrEmpty(s2) ? string.Empty : s2;
            }
            else
            {
                s1 = s1.ToLower();
            }

            if (string.IsNullOrEmpty(s2))
            {
                return s1;
            }
            else
            {
                s2 = s2.ToLower();
            }
                
            int firstStrLength = s1.Length - 1;
            int secondStrLength = s2.Length - 1;

            char firstStrLastChar = s1[firstStrLength];
            char secondStrFirstChar = s2[0];

            int idxSecondStrLastChar = s2.LastIndexOf(firstStrLastChar, Math.Min(firstStrLength, secondStrLength));
            while (idxSecondStrLastChar != -1)
            {
                if (s1[firstStrLength - idxSecondStrLastChar] == secondStrFirstChar)
                {
                    int idx = idxSecondStrLastChar;
                    while ((idx != -1) && (s1[firstStrLength - idxSecondStrLastChar + idx] == s2[idx]))
                    {
                        idx--;
                    }
                        
                    if (idx == -1)
                        return string.Concat(s1, s2.Substring(idxSecondStrLastChar + 1));
                }
                idxSecondStrLastChar = s2.LastIndexOf(firstStrLastChar, idxSecondStrLastChar - 1);
            }
            return s1 + s2;
        }
    }
}

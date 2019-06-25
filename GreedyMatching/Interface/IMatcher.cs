using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreedyMatching.Interface
{
    public interface IMatcher
    {
        int GetOverlappedStringLength(string s1, string s2);
        string ConcatWithOverlapping(string s1, string s2);
    }
}

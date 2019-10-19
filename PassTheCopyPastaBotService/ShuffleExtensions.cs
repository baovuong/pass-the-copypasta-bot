using System;
using System.Collections.Generic;
using System.Linq;

namespace PassTheCopyPastaBotService
{
    public static class ShuffleExtensions
    {
        public static IEnumerable<tsource>
       RandomShuffle<tsource>(this IEnumerable<tsource> source)
        {
            return source.Select(t => new 
            {
                Index = Guid.NewGuid(),
                Value = t
            })
                .OrderBy(p => p.Index)
                .Select(p => p.Value);
        }
    }
}

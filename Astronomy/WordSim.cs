using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyExtensions;

namespace James
{
    static public class WordSim
    {
        /// <summary>
        /// Calculates the Levenshtein distance between two strings. Upper bound:
        /// length of the larger string. Lower bound: zero. The lower the score, the
        /// more similar the words should be. In proportion to the length of the string.
        /// This is solved and tested, needs no further review.
        /// </summary>
        /// <param name="word1"></param>
        /// <param name="word2"></param>
        /// <returns></returns>
        public static int LevenshteinDist(string word1, string word2)
        {
            int n = word1.Length;
            int m = word2.Length;
            int[,] d = new int[n + 1, m + 1];

            if (n == 0 || m == 0)
                return n + m;

            for (int i = 0; i <= n; d[i, 0] = i++){ }

            for (int j = 0; j <= m; d[0, j] = j++){ }

            for (int i = 1; i <= n; i++)
            {
                for (int j = 1; j <= m; j++)
                {
                    int cost = (word2[j - 1] == word1[i - 1]) ? 0 : 1;

                    d[i, j] = Math.Min(
                        Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1),
                            d[i - 1, j - 1] + cost);

                }
            }

            return d[n, m];
        }

        /// <summary>
        /// Calculates the Damerau-Levenshtein distance between two strings. Similar to
        /// the Levenshtein distance 
        /// </summary>
        /// <param name="word1"></param>
        /// <param name="word2"></param>
        /// <returns></returns>
        public static int DamerauLevDist(string word1, string word2)
        {
            var bounds = new { Height = word1.Length + 1, Width = word2.Length + 1 };

            int[,] matrix = new int[bounds.Height, bounds.Width];

            for (int height = 0; height < bounds.Height; height++) { matrix[height, 0] = height; };
            for (int width = 0; width < bounds.Width; width++) { matrix[0, width] = width; };

            for (int height = 1; height < bounds.Height; height++)
            {
                for (int width = 1; width < bounds.Width; width++)
                {
                    int cost = (word1[height - 1] == word2[width - 1]) ? 0 : 1;
                    int insertion = matrix[height, width - 1] + 1;
                    int deletion = matrix[height - 1, width] + 1;
                    int substitution = matrix[height - 1, width - 1] + cost;

                    int distance = Math.Min(insertion, Math.Min(deletion, substitution));

                    if (height > 1 && width > 1 && word1[height - 1] == word2[width - 2] && word1[height - 2] == word2[width - 1])
                    {
                        distance = Math.Min(distance, matrix[height - 2, width - 2] + cost);
                    }

                    matrix[height, width] = distance;
                }
            }

            return matrix[bounds.Height - 1, bounds.Width - 1];
        }

        public static double DameraueLevDistPercent(string word1, string word2)
        {
            int greaterLength = Math.Max(word1.Length, word2.Length);

            return (1 - ((double)DamerauLevDist(word1, word2) / greaterLength)) * 100.0;
        }

        public static int HammingDist(string word1, string word2)
        {
            

            return 0;
        }
        

        ///// <summary>
        ///// Measures the similarity of two strings in a very rough homemade
        ///// algorithm, dependent both on how many characters it has in common
        ///// and how many streaks of similar characters it has. Highest score is
        ///// double the length of the longest string, lowest score is 0. Longest
        ///// possible loop is the longest strings length to the power of the shortest
        ///// strings length (YIKES, FIX PLEASE), and the shortest possible loop
        ///// is the length of the shortest string.
        ///// </summary>
        ///// <param name="word1"></param>
        ///// <param name="word2"></param>
        ///// <returns></returns>
        //public static double HomemadeDistOther(string word1, string word2)
        //{
        //    word1 = word1.ToLower();
        //    word2 = word2.ToLower();
        //    if (word1.Length < word2.Length)
        //        //word2 = word1.SwapStorage(word2);

        //    int totalMatch = 0;
        //    int currentStreak = 0;
        //    int highestStreak = 0;
        //    int a = 0;

        //    while(a < word1.Length)
        //    {
        //        for (int b = 0; b < word2.Length; b++)
        //        {
        //            if (a < word1.Length && word1[a] == word2[b])
        //            {
        //                currentStreak++;
        //                a++;
        //                break;
        //            }
        //            if (a == word1.Length)
        //                break;
        //            if (word1[a] != word2[b] || b == word2.Length - 1)
        //            {
        //                totalMatch += currentStreak;
        //                if (currentStreak > highestStreak)
        //                    highestStreak = currentStreak;
        //                currentStreak = 0;
        //                a++;
        //            }

        //        }
        //    }
        //    if (currentStreak > highestStreak)
        //        highestStreak = currentStreak;

        //    return (highestStreak + totalMatch)/(2.0 * word1.Length) * 100;
        //}

        //public static double HomemadeDist(string word1, string word2)
        //{
        //    word1 = word1.ToLower();
        //    word2 = word2.ToLower();
        //    if (word1.Length < word2.Length)
        //        //word2 = word1.SwapStorage(word2);

        //    int totalMatch = 0;
        //    int currentStreak = 0;
        //    int highestStreak = 0;
        //    int a = 0;
        //    int b = 0;
        //    int consecutiveFailures = 0;

        //    while ( /*b < word2.Length &&*/ a < word1.Length)
        //    {
        //        if (word1[a] == word2[b])
        //        {
        //            currentStreak++;
        //            a++;
        //            b++;
        //            if (a >= word1.Length || b >= word2.Length)
        //                break;
        //        }
        //        else
        //        {
        //            totalMatch += currentStreak;
        //            if (currentStreak > highestStreak)
        //                highestStreak = currentStreak;
        //            currentStreak = 0;
        //            b++;
        //            consecutiveFailures++;
        //            if (b == word2.Length)
        //                b = 0;
        //        }
        //        if (consecutiveFailures == word2.Length)
        //        {
        //            a++;
        //            consecutiveFailures = 0;
        //        }
        //    }
            


        //    return (highestStreak + totalMatch) / (2.0 * word1.Length) * 100;
        //}


        public static int QuickCheck(string[] options, string input)
        {
            for (int a = 1; a < options.Length; a++)
            {
                if (options[a][0] == input[0])
                {
                    if (DameraueLevDistPercent(options[a], input) > 70)
                        return a;
                    else
                        options[a] = "";
                }
            }

            for (int a = 1; a < options.Length; a++)
            {
                if (options[a][options[a].Length - 1] == input[input.Length - 1])
                {
                    if (DameraueLevDistPercent(options[a], input) > 70)
                        return a;
                    else
                        options[a] = "";
                }
            }

            foreach (string a in options)
            {
                if (a != "")
                    if (DameraueLevDistPercent(a, input) > 70)
                        return options.ToList().IndexOf(a);
            }
            throw new Exception("No passible similarity was found within the list of possible options");
        }
    }
}
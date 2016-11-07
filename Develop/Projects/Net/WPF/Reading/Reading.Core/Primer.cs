using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Savchin.Core;


namespace Reading.Core
{
    public enum SyllablesMode
    {
        Popular,
        All
    }

    [Flags]
    public enum SyllablesTypes
    {
        Forward = 1,
        Backward = 2,
        All = 3

    }
    [Flags]
    public enum LettersTypes
    {
        Consonants = 1,
        Vowels = 2,
        All = 3

    }
    public class Primer
    {
        private static readonly char[] Vowels = { 'а', 'у', 'е', 'и', 'о', 'я', 'ю', 'э', 'ы', 'ё' };
        private static readonly char[] Consonants = { 'л', 'б', 'д', 'з', 'р', 'т', 'г', 'в', 'с', 'к', 'м', 'п', 'н', 'ф', 'ц', 'ч', 'ш', 'щ', 'ж', 'х' };
        private static readonly string[] Exepts = { "ёб", "ёп", "еб" };

        /// <summary>
        /// Gets the forward syllable.
        /// </summary>
        /// <returns></returns>
        public string GetForwardSyllable(SyllablesMode mode)
        {
            return GetSyllable(mode, GetConsonant, GetVowel);
        }

        public char GetLetter(LettersTypes types, SyllablesMode mode)
        {
            switch (types)
            {
                case LettersTypes.Consonants:
                    return GetConsonant(mode);

                case LettersTypes.Vowels:
                    return GetVowel(mode);
                default:
                    return Randomizer.GetBoolean() ? GetConsonant(mode) : GetVowel(mode);
            }
        }

        /// <summary>
        /// Gets the backward syllable.
        /// </summary>
        /// <returns></returns>
        public string GetBackwardSyllable(SyllablesMode mode)
        {
            return GetSyllable(mode, GetVowel, GetConsonant);
        }
        /// <summary>
        /// Gets the syllable.
        /// </summary>
        /// <returns></returns>
        public string GetSyllable(SyllablesMode mode, SyllablesTypes types)
        {
            switch (types)
            {
                case SyllablesTypes.Forward:
                    return GetForwardSyllable(mode);
                case SyllablesTypes.Backward:
                    return GetBackwardSyllable(mode);
                default:
                    return Randomizer.GetBoolean() ? GetForwardSyllable(mode) : GetBackwardSyllable(mode);
            }

        }
        #region Random Syllable

        public static char GetConsonant(SyllablesMode mode)
        {
            return mode == SyllablesMode.All ? Randomizer.GetFromArray(Consonants) : Consonants[Randomizer.GetIntegerBetween(0, 13)];
        }

        public static char GetVowel(SyllablesMode mode)
        {
            return mode == SyllablesMode.All ? Randomizer.GetFromArray(Vowels) : Vowels[Randomizer.GetIntegerBetween(0, 5)];
        }

        private static string GetSyllable(SyllablesMode mode, Func<SyllablesMode, char> firstSource, Func<SyllablesMode, char> secondSource)
        {
            char first;
            string result;
            do
            {
                first = firstSource(mode);
                result = string.Format("{0}{1}", first, secondSource(mode));
            } while (first == 'ы' || Exepts.Contains(result));

            return result;
        }
        #endregion

        public static int GetSyllablesCount(string word)
        {
            return word.ToCharArray().Count(IsVowel);
        }

        /// <summary>
        /// Gets the syllables.
        /// </summary>
        /// <param name="word">The word.</param>
        /// <returns></returns>
        public static List<string> GetSyllables(string word)
        {
            var chars = word.ToCharArray();
            //   var countSyllables = chars.Count(IsVowel);
            var builder = new StringBuilder();
            var syllables = new List<string>();
            for (int index = 0; index < chars.Length; index++)
            {
                var c = chars[index];
                builder.Append(c);



                if (IsVowel(c))
                {
                    // слог  заканчивается  на гласной, если за ней идет тоже гласная («ко-а-ла»)
                    if (index < chars.Length - 1 && IsVowel(chars[index + 1]))
                    {
                        syllables.Add(builder.ToString());
                        builder.Clear();
                    }//гласную, если перед ней только одна  согласная  («мо-ло-ко»),
                    else if (index < chars.Length - 2 && !IsVowel(chars[index + 1]) && IsVowel(chars[index + 2]))
                    {
                        syllables.Add(builder.ToString());
                        builder.Clear();
                    }//гласную  после  согласной,  если согласных  подряд  две  или больше («мор-ски-е»)
                    else if (builder.Length > 2)
                    {
                        syllables.Add(builder.ToString());
                        builder.Clear();
                    }
                }
                else
                {
                    if (builder.Length > 1 && IsVowel(builder[builder.Length - 2]))
                    {
                        syllables.Add(builder.ToString());
                        builder.Clear();
                    }
                }
            }
            if (builder.Length > 0)
                syllables.Add(builder.ToString());
            return syllables;
        }
        public static bool IsVowel(char ch)
        {
            return Vowels.Contains(ch);
        }

        /// <summary>
        /// Sets the prob.
        /// </summary>
        /// <param name="word">The word.</param>
        /// <returns></returns>
        public static string setProb(string word)
        {
            int sum = 0, ind = 1;
            var chars = word.ToCharArray();
            var s = chars.Count(IsVowel);


            if (s < 2)
                return word;

            var builder = new StringBuilder(word.Length);
            if (s > 1)
            {
                for (int i = 0; i < chars.Length; i++)
                {
                    builder.Append(chars[i]);
                    if (i == chars.Length - 1) break;

                    if (IsVowel(chars[i]))
                    {
                        sum++;
                        if (IsVowel(chars[i + 1]))
                            builder.Append("-");
                        else if (i + 2 < chars.Length && IsVowel(chars[i + 2]))
                            builder.Append("-");
                        else if (i + 3 < chars.Length && IsVowel(chars[i + 3]))
                            ind = 2;
                        else if (i + 4 < chars.Length && IsVowel(chars[i + 4]))
                            ind = 3;
                        else ind = 4;
                    }
                    else if ((ind == 2) || (ind == 3) || (ind == 4))
                    {
                        if (s != sum) builder.Append("-");
                        ind = 0;
                    }
                }
            }
            return builder.ToString();
        }
    }
}

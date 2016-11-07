/*
 * Alexey A. Popov
 * Copyright (c) 2004, Alexey A. Popov
 * All rights reserved.
 *
 * Redistribution and use in source and binary forms, with or without modification, are
 * permitted provided that the following conditions are met:
 *
 * - Redistributions of source code must retain the above copyright notice, this list
 *   of conditions and the following disclaimer.
 *
 * - Redistributions in binary form must reproduce the above copyright notice, this list
 *   of conditions and the following disclaimer in the documentation and/or other materials
 *   provided with the distribution.
 *
 * - Neither the name of the Alexey A. Popov nor the names of its contributors may be used to
 *   endorse or promote products derived from this software without specific prior written
 *   permission.
 *
 * THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS &AS IS& AND ANY EXPRESS
 * OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY
 * AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR
 * CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL
 * DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE,
 * DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER
 * IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT
 * OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 */

using System;
using System.Reflection;
using System.Resources;
using System.Text;

namespace Savchin.Html
{
	/// <summary>
	/// Represents an HTML entity. An entity is a specially encoded
	/// extended character.
	/// </summary>
	/// <remarks>
	/// <para>Many Unicode characters may appear in HTML encoded text
	/// files. <see cref="Entity"/> provide ways to translate these
	/// characters while reading/writing.</para>
	/// <para>Note that translation must be performed after the data is
	/// loaded to memory and before it is written to disk. .Net framework
	/// may garble the extended characters while translating in-memory
	/// Unicode characters to on-disk UTF-8 or other encoding.</para>
	/// <para>Note to developers: to compile the programs using this
	/// class embed the file 'data.resources' to the resulting
	/// assembly.</para>
	/// </remarks>
    internal sealed class Entity
    {
        /// <summary>
        /// The character.
        /// </summary>
        /// <seealso cref="Entity"/>
        internal readonly string Char;

        /// <summary>
        /// The textual representation of the entity
        /// </summary>
        /// <seealso cref="Char"/>
        internal readonly string Text;

        /// <summary>
        /// Initializes an instance of Entity.
        /// </summary>
        /// <param name="ch">A string with the character.</param>
        /// <param name="code">A string with the character's
        /// representation.</param>
        internal Entity(string ch, string code)
        {
            Char = ch;
            Text = code;
        }

	    private static Entity[] Map = {
	        new Entity("&", "&amp;"),
	        new Entity("<", "&lt;"),
	        new Entity(">", "&gt;"),
	        new Entity("\"", "&quot;"),
	        new Entity(new String((char)160, 1), "&nbsp;"),
	        new Entity(new String((char)161, 1), "&iexcl;"),
	        new Entity(new String((char)162, 1), "&cent;"),
	        new Entity(new String((char)163, 1), "&pound;"),
	        new Entity(new String((char)164, 1), "&curren;"),
	        new Entity(new String((char)165, 1), "&yen;"),
	        new Entity(new String((char)166, 1), "&brvbar;"),
	        new Entity(new String((char)167, 1), "&sect;"),
	        new Entity(new String((char)168, 1), "&uml;"),
	        new Entity(new String((char)169, 1), "&copy;"),
	        new Entity(new String((char)170, 1), "&ordf;"),
	        new Entity(new String((char)171, 1), "&laquo;"),
	        new Entity(new String((char)172, 1), "&not;"),
	        new Entity(new String((char)173, 1), "&shy;"),
	        new Entity(new String((char)174, 1), "&reg;"),
	        new Entity(new String((char)175, 1), "&macr;"),
	        new Entity(new String((char)176, 1), "&deg;"),
	        new Entity(new String((char)177, 1), "&plusmn;"),
	        new Entity(new String((char)178, 1), "&sup2;"),
	        new Entity(new String((char)179, 1), "&sup3;"),
	        new Entity(new String((char)180, 1), "&acute;"),
	        new Entity(new String((char)181, 1), "&micro;"),
	        new Entity(new String((char)182, 1), "&para;"),
	        new Entity(new String((char)183, 1), "&middot;"),
	        new Entity(new String((char)184, 1), "&cedil;"),
	        new Entity(new String((char)185, 1), "&sup1;"),
	        new Entity(new String((char)186, 1), "&ordm;"),
	        new Entity(new String((char)187, 1), "&raquo;"),
	        new Entity(new String((char)188, 1), "&frac14;"),
	        new Entity(new String((char)189, 1), "&frac12;"),
	        new Entity(new String((char)190, 1), "&frac34;"),
	        new Entity(new String((char)191, 1), "&iquest;"),
	        new Entity(new String((char)215, 1), "&times;"),
	        new Entity(new String((char)247, 1), "&divide;"),
	        new Entity(new String((char)192, 1), "&Agrave;"),
	        new Entity(new String((char)193, 1), "&Aacute;"),
	        new Entity(new String((char)194, 1), "&Acirc;"),
	        new Entity(new String((char)195, 1), "&Atilde;"),
	        new Entity(new String((char)196, 1), "&Auml;"),
	        new Entity(new String((char)197, 1), "&Aring;"),
	        new Entity(new String((char)198, 1), "&AElig;"),
	        new Entity(new String((char)199, 1), "&Ccedil;"),
	        new Entity(new String((char)200, 1), "&Egrave;"),
	        new Entity(new String((char)201, 1), "&Eacute;"),
	        new Entity(new String((char)202, 1), "&Ecirc;"),
	        new Entity(new String((char)203, 1), "&Euml;"),
	        new Entity(new String((char)204, 1), "&Igrave;"),
	        new Entity(new String((char)205, 1), "&Iacute;"),
	        new Entity(new String((char)206, 1), "&Icirc;"),
	        new Entity(new String((char)207, 1), "&Iuml;"),
	        new Entity(new String((char)208, 1), "&ETH;"),
	        new Entity(new String((char)209, 1), "&Ntilde;"),
	        new Entity(new String((char)210, 1), "&Ograve;"),
	        new Entity(new String((char)211, 1), "&Oacute;"),
	        new Entity(new String((char)212, 1), "&Ocirc;"),
	        new Entity(new String((char)213, 1), "&Otilde;"),
	        new Entity(new String((char)214, 1), "&Ouml;"),
	        new Entity(new String((char)216, 1), "&Oslash;"),
	        new Entity(new String((char)217, 1), "&Ugrave;"),
	        new Entity(new String((char)218, 1), "&Uacute;"),
	        new Entity(new String((char)219, 1), "&Ucirc;"),
	        new Entity(new String((char)220, 1), "&Uuml;"),
	        new Entity(new String((char)221, 1), "&Yacute;"),
	        new Entity(new String((char)222, 1), "&THORN;"),
	        new Entity(new String((char)223, 1), "&szlig;"),
	        new Entity(new String((char)224, 1), "&agrave;"),
	        new Entity(new String((char)225, 1), "&aacute;"),
	        new Entity(new String((char)226, 1), "&acirc;"),
	        new Entity(new String((char)227, 1), "&atilde;"),
	        new Entity(new String((char)228, 1), "&auml;"),
	        new Entity(new String((char)229, 1), "&aring;"),
	        new Entity(new String((char)230, 1), "&aelig;"),
	        new Entity(new String((char)231, 1), "&ccedil;"),
	        new Entity(new String((char)232, 1), "&egrave;"),
	        new Entity(new String((char)233, 1), "&eacute;"),
	        new Entity(new String((char)234, 1), "&ecirc;"),
	        new Entity(new String((char)235, 1), "&euml;"),
	        new Entity(new String((char)236, 1), "&igrave;"),
	        new Entity(new String((char)237, 1), "&iacute;"),
	        new Entity(new String((char)238, 1), "&icirc;"),
	        new Entity(new String((char)239, 1), "&iuml;"),
	        new Entity(new String((char)240, 1), "&eth;"),
	        new Entity(new String((char)241, 1), "&ntilde;"),
	        new Entity(new String((char)242, 1), "&ograve;"),
	        new Entity(new String((char)243, 1), "&oacute;"),
	        new Entity(new String((char)244, 1), "&ocirc;"),
	        new Entity(new String((char)245, 1), "&otilde;"),
	        new Entity(new String((char)246, 1), "&ouml;"),
	        new Entity(new String((char)248, 1), "&oslash;"),
	        new Entity(new String((char)249, 1), "&ugrave;"),
	        new Entity(new String((char)250, 1), "&uacute;"),
	        new Entity(new String((char)251, 1), "&ucirc;"),
	        new Entity(new String((char)252, 1), "&uuml;"),
	        new Entity(new String((char)253, 1), "&yacute;"),
	        new Entity(new String((char)254, 1), "&thorn;"),
	        new Entity(new String((char)255, 1), "&yuml;"),
	        new Entity(new String((char)338, 1), "&OElig;"),
	        new Entity(new String((char)339, 1), "&oelig;"),
	        new Entity(new String((char)352, 1), "&Scaron;"),
	        new Entity(new String((char)353, 1), "&scaron;"),
	        new Entity(new String((char)376, 1), "&Yuml;"),
	        new Entity(new String((char)710, 1), "&circ;"),
	        new Entity(new String((char)732, 1), "&tilde;"),
	        new Entity(new String((char)8194, 1), "&ensp;"),
	        new Entity(new String((char)8195, 1), "&emsp;"),
	        new Entity(new String((char)8201, 1), "&thinsp;"),
	        new Entity(new String((char)8204, 1), "&zwnj;"),
	        new Entity(new String((char)8205, 1), "&zwj;"),
	        new Entity(new String((char)8206, 1), "&lrm;"),
	        new Entity(new String((char)8207, 1), "&rlm;"),
	        new Entity(new String((char)8211, 1), "&ndash;"),
	        new Entity(new String((char)8212, 1), "&mdash;"),
	        new Entity(new String((char)8216, 1), "&lsquo;"),
	        new Entity(new String((char)8217, 1), "&rsquo;"),
	        new Entity(new String((char)8218, 1), "&sbquo;"),
	        new Entity(new String((char)8220, 1), "&ldquo;"),
	        new Entity(new String((char)8221, 1), "&rdquo;"),
	        new Entity(new String((char)8222, 1), "&bdquo;"),
	        new Entity(new String((char)8224, 1), "&dagger;"),
	        new Entity(new String((char)8225, 1), "&Dagger;"),
	        new Entity(new String((char)8230, 1), "&hellip;"),
	        new Entity(new String((char)8240, 1), "&permil;"),
	        new Entity(new String((char)8249, 1), "&lsaquo;"),
	        new Entity(new String((char)8250, 1), "&rsaquo;"),
	        new Entity(new String((char)8364, 1), "&euro;"),
	        new Entity(new String((char)8482, 1), "&trade;")
	    };

	    /// <summary>
	    /// Maps an HTML entity to a readable character.
	    /// </summary>
	    /// <param name="text">The construct to map.</param>
	    /// <returns>The mapped character.</returns>
	    /// <exception cref="HtmlException">
	    /// The method cannot find the construct in it's
	    /// internal dictionary.
	    /// </exception>
	    internal static char MapEntityToChar(string text)
	    {
	        for(int i = 0; i < Map.Length; i++)
	            if(Map[i].Text == text)
	                return Map[i].Char[0];

	        throw new HtmlException(RD.GetString("badEntity"));
	    }

	    /// <summary>
	    /// Maps an readable character to an HTML entity.
	    /// </summary>
	    /// <param name="ch">The character to map.</param>
	    /// <returns>The mapped entity or the original
	    /// character.</returns>
	    internal static string MapCharToEntity(char ch)
	    {
	        for(int i = 0; i < Map.Length; i++)
	            if(Map[i].Char[0] == ch)
	                return Map[i].Text;

	        return new String(ch, 1);
	    }

	    /// <summary>
	    /// Replaces all known entities in the string to
	    /// readable characters.
	    /// </summary>
	    /// <param name="text">The string to look up
	    /// constructs for.</param>
	    /// <returns>The string with all known constructs replaced
	    /// to readable characters.</returns>
	    internal static string MapEntitiesToChars(string text)
	    {
	        StringBuilder result = new StringBuilder(text);

	        for(int i = 0; i < Map.Length; i++)
	            result = result.Replace(Map[i].Text, Map[i].Char);

	        return result.ToString();
	    }

	    /// <summary>
	    /// Replaces readable characters in the string to
	    /// entities.
	    /// </summary>
	    /// <param name="text">The string to look up
	    /// special characters for.</param>
	    /// <returns>The string with all known special characters replaced
	    /// to constructs.</returns>
	    internal static string MapCharsToEntities(string text)
	    {
	        StringBuilder result = new StringBuilder(text);

	        for(int i = 0; i < Map.Length; i++)
	            result = result.Replace(Map[i].Char, Map[i].Text);

	        return result.ToString();
	    }
    }
}

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

namespace Savchin.Html
{
	/// <summary>
	/// Defines the DTD subtype for HTML 4.x.
	/// </summary>
    /// <remarks>
    /// <para>Possible values are:
    /// <list type="table">
    ///	<listheader>
    ///	<term>Value</term>
    ///	<description>Description</description>
    ///	</listheader>
    ///	<item>
    ///	<term>-//IETF//DTD HTML//EN</term>
    ///	<description>HTML 2.0. No tables, frames, language support.</description>
    ///	</item>
    ///	<item>
    ///	<term>-//W3C//DTD HTML 3.2 Final//EN</term>
    ///	<description>HTML 3.2. Limited support for
    ///	style sheets, no frames and languages.</description>
    ///	</item>
    ///	<item>
    ///	<term>-//W3C//DTD HTML 4.01//EN</term>
    ///	<description>HTML 4.01 in strict mode.
    ///	The HTML code describes the document structure, not
    ///	the document's presentation.</description>
    ///	</item>
    ///	<item>
    ///	<term>-//W3C//DTD HTML 4.01 Transitional//EN</term>
    ///	<description>HTML 4.01 in transitional mode.
    ///	All strict mode elements, attributes and features
    ///	are supported but presentation attributes,
    ///	deprecated elements and link targets are
    ///	also supported.</description>
    ///	</item>
    ///	<item>
    ///	<term>-//W3C//DTD HTML 4.01 Frameset//EN</term>
    ///	<description>A variant of HTML 4.01 transitional mode that supports frames.</description>
    ///	</item>
    /// </list>
    /// </para>
    /// </remarks>
	public enum HtmlDtdType
	{
		/// <summary>
		/// No DTD for this version of HTML specification.
		/// </summary>
		None,

		/// <summary>
		/// HMTL 4.x strict DTD.
		/// </summary>
		Strict,

		/// <summary>
		/// HMTL 4.x transitional DTD (HTML with some "quirks").
		/// </summary>
		Transitional,

		/// <summary>
		/// HMTL 4.x with frames DTD.
		/// </summary>
		Frameset
	}
}

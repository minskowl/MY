using System;
using NUnit.Framework;
using System.Configuration;
using System.Collections;
using System.IO;

using Commons.Collections;

namespace NVelocity.Test {

    /// <summary>
    /// Make sure that properties files are loaded correctly
    /// </summary>
    [TestFixture]
    public class CommonsTest {

	[TearDown]
	protected void TearDown() {
	    FileInfo file = new FileInfo("test1.properties");
	    try {
		file.Delete();
	    } catch(System.Exception) {
		// ignore problems cleaning up file
	    }
	}

	[Test]
	public void Test_ExtendedProperties() {
	    FileInfo file = new FileInfo("test1.properties");
	    StreamWriter sw = file.CreateText();
	    sw.WriteLine("# lines starting with # are comments.  Blank lines are ignored");
	    sw.WriteLine("");
	    sw.WriteLine("# This is the simplest property");
	    sw.WriteLine("key = value");
	    sw.WriteLine("");
	    sw.WriteLine("# A long property may be separated on multiple lines");
	    sw.WriteLine("longvalue = aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa \\");
	    sw.WriteLine("            aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");
	    sw.WriteLine("");
	    sw.WriteLine("# This is a property with many tokens");
	    sw.WriteLine("tokens_on_a_line = first token, second token");
	    sw.WriteLine("");
	    sw.WriteLine("# This sequence generates exactly the same result");
	    sw.WriteLine("tokens_on_multiple_lines = first token");
	    sw.WriteLine("tokens_on_multiple_lines = second token");
	    sw.WriteLine("");
	    sw.WriteLine("# commas may be escaped in tokens");
	    sw.WriteLine("commas.excaped = Hi\\, what'up?");
	    sw.Flush();
	    sw.Close();

	    StreamReader sr = file.OpenText();
	    String s = sr.ReadToEnd();
	    sr.Close();

	    // TODO: could build string, then write, then read and compare.
	    ExtendedProperties props = new ExtendedProperties(file.FullName);

	    Assertion.Assert("expected to have 5 properties, had " + props.Count.ToString(), props.Count==5);

	    Assertion.Assert("key was not correct: " + props.GetString("key"), props.GetString("key").Equals("value"));
	    Assertion.Assert("commas.excaped was not correct: " + props.GetString("commas.excaped"), props.GetString("commas.excaped").Equals("Hi, what'up?"));

	    Object o = props.GetProperty("tokens_on_a_line");
	    Assertion.Assert("tokens_on_a_line was expected to be an ArrayList", (o is ArrayList));
	    Assertion.Assert("tokens_on_a_line was expected to be an ArrayList with 2 elements", ((ArrayList)o).Count==2);

	    StringWriter writer = new StringWriter();
	    props.Save(writer, "header");
	}


    }
}

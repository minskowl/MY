using System;
using System.IO;
using System.Text;
using NUnit.Framework;
using Savchin.Data.CSV;
using Savchin.Data.CSV.Resources;

namespace DataTest.Csv
{
	[TestFixture()]
	public class CsvReaderTest
	{
		#region Argument validation tests

		#region Constructors

		[Test]
		[ExpectedException(typeof(ArgumentNullException))]
		public void ArgumentTestCtor1()
		{
        
			using (CsvReader csv = new CsvReader(null, false))
			{
			}
		}

		[Test]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void ArgumentTestCtor2()
		{
           using (CsvReader csv = new CsvReader(new StringReader(CsvReaderSampleData.SampleData1), false, 0))
			{
			}
		}

		[Test]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void ArgumentTestCtor3()
		{
			using (CsvReader csv = new CsvReader(new StringReader(CsvReaderSampleData.SampleData1), false, -1))
			{
			}
		}

		[Test]
		public void ArgumentTestCtor4()
		{
			using (CsvReader csv = new CsvReader(new StringReader(""), false, 123))
			{
				Assert.AreEqual(123, csv.BufferSize);
			}
		}

		#endregion

		#region Indexers

		[Test]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void ArgumentTestIndexer1()
		{
			using (CsvReader csv = new CsvReader(new StringReader(CsvReaderSampleData.SampleData1), false))
			{
				string s = csv[-1];
			}
		}

		[Test]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void ArgumentTestIndexer2()
		{
			using (CsvReader csv = new CsvReader(new StringReader(CsvReaderSampleData.SampleData1), false))
			{
				string s = csv[CsvReaderSampleData.SampleData1RecordCount];
			}
		}

		[Test]
		[ExpectedException(typeof(InvalidOperationException))]
		public void ArgumentTestIndexer3()
		{
			using (CsvReader csv = new CsvReader(new StringReader(CsvReaderSampleData.SampleData1), false))
			{
				string s = csv["asdf"];
			}
		}

		[Test]
		[ExpectedException(typeof(InvalidOperationException))]
		public void ArgumentTestIndexer4()
		{
			using (CsvReader csv = new CsvReader(new StringReader(CsvReaderSampleData.SampleData1), false))
			{
				string s = csv[CsvReaderSampleData.SampleData1Header0];
			}
		}

		[Test]
		[ExpectedException(typeof(ArgumentNullException))]
		public void ArgumentTestIndexer5()
		{
			using (CsvReader csv = new CsvReader(new StringReader(CsvReaderSampleData.SampleData1), false))
			{
				string s = csv[null];
			}
		}

		[Test]
		[ExpectedException(typeof(ArgumentNullException))]
		public void ArgumentTestIndexer6()
		{
			using (CsvReader csv = new CsvReader(new StringReader(CsvReaderSampleData.SampleData1), false))
			{
				string s = csv[string.Empty];
			}
		}

		[Test]
		[ExpectedException(typeof(ArgumentNullException))]
		public void ArgumentTestIndexer7()
		{
			using (CsvReader csv = new CsvReader(new StringReader(CsvReaderSampleData.SampleData1), true))
			{
				string s = csv[null];
			}
		}

		[Test]
		[ExpectedException(typeof(ArgumentNullException))]
		public void ArgumentTestIndexer8()
		{
			using (CsvReader csv = new CsvReader(new StringReader(CsvReaderSampleData.SampleData1), true))
			{
				string s = csv[string.Empty];
			}
		}

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void ArgumentTestIndexer9()
		{
			using (CsvReader csv = new CsvReader(new StringReader(CsvReaderSampleData.SampleData1), true))
			{
				string s = csv["asdf"];
			}
		}

		[Test]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void ArgumentTestIndexer10()
		{
			using (CsvReader csv = new CsvReader(new StringReader(CsvReaderSampleData.SampleData1), false))
			{
				string s = csv[-1, 0];
			}
		}

		#endregion

		#region CopyCurrentRecordTo

		[Test]
		[ExpectedException(typeof(ArgumentNullException))]
		public void ArgumentTestCopyCurrentRecordTo1()
		{
			using (CsvReader csv = new CsvReader(new StringReader(CsvReaderSampleData.SampleData1), false))
			{
				csv.CopyCurrentRecordTo(null);
			}
		}

		[Test]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void ArgumentTestCopyCurrentRecordTo2()
		{
			using (CsvReader csv = new CsvReader(new StringReader(CsvReaderSampleData.SampleData1), false))
			{
				csv.CopyCurrentRecordTo(new string[1], -1);
			}
		}

		[Test]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void ArgumentTestCopyCurrentRecordTo3()
		{
			using (CsvReader csv = new CsvReader(new StringReader(CsvReaderSampleData.SampleData1), false))
			{
				csv.CopyCurrentRecordTo(new string[1], 1);
			}
		}

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void ArgumentTestCopyCurrentRecordTo4()
		{
			using (CsvReader csv = new CsvReader(new StringReader(CsvReaderSampleData.SampleData1), false))
			{
				csv.ReadNextRecord();
				csv.CopyCurrentRecordTo(new string[CsvReaderSampleData.SampleData1RecordCount - 1], 0);
			}
		}

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void ArgumentTestCopyCurrentRecordTo5()
		{
			using (CsvReader csv = new CsvReader(new StringReader(CsvReaderSampleData.SampleData1), false))
			{
				csv.ReadNextRecord();
				csv.CopyCurrentRecordTo(new string[CsvReaderSampleData.SampleData1RecordCount], 1);
			}
		}

		#endregion

		#region MoveTo

		[Test]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void ArgumentTestMoveTo1()
		{
			using (CsvReader csv = new CsvReader(new StringReader(CsvReaderSampleData.SampleData1), false))
			{
				csv.MoveTo(-1);
			}
		}

		#endregion

		#endregion

		#region Parsing tests

		[Test]
		public void ParsingTest1()
		{
			const string data = "1\r\n\r\n1";

			using (CsvReader csv = new CsvReader(new StringReader(data), false))
			{
				Assert.IsTrue(csv.ReadNextRecord());
				Assert.AreEqual("1", csv[0]);

				Assert.IsTrue(csv.ReadNextRecord());
				Assert.AreEqual("1", csv[0]);

				Assert.IsFalse(csv.ReadNextRecord());
			}
		}

		[Test]
		public void ParsingTest2()
		{
			// ["Bob said, ""Hey!""",2, 3 ]
			const string data = "\"Bob said, \"\"Hey!\"\"\",2, 3 ";

			using (CsvReader csv = new CsvReader(new StringReader(data), false))
			{
				Assert.IsTrue(csv.ReadNextRecord());
				Assert.AreEqual(@"Bob said, ""Hey!""", csv[0]);
				Assert.AreEqual("2", csv[1]);
				Assert.AreEqual("3", csv[2]);

				Assert.IsFalse(csv.ReadNextRecord());
			}

			using (CsvReader csv = new CsvReader(new StringReader(data), false, ',', '"', '"', '#', false))
			{
				Assert.IsTrue(csv.ReadNextRecord());
				Assert.AreEqual(@"Bob said, ""Hey!""", csv[0]);
				Assert.AreEqual("2", csv[1]);
				Assert.AreEqual(" 3 ", csv[2]);

				Assert.IsFalse(csv.ReadNextRecord());
			}
		}

		[Test]
		public void ParsingTest3()
		{
			const string data = "1\r2\n";

			using (CsvReader csv = new CsvReader(new StringReader(data), false))
			{
				Assert.IsTrue(csv.ReadNextRecord());
				Assert.AreEqual("1", csv[0]);

				Assert.IsTrue(csv.ReadNextRecord());
				Assert.AreEqual("2", csv[0]);

				Assert.IsFalse(csv.ReadNextRecord());
			}
		}

		[Test]
		public void ParsingTest4()
		{
			const string data = "\"\n\r\n\n\r\r\",,\t,\n";

			using (CsvReader csv = new CsvReader(new StringReader(data), false))
			{
				Assert.IsTrue(csv.ReadNextRecord());

				Assert.AreEqual(4, csv.FieldCount);

				Assert.AreEqual("\n\r\n\n\r\r", csv[0]);
				Assert.AreEqual("", csv[1]);
				Assert.AreEqual("", csv[2]);
				Assert.AreEqual("", csv[3]);

				Assert.IsFalse(csv.ReadNextRecord());
			}
		}

		[Test]
		public void ParsingTest5()
		{
			Checkdata5(1024);

			// some tricky ones ...

			Checkdata5(1);
			Checkdata5(9);
			Checkdata5(14);
			Checkdata5(39);
			Checkdata5(166);
			Checkdata5(194);
		}

		[Test]
		public void ParsingTest5_RandomBufferSizes()
		{
			Random random = new Random();

			for (int i = 0; i < 1000; i++)
				Checkdata5(random.Next(1, 512));
		}

		public void Checkdata5(int bufferSize)
		{
			const string data = CsvReaderSampleData.SampleData1;

			try
			{
				using (CsvReader csv = new CsvReader(new StringReader(data), true, bufferSize))
				{
					CsvReaderSampleData.CheckSampleData1(csv, true);
				}
			}
			catch (Exception ex)
			{
				throw new Exception(string.Format("BufferSize={0}", bufferSize), ex);
			}
		}

		[Test]
		public void ParsingTest6()
		{
			using (CsvReader csv = new CsvReader(new StringReader("1,2"), false))
			{
				Assert.IsTrue(csv.ReadNextRecord());
				Assert.AreEqual("1", csv[0]);
				Assert.AreEqual("2", csv[1]);
				Assert.AreEqual(',', csv.Delimiter);
				Assert.AreEqual(0, csv.CurrentRecordIndex);
				Assert.AreEqual(2, csv.FieldCount);
				Assert.IsFalse(csv.ReadNextRecord());
			}
		}

		[Test]
		public void ParsingTest7()
		{
			using (CsvReader csv = new CsvReader(new StringReader("\r\n1\r\n"), false))
			{
				Assert.IsTrue(csv.ReadNextRecord());
				Assert.AreEqual(',', csv.Delimiter);
				Assert.AreEqual(0, csv.CurrentRecordIndex);
				Assert.AreEqual(1, csv.FieldCount);
				Assert.AreEqual("1", csv[0]);
				Assert.IsFalse(csv.ReadNextRecord());
			}
		}

		[Test]
		public void ParsingTest8()
		{
			const string data = "\"bob said, \"\"Hey!\"\"\",2, 3 ";

			using (CsvReader csv = new CsvReader(new StringReader(data), false, ',', '\"', '\"', '#', true))
			{
				Assert.IsTrue(csv.ReadNextRecord());
				Assert.AreEqual("bob said, \"Hey!\"", csv[0]);
				Assert.AreEqual("2", csv[1]);
				Assert.AreEqual("3", csv[2]);
				Assert.AreEqual(',', csv.Delimiter);
				Assert.AreEqual(0, csv.CurrentRecordIndex);
				Assert.AreEqual(3, csv.FieldCount);
				Assert.IsFalse(csv.ReadNextRecord());
			}
		}

		[Test]
		public void ParsingTest9()
		{
			const string data = ",";

			using (CsvReader csv = new CsvReader(new StringReader(data), false))
			{
				Assert.IsTrue(csv.ReadNextRecord());
				Assert.AreEqual(String.Empty, csv[0]);
				Assert.AreEqual(String.Empty, csv[1]);
				Assert.AreEqual(',', csv.Delimiter);
				Assert.AreEqual(0, csv.CurrentRecordIndex);
				Assert.AreEqual(2, csv.FieldCount);
				Assert.IsFalse(csv.ReadNextRecord());
			}
		}

		[Test]
		public void ParsingTest10()
		{
			const string data = "1\r2";

			using (CsvReader csv = new CsvReader(new StringReader(data), false))
			{
				Assert.IsTrue(csv.ReadNextRecord());
				Assert.AreEqual("1", csv[0]);
				Assert.AreEqual(0, csv.CurrentRecordIndex);
				Assert.AreEqual(1, csv.FieldCount);
				Assert.IsTrue(csv.ReadNextRecord());
				Assert.AreEqual("2", csv[0]);
				Assert.AreEqual(1, csv.CurrentRecordIndex);
				Assert.AreEqual(1, csv.FieldCount);
				Assert.IsFalse(csv.ReadNextRecord());
			}
		}

		[Test]
		public void ParsingTest11()
		{
			const string data = "1\n2";

			using (CsvReader csv = new CsvReader(new StringReader(data), false))
			{
				Assert.IsTrue(csv.ReadNextRecord());
				Assert.AreEqual("1", csv[0]);
				Assert.AreEqual(0, csv.CurrentRecordIndex);
				Assert.AreEqual(1, csv.FieldCount);
				Assert.IsTrue(csv.ReadNextRecord());
				Assert.AreEqual("2", csv[0]);
				Assert.AreEqual(1, csv.CurrentRecordIndex);
				Assert.AreEqual(1, csv.FieldCount);
				Assert.IsFalse(csv.ReadNextRecord());
			}
		}

		[Test]
		public void ParsingTest12()
		{
			const string data = "1\r\n2";

			using (CsvReader csv = new CsvReader(new StringReader(data), false))
			{
				Assert.IsTrue(csv.ReadNextRecord());
				Assert.AreEqual("1", csv[0]);
				Assert.AreEqual(0, csv.CurrentRecordIndex);
				Assert.AreEqual(1, csv.FieldCount);
				Assert.IsTrue(csv.ReadNextRecord());
				Assert.AreEqual("2", csv[0]);
				Assert.AreEqual(1, csv.CurrentRecordIndex);
				Assert.AreEqual(1, csv.FieldCount);
				Assert.IsFalse(csv.ReadNextRecord());
			}
		}

		[Test]
		public void ParsingTest13()
		{
			const string data = "1\r";

			using (CsvReader csv = new CsvReader(new StringReader(data), false))
			{
				Assert.IsTrue(csv.ReadNextRecord());
				Assert.AreEqual("1", csv[0]);
				Assert.AreEqual(0, csv.CurrentRecordIndex);
				Assert.AreEqual(1, csv.FieldCount);
				Assert.IsFalse(csv.ReadNextRecord());
			}
		}

		[Test]
		public void ParsingTest14()
		{
			const string data = "1\n";

			using (CsvReader csv = new CsvReader(new StringReader(data), false))
			{
				Assert.IsTrue(csv.ReadNextRecord());
				Assert.AreEqual("1", csv[0]);
				Assert.AreEqual(0, csv.CurrentRecordIndex);
				Assert.AreEqual(1, csv.FieldCount);
				Assert.IsFalse(csv.ReadNextRecord());
			}
		}

		[Test]
		public void ParsingTest15()
		{
			const string data = "1\r\n";

			using (CsvReader csv = new CsvReader(new StringReader(data), false))
			{
				Assert.IsTrue(csv.ReadNextRecord());
				Assert.AreEqual("1", csv[0]);
				Assert.AreEqual(0, csv.CurrentRecordIndex);
				Assert.AreEqual(1, csv.FieldCount);
				Assert.IsFalse(csv.ReadNextRecord());
			}
		}

		[Test]
		public void ParsingTest16()
		{
			const string data = "1\r2\n";

			using (CsvReader csv = new CsvReader(new StringReader(data), false, '\r', '"', '\"', '#', true))
			{
				Assert.IsTrue(csv.ReadNextRecord());
				Assert.AreEqual("1", csv[0]);
				Assert.AreEqual("2", csv[1]);
				Assert.AreEqual(0, csv.CurrentRecordIndex);
				Assert.AreEqual(2, csv.FieldCount);
				Assert.IsFalse(csv.ReadNextRecord());
			}
		}

		[Test]
		public void ParsingTest17()
		{
			const string data = "\"July 4th, 2005\"";

			using (CsvReader csv = new CsvReader(new StringReader(data), false))
			{
				Assert.IsTrue(csv.ReadNextRecord());
				Assert.AreEqual("July 4th, 2005", csv[0]);
				Assert.AreEqual(0, csv.CurrentRecordIndex);
				Assert.AreEqual(1, csv.FieldCount);
				Assert.IsFalse(csv.ReadNextRecord());
			}
		}

		[Test]
		public void ParsingTest18()
		{
			const string data = " 1";

			using (CsvReader csv = new CsvReader(new StringReader(data), false, ',', '\"', '\"', '#', false))
			{
				Assert.IsTrue(csv.ReadNextRecord());
				Assert.AreEqual(" 1", csv[0]);
				Assert.AreEqual(0, csv.CurrentRecordIndex);
				Assert.AreEqual(1, csv.FieldCount);
				Assert.IsFalse(csv.ReadNextRecord());
			}
		}

		[Test]
		public void ParsingTest19()
		{
			string data = String.Empty;

			using (CsvReader csv = new CsvReader(new StringReader(data), false))
			{
				Assert.IsFalse(csv.ReadNextRecord());
			}
		}

		[Test]
		public void ParsingTest20()
		{
			const string data = "user_id,name\r\n1,Bruce";

			using (CsvReader csv = new CsvReader(new StringReader(data), true))
			{
				Assert.IsTrue(csv.ReadNextRecord());
				Assert.AreEqual("1", csv[0]);
				Assert.AreEqual("Bruce", csv[1]);
				Assert.AreEqual(0, csv.CurrentRecordIndex);
				Assert.AreEqual(2, csv.FieldCount);
				Assert.AreEqual("1", csv["user_id"]);
				Assert.AreEqual("Bruce", csv["name"]);
				Assert.IsFalse(csv.ReadNextRecord());
				Assert.IsFalse(csv.ReadNextRecord());
			}
		}

		[Test]
		public void ParsingTest21()
		{
			const string data = "\"data \r\n here\"";

			using (CsvReader csv = new CsvReader(new StringReader(data), false, ',', '\"', '\"', '#', false))
			{
				Assert.IsTrue(csv.ReadNextRecord());
				Assert.AreEqual("data \r\n here", csv[0]);
				Assert.AreEqual(0, csv.CurrentRecordIndex);
				Assert.AreEqual(1, csv.FieldCount);
				Assert.IsFalse(csv.ReadNextRecord());
			}
		}

		[Test]
		public void ParsingTest22()
		{
			const string data = "\r\r\n1\r";

			using (CsvReader csv = new CsvReader(new StringReader(data), false, '\r', '\"', '\"', '#', false))
			{
				Assert.IsTrue(csv.ReadNextRecord());
				Assert.AreEqual(3, csv.FieldCount);

				Assert.AreEqual(String.Empty, csv[0]);
				Assert.AreEqual(String.Empty, csv[1]);
				Assert.AreEqual(String.Empty, csv[2]);
				Assert.AreEqual(0, csv.CurrentRecordIndex);
				Assert.IsTrue(csv.ReadNextRecord());
				Assert.AreEqual("1", csv[0]);
				Assert.AreEqual(String.Empty, csv[1]);
				Assert.AreEqual(1, csv.CurrentRecordIndex);
				Assert.IsFalse(csv.ReadNextRecord());
			}
		}

		[Test]
		public void ParsingTest23()
		{
			const string data = "\"double\"\"\"\"double quotes\"";

			using (CsvReader csv = new CsvReader(new StringReader(data), false, ',', '\"', '\"', '#', false))
			{
				Assert.IsTrue(csv.ReadNextRecord());
				Assert.AreEqual("double\"\"double quotes", csv[0]);
				Assert.AreEqual(0, csv.CurrentRecordIndex);
				Assert.AreEqual(1, csv.FieldCount);
				Assert.IsFalse(csv.ReadNextRecord());
			}
		}

		[Test]
		public void ParsingTest24()
		{
			const string data = "1\r";

			using (CsvReader csv = new CsvReader(new StringReader(data), false))
			{
				Assert.IsTrue(csv.ReadNextRecord());
				Assert.AreEqual("1", csv[0]);
				Assert.AreEqual(0, csv.CurrentRecordIndex);
				Assert.AreEqual(1, csv.FieldCount);
				Assert.IsFalse(csv.ReadNextRecord());
			}
		}

		[Test]
		public void ParsingTest25()
		{
			const string data = "1\r\n";

			using (CsvReader csv = new CsvReader(new StringReader(data), false))
			{
				Assert.IsTrue(csv.ReadNextRecord());
				Assert.AreEqual("1", csv[0]);
				Assert.AreEqual(0, csv.CurrentRecordIndex);
				Assert.AreEqual(1, csv.FieldCount);
				Assert.IsFalse(csv.ReadNextRecord());
			}
		}

		[Test]
		public void ParsingTest26()
		{
			const string data = "1\n";

			using (CsvReader csv = new CsvReader(new StringReader(data), false))
			{
				Assert.IsTrue(csv.ReadNextRecord());
				Assert.AreEqual("1", csv[0]);
				Assert.AreEqual(0, csv.CurrentRecordIndex);
				Assert.AreEqual(1, csv.FieldCount);
				Assert.IsFalse(csv.ReadNextRecord());
			}
		}

		[Test]
		public void ParsingTest27()
		{
			const string data = "'bob said, ''Hey!''',2, 3 ";

			using (CsvReader csv = new CsvReader(new StringReader(data), false, ',', '\'', '\'', '#', true))
			{
				Assert.IsTrue(csv.ReadNextRecord());
				Assert.AreEqual("bob said, 'Hey!'", csv[0]);
				Assert.AreEqual("2", csv[1]);
				Assert.AreEqual("3", csv[2]);
				Assert.AreEqual(',', csv.Delimiter);
				Assert.AreEqual(0, csv.CurrentRecordIndex);
				Assert.AreEqual(3, csv.FieldCount);
				Assert.IsFalse(csv.ReadNextRecord());
			}
		}

		[Test]
		public void ParsingTest28()
		{
			const string data = "\"data \"\" here\"";

			using (CsvReader csv = new CsvReader(new StringReader(data), false, ',', '\0', '\\', '#', false))
			{
				Assert.IsTrue(csv.ReadNextRecord());
				Assert.AreEqual("\"data \"\" here\"", csv[0]);
				Assert.AreEqual(0, csv.CurrentRecordIndex);
				Assert.AreEqual(1, csv.FieldCount);
				Assert.IsFalse(csv.ReadNextRecord());
			}
		}

		[Test]
		public void ParsingTest29()
		{
			string data = new String('a', 75) + "," + new String('b', 75);

			using (CsvReader csv = new CsvReader(new StringReader(data), false))
			{
				Assert.IsTrue(csv.ReadNextRecord());
				Assert.AreEqual(new String('a', 75), csv[0]);
				Assert.AreEqual(new String('b', 75), csv[1]);
				Assert.AreEqual(0, csv.CurrentRecordIndex);
				Assert.AreEqual(2, csv.FieldCount);
				Assert.IsFalse(csv.ReadNextRecord());
			}
		}

		[Test]
		public void ParsingTest30()
		{
			const string data = "1\r\n\r\n1";

			using (CsvReader csv = new CsvReader(new StringReader(data), false))
			{
				Assert.IsTrue(csv.ReadNextRecord());
				Assert.AreEqual("1", csv[0]);
				Assert.AreEqual(0, csv.CurrentRecordIndex);
				Assert.AreEqual(1, csv.FieldCount);
				Assert.IsTrue(csv.ReadNextRecord());
				Assert.AreEqual("1", csv[0]);
				Assert.AreEqual(1, csv.CurrentRecordIndex);
				Assert.AreEqual(1, csv.FieldCount);
				Assert.IsFalse(csv.ReadNextRecord());
			}
		}

		[Test]
		public void ParsingTest31()
		{
			const string data = "1\r\n# bunch of crazy stuff here\r\n1";

			using (CsvReader csv = new CsvReader(new StringReader(data), false, ',', '\"', '\"', '#', false))
			{
				Assert.IsTrue(csv.ReadNextRecord());
				Assert.AreEqual("1", csv[0]);
				Assert.AreEqual(0, csv.CurrentRecordIndex);
				Assert.AreEqual(1, csv.FieldCount);
				Assert.IsTrue(csv.ReadNextRecord());
				Assert.AreEqual("1", csv[0]);
				Assert.AreEqual(1, csv.CurrentRecordIndex);
				Assert.AreEqual(1, csv.FieldCount);
				Assert.IsFalse(csv.ReadNextRecord());
			}
		}

		[Test]
		public void ParsingTest32()
		{
			const string data = "\"1\",Bruce\r\n\"2\n\",Toni\r\n\"3\",Brian\r\n";

			using (CsvReader csv = new CsvReader(new StringReader(data), false, ',', '\"', '\"', '#', false))
			{
				Assert.IsTrue(csv.ReadNextRecord());
				Assert.AreEqual("1", csv[0]);
				Assert.AreEqual("Bruce", csv[1]);
				Assert.AreEqual(0, csv.CurrentRecordIndex);
				Assert.AreEqual(2, csv.FieldCount);
				Assert.IsTrue(csv.ReadNextRecord());
				Assert.AreEqual("2\n", csv[0]);
				Assert.AreEqual("Toni", csv[1]);
				Assert.AreEqual(1, csv.CurrentRecordIndex);
				Assert.AreEqual(2, csv.FieldCount);
				Assert.IsTrue(csv.ReadNextRecord());
				Assert.AreEqual("3", csv[0]);
				Assert.AreEqual("Brian", csv[1]);
				Assert.AreEqual(2, csv.CurrentRecordIndex);
				Assert.AreEqual(2, csv.FieldCount);
				Assert.IsFalse(csv.ReadNextRecord());
			}
		}

		[Test]
		public void ParsingTest33()
		{
			const string data = "\"double\\\\\\\\double backslash\"";

			using (CsvReader csv = new CsvReader(new StringReader(data), false, ',', '\"', '\\', '#', false))
			{
				Assert.IsTrue(csv.ReadNextRecord());
				Assert.AreEqual("double\\\\double backslash", csv[0]);
				Assert.AreEqual(0, csv.CurrentRecordIndex);
				Assert.AreEqual(1, csv.FieldCount);
				Assert.IsFalse(csv.ReadNextRecord());
			}
		}

		[Test]
		public void ParsingTest34()
		{
			const string data = "\"Chicane\", \"Love on the Run\", \"Knight Rider\", \"This field contains a comma, but it doesn't matter as the field is quoted\"\r\n" +
					  "\"Samuel Barber\", \"Adagio for Strings\", \"Classical\", \"This field contains a double quote character, \"\", but it doesn't matter as it is escaped\"";

			using (CsvReader csv = new CsvReader(new StringReader(data), false, ',', '\"', '\"', '#', true))
			{
				Assert.IsTrue(csv.ReadNextRecord());
				Assert.AreEqual("Chicane", csv[0]);
				Assert.AreEqual("Love on the Run", csv[1]);
				Assert.AreEqual("Knight Rider", csv[2]);
				Assert.AreEqual("This field contains a comma, but it doesn't matter as the field is quoted", csv[3]);
				Assert.AreEqual(0, csv.CurrentRecordIndex);
				Assert.AreEqual(4, csv.FieldCount);
				Assert.IsTrue(csv.ReadNextRecord());
				Assert.AreEqual("Samuel Barber", csv[0]);
				Assert.AreEqual("Adagio for Strings", csv[1]);
				Assert.AreEqual("Classical", csv[2]);
				Assert.AreEqual("This field contains a double quote character, \", but it doesn't matter as it is escaped", csv[3]);
				Assert.AreEqual(1, csv.CurrentRecordIndex);
				Assert.AreEqual(4, csv.FieldCount);
				Assert.IsFalse(csv.ReadNextRecord());
			}
		}

		[Test]
		public void ParsingTest35()
		{
			using (CsvReader csv = new CsvReader(new StringReader("\t"), false, '\t'))
			{
				Assert.AreEqual(2, csv.FieldCount);

				Assert.IsTrue(csv.ReadNextRecord());

				Assert.AreEqual(string.Empty, csv[0]);
				Assert.AreEqual(string.Empty, csv[1]);

				Assert.IsFalse(csv.ReadNextRecord());
			}
		}

		[Test]
		public void ParsingTest36()
		{
			using (CsvReader csv = new CsvReader(new StringReader(CsvReaderSampleData.SampleData1), true))
			{
				csv.SupportsMultiline = false;
				CsvReaderSampleData.CheckSampleData1(csv, true);
			}
		}

		[Test]
		public void ParsingTest37()
		{
			using (CsvReader csv = new CsvReader(new StringReader(CsvReaderSampleData.SampleData1), false))
			{
				csv.SupportsMultiline = false;
				CsvReaderSampleData.CheckSampleData1(csv, true);
			}
		}

		#endregion

		#region UnicodeParsing tests

		[Test]
		public void UnicodeParsingTest1()
		{
			// control characters and comma are skipped

			char[] raw = new char[65536 - 13];

			for (int i = 0; i < raw.Length; i++)
				raw[i] = (char) (i + 14);

			raw[44 - 14] = ' '; // skip comma

			string data = new string(raw);

			using (CsvReader csv = new CsvReader(new StringReader(data), false))
			{
				Assert.IsTrue(csv.ReadNextRecord());
				Assert.AreEqual(data, csv[0]);
				Assert.IsFalse(csv.ReadNextRecord());
			}
		}

		[Test]
		public void UnicodeParsingTest2()
		{
			byte[] buffer;

			string test = "M�nchen";

			using (MemoryStream stream = new MemoryStream())
			{
				using (TextWriter writer = new StreamWriter(stream, Encoding.Unicode))
				{
					writer.WriteLine(test);
				}

				buffer = stream.ToArray();
			}

			using (CsvReader csv = new CsvReader(new StreamReader(new MemoryStream(buffer), Encoding.Unicode, false), false))
			{
				Assert.IsTrue(csv.ReadNextRecord());
				Assert.AreEqual(test, csv[0]);
				Assert.IsFalse(csv.ReadNextRecord());
			}
		}

		[Test]
		public void UnicodeParsingTest3()
		{
			byte[] buffer;

			string test = "M�nchen";

			using (MemoryStream stream = new MemoryStream())
			{
				using (TextWriter writer = new StreamWriter(stream, Encoding.Unicode))
				{
					writer.Write(test);
				}

				buffer = stream.ToArray();
			}

			using (CsvReader csv = new CsvReader(new StreamReader(new MemoryStream(buffer), Encoding.Unicode, false), false))
			{
				Assert.IsTrue(csv.ReadNextRecord());
				Assert.AreEqual(test, csv[0]);
				Assert.IsFalse(csv.ReadNextRecord());
			}
		}

		#endregion

		#region FieldCount

		[Test]
		public void FieldCountTest1()
		{
			using (CsvReader csv = new CsvReader(new StringReader(CsvReaderSampleData.SampleData1), false))
			{
				CsvReaderSampleData.CheckSampleData1(csv, true);
			}
		}

		#endregion

		#region GetFieldHeaders

		[Test]
		public void GetFieldHeadersTest1()
		{
			using (CsvReader csv = new CsvReader(new StringReader(CsvReaderSampleData.SampleData1), false))
			{
				string[] headers = csv.GetFieldHeaders();

				Assert.IsNotNull(headers);
				Assert.AreEqual(0, headers.Length);
			}
		}

		[Test]
		public void GetFieldHeadersTest2()
		{
			using (CsvReader csv = new CsvReader(new StringReader(CsvReaderSampleData.SampleData1), true))
			{
				string[] headers = csv.GetFieldHeaders();

				Assert.IsNotNull(headers);
				Assert.AreEqual(CsvReaderSampleData.SampleData1RecordCount, headers.Length);

				Assert.AreEqual(CsvReaderSampleData.SampleData1Header0, headers[0]);
				Assert.AreEqual(CsvReaderSampleData.SampleData1Header1, headers[1]);
				Assert.AreEqual(CsvReaderSampleData.SampleData1Header2, headers[2]);
				Assert.AreEqual(CsvReaderSampleData.SampleData1Header3, headers[3]);
				Assert.AreEqual(CsvReaderSampleData.SampleData1Header4, headers[4]);
				Assert.AreEqual(CsvReaderSampleData.SampleData1Header5, headers[5]);

				Assert.AreEqual(0, csv.GetFieldIndex(CsvReaderSampleData.SampleData1Header0));
				Assert.AreEqual(1, csv.GetFieldIndex(CsvReaderSampleData.SampleData1Header1));
				Assert.AreEqual(2, csv.GetFieldIndex(CsvReaderSampleData.SampleData1Header2));
				Assert.AreEqual(3, csv.GetFieldIndex(CsvReaderSampleData.SampleData1Header3));
				Assert.AreEqual(4, csv.GetFieldIndex(CsvReaderSampleData.SampleData1Header4));
				Assert.AreEqual(5, csv.GetFieldIndex(CsvReaderSampleData.SampleData1Header5));
			}
		}

		#endregion

		#region CopyCurrentRecordTo

		[Test]
		[ExpectedException(typeof(InvalidOperationException))]
		public void CopyCurrentRecordToTest1()
		{
			using (CsvReader csv = new CsvReader(new StringReader(CsvReaderSampleData.SampleData1), false))
			{
				csv.CopyCurrentRecordTo(new string[CsvReaderSampleData.SampleData1RecordCount]);
			}
		}

		#endregion

		#region MoveTo tests

		[Test]
		public void MoveToTest1()
		{
			using (CsvReader csv = new CsvReader(new StringReader(CsvReaderSampleData.SampleData1), true))
			{
				for (int i = 0; i < CsvReaderSampleData.SampleData1RecordCount; i++)
				{
					csv.MoveTo(i);
					CsvReaderSampleData.CheckSampleData1(i, csv);
				}
			}
		}

		[Test]
		[ExpectedException(typeof(InvalidOperationException))]
		public void MoveToTest2()
		{
			using (CsvReader csv = new CsvReader(new StringReader(CsvReaderSampleData.SampleData1), true))
			{
				csv.MoveTo(1);
				csv.MoveTo(0);
			}
		}

		[Test]
		[ExpectedException(typeof(EndOfStreamException))]
		public void MoveToTest3()
		{
			using (CsvReader csv = new CsvReader(new StringReader(CsvReaderSampleData.SampleData1), true))
			{
				csv.MoveTo(CsvReaderSampleData.SampleData1RecordCount);
			}
		}

		#endregion

		#region Iteration tests

		[Test]
		public void IterationTest1()
		{
			using (CsvReader csv = new CsvReader(new StringReader(CsvReaderSampleData.SampleData1), true))
			{
				int index = 0;

				foreach (string[] record in csv)
				{
					CsvReaderSampleData.CheckSampleData1(csv.HasHeaders, index, record);
					index++;
				}
			}
		}

		#endregion

		#region Indexer tests

		[Test]
		public void IndexerTest1()
		{
			using (CsvReader csv = new CsvReader(new StringReader(CsvReaderSampleData.SampleData1), true))
			{
				for (int i = 0; i < CsvReaderSampleData.SampleData1RecordCount; i++)
				{
					string s = csv[i, 0];
					CsvReaderSampleData.CheckSampleData1(i, csv);
				}
			}
		}

		[Test]
		[ExpectedException(typeof(InvalidOperationException))]
		public void IndexerTest2()
		{
			using (CsvReader csv = new CsvReader(new StringReader(CsvReaderSampleData.SampleData1), true))
			{
				string s = csv[1, 0];
				s = csv[0, 0];
			}
		}

		[Test]
		[ExpectedException(typeof(EndOfStreamException))]
		public void IndexerTest3()
		{
			using (CsvReader csv = new CsvReader(new StringReader(CsvReaderSampleData.SampleData1), true))
			{
				string s = csv[CsvReaderSampleData.SampleData1RecordCount, 0];
			}
		}

		#endregion
	}
}
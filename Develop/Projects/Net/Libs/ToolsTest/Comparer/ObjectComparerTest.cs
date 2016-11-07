using System;
using NUnit.Framework;
using Savchin.Comparer;

namespace ToolsTest.Comparer
{
    [TestFixture]
    public class ObjectComparerTest
    {
        private Configuration conf = new Configuration { CompareMode = CompareMode.Marked, TrackDifference = true };
        private CompareObject GetObject()
        {
            CompareObject result = new CompareObject();
            result.Name = "Object1";

            result.LinkedObject.Name = "LinkedObject 1";
            result.LinkedObject.IntValue = 1;


            result.NestedObjects.Add("Nested1", new NestedObject("Nested 1", 1));
            result.NestedObjects.Add("Nested2", new NestedObject("Nested 2", 2));


            result.KeyObjects.Add(new NestedObject("Nested 1", 1));
            result.KeyObjects.Add(new NestedObject("Nested 2", 2));
            return result;
        }

        [Test]
        public void EqualTest()
        {
            var result = (ObjectResult)conf.Compare(typeof(CompareObject), GetObject(), GetObject());
            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsEquals);

            Assert.IsTrue(result.Results["Name"].IsEquals);
            Assert.IsTrue(result.Results["LinkedObject"].IsEquals);
            Assert.IsTrue(result.Results["NestedObjects"].IsEquals);

            DictionaryResult dictionaryResult = result.Results["NestedObjects"] as DictionaryResult;
            Assert.AreEqual(dictionaryResult["Nested1"].ResultType, ResultType.Equal);
            Assert.AreEqual(dictionaryResult["Nested2"].ResultType, ResultType.Equal);

            KeyEnumerableResult enumerableResult = result.Results["KeyObjects"] as KeyEnumerableResult;
            Assert.AreEqual(enumerableResult["1"].ResultType, ResultType.Equal);
            Assert.AreEqual(enumerableResult["2"].ResultType, ResultType.Equal);

        }
        [Test]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "Different type objects")]
        public void DifferentTypesTest()
        {
            conf.Compare(typeof(CompareObject), GetObject(), new OtherObject());
        }

        [Test]
        public void DifferentPrimitives()
        {
            CompareObject destination = GetObject();
            destination.Name = "Another Name";

            var result = (ObjectResult)conf.Compare(typeof(CompareObject), GetObject(), destination);
            Assert.IsNotNull(result);

            Assert.IsFalse(result.IsEquals);
            Assert.IsFalse(result.Results["Name"].IsEquals);

            Assert.IsTrue(result.Results["LinkedObject"].IsEquals);
            Assert.IsTrue(result.Results["NestedObjects"].IsEquals);
        }

        [Test]
        public void DifferentLinkedObjects()
        {
            CompareObject destination = GetObject();
            destination.LinkedObject.Name = "Another Name";

            ObjectResult result = (ObjectResult)conf.Compare(typeof(CompareObject), GetObject(), destination);
            Assert.IsNotNull(result);

            Assert.IsFalse(result.IsEquals);

            Assert.IsTrue(result.Results["Name"].IsEquals);
            ObjectResult linkedObjectResult = result.Results["LinkedObject"] as ObjectResult;
            Assert.IsFalse(linkedObjectResult.IsEquals);
            Assert.IsFalse(linkedObjectResult.Results["Name"].IsEquals);

            Assert.IsTrue(result.Results["NestedObjects"].IsEquals);
        }

        [Test]
        public void DictionaryNewItemTest()
        {
            CompareObject source = GetObject();
            source.NestedObjects.Add("New object", new NestedObject("New object", 3));

            ObjectResult result = (ObjectResult)conf.Compare(typeof(CompareObject), source, GetObject());

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsEquals);

            Assert.IsTrue(result.Results["Name"].IsEquals);
            Assert.IsTrue(result.Results["LinkedObject"].IsEquals);

            Assert.IsFalse(result.Results["NestedObjects"].IsEquals);

            DictionaryResult dictionaryResult = result.Results["NestedObjects"] as DictionaryResult;
            Assert.AreEqual(dictionaryResult["Nested1"].ResultType, ResultType.Equal);
            Assert.AreEqual(dictionaryResult["Nested2"].ResultType, ResultType.Equal);
            Assert.AreEqual(dictionaryResult["New object"].ResultType, ResultType.New);

        }

        [Test]
        public void DictionaryDeleteItemTest()
        {
            var source = GetObject();
            var destination = GetObject();
            destination.NestedObjects.Add("New object", new NestedObject("New object", 3));

            var result = (ObjectResult)conf.Compare(typeof(CompareObject), source, destination);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsEquals);

            Assert.IsTrue(result.Results["Name"].IsEquals);
            Assert.IsTrue(result.Results["LinkedObject"].IsEquals);

            Assert.IsFalse(result.Results["NestedObjects"].IsEquals);

            DictionaryResult dictionaryResult = result.Results["NestedObjects"] as DictionaryResult;
            Assert.AreEqual(dictionaryResult["Nested1"].ResultType, ResultType.Equal);
            Assert.AreEqual(dictionaryResult["Nested2"].ResultType, ResultType.Equal);
            Assert.AreEqual(dictionaryResult["New object"].ResultType, ResultType.Delete);

        }


        [Test]
        public void DictionaryNotEqualItemTest()
        {
            CompareObject destination = GetObject();
            destination.NestedObjects["Nested2"].IntValue = 444;
            ObjectResult result = (ObjectResult)conf.Compare(typeof(CompareObject), GetObject(), destination);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsEquals);

            Assert.IsTrue(result.Results["Name"].IsEquals);
            Assert.IsTrue(result.Results["LinkedObject"].IsEquals);

            Assert.IsFalse(result.Results["NestedObjects"].IsEquals);

            DictionaryResult dictionaryResult = result.Results["NestedObjects"] as DictionaryResult;
            Assert.AreEqual(dictionaryResult["Nested1"].ResultType, ResultType.Equal);
            Assert.AreEqual(dictionaryResult["Nested2"].ResultType, ResultType.NotEqual);

        }



        [Test]
        public void KeyListNewItemTest()
        {
            CompareObject source = GetObject();
            source.KeyObjects.Add(new NestedObject("New object", 3));

            ObjectResult result = (ObjectResult)conf.Compare(typeof(CompareObject), source, GetObject());

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsEquals);

            Assert.IsTrue(result.Results["Name"].IsEquals);
            Assert.IsTrue(result.Results["LinkedObject"].IsEquals);
            Assert.IsTrue(result.Results["NestedObjects"].IsEquals);

            Assert.IsFalse(result.Results["KeyObjects"].IsEquals);
            var dictionaryResult = result.Results["KeyObjects"] as KeyEnumerableResult;
            Assert.AreEqual(dictionaryResult["1"].ResultType, ResultType.Equal);
            Assert.AreEqual(dictionaryResult["2"].ResultType, ResultType.Equal);
            Assert.AreEqual(dictionaryResult["3"].ResultType, ResultType.New);

        }

        [Test]
        public void KeyLisDeleteItemTest()
        {
            var source = GetObject();
            var destination = GetObject();
            destination.KeyObjects.Add(new NestedObject("New object", 3));

            var result = (ObjectResult)conf.Compare(typeof(CompareObject), source, destination);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsEquals);

            Assert.IsTrue(result.Results["Name"].IsEquals);
            Assert.IsTrue(result.Results["LinkedObject"].IsEquals);
            Assert.IsTrue(result.Results["NestedObjects"].IsEquals);

            Assert.IsFalse(result.Results["KeyObjects"].IsEquals);
            var dictionaryResult = result.Results["KeyObjects"] as KeyEnumerableResult;
            Assert.AreEqual(dictionaryResult["1"].ResultType, ResultType.Equal);
            Assert.AreEqual(dictionaryResult["2"].ResultType, ResultType.Equal);
            Assert.AreEqual(dictionaryResult["3"].ResultType, ResultType.Delete);

        }


        [Test]
        public void KeyLisNotEqualItemTest()
        {
            CompareObject destination = GetObject();
            destination.KeyObjects[1].Name = "???";
            var result = (ObjectResult)conf.Compare(typeof(CompareObject), GetObject(), destination);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsEquals);

            Assert.IsTrue(result.Results["Name"].IsEquals);
            Assert.IsTrue(result.Results["LinkedObject"].IsEquals);
            Assert.IsTrue(result.Results["NestedObjects"].IsEquals);

            Assert.IsFalse(result.Results["KeyObjects"].IsEquals);
            var dictionaryResult = result.Results["KeyObjects"] as KeyEnumerableResult;
            Assert.AreEqual(dictionaryResult["1"].ResultType, ResultType.Equal);
            Assert.AreEqual(dictionaryResult["2"].ResultType, ResultType.NotEqual);

        }
    }
}

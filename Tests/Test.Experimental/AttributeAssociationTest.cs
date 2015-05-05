using System.Collections.Generic;
using System.Linq;
using en.AndrewTorski.CineOS.Logic.Model.Association;
using NUnit.Framework;

namespace en.AndrewTorski.CineOS.Test.Experimental
{
	[TestFixture]
	public class AttributeAssociationTest
	{
		[Test]
		public void Test_Linking_Objects()
		{
			const string associationName = "Test_Linking_Objects";
			//	Arrange
			var test1 = new TestClass1();
			var test2 = new TestClass2();
			var attribute = new AttributeExample();

			var attributeAssociation = new AttributeAssociation<TestClass2, TestClass1, AttributeExample>(associationName);

			//	Act
			attributeAssociation.Link(test1, test2, attribute);

			//	Assert
			var foundObjectCollectionForTest1 = attributeAssociation.GetLinkedObjects(test1);
			var foundObjectCollectionForTest2 = attributeAssociation.GetLinkedObjects(test2);

			var foundTupleCollectionForTest1 = attributeAssociation.GetLinkedAttributesAndObjects(test1);
			var foundTupleCollectionForTest2 = attributeAssociation.GetLinkedAttributesAndObjects(test2);

			var foundAttributeCollectionForTest1 = attributeAssociation.GetLinkedAttributes(test1);
			var foundAttributeCollectionForTest2 = attributeAssociation.GetLinkedAttributes(test2);

			Assert.That(foundObjectCollectionForTest1.FirstOrDefault(), Is.EqualTo(test2));
			Assert.That(foundObjectCollectionForTest2.FirstOrDefault(), Is.EqualTo(test1));

			var firstTuple = foundTupleCollectionForTest1.FirstOrDefault();
			var secondTuple = foundTupleCollectionForTest2.FirstOrDefault();

			Assert.That(firstTuple.Item1, Is.Not.Null);
			Assert.That(firstTuple.Item2, Is.Not.Null);

			Assert.That(firstTuple.Item1, Is.EqualTo(attribute));
			Assert.That(firstTuple.Item2, Is.EqualTo(test2));

			Assert.That(secondTuple.Item1, Is.Not.Null);
			Assert.That(secondTuple.Item2, Is.Not.Null);

			Assert.That(secondTuple.Item1, Is.EqualTo(attribute));
			Assert.That(secondTuple.Item2, Is.EqualTo(test1));

			Assert.That(foundAttributeCollectionForTest1.FirstOrDefault(), Is.EqualTo(attribute));
			Assert.That(foundAttributeCollectionForTest2.FirstOrDefault(), Is.EqualTo(attribute));
		}

		[Test]
		public void Test_Linking_Multiple_Objects_To_One()
		{
			const string associationName = "Test_Linking_Multiple_Objects_To_One";
			//	Arrange
			var firstObject = new TestClass1();
			var collectionOfSecondClassObjects = new List<TestClass2> {new TestClass2(), new TestClass2(), new TestClass2(), new TestClass2()};
			var collectionfOfCorrespondingAttributes = new List<AttributeExample> {new AttributeExample(), new AttributeExample(), new AttributeExample(), new AttributeExample()};

			var attributeAssociation = new AttributeAssociation<TestClass2, TestClass1, AttributeExample>(associationName);

			//	Act
			for (var i = 0; i < collectionOfSecondClassObjects.Count; i++)
			{
				attributeAssociation.Link(collectionOfSecondClassObjects[i], firstObject, collectionfOfCorrespondingAttributes[i]);
			}

			//	Assert
			var foundAttributesLinkedToFirstObject = attributeAssociation.GetLinkedAttributes(firstObject);
			var foundObjectsLinkedToFirstObject = attributeAssociation.GetLinkedObjects(firstObject);

			Assert.IsTrue(foundAttributesLinkedToFirstObject.SequenceEqual(collectionfOfCorrespondingAttributes));
			Assert.IsTrue(foundObjectsLinkedToFirstObject.SequenceEqual(collectionOfSecondClassObjects));
		}

		[Test]
		public void Test_Int_As_Attribute()
		{
			const string associationName = "Test_Int_As_Attribute";

			//	Arrange
			var firstObject = new TestClass1();
			var collectionOfSecondClassObjects = new List<TestClass2> {new TestClass2(), new TestClass2(), new TestClass2(), new TestClass2()};
			var collectionfOfCorrespondingAttributes = new List<int> {2, 2, 2, 2};

			var attributeAssociation = new AttributeAssociation<TestClass1, TestClass2, int>(associationName);

			//	Act
			for (var i = 0; i < collectionOfSecondClassObjects.Count; i++)
			{
				attributeAssociation.Link(collectionOfSecondClassObjects[i], firstObject, collectionfOfCorrespondingAttributes[i]);
			}

			//	Assert
			var sumOfAttributesLinkedWithFirstObject = attributeAssociation.GetLinkedAttributes(firstObject)
				.Sum();

			Assert.That(sumOfAttributesLinkedWithFirstObject, Is.EqualTo(8));
		}
	}
}
using en.AndrewTorski.CineOS.Logic.Model.Association;
using NUnit.Framework;

namespace en.AndrewTorski.CineOS.Test.Experimental
{
	[TestFixture]
	public class StandardAssociationTest
	{
		[Test]
		public void TestIfAssocationConstructorConstructsTuplesCorrectly()
		{
			//	Arrange
			var association = new StandardAssociation<Test1, Test2>("ownership");

			//	Act
			var type1 = association.Type1;
			var type2 = association.Type2;

			//	Assert
			Assert.That(type1 == typeof (Test1));
			Assert.That(type2 == typeof(Test2));
		}

		[Test]
		public void AssoctiaionConformsWithOneTypeOnlyTest()
		{
			//	Arrange
			var association = new StandardAssociation<Test1, Test2>("ownership");

			//	Act
			var doesTest1ConformAssociation = association.ConformsWith(typeof(Test1));
			var doesTest2ConformAssociation = association.ConformsWith(typeof(Test2));

			//	Assert
			Assert.That(doesTest1ConformAssociation);
			Assert.That(doesTest2ConformAssociation);
		}

		[Test]
		public void AssociationConformsWithTwoTypesTest()
		{
			//	Arrange
			var association = new StandardAssociation<Test1, Test2>("ownership");

			//	Act & Assert
			Assert.IsTrue(association.ConformsWith(typeof(Test1), typeof(Test2)));
		}

		[Test]
		public void AreObjectsLinkedTest()
		{
			//	Arrange
			var obj1 = new Test1();
			var obj2 = new Test2();
			var obj3 = new Test2();
			var obj4 = new Test2();

			var association = new StandardAssociation<Test1, Test2>("test");
			//	Act
			association.Link(obj1, obj2);
			association.Link(obj1, obj3);
			association.Link(obj1, obj4);

			//	Assert
			var associatedObjects = association.GetLinkedObjects(obj1);
			Assert.IsTrue(associatedObjects.Contains(obj2));
			Assert.IsTrue(associatedObjects.Contains(obj3));
			Assert.IsTrue(associatedObjects.Contains(obj4));
		}

		[Test]
		public void TestEqualsDifferentNames()
		{
			//	Arrange
			var association1ForNameFailureCase = new StandardAssociation<Test1, Test2>("ownership");
			var association2ForNameFailureCase = new StandardAssociation<Test1, Test2>("owner");

			//	Act
			var nameFailureCaseResult = association1ForNameFailureCase.Equals(association2ForNameFailureCase);
			//	Different names should result in false.
			//	Assert
			Assert.That(nameFailureCaseResult, Is.False);
		}

		[Test]
		public void TestEqualsReturnsTrueForDifferentPermutations()
		{
			//	Arrange
			const string name = "";
			var assocation12 = new StandardAssociation<Test1, Test2>(name);
			var assocation21 = new StandardAssociation<Test2, Test1>(name);
			var assocation12Second = new StandardAssociation<Test1, Test2>(name);
			var assocation21Second = new StandardAssociation<Test2, Test1>(name);

			//	Act
			var resultForComparing12Vs12 = assocation12.Equals(assocation12Second);
			var resultForComparing12Vs21 = assocation12.Equals(assocation21);
			var resultForComparing21Vs12 = assocation21.Equals(assocation12);
			var resultForComparing21Vs21 = assocation21.Equals(assocation21Second);

			//	Assert
			Assert.That(resultForComparing12Vs12, Is.True);
			Assert.That(resultForComparing12Vs21, Is.True);
			Assert.That(resultForComparing21Vs12, Is.True);
			Assert.That(resultForComparing21Vs21, Is.True);
		}

		class Test1   { }
		class Test2   { }
		class Test3  { }
		class Test4 { }
	}
}

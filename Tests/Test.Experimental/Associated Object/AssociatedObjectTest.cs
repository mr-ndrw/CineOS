using en.AndrewTorski.CineOS.Logic.Model.InterfaceAndBase;
using NUnit.Framework;

namespace en.AndrewTorski.CineOS.Test.Experimental.Associated_Object
{
	internal class TestClass1 : BusinessObject { }
	internal class TestClass2 : BusinessObject { }

	[TestFixture]
	public class AssociatedObjectTest
	{
		[Test]
		public void Test_If_Associations_Are_Registered_Correctly()
		{
			//	Act
			BusinessObject.RegisterAssociation<TestClass1, TestClass2>("test");
			BusinessObject.RegisterAssociation<TestClass1, TestClass2>("test1", 10, 10);
			BusinessObject.RegisterAssociation<TestClass1, TestClass2>("test2", 0, 10, 0, 10);

			//	Assert
			Assert.IsTrue(BusinessObject.ContainsAssociation("test"));
			Assert.IsTrue(BusinessObject.ContainsAssociation("test1"));
			Assert.IsTrue(BusinessObject.ContainsAssociation("test2"));
		}

		[Test]
		public void Test_If_Registered_Boundaries_Are_Correctly_Returned()
		{
			//	Arrange
			const int lowerFirst = 5, upperFirst = 15, lowerSecond = 3, upperSecond = 10;
			const string associationName = "test3";

			//	Act
			BusinessObject.RegisterAssociation<TestClass1, TestClass2>(associationName, lowerFirst, upperFirst, lowerSecond, upperSecond);


			var tuple = BusinessObject.GetAmountBoundariesForAssociation(associationName);

			//	Assert
			Assert.That(tuple.Item1, Is.EqualTo(lowerFirst));
			Assert.That(tuple.Item2, Is.EqualTo(upperFirst));
			Assert.That(tuple.Item3, Is.EqualTo(lowerSecond));
			Assert.That(tuple.Item4, Is.EqualTo(upperSecond));
		}

		[Test]
		public void Test_If_Objects_Are_Linked()
		{
			//	Arrange
			const string associationName = "test4";
			var test1 = new TestClass1();
			var test2 = new TestClass2();
			BusinessObject.RegisterAssociation<TestClass1, TestClass2>(associationName);

			//	Act
			test1.Link(associationName, test2);

			//	Assert
			var collectionOfLinkedObjectsForTest1 = test1.GetLinkedObjects(associationName);
			var collectionOfLinkedObjectsForTest2 = test2.GetLinkedObjects(associationName);

			var does_Collection_Of_Linked_Objects_For_Test1_Contain_Test2_Object = collectionOfLinkedObjectsForTest1.Contains(test2);
			var does_Collection_Of_Linked_Objects_For_Test2_Contain_Test1_Object = collectionOfLinkedObjectsForTest2.Contains(test1);

			Assert.IsTrue(does_Collection_Of_Linked_Objects_For_Test1_Contain_Test2_Object);
			Assert.IsTrue(does_Collection_Of_Linked_Objects_For_Test2_Contain_Test1_Object);
		}

		public void Test_If_Recurrent_Associations_Between_Objects_Are_Constructed_Correctly()
		{
			//	Arrange
			//	Schema for this scenario
			//        __________
			//       | inf      |
			//	   -------______|
			//	   |Test1| 0..1
			//     -------	chief
			//
			//
			//	Act
			//	Assert
		}

		
	}
}

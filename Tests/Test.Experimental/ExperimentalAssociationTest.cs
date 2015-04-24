using System;
using en.AndrewTorski.CineOS.Logic.Model;
using en.AndrewTorski.CineOS.Logic.Model.InterfaceAndBase;
using NUnit.Framework;

namespace Test.Experimental
{
	[TestFixture]
	public class ExperimentalAssociationTest
	{
		[Test]
		public void TestIfAssocationConstructorConstructsTuplesCorrectly()
		{
			//	Arrange
			var association = new Asso("ownership", typeof(Test1), typeof(Test2));

			//	Act
			var type1 = association.Type1;
			var type2 = association.Type2;

			//	Assert
			Assert.That(type1 == typeof (Test1));
			Assert.That(type2 == typeof(Test2));
		}

		[Test]
		public void TestEqualsDifferentNames()
		{
			//	Arrange
			var association1ForNameFailureCase = new Asso("ownership", typeof(Test1), typeof(Test2));
			var association2ForNameFailureCase = new Asso("owner"    , typeof(Test1), typeof(Test2));

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
			var assocation12 = new Asso(name, typeof(Test1), typeof(Test2));
			var assocation21 = new Asso(name, typeof(Test2), typeof(Test1));
			var assocation12Second = new Asso(name, typeof(Test1), typeof(Test2));
			var assocation21Second = new Asso(name, typeof(Test2), typeof(Test1));

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

		class Test1 : AssociatedObject { }
		class Test2 : AssociatedObject { }
		class Test3 : AssociatedObject { }
		class Test4 : AssociatedObject { }
	}
}

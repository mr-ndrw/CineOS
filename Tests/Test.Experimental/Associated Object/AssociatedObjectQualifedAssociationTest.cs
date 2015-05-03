using System.Collections.Generic;
using System.Linq.Expressions;
using en.AndrewTorski.CineOS.Logic.Model.Associations;
using en.AndrewTorski.CineOS.Logic.Model.Exceptions;
using en.AndrewTorski.CineOS.Logic.Model.InterfaceAndBase;
using NUnit.Framework;

namespace en.AndrewTorski.CineOS.Test.Experimental.Associated_Object
{
	[TestFixture]
	public class AssociatedObjectQualifedAssociationTest
	{
		private static readonly IEqualityComparer<Qualifier> QualifierEqualityComparer = new QulifierEqualityComparer();

		[Test]
		public void Test_If_Qualified_Registration_Methods_Works()
		{
			//	Arrange
			AssociatedObject.RegisterQualifiedAssociation<Identifier, Identifiable, Qualifier>("Test1", 1,  1, QualifierEqualityComparer);
	
			//	Act & Assert
			Assert.That(AssociatedObject.DoesAssociationExist("Test1"));
		}

		[Test]
		public void Test_Linking_Objects_With_Qualifier()
		{
			//	Arrange
			AssociatedObject.RegisterQualifiedAssociation<Identifier, Identifiable, Qualifier>("Test2", 1, 1, QualifierEqualityComparer);

			var identifier = new Identifier();
			var identifiable = new Identifiable {X = 2, Y = 2};
			var qualifier = new Qualifier {X = 2, Y = 2};

			//	Act
			identifier.Link<Qualifier>("Test2", identifiable, qualifier);

			//	Assert
			var foundObjects = identifier.GetQualifiedLinkedObject("Test2", new Qualifier{X=2, Y=2});
			Assert.IsTrue(foundObjects.Contains(identifiable));
		}

		[Test]
		public void Test_Linking_Objects_With_Qualifier_Wrong_Qualifier_Objects_Not_Found()
		{
			//	Arrange
			AssociatedObject.RegisterQualifiedAssociation<Identifier, Identifiable, Qualifier>("Test3", 1, 1, QualifierEqualityComparer);

			var identifier = new Identifier();
			var identifiable = new Identifiable { X = 2, Y = 2 };
			var qualifier = new Qualifier { X = 2, Y = 2 };

			//	Act
			identifier.Link<Qualifier>("Test3", identifiable, qualifier);

			//	Assert
			var foundObjects = identifier.GetQualifiedLinkedObject("Test3", new Qualifier { X = 3, Y = 2 });
			Assert.IsFalse(foundObjects.Contains(identifiable));
		}


		public void Test_If_Exception_Is_Thrown_Upon_Registering_Association_Of_Already_Existing_Name()
		{
			//	Arrange & Act
			var wasExceptionThrown = false;
			try
			{
				AssociatedObject.RegisterQualifiedAssociation<Identifier, Identifiable, Qualifier>("Test3", 1, 1, QualifierEqualityComparer);
			}
			catch (AssociationOfProvidedNameAlreadyExistsException)
			{
				wasExceptionThrown = true;
			}

			Assert.IsTrue(wasExceptionThrown);
		}

		[Test]
		public void Test_If_TypesNotConformingWithAssociationException_Is_Thrown_Upon_Linking_Two_Identifiers()
		{
			//	Arrange
			AssociatedObject.RegisterQualifiedAssociation<Identifier, Identifiable, Qualifier>("Test4", 1, 1, QualifierEqualityComparer);

			var identifier1 = new Identifier();
			var identifier2 = new Identifier();
			var qualifier = new Qualifier();

			var wasTypesNotConformingWithAssociationExceptionThrown = false;

			//	Act
			try
			{
				identifier1.Link<Qualifier>("Test4", identifier2, qualifier);
			}
			catch (TypesNotConformingWithAssociationException)
			{
				wasTypesNotConformingWithAssociationExceptionThrown = true;
			}

			//	Assert
			Assert.IsTrue(wasTypesNotConformingWithAssociationExceptionThrown);
		}

		[Test]
		public void Test_If_TypesNotConformingWithAssociationException_Is_Thrown_Upon_Linking_Two_Identifiables()
		{
			//	Arrange
			AssociatedObject.RegisterQualifiedAssociation<Identifier, Identifiable, Qualifier>("Test4", 1, 1, QualifierEqualityComparer);

			var identifiable1 = new Identifier();
			var identifiable2 = new Identifier();
			var qualifier = new Qualifier();

			var wasTypesNotConformingWithAssociationExceptionThrown = false;

			//	Act
			try
			{
				identifiable1.Link<Qualifier>("Test4", identifiable2, qualifier);
			}
			catch (TypesNotConformingWithAssociationException)
			{
				wasTypesNotConformingWithAssociationExceptionThrown = true;
			}

			//	Assert
			Assert.IsTrue(wasTypesNotConformingWithAssociationExceptionThrown);
		}


		[Test]
		public void Scenario_1()
		{
			
		}
	}
}
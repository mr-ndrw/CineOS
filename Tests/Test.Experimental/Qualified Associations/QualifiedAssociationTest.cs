using System.Collections.Generic;
using System.Linq;
using en.AndrewTorski.CineOS.Logic.Model.Associations;
using en.AndrewTorski.CineOS.Logic.Model.Exceptions;
using en.AndrewTorski.CineOS.Test.Experimental;
using NUnit.Framework;

namespace en.AndrewTorski.CineOS.Test.Experimental.Qualified_Associations
{

	[TestFixture]
	public class QualifiedAssociationTest
	{
		private static readonly IEqualityComparer<Qualifier> QualifierEqualityComparer = new QulifierEqualityComparer();

		[Test]
		public void Test_If_Provided_EqualityComparer_GetHashCode_Returns_Same_Result_For_Similar_Objects()
		{
			//	Act
			var qualifier1 = new Qualifier {X = 1, Y = 1};
			var qualifier2 = new Qualifier {X = 1, Y = 1};
			var qualifier3 = new Qualifier {X = 234567, Y = 574};
			var qualifier4 = new Qualifier {X = 234567, Y = 574};
			var qualifier5 = new Qualifier {X = 1, Y = 2};
			var qualifier6 = new Qualifier {X = 2, Y = 1};

			IEqualityComparer<Qualifier> qualifierComparer = new QulifierEqualityComparer();

			//	Act
			var qualifier1HashCode = qualifierComparer.GetHashCode(qualifier1);
			var qualifier2HashCode = qualifierComparer.GetHashCode(qualifier2);
			var qualifier3HashCode = qualifierComparer.GetHashCode(qualifier3);
			var qualifier4HashCode = qualifierComparer.GetHashCode(qualifier4);
			var qualifier5HashCode = qualifierComparer.GetHashCode(qualifier5);
			var qualifier6HashCode = qualifierComparer.GetHashCode(qualifier6);

			//	Assert
			Assert.That(qualifier1HashCode, Is.EqualTo(qualifier2HashCode));
			Assert.That(qualifier3HashCode, Is.EqualTo(qualifier4HashCode));
			Assert.That(qualifier5HashCode, Is.Not.EqualTo(qualifier6HashCode));
		}

		[Test]
		public void Test_If_QualifiedAssociatons_Are_Constructed_Properly()
		{
			//	Arrange
			const int identifierUpperBound = 3;
			const int identifiableUpperBound = 4;
			const int identifierLowerBound = 0;
			const int identifiableLowerBound = 1;

			var qualifiedAssociation1 = new QualifiedAssociation<Identifier, Identifiable, Qualifier>("test", identifierUpperBound, identifiableUpperBound, QualifierEqualityComparer);
			var qualifiedAssociation2 = new QualifiedAssociation<Identifier, Identifiable, Qualifier>("test", identifierLowerBound, identifierUpperBound, identifiableLowerBound, identifiableUpperBound, QualifierEqualityComparer);

			//	Act
			var qualifiedAssociation1IdentifierUpperBound = qualifiedAssociation1.IdentifierUpperAmountBound;
			var qualifiedAssociation1IdentifierLowerBound = qualifiedAssociation1.IdentifierLowerAmountBound;
			var qualifiedAssociation1IdentifiableUpperBound = qualifiedAssociation1.IdentifiableUpperAmountBound;
			var qualifiedAssociationIdentifiableLowerBound = qualifiedAssociation1.IdentifiableLowerAmountBound;

			var qualifiedAssociation2IdentifierUpperBound = qualifiedAssociation2.IdentifierUpperAmountBound;
			var qualifiedAssociation2IdentifierLowerBound = qualifiedAssociation2.IdentifierLowerAmountBound;
			var qualifiedAssociation2IdentifiableUpperBound = qualifiedAssociation2.IdentifiableUpperAmountBound;
			var qualifiedAssociation2dentifiableLowerBound = qualifiedAssociation2.IdentifiableLowerAmountBound;

			//	Assert
			Assert.That(qualifiedAssociation1IdentifierUpperBound, Is.EqualTo(identifierUpperBound));
			Assert.That(qualifiedAssociation1IdentifierLowerBound, Is.EqualTo(0));
			Assert.That(qualifiedAssociation1IdentifiableUpperBound, Is.EqualTo(identifiableUpperBound));
			Assert.That(qualifiedAssociationIdentifiableLowerBound, Is.EqualTo(0));

			Assert.That(qualifiedAssociation2IdentifierUpperBound, Is.EqualTo(identifierUpperBound));
			Assert.That(qualifiedAssociation2IdentifierLowerBound, Is.EqualTo(identifierLowerBound));
			Assert.That(qualifiedAssociation2IdentifiableUpperBound, Is.EqualTo(identifiableUpperBound));
			Assert.That(qualifiedAssociation2dentifiableLowerBound, Is.EqualTo(identifiableLowerBound));
		}

		[Test]
		public void Test_If_Simple_Link_Operations_Work()
		{
			//	Arrange
			var identifier = new Identifier();
			var identifiable = new Identifiable {X = 2, Y = 2};
			var qualifier = new Qualifier {X = identifiable.X, Y = identifiable.Y};

			//	We'll create a qualified association which associates zero or one identifiable with zero or one identifier.
			var qualifiedAssociation = new QualifiedAssociation<Identifier, Identifiable, Qualifier>("test", 1, 1, QualifierEqualityComparer);

			//	Act
			//	Now link identifier with the identifiable using qualifier which will enable retriving the identifiable in an instant.
			qualifiedAssociation.Link(identifier, identifiable, qualifier);

			//	Having identifier and a qualifier which is equal in terms of values(but not referentially!!!), it should be possible to retrieve the identifiable declared above.
			var equalQualifier = new Qualifier {X = qualifier.X, Y = qualifier.Y};
			var foundObjects = qualifiedAssociation.GetQualifiedLinkedObjects(identifier, equalQualifier);
			var foundObject = foundObjects.FirstOrDefault();

			var foundIdentifiable = foundObject as Identifiable;

			//	Assert
			Assert.That(foundObject, Is.Not.Null);
			Assert.That(foundIdentifiable, Is.Not.Null);
			Assert.That(foundIdentifiable.X, Is.EqualTo(identifiable.X));
			Assert.That(foundIdentifiable.Y, Is.EqualTo(identifiable.Y));
		}

		[Test]
		public void Test_If_Inf_Identifier_And_0_1_Identifiable_QualifiedAssociation_Will_Throw_Exception_Upon_Linking_With_Same_Qualifier()
		{
			//	Arrange
			var identifier = new Identifier();
			var identifiable1 = new Identifiable {X = 2, Y = 2};
			var qualifier = new Qualifier {X = identifiable1.X, Y = identifiable1.Y};

			//	We'll create a qualified association which associates Many identifiable with zero or one identifier.
			var qualifiedAssociation = new QualifiedAssociation<Identifier, Identifiable, Qualifier>("test", int.MaxValue, 1, QualifierEqualityComparer);

			var wasInvalidQualifiedLinkingOperationExceptionThrown = false;

			/*	Basically what is done here is that we're trying to associate an identifiable with a qualifier which already exists in the
			 *	Identifier's scope of qualifiers. It should fail, as we explicilty stated during the creation of QualifiedAssociation that using
			 *	one qualifier we are able to get only one Identifiable.
			 */
			try
			{
				//	Act
				//	Now link identifier with the identifiable using qualifier which will enable retriving the identifiable in an instant.
				qualifiedAssociation.Link(identifier, identifiable1, qualifier);
				//	And try doing it again.
				qualifiedAssociation.Link(identifier, identifiable1, qualifier);
			}
			catch (InvalidQualifiedLinkingOperationException)
			{
				wasInvalidQualifiedLinkingOperationExceptionThrown = true;
			}

			//	Assert
			Assert.IsTrue(wasInvalidQualifiedLinkingOperationExceptionThrown, "InvalidQualifiedLinkingOperationException was not thrown when!");
		}

		[Test]
		public void Test_0_1_Identifier_And_Inf_Identifiable_QualifiedAssociation_Using_Only_1_Qualifier()
		{
			//	Arrange
			var identifier1 = new Identifier();

			var identifiable1 = new Identifiable {X = 2, Y = 2};
			var identifiable2 = new Identifiable {X = 2, Y = 2};
			var identifiable3 = new Identifiable {X = 2, Y = 2};
			var identifiable4 = new Identifiable {X = 2, Y = 2};
			var identifiable5 = new Identifiable {X = 2, Y = 2};

			var qualifier = new Qualifier {X = identifiable1.X, Y = identifiable1.Y};

			//	Below association says that using one Qualifier we are able to find many identifiables, but one Identifiable can only be linked
			//	to one Identifier.
			var qualifiedAssociation = new QualifiedAssociation<Identifier, Identifiable, Qualifier>("test", 0, 1, 0, int.MaxValue, QualifierEqualityComparer);

			var listOfIdentifiables = new List<Identifiable> {identifiable1, identifiable2, identifiable3, identifiable4, identifiable5};

			//	Act
			//	Link identifier1 with identifiables1-5
			qualifiedAssociation.Link(identifier1, identifiable1, qualifier);
			qualifiedAssociation.Link(identifier1, identifiable2, qualifier);
			qualifiedAssociation.Link(identifier1, identifiable3, qualifier);
			qualifiedAssociation.Link(identifier1, identifiable4, qualifier);
			qualifiedAssociation.Link(identifier1, identifiable5, qualifier);

			//	Now using our identifier1 and non-referentially equal qualifier retrieve all the identifiables.
			var equalQualifier = new Qualifier {X = qualifier.X, Y=qualifier.Y};
			var foundIdentifiables = qualifiedAssociation.GetQualifiedLinkedObjects(identifier1, equalQualifier);

			//	Assert
			//	GetQualifiedLinkedObjects should return the references to the objects which we created earlier, so SequenceEquals should return true when compared to the listOfIdentifiables.
			Assert.IsTrue(foundIdentifiables.SequenceEqual(listOfIdentifiables));
		}

		[Test]
		public void Test_0_1_Identifier_And_Inf_Identifiable_QualifiedAssociation_Using_Only_1_Qualifier_For_Two_Different_Identifiers()
		{
			//	Arrange
			var identifier1 = new Identifier();
			var identifier2 = new Identifier();

			var identifiable1 = new Identifiable {X = 2, Y = 2};
			var identifiable2 = new Identifiable {X = 2, Y = 2};
			var identifiable3 = new Identifiable {X = 2, Y = 2};
			var identifiable4 = new Identifiable {X = 2, Y = 2};
			var identifiable5 = new Identifiable {X = 2, Y = 2};

			var identifiable6 = new Identifiable {X = 2, Y = 2};
			var identifiable7 = new Identifiable {X = 2, Y = 2};
			var identifiable8 = new Identifiable {X = 2, Y = 2};
			var identifiable9 = new Identifiable {X = 2, Y = 2};
			var identifiable0 = new Identifiable {X = 2, Y = 2};

			var qualifier1 = new Qualifier {X = identifiable1.X, Y = identifiable1.Y};
			var qualifier2 = new Qualifier { X = identifiable1.X, Y = identifiable1.Y };
			//	Below association says that using one Qualifier we are able to find many identifiables, but one Identifiable can only be linked
			//	to one Identifier.
			var qualifiedAssociation = new QualifiedAssociation<Identifier, Identifiable, Qualifier>("test", 0, 1, 0, int.MaxValue, QualifierEqualityComparer);

			var listOfIdentifiablesForIdentifier1 = new List<Identifiable> {identifiable1, identifiable2, identifiable3, identifiable4, identifiable5};
			var listOfIdentifiablesForIdentifier2 = new List<Identifiable>{identifiable6, identifiable7, identifiable8, identifiable9, identifiable0};

			//	Act
			//	Link identifier1 with identifiables1-5
			qualifiedAssociation.Link(identifier1, identifiable1, qualifier1);
			qualifiedAssociation.Link(identifier1, identifiable2, qualifier1);
			qualifiedAssociation.Link(identifier1, identifiable3, qualifier1);
			qualifiedAssociation.Link(identifier1, identifiable4, qualifier1);
			qualifiedAssociation.Link(identifier1, identifiable5, qualifier1);

			qualifiedAssociation.Link(identifier2, identifiable6, qualifier2);
			qualifiedAssociation.Link(identifier2, identifiable7, qualifier2);
			qualifiedAssociation.Link(identifier2, identifiable8, qualifier2);
			qualifiedAssociation.Link(identifier2, identifiable9, qualifier2);
			qualifiedAssociation.Link(identifier2, identifiable0, qualifier2);


			//	Now using our identifier1 and non-referentially-equal qualifier retrieve all the identifiables.
			var equalQualifier1 = new Qualifier {X = qualifier1.X, Y = qualifier1.Y};
			var foundIdentifiables1 = qualifiedAssociation.GetQualifiedLinkedObjects(identifier1, equalQualifier1);
			//	Now using our identifier2 and non-referentially-equal qualifier retrieve all the identifiables.
			var equalQualifier2 = new Qualifier { X = qualifier2.X, Y = qualifier2.Y };
			var foundIdentifiables2 = qualifiedAssociation.GetQualifiedLinkedObjects(identifier2, equalQualifier2);

			//	Assert
			//	GetQualifiedLinkedObjects should return the references to the objects which we created earlier, so SequenceEquals should return true when compared to the listOfIdentifiables.
			Assert.IsTrue(foundIdentifiables1.SequenceEqual(listOfIdentifiablesForIdentifier1));
			//	By Analogy do the same for second list

			Assert.IsTrue(foundIdentifiables2.SequenceEqual(listOfIdentifiablesForIdentifier2));
		}
	}
}
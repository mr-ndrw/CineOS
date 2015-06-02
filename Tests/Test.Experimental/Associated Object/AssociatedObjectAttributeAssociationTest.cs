using System.Linq;
using System.Reflection;
using en.AndrewTorski.CineOS.Logic.Model.InterfaceAndBase;
using NUnit.Framework;

namespace en.AndrewTorski.CineOS.Test.Experimental.Associated_Object
{
	[TestFixture]
	public class AssociatedObjectAttributeAssociationTest
	{
		[Test]
		public void Test_Registering_Applications_And_Performing_Basic_Link()
		{
			var associationName = MethodBase.GetCurrentMethod().Name;

			//	Arrange
			var firstObject = new TestClass1();
			var secondObject = new TestClass2();
			var attribute = new AttributeExample();

			//	Act
			//	Many to many
			BusinessObject.RegisterAttributeAssociation<TestClass1, TestClass2, AttributeExample>(associationName);
			firstObject.LinkWithAttribute(associationName, secondObject, attribute);

			//	Assert
			var foundObjectFromFirstObjectPerspective = firstObject.GetLinkedObjects(associationName).FirstOrDefault();
			var foundObjectFromSecondObjectPerspective = secondObject.GetLinkedObjects(associationName).FirstOrDefault();

			var foundAttributeFromFirstObjectPerspective = firstObject.GetLinkedAttributes<AttributeExample>(associationName).FirstOrDefault();
			var foundAttributeFromSecondObjectPerspective = secondObject.GetLinkedAttributes<AttributeExample>(associationName)
				.FirstOrDefault();

			Assert.That(foundObjectFromFirstObjectPerspective, Is.EqualTo(secondObject));
			Assert.That(foundObjectFromSecondObjectPerspective, Is.EqualTo(firstObject));

			Assert.That(foundAttributeFromFirstObjectPerspective, Is.EqualTo(attribute));
			Assert.That(foundAttributeFromSecondObjectPerspective, Is.EqualTo(attribute));
		}
	}
}
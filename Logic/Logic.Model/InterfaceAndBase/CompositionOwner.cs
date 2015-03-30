using System;
using System.Collections.Generic;
using System.Linq;

namespace en.AndrewTorski.CineOS.Logic.Model.InterfaceAndBase
{
	public abstract class CompositionOwner
	{
		private readonly List<CompositionPart> _parts;

		private readonly static List<CompositionOwner> CompositionOwners;

		static CompositionOwner()
		{
			CompositionOwners = new List<CompositionOwner>();
		}

		protected CompositionOwner()
		{
			_parts = new List<CompositionPart>();
			CompositionOwners.Add(this);
		}

		public void AddPart(CompositionPart compositionPart)
		{
			if (CompositionOwners.Any(compositionOwner => compositionOwner.Contains(compositionPart)))
			{
				throw new Exception("Part may only have one owner!");
			}

			_parts.Add(compositionPart);
		}

		public bool Contains(CompositionPart compositionPart)
		{
			return _parts.Contains(compositionPart);
		}
	}
}

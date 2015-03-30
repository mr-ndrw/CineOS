namespace en.AndrewTorski.CineOS.Logic.Model.InterfaceAndBase
{
	public abstract class CompositionPart
	{
		protected CompositionPart(CompositionOwner compositionOwner)
		{
			CompositionOwner = compositionOwner;
			CompositionOwner.AddPart(this);
		}

		/// <summary>
		///		Gets the Owner of this Part.
		/// </summary>
		public CompositionOwner CompositionOwner { get; private set; }
	}
}

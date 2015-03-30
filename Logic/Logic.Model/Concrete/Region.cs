namespace en.AndrewTorski.CineOS.Logic.Model.Concrete
{

	/// <summary>
	///		Region of any kind.
	/// </summary>
	/// <remarks>
	///		May be either geographical or political.
	/// </remarks>
	public class Region
	{
		/// <summary>
		///		Unique identifier of the Region.
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		///		Name of the Region.
		/// </summary>
		public string Name { get; set; }


	}
}

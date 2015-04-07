using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace en.AndrewTorski.CineOS.Shared.HelperLibrary.EqualityComparer
{
	public class PairComparer<T1, T2> : IEqualityComparer<Tuple<T1, T2>>
	{
		public bool Equals(Tuple<T1, T2> x, Tuple<T1, T2> y)
		{
			return x.Item1.Equals(y.Item1) && x.Item2.Equals(y.Item2);
		}

		public int GetHashCode(Tuple<T1, T2> obj)
		{
			return 0;
		}
	}
}
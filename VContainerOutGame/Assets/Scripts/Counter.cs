using UnityEngine;

namespace DefaultNamespace
{

	public class Counter
	{
		public int Max { get; }
		public int Count => count;

		int count;

		public Counter(int max)
		{
			Max = max;
		}

		public void Add(int count)
		{
			this.count = Mathf.Min(this.count + count, Max);
		}

		public void Remove(int count)
		{
			this.count = Mathf.Max(this.count - count, 0);
		}
	}

}
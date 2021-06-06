using UniRx;

namespace DefaultNamespace
{

	public class TestViewModel
	{
		public IReactiveProperty<int> Count => count;
		readonly ReactiveProperty<int> count = new ReactiveProperty<int>();


		readonly Counter counter;

		public TestViewModel(Counter counter)
		{
			this.counter = counter;
		}
		
		public void OnPlusButton()
		{
			counter.Add(1);
			count.Value = counter.Count;
		}

		public void OnMinusButton()
		{
			counter.Remove(1);
			count.Value = counter.Count;
		}

	}

}
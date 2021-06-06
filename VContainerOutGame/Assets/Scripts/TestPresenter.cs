using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{

	public class TestPresenter : MonoBehaviour
	{
		[SerializeField]
		private Text count;

		[SerializeField]
		private Button plusButton;


		[SerializeField]
		private Button minusButton;


		TestViewModel model;


		public void Run(TestViewModel model)
		{
			this.model = model;

			model.Count
				.Subscribe(c => count.text = c.ToString())
				.AddTo(this);

			plusButton.OnClickAsObservable()
				.Subscribe(_ => model.OnPlusButton())
				.AddTo(this);

			minusButton.OnClickAsObservable()
				.Subscribe(_ => model.OnMinusButton())
				.AddTo(this);
		}
	}

}
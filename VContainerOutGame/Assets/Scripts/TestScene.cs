using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace DefaultNamespace
{

	public class TestScene : MonoBehaviour
	{

		[SerializeField]
		TestPresenter presenter;

		TestViewModel model;

		[Inject]
		public void Initialize(TestViewModel model)
		{
			this.model = model;
		}

		void Start()
		{
			presenter.Run(model);
		}
	}

}
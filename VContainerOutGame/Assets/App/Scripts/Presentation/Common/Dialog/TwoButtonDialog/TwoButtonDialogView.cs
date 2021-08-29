using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace App.Presentation
{
	public class TwoButtonDialogView : DialogViewBase
	{
		public class Factory
		{
			readonly TwoButtonDialogView prefab;

			[Inject]
			public Factory(TwoButtonDialogView prefab)
			{
				this.prefab = prefab;
			}

			public TwoButtonDialogView Create(Transform parent, TwoButtonDialogPresenter presenter)
			{
				var view = Instantiate(prefab, parent);
				view.Construct(presenter);
				return view;
			}
		}


		[SerializeField]
		Text title = default;

		[SerializeField]
		Text body = default;

		[SerializeField]
		Text okButtonText = default;

		[SerializeField]
		Text cancelButtonText = default;

		[SerializeField]
		Button okButton = default;

		[SerializeField]
		Button cancelButton = default;

		ITwoButtonDialogPresenter presenter;

		public void Construct(ITwoButtonDialogPresenter presenter)
		{
			this.presenter = presenter;
		}

		public async UniTask Prepare()
		{
			title.text = presenter.Title;
			body.text = presenter.Body;
			okButtonText.text = presenter.OkButtonName;
			cancelButtonText.text = presenter.CancelButtonName;

			okButton.OnClickAsObservable()
				.Subscribe(_ =>
				{
					presenter.Ok();
					presenter.CloseAsync().Forget();
				})
				.AddTo(this);
			cancelButton.OnClickAsObservable()
				.Subscribe(_ =>
				{
					presenter.Cancel();
					presenter.CloseAsync().Forget();
				})
				.AddTo(this);

			await UniTask.CompletedTask;
		}
	}
}
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace App.Presentation
{
	public class TwoButtonDialogView : DialogViewBase
	{
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

		TwoButtonDialogPresenter presenter;

		public void Construct(TwoButtonDialogPresenter presenter)
		{
			this.presenter = presenter;
		}

		public async UniTask Prepare()
		{
			await presenter.Prepare();

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
		}
	}
}
using App.Domain;
using App.Presentation;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;

namespace App.EntryPoint
{
	public class TwoButtonDialogFactory : ITwoButtonDialogFactory
	{
		readonly Transform parent;
		readonly TwoButtonDialogView.Factory viewFactory;
		readonly TwoButtonDialogPresenter.Factory presenterFactory;
		readonly TwoButtonDialogUseCase.Factory useCaseFactory;

		[Inject]
		public TwoButtonDialogFactory(Transform parent, TwoButtonDialogView.Factory viewFactory, TwoButtonDialogPresenter.Factory presenterFactory, TwoButtonDialogUseCase.Factory useCaseFactory)
		{
			this.parent = parent;
			this.viewFactory = viewFactory;
			this.presenterFactory = presenterFactory;
			this.useCaseFactory = useCaseFactory;
		}

		public async UniTask<TwoButtonDialogResult> Show(string title, string body, string okButtonName, string cancelButtonName)
		{
			var useCase = useCaseFactory.Create();
			var presenter = presenterFactory.Create(useCase, title, body, okButtonName, cancelButtonName);
			var view = viewFactory.Create(parent, presenter);

			useCase.SetDialogAnimator(view);

			await view.Prepare();

			return await useCase.WaitResultAsync();
		}
	}
}
using System;
using App.Domain;
using App.Presentation;
using Cysharp.Threading.Tasks;
using VContainer;

namespace App.EntryPoint
{
	public class TwoButtonDialogFactory : ITwoButtonDialogFactory
	{
		readonly Func<ITwoButtonDialogUseCase> useCaseFactory;
		readonly Func<ITwoButtonDialogUseCase, TwoButtonDialogPresenter.Input, TwoButtonDialogPresenter> presenterFactory;
		readonly Func<TwoButtonDialogPresenter, TwoButtonDialogView> viewFactory;

		[Inject]
		public TwoButtonDialogFactory(
			Func<ITwoButtonDialogUseCase> useCaseFactory,
			Func<ITwoButtonDialogUseCase, TwoButtonDialogPresenter.Input, TwoButtonDialogPresenter> presenterFactory,
			Func<TwoButtonDialogPresenter, TwoButtonDialogView> viewFactory
		)
		{
			this.useCaseFactory = useCaseFactory;
			this.presenterFactory = presenterFactory;
			this.viewFactory = viewFactory;
		}

		public async UniTask<TwoButtonDialogResult> Show(string title, string body, string okButtonName, string cancelButtonName)
		{
			var useCase = useCaseFactory();
			var presenter = presenterFactory(useCase, new TwoButtonDialogPresenter.Input(title, body, okButtonName, cancelButtonName));
			var view = viewFactory(presenter);

			useCase.SetDialogAnimator(view);

			await view.Prepare();

			return await useCase.WaitResultAsync();
		}
	}
}
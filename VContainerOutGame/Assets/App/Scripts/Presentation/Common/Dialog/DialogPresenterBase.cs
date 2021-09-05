using App.Domain;
using Cysharp.Threading.Tasks;

namespace App.Presentation
{
	public abstract class DialogPresenterBase : IDialogPresenter
	{
		readonly IDialogUseCase useCase;

		protected DialogPresenterBase(IDialogUseCase useCase)
		{
			this.useCase = useCase;
		}

		public async UniTask Prepare()
		{
			await UniTask.CompletedTask;
		}

		public async UniTask CloseAsync()
		{
			await useCase.CloseAsync();
		}
	}
}
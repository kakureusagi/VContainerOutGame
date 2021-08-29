using App.Domain;

namespace App.Presentation
{
	public interface ITwoButtonDialogPresenter : IDialogPresenter
	{
		string Title { get; }
		string Body { get; }
		string OkButtonName { get; }
		string CancelButtonName { get; }

		void Ok();
		void Cancel();
	}

	public class TwoButtonDialogPresenter : DialogPresenterBase, ITwoButtonDialogPresenter
	{
		public class Factory
		{
			public TwoButtonDialogPresenter Create(ITwoButtonDialogUseCase useCase, string title, string body, string okButtonName, string cancelButtonName)
			{
				return new TwoButtonDialogPresenter(useCase, title, body, okButtonName, cancelButtonName);
			}
		}


		public string Title { get; }
		public string Body { get; }
		public string OkButtonName { get; }
		public string CancelButtonName { get; }

		readonly ITwoButtonDialogUseCase useCase;

		public TwoButtonDialogPresenter(ITwoButtonDialogUseCase useCase, string title, string body, string okButtonName, string cancelButtonName) : base(useCase)
		{
			this.useCase = useCase;
			Title = title;
			Body = body;
			OkButtonName = okButtonName;
			CancelButtonName = cancelButtonName;
		}

		public void Ok()
		{
			useCase.SetResult(TwoButtonDialogResult.Ok);
		}

		public void Cancel()
		{
			useCase.SetResult(TwoButtonDialogResult.Cancel);
		}
	}
}
using App.Domain;

namespace App.Presentation
{
	public class TwoButtonDialogPresenter : DialogPresenterBase
	{
		public class Input
		{
			public string Title { get; }
			public string Body { get; }
			public string OkButtonName { get; }
			public string CancelButtonName { get; }

			public Input(string title, string body, string okButtonName, string cancelButtonName)
			{
				Title = title;
				Body = body;
				OkButtonName = okButtonName;
				CancelButtonName = cancelButtonName;
			}
		}

		public string Title => input.Title;
		public string Body => input.Body;
		public string OkButtonName => input.OkButtonName;
		public string CancelButtonName => input.CancelButtonName;

		readonly ITwoButtonDialogUseCase useCase;
		readonly Input input;

		public TwoButtonDialogPresenter(ITwoButtonDialogUseCase useCase, Input input) : base(useCase)
		{
			this.useCase = useCase;
			this.input = input;
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
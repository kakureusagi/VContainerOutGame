namespace App.Domain
{
	public interface ITwoButtonDialogUseCase : IDialogUseCase<TwoButtonDialogResult>
	{
	}

	public class TwoButtonDialogUseCase : DialogUseCaseBase<TwoButtonDialogResult>, ITwoButtonDialogUseCase
	{
		public class Factory
		{
			public TwoButtonDialogUseCase Create()
			{
				return new TwoButtonDialogUseCase();
			}
		}
	}
}
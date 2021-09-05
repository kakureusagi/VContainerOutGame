namespace App.Domain
{
	public interface ITwoButtonDialogUseCase : IDialogUseCase<TwoButtonDialogResult>
	{
	}

	public class TwoButtonDialogUseCase : DialogUseCaseBase<TwoButtonDialogResult>, ITwoButtonDialogUseCase
	{
	}
}
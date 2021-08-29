using Cysharp.Threading.Tasks;

namespace App.Domain
{
	public interface IDialogUseCase
	{
		UniTask CloseAsync();
		void SetDialogAnimator(IDialogAnimator dialogAnimator);
	}

	public interface IDialogUseCase<T> : IDialogUseCase
	{
		UniTask<T> WaitResultAsync();
		void SetResult(T result);
	}
}
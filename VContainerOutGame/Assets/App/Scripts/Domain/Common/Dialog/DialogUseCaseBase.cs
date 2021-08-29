using Cysharp.Threading.Tasks;

namespace App.Domain
{
	public abstract class DialogUseCaseBase<T> : IDialogUseCase<T>
	{
		readonly UniTaskCompletionSource<T> taskCompletionSource = new UniTaskCompletionSource<T>();
		IDialogAnimator dialogAnimator;
		T result;


		public void SetDialogAnimator(IDialogAnimator dialogAnimator)
		{
			this.dialogAnimator = dialogAnimator;
		}

		public async UniTask<T> WaitResultAsync()
		{
			return await taskCompletionSource.Task;
		}

		public async UniTask CloseAsync()
		{
			await dialogAnimator.CloseAsync();
			taskCompletionSource.TrySetResult(result);
		}

		public void SetResult(T result)
		{
			this.result = result;
		}
	}
}
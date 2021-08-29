using Cysharp.Threading.Tasks;

namespace App.Domain
{
	public interface IDialogAnimator
	{
		UniTask CloseAsync();
	}
}
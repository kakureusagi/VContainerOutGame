using Cysharp.Threading.Tasks;

namespace App.Presentation
{
	public interface IDialogPresenter
	{
		UniTask CloseAsync();
	}
}
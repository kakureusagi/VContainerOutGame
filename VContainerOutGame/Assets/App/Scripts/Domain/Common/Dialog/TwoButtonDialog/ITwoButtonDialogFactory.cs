using Cysharp.Threading.Tasks;

namespace App.Domain
{
	public interface ITwoButtonDialogFactory
	{
		UniTask<TwoButtonDialogResult> Show(string title, string body, string okButtonName, string cancelButtonName);
	}
}
using Cysharp.Threading.Tasks;

namespace App.Domain
{
	public interface ICharacterListDialogHelper
	{
		UniTask<bool> ConfirmSale();
	}
}
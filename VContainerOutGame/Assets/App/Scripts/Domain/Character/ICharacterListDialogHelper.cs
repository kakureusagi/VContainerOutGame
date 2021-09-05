using Cysharp.Threading.Tasks;

namespace App.Domain.Character
{
	public interface ICharacterListDialogHelper
	{
		UniTask<bool> ConfirmSale();
	}
}
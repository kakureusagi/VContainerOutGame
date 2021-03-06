using App.Domain;
using App.Domain.Character;
using Cysharp.Threading.Tasks;
using VContainer;

namespace App.Presentation.Character
{
	public class CharacterListDialogHelper : ICharacterListDialogHelper
	{
		readonly ITwoButtonDialogFactory twoButtonDialogFactory;

		[Inject]
		public CharacterListDialogHelper(ITwoButtonDialogFactory twoButtonDialogFactory)
		{
			this.twoButtonDialogFactory = twoButtonDialogFactory;
		}

		public async UniTask<bool> ConfirmSale()
		{
			var result = await twoButtonDialogFactory.Show("カード売却", "選択したカードを全て売却しますか？", "OK", "Cancel");
			return result == TwoButtonDialogResult.Ok;
		}
	}
}
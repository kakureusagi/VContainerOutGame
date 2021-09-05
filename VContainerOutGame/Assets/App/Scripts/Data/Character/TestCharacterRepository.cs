using System.Collections.Generic;
using App.Domain.Character;
using Cysharp.Threading.Tasks;

namespace App.Data.Character
{

	public class TestCharacterRepository : ICharacterRepository
	{

		public async UniTask<IReadOnlyList<CharacterCard>> GetOwnedCharacters()
		{
			// テストしたいデータを作成する
			// データはUnityのInspectorから受け取ってもいいし、ファイルから読み込んだりしてもいい
			// 適当に変更できて、短いサイクルでテストできると良い
			await UniTask.Delay(100);
			return new[]
			{
				new CharacterCard(1, "スライム", CharacterRarity.Common, 1, 11, 11),
				new CharacterCard(2, "ワーム", CharacterRarity.Rare, 2, 22, 33),
				new CharacterCard(3, "ハーピー", CharacterRarity.Epic, 3, 33, 33),
				new CharacterCard(4, "デーモン", CharacterRarity.Legendary, 4, 44, 44),
			};
		}

		public async UniTask SellCharacters(IEnumerable<CharacterCard> characters)
		{
			await UniTask.Delay(100);
		}
	}

}
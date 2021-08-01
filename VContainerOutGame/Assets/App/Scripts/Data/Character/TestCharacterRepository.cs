using System.Collections.Generic;
using App.Domain.Character;
using Cysharp.Threading.Tasks;

namespace App.Data.Character
{

	public class TestCharacterRepository : ICharacterRepository
	{

		public async UniTask<IReadOnlyList<CharacterEntity>> GetOwnedCharacters()
		{
			// テストしたいデータを作成する
			// データはUnityのInspectorから受け取ってもいいし、ファイルから読み込んだりしてもいい
			// 適当に変更できて、短いサイクルでテストできると良い
			await UniTask.Delay(100);
			return new[]
			{
				new CharacterEntity(1, "スライム", CharacterRarity.Common, 1, 11, 11),
				new CharacterEntity(2, "ワーム", CharacterRarity.Rare, 2, 22, 33),
				new CharacterEntity(3, "ハーピー", CharacterRarity.Epic, 3, 33, 33),
				new CharacterEntity(4, "デーモン", CharacterRarity.Legendary, 4, 44, 44),
			};
		}

		public async UniTask SellCharacters(IEnumerable<CharacterEntity> characters)
		{
			await UniTask.Delay(100);
		}
	}

}
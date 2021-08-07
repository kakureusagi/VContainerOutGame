using System.Linq;
using App.Data.App.Scripts.Data.Character;
using App.Domain.Character;

namespace App.Data.Translator
{

	/// <summary>
	/// 様々な情報からCharacterEntityへと変換する
	/// </summary>
	public class CharacterTranslator
	{
		/// <summary>
		/// 実際にはCSVやJSONやMessagePackに定義されたマスターデータを使うはず
		/// ここでは適当なものを用意する
		/// </summary>
		class CharacterTable
		{
			public CharacterTableRecord[] Records { get; set; }
		}

		class CharacterTableRecord
		{
			public int Id { get; set; }
			public string Name { get; set; }
			public int Rarity { get; set; }
			public int Hp { get; set; }
			public int Attack { get; set; }
		}

		readonly CharacterTable master = new CharacterTable
		{
			Records = new CharacterTableRecord[]
			{
				new CharacterTableRecord { Id = 1, Rarity = 10, Hp = 20, Attack = 10, Name = "ゴブリン" },
				new CharacterTableRecord { Id = 2, Rarity = 10, Hp = 10, Attack = 20, Name = "スライム" },
				new CharacterTableRecord { Id = 3, Rarity = 20, Hp = 30, Attack = 20, Name = "ウルフ" },
				new CharacterTableRecord { Id = 4, Rarity = 20, Hp = 20, Attack = 30, Name = "オーガ" },
				new CharacterTableRecord { Id = 5, Rarity = 30, Hp = 30, Attack = 30, Name = "デーモン" },
				new CharacterTableRecord { Id = 6, Rarity = 40, Hp = 40, Attack = 40, Name = "ゴッド" },
			}
		};


		public CharacterEntity[] Convert(CharacterListResponseBody response)
		{
			// マスターデータとAPIのレスポンス情報からゲームで使用するEntityを作成する

			var entities = new CharacterEntity[response.Characters.Length];
			for (var i = 0; i < entities.Length; i++)
			{
				var responseCharacter = response.Characters[i];
				var masterCharacter = master.Records.First(m => m.Id == responseCharacter.Id);

				entities[i] = new CharacterEntity(
					responseCharacter.Id,
					masterCharacter.Name,
					(CharacterRarity)masterCharacter.Rarity,
					responseCharacter.Level,
					responseCharacter.Level * masterCharacter.Hp, // 本題ではないので計算式は適当
					responseCharacter.Level * masterCharacter.Attack // 本題ではないので計算式は適当
				);
			}

			return entities;
		}
	}

}
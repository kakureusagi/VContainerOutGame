using System.Collections.Generic;
using Cysharp.Threading.Tasks;

namespace App.Domain.Character
{

	public interface ICharacterRepository
	{
		/// <summary>
		/// 所持しているキャラクター一覧を取得する
		/// </summary>
		/// <returns></returns>
		UniTask<IReadOnlyList<CharacterCard>> GetOwnedCharacters();

		/// <summary>
		/// キャラクターを売却する
		/// </summary>
		UniTask SellCharacters(IEnumerable<CharacterCard> characters);
	}

}
using System.Collections.Generic;
using System.Linq;
using App.Data.Common;
using App.Domain.Character;
using Cysharp.Threading.Tasks;

namespace App.Data.Character
{
	/// <summary>
	/// 本番に近い実装のRepository
	/// 通信処理ができないので動作しません
	/// </summary>
	public class CharacterRepository : ICharacterRepository
	{
		readonly IWebRequest webRequest;
		readonly CharacterTranslator characterTranslator;


		public CharacterRepository(IWebRequest webRequest, CharacterTranslator characterTranslator)
		{
			this.webRequest = webRequest;
			this.characterTranslator = characterTranslator;
		}

		public async UniTask<IReadOnlyList<CharacterEntity>> GetOwnedCharacters()
		{
			var requestContext = new RequestContext
			{
				Api = "character/list"
			};
			var response = await webRequest.SendAsync<RequestContext, CharacterListResponseContext>(requestContext);

			// 通信詳細やマスター構造などはロジックから隠蔽したいので、Entityに変換する
			return characterTranslator.Convert(response.Body);
		}

		public async UniTask SellCharacters(IEnumerable<CharacterEntity> characters)
		{
			var requestContext = new CharacterSellRequestContext
			{
				Api = "character/sell",
				Body = new CharacterSellRequestBody
				{
					CharacterIds = characters.Select(c => c.Id).ToArray(),
				}
			};
			await webRequest.SendAsync<CharacterSellRequestContext, ResponseContext>(requestContext);
		}
	}
}
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UniRx;

namespace App.Domain.Character
{
	public interface ICharacterListUseCase : ICharacterIconUseCase
	{
		IReadOnlyReactiveProperty<bool> CanSell { get; }
		IReadOnlyReactiveProperty<int> TotalSellPrice { get; }
		IReadOnlyReactiveProperty<IReadOnlyList<CharacterCard>> Characters { get; }

		UniTask Prepare();
		UniTask<bool> SellAsync();
	}
}
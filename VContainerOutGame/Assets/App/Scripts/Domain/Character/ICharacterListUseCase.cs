using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UniRx;

namespace App.Domain.Character
{

	public interface ICharacterListUseCase
	{
		IReadOnlyReactiveProperty<bool> CanSell { get; }
		IReadOnlyReactiveProperty<int> TotalSellPrice { get; }
		IReadOnlyReactiveProperty<IReadOnlyList<CharacterEntity>> Characters { get; }

		UniTask Prepare();
		UniTask SellAsync();
	}

}
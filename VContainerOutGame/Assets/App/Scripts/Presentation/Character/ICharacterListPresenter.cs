using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UniRx;

namespace App.Presentation.Character
{

	public interface ICharacterListPresenter
	{
		IReadOnlyReactiveProperty<IReadOnlyList<ICharacterIconPresenter>> IconPresenters { get; }
		IReadOnlyReactiveProperty<int> TotalPrice { get; }
		IReadOnlyReactiveProperty<bool> CanSell { get; }

		UniTask Prepare();
		UniTask SellSelectedCharacters();
	}

}
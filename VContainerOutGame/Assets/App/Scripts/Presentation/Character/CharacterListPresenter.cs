using System.Collections.Generic;
using App.Domain.Character;
using Cysharp.Threading.Tasks;
using UniRx;
using VContainer;

namespace App.Presentation.Character
{
	public class CharacterListPresenter
	{
		public IReadOnlyReactiveProperty<int> TotalPrice => useCase.TotalSellPrice;
		public IReadOnlyReactiveProperty<bool> CanSell => useCase.CanSell;
		public IReadOnlyReactiveProperty<IReadOnlyList<CharacterIconPresenter>> IconPresenters => iconPresenters;

		readonly ICharacterListUseCase useCase;
		readonly IResourceLoader resourceLoader;

		readonly ReactiveProperty<IReadOnlyList<CharacterIconPresenter>> iconPresenters = new ReactiveProperty<IReadOnlyList<CharacterIconPresenter>>();


		[Inject]
		public CharacterListPresenter(ICharacterListUseCase useCase, IResourceLoader resourceLoader)
		{
			this.useCase = useCase;
			this.resourceLoader = resourceLoader;
		}

		public async UniTask Prepare()
		{
			await useCase.Prepare();
			await PrepareIcons(useCase.Characters.Value);
		}

		public async UniTask SellSelectedCharacters()
		{
			if (!await useCase.SellAsync())
			{
				return;
			}

			await PrepareIcons(useCase.Characters.Value);
		}

		async UniTask PrepareIcons(IReadOnlyList<CharacterEntity> entities)
		{
			var presenters = new CharacterIconPresenter[entities.Count];
			for (var i = 0; i < presenters.Length; i++)
			{
				var presenter = new CharacterIconPresenter(entities[i], useCase, resourceLoader);
				presenter.Prepare();
				presenters[i] = presenter;
			}

			await resourceLoader.WaitForLoadFinish();
			iconPresenters.Value = presenters;
		}
	}
}
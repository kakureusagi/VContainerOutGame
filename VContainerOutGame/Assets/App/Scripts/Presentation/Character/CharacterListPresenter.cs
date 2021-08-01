using System;
using System.Collections.Generic;
using App.Domain.Character;
using Cysharp.Threading.Tasks;
using UniRx;
using VContainer;

namespace App.Presentation.Character
{

	public class CharacterListPresenter : ICharacterListPresenter
	{
		public IReadOnlyReactiveProperty<int> TotalPrice => useCase.TotalSellPrice;
		public IReadOnlyReactiveProperty<bool> CanSell => useCase.CanSell;
		public IReadOnlyReactiveProperty<IReadOnlyList<ICharacterIconPresenter>> IconPresenters => iconPresenters;

		readonly ICharacterListUseCase useCase;
		readonly Func<CharacterEntity, ICharacterIconPresenter> iconFactory;
		readonly IResourceLoader resourceLoader;

		readonly ReactiveProperty<IReadOnlyList<ICharacterIconPresenter>> iconPresenters = new ReactiveProperty<IReadOnlyList<ICharacterIconPresenter>>();


		[Inject]
		public CharacterListPresenter(ICharacterListUseCase useCase, Func<CharacterEntity, ICharacterIconPresenter> iconFactory, IResourceLoader resourceLoader)
		{
			this.useCase = useCase;
			this.iconFactory = iconFactory;
			this.resourceLoader = resourceLoader;
		}

		public async UniTask Prepare()
		{
			await useCase.Prepare();
			await PrepareIcons(useCase.Characters.Value);
		}

		public async UniTask SellSelectedCharacters()
		{
			await useCase.SellAsync();
			await PrepareIcons(useCase.Characters.Value);
		}

		async UniTask PrepareIcons(IReadOnlyList<CharacterEntity> entities)
		{
			var presenters = new ICharacterIconPresenter[entities.Count];
			for (var i = 0; i < presenters.Length; i++)
			{
				var presenter = iconFactory(entities[i]);
				presenter.Prepare();
				presenters[i] = presenter;
			}

			await resourceLoader.WaitForLoadFinish();
			iconPresenters.Value = presenters;
		}
	}

}
using App.Domain.Character;
using UniRx;
using UnityEngine;
using VContainer;

namespace App.Presentation.Character
{

	public class CharacterIconPresenter : ICharacterIconPresenter
	{
		public class Factory : ICharacterIconPresenter.IFactory
		{
			readonly ICharacterIconUseCase useCase;
			readonly IResourceLoader resourceLoader;

			[Inject]
			public Factory(ICharacterIconUseCase useCase, IResourceLoader resourceLoader)
			{
				this.useCase = useCase;
				this.resourceLoader = resourceLoader;
			}

			public ICharacterIconPresenter Create(CharacterEntity entity)
			{
				return new CharacterIconPresenter(entity, useCase, resourceLoader);
			}
		}
		
		
		public IReadOnlyReactiveProperty<bool> IsSelected => isSelected;

		public string Name => entity.Name;
		public CharacterRarity Rarity => entity.Rarity;
		public int Level => entity.Level;
		public int Hp => entity.Hp;
		public int Attack => entity.Attack;
		public Sprite Sprite => sprite;

		readonly CharacterEntity entity;
		readonly ICharacterIconUseCase useCase;
		readonly IResourceLoader resourceLoader;
		Sprite sprite;

		IReadOnlyReactiveProperty<bool> isSelected;


		CharacterIconPresenter(CharacterEntity entity, ICharacterIconUseCase useCase, IResourceLoader resourceLoader)
		{
			this.entity = entity;
			this.useCase = useCase;
			this.resourceLoader = resourceLoader;
		}

		public void Prepare()
		{
			var add = useCase.SelectedCharacters.ObserveAdd()
				.Where(e => e.Value == entity)
				.Select(_ => true);
			var remove = useCase.SelectedCharacters.ObserveRemove()
				.Where(e => e.Value == entity)
				.Select(_ => false);
			isSelected = add.Merge(remove).ToReadOnlyReactiveProperty(false);

			resourceLoader.Load<Sprite>(AssetBundlePath.GetCharacterIcon(entity), asset => sprite = asset);
		}

		public void OnClick()
		{
			if (isSelected.Value)
			{
				useCase.Unselect(entity);
			}
			else
			{
				useCase.Select(entity);
			}
		}
	}

}
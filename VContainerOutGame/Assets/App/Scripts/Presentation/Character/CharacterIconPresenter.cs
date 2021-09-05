using App.Domain.Character;
using UniRx;
using UnityEngine;

namespace App.Presentation.Character
{
	public class CharacterIconPresenter
	{
		public IReadOnlyReactiveProperty<bool> IsSelected => isSelected;

		public string Name => card.Name;
		public CharacterRarity Rarity => card.Rarity;
		public int Level => card.Level;
		public int Hp => card.Hp;
		public int Attack => card.Attack;
		public Sprite Sprite => sprite;

		readonly CharacterCard card;
		readonly ICharacterIconUseCase useCase;
		readonly IResourceLoader resourceLoader;
		Sprite sprite;

		IReadOnlyReactiveProperty<bool> isSelected;


		public CharacterIconPresenter(CharacterCard card, ICharacterIconUseCase useCase, IResourceLoader resourceLoader)
		{
			this.card = card;
			this.useCase = useCase;
			this.resourceLoader = resourceLoader;
		}

		public void Prepare()
		{
			var add = useCase.SelectedCharacters.ObserveAdd()
				.Where(e => e.Value == card)
				.Select(_ => true);
			var remove = useCase.SelectedCharacters.ObserveRemove()
				.Where(e => e.Value == card)
				.Select(_ => false);
			isSelected = add.Merge(remove).ToReadOnlyReactiveProperty(false);

			resourceLoader.Load<Sprite>(AssetBundlePath.GetCharacterIcon(card), asset => sprite = asset);
		}

		public void OnClick()
		{
			if (isSelected.Value)
			{
				useCase.Unselect(card);
			}
			else
			{
				useCase.Select(card);
			}
		}
	}
}
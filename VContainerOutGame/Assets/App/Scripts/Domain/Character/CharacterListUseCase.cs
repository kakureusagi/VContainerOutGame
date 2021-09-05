using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using UniRx;
using VContainer;

namespace App.Domain.Character
{
	public class CharacterListUseCase : ICharacterListUseCase
	{
		public IReadOnlyReactiveProperty<bool> CanSell => canSell;
		public IReadOnlyReactiveProperty<int> TotalSellPrice => totalSellPrice;
		public IReadOnlyReactiveProperty<IReadOnlyList<CharacterEntity>> Characters => characters;
		public IReadOnlyReactiveCollection<CharacterEntity> SelectedCharacters => selectedCharacters;

		readonly ReactiveProperty<bool> canSell = new ReactiveProperty<bool>();
		readonly ReactiveProperty<int> totalSellPrice = new ReactiveProperty<int>();
		readonly ReactiveProperty<IReadOnlyList<CharacterEntity>> characters = new ReactiveProperty<IReadOnlyList<CharacterEntity>>();
		readonly ReactiveCollection<CharacterEntity> selectedCharacters = new ReactiveCollection<CharacterEntity>();

		readonly CharacterPriceCalculator priceCalculator;
		readonly ICharacterRepository characterRepository;
		readonly ICharacterListDialogHelper dialogHelper;


		[Inject]
		public CharacterListUseCase(CharacterPriceCalculator priceCalculator, ICharacterRepository characterRepository, ICharacterListDialogHelper dialogHelper)
		{
			this.priceCalculator = priceCalculator;
			this.characterRepository = characterRepository;
			this.dialogHelper = dialogHelper;
		}

		public async UniTask Prepare()
		{
			var temp = await characterRepository.GetOwnedCharacters();
			characters.Value = temp;
		}

		public async UniTask<bool> SellAsync()
		{
			if (!canSell.Value)
			{
				throw new InvalidOperationException();
			}

			if (!await dialogHelper.ConfirmSale())
			{
				return false;
			}

			await characterRepository.SellCharacters(selectedCharacters.ToArray());

			var temp = new List<CharacterEntity>(characters.Value);
			foreach (var selectedCharacter in selectedCharacters)
			{
				temp.Remove(selectedCharacter);
			}

			characters.SetValueAndForceNotify(temp);
			selectedCharacters.Clear();
			totalSellPrice.Value = 0;
			canSell.Value = false;

			return true;
		}

		public void Select(CharacterEntity character)
		{
			if (character == null)
			{
				throw new ArgumentNullException(nameof(character));
			}

			if (!characters.Value.Contains(character))
			{
				throw new InvalidOperationException(nameof(character));
			}

			if (selectedCharacters.Contains(character))
			{
				return;
			}

			selectedCharacters.Add(character);
			totalSellPrice.Value += priceCalculator.CalculatePrice(character);
			canSell.Value = true;
		}

		public void Unselect(CharacterEntity character)
		{
			if (character == null)
			{
				throw new ArgumentNullException(nameof(character));
			}

			if (!characters.Value.Contains(character))
			{
				throw new InvalidOperationException(nameof(character));
			}

			if (!selectedCharacters.Contains(character))
			{
				return;
			}

			selectedCharacters.Remove(character);
			totalSellPrice.Value -= priceCalculator.CalculatePrice(character);
			canSell.Value = selectedCharacters.Any();
		}
	}
}
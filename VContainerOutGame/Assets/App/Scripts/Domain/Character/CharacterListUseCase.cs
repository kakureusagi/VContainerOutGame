using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using UniRx;
using VContainer;

namespace App.Domain.Character
{

	public class CharacterListUseCase : ICharacterListUseCase, ICharacterIconUseCase
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


		[Inject]
		public CharacterListUseCase(CharacterPriceCalculator priceCalculator, ICharacterRepository characterRepository)
		{
			this.priceCalculator = priceCalculator;
			this.characterRepository = characterRepository;
		}

		public async UniTask Prepare()
		{
			var temp = await characterRepository.GetOwnedCharacters();
			characters.Value = temp;
		}

		public async UniTask SellAsync()
		{
			if (!canSell.Value)
			{
				throw new InvalidOperationException();
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
		}

		public void Select(CharacterEntity character)
		{
			if (character == null)
			{
				throw new ArgumentNullException(nameof(character));
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

			selectedCharacters.Remove(character);
			totalSellPrice.Value -= priceCalculator.CalculatePrice(character);
			canSell.Value = selectedCharacters.Any();
		}
	}
}
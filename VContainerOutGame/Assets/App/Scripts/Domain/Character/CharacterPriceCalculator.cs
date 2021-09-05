using System;

namespace App.Domain.Character
{

	public class CharacterPriceCalculator
	{
		public int CalculatePrice(CharacterCard card)
		{
			if (card == null)
			{
				throw new ArgumentNullException(nameof(card));
			}

			var basePrice = card.Rarity switch
			{
				CharacterRarity.Common => 10,
				CharacterRarity.Rare => 20,
				CharacterRarity.Epic => 40,
				CharacterRarity.Legendary => 100,
				_ => throw new ArgumentOutOfRangeException()
			};

			return card.Level * basePrice;
		}
	}

}
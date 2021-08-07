using System;

namespace App.Domain.Character
{

	public class CharacterPriceCalculator
	{
		public int CalculatePrice(CharacterEntity entity)
		{
			if (entity == null)
			{
				throw new ArgumentNullException(nameof(entity));
			}

			var basePrice = entity.Rarity switch
			{
				CharacterRarity.Common => 10,
				CharacterRarity.Rare => 20,
				CharacterRarity.Epic => 40,
				CharacterRarity.Legendary => 100,
				_ => throw new ArgumentOutOfRangeException()
			};

			return entity.Level * basePrice;
		}
	}

}
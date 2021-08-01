using System;

namespace App.Domain.Character
{

	public class CharacterPriceCalculator
	{

		public int CalculatePrice(CharacterEntity character)
		{
			if (character == null)
			{
				throw new ArgumentNullException(nameof(character));
			}

			return character.Level * (int)character.Rarity;
		}
	}

}
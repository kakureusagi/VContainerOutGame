using App.Domain.Character;

namespace App.Presentation
{

	public static class SpriteName
	{

		public static string GetCharacterRarity(CharacterRarity rarity)
		{
			return $"rarity_{rarity.ToString().ToLower()}";
		}
	}

}
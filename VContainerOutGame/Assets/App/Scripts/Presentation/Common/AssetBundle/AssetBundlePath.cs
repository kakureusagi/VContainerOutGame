using App.Domain.Character;

namespace App.Presentation
{

	public static class AssetBundlePath
	{

		public static string GetCharacterIcon(CharacterCard card)
		{
			return $"Assets/App/AssetBundles/Character/chara_{card.Id}.png";
		}
	}

}
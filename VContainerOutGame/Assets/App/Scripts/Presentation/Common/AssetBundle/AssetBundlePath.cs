using App.Domain.Character;

namespace App.Presentation
{

	public static class AssetBundlePath
	{

		public static string GetCharacterIcon(CharacterEntity entity)
		{
			return $"Assets/App/AssetBundles/Character/chara_{entity.Id}.png";
		}
	}

}
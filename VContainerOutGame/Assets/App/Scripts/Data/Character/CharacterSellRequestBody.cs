namespace App.Data.Character
{

	public class CharacterSellRequestBody
	{
		public int[] CharacterIds { get; set; }
	}

	public class CharacterSellRequestContext : RequestContext<CharacterSellRequestBody>
	{

	}

}
namespace App.Data.Character
{
	public class CharacterListResponseContext : ResponseContext<CharacterListResponseBody>
	{
	}

	public class CharacterListResponseBody
	{
		public CharacterData[] Characters { get; set; }
	}

	public class CharacterData
	{
		public int Id { get; set; }
		public int Level { get; set; }
	}
}
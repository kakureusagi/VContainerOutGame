namespace App.Domain.Character
{

	/// <summary>
	/// キャラクター情報
	/// </summary>
	public class CharacterCard
	{

		public int Id { get; }
		public string Name { get; }
		public CharacterRarity Rarity { get; }
		public int Level { get; }
		public int Hp { get; }
		public int Attack { get; }

		public CharacterCard(int id, string name, CharacterRarity rarity, int level, int hp, int attack)
		{
			Id = id;
			Name = name;
			Rarity = rarity;
			Level = level;
			Hp = hp;
			Attack = attack;
		}

	}

}
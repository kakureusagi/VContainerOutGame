using App.Domain.Character;
using NUnit.Framework;

namespace App.Domain.Tests.Characters
{

	public class CharacterPriceCalculatorTests
	{
		CharacterPriceCalculator calculator;

		[SetUp]
		public void SetUp()
		{
			calculator = new CharacterPriceCalculator();
		}

		[TestCase(CharacterRarity.Common, 1, 10)]
		[TestCase(CharacterRarity.Common, 3, 30)]
		[TestCase(CharacterRarity.Rare, 1, 20)]
		[TestCase(CharacterRarity.Rare, 3, 60)]
		[TestCase(CharacterRarity.Epic, 1, 40)]
		[TestCase(CharacterRarity.Epic, 3, 120)]
		[TestCase(CharacterRarity.Legendary, 1, 100)]
		[TestCase(CharacterRarity.Legendary, 3, 300)]
		public void 正しく計算できる(CharacterRarity rarity, int level, int expected)
		{
			var entity = new CharacterEntity(1, "test_name", rarity, level, 99, 99);
			var price = calculator.CalculatePrice(entity);
			Assert.That(price, Is.EqualTo(expected));
		}
	}

}
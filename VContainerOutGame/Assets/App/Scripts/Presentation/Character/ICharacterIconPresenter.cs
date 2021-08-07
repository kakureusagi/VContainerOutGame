using App.Domain.Character;
using UniRx;
using UnityEngine;

namespace App.Presentation.Character
{

	public interface ICharacterIconPresenter
	{
		public interface IFactory
		{
			ICharacterIconPresenter Create(CharacterEntity entity);
		}

		public string Name { get; }
		public CharacterRarity Rarity { get; }
		public int Level { get; }
		public int Hp { get; }
		public int Attack { get; }
		public Sprite Sprite { get; }

		void Prepare();

		IReadOnlyReactiveProperty<bool> IsSelected { get; }

		void OnClick();
	}

}
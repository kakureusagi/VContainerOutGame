using UniRx;

namespace App.Domain.Character
{

	public interface ICharacterIconUseCase
	{
		void Select(CharacterCard character);

		void Unselect(CharacterCard character);

		IReadOnlyReactiveCollection<CharacterCard> SelectedCharacters { get; }
	}

}
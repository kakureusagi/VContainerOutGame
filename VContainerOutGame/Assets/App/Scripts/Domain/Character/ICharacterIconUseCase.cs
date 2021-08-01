using UniRx;

namespace App.Domain.Character
{

	public interface ICharacterIconUseCase
	{
		void Select(CharacterEntity character);
		
		void Unselect(CharacterEntity character);
		
		IReadOnlyReactiveCollection<CharacterEntity> SelectedCharacters { get; }
	}

}
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace App.Presentation.Character
{

	public class CharacterIconView : MonoBehaviour
	{

		[SerializeField]
		Text characterName = default;

		[SerializeField]
		Text level = default;

		[SerializeField]
		Text hp = default;

		[SerializeField]
		Text attack = default;

		[SerializeField]
		AtlasImage rarity = default;

		[SerializeField]
		Image selectionFrame = default;

		[SerializeField]
		Button button = default;

		[SerializeField]
		Image characterImage = default;


		ICharacterIconPresenter presenter;


		public void Construct(ICharacterIconPresenter presenter)
		{
			this.presenter = presenter;
		}

		public void Run()
		{
			characterName.text = presenter.Name;
			level.text = presenter.Level.ToString();
			hp.text = presenter.Hp.ToString();
			attack.text = presenter.Attack.ToString();
			rarity.UpdateSprite(SpriteName.GetCharacterRarity(presenter.Rarity));
			characterImage.sprite = presenter.Sprite;

			presenter.IsSelected
				.Subscribe(isSelected =>
				{
					
					selectionFrame.gameObject.SetActive(isSelected);
				})
				.AddTo(this);

			button.OnClickAsObservable()
				.Subscribe(_ => presenter.OnClick())
				.AddTo(this);
		}
	}

}
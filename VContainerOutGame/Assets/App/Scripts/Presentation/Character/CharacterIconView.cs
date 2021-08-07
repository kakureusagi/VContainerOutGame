using UniRx;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace App.Presentation.Character
{

	public class CharacterIconView : MonoBehaviour
	{
		public class Factory
		{
			readonly CharacterIconView prefab;

			[Inject]
			public Factory(CharacterIconView prefab)
			{
				this.prefab = prefab;
			}
			
			public CharacterIconView Create(Transform parent, ICharacterIconPresenter presenter)
			{
				var instance = Instantiate(prefab, parent);
				instance.Construct(presenter);
				return instance;
			}
		}
		

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


		void Construct(ICharacterIconPresenter presenter)
		{
			this.presenter = presenter;
		}

		public void Prepare()
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
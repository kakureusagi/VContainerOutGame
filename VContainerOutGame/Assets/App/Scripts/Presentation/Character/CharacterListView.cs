using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace App.Presentation.Character
{

	public class CharacterListView : MonoBehaviour
	{
		[SerializeField]
		GameObject iconRoot = default;

		[SerializeField]
		Button sellButton = default;

		[SerializeField]
		Image sellButtonMask = default;

		[SerializeField]
		Text totalPrice = default;


		ICharacterListPresenter presenter;
		CharacterIconView.Factory iconFactory;


		[Inject]
		public void Construct(ICharacterListPresenter presenter, CharacterIconView.Factory iconFactory)
		{
			this.presenter = presenter;
			this.iconFactory = iconFactory;
		}

		public async UniTask Prepare()
		{
			await presenter.Prepare();
			
			presenter.IconPresenters
				.Subscribe(icons =>
				{
					// パフォーマンスを上げるためにInstantiateするプレハブの数を制限したりしてもよいね
					iconRoot.DestroyChildren();

					foreach (var icon in icons)
					{
						var iconView = iconFactory.Create(iconRoot.transform, icon);
						iconView.Prepare();
					}
				})
				.AddTo(this);

			presenter.CanSell
				.Subscribe(canSell =>
				{
					sellButtonMask.enabled = !canSell;
					sellButton.interactable = canSell;
				})
				.AddTo(this);

			presenter.TotalPrice
				.Subscribe(price => totalPrice.text = price.ToString())
				.AddTo(this);

			sellButton.OnClickAsObservable()
				.Subscribe(_ => presenter.SellSelectedCharacters().Forget())
				.AddTo(this);
			
		}

	}

}
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

		[SerializeField]
		CharacterIconView iconPrefab = default;


		CharacterListPresenter presenter;


		[Inject]
		public void Construct(CharacterListPresenter presenter)
		{
			this.presenter = presenter;
		}

		public async UniTask Prepare()
		{
			await presenter.Prepare();

			presenter.IconPresenters
				.Subscribe(icons =>
				{
					iconRoot.DestroyChildren();

					foreach (var icon in icons)
					{
						var iconView = Instantiate(iconPrefab, iconRoot.transform);
						iconView.Construct(icon);
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
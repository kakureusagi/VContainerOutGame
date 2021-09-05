using App.Domain;
using App.Presentation;
using UnityEngine;
using VContainer;

namespace App.EntryPoint
{
	public class TwoButtonDialogLifeTimeScope : DialogLifeTimeScope
	{
		[SerializeField]
		TwoButtonDialogView viewPrefab = default;

		public override void Configure(IContainerBuilder builder, Transform parent)
		{
			builder.RegisterFactory<ITwoButtonDialogUseCase>(
				container => () => new TwoButtonDialogUseCase(),
				Lifetime.Transient
			);
			builder.RegisterFactory<ITwoButtonDialogUseCase, TwoButtonDialogPresenter.Input, TwoButtonDialogPresenter>(
				container => (useCase, input) => new TwoButtonDialogPresenter(useCase, input),
				Lifetime.Transient
			);
			builder.RegisterFactory<TwoButtonDialogPresenter, TwoButtonDialogView>(container =>
				{
					return presenter =>
					{
						var view = Instantiate(viewPrefab, parent);
						view.Construct(presenter);
						return view;
					};
				},
				Lifetime.Transient
			);

			builder.Register<ITwoButtonDialogFactory, TwoButtonDialogFactory>(Lifetime.Transient);
		}
	}
}
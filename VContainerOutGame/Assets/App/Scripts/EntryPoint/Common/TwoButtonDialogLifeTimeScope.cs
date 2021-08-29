using App.Domain;
using App.Presentation;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace App.EntryPoint
{
	public class TwoButtonDialogLifeTimeScope : DialogLifeTimeScope
	{
		[SerializeField]
		TwoButtonDialogView view = default;


		public override void Configure(IContainerBuilder builder, Transform parent)
		{
			builder.Register<TwoButtonDialogUseCase.Factory>(Lifetime.Transient);
			builder.Register<TwoButtonDialogPresenter.Factory>(Lifetime.Transient);
			builder.Register<TwoButtonDialogView.Factory>(Lifetime.Transient);
			builder.RegisterComponent(view);

			builder.Register<ITwoButtonDialogFactory, TwoButtonDialogFactory>(Lifetime.Transient)
				.WithParameter(parent);
		}
	}
}
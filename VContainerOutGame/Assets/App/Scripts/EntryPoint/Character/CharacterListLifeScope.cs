using App.Data.Character;
using App.Domain.Character;
using App.Presentation;
using App.Presentation.Character;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace App.EntryPoint
{
	public class CharacterListLifeScope : LifetimeScope
	{
		[SerializeField]
		CharacterListView view = default;

		[SerializeField]
		Transform dialogRoot = default;

		[SerializeField]
		TwoButtonDialogLifeTimeScope[] dialogLifeTimeScopes = default;


		protected override void Configure(IContainerBuilder builder)
		{
			base.Configure(builder);

			//
			// ここはこのシーンで管理するのではなくて、全体で定義するのが良さそう
			//
			builder.Register<IResourceLoader, EditorResourceLoader>(Lifetime.Singleton);

			//
			// Character List
			//
			builder.Register<ICharacterListUseCase, CharacterListUseCase>(Lifetime.Transient);
			builder.Register<CharacterListPresenter>(Lifetime.Transient);
			builder.RegisterComponent(view);

			builder.Register<ICharacterRepository, TestCharacterRepository>(Lifetime.Transient);
			builder.Register<CharacterPriceCalculator>(Lifetime.Transient);
			builder.Register<ICharacterListDialogHelper, CharacterListDialogHelper>(Lifetime.Transient);

			foreach (var scope in dialogLifeTimeScopes)
			{
				scope.Configure(builder, dialogRoot);
			}
		}
	}
}
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
		CharacterIconView iconView = default;

		[SerializeField]
		CharacterListView view = default;


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
			builder.Register<CharacterListUseCase>(Lifetime.Singleton)
				.AsImplementedInterfaces()
				.AsSelf();
			builder.Register<CharacterListPresenter>(Lifetime.Singleton)
				.AsImplementedInterfaces()
				.AsSelf();
			builder.RegisterComponent(view);
			builder.Register<ICharacterRepository, TestCharacterRepository>(Lifetime.Transient);
			builder.Register<CharacterPriceCalculator>(Lifetime.Transient);

			//
			// CharacterIcon
			//
			builder.RegisterComponent(iconView);
			builder.Register<CharacterIconView.Factory>(Lifetime.Transient);
			builder.Register<ICharacterIconPresenter.IFactory, CharacterIconPresenter.Factory>(Lifetime.Transient);
		}

	}

}
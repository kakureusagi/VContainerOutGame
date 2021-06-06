using DefaultNamespace;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class TestLifetimeScope : LifetimeScope
{
	[SerializeField]
	TestScene scene;
	
	protected override void Configure(IContainerBuilder builder)
	{
		builder.Register<Counter>(Lifetime.Scoped).WithParameter<int>(3);
		builder.Register<TestViewModel>(Lifetime.Scoped);
	}
}
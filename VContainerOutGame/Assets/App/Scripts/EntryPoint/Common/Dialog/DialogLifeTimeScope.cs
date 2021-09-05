using UnityEngine;
using VContainer;

namespace App.EntryPoint
{
	public abstract class DialogLifeTimeScope : MonoBehaviour
	{
		public abstract void Configure(IContainerBuilder builder, Transform parent);
	}
}
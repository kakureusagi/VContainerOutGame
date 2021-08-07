using System;
using Cysharp.Threading.Tasks;
using Object = UnityEngine.Object;

namespace App.Presentation
{

	public interface IResourceLoader
	{
		void Load<T>(string path, Action<T> callback) where T : Object;
		UniTask WaitForLoadFinish();
	}

}
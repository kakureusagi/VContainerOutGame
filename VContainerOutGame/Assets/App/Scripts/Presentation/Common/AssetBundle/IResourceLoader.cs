using System;
using Cysharp.Threading.Tasks;

namespace App.Presentation
{
	public interface IResourceLoader
	{
		void Load<T>(string path, Action<T> callback) where T : UnityEngine.Object;
		UniTask WaitForLoadFinish();
	}

}
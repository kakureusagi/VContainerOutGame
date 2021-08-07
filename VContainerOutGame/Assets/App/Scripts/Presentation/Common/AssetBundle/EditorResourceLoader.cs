using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEditor;
using Object = UnityEngine.Object;

namespace App.Presentation
{

	public class EditorResourceLoader : IResourceLoader
	{
		interface IRequest
		{
			void Load();
		}

		class LoadRequest<T> : IRequest where T : Object
		{
			readonly string path;
			readonly Action<T> callback;

			public LoadRequest(string path, Action<T> callback)
			{
				this.path = path;
				this.callback = callback;
			}

			public void Load()
			{
				callback(AssetDatabase.LoadAssetAtPath<T>(path));
			}
		}


		readonly List<IRequest> requests = new List<IRequest>();

		public void Load<T>(string path, Action<T> callback) where T : Object
		{
			requests.Add(new LoadRequest<T>(path, callback));
		}

		public async UniTask WaitForLoadFinish()
		{
			await UniTask.DelayFrame(1);

			// 実際は非同期読み込みになると思うけれど、Editorだと非同期読み込みが用意されてない
			foreach (var request in requests)
			{
				request.Load();
			}
		}
	}

}
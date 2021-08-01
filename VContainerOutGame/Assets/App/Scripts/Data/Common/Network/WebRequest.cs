using Cysharp.Threading.Tasks;

namespace App.Data.Common
{


	public class WebRequest : IWebRequest
	{
		public async UniTask<TResponse> SendAsync<TRequest, TResponse>(TRequest request)
		{
			// 通信処理は省略。適当に通信してるように時間だけかけておく。
			await UniTask.Delay(100);
			return default;
		}
	}

}
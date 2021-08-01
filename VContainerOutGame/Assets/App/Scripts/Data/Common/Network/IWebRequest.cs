using Cysharp.Threading.Tasks;

namespace App.Data.Common
{

	/// <summary>
	/// サーバーと通信する
	/// </summary>
	public interface IWebRequest
	{
		/// <summary>
		/// サーバーと通信する
		/// </summary>
		UniTask<TResponseContext> SendAsync<TRequestContext, TResponseContext>(TRequestContext request);
	}

}
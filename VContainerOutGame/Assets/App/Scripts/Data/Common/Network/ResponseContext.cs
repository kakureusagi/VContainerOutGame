namespace App.Data.Character
{

	public class ResponseContext
	{
		public int ErrorCode { get; set; }
	}

	public class ResponseContext<T> : RequestContext
	{

		public T Body { get; set; }
	}

}
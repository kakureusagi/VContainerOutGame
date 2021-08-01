namespace App.Data
{

	public class RequestContext
	{
		public string Api { get; set; }
	}

	public class RequestContext<T> : RequestContext
	{
		public T Body { get; set; }
	}

}
namespace Booted.Core.Abstractions
{
	public interface IBuilder<TResult>
	{
		TResult Build();
	}
}
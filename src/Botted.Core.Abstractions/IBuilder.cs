namespace Botted.Core.Abstractions
{
	public interface IBuilder<out TResult>
	{
		TResult Build();
	}
}
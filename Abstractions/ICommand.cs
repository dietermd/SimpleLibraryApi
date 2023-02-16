using MediatR;

namespace SimpleLibraryApi.Abstractions
{
    public interface ICommand<out TResponse> : IRequest<TResponse>
    {
    }
}

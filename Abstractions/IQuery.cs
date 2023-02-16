using MediatR;

namespace SimpleLibraryApi.Abstractions
{
    public interface IQuery<out TResponse> : IRequest<TResponse>
    {
    }
}

using MediatR;

namespace SimpleLibraryApi.Application.Abstractions
{
    public interface IQuery<out TResponse> : IRequest<TResponse> { }
    public interface IQuery : IRequest { }
}

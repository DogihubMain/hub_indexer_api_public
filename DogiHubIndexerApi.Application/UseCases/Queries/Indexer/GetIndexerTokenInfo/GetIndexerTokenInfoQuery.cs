using MediatR;

namespace DogiHubIndexerApi.Application.UseCases.Queries.Indexer.GetIndexerTokenInfo
{
    public record GetIndexerTokenInfoQuery : IRequest<GetIndexerTokenInfoResponse>
    {
        public required string Tick { get; init; }
    }
}

using DogiHubIndexerApi.Application.Entities;
using DogiHubIndexerApi.Application.UseCases.Queries.Indexer.GetIndexerTransaction;

namespace DogiHubIndexerApi.Application.Extensions;

public static class InscriptionTransferContractExtensions
{
    public static GetIndexerTransactionResponse ToGetIndexerTransactionResponse(
        this InscriptionTransferEntity inscriptionTransfer)
    {
        return new GetIndexerTransactionResponse()
        {
            Amount = inscriptionTransfer.Inscription?.TokenContent?.amt,
            Date = inscriptionTransfer.Date.LocalDateTime,
            Type = GetTransactionType(inscriptionTransfer.InscriptionTransferType),
            BlockHeight = inscriptionTransfer!.BlockNumber,
            InscriptionId = inscriptionTransfer.InscriptionId,
            TransactionId = inscriptionTransfer.TransactionHash,
            Address = GetTransactionAddress(inscriptionTransfer),
            TokenTick = inscriptionTransfer.Inscription?.TokenContent?.tick,
            From = GetTransactionFrom(inscriptionTransfer),
            To = GetTransactionTo(inscriptionTransfer)
        };
    }

    private static string? GetTransactionTo(
        InscriptionTransferEntity inscriptionTransferEntity)
    {
        if (inscriptionTransferEntity.InscriptionTransferType == InscriptionTransferType.PENDING_TRANSFER
            || inscriptionTransferEntity.InscriptionTransferType == InscriptionTransferType.PENDING_DNS
            || inscriptionTransferEntity.InscriptionTransferType == InscriptionTransferType.PENDING_NFT
            || inscriptionTransferEntity.InscriptionTransferType == InscriptionTransferType.PENDING_DOGEMAP
           )
        {
            return inscriptionTransferEntity?.Receiver;
        }

        return null;
    }

    private static string? GetTransactionFrom(
        InscriptionTransferEntity inscriptionTransferEntity)
    {
        if (inscriptionTransferEntity.InscriptionTransferType == InscriptionTransferType.PENDING_TRANSFER
            || inscriptionTransferEntity.InscriptionTransferType == InscriptionTransferType.PENDING_DNS
            || inscriptionTransferEntity.InscriptionTransferType == InscriptionTransferType.PENDING_NFT
            || inscriptionTransferEntity.InscriptionTransferType == InscriptionTransferType.PENDING_DOGEMAP
           )
        {
            return inscriptionTransferEntity?.Sender;
        }

        return null;
    }

    private static string? GetTransactionAddress(
        InscriptionTransferEntity inscriptionTransferEntity)
    {
        if (inscriptionTransferEntity.InscriptionTransferType == InscriptionTransferType.DEPLOY
            || inscriptionTransferEntity.InscriptionTransferType == InscriptionTransferType.MINT
            || inscriptionTransferEntity.InscriptionTransferType == InscriptionTransferType.INSCRIBE_TRANSFER)
        {
            return inscriptionTransferEntity?.Receiver;
        }

        return null;
    }


    private static string GetTransactionType(InscriptionTransferType inscriptionTransferType)
    {
        switch (inscriptionTransferType)
        {
            case InscriptionTransferType.DEPLOY:
                return "deploy";
            case InscriptionTransferType.MINT:
                return "mint";
            case InscriptionTransferType.INSCRIBE_TRANSFER:
                return "inscribe-transfer";
            case InscriptionTransferType.PENDING_TRANSFER:
                return "transfer";
            case InscriptionTransferType.PENDING_DNS:
                return "dns";
            case InscriptionTransferType.PENDING_NFT:
                return "nft";
            case InscriptionTransferType.PENDING_DOGEMAP:
                return "dogemap";
            default:
                return "UNKNOWN";
        }
    }
}
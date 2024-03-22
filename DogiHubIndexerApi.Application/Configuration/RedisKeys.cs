using DogiHubIndexerApi.Application.Entities;

namespace DogiHubIndexerApi.Application.Configuration
{
    public static class RedisKeys
    {
        //InscriptionTransfer:{transactionHash}
        public const string InscriptionTransferHashKeyFormat = "it:{0}";
        
        //InscriptionTransfer:Block:{blockNumber}
        public const string InscriptionTransferByBlockKeyFormat = "it:b:{0}";
        
        //"Inscription:{0}"
        public const string InscriptionKeyFormat = "i:{0}";
        
        //Block:ReadModels:LastSync
        public const string LastReadModelsBlockSyncFormat = "b:rm:l";

        //UserBalance:{shortInscriptionType}:{address}"
        public const string UserBalanceKeyFormat = "ub:{0}:{1}";

        //BalanceDetail:{address}:{tick}"
        public const string BalanceDetailKeyFormat = "bd:{0}:{1}";

        public static string GetLastReadModelsBlockSyncKey()
        {
            return LastReadModelsBlockSyncFormat;
        }

        //"Token:{0}"
        public const string TokenInfoKeyFormat = "t:{0}";

        public static string GetUserBalanceKey(InscriptionTypeEntity inscriptionType, string address)
        {
            var shortInscriptionType = GetShortInscriptionType(inscriptionType);

            return string.Format(UserBalanceKeyFormat, shortInscriptionType, address);
        }
        
        public static string GetBalanceDetailKey(string address, string tick)
        {
            return string.Format(BalanceDetailKeyFormat, address, tick);
        }

        public static string GetTokenInfoKey(string tick)
        {
            return string.Format(TokenInfoKeyFormat, tick);
        }
        
        public static string GetInscriptionTransferHashKey(string transactionHash)
        {
            return string.Format(InscriptionTransferHashKeyFormat, transactionHash);
        }
        
        public static string GetInscriptionKey(string inscriptionId)
        {
            return string.Format(InscriptionKeyFormat, inscriptionId);
        }
        
        public static string GetInscriptionTransferByBlockKey(int blockNumber)
        {
            return string.Format(InscriptionTransferByBlockKeyFormat, blockNumber);
        }

        private static string GetShortInscriptionType(InscriptionTypeEntity inscriptionTypeEnum)
        {
            return inscriptionTypeEnum switch
            {
                InscriptionTypeEntity.Dns => "d",
                InscriptionTypeEntity.Nft => "n",
                InscriptionTypeEntity.Dogemap => "m",
                InscriptionTypeEntity.Token => "t",
                _ => throw new ArgumentException("Invalid string value for inscription type", nameof(InscriptionTypeEntity))
            };
        }
    }
}

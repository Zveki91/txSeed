using System;

namespace Application.TransactionProducer.Models
{
    public class Transaction
    {
        public long TransactionID { get; set; }

        public decimal Amount { get; set; }

        public decimal BalanceBefore { get; set; }

        public decimal BalanceAfter { get; set; }

        public decimal VirtualBalanceBefore { get; set; }

        public decimal VirtualBalanceAfter { get; set; }

        public decimal BonusBalanceBefore { get; set; }

        public decimal BonusBalanceAfter { get; set; }

        public int TransactionTypeID { get; set; }

        public DateTime TransactionDate { get; set; }

        public long? WalletID { get; set; }
        public string? Comment { get; set; }

        public string? GameCode { get; set; }

        public int? PspID { get; set; }

        public Guid? ApcoReferenceID { get; set; }

        public long? ExchangeRateID { get; set; }

        public DateTime ModifiedOn { get; set; }

        public string ModifiedBy { get; set; } = null!;

        public int? ModifiedByUserID { get; set; }

        public decimal? RealPercentage { get; set; }

        public decimal? BonusPercentage { get; set; }

        public string? SessionId { get; set; }

        public long? OriginalTransactionID { get; set; }

        public int? ApprovalStatus { get; set; }

        public decimal? TransactionFee { get; set; }

        public decimal OriginalAmount { get; set; }

        public long? OriginalCurrencyID { get; set; }

        public string? GameHandID { get; set; }

        public int? PaymentAccountID { get; set; }

        public decimal BonusAmount { get; set; }

        public decimal OriginalBonusAmount { get; set; }

        public decimal OriginalBonusBefore { get; set; }

        public decimal OriginalBonusAfter { get; set; }

        public decimal OriginalBalanceBefore { get; set; }

        public decimal OriginalBalanceAfter { get; set; }

        public int? TransactionProviderType { get; set; }


        public string? ProviderTransactionID { get; set; }

        public string? SkrillUniqueID { get; set; }

        public decimal? FeeAmount { get; set; }

        public decimal? OriginalFeeAmount { get; set; }

        public decimal JackpotContribution { get; set; }

        public decimal? SatisfiedAmount { get; set; }

        public bool ProcessAutomaticly { get; set; }

        public int? PaymentProfilePaymentMethodID { get; set; }

        public int? ClientApiId { get; set; }

        public long? TrigerBonusID { get; set; }

        public bool IsDirect { get; set; }

        public int? PaymentLimitProfilePaymentLimitPerDayID { get; set; }

        public int? WithdrawalPendingTime { get; set; }

        public decimal? WithdrawalPremiumFee { get; set; }

        public bool IsAdmin { get; set; }

        public int WithdrawalRequestActionType { get; set; }

        public string? BankName { get; set; }

        public bool? IsPaid { get; set; }

        public DateTime? IsPaidDate { get; set; }

        public Guid? GroupGuid { get; set; }

        public bool IsPostponed { get; set; }

        public bool MarkedAsSuspicious { get; set; }

        public int? TransactionReasonTypeID { get; set; }

        public decimal? GatewayFeeAmount { get; set; }

        public decimal? OriginalGatewayFeeAmount { get; set; }

        public decimal? GameFeeAmount { get; set; }

        public decimal? OriginalGameFeeAmount { get; set; }

        public int TransactionClassID { get; set; }

        public decimal? B2DRatio { get; set; }

        public decimal? B2RRatio { get; set; }

        public string? IpAddress { get; set; }

        public decimal? AmountToSettle { get; set; }

        public long? PaymentCurrencyID { get; set; }

        public decimal? PaymentCurrencyAmount { get; set; }

        public string? ProviderRoundID { get; set; }

        public int? PlatformID { get; set; }

        public int? NumberOfCombinations { get; set; }

        public string Username { get; set; } = null!;

        public string Hash { get; set; } = null!;

        public string Fullname { get; set; } = null!;

        public int? PaymentGateway { get; set; }

        public int? PaymentMethod { get; set; }

        public int? TransactionUserBonus { get; set; }

        public int? SBTransactionDetails { get; set; }

        public int? TransactionData { get; set; }

        public long UserId { get; set; }

        public DateTime EventDate { get; set; }

        public DateTime EventDateISO { get; set; }

        public string? IPAddress { get; set; }

        public string? Browser { get; set; }

        public string? OnlineToken { get; set; }

        public string? EmailTemplateParameters { get; set; }

        public BalanceDetails BalanceDetails { get; set; } = null!;

        public TransactionDetails TransactionDetails { get; set; } = null!;

        public bool? IsFromEndGame { get; set; }

        public string? PreviousHash { get; set; }
    }

    public class BalanceDetails
    {
        public float BonusBalancePCBefore { get; set; }
        public float BonusBalancePCAfter { get; set; }
        public float BalancePCBefore { get; set; }
        public float BalancePCAfter { get; set; }
    }

    public class TransactionDetails
    {
        public int TransactionID { get; set; }
        public int OriginalTransactionID { get; set; }
        public float RealAmountPC { get; set; }
        public float BonusAmountPC { get; set; }
    }
}

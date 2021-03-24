using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.TransactionProducer.Models;
using Bogus;
using Npgsql;

namespace Application.TransactionProducer.DataGenerator
{
    public class CreateTransactions
    {
        public static async Task<List<Transaction>> Create(int numberOfBatches)
        {
            var players =await getPlayerInfoDto();
            var txFaker = new Faker<Transaction>()
                .RuleFor(x => x.TransactionID, x => x.UniqueIndex)
                .RuleFor(x => x.Amount, x => Math.Round(x.Random.Decimal(1, 15000),2))
                .RuleFor(x => x.BalanceBefore, x => 0)
                .RuleFor(x => x.BalanceAfter, x => 0)
                .RuleFor(x => x.VirtualBalanceBefore, x => 0)
                .RuleFor(x => x.VirtualBalanceAfter, x => 0)
                .RuleFor(x => x.BonusBalanceBefore, x => 0)
                .RuleFor(x => x.BonusBalanceAfter, x => 0)
                .RuleFor(x => x.TransactionTypeID, x => x.PickRandom(new List<int> {3, 4, 8}))
                .RuleFor(x => x.TransactionDate, x => x.Date.Past())
                .RuleFor(x => x.WalletID, x => x.PickRandom(players.Select(p => p.WalletId).ToList()))
                .RuleFor(x => x.Comment, x => x.Random.Word())
                .RuleFor(x => x.GameCode, x => x.PickRandom(new List<string?> {"BtoBetSport", "Nesto Drugo"}))
                .RuleFor(x => x.PspID, x => null)
                .RuleFor(x => x.ApcoReferenceID, x => null)
                .RuleFor(x => x.ExchangeRateID, x => null)
                .RuleFor(x => x.ModifiedOn, x => x.Date.Between(new DateTime(2020, 1, 1), new DateTime(2021, 12, 31)))
                .RuleFor(x => x.ModifiedBy, x => x.PickRandom(players.Select(p => p.UserName).ToList()))
                .RuleFor(x => x.ModifiedByUserID, x => x.PickRandom(players.Select(p => p.WalletId).ToList()))
                .RuleFor(x => x.RealPercentage, x => null)
                .RuleFor(x => x.BonusPercentage, x => null)
                .RuleFor(x => x.SessionId, x => x.Random.AlphaNumeric(13))
                .RuleFor(x => x.OriginalTransactionID, x => null)
                .RuleFor(x => x.ApprovalStatus, x => x.PickRandom(new List<int?> {null, 1, 2, 3, 4, 8, 15}))
                .RuleFor(x => x.TransactionFee, x => 0)
                .RuleFor(x => x.OriginalAmount, x => 0)
                .RuleFor(x => x.OriginalCurrencyID, x => 0)
                .RuleFor(x => x.GameHandID, x => null)
                .RuleFor(x => x.PaymentAccountID, x => 0)
                .RuleFor(x => x.BonusAmount, x => 0)
                .RuleFor(x => x.OriginalBonusAmount, x => 0)
                .RuleFor(x => x.OriginalBonusBefore, x => 0)
                .RuleFor(x => x.OriginalBonusAfter, x => 0)
                .RuleFor(x => x.OriginalBalanceBefore, x => 0)
                .RuleFor(x => x.OriginalBalanceAfter, x => 0)
                .RuleFor(x => x.TransactionProviderType, x => x.PickRandom(new List<int?> {1, 3, 6, 8}))
                .RuleFor(x => x.ProviderTransactionID, x => null)
                .RuleFor(x => x.SkrillUniqueID, x => null)
                .RuleFor(x => x.FeeAmount, x => 0)
                .RuleFor(x => x.OriginalFeeAmount, x => 0)
                .RuleFor(x => x.JackpotContribution, x => 0)
                .RuleFor(x => x.SatisfiedAmount, x => 0)
                .RuleFor(x => x.ProcessAutomaticly, x => false)
                .RuleFor(x => x.PaymentProfilePaymentMethodID, x => 0)
                .RuleFor(x => x.ClientApiId, x => null)
                .RuleFor(x => x.TrigerBonusID, x => null)
                .RuleFor(x => x.IsDirect, x => false)
                .RuleFor(x => x.PaymentLimitProfilePaymentLimitPerDayID, x => null)
                .RuleFor(x => x.WithdrawalPendingTime, x => null)
                .RuleFor(x => x.WithdrawalPremiumFee, x => null)
                .RuleFor(x => x.IsAdmin, x => false)
                .RuleFor(x => x.WithdrawalRequestActionType, x => 0)
                .RuleFor(x => x.BankName, x => string.Empty)
                .RuleFor(x => x.IsPaid, x => x.Random.Bool())
                .RuleFor(x => x.IsPaidDate, x => x.PickRandom(new List<DateTime?> {null, DateTime.Now}))
                .RuleFor(x => x.GroupGuid, x => null)
                .RuleFor(x => x.IsPostponed, x => false)
                .RuleFor(x => x.MarkedAsSuspicious, x => x.Random.Bool())
                .RuleFor(x => x.TransactionReasonTypeID, x => null)
                .RuleFor(x => x.GatewayFeeAmount, x => 0)
                .RuleFor(x => x.OriginalGatewayFeeAmount, x => 0)
                .RuleFor(x => x.GameFeeAmount, x => 0)
                .RuleFor(x => x.OriginalGameFeeAmount, x => 0)
                .RuleFor(x => x.TransactionClassID, x => x.PickRandom(new List<int> {1, 2, 3}))
                .RuleFor(x => x.B2DRatio, x => null)
                .RuleFor(x => x.B2RRatio, x => null)
                .RuleFor(x => x.IpAddress, x => x.Internet.Ip())
                .RuleFor(x => x.AmountToSettle, x => 0)
                .RuleFor(x => x.PaymentCurrencyID, x => null)
                .RuleFor(x => x.PaymentCurrencyAmount, x => 0)
                .RuleFor(x => x.ProviderRoundID, x => null)
                .RuleFor(x => x.PlatformID, x => null)
                .RuleFor(x => x.NumberOfCombinations, x => 0)
                .RuleFor(x => x.Username, x => x.Person.UserName)
                .RuleFor(x => x.Hash, x => " ")
                .RuleFor(x => x.Fullname, x => x.Person.FullName)
                .RuleFor(x => x.PaymentGateway, x => null)
                .RuleFor(x => x.PaymentMethod, x => null)
                .RuleFor(x => x.TransactionUserBonus, x => null)
                .RuleFor(x => x.SBTransactionDetails, x => null)
                .RuleFor(x => x.TransactionData, x => null)
                .RuleFor(x => x.UserId, x => x.PickRandom(players.Select(p => p.Id).ToList()))
                .RuleFor(x => x.EventDate, x => x.Date.Recent())
                .RuleFor(x => x.EventDateISO, x => x.Date.Recent())
                .RuleFor(x => x.IPAddress, x => x.Internet.Ip())
                .RuleFor(x => x.Browser, x => "Chrome")
                .RuleFor(x => x.OnlineToken, x => x.Random.AlphaNumeric(40))
                .RuleFor(x => x.EmailTemplateParameters, x => null)
                .RuleFor(x => x.BalanceDetails, x => new BalanceDetails
                {
                    BonusBalancePCBefore = 0,
                    BonusBalancePCAfter = 0,
                    BalancePCBefore = 0,
                    BalancePCAfter = 0
                })
                .RuleFor(x => x.TransactionDetails, x => new TransactionDetails
                {
                    TransactionID = 0,
                    OriginalTransactionID = 0,
                    RealAmountPC = 0,
                    BonusAmountPC = 0
                })
                .RuleFor(x => x.IsFromEndGame, x => false)
                .RuleFor(x => x.PreviousHash, x => "");

            return new List<Transaction>(txFaker.Generate(numberOfBatches * 10000));
        }

        private static async Task<List<PlayerInfoDto>> getPlayerInfoDto()
        {
            var plist = new List<PlayerInfoDto>();
            var connectionString = "Server=localhost;Port=5432;Database=BtoBet;User Id=postgres;Password=postgres;";
            await using var conn = new NpgsqlConnection(connectionString);
            await conn.OpenAsync();
            await using var cmd = new NpgsqlCommand("Select user_id,user_name,wallet_id from public.user",conn);
            await cmd.PrepareAsync();
            var dr = await cmd.ExecuteReaderAsync();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    plist.Add(new PlayerInfoDto
                    {
                        Id = (long)dr[0],
                        UserName = (string)dr[1],
                        WalletId = (int)dr[2]
                    });
                }
            }
            await conn.CloseAsync();
            return plist;
        }
    }
}

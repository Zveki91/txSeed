using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Application.TransactionProducer.DataGenerator;
using Application.TransactionProducer.Models;
using Npgsql;
using NpgsqlTypes;

namespace Application.TransactionProducer.Seeds
{
    public class SeedTransactions
    {
        private const string InsertTransactionSql = @"
                INSERT INTO public.transaction
                (
                    transaction_id,amount,balance_before,balance_after,virtual_balance_before,virtual_balance_after,
                 bonus_balance_before,bonus_balance_after,transaction_type_id,transaction_date,wallet_id,comment,
                 game_code,psp_id,apco_reference_id,exchange_rate_id,modified_on,modified_by,modified_by_user_id,
                 real_percentage,bonus_percentage,session_id,original_transaction_id,approval_status,transaction_fee,
                 original_amount,original_currency_id,game_hand_id,payment_account_id,bonus_amount,original_bonus_amount,
                 original_bonus_before,original_bonus_after,original_balance_before,original_balance_after,
                 transaction_provider_type_id,provider_transaction_id,skrill_unique_id,fee_amount,original_fee_amount,
                 jackpot_contribution,satisfied_amount,process_automaticly,payment_profile_payment_method_id,client_api_id,
                 triger_bonus_id,is_direct,payment_limit_profile_payment_limit_per_day_id,withdrawal_pending_time,
                 withdrawal_premium_fee,is_admin,withdrawal_request_action_type,bank_name,is_paid,is_paid_date,
                 group_guid,is_postponed,marked_as_suspicious,user_name,full_name,transaction_reason_type_id,
                 gateway_fee_amount,original_gateway_fee_amount,game_fee_amount,original_game_fee_amount,
                 transaction_class_id,b2d_ratio,b2r_ratio,ip_address,amount_to_settle,payment_currency_id,
                 payment_currency_amount,provider_round_id,platform_id,number_of_combinations,payment_gateway_id,
                 payment_method_id,transaction_user_bonus,sb_transactions_details,transaction_data,user_id,
                 event_date,event_date_iso,browser,online_token,email_template_parameters,balance_details,
                 transaction_details,is_from_end_game
                )
                VALUES
                (
                    @transaction_id,@amount,@balance_before,@balance_after,@virtual_balance_before,@virtual_balance_after,
                 @bonus_balance_before,@bonus_balance_after,@transaction_type_id,@transaction_date,@wallet_id,@comment,
                 @game_code,@psp_id,@apco_reference_id,@exchange_rate_id,@modified_on,@modified_by,@modified_by_user_id,
                 @real_percentage,@bonus_percentage,@session_id,@original_transaction_id,@approval_status,@transaction_fee,
                 @original_amount,@original_currency_id,@game_hand_id,@payment_account_id,@bonus_amount,@original_bonus_amount,
                 @original_bonus_before,@original_bonus_after,@original_balance_before,@original_balance_after,
                 @transaction_provider_type_id,@provider_transaction_id,@skrill_unique_id,@fee_amount,@original_fee_amount,
                 @jackpot_contribution,@satisfied_amount,@process_automaticly,@payment_profile_payment_method_id,@client_api_id,
                 @triger_bonus_id,@is_direct,@payment_limit_profile_payment_limit_per_day_id,@withdrawal_pending_time,
                 @withdrawal_premium_fee,@is_admin,@withdrawal_request_action_type,@bank_name,@is_paid,@is_paid_date,@group_guid,
                 @is_postponed,@marked_as_suspicious,@user_name,@full_name,@transaction_reason_type_id,@gateway_fee_amount,
                 @original_gateway_fee_amount,@game_fee_amount,@original_game_fee_amount,@transaction_class_id,@b2d_ratio,
                 @b2r_ratio,@ip_address,@amount_to_settle,@payment_currency_id,@payment_currency_amount,@provider_round_id,
                 @platform_id,@number_of_combinations,@payment_gateway_id,@payment_method_id,@transaction_user_bonus,
                 @sb_transactions_details,@transaction_data,@user_id,@event_date,@event_date_iso,@browser,@online_token,
                 @email_template_parameters,@balance_details,@transaction_details,@is_from_end_game
                ) ON CONFLICT DO NOTHING;";

        public static async Task SeedTx()
        {
            var timer = new Stopwatch();
            var batchTimer = new Stopwatch();
            List<Transaction> txList;
            try
            {
                timer.Start();
                // Batch size is set to 10,000
                txList =await  CreateTransactions.Create(1);
                timer.Stop();
                Console.WriteLine($"Tx creation took {timer.ElapsedMilliseconds}ms");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            var batchSize = 10000;
            var batches = txList.Count / batchSize;

            var connectionString = "Server=localhost;Port=5432;Database=BtoBet;User Id=postgres;Password=postgres;";

            await using var conn = new NpgsqlConnection(connectionString);

            conn.Open();

            for (int k = 0; k <= batches; k++)
            {
                var batch = txList.Skip(k * batchSize).Take(batchSize).ToList();

                Console.WriteLine($"Batch {k} started");
                batchTimer.Start();
                for (int i = 0; i < batch.Count; i++)
                {
                  await  using var cmd = new NpgsqlCommand(InsertTransactionSql, conn);
                    AddParameters(cmd, batch[i]);

                    await cmd.PrepareAsync();
                    await cmd.ExecuteNonQueryAsync();
                }

                batchTimer.Stop();
                Console.WriteLine($"Batch {k} completed in {batchTimer.Elapsed}ms.");
            }

            await conn.CloseAsync();

            Console.WriteLine("Tx seed done");
        }

        private static void AddParameters(NpgsqlCommand cmd, Transaction item)
        {
            cmd.Parameters.AddWithValue("transaction_id", item.TransactionID);
            cmd.Parameters.AddWithValue("amount", item.Amount);
            cmd.Parameters.AddWithValue("balance_before", item.BalanceBefore);
            cmd.Parameters.AddWithValue("balance_after", item.BalanceAfter);
            cmd.Parameters.AddWithValue("virtual_balance_before", item.VirtualBalanceBefore);
            cmd.Parameters.AddWithValue("virtual_balance_after", item.VirtualBalanceAfter);
            cmd.Parameters.AddWithValue("bonus_balance_before", item.BonusBalanceBefore);
            cmd.Parameters.AddWithValue("bonus_balance_after", item.BonusBalanceAfter);
            cmd.Parameters.AddWithValue("transaction_type_id", NpgsqlDbType.Integer,
                item.TransactionTypeID);
            cmd.Parameters.AddWithValue("transaction_date", item.TransactionDate);
            cmd.Parameters.AddWithValue("wallet_id", (object) item.WalletID ?? DBNull.Value);
            cmd.Parameters.AddWithValue("comment", (object) item.Comment ?? DBNull.Value);
            cmd.Parameters.AddWithValue("game_code", (object) item.GameCode ?? DBNull.Value);
            cmd.Parameters.AddWithValue("psp_id", (object) item.PspID ?? DBNull.Value);
            cmd.Parameters.AddWithValue("apco_reference_id",
                (object) item.ApcoReferenceID ?? DBNull.Value);
            cmd.Parameters.AddWithValue("exchange_rate_id",
                (object) item.ExchangeRateID ?? DBNull.Value);
            cmd.Parameters.AddWithValue("modified_on", item.ModifiedOn);
            cmd.Parameters.AddWithValue("modified_by", item.ModifiedBy);
            cmd.Parameters.AddWithValue("modified_by_user_id",
                (object) item.ModifiedByUserID ?? DBNull.Value);
            cmd.Parameters.AddWithValue("real_percentage",
                (object) item.RealPercentage ?? DBNull.Value);
            cmd.Parameters.AddWithValue("bonus_percentage",
                (object) item.BonusPercentage ?? DBNull.Value);
            cmd.Parameters.AddWithValue("session_id", (object) item.SessionId ?? DBNull.Value);
            cmd.Parameters.AddWithValue("original_transaction_id",
                (object) item.OriginalTransactionID ?? DBNull.Value);
            cmd.Parameters.AddWithValue("approval_status",
                (object) item.ApprovalStatus ?? DBNull.Value);
            cmd.Parameters.AddWithValue("transaction_fee",
                (object) item.TransactionFee ?? DBNull.Value);
            cmd.Parameters.AddWithValue("original_amount", item.OriginalAmount);
            cmd.Parameters.AddWithValue("original_currency_id",
                (object) item.OriginalCurrencyID ?? DBNull.Value);
            cmd.Parameters.AddWithValue("game_hand_id", (object) item.GameHandID ?? DBNull.Value);
            cmd.Parameters.AddWithValue("payment_account_id",
                (object) item.PaymentAccountID ?? DBNull.Value);
            cmd.Parameters.AddWithValue("bonus_amount", item.BonusAmount);
            cmd.Parameters.AddWithValue("original_bonus_amount", item.OriginalBonusAmount);
            cmd.Parameters.AddWithValue("original_bonus_before", item.OriginalBonusBefore);
            cmd.Parameters.AddWithValue("original_bonus_after", item.OriginalBonusAfter);
            cmd.Parameters.AddWithValue("original_balance_before", item.OriginalBalanceBefore);
            cmd.Parameters.AddWithValue("original_balance_after", item.OriginalBalanceAfter);
            cmd.Parameters.AddWithValue("transaction_provider_type_id",
                (object) item.TransactionProviderType ?? DBNull.Value);
            cmd.Parameters.AddWithValue("provider_transaction_id",
                (object) item.ProviderTransactionID ?? DBNull.Value);
            cmd.Parameters.AddWithValue("skrill_unique_id",
                (object) item.SkrillUniqueID ?? DBNull.Value);
            cmd.Parameters.AddWithValue("fee_amount", (object) item.FeeAmount ?? DBNull.Value);
            cmd.Parameters.AddWithValue("original_fee_amount",
                (object) item.OriginalFeeAmount ?? DBNull.Value);
            cmd.Parameters.AddWithValue("jackpot_contribution", item.JackpotContribution);
            cmd.Parameters.AddWithValue("satisfied_amount",
                (object) item.SatisfiedAmount ?? DBNull.Value);
            cmd.Parameters.AddWithValue("process_automaticly", item.ProcessAutomaticly);
            cmd.Parameters.AddWithValue("payment_profile_payment_method_id",
                (object) item.PaymentProfilePaymentMethodID ?? DBNull.Value);
            cmd.Parameters.AddWithValue("client_api_id", (object) item.ClientApiId ?? DBNull.Value);
            cmd.Parameters.AddWithValue("triger_bonus_id", (object) item.TrigerBonusID ?? DBNull.Value);
            cmd.Parameters.AddWithValue("is_direct", item.IsDirect);
            cmd.Parameters.AddWithValue("payment_limit_profile_payment_limit_per_day_id",
                (object) item.PaymentLimitProfilePaymentLimitPerDayID ?? DBNull.Value);
            cmd.Parameters.AddWithValue("withdrawal_pending_time",
                (object) item.WithdrawalPendingTime ?? DBNull.Value);
            cmd.Parameters.AddWithValue("withdrawal_premium_fee",
                (object) item.WithdrawalPremiumFee ?? DBNull.Value);
            cmd.Parameters.AddWithValue("is_admin", item.IsAdmin);
            cmd.Parameters.AddWithValue("withdrawal_request_action_type",
                item.WithdrawalRequestActionType);
            cmd.Parameters.AddWithValue("bank_name", (object) item.BankName ?? DBNull.Value);
            cmd.Parameters.AddWithValue("is_paid", (object) item.IsPaid ?? DBNull.Value);
            cmd.Parameters.AddWithValue("is_paid_date", (object) item.IsPaidDate ?? DBNull.Value);
            cmd.Parameters.AddWithValue("group_guid", (object) item.GroupGuid ?? DBNull.Value);
            cmd.Parameters.AddWithValue("is_postponed", item.IsPostponed);
            cmd.Parameters.AddWithValue("marked_as_suspicious", item.MarkedAsSuspicious);
            cmd.Parameters.AddWithValue("transaction_reason_type_id",
                (object) item.TransactionReasonTypeID ?? DBNull.Value);
            cmd.Parameters.AddWithValue("gateway_fee_amount",
                (object) item.GatewayFeeAmount ?? DBNull.Value);
            cmd.Parameters.AddWithValue("original_gateway_fee_amount",
                (object) item.OriginalGatewayFeeAmount ?? DBNull.Value);
            cmd.Parameters.AddWithValue("game_fee_amount", (object) item.GameFeeAmount ?? DBNull.Value);
            cmd.Parameters.AddWithValue("original_game_fee_amount",
                (object) item.OriginalGameFeeAmount ?? DBNull.Value);
            cmd.Parameters.AddWithValue("transaction_class_id", item.TransactionClassID);
            cmd.Parameters.AddWithValue("b2d_ratio", (object) item.B2DRatio ?? DBNull.Value);
            cmd.Parameters.AddWithValue("b2r_ratio", (object) item.B2RRatio ?? DBNull.Value);
            cmd.Parameters.AddWithValue("ip_address", (object) item.IpAddress ?? DBNull.Value);
            cmd.Parameters.AddWithValue("amount_to_settle",
                (object) item.AmountToSettle ?? DBNull.Value);
            cmd.Parameters.AddWithValue("payment_currency_id",
                (object) item.PaymentCurrencyID ?? DBNull.Value);
            cmd.Parameters.AddWithValue("payment_currency_amount",
                (object) item.PaymentCurrencyAmount ?? DBNull.Value);
            cmd.Parameters.AddWithValue("provider_round_id",
                (object) item.ProviderRoundID ?? DBNull.Value);
            cmd.Parameters.AddWithValue("platform_id", (object) item.PlatformID ?? DBNull.Value);
            cmd.Parameters.AddWithValue("number_of_combinations",
                (object) item.NumberOfCombinations ?? DBNull.Value);
            cmd.Parameters.AddWithValue("user_name", (object) item.Username ?? DBNull.Value);
            cmd.Parameters.AddWithValue("full_name", (object) item.Fullname ?? DBNull.Value);
            cmd.Parameters.AddWithValue("payment_gateway_id",
                (object) item.PaymentGateway ?? DBNull.Value);
            cmd.Parameters.AddWithValue("payment_method_id",
                (object) item.PaymentMethod ?? DBNull.Value);
            cmd.Parameters.AddWithValue("transaction_user_bonus",
                (object) item.TransactionUserBonus ?? DBNull.Value);
            cmd.Parameters.AddWithValue("sb_transactions_details",
                (object) item.SBTransactionDetails ?? DBNull.Value);
            cmd.Parameters.AddWithValue("transaction_data",
                (object) item.TransactionData ?? DBNull.Value);
            cmd.Parameters.AddWithValue("user_id", (object) item.UserId ?? DBNull.Value);
            cmd.Parameters.AddWithValue("event_date", item.EventDate);
            cmd.Parameters.AddWithValue("event_date_iso", item.EventDateISO);
            cmd.Parameters.AddWithValue("browser", (object) item.Browser ?? DBNull.Value);
            cmd.Parameters.AddWithValue("online_token", (object) item.OnlineToken ?? DBNull.Value);
            cmd.Parameters.AddWithValue("email_template_parameters",
                (object) item.EmailTemplateParameters ?? DBNull.Value);
            cmd.Parameters.AddWithValue("balance_details", NpgsqlDbType.Jsonb,
                (object) item.BalanceDetails ?? DBNull.Value);
            cmd.Parameters.AddWithValue("transaction_details", NpgsqlDbType.Jsonb,
                (object) item.TransactionDetails ?? DBNull.Value);
            cmd.Parameters.AddWithValue("is_from_end_game",
                (object) item.IsFromEndGame ?? DBNull.Value);
        }
    }
}

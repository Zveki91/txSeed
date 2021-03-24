using System;
using System.Threading.Tasks;
using Application.TransactionProducer.DataGenerator;
using Npgsql;

namespace Application.TransactionProducer.Seeds
{
    public class SeedUsers
    {
        private const string _insertUserSql = @"
                INSERT INTO public.user
                (
                    user_id,first_name,last_name,user_name,device,note,token,parent_id,email,mobile_phone,phone,
                 registered_on,currency,trigger_bonus_id,portal_url,middle_name,gender,title,civil_identification_number,
                 language,second_last_name,account_upgrade_status,block_game_play,block_login,block_transaction,is_active,
                 b_tag,marketing_code,generate_password,date_of_birth,brand_id,event_date,event_date_iso,ip_address,wallet_id,
                 browser,online_token,address,city,country,province,province_id,provider_id
                )
                VALUES
                (
                    @user_id,@first_name,@last_name,@user_name,@device,@note,@token,@parent_id,@email,@mobile_phone,@phone,
                 @registered_on,@currency,@trigger_bonus_id,@portal_url,@middle_name,@gender,@title,@civil_identification_number,
                 @language,@second_last_name,@account_upgrade_status,@block_game_play,@block_login,@block_transaction,@is_active,
                 @b_tag,@marketing_code,@generate_password,@date_of_birth,@brand_id,@event_date,@event_date_iso,@ip_address,
                 @wallet_id,@browser,@online_token,@address,@city,@country,@province,@province_id,@provider_id
                ) ON CONFLICT DO NOTHING ;";
        public static async Task SeedPlayers()
        {
            var players =await CreateFakePlayers.Create(20);
            var connectionString = "Server=localhost;Port=5432;Database=BtoBet;User Id=postgres;Password=postgres;";
            await using var conn = new NpgsqlConnection(connectionString);
            await conn.OpenAsync();
            for (int i = 0; i < players.Count; i++)
            {
                var registeredUser = players[i];
               await  using var cmd = new NpgsqlCommand(_insertUserSql, conn);

                cmd.Parameters.AddWithValue("user_id", registeredUser.UserId);
                cmd.Parameters.AddWithValue("first_name", registeredUser.FirstName);
                cmd.Parameters.AddWithValue("last_name", registeredUser.LastName);
                cmd.Parameters.AddWithValue("user_name", registeredUser.Username);
                cmd.Parameters.AddWithValue("device", registeredUser.Device);
                cmd.Parameters.AddWithValue("note", registeredUser.Note);
                cmd.Parameters.AddWithValue("token", registeredUser.Token);
                cmd.Parameters.AddWithValue("parent_id", (object) registeredUser.ParentID ?? DBNull.Value);
                cmd.Parameters.AddWithValue("email", registeredUser.Email);
                cmd.Parameters.AddWithValue("mobile_phone", registeredUser.MobilePhone);
                cmd.Parameters.AddWithValue("phone", registeredUser.Phone);
                cmd.Parameters.AddWithValue("registered_on", registeredUser.RegisteredOn);
                cmd.Parameters.AddWithValue("currency", (object) registeredUser.Currency ?? DBNull.Value);
                cmd.Parameters.AddWithValue("trigger_bonus_id", (object) registeredUser.TriggerBonusID ?? DBNull.Value);
                cmd.Parameters.AddWithValue("portal_url", registeredUser.PortalUrl);
                cmd.Parameters.AddWithValue("middle_name", registeredUser.MidleName);
                cmd.Parameters.AddWithValue("gender", registeredUser.Gender);
                cmd.Parameters.AddWithValue("title", (object) registeredUser.Title ?? DBNull.Value);
                cmd.Parameters.AddWithValue("civil_identification_number", registeredUser.CIN);
                cmd.Parameters.AddWithValue("language", (object) registeredUser.Language ?? DBNull.Value);
                cmd.Parameters.AddWithValue("second_last_name", registeredUser.SecondLastName ?? " ");
                cmd.Parameters.AddWithValue("account_upgrade_status", registeredUser.AccountUpgradeStatus);
                cmd.Parameters.AddWithValue("block_game_play", registeredUser.BlockGamePlay);
                cmd.Parameters.AddWithValue("block_login", registeredUser.BlockLogin);
                cmd.Parameters.AddWithValue("block_transaction", registeredUser.BlockTransaction);
                cmd.Parameters.AddWithValue("is_active", registeredUser.IsActive);
                cmd.Parameters.AddWithValue("b_tag", (object) registeredUser.Btag ?? DBNull.Value);
                cmd.Parameters.AddWithValue("marketing_code", (object) registeredUser.MarketingCode ?? DBNull.Value);
                cmd.Parameters.AddWithValue("generate_password", registeredUser.GeneratePassword);
                cmd.Parameters.AddWithValue("date_of_birth", registeredUser.DateOfBirth);
                cmd.Parameters.AddWithValue("brand_id", registeredUser.BrandId);
                cmd.Parameters.AddWithValue("event_date", registeredUser.EventDate);
                cmd.Parameters.AddWithValue("event_date_iso", registeredUser.EventDateISO);
                cmd.Parameters.AddWithValue("ip_address", registeredUser.IPAddress);
                cmd.Parameters.AddWithValue("wallet_id", (object) registeredUser.WalletID ?? DBNull.Value);
                cmd.Parameters.AddWithValue("browser", registeredUser.Browser);
                cmd.Parameters.AddWithValue("online_token", registeredUser.OnlineToken);
                cmd.Parameters.AddWithValue("address", registeredUser.Address);
                cmd.Parameters.AddWithValue("province", registeredUser.Province);
                cmd.Parameters.AddWithValue("city", registeredUser.City);
                cmd.Parameters.AddWithValue("country", (object) registeredUser.Country ?? DBNull.Value);
                cmd.Parameters.AddWithValue("provider_id", (object) registeredUser.ProviderId ?? DBNull.Value);
                cmd.Parameters.AddWithValue("province_id", (object) registeredUser.ProvinceId ?? DBNull.Value);
                cmd.Parameters.AddWithValue("zip_code", (object) registeredUser.ZipCode ?? DBNull.Value);

                await cmd.PrepareAsync();
                await cmd.ExecuteNonQueryAsync();
            }

            await conn.CloseAsync();
            Console.WriteLine("User seed done");
        }
    }
}

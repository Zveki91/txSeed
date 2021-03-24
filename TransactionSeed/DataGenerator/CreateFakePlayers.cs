using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.TransactionProducer.Models;
using Bogus;
using Bogus.Extensions.Portugal;

namespace Application.TransactionProducer.DataGenerator
{
    public class CreateFakePlayers
    {
        public static async Task<List<User>> Create(int amount)
        {
            var playerFaker = new Faker<User>()
                .RuleFor(x => x.PortalUrl, x => x.Internet.DomainName())
                .RuleFor(x => x.FirstName, x => x.Person.FirstName)
                .RuleFor(x => x.LastName, x => x.Person.LastName)
                .RuleFor(x => x.Username, x => x.Person.UserName)
                .RuleFor(x => x.Device, x => x.Commerce.Product())
                .RuleFor(x => x.Note, x => x.Lorem.Word())
                .RuleFor(x => x.Token, x => x.Random.AlphaNumeric(42))
                .RuleFor(x => x.ParentID, x => x.Random.Number())
                .RuleFor(x => x.Email, x => x.Person.Email)
                .RuleFor(x => x.MobilePhone, x => x.Phone.PhoneNumber())
                .RuleFor(x => x.Phone, x => x.Phone.PhoneNumber())
                .RuleFor(x => x.RegisteredOn, x => x.Date.Between(new DateTime(2005, 1, 1), new DateTime(2021, 3, 25)))
                .RuleFor(x => x.Currency, x => x.Finance.Currency().Description)
                .RuleFor(x => x.TriggerBonusID, x => x.Random.Int())
                .RuleFor(x => x.MidleName, x => x.Person.FirstName)
                .RuleFor(x => x.Gender, x => "male")
                .RuleFor(x => x.Title, x => "mr")
                .RuleFor(x => x.CIN, x => x.Person.Nif())
                .RuleFor(x => x.Language, x => null)
                .RuleFor(x => x.SecondLastName, x => null)
                .RuleFor(x => x.AccountUpgradeStatus, x => 1)
                .RuleFor(x => x.BlockLogin, x => false)
                .RuleFor(x => x.BlockTransaction, x => false)
                .RuleFor(x => x.BlockGamePlay, x => false)
                .RuleFor(x => x.IsActive, x => true)
                .RuleFor(x => x.Btag, x => null)
                .RuleFor(x => x.MarketingCode, x => null)
                .RuleFor(x => x.GeneratePassword, false)
                .RuleFor(x => x.DateOfBirth, x => x.Person.DateOfBirth)
                .RuleFor(x => x.BrandId, x => x.Random.Int())
                .RuleFor(x => x.UserId, x => x.UniqueIndex)
                .RuleFor(x => x.WalletID, x => x.Random.Int(5000, 13500))
                .RuleFor(x => x.EventDate, x => x.Date.Recent())
                .RuleFor(x => x.EventDateISO, x=> x.Date.Recent())
                .RuleFor(x => x.IPAddress, x => x.Internet.IpAddress().ToString())
                .RuleFor(x => x.Browser, x => "Chrome")
                .RuleFor(x => x.OnlineToken, x => x.Random.AlphaNumeric(45))
                .RuleFor(x => x.IsFromEndGame, false)
                .RuleFor(x => x.Address, x => x.Address.StreetAddress())
                .RuleFor(x => x.City, x => x.Address.City())
                .RuleFor(x => x.Province, x => x.Address.State())
                .RuleFor(x => x.Country, x => x.Address.Country())
                .RuleFor(x => x.ProviderId, x => x.Random.ListItem(new List<int?>() {0, 10}))
                .RuleFor(x => x.ProvinceId,
                    x => x.Random.ListItem(new List<int?>() {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15}))
                .RuleFor(x => x.ZipCode, x => x.Address.ZipCode());

            var players = playerFaker.Generate(amount);
            return players;
        }


    }
}

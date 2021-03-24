using System;

namespace Application.TransactionProducer.Models
{
    public class User
    {
        public string PortalUrl { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Device { get; set; } = null!;
        public string Note { get; set; } = null!;
        public string Token { get; set; } = null!;
        public int? ParentID { get; set; }
        public string Email { get; set; } = null!;
        public string MobilePhone { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public DateTime RegisteredOn { get; set; }
        public string? Currency { get; set; }
        public long? TriggerBonusID { get; set; }
        public string MidleName { get; set; } = null!;
        public string Gender { get; set; } = null!;
        public string? Title { get; set; }
        public string CIN { get; set; } = null!;
        public string? Language { get; set; }
        public string SecondLastName { get; set; } = null!;
        public int AccountUpgradeStatus { get; set; }
        public bool BlockGamePlay { get; set; }
        public bool BlockLogin { get; set; }
        public bool BlockTransaction { get; set; }
        public bool IsActive { get; set; }
        public string? Btag { get; set; }
        public string? MarketingCode { get; set; }
        public bool GeneratePassword { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int BrandId { get; set; }
        public long UserId { get; set; }
        public int? WalletID { get; set; }
        public DateTime EventDate { get; set; }
        public DateTime EventDateISO { get; set; }
        public string IPAddress { get; set; } = null!;
        public string Browser { get; set; } = null!;
        public string OnlineToken { get; set; } = null!;
        public bool IsFromEndGame { get; set; }
        public string Address { get; set; } = null!;
        public string Province { get; set; } = null!;
        public string City { get; set; } = null!;
        public string? Country { get; set; }
        public int? ProviderId { get; set; }
        public int? ProvinceId { get; set; }
        public string? ZipCode { get; set; }
    }
}

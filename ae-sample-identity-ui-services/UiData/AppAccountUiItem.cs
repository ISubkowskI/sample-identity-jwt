﻿using System.Security.Claims;

namespace Ae.Sample.Identity.Ui.UiData
{
    public sealed record AppAccountUiItem
    {
        public Guid Id { get; set; } = Guid.Empty;
        public string EmailAddress { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool IsLocked { get; set; } = false;
        //public IEnumerable<Claim> Claims { get; set; } = [];
        public DateTimeOffset CreatedAtUtc { get; set; } = DateTimeOffset.MinValue;
        public DateOnly EmploymentDate { get; set; } = DateOnly.MinValue;
        public DateOnly EmploymentExpiredDate { get; set; } = DateOnly.MinValue;
        public DateTimeOffset LastLoginUtc { get; set; } = DateTimeOffset.MinValue;
        public DateTimeOffset LastPasswordChangeUtc { get; set; } = DateTimeOffset.MinValue;
        public DateTimeOffset PasswordExpiredOnUtc { get; set; } = DateTimeOffset.MaxValue;
        public DateTimeOffset EmailVerifiedOnUtc { get; set; } = DateTimeOffset.MinValue;
    }
}

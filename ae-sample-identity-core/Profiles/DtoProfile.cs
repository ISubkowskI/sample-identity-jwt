﻿using AutoMapper;
using Ae.Sample.Identity.Data;
using Ae.Sample.Identity.Dtos;
using Ae.Sample.Identity.Utils;

namespace Ae.Sample.Identity.Profiles
{
    public sealed class DtoProfile : Profile
    {
        public DtoProfile()
        {
            CreateMap<AccountRegistrationIncomingDto, AccountRegistration>();
            CreateMap<AccountRegistrationResult, AccountRegistrationOutgoingDto>();

            CreateMap<AppClaim, AppClaimOutgoingDto>();
            CreateMap<AppClaimIncomingDto, AppClaim>();
            
            CreateMap<AccountIdentity, AccountIdentityOutgoingDto>()
                .ConvertUsing((source, destination, context) =>
                {
                    return new AccountIdentityOutgoingDto
                    {
                        Id = source.Id,
                        EmailAddress = source.EmailAddress,
                        DisplayName = source.DisplayName,
                        Description = source.Description,
                        IsLocked = source.IsLocked,
                        ClaimsJson = ClaimUtils.SerializeToJson(source.Claims),
                        CreatedAtUtc = source.CreatedAtUtc,
                        EmploymentDate = source.EmploymentDate,
                        EmploymentExpiredDate = source.EmploymentExpiredDate,
                        LastLoginUtc = source.LastLoginUtc,
                        LastPasswordChangeUtc = source.LastPasswordChangeUtc,
                        PasswordExpiredOnUtc = source.PasswordExpiredOnUtc,
                        EmailVerifiedOnUtc = source.EmailVerifiedOnUtc
                    };
                });
        }
    }
}

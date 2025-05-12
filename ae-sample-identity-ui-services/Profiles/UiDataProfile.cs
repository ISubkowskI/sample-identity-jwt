using AutoMapper;
using Ae.Sample.Identity.Ui.Dtos;
using Ae.Sample.Identity.Ui.UiData;

namespace Ae.Sample.Identity.Ui.Profiles
{
    public class UiDataProfile : Profile
    {
        public UiDataProfile()
        {
            CreateMap<AppClaimDto, AppClaimUiItem>();
            CreateMap<AppClaimUiItem, AppClaimDto>();

            CreateMap<AppAccountDto, AppAccountUiItem>();
        }
    }
}

using AutoMapper;
using TCAPArchive.Shared.Domain;
using TCAPArchive.Shared.ViewModels;

public class TCAPMappingProfile : Profile
{
    public TCAPMappingProfile()
    {
        CreateMap<RegisterViewModel, ApplicationUser>();
    }
}
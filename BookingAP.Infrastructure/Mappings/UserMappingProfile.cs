using AutoMapper;
using BookingAp.Contract.Users;
using BookingAP.Domain.Users;

namespace BookingAP.Infrastructure.Mappings
{
    public sealed class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateProjection<User, UserResponse>()
                .ForMember(dest => dest.Id, src => src.MapFrom(p => p.Id.Value))
                .ForMember(dest => dest.Email, src => src.MapFrom(p => p.Email.Value))
                .ForMember(dest => dest.FirstName, src => src.MapFrom(p => p.FirstName.Value))
                .ForMember(dest => dest.LastName, src => src.MapFrom(p => p.LastName.Value));

            CreateProjection<User, UserIdDto>()
              .ForMember(dest => dest.Id, src => src.MapFrom(p => p.Id.Value));
        }
    }
}

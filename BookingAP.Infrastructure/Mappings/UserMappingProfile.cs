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
                .ForMember(src => src.Id, dest => dest.MapFrom(p => p.Id.Value))
                .ForMember(src => src.Email, dest => dest.MapFrom(p => p.Email.Value))
                .ForMember(src => src.FirstName, dest => dest.MapFrom(p => p.FirstName.Value))
                .ForMember(src => src.LastName, dest => dest.MapFrom(p => p.LastName.Value));

            CreateProjection<User, UserIdDto>()
              .ForMember(src => src.Id, dest => dest.MapFrom(p => p.Id.Value));
        }
    }
}

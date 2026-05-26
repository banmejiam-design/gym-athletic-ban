using AutoMapper;
using GymManagement.API.DTOs;
using GymManagement.Domain.Entities;

namespace GymManagement.API.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Member, MemberDto>();
        CreateMap<CreateMemberDto, Member>();
        CreateMap<UpdateMemberDto, Member>();

        CreateMap<Trainer, TrainerDto>();
        CreateMap<CreateTrainerDto, Trainer>();
        CreateMap<UpdateTrainerDto, Trainer>();

        CreateMap<GymClass, GymClassDto>()
            .ForMember(dest => dest.TrainerName,
                opt => opt.MapFrom(src => src.Trainer != null
                    ? $"{src.Trainer.FirstName} {src.Trainer.LastName}"
                    : string.Empty));
        CreateMap<CreateGymClassDto, GymClass>();
        CreateMap<UpdateGymClassDto, GymClass>();

        CreateMap<Enrollment, EnrollmentDto>()
            .ForMember(dest => dest.MemberName,
                opt => opt.MapFrom(src => src.Member != null
                    ? $"{src.Member.FirstName} {src.Member.LastName}"
                    : string.Empty))
            .ForMember(dest => dest.ClassName,
                opt => opt.MapFrom(src => src.GymClass != null ? src.GymClass.Name : string.Empty));

        CreateMap<Membership, MembershipDto>()
            .ForMember(dest => dest.MemberName,
                opt => opt.MapFrom(src => src.Member != null
                    ? $"{src.Member.FirstName} {src.Member.LastName}"
                    : string.Empty));
        CreateMap<CreateMembershipDto, Membership>();
        CreateMap<UpdateMembershipDto, Membership>();
    }
}

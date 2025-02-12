using AutoMapper;
using dataAccess.Models;
using graphQLService.DTOs.GraphQLTypes;
using graphQLService.DTOs.MLEngine;

namespace graphQLService.Assemblers
{
    /// <summary>
    /// Automapper profile file. 
    /// Types that are mapped using automapper are registered here.
    /// For field names in source that does not match field names in destination custom mapping is needed (ForMember) 
    /// </summary>
    public class MappingsProfile : Profile
    {
        public MappingsProfile()
        {
            CreateMatchHistoryMap();
            CreateCompletedEventMap();
            CreateFighterMap();
            CreatePredictedWinnerMap();
            CreateUpcomingFightMap();
            CreateUpcomingEventMap();
        }

        private void CreateMatchHistoryMap()
        {
            CreateMap<MatchHistory, MatchHistoryType>()
                .ForMember(dest => dest.BodyBlue, source => source.MapFrom(item => item.SigBodyBlue))
                .ForMember(dest => dest.BodyRed, source => source.MapFrom(item => item.SigBodyRed))
                .ForMember(dest => dest.DistanceBlue, source => source.MapFrom(item => item.SigDistanceBlue))
                .ForMember(dest => dest.DistanceRed, source => source.MapFrom(item => item.SigDistanceRed))
                .ForMember(dest => dest.HeadBlue, source => source.MapFrom(item => item.SigHeadBlue))
                .ForMember(dest => dest.HeadRed, source => source.MapFrom(item => item.SigHeadRed))
                .ForMember(dest => dest.LegBlue, source => source.MapFrom(item => item.SigLegBlue))
                .ForMember(dest => dest.LegRed, source => source.MapFrom(item => item.SigLegRed))
                .ForMember(dest => dest.StrikesBlue, source => source.MapFrom(item => item.SigStrikesBlue))
                .ForMember(dest => dest.StrikesRed, source => source.MapFrom(item => item.SigStrikesRed))
                .ForMember(dest => dest.SubBlue, source => source.MapFrom(item => item.SubAttemptBlue))
                .ForMember(dest => dest.SubRed, source => source.MapFrom(item => item.SubAttemptRed))
                .ForMember(dest => dest.TdBlue, source => source.MapFrom(item => item.TakeDownBlue))
                .ForMember(dest => dest.TdRed, source => source.MapFrom(item => item.TakeDownRed))
                .ForMember(dest => dest.TotalBlue, source => source.MapFrom(item => item.TotalStrikesBlue))
                .ForMember(dest => dest.TotalRed, source => source.MapFrom(item => item.TotalStrikesRed))
                .ForMember(dest => dest.BlueImage, source => source.MapFrom(item => item.BlueImageString))
                .ForMember(dest => dest.RedImage, source => source.MapFrom(item => item.RedImageString));
        }

        private void CreateFighterMap()
        {
            CreateMap<Fighter, FighterType>()
                .ForMember(dest => dest.Image, source => source.MapFrom(item => item.ImageString))
                .ForMember(dest => dest.ImageArray, source => source.MapFrom(item => item.Image));
        }

        private void CreateCompletedEventMap()
        {
            CreateMap<CompletedEvent, CompletedEventType>()
                .ForMember(dest => dest.EventName, source => source.MapFrom(item => item.Event));
        }

        private void CreatePredictedWinnerMap()
        {
            CreateMap<PredictedWinnerResponse, PredictedWinnerType>();
        }

        private void CreateUpcomingFightMap()
        {
            CreateMap<UpcomingFight, UpcomingFightType>()
                .ForMember(dest => dest.EventName, source => source.MapFrom(item => item.Event))
                .ForMember(dest => dest.Fighter1, source => source.MapFrom(item => item.Name1))
                .ForMember(dest => dest.Fighter2, source => source.MapFrom(item => item.Name2))
                .ForMember(dest => dest.BlueImage, source => source.MapFrom(item => item.BlueImageString))
                .ForMember(dest => dest.RedImage, source => source.MapFrom(item => item.RedImageString));
        }

        private void CreateUpcomingEventMap()
        {
            CreateMap<UpcomingEvent, UpcomingEventType>()
                .ForMember(dest => dest.EventName, source => source.MapFrom(item => item.Event));
        }
    }
}

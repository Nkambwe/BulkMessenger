using AutoMapper;
using BulkMessager.Data.Entities;
using BulkMessager.Dtos;
using BulkMessager.Extensions;

namespace BulkMessager.Utils {
     public class MappingProfile : Profile {
        public MappingProfile() {

            CreateMap<Message, MessageDto>()
                .ForMember(dto => dto.Id, opt => opt.MapFrom(o => o.Id))
                .ForMember(dto => dto.Message, opt => opt.MapFrom(o => (o.Text ?? string.Empty).Trim()))
                .ForMember(dto => dto.Duration, opt => opt.MapFrom(o => Enum.GetName(typeof(MessageDuration), o.Duration)))
                .ForMember(dto => dto.StartSending, opt => opt.MapFrom(o => o.RunFrom))
                .ForMember(dto => dto.StopSending, opt => opt.MapFrom(o => o.RunTo))
                .ForMember(dto => dto.MessageInterval, opt => opt.MapFrom(o => Enum.GetName(typeof(Interval), o.Interval)))
                .ForMember(dto => dto.IntervalStatus, opt => opt.MapFrom(o => o.IntervalStatus))
                .ForMember(dto => dto.MessageApproved, opt => opt.MapFrom(o => o.IsApproved ?
                    Enum.GetName(typeof(MessageApproved), MessageApproved.Approved) :
                    Enum.GetName(typeof(MessageApproved), MessageApproved.Pending)))
                .ForMember(dto => dto.AddedBy, opt => opt.MapFrom(o => (o.AddedBy ?? string.Empty).Trim()))
                .ForMember(dto => dto.AddedOn, opt => opt.MapFrom(o => o.AddedOn))
                .ForMember(dto => dto.ModifiedBy, opt => opt.MapFrom(o => (o.LastModifiedBy ?? string.Empty).Trim()))
                .ForMember(dto => dto.ModifiedOn, opt => opt.MapFrom(o => o.LastModifiedOn))
                .ForMember(dto => dto.LastSent, opt => opt.MapFrom(o => o.LastSent))
                .ForMember(dto => dto.MessageDeleted, opt => opt.MapFrom(o => Enum.GetName(typeof(Deleted), o.IsDeleted)));

            
            CreateMap<MessageDto, Message>()
                .ForMember(msg => msg.Id, opt => opt.MapFrom(o => o.Id))
                .ForMember(msg => msg.Text, opt => opt.MapFrom(o => (o.Message ?? string.Empty).Trim()))
                .ForMember(msg => msg.Duration, opt => opt.MapFrom(o => Enum.GetName(typeof(MessageDuration), o.Duration)))
                .ForMember(msg => msg.RunFrom, opt => opt.MapFrom(o => o.StartSending))
                .ForMember(msg => msg.RunTo, opt => opt.MapFrom(o => o.StopSending))
                .ForMember(msg => msg.Interval, opt => opt.MapFrom(o => Enums.ParseEnum<Interval>(o.MessageInterval)))
                .ForMember(msg => msg.IntervalStatus, opt => opt.MapFrom(o => o.IntervalStatus))
                .ForMember(msg => msg.IsApproved,opt => opt.MapFrom(o => Enums.ParseEnum<MessageApproved>(o.MessageApproved) == MessageApproved.Approved))
                .ForMember(msg => msg.AddedBy, opt => opt.MapFrom(o => (o.AddedBy ?? string.Empty).Trim()))
                .ForMember(msg => msg.AddedOn, opt => opt.MapFrom(o => o.AddedOn))
                .ForMember(msg => msg.LastModifiedBy, opt => opt.MapFrom(o => (o.ModifiedBy ?? string.Empty).Trim()))
                .ForMember(msg => msg.LastModifiedOn, opt => opt.MapFrom(o => o.ModifiedOn))
                .ForMember(msg => msg.LastSent, opt => opt.MapFrom(o => o.LastSent))
                .ForMember(msg => msg.IsDeleted, opt => opt.MapFrom(o => Enums.ParseEnum<Deleted>(o.MessageApproved) == Deleted.YES));

        }
    }
}

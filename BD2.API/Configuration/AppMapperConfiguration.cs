using AutoMapper;
using BD2.API.Database.Entities;
using BD2.API.Models.Auth;
using BD2.API.Models.Groups;
using BD2.API.Models.Posts;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BD2.API.Configuration
{
    public class AppMapperConfiguration
    {
        public static Action<IMapperConfigurationExpression> Configuration(IConfiguration appConfig) => (IMapperConfigurationExpression cfg) =>
        {
            cfg.CreateMap<Account, UserModel>();
            cfg.CreateMap<PostUpdateModel, Post>();

            cfg.CreateMap<PostComment, PostCommentModel>();
            cfg.CreateMap<PostCommentAddModel, PostComment>()
                .ForMember(x => x.CommentDate, opt => opt.MapFrom(src => DateTime.Now));

            cfg.CreateMap<PostReaction, PostReactionModel>();
            cfg.CreateMap<PostReactionAddModel, PostReaction>()
                .ForMember(x => x.ReactionDate, opt => opt.MapFrom(src => DateTime.Now));

            cfg.CreateMap<Group, GroupModel>()
                .ForMember(
                    x => x.PacketPeopleLimit,
                    opt => opt.MapFrom(src => src.Subscription != null && src.Subscription.Packet != null ?
                        src.Subscription.Packet.PeopleLimit :
                        appConfig.GetValue<int>("AppBussinesDefault:Groups:DefaultPeopleLimit")))
                .ForMember(
                    x => x.PacketName,
                    opt => opt.MapFrom(src => src.Subscription != null && src.Subscription.Packet != null ?
                             src.Subscription.Packet.Name : null))
                .ForMember(
                    x => x.IsOpen,
                    opt => opt.MapFrom(src => src.Subscription != null && src.Subscription.Packet != null ?
                             src.Subscription.Packet.IsOpen : false))
                .ForMember(
                    x => x.PacketId,
                    opt => opt.MapFrom(src => src.Subscription != null ? (Guid?)src.Subscription.PacketId : null))
                .ForMember(
                    x => x.SubscriptionExpirationDate,
                    opt => opt.MapFrom(src => src.Subscription != null ? (DateTime?)src.Subscription.ExpirationDate : null))
                .ForMember(
                    x => x.MembersCount,
                    opt => opt.MapFrom(src => src.Members != null ? src.Members.Count() : 0))
                .ForMember(
                    x => x.PostsCount,
                    opt => opt.MapFrom(src => src.Posts != null ? src.Posts.Count() : 0));

            cfg.CreateMap<Group, GroupSimplifiedModel>()
                .ForMember(
                    x => x.IsOpen,
                    opt => opt.MapFrom(src => src.Subscription != null && src.Subscription.Packet != null ?
                             src.Subscription.Packet.IsOpen : false))
                .ForMember(
                    x => x.MembersCount,
                    opt => opt.MapFrom(src => src.Members != null ? src.Members.Count() : 0));

            cfg.CreateMap<GroupAddModel, Group>()
                .ForMember(x => x.CreatedDate, opt => opt.MapFrom(src => DateTime.Now));
        };
    }
}

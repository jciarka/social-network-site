using AutoMapper;
using BD2.API.Database.Dtos.Chat;
using BD2.API.Database.Entities;
using BD2.API.Models;
using BD2.API.Models.Abusements;
using BD2.API.Models.Auth;
using BD2.API.Models.GroupAccount;
using BD2.API.Models.Groups;
using BD2.API.Models.Packets;
using BD2.API.Models.Posts;
using BD2.API.Models.Subcriptions;
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
                    opt => opt.MapFrom(src => src.Posts != null ? src.Posts.Count() : 0))
                .ForMember(
                    x => x.IsPacketExpired,
                    opt => opt.MapFrom(src => src.Subscription != null && src.Subscription.ExpirationDate < DateTime.Now))
                .ForMember(
                    x => x.IsPacketExpired,
                    opt => opt.MapFrom(src => src.Subscription != null && src.Subscription.ExpirationDate < DateTime.Now && 
                    src.Members.Count() > appConfig.GetValue<int>("AppBussinesDefault:Groups:DefaultPeopleLimit")));

            cfg.CreateMap<Group, GroupSimplifiedModel>()
                .ForMember(
                    x => x.IsOpen,
                    opt => opt.MapFrom(src => src.Subscription != null && src.Subscription.Packet != null ?
                             src.Subscription.Packet.IsOpen : false))
                .ForMember(
                    x => x.MembersCount,
                    opt => opt.MapFrom(src => src.Members != null ? src.Members.Count() : 0))
                .ForMember(
                    x => x.IsPacketExpired,
                    opt => opt.MapFrom(src => src.Subscription != null && src.Subscription.ExpirationDate < DateTime.Now))
                .ForMember(
                    x => x.IsPacketExpired,
                    opt => opt.MapFrom(src => src.Subscription != null && src.Subscription.ExpirationDate < DateTime.Now &&
                    src.Members.Count() > appConfig.GetValue<int>("AppBussinesDefault:Groups:DefaultPeopleLimit")));

            cfg.CreateMap<GroupAddModel, Group>()
                .ForMember(x => x.CreatedDate, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(x => x.Members, opt => opt.MapFrom(src => new List<GroupAccount>()))
                .ForMember(x => x.LastPostDate, opt => opt.MapFrom(src => DateTime.Now));

            cfg.CreateMap<AddSubscriptionPacketModel, PacketSubscription>();

            cfg.CreateMap<PacketSubscription, PacketSubcriptionsModel>()
                .ForMember(x => x.Name, opt => opt.MapFrom(src => src.Packet != null ? src.Packet.Name : null))
                .ForMember(x => x.PeopleLimit, opt => opt.MapFrom(src => src.Packet != null ? src.Packet.PeopleLimit : 0))
                .ForMember(x => x.GroupsLimit, opt => opt.MapFrom(src => src.Packet != null ? src.Packet.GroupsLimit : 0))
                .ForMember(x => x.FreeSlots, opt => opt.MapFrom(src => countSubcriptionFreeSlots(src)));

            cfg.CreateMap<GroupAccount, GroupAccountModel>()
                .ForMember(x => x.Firstname, opt => opt.MapFrom(src => src.Account != null ? src.Account.Firstname : ""))
                .ForMember(x => x.Lastname, opt => opt.MapFrom(src => src.Account != null ? src.Account.Lastname : ""))
                .ForMember(x => x.Email, opt => opt.MapFrom(src => src.Account.Email != null ? src.Account.Email : ""));

            cfg.CreateMap<GroupAccountAddModel, GroupAccount>();

            cfg.CreateMap<CreateChatModel, Chat>()
                .ForMember(x => x.Id, opt => opt.MapFrom(src => new Guid()))
                .ForMember(x => x.Members, opt => opt.MapFrom(src => new List<ChatAccount>()))
                .ForMember(x => x.LastPostDate, opt => opt.MapFrom(src => DateTime.Now));

            cfg.CreateMap<UpdateChatModel, Chat>();

            cfg.CreateMap<PostAbusement, PostAbusementModel>()
                .ForMember(x => x.Firstname, opt => opt.MapFrom(src => src.Account != null ? src.Account.Firstname : null))
                .ForMember(x => x.Lastname, opt => opt.MapFrom(src => src.Account != null ? src.Account.Lastname : null))
                .ForMember(x => x.PostTitle, opt => opt.MapFrom(src => src.Post != null ? src.Post.Title : null));


            cfg.CreateMap<AddPostAbusementModel, PostAbusement>()
                .ForMember(x => x.AbusementDate, opt => opt.MapFrom(src => DateTime.Now));

        };

        private static int countSubcriptionFreeSlots(PacketSubscription src)
        {
            if (src.Packet == null) return 0;
            return src.Groups != null ? src.Packet.GroupsLimit - src.Groups.Count() : src.Packet.GroupsLimit;
        }
    }
}

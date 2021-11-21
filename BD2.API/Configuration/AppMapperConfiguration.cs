using AutoMapper;
using BD2.API.Database.Entities;
using BD2.API.Models.Auth;
using BD2.API.Models.Posts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BD2.API.Configuration
{
    public class AppMapperConfiguration
    {
        public static Action<IMapperConfigurationExpression> Configuration => (IMapperConfigurationExpression cfg) =>
        {
            cfg.CreateMap<Account, UserModel>();
            cfg.CreateMap<PostUpdateModel, Post>();

            cfg.CreateMap<PostComment, PostCommentModel>();
            cfg.CreateMap<PostCommentAddModel, PostComment>()
                .ForMember(x => x.CommentDate, opt => opt.MapFrom(src => DateTime.Now));

            cfg.CreateMap<PostReaction, PostReactionModel>();
            cfg.CreateMap<PostReactionAddModel, PostReaction>()
                .ForMember(x => x.ReactionDate, opt => opt.MapFrom(src => DateTime.Now));

        };
    }
}

using AutoMapper;
using Blog.Data.Entities;
using Blog.Data.Entities.Identity;
using Blog.Model;
using File = Blog.Data.Entities.File;

namespace Blog.Web.Extensions.AutoMapper
{
    public class Mappings : Profile, IProfile
    {
        public Mappings()
        {
            CreateMap<Article, ArticleModel>()
                .ForMember(m => m.ChannelName, opt => opt.MapFrom(s => s.Channel.Title))
                .ForMember(m => m.Files, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(m => m.Channel, opt => opt.Ignore())
                .ForMember(m => m.Files, opt => opt.Ignore())
                .ForMember(m => m.ChannelId, opt => opt.MapFrom(s => s.ChannelId));
            CreateMap<Article, ArticleListModel>()
                .ForMember(m => m.ChannelName, opt => opt.MapFrom(s => s.Channel.Title))
                .ForMember(m => m.UserName, opt => opt.MapFrom(s => s.User.UserName))
                .ForMember(m => m.CommentCount, opt => opt.MapFrom(s => s.Comments.Count))
                .ForMember(m => m.File, opt => opt.MapFrom(s => s.Files.FirstOrDefault()));
            CreateMap<Channel, ChannelModel>().ReverseMap();
            CreateMap<Comment, CommentModel>().ReverseMap();
            CreateMap<File, FileModel>().ReverseMap();
            CreateMap<User, UserModel>().ReverseMap();
            CreateMap<Role, RoleModel>().ReverseMap();
        }
    }
}
using AutoMapper;
using Forum.Data.Entities;
using Forum.Models;

namespace Forum.Data
{
    public class ForumProfile : Profile
    {
        public ForumProfile()
        {
            this.CreateMap<User, UserModel>().ReverseMap();
            this.CreateMap<Question, QuestionModel>().ReverseMap();
            this.CreateMap<Answer, AnswerModel>().ReverseMap();
            this.CreateMap<Comment, CommentModel>().ReverseMap();
            this.CreateMap<User, RegisterUserModel>().ReverseMap();

        }
    }
}

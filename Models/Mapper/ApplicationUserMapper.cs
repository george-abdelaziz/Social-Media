using Model.Dto.ApplicationUserDTO;
using Model.Entity;

namespace Model.Mapper
{
    public static class ApplicationUserMapper
    {
        public static ApplicationUserDto ToApplicationUserDto(this ApplicationUser applicationUser, string token = "")
        {
            return new ApplicationUserDto
            {
                UserName = applicationUser.UserName,
                Email = applicationUser.Email,
                Token = token,
                //Posts = applicationUser.Posts.Select(post => post.ToPostDto()).ToList(),
                //Comments = applicationUser.Comments.Select(comment => comment.ToCommentDto()).ToList()
            };
        }
    }
}

namespace SocialNetwork.BLL.Models
{
    // This interface made to reuse some of view code for friends list, but keep the passwords safe from outside access.
    public interface IUser
    {
        string Email { get; set; }
        string FavoriteBook { get; set; }
        string FavoriteMovie { get; set; }
        string FirstName { get; set; }
        int Id { get; }
        string LastName { get; set; }
        string Photo { get; set; }
    }
}
namespace KillerAppClassLibrary.Classes
{
    public class Comment
    {
        private int Id { get; }
        private User User { get; }
        private string Content { get; }

        //Used when creating a new comment.
        public Comment(User user, string content)
        {
            User = user;
            Content = content;
        }

        //Used when getting a comment from the database.
        public Comment(int id, User user, string content)
        {
            Id = id;
            User = user;
            Content = content;
        }
    }
}

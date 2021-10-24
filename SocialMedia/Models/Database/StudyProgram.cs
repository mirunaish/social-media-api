namespace SocialMedia.Models.Database
{
    public class StudyProgram
    {
        public int Id { get; set; }
        public Major Major1 { get; set; }
        public Major Major2 { get; set; }
        public Major Minor1 { get; set; }
        public Major Minor2 { get; set; }
        public Major Modification { get; set; }
    }
}
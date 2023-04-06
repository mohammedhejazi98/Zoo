namespace Zoo.Models
{
    public class Dragon
    {
        #region Public Properties

        public int Age { get; set; }
        public string FavouriteFood { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public Room? Room { get; set; }
        public int RoomId { get; set; }
        #endregion Public Properties
    }
}
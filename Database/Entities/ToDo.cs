namespace ToDoList.API.Database.Entities
{
    public class ToDo
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
    }
}

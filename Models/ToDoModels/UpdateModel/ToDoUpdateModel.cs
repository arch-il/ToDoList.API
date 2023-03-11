namespace ToDoList.API.Models.ToDoModels.UpdateModel
{
    public class ToDoUpdateModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
    }
}

namespace RealTimeTodo.Web.Model
{
    public class TodoListMinimal 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Pending { get; set; }
        public int Completed { get; set; }
    }
}
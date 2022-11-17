namespace ToDos.MinimalAPI;

public interface IToDoService
{
    void Create(ToDo toDo);
    void Delete(Guid id);
    ToDo GedById(Guid id);
    List<ToDo> GetAll();
    void Update(ToDo toDo);
}

public class ToDoService : IToDoService
{
    public ToDoService()
    {
        var sampleToDo = new ToDo { Value = "Learn MinimalAPI" };
        _toDo[sampleToDo.Id] = sampleToDo;
    }
    private readonly Dictionary<Guid, ToDo> _toDo = new();
    public ToDo GedById(Guid id)
    {
        return _toDo.GetValueOrDefault(id);
    }
    public List<ToDo> GetAll()
    {
        return _toDo.Values.ToList();
    }
    public void Create(ToDo toDo)
    {
        if (toDo is null) { return; }
        _toDo[toDo.Id] = toDo;
    }
    public void Update(ToDo toDo)
    {
        var existingToDo = GedById(toDo.Id);
        if (existingToDo is null) 
        { 
            return; 
        }
        _toDo[toDo.Id] = toDo;
    }
    public void Delete(Guid id)
    {
        _toDo.Remove(id);
    }
}


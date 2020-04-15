using ToDo.Domain.Interfaces;
namespace ToDo.Repositories.Repositories
{
    public static class ModuleInitializer
    {
        public static void Init()
        {
            ToDo.IoC.IoC.Register(typeof(IToDoRepository), typeof(ToDoRepository));
            ToDo.IoC.IoC.Register(typeof(IUserRepository), typeof(UserRepository));
        }
    }
}
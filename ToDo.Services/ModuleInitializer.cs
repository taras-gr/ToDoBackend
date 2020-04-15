using ToDo.Domain;
using ToDo.Domain.Interfaces;
using ToDo.Services.Interfaces;

namespace ToDo.Services
{
    public static class ModuleInitializer
    {
        public static void Init()
        {
            ToDo.IoC.IoC.Register(typeof(IUserService), typeof(UserService));
            ToDo.IoC.IoC.Register(typeof(IToDoService), typeof(ToDoService));
        }
    }
}
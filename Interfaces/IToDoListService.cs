using ToDoList.API.Models;
using ToDoList.API.Models.ToDoModels.CreateModel;
using ToDoList.API.Models.ToDoModels.UpdateModel;
using ToDoList.API.Models.ToDoModels.ViewModel;

namespace ToDoList.API.Interfaces
{
    public interface IToDoListService
    {
        Task<CustomResponseModel<IEnumerable<ToDoViewModel>>> GetAll();
        Task<CustomResponseModel<bool>> Create(ToDoCreateModel toDoCreateModel);
        Task<CustomResponseModel<ToDoViewModel>> Update(ToDoUpdateModel toDoUpdateModel);
        Task<CustomResponseModel<bool>> Delete(int id);
        Task<CustomResponseModel<int>> GetCompletedTaskCount();

    }
}

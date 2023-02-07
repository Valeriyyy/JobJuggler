using Domain.Models.Enums;

namespace Application.Services.Interfaces;
public interface IPicklistService
{
    public Task<List<EnumModel>> GetAll();
}

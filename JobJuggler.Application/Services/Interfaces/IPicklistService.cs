using JobJuggler.Domain.Models.Enums;

namespace JobJuggler.Application.Services.Interfaces;
public interface IPicklistService {
    public Task<List<EnumModel>> GetAll();
}

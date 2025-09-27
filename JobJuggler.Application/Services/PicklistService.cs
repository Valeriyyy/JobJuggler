using JobJuggler.Domain.Enums;
using JobJuggler.Application.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using JobJuggler.Persistence;

namespace JobJuggler.Application.Services;
public class PicklistService : IPicklistService {
    private readonly DataContext _context;
    private readonly IUserAccessor _userAccessor;

    public PicklistService(DataContext context, IUserAccessor userAccessor)
    {
        _context = context;
        _userAccessor = userAccessor;
    }

    // public async Task<List<EnumModel>> GetAll() {
    //     var options = await _context.EnumModels.ToListAsync();
    //
    //     var aa = _userAccessor.GetUsername();
    //     var id = _userAccessor.GetUserId();
    //
    //     return options;
    // }
}

using JobJuggler.Domain.Enums;
using JobJuggler.Application.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using JobJuggler.Persistence;

namespace JobJuggler.Application.Services;
public class PicklistService : IPicklistService {
    private readonly DataContext _context;

    public PicklistService(DataContext context) {
        _context = context;
    }

    public async Task<List<EnumModel>> GetAll() {
        var options = await _context.EnumModels.ToListAsync();

        return options;
    }
}

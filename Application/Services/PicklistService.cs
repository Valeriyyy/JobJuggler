using Application.Services.Interfaces;
using Persistence;
using Microsoft.EntityFrameworkCore;
using Domain.Models.Enums;

namespace Application.Services;
public class PicklistService : IPicklistService
{
    private readonly DataContext _context;

    public PicklistService(DataContext context)
    {
        _context = context;
    }

    public async Task<List<EnumModel>> GetAll()
    {
        var options = await _context.EnumModels.ToListAsync();

        return options;
    }
}

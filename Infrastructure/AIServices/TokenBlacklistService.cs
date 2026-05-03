using Application.Interfaces;
using Domain;
using Luvia.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

public class TokenBlacklistService : ITokenBlacklistService
{
    private readonly LuviaDbContext _context;

    public TokenBlacklistService(LuviaDbContext context)
    {
        _context = context;
    }

    public async Task RevokeTokenAsync(string token)
    {
        await _context.RevokedTokens.AddAsync(new RevokedToken
        {
            Token = token,
            RevokedAt = DateTime.UtcNow
        });

        await _context.SaveChangesAsync();
    }

    public async Task<bool> IsTokenRevokedAsync(string token)
    {
        return await _context.RevokedTokens
            .AnyAsync(t => t.Token == token);
    }
}
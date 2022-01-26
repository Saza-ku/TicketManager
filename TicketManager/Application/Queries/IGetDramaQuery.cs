using System;
using TicketManager.Application.Entities;

namespace TicketManager.Application.Queries
{
    public interface IDramaQuery
    {
        Drama Get(int id);
        Drama Add(Drama drama);
        Drama Edit(Drama drama);
        Drama Delete(Drama drama);
    }
}

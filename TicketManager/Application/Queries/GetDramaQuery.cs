using System;
using TicketManager.Application.Entities;

namespace TicketManager.Application.Queries
{
    public class DramaQuery : IDramaQuery
    {
        public Drama Get(int id)
        {
            return new Drama();
        }

        public Drama Add(Drama drama)
        {
            return drama;
        }

        public Drama Edit(Drama drama)
        {
            return drama;
        }

        public Drama Delete(Drama drama)
        {
            return drama;
        }
    }
}

using TicketManager.Data;

public class DbInitializer
{
    public static void Initialize(TicketContext context)
    {
        // これね
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();
    }
}
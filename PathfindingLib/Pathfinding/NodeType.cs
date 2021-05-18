namespace PathfindingLib.Pathfinding
{
    public enum NodeType
    {
        NotVisited,
        Visited,
        NotAvailable, // Impassable (wall)
        Frontier,
        Active,
        Start,
        Goal
        //Neibghor,
    }
}

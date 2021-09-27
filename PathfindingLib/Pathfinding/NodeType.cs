namespace PathfindingLib.Pathfinding
{
    public enum NodeType
    {
        NotVisited,
        Visited,
        Forest,       // Movement cost incresed #8bad85
        NotAvailable, // Impassable (wall)
        Frontier,
        Active,
        //Start,
        //Goal
        //Neibghor,
    }
}

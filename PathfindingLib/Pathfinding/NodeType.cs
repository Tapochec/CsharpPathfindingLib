namespace PathfindingLib.Pathfinding
{
    public enum NodeType
    {
        NotVisited,
        Visited,
        Forest,       // Movement cost incresed
        Wall,         // Impassable
        Frontier,
        Active,
        //Start,
        //Goal
        //Neibghor,
    }
}

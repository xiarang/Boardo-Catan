using System;
using System.Collections;

[Serializable]
public class Tile
{
    public int identify;
    public string resource;
    public int number;
}

[Serializable]
public class Tiles
{
    public Tile[] board;
}
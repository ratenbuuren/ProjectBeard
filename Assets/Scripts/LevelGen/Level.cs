public class Level {
    private int width, height;
    private Tile[,] tiles;

    public Level(int width, int height) {
        this.width = width;
        this.height = height;
        tiles = new Tile[width, height];
    }

    public void set(IntVector2 pos, TerrainType terrainType) {
        tiles[pos.x, pos.y] = new Tile(terrainType);
    }

    public Tile get(int x, int y) {
        return tiles[x, y];
    }

    public void line(IntVector2 start, int length, Direction dir, TerrainType terrainType) {
        IntVector2 pos = start;
        for (var i = 0; i < length; i++) {
            if (!inbounds(pos)) {
                break;
            }
            set(pos, terrainType);
            if (dir == Direction.Horizontal) {
                pos.x += 1;
            } else {
                pos.y += 1;
            }
        }
    }

    public void fill(IntVector2 start, IntVector2 end, TerrainType terrainType) {
        IntVector2 pos = start;
        for (var y = start.y; y <= end.y; y++) {
            pos.y = y;
            line(pos, end.y, Direction.Horizontal, terrainType);
        }
    }

    private bool inbounds(IntVector2 pos) {
        return 0 <= pos.x && pos.x < width && 0 <= pos.y && pos.y < height;
    }

    public int Width {
        get { return width; }
    }

    public int Height {
        get { return height; }
    }
}
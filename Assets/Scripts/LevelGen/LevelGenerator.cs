public class LevelGenerator {
   public static Level generate(int width, int height) {
      Level level = new Level(width+2, height+2);
      level.fill(new IntVector2(0,0), new IntVector2(width+1, height+1), TerrainType.Ground);
      
      level.line(new IntVector2(0,0), width+2, Direction.Horizontal, TerrainType.Edge);
      level.line(new IntVector2(0,height+1), width+2, Direction.Horizontal, TerrainType.Edge);
      
      level.line(new IntVector2(0,0), height+2, Direction.Vertical, TerrainType.Edge);
      level.line(new IntVector2(width+1,0), height+2, Direction.Vertical, TerrainType.Edge);
      
      return level;
   }
}
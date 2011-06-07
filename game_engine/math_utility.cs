namespace GameEngine {
  
  public static class MathUtility
  {
    //-------------------------------------------------------------------------------------
    /// <summary>
    /// Converts a degrees angle into a radians angle.
    /// </summary>
    /// <param name="angle">Angle to convert.</param>
    /// <returns>Angle in radians.</returns>
    //-------------------------------------------------------------------------------------
    public static float DegreesToRadians(float angle)
    {
      return angle * ((float) System.Math.PI / 180.0f);
    }

  }
  
}

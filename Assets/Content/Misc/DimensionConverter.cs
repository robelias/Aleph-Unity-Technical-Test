namespace Content.Misc
{
    public static class DimensionConverter
    {
        public static float CmToUnityUnits(float cm)
        {
            return cm / 100f;  // Unity units
        }
    }
}


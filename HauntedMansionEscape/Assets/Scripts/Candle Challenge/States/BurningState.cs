public enum BurningState
{
    UNBURNED,
    BURNING,
    BURNING_BLUE,   // Copper Chloride
    BURNING_GREEN,  // Copper Sulfate
    BURNING_RED,    // Strontium Chloride
    BURNING_YELLOW, // Sodium Chloride
    BURNING_WHITE,  // Magnesium Sulfate
    BURNED,
    REQUIRED_NONE,  // Only used for required state
    EXTINGUISH,
}

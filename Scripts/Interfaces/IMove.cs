public interface IMove {
    // This tells us that anything implementing IMove i.e. anything that moves
    // has a Speed that we can read
    float Speed { get; }
}
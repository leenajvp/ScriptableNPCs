public interface IPlayerControls
{
    void Forward();
    void Backward();
    void Run();
    void TurnRight();
    void TurnLeft();
    void PickUp();
    void Swat();
    void Magic();

    bool isRunning { get; set; }
}

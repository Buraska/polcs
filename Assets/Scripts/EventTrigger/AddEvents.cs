namespace EventTrigger
{
    public class AddEvents : Trigger
    {
        private void Start()
        {
            foreach (var eEvent in gameEvents) GameManager.Instance.EventManager.AddEvent(eEvent);
        }
    }
}
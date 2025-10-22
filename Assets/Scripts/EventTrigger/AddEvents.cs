namespace EventTrigger
{
    public class AddEvents : Trigger
    {
        private void Start()
        {
            #if UNITY_EDITOR
            foreach (var eEvent in gameEvents) GameManager.Instance.EventManager.AddEvent(eEvent);
            #endif
        }
    }
}
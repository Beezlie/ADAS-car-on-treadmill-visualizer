namespace RosSharp.RosBridgeClient
{
    public class Float64Subscriber : Subscriber<Messages.Standard.Float64>
    {
        public float messageData;

        protected override void Start()
        {
            base.Start();
        }

        protected override void ReceiveMessage(Messages.Standard.Float64 message)
        {
            messageData = message.data;
        }
    }
}